using System;
using System.Linq;
using System.Text.RegularExpressions;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.ClientAggregate;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.ConfigurationSettings;
using ChatterBox.Domain.Extensions;
using ChatterBox.Domain.Properties;

namespace ChatterBox.Domain.Services
{
    public interface IChatService
    {
        // Users 
        Client AddClient(Guid userId, Guid clientId, string userAgent);
        void UpdateActivity(Guid userId, Guid clientId, string userAgent);
        Guid DisconnectClient(Guid clientId);

        // Rooms
        Room AddRoom(Guid userId, string roomName);
        void JoinRoom(Guid userId, Guid roomId, string inviteCode);
        void LeaveRoom(Guid userId, Guid roomId);
        void SetInviteCode(Guid userId, Guid roomId, string inviteCode);

        // Messages
        Message AddMessage(User user, Room room, string id, string content);
        Message AddMessage(string userId, string roomName, string url);

        // Owner commands
        void AddOwner(User user, User targetUser, Room targetRoom);
        void RemoveOwner(User user, User targetUser, Room targetRoom);
        void KickUser(User user, User targetUser, Room targetRoom);
        void AllowUser(User user, User targetUser, Room targetRoom);
        void UnallowUser(User user, User targetUser, Room targetRoom);
        void LockRoom(User user, Room targetRoom);
        void CloseRoom(User user, Room targetRoom);
        void OpenRoom(User user, Room targetRoom);
        void ChangeTopic(User user, Room room, string newTopic);
        void ChangeWelcome(User user, Room room, string newWelcome);
        void AppendMessage(string id, string content);

        // Admin commands
        void AddAdmin(User admin, User targetUser);
        void RemoveAdmin(User admin, User targetUser);
        void BanUser(User callingUser, User targetUser);

        //void AddAttachment(Message message, string fileName, string contentType, long size, UploadResult result);

        // Add mention
        void AddNotification(User mentionedUser, Message message, Room room, bool markAsRead);
    }

    public class ChatService : IChatService
    {
        private readonly Func<IUnitOfWork> _unitOfWork;
        private readonly IClock _clock;
        private readonly AllowRoomCreationSetting _allowRoomCreation;

        private const int NoteMaximumLength = 140;
        private const int TopicMaximumLength = 80;
        private const int WelcomeMaximumLength = 200;

        public ChatService(Func<IUnitOfWork> unitOfWork, IClock clock, AllowRoomCreationSetting allowRoomCreation)
        {
            _unitOfWork = unitOfWork;
            _clock = clock;
            _allowRoomCreation = allowRoomCreation;
        }

        public Client AddClient(Guid userId, Guid clientId, string userAgent)
        {
            using (var context = _unitOfWork())
            {
                var user = context.Repository<User>().VerifyUser(userId);

                var repository = context.Repository<Client>();

                var client = repository.GetById(clientId);
                if (client != null)
                {
                    return client;
                }

                client = new Client(clientId, user.Id, userAgent, _clock.UtcNow);

                repository.Add(client);

                context.Complete();

                return client;
            }
        }

        public void UpdateActivity(Guid userId, Guid clientId, string userAgent)
        {
            using (var context = _unitOfWork())
            {
                var now = _clock.UtcNow;

                var user = context.Repository<User>().VerifyUser(userId);
                user.UpdateStatus(UserStatus.Active);
                user.UpdateLastActivity(now);

                var repository = context.Repository<Client>();
                var client = repository.GetById(clientId);

                if (client == null)
                {
                    client = new Client(clientId, user.Id, userAgent, now);
                    repository.Add(client);
                }
                else
                {
                    client.UpdateUserAgent(userAgent);
                    client.UpdateLastActivity(now);
                }

                context.Complete();
            }
        }

        public Guid DisconnectClient(Guid clientId)
        {
            using (var context = _unitOfWork())
            {   
                var repository = context.Repository<Client>();

                var client = repository.GetById(clientId);
                
                if (client == null)
                {
                    return Guid.Empty;
                }

                var user = context.Repository<User>().GetById(client.UserId);

                if (user != null)
                {
                    user.RemoveConnectedClient(client);

                    if (!user.ConnectedClients.Any())
                    {
                        user.UpdateStatus(UserStatus.Offline);
                    }

                    repository.Remove(client);
                    context.Complete();
                }

                return user.Id;
            }
        }

        public Room AddRoom(Guid userId, string roomName)
        {
            using (var context = _unitOfWork())
            {
                var user = context.Repository<User>().VerifyUser(userId);

                if (!_allowRoomCreation && !user.IsAdmin)
                {
                    throw new Exception(LanguageResources.RoomCreationDisabled);
                }

                if (roomName.Equals("Lobby", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception(LanguageResources.RoomCannotBeNamedLobby);
                }

                if (!IsValidRoomName(roomName))
                {
                    throw new Exception(String.Format(LanguageResources.RoomInvalidName, roomName));
                }

                var repository = context.Repository<Room>();

                var room = new Room(roomName, user.Id);

                room.AddOwner(user);

                repository.Add(room);

                context.Complete();

                return room;
            }
        }

        private bool IsValidRoomName(string roomName)
        {
            return !String.IsNullOrEmpty(roomName) && Regex.IsMatch(roomName, "^[\\w-_]{1,30}$");
        }

        public void JoinRoom(Guid userId, Guid roomId, string inviteCode)
        {
            using (var context = _unitOfWork())
            {
                var user = context.Repository<User>().VerifyUser(userId);
                var room = context.Repository<Room>().VerifyRoom(roomId);

                if (room.PrivateRoom)
                {
                    if (!String.IsNullOrEmpty(inviteCode) &&
                        String.Equals(inviteCode, room.InviteCode, StringComparison.OrdinalIgnoreCase))
                    {
                        room.AllowUser(user);
                    }

                    if (!room.IsUserAllowed(user))
                    {
                        throw new Exception(String.Format(LanguageResources.Join_LockedAccessPermission, room.Name));
                    }
                }
                
                room.Join(user);

                context.Complete();
            }
        }

        public void LeaveRoom(Guid userId, Guid roomId)
        {
            using (var context = _unitOfWork())
            {
                var user = context.Repository<User>().VerifyUser(userId);
                var room = context.Repository<Room>().VerifyRoom(roomId);

                room.Leave(user);

                context.Complete();
            }
        }

        public void SetInviteCode(Guid userId, Guid roomId, string inviteCode)
        {
            using (var context = _unitOfWork())
            {
                var user = context.Repository<User>().VerifyUser(userId);
                var room = context.Repository<Room>().VerifyRoom(roomId);

                room.EnsureOwnerOrAdmin(user);

                if (!room.PrivateRoom)
                {
                    throw new Exception(LanguageResources.InviteCode_PrivateRoomRequired);
                }

                room.SetInviteCode(inviteCode);
                context.Complete();
            }
        }

        public Message AddMessage(User user, Room room, string id, string content)
        {
            using (var context = _unitOfWork())
            {
                throw new NotImplementedException();
            }
        }

        public Message AddMessage(string userId, string roomName, string url)
        {
            using (var context = _unitOfWork())
            {
                throw new NotImplementedException();
            }
        }

        public void AddOwner(User user, User targetUser, Room targetRoom)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveOwner(User user, User targetUser, Room targetRoom)
        {
            throw new System.NotImplementedException();
        }

        public void KickUser(User user, User targetUser, Room targetRoom)
        {
            throw new System.NotImplementedException();
        }

        public void AllowUser(User user, User targetUser, Room targetRoom)
        {
            throw new System.NotImplementedException();
        }

        public void UnallowUser(User user, User targetUser, Room targetRoom)
        {
            throw new System.NotImplementedException();
        }

        public void LockRoom(User user, Room targetRoom)
        {
            throw new System.NotImplementedException();
        }

        public void CloseRoom(User user, Room targetRoom)
        {
            throw new System.NotImplementedException();
        }

        public void OpenRoom(User user, Room targetRoom)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeTopic(User user, Room room, string newTopic)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeWelcome(User user, Room room, string newWelcome)
        {
            throw new System.NotImplementedException();
        }

        public void AppendMessage(string id, string content)
        {
            throw new System.NotImplementedException();
        }

        public void AddAdmin(User admin, User targetUser)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAdmin(User admin, User targetUser)
        {
            throw new System.NotImplementedException();
        }

        public void BanUser(User callingUser, User targetUser)
        {
            throw new System.NotImplementedException();
        }

        public void AddNotification(User mentionedUser, Message message, Room room, bool markAsRead)
        {
            throw new System.NotImplementedException();
        }
    }
}
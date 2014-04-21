using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Users.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class PreviousMessagesRequestHandler : IHandleRequest<PreviousMessagesRequest, PreviousMessagesResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Message> _messageRepository;
        private readonly IMapToNew<Message, MessageDto> _mapper;

        public PreviousMessagesRequestHandler(
            IUnitOfWork unitOfWork,
            IRepository<User> userRepository,
            IRepository<Message> messageRepository, 
            IMapToNew<Message, MessageDto> mapper)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");
            
            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");
            
            if (messageRepository == null) 
                throw new ArgumentNullException("messageRepository");
            
            if (mapper == null) 
                throw new ArgumentNullException("mapper");

            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<PreviousMessagesResponse> Handle(PreviousMessagesRequest request)
        {
            if (request == null) 
                throw new ArgumentNullException("request");

            try
            {
                var callingUser = _userRepository.VerifyUser(request.CallingUserId);
                var results = _messageRepository.GetMessagesFromId(request.FromId, request.NumberOfMessages);

                var response = new PreviousMessagesResponse(results.Select(_mapper.Map).ToArray());

                _unitOfWork.Complete();

                return response;
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}
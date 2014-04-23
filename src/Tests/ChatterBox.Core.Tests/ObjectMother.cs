using System;
using ChatterBox.Core.Extensions;
using ChatterBox.Domain.Aggregates.UserAggregate;

namespace ChatterBox.Core.Tests
{
    public static class ObjectMother
    {
        public static User CreateUser(string userName, string emailAddress, string salt, string password, DateTimeOffset lastActivity)
        {
            return new User(userName, emailAddress, emailAddress.ToMD5(), salt, password.ToSha256(salt), lastActivity);
        }
    }
}
using System;

namespace ChatterBox.Core.Infrastructure
{
    public static class Constants
    {
        public static readonly string AuthResultCookie = "chatterbox.authResult";
        public static readonly Version ChatterBoxVersion = typeof(Constants).Assembly.GetName().Version;
        public static readonly string ChatterBoxAuthType = "ChatterBox";
    }

    public static class ChatterBoxClaimTypes
    {
        public const string Identifier = "urn:chatterbox:id";
        public const string Admin = "urn:chatterbox:admin";
        public const string PartialIdentity = "urn:chatterbox:partialid";
    }

    public static class AcsClaimTypes
    {
        public static readonly string IdentityProvider = "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/IdentityProvider";
    }
}

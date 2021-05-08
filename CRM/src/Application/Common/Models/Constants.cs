namespace CRM.Application.Common.Models
{
    public static class Constants
    {
        public static class UserRoles
        {
            public const string SuperAdmin = "SuperAdmin";
            public const string Administrator = "Administrator";
            public const string Consultant = "Consultant";
            public const string Client = "Client";
        }

        public static class UserCustomClaims
        {
            public const string UserId = "UserId";
            public const string SocieteId = "SocieteId";
            public const string ApplicationAuthorisations = "ApplicationAuthorisations";
            public const string UserFirstName = "UserFirstName";
            public const string UserLastName = "UserLastName";
            public const string UserPhone = "UserPhone";
        }

        public static class UserPolicies
        {
            public const string SuperAdminPolicy = "SuperAdminPolicy";
            public const string AdministratorPolicy = "AdministratorPolicy";
            public const string ConsultantPolicy = "ConsultantPolicy";
            public const string ClientPolicy = "ClientPolicy";
        }

    }
}
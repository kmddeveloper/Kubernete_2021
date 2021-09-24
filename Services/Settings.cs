namespace Kubernetes.Services
{
    static public class Settings
    {
        public static class Token
        {
            public const int GUEST_TOKEN_EXPIRE_IN_MINUTES = 16200;
            public const int TOKEN_EXPIRE_IN_MINUTES = 60;
            public const int REFRESH_TOKEN_EXPIRE_IN_MINUTES = 120;

        }
    }
}

using System;

namespace JerseyStore
{
    public static class Session
    {
        private static string _loggedInUsername;

        // Raised when username changes (sign in or sign out)
        public static event EventHandler UsernameChanged;

        public static string LoggedInUsername
        {
            get => _loggedInUsername;
            private set
            {
                if (_loggedInUsername == value)
                    return;

                _loggedInUsername = value;
                UsernameChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        public static bool IsSignedIn => !string.IsNullOrEmpty(_loggedInUsername);

        public static void SignIn(string username)
        {
            LoggedInUsername = (username ?? string.Empty).Trim();
        }

        public static void SignOut()
        {
            LoggedInUsername = null;
        }
    }
}

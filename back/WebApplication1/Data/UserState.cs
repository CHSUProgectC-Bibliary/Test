namespace BookReviewAPI.Data
{
    public class UserState
    {
        public int UserId { get; private set; }
        public string UserName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public bool IsAuthenticated { get; private set; }

        public event Action? OnChange;

        public void Login(int userId, string userName, string email)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            IsAuthenticated = true;
            NotifyStateChanged();
        }

        public void Logout()
        {
            UserId = 0;
            UserName = string.Empty;
            Email = string.Empty;
            IsAuthenticated = false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}

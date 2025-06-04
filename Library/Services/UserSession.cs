using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Library.Services
{
    public class UserSession : INotifyPropertyChanged
    {
        private const string StorageEmailKey = "user_email";
        private const string StorageNameKey = "user_name";

        private readonly ProtectedLocalStorage _localStorage;

        public int UserId { get; set; }

        private bool _isLoggedIn;
        private string _email = "";
        private string _userName = "";

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            private set
            {
                if (_isLoggedIn != value)
                {
                    _isLoggedIn = value;
                    OnPropertyChanged(nameof(IsLoggedIn));
                }
            }
        }

        public string Email
        {
            get => _email;
            private set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string UserName
        {
            get => _userName;
            private set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        public UserSession(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var emailResult = await _localStorage.GetAsync<string>(StorageEmailKey);
                var nameResult = await _localStorage.GetAsync<string>(StorageNameKey);
                if (emailResult.Success && !string.IsNullOrEmpty(emailResult.Value))
                {
                    Email = emailResult.Value!;
                    UserName = nameResult.Success ? nameResult.Value ?? "" : "";
                    IsLoggedIn = true;
                }
            }
            catch { }
        }

        public async Task LoginAsync(UserDto user)
        {
            try
            {
                Email = user.Email;
                UserName = user.User_name;
                UserId = user.User_Id;
                IsLoggedIn = true;

                await _localStorage.SetAsync(StorageEmailKey, Email);
                await _localStorage.SetAsync(StorageNameKey, UserName);

                Console.WriteLine($"User session updated: {UserName}");
                // Явно уведомляем об изменении всех свойств
                OnPropertyChanged(nameof(IsLoggedIn));
                OnPropertyChanged(nameof(UserName));
                OnPropertyChanged(nameof(Email));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoginAsync error: {ex}");
            }
        }

        public async Task LogoutAsync()
        {
            Email = "";
            UserName = "";
            UserId = 0;
            IsLoggedIn = false;
            await _localStorage.DeleteAsync(StorageEmailKey);
            await _localStorage.DeleteAsync(StorageNameKey);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}

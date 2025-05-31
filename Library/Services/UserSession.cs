using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Library.Services
{
    public class UserSession : INotifyPropertyChanged
    {
        private const string StorageKey = "user_email";
        private readonly ProtectedLocalStorage _localStorage;
        public int UserId { get; set; }


        public bool IsAuthenticated => UserId > 0;
        private bool _isLoggedIn;
        private string _email = "";

        public UserSession(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

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

        public async Task InitializeAsync()
        {
            try
            {
                var result = await _localStorage.GetAsync<string>(StorageKey);
                if (result.Success && !string.IsNullOrEmpty(result.Value))
                {
                    Email = result.Value!;
                    IsLoggedIn = true;
                }
            }
            catch
            {
                // ignore
            }
        }

        public async Task LoginAsync(string email)
        {
            Email = email;
            IsLoggedIn = true;
            await _localStorage.SetAsync(StorageKey, email);
        }

        public async Task LogoutAsync()
        {
            Email = "";
            IsLoggedIn = false;
            await _localStorage.DeleteAsync(StorageKey);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

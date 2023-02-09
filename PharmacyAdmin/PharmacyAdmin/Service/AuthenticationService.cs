using System.Threading.Tasks;
using PharmacyAdmin.Helpers;
using PharmacyAdmin.Model;
using Microsoft.AspNetCore.Components;

namespace PharmacyAdmin.Service
{
    public interface IAuthenticationService
    {
        UserModel User { get; }
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
    }

    public class AuthenticationService : IAuthenticationService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;

        public UserModel User { get; private set; }

        public AuthenticationService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<UserModel>("user");
        }

        public async Task Login(string username, string password)
        {
            User = await _httpService.Post<UserModel>("/users/authenticate", new { username, password });
            User.AuthData = $"{username}:{password}".EncodeBase64();
            await _localStorageService.SetItem("user", User);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login");
        }
    }
}

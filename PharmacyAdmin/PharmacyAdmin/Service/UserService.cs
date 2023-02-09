using PharmacyAdmin.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Global;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace PharmacyAdmin.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAll();
        Task<bool> CheckUserAccess(string ResourceName, string UserName);
    }

    public class UserService : IUserService
    {
        private readonly IConfiguration _config;

        private Settings _appSettings;

        private Settings GetAppSettings()
        {
            Settings AppSettings = new Settings();
            AppSettings.Mode = _config.GetSection("AppSettings:Mode").Value;

            return AppSettings;
        }

        public UserService(IConfiguration config)
        {
            _config = config;
            _appSettings = GetAppSettings();
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            string FedExSearchPageAllowedUsers = "aclouse@aprilrx.com,ajacob@aprilrx.com,mignacio@aprilrx.com,cyu@hooverrx.com,jhan@aprilrx.com,jazim@aprilrx.com,nhughes@aprilrx.com,jlee@aprilrx.com";
            var ValidUsers = FedExSearchPageAllowedUsers.Split(',').ToList();
            List<UserModel> ResultList = new List<UserModel>();
            return await Task.FromResult(ResultList);
        }


        private string GetPageAllowedUsers(string ResourceName)
        {    
            string FedExSearchPageAllowedUsers = string.Empty;       
            string PageAllowedUsersConfigKey = string.Format("AppSettings:{0}PageAllowedUsers", ResourceName);
            // FedExSearchPageAllowedUsers = "aclouse@aprilrx.com,ajacob@aprilrx.com,mignacio@aprilrx.com,cyu@hooverrx.com,jhan@aprilrx.com,jazim@aprilrx.com,nhughes@aprilrx.com,jlee@aprilrx.com";
            FedExSearchPageAllowedUsers = "aclouse,Ann.Clouse,ajacob,Ann.Jacob,mignacio,Mallory.Ignacio,cyu,Christine.Yu,jhan,Jonathan.Han,jazim,Jason.Azim,nhughes,Nicole.Hughes,jlee,Jason.Lee";

            return _config.GetSection(PageAllowedUsersConfigKey).Value;
        }

        public Task<bool> CheckUserAccess(string ResourceName, string UserName)
        {
            bool HasAccess = false;
            // Server has login ids in this format : ALPHASCRIPT\jazim
            UserName = UserName.Replace(@"ALPHASCRIPT\", "").Replace("@aprilrx.com", "").Replace("@hooverrx.com","");

            if (ResourceName == "FedExSearch")
            {
                string FedExSearchPageAllowedUsers = string.Empty;

                FedExSearchPageAllowedUsers = GetPageAllowedUsers(ResourceName);
                this._appSettings.PageAllowedUsers = FedExSearchPageAllowedUsers;

                var ValidUsers = FedExSearchPageAllowedUsers.Split(',').ToList();
                foreach (var user in ValidUsers)
                {
                    if (user.ToLower() == UserName.ToLower())
                    {
                        HasAccess = true;
                        break;
                    }
                }
            }

            return Task.FromResult(HasAccess);
        }
    }
}
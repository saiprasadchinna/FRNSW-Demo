using MAUI_Demo_Service.Models;
using MAUI_Demo_Service.Services;

namespace MAUI_Demo_Service.Data
{
    public class UserService
    {
        RestService service = new RestService();

        public async Task<List<UserRolePages>> GetUserRolePages(string email, string AccessToken)
        {
            return await service.GetUserRolePages(email, AccessToken);
        }
        public async Task<List<User>> GetUserList(string AccessToken)
        {
            return await service.GetUserList(AccessToken);
        }
        public async Task<List<Role>> GetRoleList(string AccessToken)
        {
            return await service.GetRoleList(AccessToken);
        }
        public async Task<List<MAUI_Demo_Service.Models.Page>> GetPageList(string AccessToken)
        {
            return await service.GetPageList(AccessToken);
        }
        public async Task<bool> AddUsers(string Name, string Email, long PhoneNumber, long RoleId, long UserId, string AccessToken)
        {
            return await service.AddUsers(Name, Email, PhoneNumber, RoleId, UserId, AccessToken);
        }
        public async Task<bool> AddPages(string PageName, long RoleId, string RoleList, long PageId, string AccessToken)
        {
            return await service.AddPages(PageName, RoleId, RoleList, PageId, AccessToken);
        }
        public async Task<bool> OktaRevokeAccessToken(string AccessToken)
        {
            return await service.OktaRevokeAccessToken(AccessToken);
        }
    }
}

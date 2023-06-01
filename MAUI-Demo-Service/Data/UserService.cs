using MAUI_Demo_Service.Models;
using MAUI_Demo_Service.Services;

namespace MAUI_Demo_Service.Data
{
    public class UserService
    {
        RestService service = new RestService();
        public async Task<List<UserRolePages>> GetUserRolePages(string email)
        {
            return await service.GetUserRolePages(email);
        }
        public async Task<List<User>> GetUserList()
        {
            return await service.GetUserList();
        }
        public async Task<List<Role>> GetRoleList()
        {
            return await service.GetRoleList();
        }
        public async Task<List<MAUI_Demo_Service.Models.Page>> GetPageList()
        {
            return await service.GetPageList();
        }
        public async Task<bool> AddUsers(string Name, string Email, long PhoneNumber, long RoleId, long UserId)
        {
            return await service.AddUsers(Name, Email, PhoneNumber, RoleId,UserId);
        }
        public async Task<bool> AddPages(string PageName,  long RoleId, long PageId)
        {
            return await service.AddPages(PageName, RoleId,PageId);
        }
        public async Task<bool> OktaRevokeAccessToken(string AccessToken)
        {
            return await service.OktaRevokeAccessToken(AccessToken);
        }
    }
}

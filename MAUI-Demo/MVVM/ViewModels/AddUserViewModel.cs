using MAUI_Demo.Auth0;
using MAUI_Demo_Service.Data;

namespace MAUI_Demo.MVVM.ViewModels
{
    public class AddUserViewModel
    {
        public List<MAUI_Demo_Service.Models.Role> roleList { get; private set; }

        UserService userService = new UserService();


        public AddUserViewModel()
        {
            roleList = getRoleList(TokenHolder.AccessToken).Result;
        }

        public Task<List<MAUI_Demo_Service.Models.Role>> getRoleList(string AccessToken)
        {
            return Task.Run(() => userService.GetRoleList(AccessToken));
        }

        public Task<bool> AddUsers(string Name, string Email, long PhoneNumber, long RoleId,long UserId,string AccessToken)
        {
            return Task.Run(() => userService.AddUsers(Name,Email,PhoneNumber,RoleId, UserId, AccessToken));
        }
    }
}

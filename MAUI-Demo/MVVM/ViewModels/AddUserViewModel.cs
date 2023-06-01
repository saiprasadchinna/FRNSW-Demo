using MAUI_Demo_Service.Data;

namespace MAUI_Demo.MVVM.ViewModels
{
    public class AddUserViewModel
    {
        public List<MAUI_Demo_Service.Models.Role> roleList { get; private set; }

        UserService userService = new UserService();


        public AddUserViewModel()
        {
            roleList = getRoleList().Result;
        }

        public Task<List<MAUI_Demo_Service.Models.Role>> getRoleList()
        {
            return Task.Run(() => userService.GetRoleList());
        }

        public Task<bool> AddUsers(string Name, string Email, long PhoneNumber, long RoleId,long UserId)
        {
            return Task.Run(() => userService.AddUsers(Name,Email,PhoneNumber,RoleId, UserId));
        }
    }
}

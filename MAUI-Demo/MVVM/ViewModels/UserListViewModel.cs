using MAUI_Demo.Auth0;
using MAUI_Demo_Service.Data;

namespace MAUI_Demo.MVVM.ViewModels
{
    public class UserListViewModel
    {
        public IEnumerable<MAUI_Demo_Service.Models.User> userList { get; private set; } 

        UserService userService = new UserService();

        public int DefaultLoad = 2;
        int CurrentPage = 1;
        int PreviousPage = 1;

        public UserListViewModel()
        {
            userList = getUserList().Result.Skip((CurrentPage - 1) * DefaultLoad).Take(DefaultLoad);
            //userList=getUserList();
        }

        public Task<List<MAUI_Demo_Service.Models.User>> getUserList()
        {
            return Task.Run(() => userService.GetUserList(TokenHolder.AccessToken));
        }
 
    }
}

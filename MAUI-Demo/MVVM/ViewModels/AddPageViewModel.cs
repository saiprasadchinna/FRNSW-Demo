using MAUI_Demo_Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Demo.MVVM.ViewModels
{
    public class AddPageViewModel
    {
        public List<MAUI_Demo_Service.Models.Role> roleList { get; private set; }

        UserService userService = new UserService();


        public AddPageViewModel()
        {
            roleList = getRoleList().Result;
        }

        public Task<List<MAUI_Demo_Service.Models.Role>> getRoleList()
        {
            return Task.Run(() => userService.GetRoleList());
        }

        public Task<bool> AddPages(string PageName, long RoleId, long PageId)
        {
            return Task.Run(() => userService.AddPages(PageName, RoleId, PageId));
        }
    }
}

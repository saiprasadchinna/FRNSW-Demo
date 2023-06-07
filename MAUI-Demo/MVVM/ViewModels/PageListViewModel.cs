using MAUI_Demo.Auth0;
using MAUI_Demo_Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Demo.MVVM.ViewModels
{
    public class PageListViewModel
    {
        public IEnumerable<MAUI_Demo_Service.Models.Page> pageList { get; private set; }

        UserService userService = new UserService();

        public int DefaultLoad = 2;
        int CurrentPage = 1;
        int PreviousPage = 1;

        public PageListViewModel()
        {
            pageList = getPageList().Result.Skip((CurrentPage - 1) * DefaultLoad).Take(DefaultLoad);
        }

        public Task<List<MAUI_Demo_Service.Models.Page>> getPageList()
        {
            return Task.Run(() => userService.GetPageList(TokenHolder.AccessToken));
        }
    }
}

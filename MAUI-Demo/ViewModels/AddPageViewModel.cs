﻿using MAUI_Demo.Auth0;
using MAUI_Demo_Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Demo.ViewModels
{
    public class AddPageViewModel
    {
        public List<MAUI_Demo_Service.Models.Role> roleList { get; private set; }

        UserService userService = new UserService();


        public AddPageViewModel()
        {
            roleList = getRoleList(TokenHolder.AccessToken).Result;
        }

        public Task<List<MAUI_Demo_Service.Models.Role>> getRoleList(string AccessToken)
        {
            return Task.Run(() => userService.GetRoleList(AccessToken));
        }

        public Task<bool> AddPages(string PageName, long RoleId, string RoleList, long PageId, string AccessToken)
        {
            return Task.Run(() => userService.AddPages(PageName, RoleId, RoleList, PageId, AccessToken));
        }
    }
}

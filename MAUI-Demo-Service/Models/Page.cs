﻿namespace MAUI_Demo_Service.Models
{
    public class Page
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<Role> RoleList { get; set; }
    }

}
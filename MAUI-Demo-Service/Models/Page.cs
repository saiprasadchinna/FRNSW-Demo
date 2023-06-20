namespace MAUI_Demo_Service.Models
{
    public class Page
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public int RoleId { get; set; }
        public string RoleIDs { get; set; }
        public string BgColor { get; set; }
        public string RoleName { get; set; }
        public string RoleNames { get; set; }
        public List<Role> RoleList { get; set; }
        public Role SelectedRole { get; set; }
    }

}

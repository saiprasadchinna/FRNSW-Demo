namespace MAUI_Demo_Service.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int PageId { get; set; }
        public string PageName { get; set; }
        public List<Role> RoleList { get; set; }
    }
}

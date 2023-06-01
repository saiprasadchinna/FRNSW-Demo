namespace WebAPI_BackOffice.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int PageId { get; set; }
        public string PageName { get; set; }
    }
}

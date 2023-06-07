using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAPI_BackOffice.DB;
using WebAPI_BackOffice.Models;

namespace WebAPI_BackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        UserContext userDB = new UserContext();
        [HttpGet("getUserRolePages")]
        [CustomAuthorize]
        public IEnumerable<UserRolePages> get_UserRolePages(string email)
        {
            DataTable dtResults = userDB.getUserRolePages(email);
            List<UserRolePages> userRolePageList = dtResults.ConvertDataTableToList<UserRolePages>();
            return userRolePageList;
        }
        [HttpGet("getRoleList")]
        [CustomAuthorize]
        public IEnumerable<Role> get_RoleList()
        {
            DataTable dtResults = userDB.getRoleList();
            List<Role> roleList = dtResults.ConvertDataTableToList<Role>();
            return roleList;
        }

        [HttpGet("getUserList")]
        [CustomAuthorize]
        public IEnumerable<User> get_UserList()
        {
            DataTable dtResults = userDB.getUserList();
            List<User> userList = dtResults.ConvertDataTableToList<User>();
            return userList;
        }

        [HttpGet("getPageList")]
        [CustomAuthorize]
        public IEnumerable<Page> get_PageList()
        {
            DataTable dtResults = userDB.getPageList();
            List<Page> pageList = dtResults.ConvertDataTableToList<Page>();
            return pageList;
        }


        [HttpPost("AddUsers")]
        [CustomAuthorize]
        public bool AddUsers(string Name, string Email, long PhoneNumber, long RoleId, long UserId)
        {
            return userDB.saveUsers(Name, Email, PhoneNumber, RoleId, UserId);
        }
        [HttpPost("AddPages")]
        [CustomAuthorize]
        public bool AddPages(string PageName, long RoleId, long PageId)
        {
            return userDB.savePages(PageName, RoleId, PageId);
        }
    }
}

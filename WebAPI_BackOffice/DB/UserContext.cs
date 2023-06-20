using System.Data;
using System.Data.SqlClient;

namespace WebAPI_BackOffice.DB
{
    public class UserContext
    {
        public DataTable getUserRolePages(string email)
        {
            SqlConnection sqlConnection = new SqlConnection("Server=.;Database=FRNSWdemo;uid=sa;pwd=pnws@123;");
            SqlCommand cmd = new SqlCommand("get_UserRolePages", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", email);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
        public DataTable getRoleList()
        {
            SqlConnection sqlConnection = new SqlConnection("Server=.;Database=FRNSWdemo;uid=sa;pwd=pnws@123;");
            SqlCommand cmd = new SqlCommand("get_Roles", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }

        public DataTable getUserList()
        {
            SqlConnection sqlConnection = new SqlConnection("Server=.;Database=FRNSWdemo;uid=sa;pwd=pnws@123;");
            SqlCommand cmd = new SqlCommand("get_AllUsers", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }


        public DataTable getPageList()
        {
            SqlConnection sqlConnection = new SqlConnection("Server=.;Database=FRNSWdemo;uid=sa;pwd=pnws@123;");
            SqlCommand cmd = new SqlCommand("get_Pages", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
        public bool saveUsers(string Name, string Email, long PhoneNumber, long RoleId, long UserId)
        {
            using (SqlConnection sqlConnection = new SqlConnection("Server=.;Database=FRNSWdemo;uid=sa;pwd=pnws@123;"))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("save_Users", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                cmd.Parameters.AddWithValue("@RoleId", RoleId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                //cmd.Parameters.Add("@UserExistsOrNot", SqlDbType.Bit);
                //cmd.Parameters["@UserExistsOrNot"].Direction = ParameterDirection.Output;
                int result = cmd.ExecuteNonQuery();
                //var userexistsOrnot = Convert.ToString(cmd.Parameters["@UserExistsOrNot"].Value);
                if (result > 0)
                    return true;
                else
                    return false;

            }
        }
        public bool savePages(string PageName, long RoleId,string RoleList, long PageId)
        {
            using (SqlConnection sqlConnection = new SqlConnection("Server=.;Database=FRNSWdemo;uid=sa;pwd=pnws@123;"))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("save_Pages", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageName", PageName);
                cmd.Parameters.AddWithValue("@RoleId", RoleId);
                cmd.Parameters.AddWithValue("@RoleList", RoleList);
                cmd.Parameters.AddWithValue("@PageId", PageId);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}

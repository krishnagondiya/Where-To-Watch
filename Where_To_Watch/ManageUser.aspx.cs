using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Where_To_Watch
{
    public partial class ManageUser : System.Web.UI.Page
    {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    LoadUserData();
                }
            }

            private void LoadUserData()
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT UserID, FullName, Username FROM Users";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridViewUsers.DataSource = dt;
                    GridViewUsers.DataBind();
                }
            }

            protected void GridViewUsers_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                if (e.CommandName == "DeleteUser")
                {
                    int userId = Convert.ToInt32(e.CommandArgument);
                    DeleteUser(userId);
                    LoadUserData(); // Refresh the data
                }
            }

            private void DeleteUser(int userId)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Users WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }
    }
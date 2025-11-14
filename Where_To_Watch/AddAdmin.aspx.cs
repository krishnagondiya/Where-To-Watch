using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Where_To_Watch
{
    public partial class AddAdmin : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAdmins();
            }
        }

        protected void btnAddAdmin_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO adminlogin (adminname, adminusername, password) VALUES (@fullName, @username, @password)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fullName", txtFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                cmd.ExecuteNonQuery();

                lblMessage.Text = "Admin Added Successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                ClearFields();
                LoadAdmins();
            }
        }

        protected void gvAdmins_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvAdmins.SelectedRow;
            hdnAdminID.Value = row.Cells[0].Text;
            txtFullName.Text = row.Cells[1].Text;
            txtUsername.Text = row.Cells[2].Text;
            txtPassword.Text = row.Cells[3].Text;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "UPDATE adminlogin SET adminname = @fullName, adminusername = @username, password = @password WHERE aid = @adminID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fullName", txtFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@adminID", hdnAdminID.Value);
                cmd.ExecuteNonQuery();

                lblMessage.Text = "Admin Updated Successfully!";
                LoadAdmins();
                ClearFields();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM adminlogin WHERE aid = @adminID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@adminID", hdnAdminID.Value);
                cmd.ExecuteNonQuery();

                lblMessage.Text = "Admin Deleted Successfully!";
                LoadAdmins();
                ClearFields();
            }
        }

        private void LoadAdmins()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM adminlogin", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvAdmins.DataSource = dt;
                gvAdmins.DataBind();
            }
        }

        private void ClearFields()
        {
            txtFullName.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            hdnAdminID.Value = "";
        }
    }
}

using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Where_To_Watch
{
    public partial class Register : System.Web.UI.Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                lblMessage.Text = "All fields are required.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Check if username already exists
                string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                SqlCommand checkUserCmd = new SqlCommand(checkUserQuery, conn);
                checkUserCmd.Parameters.AddWithValue("@username", username);
                int userExists = (int)checkUserCmd.ExecuteScalar();

                if (userExists > 0)
                {
                    lblMessage.Text = "Username already exists. Please choose a different one.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return; // Stop execution if username exists, keep passwords
                }

                // Insert new user if username is unique
                string query = "INSERT INTO Users (FullName, Username, Password) VALUES (@fullName, @username, @password)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fullName", fullName);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password); // ⚠️ Storing plain text password is not secure!

                cmd.ExecuteNonQuery();

                lblMessage.Text = "Registered successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;

                clear();
                conn.Close();
            }
        }

        protected void clear()
        {
            txtFullName.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
        }
    }
}

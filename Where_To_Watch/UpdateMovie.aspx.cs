using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Where_To_Watch
{
	public partial class UpdateMovie : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPlatforms();
                if (Request.QueryString["id"] != null)
                {
                    int movieID = Convert.ToInt32(Request.QueryString["id"]);
                    LoadMovieDetails(movieID);
                }
            }
        }

        private void LoadPlatforms()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT PlatformID, PlatformName FROM Platforms";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                rptPlatforms.DataSource = reader;
                rptPlatforms.DataBind();
                reader.Close();
            }
        }

        private void LoadMovieDetails(int movieID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Movies WHERE MovieID = @MovieID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MovieID", movieID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtMovieName.Text = reader["MovieName"].ToString();
                    txtDescription.Text = reader["Description"].ToString();
                    txtIMDbRating.Text = reader["IMDbRating"].ToString();
                    txtMovieDuration.Text = reader["MovieDuration"].ToString();
                    txtReleaseDate.Text = Convert.ToDateTime(reader["ReleaseDate"]).ToString("yyyy-MM-dd");
                    txtPosterLink.Text = reader["PosterLink"].ToString();
                }
                reader.Close();

                string platformQuery = "SELECT PlatformID, IsFree FROM MoviePlatforms WHERE MovieID = @MovieID";
                cmd = new SqlCommand(platformQuery, conn);
                cmd.Parameters.AddWithValue("@MovieID", movieID);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int platformID = Convert.ToInt32(reader["PlatformID"]);
                    bool isFree = Convert.ToBoolean(reader["IsFree"]);

                    foreach (RepeaterItem item in rptPlatforms.Items)
                    {
                        HiddenField hfPlatformID = (HiddenField)item.FindControl("hfPlatformID");
                        CheckBox chkPlatform = (CheckBox)item.FindControl("chkPlatform");
                        CheckBox chkIsFree = (CheckBox)item.FindControl("chkIsFree");

                        if (hfPlatformID != null && chkPlatform != null && chkIsFree != null)
                        {
                            if (Convert.ToInt32(hfPlatformID.Value) == platformID)
                            {
                                chkPlatform.Checked = true;
                                chkIsFree.Checked = isFree;
                            }
                        }
                    }
                }
                reader.Close();
            }
        }

        protected void btnUpdateMovie_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["id"] != null)
            {
                int movieID = Convert.ToInt32(Request.QueryString["id"]);
                string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Movies WHERE MovieName = @MovieName AND MovieID != @MovieID";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@MovieName", txtMovieName.Text.Trim());
                    checkCmd.Parameters.AddWithValue("@MovieID", movieID);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        Response.Write("<script>alert('This movie name already exists, please select a different one.');</script>");
                        return;
                    }

                    string updateQuery = "UPDATE Movies SET MovieName=@MovieName, Description=@Description, IMDbRating=@IMDbRating, " +
                                         "MovieDuration=@MovieDuration, ReleaseDate=@ReleaseDate, PosterLink=@PosterLink WHERE MovieID=@MovieID";
                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@MovieName", txtMovieName.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@IMDbRating", Convert.ToDecimal(txtIMDbRating.Text));
                    cmd.Parameters.AddWithValue("@MovieDuration", Convert.ToInt32(txtMovieDuration.Text));
                    cmd.Parameters.AddWithValue("@ReleaseDate", Convert.ToDateTime(txtReleaseDate.Text));
                    cmd.Parameters.AddWithValue("@PosterLink", txtPosterLink.Text);
                    cmd.Parameters.AddWithValue("@MovieID", movieID);
                    cmd.ExecuteNonQuery();

                    string deletePlatforms = "DELETE FROM MoviePlatforms WHERE MovieID=@MovieID";
                    cmd = new SqlCommand(deletePlatforms, conn);
                    cmd.Parameters.AddWithValue("@MovieID", movieID);
                    cmd.ExecuteNonQuery();

                    foreach (RepeaterItem item in rptPlatforms.Items)
                    {
                        CheckBox chkPlatform = (CheckBox)item.FindControl("chkPlatform");
                        CheckBox chkIsFree = (CheckBox)item.FindControl("chkIsFree");
                        HiddenField hfPlatformID = (HiddenField)item.FindControl("hfPlatformID");

                        if (chkPlatform.Checked)
                        {
                            string insertQuery = "INSERT INTO MoviePlatforms (MovieID, PlatformID, IsFree) VALUES (@MovieID, @PlatformID, @IsFree)";
                            cmd = new SqlCommand(insertQuery, conn);
                            cmd.Parameters.AddWithValue("@MovieID", movieID);
                            cmd.Parameters.AddWithValue("@PlatformID", Convert.ToInt32(hfPlatformID.Value));
                            cmd.Parameters.AddWithValue("@IsFree", chkIsFree.Checked);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                ClearFields();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Movie updated successfully!'); window.location='AdminDashboard.aspx';", true);

            }
        }

        private void ClearFields()
        {
            txtMovieName.Text = "";
            txtDescription.Text = "";
            txtIMDbRating.Text = "";
            txtMovieDuration.Text = "";
            txtReleaseDate.Text = "";
            txtPosterLink.Text = "";

            foreach (RepeaterItem item in rptPlatforms.Items)
            {
                CheckBox chkPlatform = (CheckBox)item.FindControl("chkPlatform");
                CheckBox chkIsFree = (CheckBox)item.FindControl("chkIsFree");

                if (chkPlatform != null) chkPlatform.Checked = false;
                if (chkIsFree != null) chkIsFree.Checked = false;
            }
        }
    }
}
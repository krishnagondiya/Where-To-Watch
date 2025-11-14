using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Where_To_Watch
{
    public partial class AddMovie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPlatforms();
                if (Request.QueryString["id"] != null)
                {
                    int movieID = Convert.ToInt32(Request.QueryString["id"]);
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


        protected void btnAddMovie_Click(object sender, EventArgs e)
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string checkMovieQuery = "SELECT COUNT(*) FROM Movies WHERE MovieName = @MovieName";
                    SqlCommand checkCmd = new SqlCommand(checkMovieQuery, conn);
                    checkCmd.Parameters.AddWithValue("@MovieName", txtMovieName.Text.Trim());

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        Response.Write("<script>alert('This movie is already exist. Please select another one.');</script>");
                        return;
                    }

                    string insertMovieQuery = "INSERT INTO Movies (MovieName, Description, IMDbRating, MovieDuration, ReleaseDate, PosterLink) " +
                                              "VALUES (@MovieName, @Description, @IMDbRating, @MovieDuration, @ReleaseDate, @PosterLink); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(insertMovieQuery, conn);
                    cmd.Parameters.AddWithValue("@MovieName", txtMovieName.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@IMDbRating", Convert.ToDecimal(txtIMDbRating.Text));
                    cmd.Parameters.AddWithValue("@MovieDuration", Convert.ToInt32(txtMovieDuration.Text));
                    cmd.Parameters.AddWithValue("@ReleaseDate", Convert.ToDateTime(txtReleaseDate.Text));
                    cmd.Parameters.AddWithValue("@PosterLink", txtPosterLink.Text);
                    int movieID = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (RepeaterItem item in rptPlatforms.Items)
                    {
                        CheckBox chkPlatform = (CheckBox)item.FindControl("chkPlatform");
                        CheckBox chkIsFree = (CheckBox)item.FindControl("chkIsFree");
                        HiddenField hfPlatformID = (HiddenField)item.FindControl("hfPlatformID");

                        if (chkPlatform.Checked)
                        {
                            string insertMoviePlatformQuery = "INSERT INTO MoviePlatforms (MovieID, PlatformID, IsFree) VALUES (@MovieID, @PlatformID, @IsFree)";
                            SqlCommand cmdMoviePlatform = new SqlCommand(insertMoviePlatformQuery, conn);
                            cmdMoviePlatform.Parameters.AddWithValue("@MovieID", movieID);
                            cmdMoviePlatform.Parameters.AddWithValue("@PlatformID", Convert.ToInt32(hfPlatformID.Value));
                            cmdMoviePlatform.Parameters.AddWithValue("@IsFree", chkIsFree.Checked);
                            cmdMoviePlatform.ExecuteNonQuery();
                        }
                    }
                }

                ClearFields();
                Response.Write("<script>alert('Movie added successfully!');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
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

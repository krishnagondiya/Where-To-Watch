using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;

namespace Where_To_Watch
{
    public partial class MovieDetails : Page
    {
        string connString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["MovieID"] != null)
                {
                    int movieID = Convert.ToInt32(Request.QueryString["MovieID"]);
                    LoadMovieDetails(movieID);
                    LoadPlatforms(movieID);
                }
                else
                {
                    Response.Redirect("UserDashboard.aspx");
                }
            }
        }

        private void LoadMovieDetails(int movieID)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = @"SELECT MovieName, Description, IMDbRating, 
                                 MovieDuration, ReleaseDate, PosterLink FROM Movies WHERE MovieID = @MovieID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MovieID", movieID);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        MovieTitle.Text = reader["MovieName"].ToString();
                        MovieDescription.Text = reader["Description"].ToString();
                        IMDbRating.Text = reader["IMDbRating"].ToString();
                        MovieDuration.Text = reader["MovieDuration"].ToString();
                        ReleaseDate.Text = Convert.ToDateTime(reader["ReleaseDate"]).ToString("yyyy-MM-dd");
                        MoviePoster.ImageUrl = reader["PosterLink"].ToString();
                    }
                    con.Close();
                }
            }
        }

        private void LoadPlatforms(int movieID)
        {
            StringBuilder html = new StringBuilder();

            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = @"SELECT p.PlatformName, p.PlatformLogo, p.WebsiteURL, mp.IsFree 
                                 FROM MoviePlatforms mp
                                 JOIN Platforms p ON mp.PlatformID = p.PlatformID
                                 WHERE mp.MovieID = @MovieID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MovieID", movieID);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string platformName = reader["PlatformName"].ToString();
                        string platformURL = reader["WebsiteURL"].ToString();
                        string platformLogo = reader["PlatformLogo"].ToString();
                        bool isFree = Convert.ToBoolean(reader["IsFree"]);

                        html.Append("<div class='platform'>");
                        html.Append($"<img src='{platformLogo}' alt='{platformName}'>");
                        html.Append($"<span><strong>{platformName}</strong> ({(isFree ? "Free" : "Premium")})</span><a class='watch-btn' href='{platformURL}'>Watch Now</a>");
                        html.Append("</div>");
                    }
                    con.Close();
                }
            }
            PlatformsDiv.Text = html.ToString();
        }
    }
}

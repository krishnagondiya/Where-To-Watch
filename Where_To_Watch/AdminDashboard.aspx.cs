using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;

namespace Where_To_Watch
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["DeleteID"] != null)
                {
                    int deleteID = Convert.ToInt32(Request.QueryString["DeleteID"]);
                    DeleteMovie(deleteID);
                }
                LoadMovies();
            }
        }

        private void LoadMovies()
        {
            StringBuilder html = new StringBuilder();

            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = @"SELECT M.MovieID, M.MovieName, M.Description, M.IMDbRating, 
                         M.MovieDuration, M.ReleaseDate, M.PosterLink
                         FROM Movies M";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        MoviesDiv.Text = "<p style='color:red; text-align:center;'>No movies found.</p>";
                        return;
                    }

                    while (reader.Read())
                    {
                        int movieID = Convert.ToInt32(reader["MovieID"]);

                        html.Append("<div class='movie-card'>");
                        html.Append($"<a href='MovieDetails1.aspx?MovieID={movieID}' class='movie-link'><strong>-: Movie Details :-</strong></a><br>");
                        html.Append($"<img src='{reader["PosterLink"]}' alt='Poster' class='poster-img' />");
                        html.Append("<div class='movie-info'>");
                        html.Append($"<h3>{reader["MovieName"]}</h3>");
                        html.Append($"<p><strong>IMDb:</strong> {reader["IMDbRating"]}/10</p>");
                        html.Append($"<p><strong>Duration:</strong> {reader["MovieDuration"]} min</p>");
                        html.Append($"<p><strong>Release:</strong> {Convert.ToDateTime(reader["ReleaseDate"]).ToString("yyyy-MM-dd")}</p>");
                        html.Append($"<p class='desc'><strong>Description:</strong> {reader["Description"]}</p>");
                        html.Append("<p><strong>-: Platforms :-</strong></p>");
                        html.Append("<div class='platform-container'>");

                        using (SqlConnection con2 = new SqlConnection(connString))
                        {
                            string platformQuery = @"SELECT P.PlatformName, P.PlatformLogo, P.WebsiteURL, MP.IsFree
                                             FROM MoviePlatforms MP
                                             INNER JOIN Platforms P ON MP.PlatformID = P.PlatformID
                                             WHERE MP.MovieID = @MovieID";

                            using (SqlCommand cmd2 = new SqlCommand(platformQuery, con2))
                            {
                                cmd2.Parameters.AddWithValue("@MovieID", movieID);
                                con2.Open();
                                SqlDataReader reader2 = cmd2.ExecuteReader();

                                if (!reader2.HasRows)
                                {
                                    html.Append("<p>No platforms available</p>");
                                }
                                else
                                {
                                    while (reader2.Read())
                                    {
                                        string platformLogo = reader2["PlatformLogo"] != DBNull.Value ? reader2["PlatformLogo"].ToString() : "default-logo.png";
                                        string platformName = reader2["PlatformName"].ToString();
                                        string websiteURL = reader2["WebsiteURL"] != DBNull.Value ? reader2["WebsiteURL"].ToString() : "#";
                                        string isFree = Convert.ToBoolean(reader2["IsFree"]) ? "Free" : "Premium";

                                        html.Append($"<a href='{websiteURL}' target='_blank' class='platform-item'>");
                                        html.Append($"<img src='{platformLogo}' class='platform-logo' alt='{platformName} Logo' />");
                                        html.Append($"<span class='platform-info'>{platformName} - <strong>{isFree}</strong></span>");
                                        html.Append("</a>");
                                    }
                                }
                                con2.Close();
                            }
                        }
                        html.Append("</div>"); 
                        html.Append($"<a href='UpdateMovie.aspx?id={reader["MovieID"]}' class='btn update-btn'>Update</a> ");
                        html.Append($"<a href='AdminDashboard.aspx?DeleteID={reader["MovieID"]}' class='btn delete-btn' onclick='return confirm(\"Are you sure you want to delete this movie?\");'>Delete</a>");
                        html.Append("</div>"); 
                        html.Append("</div>"); 
                    }
                    con.Close();
                }
            }
            MoviesDiv.Text = html.ToString();
        }



        private void DeleteMovie(int movieID)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                using (SqlCommand cmd1 = new SqlCommand("DELETE FROM MoviePlatforms WHERE MovieID = @MovieID", con))
                {
                    cmd1.Parameters.AddWithValue("@MovieID", movieID);
                    cmd1.ExecuteNonQuery();
                }

                using (SqlCommand cmd2 = new SqlCommand("DELETE FROM Movies WHERE MovieID = @MovieID", con))
                {
                    cmd2.Parameters.AddWithValue("@MovieID", movieID);
                    cmd2.ExecuteNonQuery();
                }
            }
        }
    }
}

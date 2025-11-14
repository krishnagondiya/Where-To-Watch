<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="Where_To_Watch.AdminDashboard" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Admin Dashboard</title>
    <style>

        body {
            font-family: Arial, sans-serif;
            background-color: #121212;
            color: #e0e0e0;
            margin: 0;
            padding: 0;
        }

        .navbar {
            background-color: #1f1f1f;
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 12px 20px;
        }

        .navbar a {
            color: white;
            text-decoration: none;
            padding: 10px 15px;
            margin-right: 10px;
            transition: 0.3s;
        }

        .navbar a:hover {
            background-color: #333;
            border-radius: 5px;
        }

        .container {
            margin: 20px;
            padding: 20px;
            background-color: #1e1e1e;
            box-shadow: 0px 0px 10px rgba(255, 255, 255, 0.1);
            border-radius: 10px;
            text-align: center;
        }

        h2 {
            color: #ffffff;
        }

        .movies-container {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(240px, 1fr));
            gap: 20px;
            margin-top: 20px;
            padding: 10px;
        }

        .movie-card {
            background-color: #252525;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(255, 255, 255, 0.1);
            padding: 15px;
            text-align: center;
            transition: transform 0.2s;
        }

        .movie-card:hover {
            transform: scale(1.05);
        }

        .poster-img {
            width: 100%;
            height: 200px;
            object-fit: cover;
            border-radius: 5px;
        }

        .movie-info h3 {
            margin: 10px 0 5px;
            color: #e0e0e0;
            font-size: 18px;
        }

        .movie-info p {
            font-size: 14px;
            color: #bbb;
            margin: 5px 0;
        }

        .desc {
            font-size: 12px;
            color: #aaa;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        .btn {
            display: inline-block;
            padding: 8px 12px;
            margin-top: 10px;
            font-size: 14px;
            text-decoration: none;
            border-radius: 5px;
            transition: 0.3s;
        }

        .update-btn {
            background-color: #28a745;
            color: white;
        }

        .update-btn:hover {
            background-color: #218838;
        }

        .delete-btn {
            background-color: #dc3545;
            color: white;
        }

        .delete-btn:hover {
            background-color: #c82333;
        }

        .platform-container {
            text-align: left;
            margin-top: 10px;
            padding: 10px;
            background: #1f1f1f;
            border-radius: 5px;
        }

        .platform-item {
            display: flex;
            align-items: center;
            gap: 8px;
            margin-bottom: 8px;
            padding: 6px;
            background: white;
            border-radius: 5px;
            text-decoration: none;
        }

        .platform-logo {
            height: 22px;
            width: 22px;
            object-fit: contain;
            border-radius: 3px;
        }

        .platform-info {
            color: #121212;
            font-size: 14px;
        }

        .search-container {
            text-align: right;
            margin-bottom: 20px;
            margin-left: auto;
        }

        #searchBox {
            width: 50%;
            padding: 10px;
            font-size: 16px;
            border: 2px solid #28a745;
            border-radius: 5px;
            background-color: #1e1e1e;
            color: #e0e0e0;
        }

        #searchBox:focus {
            outline: none;
            border-color: #218838;
        }

        .movie-link {
            text-decoration: none;
            color: goldenrod;
            display: block;
        }

        .movie-link:hover .movie-card {
            transform: scale(1.05);
            box-shadow: 0px 0px 15px rgba(255, 255, 255, 0.2);
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar">
            <a href="AdminDashboard.aspx">Home</a>
            <a href="AddMovie.aspx">Add Movie</a>
            <a href="AddAdmin.aspx">Add Admin</a>
            <a href="ManageUser.aspx">Manage User</a>
            <a href="Login.aspx">Logout</a>
            <div class="search-container">
                <input type="text" id="searchBox" placeholder="Search movie.." onkeyup="searchMovie()">
            </div>
        </div>

        <div class="container">
            <h2>Movie List</h2>
            <div class="movies-container" id="moviesContainer">
                <asp:Literal ID="MoviesDiv" runat="server"></asp:Literal>
            </div>
        </div>
    </form>

    <script>
        function searchMovie() {
            var input, filter, movies, movieCards, movieName, i;
            input = document.getElementById("searchBox");
            filter = input.value.toLowerCase();
            movies = document.getElementById("moviesContainer");
            movieCards = movies.getElementsByClassName("movie-card");

            for (i = 0; i < movieCards.length; i++) {
                movieName = movieCards[i].getElementsByTagName("h3")[0];
                if (movieName) {
                    if (filter.length > 0 && movieName.innerHTML.toLowerCase().indexOf(filter) > -1) {
                        movieCards[i].style.order = "-1";
                        movieCards[i].style.border = "3px double #05f551";
                    } else {
                        movieCards[i].style.order = "";
                        movieCards[i].style.border = "";
                    }
                }
            }
        }

    </script>
</body>
</html>

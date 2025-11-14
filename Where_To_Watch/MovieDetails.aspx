<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MovieDetails.aspx.cs" Inherits="Where_To_Watch.MovieDetails" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Movie Details</title>
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
            transition: 0.3s;
        }

        .navbar a:hover {
            background-color: #333;
            border-radius: 5px;
        }

        .container {
            max-width: 900px;
            margin: 40px auto;
            padding: 20px;
            background-color: #1e1e1e;
            box-shadow: 0px 0px 10px rgba(255, 255, 255, 0.1);
            border-radius: 10px;
            display: flex;
            align-items: flex-start;
            gap: 20px;
        }

        .poster {
            width: 250px;
            height: 380px;
            border-radius: 10px;
            object-fit: cover;
        }

        .details {
            flex-grow: 1;
        }

        h2 {
            margin-bottom: 10px;
            font-size: 28px;
            color: #fff;
        }

        .info {
            font-size: 16px;
            margin-bottom: 8px;
        }

        .description {
            font-size: 14px;
            line-height: 1.5;
            color: #ccc;
        }

        .platforms {
            margin-top: 15px;
        }

        .platform {
            background: #282828;
            padding: 10px;
            border-radius: 5px;
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 8px;
            margin-bottom: 8px;
            color: #fff;
        }

        .platform img {
            width: 75px;
            height: 75px;
            object-fit: contain;
            border-radius: 3px;
        }

        .back-btn {
            display: inline-block;
            padding: 8px 15px;
            background: #28a745;
            color: white;
            text-decoration: none;
            border-radius: 5px;
            margin-top: 15px;
            transition: 0.3s;
        }

        .back-btn:hover {
            background: #218838;
        }
        
        .watch-btn {
            display: inline-block;
            padding: 8px 15px;
            background: #ffac00;
            color: white;
            text-decoration: none;
            border-radius: 5px;
            margin-top: 10px;
            transition: 0.3s;
            margin-left: auto;
            font-weight:1000;       
        }

        .watch-btn:hover {
            background: #ffe546;
            color: green;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar">
            <a href="UserDashboard.aspx">Back to Dashboard</a>
        </div>

        <div class="container">
            <asp:Image ID="MoviePoster" runat="server" CssClass="poster" />
            <div class="details">
                <h2><asp:Label ID="MovieTitle" runat="server"></asp:Label></h2>
                <p class="info"><strong>IMDb:</strong> <asp:Label ID="IMDbRating" runat="server"></asp:Label>/10</p>
                <p class="info"><strong>Duration:</strong> <asp:Label ID="MovieDuration" runat="server"></asp:Label> min</p>
                <p class="info"><strong>Release Date:</strong> <asp:Label ID="ReleaseDate" runat="server"></asp:Label></p>
                <p class="description"><asp:Label ID="MovieDescription" runat="server"></asp:Label></p>

                <h3>Available on:</h3>
                <div class="platforms">
                    <asp:Literal ID="PlatformsDiv" runat="server"></asp:Literal>
                </div>

                <a href="UserDashboard.aspx" class="back-btn">Go Back</a>
            </div>
        </div>
    </form>
</body>
</html>

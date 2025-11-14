<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMovie.aspx.cs" Inherits="Where_To_Watch.AddMovie" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Add / Update Movie</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }
        .container {
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            width: 90%;
            max-width: 800px;
        }
        h2 {
            text-align: center;
        }
        .form-row {
            display: flex;
            align-items: center;
            margin-bottom: 10px;
        }
        label {
            width: 150px;
            font-weight: bold;
        }
        input[type="text"], input[type="number"], input[type="date"], textarea {
            flex: 1;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .platform-container {
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 5px;
            background: #f9f9f9;
            margin-top: 5px;
            border-radius: 4px;
        }
        .btn-container {
            display: flex;
            justify-content: space-between;
            margin-top: 15px;
        }
        .btn {
            padding: 10px 15px;
            background-color: #28a745;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            flex: 1;
            margin: 0 5px;
            text-align: center;
        }
        
        .btn:hover {
            opacity: 0.9;
        }
        .error {
            color: red;
            font-size: 12px;
            margin-left: 5px;
        }
        .btn-home {
            position: absolute;
            top: 20px;
            left: 20px;
            background-color: #28a745;
            color: white;
            padding: 12px 16px;
            border-radius: 6px;
            text-decoration: none;
            font-size: 14px;
            font-weight: 500;
            transition: background 0.3s;
        }

        .btn-home:hover {
            background-color: #218838;
        }
    </style>
</head>
<body>
    <a href="AdminDashboard.aspx" class="btn-home">Home</a>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Add Movie</h2>

            <div class="form-row">
                <label>Movie Name:</label>
                <asp:TextBox ID="txtMovieName" runat="server" required></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvMovieName" runat="server" ControlToValidate="txtMovieName" ErrorMessage="Required." CssClass="error" Display="Dynamic" />
            </div>

            <div class="form-row">
                <label>Description:</label>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Required." CssClass="error" Display="Dynamic" />
            </div>

            <div class="form-row">
                <label>IMDb Rating:</label>
                <asp:TextBox ID="txtIMDbRating" runat="server" type="number" min="0" max="10" step="0.1" required></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvIMDbRating" runat="server" ControlToValidate="txtIMDbRating" ErrorMessage="Required." CssClass="error" Display="Dynamic" />
            </div>

            <div class="form-row">
                <label>Duration (mins):</label>
                <asp:TextBox ID="txtMovieDuration" runat="server" type="number" required></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvMovieDuration" runat="server" ControlToValidate="txtMovieDuration" ErrorMessage="Required." CssClass="error" Display="Dynamic" />
            </div>

            <div class="form-row">
                <label>Release Date:</label>
                <asp:TextBox ID="txtReleaseDate" runat="server" type="date" required></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvReleaseDate" runat="server" ControlToValidate="txtReleaseDate" ErrorMessage="Required." CssClass="error" Display="Dynamic" />
            </div>

            <div class="form-row">
                <label>Poster Link:</label>
                <asp:TextBox ID="txtPosterLink" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPosterLink" runat="server" ControlToValidate="txtPosterLink" ErrorMessage="Required." CssClass="error" Display="Dynamic" />
            </div>

            <h3>Available Platforms</h3>
            <asp:Repeater ID="rptPlatforms" runat="server">
                <ItemTemplate>
                    <div class="platform-container">
                        <asp:HiddenField ID="hfPlatformID" runat="server" Value='<%# Eval("PlatformID") %>' />
                        <asp:CheckBox ID="chkPlatform" runat="server" Text='<%# Eval("PlatformName") %>' />
                        <label>Is Free:</label>
                        <asp:CheckBox ID="chkIsFree" runat="server" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <div class="btn-container">
                <asp:Button ID="btnAddMovie" runat="server" Text="Add Movie" CssClass="btn" OnClick="btnAddMovie_Click" />
            </div>
        </div>
    </form>
</body>
</html>

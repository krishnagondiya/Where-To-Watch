<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="Where_To_Watch.AdminLogin" %>

<!DOCTYPE html>
<html>
<head>
    <title>Login</title>
    <style>
        /* Global Styles */
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            text-align: center;
            margin: 0;
            padding: 0;
        }

        /* Centered Container */
        .container {
            width: 300px;
            background: #fff;
            margin: 100px auto;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
        }

        h2 {
            margin-bottom: 20px;
            color: #333;
        }

        /* Input Fields */
        input[type="text"], input[type="password"] {
            width: 90%;
            padding: 12px;
            margin: 10px 0;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 14px;
            transition: border-color 0.3s ease-in-out;
        }

        input:focus {
            border-color: #007BFF;
            outline: none;
        }

        /* Validation Message */
        .error {
            color: red;
            font-size: 12px;
            text-align: left;
            display: block;
            margin-top: -8px;
        }

        /* Login Button */
        .btn {
            width: 100%;
            background-color: #007BFF;
            color: white;
            padding: 12px;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
            margin-top: 15px;
            transition: background 0.3s ease-in-out;
        }

        .btn:hover {
            background-color: #0056b3;
        }

        /* Link to Register */
        .link {
            display: block;
            margin-top: 15px;
            text-decoration: none;
            color: #007BFF;
            font-size: 14px;
        }

        .link:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="container">
            <h2>Admin Login</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

            <asp:TextBox ID="txtAdminname" runat="server" placeholder="Adminname"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtAdminname" runat="server" CssClass="error" ErrorMessage="Username is required." />

            <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtPassword" runat="server" CssClass="error" ErrorMessage="Password is required." />

            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn" OnClick="btnLogin_Click" />
            <br><br><hr>
            <a href="Login.aspx" class="link">Login as a user</a>

        </div>
    </form>
</body>
</html>

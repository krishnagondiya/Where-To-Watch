<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Where_To_Watch.Register" %>

<!DOCTYPE html>
<html runat="server">
<head>
    <title>Register</title>
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
            margin: 80px auto;
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
            padding: 15px;
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
        

        /* Register Button */
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

        /* Link to Login */
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
            <h2>User Registration</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="green"></asp:Label>

            <asp:TextBox ID="txtFullName" runat="server" placeholder="Full Name" AutoPostBack="false"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtFullName" runat="server" CssClass="error" ErrorMessage="Full Name is required." />
            <asp:RegularExpressionValidator 
                    ControlToValidate="txtFullName" 
                    runat="server" 
                    CssClass="error"
                    ErrorMessage="Only alphabets are allowed." 
                     ValidationExpression="^[a-zA-Z\s]+$" />

            <asp:TextBox ID="txtUsername" runat="server" placeholder="Username" AutoPostBack="false"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtUsername" runat="server" CssClass="error" ErrorMessage="Username is required." />
            <asp:RegularExpressionValidator 
                    ControlToValidate="txtUsername" 
                    runat="server" 
                    CssClass="error"
                    ErrorMessage="Username must be at least 6 characters long and must be mix of charecters and numbers." 
                    ValidationExpression="^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]{6,}$" />


            <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password" AutoPostBack="false"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtPassword" runat="server" CssClass="error" ErrorMessage="Password is required." />
            <asp:RegularExpressionValidator 
                ControlToValidate="txtPassword" 
                runat="server" 
                CssClass="error"
                ErrorMessage="Password must be at least 8 characters long and must be mix of charecters , numbers & special latter(@,#,_)." 
                ValidationExpression="^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@#_])[a-zA-Z\d@#_]{8,}$" />


            <asp:TextBox ID="txtConfirmPassword" runat="server" placeholder="Confirm Password" TextMode="Password" AutoPostBack="false"></asp:TextBox>
            <asp:CompareValidator ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" runat="server" CssClass="error" ErrorMessage="Passwords do not match." />

            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn" OnClick="btnRegister_Click" />

            <a href="Login.aspx" class="link">Already have an account? Login here</a>
            

        </div>
    </form>
</body>
</html>

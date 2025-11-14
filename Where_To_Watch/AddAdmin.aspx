<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAdmin.aspx.cs" Inherits="Where_To_Watch.AddAdmin" %>

<!DOCTYPE html>
<html runat="server">
<head>
    <title>Manage Admins</title>
    <style>
        body {
            font-family: 'Poppins', sans-serif;
            background-color: #eef2f3;
            text-align: center;
            margin: 0;
            padding: 0;
        }

        .container {
            width: 450px;
            background: #ffffff;
            margin: 50px auto;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0px 5px 15px rgba(0, 0, 0, 0.2);
        }

        h2 {
            margin-bottom: 20px;
            color: #333;
            font-weight: 600;
        }

        input[type="text"], input[type="password"] {
            width: 100%;
            padding: 12px;
            margin: 10px 0;
            border: 1px solid #ccc;
            border-radius: 6px;
            font-size: 14px;
            transition: all 0.3s ease-in-out;
            display: block;
        }

        input:focus {
            border-color: #007BFF;
            outline: none;
            box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
        }

        .btn {
            width: 100%;
            background-color: #007BFF;
            color: white;
            padding: 12px;
            border: none;
            border-radius: 6px;
            font-size: 16px;
            cursor: pointer;
            margin-top: 10px;
            transition: background 0.3s ease-in-out;
            font-weight: 500;
        }

        .btn:hover {
            background-color: #0056b3;
        }

        .btn-delete {
            background-color: #dc3545;
        }

        .btn-delete:hover {
            background-color: #c82333;
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

        table {
            width: 100%;
            margin-top: 20px;
            border-collapse: collapse;
            background: white;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0px 5px 10px rgba(0, 0, 0, 0.1);
        }

        th, td {
            padding: 12px;
            border: 1px solid #ddd;
            text-align: left;
        }

        th {
            background-color: #007BFF;
            color: white;
            font-weight: 600;
        }

        tr:hover {
            background-color: #f1f1f1;
            cursor: pointer;
        }

        .error {
            color: red;
            font-size: 12px;
            text-align: left;
            display: block;
            margin-top: -8px;
        }
    </style>
</head>
<body>
    <a href="AdminDashboard.aspx" class="btn-home">Home</a>
    <form runat="server">
        <div class="container">
            <h2>Manage Admins</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="green"></asp:Label>

            <asp:HiddenField ID="hdnAdminID" runat="server" />

            <asp:TextBox ID="txtFullName" runat="server" placeholder="Full Name"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtFullName" runat="server" CssClass="error" ErrorMessage="Full Name is required." />

            <asp:TextBox ID="txtUsername" runat="server" placeholder="Username"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtUsername" runat="server" CssClass="error" ErrorMessage="Username is required." />

            <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtPassword" runat="server" CssClass="error" ErrorMessage="Password is required." />

            <asp:Button ID="btnAddAdmin" runat="server" Text="Add" CssClass="btn" OnClick="btnAddAdmin_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-delete" OnClick="btnDelete_Click" />

            <h3>Admin List</h3>
            <asp:GridView ID="gvAdmins" runat="server" AutoGenerateColumns="False" CssClass="table"
                OnSelectedIndexChanged="gvAdmins_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="aid" HeaderText="ID" />
                    <asp:BoundField DataField="adminname" HeaderText="Full Name" />
                    <asp:BoundField DataField="adminusername" HeaderText="Username" />
                    <asp:BoundField DataField="password" HeaderText="password" />
                    <asp:CommandField ShowSelectButton="True" SelectText="Select" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>

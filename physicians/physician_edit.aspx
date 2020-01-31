<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="physician_edit.aspx.cs" Inherits="FinalProject.physicians.physician_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
        <h2>Edit Physician Information</h2>
        
    <form id="form1" runat="server">
        
    First Name:
    <asp:TextBox ID="Fname" runat="server"></asp:TextBox>
    <br />
    <br />
    Last Name:
     <asp:TextBox ID="Lname" runat="server"></asp:TextBox>
    <br />
    <br />
    Middle Initial:
     <asp:TextBox ID="MI" runat="server"></asp:TextBox>
    <br />
    <br />
    Gender:
     <asp:TextBox ID="Gender" runat="server"></asp:TextBox>
    <br />
    <br />
    DOB:
     <asp:TextBox ID="DOB" runat="server"></asp:TextBox>
    <br />
    <br />

    <asp:Button ID="btnSave" runat="server" Text="Save" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />

        <div>
        </div>
    </form>
</body>
</html>
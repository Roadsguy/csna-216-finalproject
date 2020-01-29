<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="drug_add.aspx.cs" Inherits="FinalProject.drugs.drug_add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Add Drug Information</h2>
        </div>
        <br />
        Drug ID: <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        Drug Name: <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        Drug Description:
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
        Method of Administration:
        <asp:TextBox ID="txtMethodOfAdmin" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnAdd" runat="server" Text="Add Drug" />
    &nbsp;
        <asp:Button ID="btnClose" runat="server" Text="Close" />
    </form>
</body>
</html>

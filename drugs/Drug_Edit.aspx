<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="drug_edit.aspx.cs" Inherits="FinalProject.drugs.Drug_Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Edit Drug Information</h2>
        </div>
            Drug ID: <asp:TextBox ID="txtDrugID" runat="server"></asp:TextBox>
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
        <asp:Button ID="btnEdit" runat="server" Text="Edit" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnClose" runat="server" Text="Close" />
    </form>
</body>
</html>

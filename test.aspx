<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="FinalProject.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color: red; height: 200px;">
			<p style="text-align:center">Hello!</p>
			<p style="text-align:center">Place your name here: Salim,</p>
        </div>
    	<p style="text-align: center">
			Drug ID:
			<asp:TextBox ID="txtDrugID" runat="server"></asp:TextBox>
&nbsp;Drug Name:
			<asp:TextBox ID="txtDrugName" runat="server"></asp:TextBox>
&nbsp;Drug Description:
			<asp:TextBox ID="txtDrugDesc" runat="server"></asp:TextBox>
&nbsp;Method of Admin:
			<asp:TextBox ID="txtMethodOfAdmin" runat="server"></asp:TextBox>
			<asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Add" />
		</p>
    	Drug ID:
		<asp:TextBox ID="txtDrugIDSearch" runat="server"></asp:TextBox>
		<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
		<br />
		<asp:GridView ID="GridView1" runat="server">
		</asp:GridView>
    </form>
</body>
</html>

<%@ Page Title="" Language="C#" MasterPageFile="~/Louis.master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="FinalProject.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Log In - Louis's Pharmacy</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContent" runat="server">
	<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
	<h2 id="pageheader">
		<asp:Label ID="lblPageHeader" runat="server" Text="Log In"></asp:Label></h2>
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
			<div class="inputwrapper">
				<div class="inputbox">
					<div class="inputleft_narrow">
						Username:
					</div>
					<div class="inputright">
						<asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
					</div>
					<div class="inputleft_narrow">
						Password:
					</div>
					<div class="inputright">
						<asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
					</div>
					<div id="lblPwd">
						<asp:Label ID="lblMessage" ForeColor="red" runat="server"></asp:Label>

					</div>
					<div class="inputboxbuttons">
						<asp:CheckBox ID="chkRemember" runat="server" CssClass="checkbox" Text="Remember Me" />
					</div>
					<div class="inputboxbuttons">
						<asp:LinkButton ID="btnLogIn" runat="server" CssClass="button" Text="Log In" OnClick="btnLogIn_Click" />
					</div>
				</div>
			</div>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>

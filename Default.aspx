<%@ Page Language="C#" MasterPageFile="~/Louis.master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FinalProject._default" %>

<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
	<title>Default Page</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="pageContent" runat="server">
	<div id="content">
		<div class="inputwrapper">
			<div class="inputbox">
				<p style="text-align: center">
			Welcome to the Louis's Pharmacy management application.
		</p>
		<p style="text-align: center">
			From this site, you can manage our prescriptions, as well as the data on the patients, physicians, and drugs themselves. To access a section, click the links on the left.
		</p>
			</div>
		</div>
	</div>
</asp:Content>
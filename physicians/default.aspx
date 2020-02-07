<%@ Page Title="" Language="C#" MasterPageFile="~/Louis.master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FinalProject.physicians._default" %>
<%@ Register src="physicians_search.ascx" TagName="uc" TagPrefix="search" %>
<%@ Register src="physicians_vieweditadd.ascx" TagName="uc" TagPrefix="vieweditadd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Physicians - Louis's Pharmacy</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContent" runat="server">
	<div id="content">
		<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
		<asp:UpdatePanel ID="pnlContent" runat="server">
			<ContentTemplate>
				Invalid controls loaded.
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>
</asp:Content>

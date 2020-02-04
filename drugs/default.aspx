<%@ Page Title="" Language="C#" MasterPageFile="~/Louis.master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FinalProject.drugs._default" %>
<%@ Register src="drugs_search.ascx" TagName="uc" TagPrefix="search" %>
<%@ Register src="drugs_vieweditadd.ascx" TagName="uc" TagPrefix="vieweditadd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
<%@ Page Title="" Language="C#" MasterPageFile="~/Louis.master" AutoEventWireup="true" CodeBehind="patients_view.aspx.cs" Inherits="FinalProject.patients.patients_view" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContent" runat="server">
	<div id="content">
		<button id="ajaxbtn" type="button" runat="server" onclick="goToSearch()">Go Back</button>
		<br />
		<asp:TextBox ID="txtAjax" runat="server"></asp:TextBox>
	</div>
</asp:Content>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="deleteconfirm.ascx.cs" Inherits="FinalProject.deleteconfirm" %>
<div id="deleteconfirm-modal">
	<asp:Label ID="lblWarning" runat="server" Text="Confirm deletion of record(s)?<br />This cannot be undone!"></asp:Label>
	<br /><br />
	<asp:Button ID="btnDeleteConfirm" runat="server" CssClass="button" Text="Confirm Delete" OnClick="btnDeleteConfirm_Click" />
	&nbsp;&nbsp;&nbsp;
	<asp:Button ID="btnDeleteCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnDeleteCancel_Click" />
</div>
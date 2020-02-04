<%@ Page Title="" Language="C#" MasterPageFile="~/Louis.master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FinalProject.patients._default" %>
<%@ Register src="patients_search.ascx" TagName="uc" TagPrefix="search" %>
<%@ Register src="patients_vieweditadd.ascx" TagName="uc" TagPrefix="vieweditadd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContent" runat="server">
	<script>
		function SelectAll(id) {
			alert('test');
			// Get reference of GridView control
			var grid = $("#grdPatients");

			// Variable to contain the cell of the grid
			var cell;

			if (grid.rows.length > 0) {
				// Loop starts from 1; rows[0] points to the header
				for (i = 1; i < grid.rows.length; i++) {
					// Get the reference of the first column
					cell = grid.rows[i].cells[0];

					// Loop according to the number of childNodes in the cell
					for (j = 0; j < cell.childNodes.length; j++) {
						// If childNode type is CheckBox
						if (cell.childNodes[j].type == 'checkbox') {
							// Assign the status of the Select All checkbox to the c ell checkbox within the grid
							cell.childNodes[j].checked = $(id).checked;
						}
					}
				}
			}
		}
	</script>
	<div id="content">
		<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
		<asp:UpdatePanel ID="pnlContent" runat="server">
			<ContentTemplate>
				Invalid controls loaded.
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>
</asp:Content>

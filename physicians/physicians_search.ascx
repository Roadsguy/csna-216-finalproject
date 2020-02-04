﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="physicians_search.ascx.cs" Inherits="FinalProject.physicians.physicians_search" %>
<script type="text/javascript">
	function SelectAll(id) {
		var grid = $("#<%= grdPhysicians.ClientID %>")
		var checkStatus = id.checked;
		$(grid).find("input[type='checkbox']").prop('checked', checkStatus);
	}

	function TestScript() {
		// Get reference of GridView control
		var grid = $("#grdPhysicians");

		// Variable to contain the cell of the grid
		var cell;

		//if (grid.rows.length > 0) {
		if (<% =grdPhysicians.Rows.Count %> > 0) {
			// Loop starts from 1; rows[0] points to the header
			for (i = 1; i < grid.rows.length; i++) {
				// Get the reference of the first column
				cell = grid.rows[i].cells[0];

				// Loop according to the number of childNodes in the cell
				for (j = 0; j < cell.childNodes.length; j++) {
					// If childNode type is CheckBox
					if (cell.childNodes[j].type == 'checkbox') {
						// Assign the status of the Select All checkbox to the cell checkbox within the grid
						cell.childNodes[j].checked = $("#chkSelectAll").checked;
					}
				}
			}
		}
	}
</script>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<h2 id="pageheader"><asp:Label ID="lblPageHeader" runat="server" Text="Search Physicians"></asp:Label></h2>
<div class="inputwrapper">
	<div class="inputbox">
		<div class="inputleft_narrow">
			Physician ID:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtSrchPhysicianID" runat="server" data-lpignore="true"></asp:TextBox>
		</div>

		<div class="inputleft_narrow">
			Last Name:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtSrchLastName" runat="server" data-lpignore="true"></asp:TextBox>
		</div>

		<div class="inputleft_narrow">
			First Name:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtSrchFirstName" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_narrow">
			Employer:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtSrchEmployer" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputboxbuttons">
			<asp:LinkButton ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
			<asp:LinkButton ID="btnAdd" runat="server" CssClass="button" Text="Add Physician" /><br />
		</div>
	</div>
</div>
<asp:UpdatePanel ID="pnlDeleteConfirm" ClientIDMode="static" runat="server">
	<ContentTemplate>

	</ContentTemplate>
</asp:UpdatePanel>
<asp:LinkButton ID="btnDeleteChecked" runat="server" CssClass="button" Text="Delete Selected" OnClick="btnDeleteChecked_Click" Enabled="false" Visible="false" />
<asp:GridView ID="grdPhysicians" ClientIDMode="static" AutoGenerateColumns="False" CssClass="gridview" runat="server" Width="100%"
	AllowPaging="True" AllowSorting="True" ShowHeaderWhenEmpty="True" PagerStyle-CssClass="gridview-pager"
	HeaderStyle-CssClass="gridview-header" RowStyle-CssClass="gridview-rows">

	<HeaderStyle CssClass="gridview-header"></HeaderStyle>

	<PagerSettings Mode="NextPreviousFirstLast" FirstPageText="<< First" PreviousPageText="< Prev" NextPageText="Next >" LastPageText="Last >>" />
	<AlternatingRowStyle CssClass="gridview-altrows" />
	<Columns>
		<asp:TemplateField HeaderText="Physician ID" ItemStyle-CssClass="gridviewchk" HeaderStyle-CssClass="gridviewchk">
			<HeaderTemplate>
				<asp:CheckBox ID="chkSelectAll" ClientIDMode="static" runat="server" OnClick="SelectAll(this, id)" />
			</HeaderTemplate>
			<ItemTemplate>
				<asp:CheckBox ID="chkPhysicianID" runat="server" AutoPostBack="false" />
			</ItemTemplate>
		</asp:TemplateField>
		<asp:BoundField DataField="physicianID" HeaderText="Physician ID" SortExpression="physicianID" />
		<asp:BoundField DataField="lName" HeaderText="Last Name" SortExpression="lName" />
		<asp:BoundField DataField="fName" HeaderText="First Name" SortExpression="fName" />
		<asp:BoundField DataField="mInit" HeaderText="Middle Initial" SortExpression="mInit" />
		<asp:BoundField DataField="gender" HeaderText="Gender" SortExpression="gender" />
		<asp:BoundField DataField="dateOfBirth" HeaderText="Date of Birth" SortExpression="dateOfBirth" DataFormatString="{0:yyyy-MM-dd}" />
		<asp:BoundField DataField="employer" HeaderText="Employer" SortExpression="employer" />
		<asp:BoundField DataField="specialty1" HeaderText="Primary Specialty" SortExpression="specialty1" />
		<asp:TemplateField HeaderText="">
			<ItemTemplate>
				<asp:ImageButton ID="imgBtnView" ClientIDMode="static" runat="server" ImageUrl="/images/view.svg" Height="24" ToolTip="View Record"
					OnCommand="View_Click" CommandName="lbtnView" CommandArgument='<% # cipher.Encrypt(Eval("physicianID").ToString()) %>' />
				<asp:ImageButton ID="imgBtnEdit" ClientIDMode="static" runat="server" ImageUrl="/images/edit.svg" Height="24" ToolTip="Edit Record"
					OnCommand="Edit_Click" CommandName="lbtnEdit" CommandArgument='<% # cipher.Encrypt(Eval("physicianID").ToString()) %>' />
				&nbsp;&nbsp;&nbsp;
				<asp:ImageButton ID="imgBtnDelete" ClientIDMode="static" runat="server" ImageUrl="/images/delete.svg" Height="24" ToolTip="Delete Record"
					OnCommand="Delete_Click" CommandName="lbtnDelete" CommandArgument='<% # cipher.Encrypt(Eval("physicianID").ToString()) %>' />
			</ItemTemplate>
			<HeaderStyle HorizontalAlign="Left" />
		</asp:TemplateField>
	</Columns>
	<PagerStyle CssClass="gridview-pager"></PagerStyle>
	<RowStyle CssClass="gridview-rows"></RowStyle>
	<EmptyDataTemplate>
		No Records Found Matching Your Search!
	</EmptyDataTemplate>
</asp:GridView>
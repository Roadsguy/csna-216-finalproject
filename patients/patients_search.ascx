<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="patients_search.ascx.cs" Inherits="FinalProject.patients.patients_search" %>
<script type="text/javascript">
	function SelectAllTest(id) {
		var grid = $("#<%= grdPatients.ClientID %>")
		var checkStatus = id.checked;
		$(grid).find("input[type='checkbox']").prop('checked', checkStatus);
	}

	function TestScript() {
		// Get reference of GridView control
		var grid = $("#grdPatients");

		// Variable to contain the cell of the grid
		var cell;

		//if (grid.rows.length > 0) {
		if (<% =grdPatients.Rows.Count %> > 0) {
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
<h2 class="pageheader"><asp:Label ID="lblPageHeader" runat="server" Text="Search Patients"></asp:Label></h2>
Patient ID: <asp:TextBox ID="txtSrchPatientID" runat="server" data-lpignore="true"></asp:TextBox><br />
Last Name: <asp:TextBox ID="txtSrchLastName" runat="server" data-lpignore="true"></asp:TextBox><br />
First Name: <asp:TextBox ID="txtSrchFirstName" runat="server" data-lpignore="true"></asp:TextBox>
<br /><br />
<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /><br />
<asp:Button ID="btnAdd" runat="server" Text="Add Patient" /><br />
<asp:Button ID="btnDeleteChecked" runat="server" Text="Delete Selected" OnClick="btnDeleteChecked_Click" />
<asp:UpdatePanel ID="pnlDeleteConfirm" ClientIDMode="static" runat="server">
	<ContentTemplate>

	</ContentTemplate>
</asp:UpdatePanel>
<asp:GridView ID="grdPatients" ClientIDMode="static" AutoGenerateColumns="False" CssClass="gridview" runat="server" Width="100%"
	AllowPaging="True" AllowSorting="True" OnSorting="grdPatients_Sorting" ShowHeaderWhenEmpty="True" PagerStyle-CssClass="gridview-pager"
	HeaderStyle-CssClass="gridview-header" RowStyle-CssClass="gridview-rows">

	<HeaderStyle CssClass="gridview-header"></HeaderStyle>

	<PagerSettings Mode="NextPreviousFirstLast" FirstPageText="Go To First Page" LastPageText="Go To Last Page" Position="Top" />
	<AlternatingRowStyle CssClass="gridview-altrows" />
	<Columns>
		<asp:TemplateField HeaderText="Student ID" ItemStyle-CssClass="gridviewchk" HeaderStyle-CssClass="gridviewchk">
			<HeaderTemplate>
				<asp:CheckBox ID="chkSelectAll" ClientIDMode="static" runat="server" OnClick="SelectAllTest(this, id)" />
			</HeaderTemplate>
			<ItemTemplate>
				<asp:CheckBox ID="chkPatientID" runat="server" AutoPostBack="false" />
			</ItemTemplate>
		</asp:TemplateField>
		<asp:BoundField DataField="patientID" HeaderText="Patient ID" SortExpression="patientID" />
		<asp:BoundField DataField="lName" HeaderText="Last Name" SortExpression="lName" />
		<asp:BoundField DataField="fName" HeaderText="First Name" SortExpression="fName" />
		<asp:BoundField DataField="mInit" HeaderText="Middle Initial" SortExpression="mInit" />
		<asp:BoundField DataField="gender" HeaderText="Gender" SortExpression="gender" />
		<asp:BoundField DataField="dateOfBirth" HeaderText="Date of Birth" SortExpression="dateOfBirth" DataFormatString="{0:yyyy-MM-dd}" />
		<asp:BoundField DataField="acctBalance" HeaderText="Account Balance" SortExpression="acctBalance" DataFormatString="{0:C}" />
		<asp:BoundField DataField="insuranceCo" HeaderText="Insurance" SortExpression="insuranceCo" />
		<asp:TemplateField HeaderText="">
			<ItemTemplate>
				<asp:ImageButton ID="imgBtnView" ClientIDMode="static" runat="server" ImageUrl="/images/view.svg" Height="24" ToolTip="View Record"
					OnCommand="View_Click" CommandName="lbtnView" CommandArgument='<% # cipher.Encrypt(Eval("patientID").ToString()) %>' />
				<asp:ImageButton ID="imgBtnEdit" ClientIDMode="static" runat="server" ImageUrl="/images/edit.svg" Height="24" ToolTip="Edit Record"
					OnCommand="Edit_Click" CommandName="lbtnEdit" CommandArgument='<% # cipher.Encrypt(Eval("patientID").ToString()) %>' />
				&nbsp;&nbsp;&nbsp;
				<asp:ImageButton ID="imgBtnDelete" ClientIDMode="static" runat="server" ImageUrl="/images/delete.svg" Height="24" ToolTip="Delete Record"
					OnCommand="Delete_Click" CommandName="lbtnDelete" CommandArgument='<% # cipher.Encrypt(Eval("patientID").ToString()) %>' />
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

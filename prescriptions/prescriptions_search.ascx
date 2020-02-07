<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="prescriptions_search.ascx.cs" Inherits="FinalProject.prescriptions.prescriptions_search" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC1" %>
<script type="text/javascript">
	function SelectAll(id) {
		var grid = $("#<%= grdPrescriptions.ClientID %>")
		var checkStatus = id.checked;
		$(grid).find("input[type='checkbox']").prop('checked', checkStatus);
	}

	function TestScript() {
		// Get reference of GridView control
		var grid = $("#grdPrescriptions");

		// Variable to contain the cell of the grid
		var cell;

		//if (grid.rows.length > 0) {
		if (<% =grdPrescriptions.Rows.Count %> > 0) {
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
<h2 id="pageheader"><asp:Label ID="lblPageHeader" runat="server" Text="Search Prescriptions"></asp:Label></h2>
<div class="inputwrapper">
	<div class="inputbox">
		<div class="inputleft_narrow">
			Rx No.:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtSrchRxNo" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div ID="divInputByID" runat="server" visible="false">
			<div class="inputleft_narrow">
				Patient ID:
			</div>
			<div class="inputright">
				<asp:TextBox ID="txtSrchPatientID" runat="server" data-lpignore="true"></asp:TextBox>
			</div>
			<div class="inputleft_narrow">
				Drug ID:
			</div>
			<div class="inputright">
				<asp:TextBox ID="txtSrchDrugID" runat="server" data-lpignore="true"></asp:TextBox>
			</div>

			<div class="inputleft_narrow">
				Physician ID:
			</div>
			<div class="inputright">
				<asp:TextBox ID="txtSrchPhysicianID" runat="server" data-lpignore="true"></asp:TextBox>
			</div>
		</div>
		<div ID="divInputByName" runat="server" visible="true">
			<div class="inputleft_narrow">
				Patient:
			</div>
			<div class="inputright">
				<asp:DropDownList ID="ddlSrchPatientName" runat="server" CssClass="search" AppendDataBoundItems="true" data-lpignore="true"></asp:DropDownList>
			</div>
			<div class="inputleft_narrow">
				Drug:
			</div>
			<div class="inputright">
				<asp:DropDownList ID="ddlSrchDrugName" runat="server" CssClass="search" AppendDataBoundItems="true" data-lpignore="true"></asp:DropDownList>
			</div>

			<div class="inputleft_narrow">
				Physician:
			</div>
			<div class="inputright">
				<asp:DropDownList ID="ddlSrchPhysicianName" runat="server" CssClass="search" AppendDataBoundItems="true" data-lpignore="true"></asp:DropDownList>
			</div>
		</div>
		<div class="inputboxbuttons">
			<asp:RadioButton ID="rdoInputByName" runat="server" GroupName="inputOpt" CssClass="radiobtn" Text="Search by Name" OnCheckedChanged="SwitchInputType" AutoPostBack="true" />
			<asp:RadioButton ID="rdoInputByID" runat="server" GroupName="inputOpt" CssClass="radiobtn" Text="Search by ID" OnCheckedChanged="SwitchInputType" AutoPostBack="true" />
		</div>
		<div class="inputboxbuttons">
			<asp:LinkButton ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
			<asp:LinkButton ID="btnAdd" runat="server" CssClass="button" Text="Add Prescription" /><br />
		</div>
	</div>
</div>
<asp:UpdatePanel ID="pnlDeleteConfirm" ClientIDMode="static" runat="server">
	<ContentTemplate>

	</ContentTemplate>
</asp:UpdatePanel>
<asp:LinkButton ID="btnDeleteChecked" runat="server" CssClass="button" Text="Delete Selected" OnClick="btnDeleteChecked_Click" Enabled="false" Visible="false" />
<asp:GridView ID="grdPrescriptions" ClientIDMode="static" AutoGenerateColumns="False" CssClass="gridview" runat="server" Width="100%"
	AllowPaging="True" AllowSorting="True" ShowHeaderWhenEmpty="True" PagerStyle-CssClass="gridview-pager"
	HeaderStyle-CssClass="gridview-header" RowStyle-CssClass="gridview-rows">

	<HeaderStyle CssClass="gridview-header"></HeaderStyle>

	<PagerSettings Mode="NextPreviousFirstLast" FirstPageText="<< First" PreviousPageText="< Prev" NextPageText="Next >" LastPageText="Last >>" />
	<AlternatingRowStyle CssClass="gridview-altrows" />
	<Columns>
		<asp:TemplateField HeaderText="Student ID" ItemStyle-CssClass="gridviewchk" HeaderStyle-CssClass="gridviewchk">
			<HeaderTemplate>
				<asp:CheckBox ID="chkSelectAll" ClientIDMode="static" runat="server" OnClick="SelectAll(this, id)" />
			</HeaderTemplate>
			<ItemTemplate>
				<asp:CheckBox ID="chkRxNo" runat="server" AutoPostBack="false" />
			</ItemTemplate>
		</asp:TemplateField>
		<asp:BoundField DataField="rxNo" HeaderText="Rx Number" SortExpression="rxNo" />
		<asp:BoundField DataField="patient" HeaderText="Patient" SortExpression="patient" />
	    <asp:BoundField DataField="patientID" visible="false" />
		<asp:BoundField DataField="drug" HeaderText="Drug" SortExpression="drug" />
		<asp:BoundField DataField="drugID" visible="false" />
		<asp:BoundField DataField="physician" HeaderText="Physician" SortExpression="physician" />
		<asp:BoundField DataField="physicianID" Visible="false" />
		<asp:BoundField DataField="dosage" HeaderText="Dosage" SortExpression="dosage" />
		<asp:BoundField DataField="startDate" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" SortExpression="startDate" />
		<asp:BoundField DataField="finishDate" HeaderText="Finish Date" DataFormatString="{0:yyyy-MM-dd}" SortExpression="finishDate" />
		<asp:BoundField DataField="refillsLeft" HeaderText="Refills Left" SortExpression="refillsLeft" />
		<asp:TemplateField HeaderText="">
			<ItemTemplate>
				<asp:ImageButton ID="imgBtnView" ClientIDMode="static" runat="server" ImageUrl="/images/view.svg" Height="24" ToolTip="View Record"
					OnCommand="View_Click" CommandName="lbtnView" CommandArgument='<% # cipher.Encrypt(Eval("rxNo").ToString()) %>' />
				<asp:ImageButton ID="imgBtnEdit" ClientIDMode="static" runat="server" ImageUrl="/images/edit.svg" Height="24" ToolTip="Edit Record"
					OnCommand="Edit_Click" CommandName="lbtnEdit" CommandArgument='<% # cipher.Encrypt(Eval("rxNo").ToString()) %>' />
				<asp:ImageButton ID="imgBtnRefill" ClientIDMode="static" runat="server" ImageUrl="/images/refill.svg" Height="24" ToolTip="Refill Prescription"
					OnCommand="Refill_Click" CommandName="lbtnRefill" CommandArgument='<% # cipher.Encrypt(Eval("rxNo").ToString()) + "," + Eval("refillsLeft") %>' />
				&nbsp;&nbsp;&nbsp;
				<asp:ImageButton ID="imgBtnDelete" ClientIDMode="static" runat="server" ImageUrl="/images/delete.svg" Height="24" ToolTip="Delete Record"
					OnCommand="Delete_Click" CommandName="lbtnDelete" CommandArgument='<% # cipher.Encrypt(Eval("rxNo").ToString()) %>' />
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

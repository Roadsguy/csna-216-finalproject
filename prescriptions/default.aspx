<%@ Page Title="" Language="C#" MasterPageFile="~/Louis.master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FinalProject.prescriptions._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function popupAdd()
        {
            var map = null;
            var features = "width=220,height=250"
            features += "left=50,top=50,resize=yes,menu=no,status"
            map = window.open("prescription_add.aspx", "mapwin", features);
            map.focus();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContent" runat="server">
    Rx Number: <asp:TextBox ID="txtRxNo" runat="server"></asp:TextBox><br />
	Patient ID: <asp:TextBox ID="txtPatientID" runat="server"></asp:TextBox><br />
	Drug ID: <asp:TextBox ID="txtDrugID" runat="server"></asp:TextBox><br />
	Physician ID: <asp:TextBox ID="txtPhysicianID" runat="server"></asp:TextBox>
	<br /><br />
	<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
	<asp:Button ID="btnAddPresc" runat="server" OnClick="btnAddPresc_Click" Text="Add Prescription" />
	<br /><br />
	<asp:GridView ID="grdPrescriptions" AutoGenerateColumns="False" CssClass="gridview" runat="server" Width="100%" AllowPaging="True" AllowSorting="True" ShowHeaderWhenEmpty="True"
			PagerStyle-CssClass="gridview-pager" HeaderStyle-CssClass="gridview-header" RowStyle-CssClass="gridview-rows">

			<HeaderStyle CssClass="gridview-header"></HeaderStyle>

			<PagerSettings Mode="NextPreviousFirstLast" FirstPageText="Go To First Page" LastPageText="Go To Last Page" Position="Top" />
			<AlternatingRowStyle CssClass="gridview-altrows" />
			<Columns>
				<asp:TemplateField HeaderText="Student ID">
					<HeaderTemplate>
						<asp:CheckBox ID="cbSelectAll" runat="server" />
						&nbsp;
					<asp:LinkButton ID="lbtnDelete" runat="server" OnCommand="Delete_Click" CommandName="lbtnDelete" CommandArgument='<%#Eval("patientID") %>'>Delete</asp:LinkButton>
					</HeaderTemplate>
					<ItemTemplate>
						<asp:CheckBox ID="chkStudentID" runat="server" AutoPostBack="false" />
                        <asp:Label ID="hidStudentID" runat="server" Text='<%#Eval("rxNo") %>' Visible="false"></asp:Label>
						</ItemTemplate>
				</asp:TemplateField>
                <asp:BoundField DataField="rxNo" HeaderText="Rx Number" SortExpression="rxNo" />
                <asp:BoundField DataField="patient" HeaderText="Patient" SortExpression="patient" />
                <asp:BoundField DataField="patientID" visible="false" />
				<asp:BoundField DataField="drug" HeaderText="Drug" SortExpression="drug" />
                <asp:BoundField DataField="drugID" visible="false" />
                <asp:BoundField DataField="physician" HeaderText="Physician" SortExpression="physician" />
                <asp:BoundField DataField="physicianID" Visible="false" />
				<asp:BoundField DataField="refillsLeft" HeaderText="Refills Left" SortExpression="refillsLeft" />
				<asp:HyperLinkField DataNavigateUrlFields="patientID" DataNavigateUrlFormatString="Display.aspx?ID={0}" HeaderText="View" Text="View" Target="_blank" />
				<asp:TemplateField HeaderText="Edit">
			<ItemTemplate>
				<asp:ImageButton ID="imgDelete" runat="server" CommandArgument='<% # Eval  ("rxNo") %>'
					OnCommand="Delete_Click" CommandName="lbtnDelete" ImageUrl="~/images/delete.svg" Height ="24" />||
				<asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<% # Eval  ("rxNo") %>'
					OnCommand="Edit_Click" CommandName="lbtnEdit" ImageUrl="~/images/edit.svg" Height ="24" />
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
</asp:Content>
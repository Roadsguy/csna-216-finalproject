<%@ Page Title="" Language="C#" MasterPageFile="~/Louis.master" AutoEventWireup="true" CodeBehind="Physician_view.aspx.cs" Inherits="FinalProject.physicians.physician_view" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script type="text/javascript">
        function ShowMap()
        {
            var map = null;
            var features = "width=220,height=250"
            features += "left=50,top=50,resize=yes,menu=no,status"
            map = window.open("physician_add.aspx", "mapwin", features);
            map.focus();
        }
	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="pageContent" runat="server">
	<br />
	<div id="information">
		Physician ID: <asp:TextBox ID="txtPhysicianID" runat="server"></asp:TextBox><br />
		Last Name:
		<asp:TextBox ID="txtLastName" runat="server"></asp:TextBox><br />
		First Name:
		<asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox><br />
		Employer:
		<asp:TextBox ID="txtEmployer" runat="server"></asp:TextBox>
		<br />
		<br />
		<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
		<br />
		<br />
		<asp:Button ID="btnAdd" runat="server" Text="Add Physician" OnClientClick="ShowMap(this)" OnClick="btnAdd_Click" />
		<br />
		<br />
		
    <asp:GridView ID="grdPhysicians" AutoGenerateColumns="False" CssClass="gridview" runat="server" Width="100%" AllowPaging="True" AllowSorting="True" ShowHeaderWhenEmpty="True"
			PagerStyle-CssClass="gridview-pager" HeaderStyle-CssClass="gridview-header" RowStyle-CssClass="gridview-rows" OnSelectedIndexChanged="grdPhysician_SelectedIndexChanged">

			<HeaderStyle CssClass="gridview-header"></HeaderStyle>

			<PagerSettings Mode="NextPreviousFirstLast" FirstPageText="Go To First Page" LastPageText="Go To Last Page" Position="Top" />
			<AlternatingRowStyle CssClass="gridview-altrows" />
			<Columns>
				<asp:TemplateField HeaderText="Physician ID">
					<HeaderTemplate>
						<asp:CheckBox ID="cbSelectAll" runat="server" />
						&nbsp;
			<asp:LinkButton ID="lbtnDelete" runat="server" OnCommand="Delete_Click" CommandName="lbtnDelete" CommandArgument='<%#Eval("physicianID") %>'>Delete</asp:LinkButton>
					</HeaderTemplate>
					<ItemTemplate>
						<asp:CheckBox ID="chkPhysicianID" runat="server" AutoPostBack="false" />
						<asp:Label ID="hidPhysicianID" runat="server" Text='<%#Eval("PhysicianID") %>' Visible="false"></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:HyperLinkField DataNavigateUrlFields="physicianID" DataNavigateUrlFormatString="Display.aspx?ID={0}" HeaderText="View" Text="View" Target="_blank" />
				<asp:BoundField DataField="fName" HeaderText="First Name" SortExpression="fName" />
				<asp:BoundField DataField="lName" HeaderText="Last Name" SortExpression="lName" />
				<asp:BoundField DataField="mInit" HeaderText="Middle Initial" SortExpression="mInit" />
				<asp:BoundField DataField="employer" HeaderText="Employer" SortExpression="employer" />
		
				<asp:TemplateField HeaderText="Edit">
					<ItemTemplate>
						<asp:ImageButton ID="imgDelete" runat="server" CommandArgument='<% # Eval("physicianID") %>'
							OnCommand="Delete_Click" CommandName="lbtnDelete" ImageUrl="~/images/delete.svg" Height="24" />||

					<asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<% # Eval("physicianID") %>'
						OnCommand="Edit_Click" CommandName="lbtnEdit" ImageUrl="~/images/edit.svg" Height="24" />

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
	</div>
</asp:Content>
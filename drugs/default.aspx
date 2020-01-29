<%@ Page Title="" Language="C#" MasterPageFile="~/Louis.master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FinalProject.drugs._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContent" runat="server">
	Drug ID: <asp:TextBox ID="txtDrugID" runat="server"></asp:TextBox><br />
	Drug Name: <asp:TextBox ID="txtDrugName" runat="server"></asp:TextBox><br />
	Drug Description: <asp:TextBox ID="txtDrugDesc" runat="server"></asp:TextBox>
	<br /><br />
	<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
	&nbsp;
    <asp:Button ID="btnAddDrug" runat="server" Text="Add Drug" />
	<br /><br />
	<asp:GridView ID="grdDrugs" AutoGenerateColumns="False" CssClass="gridview" runat="server" Width="100%" AllowPaging="True" AllowSorting="True" ShowHeaderWhenEmpty="True"
			PagerStyle-CssClass="gridview-pager" HeaderStyle-CssClass="gridview-header" RowStyle-CssClass="gridview-rows">

			<HeaderStyle CssClass="gridview-header"></HeaderStyle>

			<PagerSettings Mode="NextPreviousFirstLast" FirstPageText="Go To First Page" LastPageText="Go To Last Page" Position="Top" />
			<AlternatingRowStyle CssClass="gridview-altrows" />
			<Columns>
				<asp:TemplateField HeaderText="Drug ID">
					<HeaderTemplate>
						<asp:CheckBox ID="cbSelectAll" runat="server" />
						&nbsp;
					<asp:LinkButton ID="lbtnDelete" runat="server" OnCommand="Delete_Click" CommandName="lbtnDelete" CommandArgument='<%#Eval("drugID") %>'>Delete</asp:LinkButton>
					</HeaderTemplate>
					<ItemTemplate>
						<asp:CheckBox ID="chkDrugID" runat="server" AutoPostBack="false" />
						<asp:Label ID="hidDrugID" runat="server" Text='<%#Eval("drugID") %>' Visible="false"></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:BoundField DataField="drugID" HeaderText="Drug ID" SortExpression="drugID" />
				<asp:BoundField DataField="drugName" HeaderText="Drug Name" SortExpression="drugName" />
				<asp:BoundField DataField="drugDesc" HeaderText="Drug Description" SortExpression="drugDesc" />
				<asp:BoundField DataField="methodOfAdmin" HeaderText="Method Of Administration" SortExpression="methodOfAdmin" />
				<asp:HyperLinkField DataNavigateUrlFields="drugID" DataNavigateUrlFormatString="Display.aspx?ID={0}" HeaderText="View" Text="View" Target="_blank" />
				<asp:TemplateField HeaderText="Edit">
					<ItemTemplate>
						<asp:ImageButton ID="imgDelete" runat="server" CommandArgument='<% # Eval("drugID") %>'
							OnCommand="Delete_Click" CommandName="lbtnDelete" ImageUrl="~/images/delete.svg" Height="24" />||
					<asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<% # Eval("drugID") %>'
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
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Louis.master" AutoEventWireup="true" CodeBehind="patients_search.aspx.cs" Inherits="FinalProject.patients.patients_search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContent" runat="server">
	<script type="text/javascript"> 
		function loadDoc() {
			var xhttp = new XMLHttpRequest();
			xhttp.onreadystatechange = function () {
				if (this.readyState == 4 && this.status == 200) {
					document.getElementById("content").innerHTML = this.responseText;
				}
			};
			xhttp.open("GET", "patients_add.txt", true);
			xhttp.send();
		}

		function ajaxTest() {
			$.ajax({
				type: "POST",
				url: "/patients/GetData",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (response) {
					$("#content").text(response.d);
				},
				failure: function (response) {
					alert(response.d);
				}
			});
		}

		function loadTest2() {
			$('#div1').load('/ #content');
		}
	</script>
	<div id="content">
		Patient ID: <asp:TextBox ID="txtStudentID" runat="server"></asp:TextBox><br />
		Last Name:
		<asp:TextBox ID="txtLastName" runat="server"></asp:TextBox><br />
		First Name:
		<asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
		<br />
		<br />
		<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" Text="Add Patient" PostBackUrl="Default.aspx" />
		<br />
		<br />
		<button id="ajaxbtn" type="button" runat="server" onclick="loadTest2()">Change Content</button>
		<br />
		<br />
		<asp:GridView ID="grdPatients" AutoGenerateColumns="False" CssClass="gridview" runat="server" Width="100%" AllowPaging="True" AllowSorting="True" ShowHeaderWhenEmpty="True"
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
						<asp:Label ID="hidStudentID" runat="server" Text='<%#Eval("patientID") %>' Visible="false"></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:BoundField DataField="lName" HeaderText="Last Name" SortExpression="lName" />
				<asp:BoundField DataField="fName" HeaderText="First Name" SortExpression="fName" />
				<asp:BoundField DataField="mInit" HeaderText="Middle Initial" SortExpression="mInit" />
				<asp:BoundField DataField="gender" HeaderText="Gender" SortExpression="gender" />
				<asp:BoundField DataField="dateOfBirth" HeaderText="Date of Birth" SortExpression="dateOfBirth" DataFormatString="{0:yyyy-MM-dd}" />
				<asp:BoundField DataField="acctBalance" HeaderText="Account Balance" SortExpression="acctBalance" DataFormatString="{0:C}" />
				<asp:BoundField DataField="insuranceCo" HeaderText="Insurance" SortExpression="insuranceCo" />
				<asp:HyperLinkField DataNavigateUrlFields="patientID" DataNavigateUrlFormatString="Display.aspx?ID={0}" HeaderText="View" Text="View" Target="_blank" />
				<asp:TemplateField HeaderText="Edit">
					<ItemTemplate>
						<asp:ImageButton ID="imgDelete" runat="server" CommandArgument='<% # Eval("patientID") %>'
							OnCommand="Delete_Click" CommandName="lbtnDelete" ImageUrl="~/images/delete.svg" Height="24" />||
					<asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<% # Eval("patientID") %>'
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

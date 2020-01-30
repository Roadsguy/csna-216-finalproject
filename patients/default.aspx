<%@ Page Title="" Language="C#" MasterPageFile="~/Louis.master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FinalProject.patients._default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC1" %>
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

		function goToView(sender, rowPatientID) {
			var vpatientID = $("#<%=txtSrchPatientID.ClientID %>").val();
			var vlastName = $("#<%=txtSrchLastName.ClientID %>").val();
			var vfirstName = $("#<%=txtSrchFirstName.ClientID %>").val();

			var senderID = $(sender).attr('id');
			var viewType;

			switch (senderID) {
				case "imgBtnView":
					viewType = "<% =cipher.Encrypt("view") %>";
					break;
				case "imgBtnEdit":
					viewType = "<% =cipher.Encrypt("edit") %>";
					break;
				case "btnAdd":
					viewType = "<% =cipher.Encrypt("add") %>";
					break;
				default:
					alert('Invalid senderID: ' + senderID);
					return false;
			};

			$.ajax({
				type: "POST",
				url: "default.aspx/SrchPatSaveSession",
				contentType: "application/json; charset=utf-8",
				data: JSON.stringify({ "patientID": vpatientID, "lastName": vlastName, "firstName": vfirstName }),
				dataType: "json",
				success: function () {
					//alert('It works!');
					switch (senderID) {
						case "imgBtnView":
						case "imgBtnEdit":
							$('#content').load('/patients/patients_vieweditadd.aspx #content', { type: viewType, ID: rowPatientID });
							break;
						case "btnAdd":
							$('#content').load('/patients/patients_vieweditadd.aspx #content', { type: viewType });
							break;
					}
				},
				error: function () {
					alert('failed');
				}
			});

			//$('#content').load('/patients/patients_vieweditadd.aspx #content', { ID: 'S0001' });
		}

		function goToSearch() {
			$('#content').load('/patients/default.aspx #content', { refresh: 'true' });
		}
	</script>
	<div id="content">
		<script>
			$(function () {
				$("#txtSrchPatientID").datepicker();
			});
		</script>

		Patient ID: <asp:TextBox ID="txtSrchPatientID" runat="server" ClientIDMode="static" data-lpignore="true"></asp:TextBox><br />
		Last Name:
		<asp:TextBox ID="txtSrchLastName" runat="server" data-lpignore="true"></asp:TextBox><br />
		First Name:
		<asp:TextBox ID="txtSrchFirstName" runat="server" data-lpignore="true"></asp:TextBox>
		<br />
		<br />
		<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		<asp:Button ID="btnAdd" ClientIDMode="static" runat="server" Text="Add Patient" OnClientClick="goToView(this); return false;" />
		<br />
		<br />
		<asp:GridView ID="grdPatients" AutoGenerateColumns="False" CssClass="gridview" runat="server" Width="100%" AllowPaging="True" AllowSorting="True" OnSorting="grdPatients_Sorting" ShowHeaderWhenEmpty="True"
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
				<asp:TemplateField HeaderText="">
					<ItemTemplate>
						<asp:ImageButton ID="imgBtnView" ClientIDMode="static" runat="server" src="/images/view.svg" Height="24"
							OnClick=<%# "goToView(this, '" + cipher.Encrypt(Eval("patientID").ToString()) + "');return false;" %> ToolTip="View Record" />
						&nbsp;&nbsp;
						<asp:ImageButton ID="imgBtnDelete" ClientIDMode="static" runat="server" CommandArgument='<% # Eval("patientID") %>'
							OnCommand="Delete_Click" CommandName="lbtnDelete" ImageUrl="/images/delete.svg" Height="24" ToolTip="Delete Record" />
						&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:ImageButton ID="imgBtnEdit" ClientIDMode="static" runat="server" src="/images/edit.svg" height="24"
							OnClick=<%# "goToView(this, '" + cipher.Encrypt(Eval("patientID").ToString()) + "');return false;" %> ToolTip="Edit Record" />
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

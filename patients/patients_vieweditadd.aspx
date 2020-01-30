<%@ Page Title="" Language="C#" MasterPageFile="~/Louis.master" AutoEventWireup="true" CodeBehind="patients_vieweditadd.aspx.cs" Inherits="FinalProject.patients.patients_vieweditadd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContent" runat="server">
	<div id="content">
		<script>
			$(function () {
				$("#txtDateOfBirth").datepicker();
			});
		</script>

		<h2 class="pageheader"><asp:Label ID="lblPageHeader" runat="server" Text=""></asp:Label></h2>
		Patient ID: <asp:TextBox ID="txtPatientID" runat="server" data-lpignore="true"></asp:TextBox><br />
		Last Name: <asp:TextBox ID="txtLastName" runat="server" data-lpignore="true"></asp:TextBox><br />
		First Name: <asp:TextBox ID="txtFirstName" runat="server" data-lpignore="true"></asp:TextBox><br />
		Middle Initial: <asp:TextBox ID="txtMidInit" runat="server" data-lpignore="true"></asp:TextBox><br />
		Gender: <asp:RadioButton ID="rdoGenderM" runat="server" GroupName="gender" Text="Male" /><asp:RadioButton ID="rdoGenderF" runat="server" GroupName="gender" Text="Female" /><br />
		Date of Birth: <asp:TextBox ID="txtDateOfBirth" runat="server" ClientIDMode="static" data-lpignore="true"></asp:TextBox><br />
		Street Address, Line 1: <asp:TextBox ID="txtAddrLine1" runat="server" data-lpignore="true"></asp:TextBox><br />
		Street Address, Line 2: <asp:TextBox ID="txtAddrLine2" runat="server" data-lpignore="true"></asp:TextBox><br />
		City: <asp:TextBox ID="txtAddrCity" runat="server" data-lpignore="true"></asp:TextBox><br />
		State: <asp:DropDownList ID="ddlAddrState" runat="server"></asp:DropDownList><br />
		ZIP Code: <asp:TextBox ID="txtAddrZip" runat="server" data-lpignore="true"></asp:TextBox><br />
		Primary Email: <asp:TextBox ID="txtEmail1" runat="server" data-lpignore="true"></asp:TextBox><br />
		Secondary Email: <asp:TextBox ID="txtEmail2" runat="server" data-lpignore="true"></asp:TextBox><br />
		Home Phone No.: <asp:TextBox ID="txtHomePhoneNo" runat="server" data-lpignore="true"></asp:TextBox><br />
		Cell Phone No.: <asp:TextBox ID="txtCellPhoneNo" runat="server" data-lpignore="true"></asp:TextBox><br />
		Account Balance: <asp:TextBox ID="txtAcctBalance" runat="server" data-lpignore="true"></asp:TextBox><br />
		Insurance Company: <asp:TextBox ID="txtInsuranceCo" runat="server" data-lpignore="true"></asp:TextBox>
		<br /><br />
		<asp:Button ID="btnBack" runat="server" Text="Go Back" OnClientClick="goToSearch(); return false;" />
		<br />
		<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
	</div>
</asp:Content>

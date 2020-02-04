<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="patients_vieweditadd.ascx.cs" Inherits="FinalProject.patients.patients_vieweditadd1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC1" %>
<h2 id="pageheader"><asp:Label ID="lblPageHeader" runat="server" Text="ViewEditAdd Patients"></asp:Label></h2>

<div class="inputwrapper">
	<div class="inputbox">
		<div class="inputboxheader">Personal Information</div>
		<div class="inputleft_narrow">
			Patient ID:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtPatientID" runat="server" CssClass="narrow" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_narrow">
			Last Name:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtLastName" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_narrow">
			First Name:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtFirstName" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_narrow">
			Mid. Initial:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtMidInit" runat="server" CssClass="narrow" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_narrow">
			Gender:
		</div>
		<div class="inputright">
			<asp:RadioButton ID="rdoGenderM" runat="server" GroupName="gender" CssClass="radiobtn" Text="Male" />
			<asp:RadioButton ID="rdoGenderF" runat="server" GroupName="gender" CssClass="radiobtn" Text="Female" />
		</div>
		<div class="inputleft_narrow">
			Birth Date:
		</div>
		<div class="inputright">
			<CC1:CalendarExtender ID="calDOB" runat="server" TargetControlID="txtDateOfBirth" Format="yyyy-MM-dd"
			PopupPosition="BottomLeft" CssClass="calendar" />
			<asp:TextBox ID="txtDateOfBirth" runat="server" ClientIDMode="static" data-lpignore="true"></asp:TextBox>
		</div>
	</div>

	<div class="inputbox">
		<div class="inputboxheader">Location</div>
		<div class="inputleft_wide">
			Street Address:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtAddrLine1" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			Street Address 2
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtAddrLine2" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			City:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtAddrCity" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			State:
		</div>
		<div class="inputright">
			<asp:DropDownList ID="ddlAddrState" runat="server" CssClass="state" data-lpignore="true"></asp:DropDownList>
		</div>
		<div class="inputleft_wide">
			ZIP Code:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtAddrZip" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
	</div>
</div>
<div class="inputwrapper">
	<div class="inputbox">
		<div class="inputboxheader">Contact Information</div>
		<div class="inputleft_wide">
			Primary Email:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtEmail1" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			Secondary Email:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtEmail2" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			Home Phone No.:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtHomePhoneNo" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			Cell Phone No.:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtCellPhoneNo" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
	</div>
	<div class="inputbox">
		<div class="inputboxheader">Financial Details</div>
		<div class="inputleft_wide">
			Account Balance:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtAcctBalance" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			Insurance Co.:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtInsuranceCo" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
	</div>
</div>
<div class="inputboxbuttons">
	<asp:LinkButton ID="btnGoBack" runat="server" CssClass="button" Text="Go Back" OnClick="btnGoBack_Click" />
	<asp:LinkButton ID="btnSubmit" runat="server" CssClass="button" Text="Submit" OnClick="btnSubmit_Click" />
</div>
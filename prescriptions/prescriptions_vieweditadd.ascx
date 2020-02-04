<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="prescriptions_vieweditadd.ascx.cs" Inherits="FinalProject.prescriptions.prescriptions_vieweditadd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC1" %>
<h2 id="pageheader"><asp:Label ID="lblPageHeader" runat="server" Text="ViewEditAdd Patients"></asp:Label></h2>

<div class="inputwrapper">
	<div class="inputbox">
		<div class="inputboxheader">Reference Information</div>
		<div class="inputleft_narrow">
			Rx No.:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtRxNo" runat="server" CssClass="narrow" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_narrow">
			Patient ID:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtPatientID" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_narrow">
			Drug ID:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtDrugID" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_narrow">
			Physician ID:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtPhysicianID" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
	</div>
</div>
<div class="inputwrapper">
	<div class="inputbox">
		<div class="inputboxheader">Prescription Details</div>
		<div class="inputleft_wide">
			Dosage:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtDosage" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			Frequency:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtFrequency" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			No. of Refills:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtRefillCount" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			Refills Left:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtRefillsLeft" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			Cost:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtCost" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			Start Date:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtStartDate" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			Finish Date:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtFinishDate" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
	</div>
</div>
<div class="inputwrapper">
	<div class="inputbox">
		<div class="inputboxheader">Prescription Refills</div>
		<em>Ignore this, I'll add it. —Albert</em>
	</div>
</div>
<div class="inputboxbuttons">
	<asp:LinkButton ID="btnGoBack" runat="server" CssClass="button" Text="Go Back" OnClick="btnGoBack_Click" />
	<asp:LinkButton ID="btnSubmit" runat="server" CssClass="button" Text="Submit" OnClick="btnSubmit_Click" />
</div>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="prescriptions_vieweditadd.ascx.cs" Inherits="FinalProject.prescriptions.prescriptions_vieweditadd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC1" %>
<h2 id="pageheader"><asp:Label ID="lblPageHeader" runat="server" Text="ViewEditAdd Prescription"></asp:Label></h2>

<div class="inputwrapper">
	<div class="inputbox">
		<div class="inputboxheader">Reference Information</div>
		<div class="inputleft_narrow">
			Rx No.:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtRxNo" runat="server" CssClass="narrow" data-lpignore="true"></asp:TextBox>
		</div>
		<div ID="divInputByID" runat="server" visible="false">
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
		<div ID="divInputByName" runat="server" visible="true">
			<div class="inputleft_narrow">
				Patient:
			</div>
			<div class="inputright">
				<asp:DropDownList ID="ddlPatientName" runat="server" CssClass="search" AppendDataBoundItems="true" data-lpignore="true"></asp:DropDownList>
			</div>
			<div class="inputleft_narrow">
				Drug:
			</div>
			<div class="inputright">
				<asp:DropDownList ID="ddlDrugName" runat="server" CssClass="search" AppendDataBoundItems="true" data-lpignore="true"></asp:DropDownList>
			</div>
			<div class="inputleft_narrow">
				Physician:
			</div>
			<div class="inputright">
				<asp:DropDownList ID="ddlPhysicianName" runat="server" CssClass="search" AppendDataBoundItems="true" data-lpignore="true"></asp:DropDownList>
			</div>
		</div>
		<div class="inputboxbuttons">
			<asp:RadioButton ID="rdoInputByName" runat="server" GroupName="inputOpt" CssClass="radiobtn" Text="Input Names" OnCheckedChanged="SwitchInputType" AutoPostBack="true" />
			<asp:RadioButton ID="rdoInputByID" runat="server" GroupName="inputOpt" CssClass="radiobtn" Text="Input IDs" OnCheckedChanged="SwitchInputType" AutoPostBack="true" />
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
			<asp:TextBox ID="txtRefillsGiven" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div id="divRefillsLeft" runat="server">
			<div class="inputleft_wide">
				Refills Left:
			</div>
			<div class="inputright">
				<asp:TextBox ID="txtRefillsLeft" runat="server" data-lpignore="true"></asp:TextBox>
			</div>
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
			<CC1:CalendarExtender ID="calStartDate" runat="server" TargetControlID="txtStartDate" Format="yyyy-MM-dd"
			PopupPosition="BottomLeft" CssClass="calendar" />
			<asp:TextBox ID="txtStartDate" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			Finish Date:
		</div>
		<div class="inputright">
			<CC1:CalendarExtender ID="calFinishDate" runat="server" TargetControlID="txtFinishDate" Format="yyyy-MM-dd"
			PopupPosition="BottomLeft" CssClass="calendar" />
			<asp:TextBox ID="txtFinishDate" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
	</div>
</div>
<div class="inputboxbuttons">
	<asp:LinkButton ID="btnGoBack" runat="server" CssClass="button" Text="Go Back" OnClick="btnGoBack_Click" />
	<asp:LinkButton ID="btnSubmit" runat="server" CssClass="button" Text="Submit" OnClick="btnSubmit_Click" />
	<asp:LinkButton ID="btnRefill" runat="server" CssClass="button" Text="Refill" OnClick="btnRefill_Click" />
</div>
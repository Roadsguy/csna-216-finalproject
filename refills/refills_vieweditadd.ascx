<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="refills_vieweditadd.ascx.cs" Inherits="FinalProject.refills.refills_vieweditadd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC1" %>
<h2 id="pageheader"><asp:Label ID="lblPageHeader" runat="server" Text="ViewEditAdd Refills"></asp:Label></h2>

<div class="inputwrapper">
	<div class="inputbox">
		<div class="inputleft_wide">
			Rx No.:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtRxNo" runat="server" CssClass="narrow" data-lpignore="true"></asp:TextBox>
		</div>
		<div id="divRefillNo" runat="server">
			<div class="inputleft_wide">
				Refill No.:
			</div>
			<div class="inputright">
				<asp:TextBox ID="txtRefillNo" runat="server" data-lpignore="true"></asp:TextBox>
			</div>
		</div>
		<div class="inputleft_wide">
			Time of Refill:
		</div>
		<div class="inputright">
			<CC1:CalendarExtender ID="calRefillDate" runat="server" TargetControlID="txtRefillDateTime" Format="yyyy-MM-dd"
			PopupPosition="BottomLeft" CssClass="calendar" />
			<asp:TextBox ID="txtRefillDateTime" runat="server" ClientIDMode="static" data-lpignore="true"></asp:TextBox>
		</div>
	</div>
</div>
<div class="inputboxbuttons">
	<asp:LinkButton ID="btnGoBack" runat="server" CssClass="button" Text="Go Back" OnClick="btnGoBack_Click" />
	<asp:LinkButton ID="btnSubmit" runat="server" CssClass="button" Text="Submit" OnClick="btnSubmit_Click" />
</div>
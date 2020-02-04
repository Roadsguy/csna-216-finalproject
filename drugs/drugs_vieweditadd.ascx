<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="drugs_vieweditadd.ascx.cs" Inherits="FinalProject.drugs.drugs_vieweditadd" %>
<h2 id="pageheader"><asp:Label ID="lblPageHeader" runat="server" Text="ViewEditAdd Drugs"></asp:Label></h2>

<div class="inputwrapper">
	<div class="inputbox">
		<div class="inputboxheader">Personal Information</div>
		<div class="inputleft_wide">
			Drug ID:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtDrugID" runat="server" CssClass="narrow" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			Drug Name:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtDrugName" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			Drug Description:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtDrugDesc" runat="server" data-lpignore="true"></asp:TextBox>
		</div>
		<div class="inputleft_wide">
			Method of Admin.:
		</div>
		<div class="inputright">
			<asp:TextBox ID="txtMethodOfAdmin" runat="server" ClientIDMode="static" data-lpignore="true"></asp:TextBox>
		</div>
	</div>
</div>
<div class="inputboxbuttons">
	<asp:LinkButton ID="btnGoBack" runat="server" CssClass="button" Text="Go Back" OnClick="btnGoBack_Click" />
	<asp:LinkButton ID="btnSubmit" runat="server" CssClass="button" Text="Submit" OnClick="btnSubmit_Click" />
</div>
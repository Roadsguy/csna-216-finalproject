<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajaxtest.aspx.cs" Inherits="FinalProject.patients.ajaxtest" %>
<%@ Register src="UCtest1.ascx" TagName="test1" TagPrefix="test1" %>
<%@ Register src="UCtest2.ascx" tagname="test2" tagprefix="test2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<script type="text/javascript">
		// JavaScript function to call inside UpdatePanel
		function jScript() {
			$("#click").click(function () {
				alert("Clicked Me!");
			});
		}

		function clicked() {
			alert("Clicked Me!");
		}

		function loadMe() {
			$('#UpdatePanel1').load('/patients/ajaxtest2.aspx #UpdatePanel1');
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
		<asp:UpdatePanel runat="server" ID="updTest">
			<ContentTemplate>

			</ContentTemplate>
		</asp:UpdatePanel>

		<asp:UpdatePanel ID="updTest2" runat="server">
			<ContentTemplate>
				<test2:test2 runat="server" ID="test2"></test2:test2>
			</ContentTemplate>
		</asp:UpdatePanel>

		<asp:UpdatePanel ID="updClear" runat="server">
			<ContentTemplate>
				<asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
				<asp:Button ID="btnLoad" runat="server" Text="Load Test" OnClick="btnLoad_Click" />
			</ContentTemplate>
		</asp:UpdatePanel>
	</form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajaxtest.aspx.cs" Inherits="FinalProject.patients.ajaxtest" %>

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
	</script>
</head>
<body>
	<form id="form1" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server">
		</asp:ScriptManager>
		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
			<ContentTemplate>
				<script type="text/javascript">
					Sys.Application.add_load(jScript);
				</script>
				<asp:Button ID="btnPostBack" runat="server"
					OnClick="btnPostBack_Click" Text="Click To Postback" />
				<a href="#" id="click" onclick="clicked()">Click Me!</a>
			</ContentTemplate>
		</asp:UpdatePanel>
		<asp:Button ID="btnTest" runat="server" Text="Button" OnClick="btnTest_Click" /><asp:Label ID="lblTest" runat="server" Text="text"></asp:Label>
	</form>
</body>
</html>

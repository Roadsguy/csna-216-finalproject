<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajaxtest2.aspx.cs" Inherits="FinalProject.patients.ajaxtest2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server">
		</asp:ScriptManager>
		<asp:UpdatePanel ID="UpdatePanel1" ClientIDMode="static" runat="server">
			<ContentTemplate>
				<script type="text/javascript">
					Sys.Application.add_load(jScript);
				</script>
				<asp:Button ID="btnPostBack" runat="server"
					OnClick="btnPostBack_Click" Text="Click To Postback" />
				<a href="#" id="click" onclick="clicked()">Click Me!</a>
			</ContentTemplate>
		</asp:UpdatePanel>
	</form>
</body>
</html>

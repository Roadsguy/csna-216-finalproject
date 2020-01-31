<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCtest1.ascx.cs" Inherits="FinalProject.patients.WebUserControl1" %>
<asp:TextBox ID="txtTest1" runat="server" Enabled="false"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="txtTest1_CalendarExtender" runat="server" BehaviorID="txtTest1_CalendarExtender" TargetControlID="txtTest1" />
<asp:Button ID="btnTest1" runat="server" Text="Try it 1" OnClick="btnTest1_Click" />

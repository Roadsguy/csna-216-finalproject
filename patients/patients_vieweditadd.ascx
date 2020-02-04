<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="patients_vieweditadd.ascx.cs" Inherits="FinalProject.patients.patients_vieweditadd1" %>
<h2 class="pageheader"><asp:Label ID="lblPageHeader" runat="server" Text="ViewEditAdd"></asp:Label></h2>
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
<asp:Button ID="btnGoBack" runat="server" Text="Go Back" OnClick="btnGoBack_Click" />
<br />
<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
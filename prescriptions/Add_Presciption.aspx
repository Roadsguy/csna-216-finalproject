<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Presciption.aspx.cs" Inherits="FinalProject.prescriptions.Add_Presciption" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 79px;
        }
        .auto-style3 {
            width: 79px;
            height: 23px;
        }
        .auto-style4 {
            height: 23px;
        }
        .auto-style5 {
            width: 79px;
            height: 45px;
        }
        .auto-style6 {
            height: 45px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style3">Rx Number:</td>
                <td class="auto-style4">
                    <br />
                    <asp:TextBox ID="txtAddRxNo" runat="server" OnTextChanged="txtAddRxNo_TextChanged"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Patient ID:</td>
                <td>
                    <br />
                    <asp:TextBox ID="txtAddPatientID" runat="server"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Drug ID:</td>
                <td class="auto-style6">
                    <br />
                    <asp:TextBox ID="txtAddDrugID" runat="server"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Physician ID:</td>
                <td class="auto-style4">
                    <br />
                    <asp:TextBox ID="txtAddDocID" runat="server"></asp:TextBox>
                    <br />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

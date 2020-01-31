<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Physician_add.aspx.cs" Inherits="FinalProject.physicians.physician_add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Physician</title>

    <script type="text/javascript">
        function closeWindow()
        {
            this.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">

        <div>
             <h2>Add Physician Information</h2>
        
    First Name:
    <asp:TextBox ID="Fname" runat="server"></asp:TextBox>
    <br />
    <br />
    Last Name:
     <asp:TextBox ID="Lname" runat="server"></asp:TextBox>
    <br />
    <br />
    Middle Initial:
     <asp:TextBox ID="MI" runat="server"></asp:TextBox>
    <br />
    <br />
    Gender:
     <asp:TextBox ID="Gender" runat="server"></asp:TextBox>
    <br />
    <br />
    DOB:
     <asp:TextBox ID="DOB" runat="server"></asp:TextBox>
    <br />
    <br />
        </div>
        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" OnClientClick="closeWindow()" />
    </form>
</body>
</html>
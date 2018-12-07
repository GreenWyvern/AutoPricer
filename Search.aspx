<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Search</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            Enter the criterias, leave the other ones blink
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        Model Name:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tbModel" runat="server"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        Make:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tbMake" runat="server"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        Milage:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tbMilageMin" runat="server" type="number"/> to  <asp:TextBox ID="tbMilageMax" runat="server" type="number"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        Age:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tbAgeMin" runat="server" type="number"/> to  <asp:TextBox ID="tbAgeMax" runat="server" type="number"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        Engine:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tbEngine" runat="server"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        Condition:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tbCondition" runat="server"/>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click"/><br/>
            <asp:Label ID="labelWarning" runat="server" Text=""/>
</asp:Content>

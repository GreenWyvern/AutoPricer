<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Homepage.aspx.cs" Inherits="Homepage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>HomePage</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <td>
    <div >
        <asp:DropDownList id="Select1" runat="server"  AutoPostBack="True"  OnSelectedIndexChanged="goToPages">
            <asp:ListItem >User Options</asp:ListItem>
            <asp:ListItem >Profile</asp:ListItem>
            <asp:ListItem >List</asp:ListItem>
            <asp:ListItem >Management</asp:ListItem>
        </asp:DropDownList>
        </br>
        <asp:Literal ID="test" runat="server"></asp:Literal>
    </div>
    <div>
        <h1 style="text-align:center;">Auto Pricer </h1>
        <hr>
        <p style="font-weight: bold">Hello there, welcome to AutoPricer, Canada&#39;s #32rd place to buy and sell old or used cars</p>
        <hr>
        <h3>Check out the FAQ for more info!</h3>
    </div>
        </td>
        <td>
            <asp:AdRotator ID="AdRotator1" runat="server" DataSourceID="XmlDataSource1" style="width:150px; height:100px"/>
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/Ad.xml"></asp:XmlDataSource>
        </td>     
    </table>
</asp:Content>

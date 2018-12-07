<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Homepage.aspx.cs" Inherits="Homepage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>HomePage</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <td>
    <div >
        <select id="Select1" runat="server" onselect="goToPages">
            <option>Profile</option>
            <option>List</option>
            <option>Management</option>
            <option>FAQ</option>
        </select>
    </div>
    <div>
        <h1 style="text-align:center;">Auto Pricer </h1>
        <hr>
        <h3>Hello there, welcome to autopricer where you can</h3>
        <hr>
        <h3>about us xxx</h3>
    </div>
        </td>
        <td>
            <asp:AdRotator ID="AdRotator1" runat="server" DataSourceID="XmlDataSource1" style="width:150px; height:100px"/>
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/Ad.xml"></asp:XmlDataSource>
        </td>     
    </table>
</asp:Content>

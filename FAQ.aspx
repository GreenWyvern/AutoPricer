<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master" CodeFile="FAQ.aspx.cs" Inherits="FAQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>FAQ</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="font-family: Verdana, Geneva, Tahoma, sans-serif; margin-right: 150px; margin-left: 150px">
        <div>
            <h1>FAQ</h1>
        </div>

        <h3>What is AutoPricer?</h3>
        <p>AutoPricer is a place to put up listings for your old and used cars, and to find deals on other people's old and used cars!</p>

        <h3>How do I buy a car?</h3>
        <p>Easy! Find a car you like, and then request the contact information of the seller! From there, you can work out a deal with the seller by communicating outside of AutoPricer.</p>

        <h3>How do I sell a car?</h3>
        <p>Just sign up, log in, and then fill out the car selling form. Then, wait for someone who finds interest in your listing!</p>

        <h3>How do I know you won't steal my credit card info?</h3>
        <p>Because you don't enter it anywhere in the site. It is physically impossible for us to steal your credit card information.</p>

        <h3>Are you sure?</h3>
        <p>Positive.</p>

        <h3>Really?</h3>
        <p>Are you a cop?</p>

        <asp:Button ID="btnBack" runat="server" Text="Back to Homepage" OnClick="btnBack_Click"/>
</div>
</asp:Content>

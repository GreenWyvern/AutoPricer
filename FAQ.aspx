<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FAQ.aspx.cs" Inherits="FAQ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-family: Verdana, Geneva, Tahoma, sans-serif; margin-right: 150px; margin-left: 150px">
    <form id="form1" runat="server">
        <div>
            <h1>FAQ</h1>
        </div>

        <h3>What is AutoPricer?</h3>
        <p>AutoPricer is a place to put up listings for your old and used cars, and to find deals on other people's old and used cars!</p>

        <h3>How do I buy a car?</h3>
        <p>Easy! Find a car you like, and then request the contact information of the seller! From there, you can work out a deal with the seller by communicating outside of AutoPricer.</p>

        <h3>How do I sell a car?</h3>
        <p>Just sign up, log in, and then fill out the car selling form. Then, wait for someone who finds interest in your listing!</p>

        <asp:Button ID="btnBack" runat="server" Text="Back to Homepage" OnClick="btnBack_Click"/>

    </form>
</body>
</html>

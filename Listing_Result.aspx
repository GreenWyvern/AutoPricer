<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Listing_Result.aspx.cs" Inherits="Listing_Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Listing Results</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

            <asp:DataList ID="DataList1" runat="server" DataKeyField="ListingID" DataSourceID="SqlDataSource1" OnItemCommand="DataList1_ItemCommand" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" >
                <ItemTemplate>
                    Username:
                    <asp:Label ID="UsernameLabel" runat="server" Text='<%# Eval("Username") %>' />
                    <br />
                    SuggestedPrice:
                    <asp:Label ID="SuggestedPriceLabel" runat="server" Text='<%# Eval("SuggestedPrice") %>' />
<br />              ListingStatus:
                    <asp:Label ID="ListingStatusLabel" runat="server" Text='<%# Eval("ListingStatus") %>' />
                    <br />
                    Model:
                    <asp:Label ID="ModelLabel" runat="server" Text='<%# Eval("Model") %>' />
                    <br />
                    Make:
                    <asp:Label ID="MakeLabel" runat="server" Text='<%# Eval("Make") %>' />
                    <br />
                    Milage:
                    <asp:Label ID="MilageLabel" runat="server" Text='<%# Eval("Milage") %>' />
                    <br />
                    Engine:
                    <asp:Label ID="EngineLabel" runat="server" Text='<%# Eval("Engine") %>' />
                    <br />
                    Condition:
                    <asp:Label ID="ConditionLabel" runat="server" Text='<%# Eval("Condition") %>' />
                    <br />
                    <br />
                </ItemTemplate>
            </asp:DataList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Listing]  INNER JOIN [Car] ON Listing.CarID=Car.CarID"></asp:SqlDataSource>
</asp:Content>
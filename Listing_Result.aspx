<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Listing_Result.aspx.cs" Inherits="Listing_Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Listing Results</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

            <asp:DataList ID="DataList1" runat="server" DataKeyField="ListingID" DataSourceID="SqlDataSource1" OnItemCommand="DataList1_ItemCommand" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" >
                <EditItemTemplate>
                    ListingID:
                    <asp:Label ID="ListingIDLabel" runat="server" Text='<%# Eval("ListingID") %>' />
                    <br />
                    Username:
                    <asp:Label ID="UsernameLabel" runat="server" Text='<%# Eval("Username") %>' />
                    <br />
                    CarID:
                    <asp:Label ID="CarIDLabel" runat="server" Text='<%# Eval("CarID") %>' />
                    <br />
                    SuggestedPrice:<asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("SuggestedPrice") %>'></asp:TextBox>
&nbsp;<br /> ListingStatus:
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Eval("ListingStatus") %>'></asp:TextBox>
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Update" CommandName="Update"  />
                    <asp:Button ID="Button2" runat="server" Text="Cancel" CommandName="Cancel" />
                </EditItemTemplate>
                <ItemTemplate>
                    ListingID:
                    <asp:Label ID="ListingIDLabel" runat="server" Text='<%# Eval("ListingID") %>' />
                    <br />
                    Username:
                    <asp:Label ID="UsernameLabel" runat="server" Text='<%# Eval("Username") %>' />
                    <br />
                    CarID:
                    <asp:Label ID="CarIDLabel" runat="server" Text='<%# Eval("CarID") %>' />
                    <br />
                    SuggestedPrice:
                    <asp:Label ID="SuggestedPriceLabel" runat="server" Text='<%# Eval("SuggestedPrice") %>' />
                    <br />
                    ListingStatus:
                    <asp:Label ID="ListingStatusLabel" runat="server" Text='<%# Eval("ListingStatus") %>' />
                    <br />
                    <asp:Button ID="Button0" runat="server" Text="Edit" CommandName="Edit"/>
<br />
                </ItemTemplate>
            </asp:DataList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Listing]"></asp:SqlDataSource>
</asp:Content>
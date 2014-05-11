<%@ Page Title="Shopping List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VeriParkStaj._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>List of Products</h1>
                <h2>
                    <asp:Label ID="categoryHeader" runat="server" />
                </h2>
            </hgroup>
        </div>
    </section>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <style>
        tr:hover {
            color: blue;
        }
    </style>

    <asp:DropDownList ID="categoryList" runat="server" Width="196px" Height="16px" Style="margin-top: 0px; padding-top: 0em; margin-left: 18px;" />
    <asp:Button ID="searchButton" runat="server" Style="margin-left: 6px; padding: 0em;" Text="Search" BorderWidth="0px" Font-Size="8pt" Height="19px" OnClick="searchButton_Click" />
    <!--Products are written via Repater -->
    <table>
        <asp:Repeater ID="products" runat="server">
            <ItemTemplate>
                <tr onclick="location.href= 'Product.aspx?ID='+<%# DataBinder.Eval(Container.DataItem, "productID") %>">
                    <td><%# DataBinder.Eval(Container.DataItem, "productName") %></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "productPrice")%> TL</td>
                    <td><%#DataBinder.Eval(Container.DataItem, "categoryName")%></td>
                </tr>
            </ItemTemplate>
            <SeparatorTemplate>
                <tr>
                    <td colspan="6">
                        <hr />
                    </td>
                </tr>
            </SeparatorTemplate>
        </asp:Repeater>
    </table>
    <br />
    <asp:Label runat="server" ID="exceptions" />
</asp:Content>

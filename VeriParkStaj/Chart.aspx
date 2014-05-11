<%@ Page Title="Chart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="VeriParkStaj.Contact" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>Chart</h1>
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
    <p>Please click on the item to remove it</p>
    <table>
        <asp:Repeater ID="chart" runat="server">
            <ItemTemplate>
                <tr onclick="location.href= 'Chart.aspx?name=<%# DataBinder.Eval(Container.DataItem, "name") %>'">
                    <td><%# DataBinder.Eval(Container.DataItem, "name") %></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "price")%> TL</td>
                    <td><%#DataBinder.Eval(Container.DataItem, "count")%></td>
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
    <asp:Button runat="server" ID="apply" Text="Buy" OnClick="apply_Click" />
    <br />
    <asp:Label runat="server" ID="exceptions" />
</asp:Content>

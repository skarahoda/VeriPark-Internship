<%@ Page Title="Product" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="VeriParkStaj.Product" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>Product</h1>
                <h2>
                    <asp:Label ID="pName" runat="server" />
                </h2>
            </hgroup>
        </div>
    </section>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Label runat="server" ID="category" />
    <br />
    <span style="padding-right: 2em">
        <asp:Label runat="server" ID="price" />
        &nbsp;TL
    </span>
    <p style="margin-top: 0px; margin-bottom: 0px">You have
        <asp:Label runat="server" ID="chartLabel" />&nbsp items in your chart</p>
    <asp:Label runat="server" ID="Sold" /><span>&nbsp items are sold.</span>
    <br />
    <asp:TextBox runat="server" ID="number" />
    <asp:Button Text="Add to Chart" runat="server" ID="add" OnClick="add_Click" />
    <asp:Button Text="Remove from Chart" runat="server" ID="remove" OnClick="remove_Click" />
    <asp:RequiredFieldValidator
        ControlToValidate="number"
        Text="<br />You have to enter a number"
        runat="server" />
    <asp:RangeValidator
        ControlToValidate="number"
        MinimumValue="1"
        MaximumValue="9999"
        Type="Integer"
        EnableClientScript="false"
        Text="<br />The number must be between 1 and 9999"
        runat="server" />
    <br />
    <asp:Label runat="server" ID="details" />
    <br />
    <asp:Label runat="server" ID="exceptions" />
</asp:Content>

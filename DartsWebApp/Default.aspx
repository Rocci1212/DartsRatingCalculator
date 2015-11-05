<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>Search.</h1>
                <h2>Find a player, team, league, or sponsor below.</h2>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Enter a term here:</h3>
    <ol class="round">
        <asp:TextBox runat="server" ID="SearchTerm" />
        <asp:Button runat="server" ID="GoButton" Width="100" Text="Go!" OnClick="GoButton_Click" />
    </ol>
    <div class="table-responsive">
        <asp:GridView ID="grdPlayerSearch" runat="server" AutoGenerateColumns="False" AllowSorting="False" 
            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4">
            <Columns>
                <asp:HyperLinkField  HeaderText="Players" DataTextField="Name" DataNavigateUrlFormatString="{0}" DataNavigateUrlFields="Link"  />                        
            </Columns>       
        </asp:GridView>  
    </div>
    <div class="table-responsive">
        <asp:GridView ID="grdTeamSearch" runat="server" AutoGenerateColumns="False" AllowSorting="False" 
            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4">
            <Columns>
                <asp:HyperLinkField  HeaderText="Teams" DataTextField="Name" DataNavigateUrlFormatString="{0}" DataNavigateUrlFields="Link"  />                        
            </Columns>       
        </asp:GridView>  
    </div>
</asp:Content>

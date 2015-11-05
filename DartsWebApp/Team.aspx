<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Team.aspx.cs" Inherits="WebApplication1.Team" ValidateRequest="false" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
                <h1>
                    <asp:HyperLink ID="lblTeam" runat="server" Text="test"></asp:HyperLink>
                </h1>
            <%--<p>
                To learn more about ASP.NET, visit <a href="http://asp.net" title="ASP.NET Website">http://asp.net</a>.
                The page features <mark>videos, tutorials, and samples</mark> to help you get the most from ASP.NET.
                If you have any questions about ASP.NET visit
                <a href="http://forums.asp.net/18.aspx" title="ASP.NET Forum">our forums</a>.
            </p>--%>
            
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="table-responsive">
        <asp:GridView ID="grdPlayer" runat="server" AutoGenerateColumns="False" AllowSorting="False" 
            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4">
            <Columns>
                <asp:HyperLinkField  HeaderText="Players" DataTextField="Name" DataNavigateUrlFormatString="{0}" DataNavigateUrlFields="Link"  />  
                <asp:BoundField HeaderText="SquadName" DataField="SquadName" Visible="false" />                      
            </Columns>       
        </asp:GridView>  
    </div>
</asp:Content>

<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Player.aspx.cs" Inherits="WebApplication1.Player" ValidateRequest="false" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
                <h1>
                    <asp:HyperLink ID="lblPlayer" runat="server" Text="test"></asp:HyperLink>
                </h1>
                <h2><asp:Label ID="lblRating" Text="test" runat="server"></asp:Label></h2>
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
        <asp:GridView ID="grdPlayer" runat="server" AutoGenerateColumns="False" GridLines="Vertical" AllowSorting="True" CssClass="table table-striped" PagerStyle-CssClass="pgr"  
            AlternatingRowStyle-CssClass="alt" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black">
            <AlternatingRowStyle CssClass="alt" BackColor="White"></AlternatingRowStyle>
            <Columns>
                <asp:HyperLinkField  HeaderText="Match" DataTextField="Match" DataNavigateUrlFormatString="http://stats.mmdl.org/index.php?view=match&matchid={0}" DataNavigateUrlFields="Match"  />                        
                <asp:BoundField HeaderText="Game #" DataField="GameNumber" />
                <asp:HyperLinkField HeaderText="Opposing Team" DataTextField="SquadName" DataNavigateUrlFormatString="http://stats.mmdl.org/index.php?view=team&teamid={0}" DataNavigateUrlFields="Squad" />
                <asp:BoundField HeaderText="Game Type" DataField="GameType" /> 
                <asp:BoundField HeaderText="Result" DataField="Result" />
                <asp:BoundField HeaderText="Initial Rating" DataField="PreRating" DataFormatString="{0:f0}"/>
                <asp:BoundField HeaderText="Ending Rating" DataField="PostRating" DataFormatString="{0:f0}"/>
                <asp:HyperLinkField HeaderText="Teammate" DataTextField="T1Name" DataNavigateUrlFormatString="Player.aspx?q={0}_all"  DataNavigateUrlFields="T1Player" />
                <asp:HyperLinkField HeaderText="Opponent 1" DataTextField="O1Name" DataNavigateUrlFormatString="Player.aspx?q={0}_all"  DataNavigateUrlFields="O1Player" />
                <asp:HyperLinkField HeaderText="Opponent 2" DataTextField="O2Name" DataNavigateUrlFormatString="Player.aspx?q={0}_all"  DataNavigateUrlFields="O2Player" />
            </Columns>       
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />

<PagerStyle CssClass="pgr" BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>  
    </div>
</asp:Content>

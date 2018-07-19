<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menusuperior.ascx.cs" Inherits="portalconteza.menusuperior" %>
    <%--CSS--%>
    <link  type="text/css" rel="stylesheet" href="css/principal.css" />

<div >

    <asp:Label CssClass="cssMargem001" ID="Label1" runat="server" Text="Selecione sua Cidade: "></asp:Label>   <asp:DropDownList ID="DropDownList1" runat="server"  Width="150px"></asp:DropDownList>

    <asp:LinkButton CssClass="cssMargem001" ID="LinkButton1" runat="server">Solicite uma Ligação</asp:LinkButton>

    <asp:LinkButton CssClass="cssMargem001" ID="LinkButton2" runat="server">Login</asp:LinkButton>

    <asp:Button CssClass="cssMargem001"  ID="Button1" runat="server" Text="Cadastre-se" />

</div>

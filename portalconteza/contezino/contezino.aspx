<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contezino.aspx.cs" Inherits="portalconteza.contezino.contezino" %>



<%@ Register Src="~/menusuperior.ascx" TagPrefix="uc1" TagName="menusuperior" %>
<%@ Register Src="~/menuprincipal.ascx" TagPrefix="uc1" TagName="menuprincipal" %>
<%@ Register Src="~/mapasiteprincipal.ascx" TagPrefix="uc1" TagName="mapasiteprincipal" %>
<%@ Register Src="~/midiassociais.ascx" TagPrefix="uc1" TagName="midiassociais" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Clube Conteza</title>

    <%--CSS--%>
    <link  type="text/css" rel="stylesheet" href="../css/principal.css" />


</head>
<body>
    <form id="form1" runat="server">
    <div id="DivExterna" >
           <div id="Divinterna"  class="card">
               <div id="DivCabecalhos">
                   <div id="DivMenuSuperior"  class="cssMenuPrincipal" >
                       <uc1:menusuperior runat="server" ID="menusuperior" />
                   </div>
                   <div id="DivBusca" style="text-align:center">
                       

                       <asp:TextBox ID="txtBusca" runat="server"></asp:TextBox>
                       <asp:Button  ID="btnBuscar" runat="server" Text="Buscar" />

                   </div>
                   <div id="DivMenuPrincipal">
                       <uc1:menuprincipal runat="server" id="menuprincipal" />
                   </div>

               </div>
               
               <div id="DivConteudo">
                          
                           texto texto texto texto texto texto texto texto
                           <br />
                           texto texto texto texto texto texto texto texto
                               <br />
                           texto texto texto texto texto texto texto texto
                               <br />
                             texto texto texto texto texto texto texto texto
                               <br />
                             texto texto texto texto texto texto texto texto
                               <br />
                             texto texto texto texto texto texto texto texto
                               <br />
                             texto texto texto texto texto texto texto texto
                               <br />
                             texto texto texto texto texto texto texto texto
                               <br />
                             texto texto texto texto texto texto texto texto
                               <br />
                             texto texto texto texto texto texto texto texto
                   <br />
               </div>

               <div id="DivRodape">
                   <div id="DivMapaSite"> 
                       <uc1:mapasiteprincipal runat="server" id="mapasiteprincipal" />
                   </div>
                   <div id="DivMidiasSociais">
                       <uc1:midiassociais runat="server" id="midiassociais" />
                   </div>
               </div> 
           </div>
    </div>
    </form>
</body>
</html>

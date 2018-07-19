<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="portalconteza.login" %>

<%@ Register Src="~/menusuperior.ascx" TagPrefix="uc1" TagName="menusuperior" %>
<%@ Register Src="~/menuprincipal.ascx" TagPrefix="uc1" TagName="menuprincipal" %>
<%@ Register Src="~/mapasiteprincipal.ascx" TagPrefix="uc1" TagName="mapasiteprincipal" %>
<%@ Register Src="~/midiassociais.ascx" TagPrefix="uc1" TagName="midiassociais" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Clube Conteza | Login</title>
        <meta name="description" content="Tela de Login do Clube Conteza : acesse sua área administrativa aqui!" />
        <!-- texto Google -->
        <meta name="keywords" content="Clube, Conteza, Descontos, Promoções, Loguin, Guarapuava, Clube de Benefícios, Clube de Descontos, Saúde, Médico" />
        <!-- palavras chave -->

        <%--CSS--%>
        <link type="text/css" rel="stylesheet" href="css/principal.css" />

     

        <style type="text/css">
            .cssConteudo {
                background-image: url(../img/bg.jpg);
                background-color: #0d3084;
            }

            div#loginbox {
                max-width: 600px;
                width: 40%;
                background: white;
                min-height: 200px;
                margin: auto;
                margin-top: 0px;
                box-shadow: 3px 3px 10px rgba(0, 0, 0, 0.59);
                min-width: 320px;
            }

            div#LogoClube {
                width: 100%;
                height: 100px;
                position: relative;
            }

            #LogoClube img {
                max-width: 290px;
                float: right;
                padding-right: 20px;
                margin-top: 20px;
                margin-bottom: 20px;
            }

            div#engloba {
                width: 95%;
                position: absolute;
                bottom: 10px;
            }

            div#copyright {
                position: relative;
                margin: auto;
                width: 365px;
                color: white;
                font-family: open sans;
                font-size: 14px;
                font-weight: 500;
            }
        </style>


    </head>
    <body>
        <form id="form1" runat="server">
            <div id="DivExterna">
                <div id="Divinterna" class="card">
                    <div id="DivCabecalhos">

                        <div id="DivMenuPrincipal">
                        </div>

                    </div>

                    <div id="DivConteudo" class="cssConteudo">

                        <div id="loginbox">
                            <asp:Label ID="Label1" runat="server" Text="CPF"></asp:Label>
                            <asp:TextBox runat="server" ID="txtCPF">00862817986</asp:TextBox>
                           <br/>

                            <asp:Label ID="Label2" runat="server" Text="Senha"></asp:Label>
                            <input runat="server" id="txtSenha" type="password" value="00862817986" /><br />
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" />

                            <div runat="server" id="DivModulos" style="background-color:red">

                               


                           <%--  <li><a href="#">Home</a></li>
                               <br />
                                	
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server" />--%>
                            </div>

                        </div>
                        <!-- fim da login box-->
                        <div id="engloba">
                            <div id="copyright">Clube Conbteza 2017 - Todos os Direitos Reservados.</div>
                        </div>
                        <!-- copyright -->
                    </div>

                </div>
            </div>
        </form>
    </body>
</html>

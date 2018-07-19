<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TelaLogin.aspx.cs" Inherits="portalconteza.TelaLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title> Login Clube Conteza </title>

    <link href="css/principal.css" rel="stylesheet" />
    

</head>

<body>

<div id="innercontent">

    <div id="header">

        <div id="headertopo"> </div>

        <div id="headerclube"> 

            <div id="logoclube"><a href="http://www.clubeconteza.com.br"><img src="http://191.232.160.34/wordpress/wp-content/uploads/2017/05/logosite.png" /></a></div>

            <div id="barrabusca">
                <div id="searchbarnovo"><form id="formulario" action="http://clubeconteza.com.br/portal/resultados-da-busca/" method="get"><input type="hidden" name="cidade" id="cidade" value="3437"><input type="hidden" name="pagina" id="pagina" value="1"><div id="name-group" class="form-group"><label for="name"></label><input type="text" class="form-control" name="buscar" id="buscar"></div><button type="submit" class="btn btn-success" id="btnBuscar"><span class="fa fa-arrow-right">Buscar</span></button></form></div>


            </div>

        </div>

        <form id="form1" runat="server">

        <div id="menu">

            <ul class="menuitem">

                <li class="itens"> <a href="#">Home</a> </li>

                <li class="itens"> <a href="#">Guia </a></li>

                <li class="itens"> <a href="#">Descontos e Promoções </a></li>

                <li class="itens"> <a href="#">O Clube Conteza</a></li>

                <li class="itens"> <a href="#">Seja Um Parceiro </a></li>

                <li class="itens"> <a href="#">Faça Parte</a></li>

                <li class="itens"> <a href="#">Contato</a></li>

            </ul>

        </div>

        <div id="logincontent"> 

            <div id="inner">

                <div id="welcomebox"><p class="welcome">Seja <strong>Bem Vindo!</strong></p>

                    <img src="http://191.232.160.34/wordpress/wp-content/uploads/2017/05/logosite.png" class="welcomeimg" />

                </div>


<div id="loginbox">

<div id="insideloginbox">

<div id="insidecpf">

<asp:Label ID="Label1" runat="server" Text="Digite o seu CPF:"></asp:Label><br/>

<asp:TextBox runat="server" ID="txtCPF">94096538949</asp:TextBox>

</div>

<div id="insidesenha">
    
<asp:Label ID="Label2" runat="server" Text="Digite sua Senha:"></asp:Label><br/>

<input runat="server" id="txtSenha" type="password" value="00862817986" />

</div>

<div id="insidebotoes">

<asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click"  />

<asp:Button ID="btnEsqueciMinhaSenha" runat="server" Text="Esqueci Minha Senha" />

</div>

<div runat="server" id="DivModulos" style="background-color:red">

 <%--<li><a href="#">Home</a></li>
                                                         	
<asp:PlaceHolder ID="PlaceHolder1" runat="server" />--%>

</div> </div> </div>

                <div id="selecioneseuperfil">Selecione seu perfil para entrar:</div>

                <div id="perfis">

                    <div id="perfil1" class="perfis"> Contezino </div>

                    <a runat="server" id="linkPerfil2" href="#" style="text-decoration:none;color:inherit;cursor:default">
                        <div id="perfil2" class="perfis"> Corporativo </div>
                    </a>

                    <div id="perfil3" class="perfis"> Parceiro </div>

                </div>

            </div>

        </div>

        <div id="widgetsfooter">

            <div id="widsobre" class="wids"><p class="tituloswd">O Clube Conteza Mudou! </p><img src="http://clubeconteza.com.br/portal/wp-content/uploads/2017/07/fav.png" /><p class="wid">Além de continuar oferecendo facilidades na área de Saúde a preços mais acessíveis, o Conteza agora é um clube de benefícios que traz novas vantagens para todos os seus associados. São descontos em lojas, restaurantes e diversos estabelecimentos conveniados que trarão comodidade e economia para você e sua família. </p></div>

            <div id="wdmenu1" class="wids"><p class="tituloswd">Clube Conteza</p>

                <ul class="menuwidsitens">

                    <li><a href="#">Conheça o Clube</a></li>

                    <li><a href="#">Descontos e Promoções</a></li>

                    <li><a href="#">Seja um Parceiro</a></li>

                </ul>

            </div>

            <div id="wdmenu2" class="wids"><p class="tituloswd">Dúvidas?</p>

                <ul class="menuwidsitens">

                    <li><a href="#">Regulamento</a></li>

                    <li><a href="#">Termos de Uso</a></li>

                    <li><a href="#">Planos de Adesão</a></li>

                    <li><a href="#">FAQ</a></li>

                </ul>
                
            </div>


            <div id="wdmenu3" class="wids"><p class="tituloswd">Vantagens</p>

                <ul class="menuwidsitens">

                    <li><a href="#">Vantagens</a></li>

                    <li><a href="#">Comércio</a></li>

                    <li><a href="#">Profissional Liberal</a></li>

                    <li><a href="#">Planos Corporativos</a></li>

                </ul>

            </div>
        </div>

        <div id="footer"><h2 class="footer">Clube Conteza 2017</h2></div>

        </form>

</div>

</div>

</body>

</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Boleto.Testes.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Cancelamento Parcela</title>

     <style>
    body 
    {
        margin: 0px
    }
    .container
    {
        width: 100%;
        height: 100%;
        background: #EDEDED;
        display: flex;
        flex-direction: row;
        justify-content: center;
        align-items: center
    }
    .box {
        width: 760px;
        height: 300px;
        background: #FFFFFF;
    }
         .auto-style1 {
             text-align: left;
         }
         .auto-style2 {
             width: 760px;
             height: 300px;
             background: #FFFFFF;
             text-align: center;
         }
         .auto-style3 {
             width: 158px;
         }
         </style>
</head>
<body >
    

    <form id="form1" runat="server">
    

  <div class="container"> 
        <div class="auto-style2">
            <img alt=''src='http://www.clubeconteza.com.br/img/sis/LogoRPT.png' /><br />
            <div style="background-color:#F3F3F3; text-align: center;">
                $Titular</div>
            <br />
            Segue detalhes dos boletos pendentes em nosso sistema<br />
            <br />
            <div>
 
                <table style="padding: 2px; margin: inherit; border: medium solid #000000; width:100%; table-layout: auto; border-spacing: inherit; border-collapse: collapse;">
                    <tr>
                        <td style="border-style: solid; background-color: #4472C4;" class="auto-style3">Parcela</td>
                        <td style="border-style: solid; background-color: #4472C4;">Vencimento</td>
                        <td style="border-style: solid; background-color: #4472C4;">Tipo</td>
                    </tr>
                    <tr>
                        <td style="border-style: solid; background-color: #D9E2F3;" class="auto-style3">
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="http://www.clubeconteza.com.br/boletos/1.pdf">A3</asp:HyperLink>
                        </td>
                        <td style="border-style: solid; background-color: #D9E2F3;">A2</td>
                        <td style="border-style: solid; background-color: #D9E2F3;; " class="auto-style1" >A3</td>
                    </tr>
                    <tr>
                        <td style="border-style: solid; background-color: #FFFFFF;" class="auto-style3">
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="http://www.clubeconteza.com.br/boletos/153854.htm">B1</asp:HyperLink>
                        </td>
                        <td style="border-style: solid; background-color: #FFFFFF;">B2</td>
                        <td style="border-style: solid; background-color: #FFFFFF;" class="auto-style1">B3</td>
                    </tr>
                </table>

            </div>
            <div>

                <br />
                Qualquer duvida, entre em contato com nossa central de atendimento no telefone: 42 3623 1441</div>
        </div>
  </div>
   
    </form>
   
</body>
</html>

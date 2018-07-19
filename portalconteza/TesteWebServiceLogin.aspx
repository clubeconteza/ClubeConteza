<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TesteWebServiceLogin.aspx.cs" Inherits="portalconteza.TesteWebServiceLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset>
          <legend>Consumir Serviço Login</legend>
          <table>
              <tr>
                  <td>
                      Token de Acesso
                  </td>
                  <td>
                      <asp:TextBox ID="txtAcesso" runat="server" Width="1224px">6Ib0xcCzRkmqTNBAAXqmLfm1Tt2EMEJPZlUdqek7yVuqcDf17jr5FJZJnCQhEar5</asp:TextBox>
                  </td>
              </tr>
              <tr>
                  <td>
                      CNPJ
                  </td>
                  <td>
                      <asp:TextBox ID="txtCNPJ" runat="server" Width="265px">18334105000142</asp:TextBox>
                  </td>
              </tr>
              <tr>
                  <td>
                      Senha
                  </td>
                  <td>
                      <asp:TextBox ID="txtSenha" runat="server" Width="268px">MettaCard73830877000183ClubeConteza</asp:TextBox>
                  </td>
              </tr>
              <tr>
                  <td>
                      <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                  </td>
              </tr>
          </table>
      </fieldset>
      <br/>
      <div>
          <asp:GridView ID="gvwServicoLogin" runat="server" AutoGenerateColumns="False" Width="100%">
              <Columns>
                  <asp:BoundField HeaderText="CPF/CNPJ do Usuário" DataField="cpfcnpj_usuario" />
                  <asp:BoundField HeaderText="Nome do Usuário" DataField="nome_usuario" />
                  <asp:BoundField HeaderText="CNPJ Plano" DataField="cnpj_plano" />
              </Columns>
          </asp:GridView>
      </div>
    </form>
</body>
</html>

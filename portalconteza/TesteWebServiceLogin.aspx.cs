using Portal.Negocios;
using System;
using System.Data;
using System.Web.UI;

namespace portalconteza
{
    public partial class TesteWebServiceLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtAcesso.Text = Session["Acesso"].ToString();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            gvwServicoLogin.DataSource = new DataTable();
            gvwServicoLogin.DataBind();

            var acesso = string.IsNullOrEmpty(txtAcesso.Text.Trim()) ? Session["Acesso"] : txtAcesso.Text.Trim();
            if (acesso == null)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Não foi possível recuprerar o acesso do usuário.')", true);
            }

            try
            {
                var login = new LoginNegocios();
                var acessoUsuario = login.AcessoUsuario(acesso.ToString(), txtCNPJ.Text.Trim(), txtSenha.Text.Trim());

                gvwServicoLogin.DataSource = MontarTabela(acessoUsuario.CpfCnpjUsuario(), acessoUsuario.NomeUsuario(), acessoUsuario.CnpjPlanos());
                gvwServicoLogin.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('" + ex.Message + "')", true);
            }
        }

        private DataTable MontarTabela(string cpfCnpjUsuario, string nomeUsuario, string cnpjPlanos)
        {
            var dteLogin = new DataTable();
            dteLogin.Columns.Add("cpfcnpj_usuario");
            dteLogin.Columns.Add("nome_usuario");
            dteLogin.Columns.Add("cnpj_plano");
            dteLogin.Rows.Add(cpfCnpjUsuario, nomeUsuario, cnpjPlanos);
            return dteLogin;
        }

    }
}
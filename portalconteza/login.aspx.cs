using Controller;
using Portal.Negocios;
using System;
using System.Web.UI;


namespace portalconteza
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            DivModulos.InnerHtml = "";
            Session["Usuario"] = "0";
            Session["Acesso"] = "0";

            Session.Remove("Usuario");
            Session.Remove("Acesso");


            /*Trata campos obrigatorios do formulario*/
            if (txtCPF.Text.Trim() == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('" + string.Format(msretorno.MS0001, "CPF") + "')", true);
                return;
            }

            if (txtSenha.Value.Trim() == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('" + string.Format(msretorno.MS0001, "Senha") + "')", true);
                return;
            }
            /*Consulta credenciais do usuario no banco*/
            try
            {
                UsuarioPortalNegocios Usuario_N = new UsuarioPortalNegocios();
                UsuarioPortalController Usuario = Usuario_N.LoginUsuarioPortal(txtCPF.Text.Trim(), txtSenha.Value.Trim());
                if (Usuario.TB033_ChaveTemporaria == "-1")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('" + msretorno.MS0002 + "')", true);
                }
                else
                {
                    if (Usuario.TB033_ChaveTemporaria == "0")
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('" + string.Format(msretorno.MS0003, txtCPF.Text.Trim()) + "')", true);
                    }
                    else
                    {
                        Session["Usuario"] = Usuario.Pessoa.TB013_NomeCompleto;
                        Session["Acesso"] = Usuario.TB033_ChaveTemporaria;

                        Session.Timeout = 60;

                        /*Verifica acesso a plano familiar*/
                        PortalContratoNegocios Contrato_N = new PortalContratoNegocios();
                        ContratosController Contezino = Contrato_N.AcessoUsuarioPlanoFamiliar(Session["Acesso"].ToString());
                        if (Contezino.TB012_Id > 0)
                        {
                            /*Libera Icone de acesso plano familiar*/
                            DivModulos.InnerHtml = " <a href='/contezino/contezino.aspx'><img src='img/Contezino.png' /></a>";
                        }
                        else
                        {
                            DivModulos.InnerHtml = " <a href='/contezino/contezino.aspx'><img src='img/ContezinoPB.png' /></a>";
                        }



                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('" + ex.Message + "')", true);
            }
        }
    }
}
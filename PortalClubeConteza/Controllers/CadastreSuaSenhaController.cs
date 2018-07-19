using Newtonsoft.Json.Linq;
using PortalClubeConteza.DAO;
using PortalClubeConteza.Models;
using PortalClubeConteza.Utilities;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace PortalClubeConteza.Controllers
{
    public class CadastreSuaSenhaController : Controller
    {
        private ContatoDAO contatoDAO;
        private PessoaDAO pessoaDAO;
        private PortalUsuarioDAO portalUsuarioDAO;

        public CadastreSuaSenhaController(ContatoDAO contatoDAO, PessoaDAO pessoaDAO, PortalUsuarioDAO portalUsuarioDAO)
        {
            this.contatoDAO = contatoDAO;
            this.pessoaDAO = pessoaDAO;
            this.portalUsuarioDAO = portalUsuarioDAO;
        }

        public ActionResult Index()
        {
            if (TempData["Municipio"] == null)
            {
                var enderecoDAO = new EnderecoDAO();
                TempData["Municipio"] = enderecoDAO.CidadesAtivas();
            }

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult GeraSenha(CadastraSenha cadastraSenha)
        {
            if (ModelState.IsValid)
            {
                var response = Request["g-recaptcha-response"];
                var secretKey = "6LfsAD4UAAAAAGW_7DCXe5bmCeqQ5a3Rq3iyR7Vv";
                var client = new WebClient();
                var recaptcha = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
                var objetoRecaptcha = JObject.Parse(recaptcha);
                var sucesso = (bool)objetoRecaptcha.SelectToken("success");

                if (!sucesso)
                {
                    ModelState.AddModelError("UsuarioInvalido", "Marque a opção \"Não sou um robô.\"");
                    return View("Index");
                }

                var pessoa = pessoaDAO.BuscaPessoaAtivaPorCpfCnpj(cadastraSenha.Cpf) ?? pessoaDAO.BuscaPessoaComAcessosMultiplosPorCpfCnpj(cadastraSenha.Cpf);
                if (pessoa == null)
                {
                    ModelState.AddModelError("UsuarioInvalido", "O CPF informado não possui conta ativa no sistema. Entre em contato com o Clube...");
                    return View("Index");
                }

                var contato = contatoDAO.BuscaContatoEmailPorPessoa(pessoa.Id);
                if (string.IsNullOrEmpty(contato.Contatos))
                {
                    ModelState.AddModelError("UsuarioInvalido", "O usuário não possui e-mail cadastrado. Favor, ligar para o Clube e cadastrar um e-mail válido.");
                    return View("Index");
                }

                var novaSenha = GeradorSenhaAleatoria.GerarSenha();
                var usuario = portalUsuarioDAO.BuscaUsuarioPorIdPessoa(pessoa.Id);
                if (usuario.IdPessoa <= 0)
                {
                    var incluir = portalUsuarioDAO.IncluirUsuario(pessoa, novaSenha);
                    if (!incluir)
                    {
                        ModelState.AddModelError("UsuarioInvalido", "Ocorreu um erro ao incluir o usuário. Tente novamente...");
                        return View("Index");
                    }
                }
                else
                {
                    var alterar = portalUsuarioDAO.AlterarSenha(usuario.IdPessoa, novaSenha);
                    if (!alterar)
                    {
                        ModelState.AddModelError("UsuarioInvalido", "Ocorreu um erro ao alterar a senha. Tente novamente...");
                        return View("Index");
                    }
                }

                var html = new StringBuilder();
                html.Append("<!DOCTYPE html>");
                html.Append("<html>");
                html.Append("<head>");
                html.Append("  <meta charset='utf-8' />");
                html.Append("</head>");
                html.Append("<body>");
                html.Append("  <p>");
                html.Append("    Nova Senha: " + novaSenha);
                html.Append("  </p>");
                html.Append("</body>");
                html.Append("</html>");

                var mail = new MailMessage();
                mail.From = new MailAddress("gerenciadorclubeconteza@clubeconteza.com.br", "Clube Conteza (Gerenciador)");
                mail.To.Add(new MailAddress(contato.Contatos));
                mail.Subject = "Nova Senha (Clube Conteza)";
                mail.IsBodyHtml = true;
                mail.Body = html.ToString();

                var smtp = new SmtpClient();
                smtp.Host = "email-ssl.com.br";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("gerenciadorclubeconteza@clubeconteza.com.br", "2016@ral");

                try
                {
                    smtp.Send(mail);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("UsuarioInvalido", "Falha ao enviar o e-mail. Tente novamente...");
                    return View("Index");
                }
                finally
                {
                    smtp.Dispose();
                }

                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View("Index");
            }
        }
    }
}
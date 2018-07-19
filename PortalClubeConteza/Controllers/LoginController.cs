using PortalClubeConteza.DAO;
using PortalClubeConteza.Models;
using System.Web.Mvc;

namespace PortalClubeConteza.Controllers
{
    public class LoginController : Controller
    {
        private PessoaDAO pessoaDAO;
        private PortalUsuarioDAO portalUsuarioDAO;

        public LoginController(PessoaDAO pessoaDAO, PortalUsuarioDAO portalUsuarioDAO)
        {
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
        public ActionResult Autentica(Login login)
        {
            Session["Usuario"] = null;
            Session["Acesso"] = null;
            Session["Cidade"] = null;

            if (ModelState.IsValid)
            {
                var usuario = portalUsuarioDAO.BuscaUsuarioPorLogin(login.Cpf, login.Senha);

                if (usuario.IdPessoa <= 0)
                {
                    ModelState.AddModelError("UsuarioInvalido", "Consulta uma cadeia de caracteres localizada semelhante a Usuário não encontrado, verifique sua digitação..");
                    return View("Index");
                }

                if (usuario.Status != 1)
                {
                    ModelState.AddModelError("UsuarioInvalido", string.Format("Consulta uma cadeia de caracteres localizada semelhante a Usuário com o CPF {0} foi desativado..", login.Cpf.Trim()));
                    return View("Index");
                }

                var pessoa = pessoaDAO.BuscaPessoaPorId(usuario.IdPessoa);

                var altera = portalUsuarioDAO.AlterarChaveTemporaria(pessoa.Id, pessoa.CpfCnpj);
                if (!altera)
                {
                    ModelState.AddModelError("UsuarioInvalido", "Ocorreu um erro ao alterar a chave de acesso. Tente novamente...");
                    return View("Index");
                }

                usuario = portalUsuarioDAO.BuscaUsuarioPorIdPessoa(pessoa.Id);

                Session["Usuario"] = pessoa.NomeCompleto.Trim();
                Session["Acesso"] = usuario.ChaveTemporaria;
                Session["Cidade"] = pessoa.IdMunicipio;
                Session.Timeout = 60;

                return RedirectToAction("Index", "Contezino");
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("Usuario");
            Session.Remove("Acesso");
            Session.Remove("Cidade");
            return RedirectToAction("Index");
        }
    }
}
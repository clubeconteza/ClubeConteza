using PortalClubeConteza.DAO;
using PortalClubeConteza.Filters;
using PortalClubeConteza.Models;
using System.Web.Mvc;

namespace PortalClubeConteza.Controllers
{
    [AutorizacaoFilter]
    public class ContezinoController : Controller
    {
        private PortalUsuarioDAO portalUsuarioDAO;

        public ContezinoController(PortalUsuarioDAO portalUsuarioDAO)
        {
            this.portalUsuarioDAO = portalUsuarioDAO;
        }

        public ActionResult Index()
        {
            if (TempData["Municipio"] == null)
            {
                var enderecoDAO = new EnderecoDAO();
                TempData["Municipio"] = enderecoDAO.CidadesAtivas();
            }

            var chaveUsuario = Session["Acesso"];

            var loginDao = new LoginDAO();
            var contezino = loginDao.AcessoUsuarioPlanoFamiliar(chaveUsuario.ToString());

            ViewBag.LinkVouchersContezino = string.Empty;
            ViewBag.LinkVouchersParceiro = string.Empty;

            if (chaveUsuario != null)
            {
                //if (contezino.Id > 0)
                //{
                    ViewBag.LinkVouchersContezino = "https://vouchers.sodaweb.com.br/meus-vouchers?userKey=" + chaveUsuario.ToString();
                //}
                //else
                //{
                    ViewBag.LinkVouchersParceiro = "https://vouchers.sodaweb.com.br/empresa?userKey=" + chaveUsuario.ToString();
                //}
            }

            return View();
        }

        public ActionResult AlteraSenha()
        {
            if (TempData["Municipio"] == null)
            {
                var enderecoDAO = new EnderecoDAO();
                TempData["Municipio"] = enderecoDAO.CidadesAtivas();
            }

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AtualizaSenha(AlteraSenha alteraSenha)
        {
            if (ModelState.IsValid)
            {
                var chaveUsuario = Session["Acesso"];

                var criptografiaDAO = new CriptografiaDAO();
                var senhaAtual = criptografiaDAO.Encrypt(alteraSenha.SenhaAtual.TrimEnd());

                var usuario = portalUsuarioDAO.BuscaUsuarioPorChaveCpfCnpj(chaveUsuario.ToString());

                if (senhaAtual != usuario.Senha.Trim())
                {
                    ModelState.AddModelError("ErroAlteraSenha", "A senha atual digitada não bate com a verdadeira.");
                    return View("AlteraSenha");
                }

                if (alteraSenha.SenhaNova.TrimEnd() != alteraSenha.ConfirmaSenha.TrimEnd())
                {
                    ModelState.AddModelError("ErroAlteraSenha", "As senhas não correspondem. A senha nova digitada não bate com a senha de confirmação.");
                    return View("AlteraSenha");
                }

                var atualiza = portalUsuarioDAO.AlterarSenha(usuario.IdPessoa, alteraSenha.SenhaNova);
                if (atualiza)
                {
                    TempData["SucessoAlteraSenha"] = "Senha atualizada com sucesso.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("ErroAlteraSenha", "Houve um problema ao trocar a sua senha. Tente novamente...");
                    return View("AlteraSenha");
                }
            }
            else
            {
                return View("AlteraSenha");
            }
        }
    }
}
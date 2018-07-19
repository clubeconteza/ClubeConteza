using PortalClubeConteza.DAO;
using PortalClubeConteza.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace PortalClubeConteza.Controllers
{
    public class ContatoController : Controller
    {
        // GET: Contato
        public ActionResult Index()
        {
            if (TempData["Municipio"] == null)
            {
                var enderecoDAO = new EnderecoDAO();
                TempData["Municipio"] = enderecoDAO.CidadesAtivas();
            }

            return View();
        }

        public ActionResult Enviar(ContatoFormulario contato)
        {
            if (ModelState.IsValid)
            {
                var html = new StringBuilder();
                html.Append("<!DOCTYPE html>");
                html.Append("<html>");
                html.Append("<head>");
                html.Append("  <meta charset='utf-8' />");
                html.Append("</head>");
                html.Append("<body>");
                html.Append("  <table border='1' style='border-color:#666;border-collapse:collapse' cellpadding='10'>");
                html.Append("    <tbody>");
                html.Append("      <tr style='background-color:#393e40;color:white;font-size:14px'>");
                html.Append("        <td colspan='2'>Contato</td>");
                html.Append("      </tr>");
                html.Append("      <tr>");
                html.Append("        <td><strong>Nome:</strong></td>");
                html.Append("        <td>" + contato.Nome + "</td>");
                html.Append("      </tr>");
                html.Append("      <tr>");
                html.Append("        <td><strong>Telefone:</strong></td>");
                html.Append("        <td>" + contato.Telefone + "</td>");
                html.Append("      </tr>");
                html.Append("      <tr>");
                html.Append("        <td><strong>Email:</strong></td>");
                html.Append("        <td><a href='mailto:" + contato.Email + "' target='_blank'>" + contato.Email + "</a></td>");
                html.Append("      </tr>");
                html.Append("      <tr>");
                html.Append("        <td><strong>Plano escolhido:</strong></td>");
                html.Append("        <td>" + contato.Plano + "</td>");
                html.Append("      </tr>");
                html.Append("    </tbody>");
                html.Append("  </table>");
                html.Append("</body>");
                html.Append("</html>");

                var mail = new MailMessage();
                mail.From = new MailAddress("gerenciadorclubeconteza@clubeconteza.com.br", "Clube Conteza (Gerenciador)");
                mail.To.Add(new MailAddress("gerenciageral@clubeconteza.com.br"));
                mail.Subject = "Contato";
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
                    ModelState.AddModelError("FalhaEnvioEmail", "Falha ao enviar o e-mail. Tente novamente...");
                    return View("Index");
                }
                finally
                {
                    smtp.Dispose();
                }

                return RedirectToAction("SucessoEnvio", "Home");
            }
            else
            {
                return View("Index");
            }
        }
    }
}
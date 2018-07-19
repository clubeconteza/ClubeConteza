using Boleto.Controller.ServicesClient;
using Boleto.DAO;
using Boleto.Negocios;
using Boleto.Negocios.ServicesClient.Sms;
using Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Services;

namespace Boleto
{
    /// <summary>
    /// Descrição resumida de Portal
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class Portal : System.Web.Services.WebService
    {

        [WebMethod(Description = "Lista Banners liberados para a Sessão")]
        public List<BannerController> BannerPorSessao(int Sessao, int StatusContrato, int StatusBanner, Int64 Cidade)
        {
            BannerDAO DAO = new BannerDAO();
            List<BannerController> retorno_L = DAO.SP_S_TB019_BannerPortal(Sessao, StatusContrato, StatusBanner, Cidade);
            return retorno_L;
        }

        [WebMethod(Description = "Lista Cidades Ativas")]
        public List<MunicipioController> CidadesAtivas()
        {
            EnderecoDAO DAO = new EnderecoDAO();
            List<MunicipioController> retorno_L = DAO.CidadesAtivas();
            return retorno_L;
        }

        [WebMethod(Description = "Lista parceiros por Sessao e Cidade (Somente parceiros Ativos)")]
        public List<UnidadeController> ParceirosSessaoCidade(Int64 Sessao, Int64 Cidade, int Registros, int Pagina, string Nivel1, string Nivel2, string Nivel3)
        {
            ParceiroDAO DAO = new ParceiroDAO();
            List<UnidadeController> Parceiros = DAO.SP_S_Parceiro_Sessao_Cidade(Sessao, Cidade, Registros, Pagina, Nivel1, Nivel2, Nivel3);
            return Parceiros;
        }

        [WebMethod(Description = "Detalhes do parceiro (Pessoa Fisica)")]
        public UnidadeController DetalhesParceiroPF(long Parceiro)
        {
            ParceiroDAO DAO = new ParceiroDAO();

            return DAO.RetornaDetalheParceiroPessoaFisica(Parceiro);
        }


        [WebMethod(Description = "Detalhes do parceiro (Pessoa Juridica)")]
        public UnidadeController DetalhesParceiroPJ(long Parceiro)
        {
            ParceiroDAO DAO = new ParceiroDAO();

            return DAO.RetornaDetalheParceiroPessoaJuridica(Parceiro);
        }

        [WebMethod(Description = "Lista as categorias ligadas a sessão informada")]
        public List<CategoriaController> CategoriasNivel1(long Sessao)
        {
            CategoriaDAO DAO = new CategoriaDAO();
            List<CategoriaController> Nivel1 = DAO.RetornoCategoriaNivel1(Sessao);
            return Nivel1;
        }

        [WebMethod(Description = "Lista as RetornoCategoriaNivel2 do nivel 2 ligadas ao nivel 1 [Separados por ';']")]
        public List<CategoriaController> CategoriasNivel2(string Nivel1)
        {
            CategoriaDAO DAO = new CategoriaDAO();
            List<CategoriaController> Nivel2 = DAO.RetornoCategoriaNivel2(Nivel1);
            return Nivel2;
        }

        [WebMethod(Description = "Lista as RetornoCategoriaNivel2 do nivel 3 ligadas ao nivel 2 [Separados por ';']")]
        public List<CategoriaController> CategoriasNivel3(string Nivel2)
        {
            CategoriaDAO DAO = new CategoriaDAO();
            List<CategoriaController> Nivel3 = DAO.RetornoCategoriaNivel3(Nivel2);
            return Nivel3;
        }


        [WebMethod(Description = "Enviar cartões gerados para impressão (Plano Familiar)")]
        public Int16 CartaoParaImpressaoFamiliar()
        {
            Int16 CartaoLote = 100;

            try
            {
                PessoaDAO DAO = new PessoaDAO();
                LogNegocios Log_N = new LogNegocios();

                //Definir novo lote
                CartaoLote = DAO.RecuperarCartaoLote();
                CartaoLote++;

                //Recupera Cartões gerados;
                List<PessoaController> Cartoes = DAO.CartoesParaImpressao(CartaoLote, 1);

                /*Enviar Email*/
                if (Cartoes.Count > 0)
                {
                    string str_from_address = ParametrosNegocios.EmailSaidaLogin;//sender
                    string str_name = ParametrosNegocios.NomeConta;
                    //The To address (Email ID)
                    string str_to_address = "impressaocartoes@clubeconteza.com.br";//recipient
                    //string str_to_address = "ti@clubeconteza.com.br";//recipient

                    System.Net.Mail.MailMessage email_msg = new MailMessage();

                    //Specifying From,Sender & Reply to address
                    email_msg.From = new MailAddress(str_from_address, str_name);
                    email_msg.Sender = new MailAddress(str_from_address, str_name);
                    //email_msg.ReplyTo = new MailAddress(str_from_address, str_name);

                    //The To Email id
                    email_msg.To.Add(str_to_address);

                    email_msg.Subject = "Cartões para impressão (Plano Familiar)";//Subject of email
                    StringBuilder HTML = new StringBuilder();

                    HTML.Append("<!DOCTYPE html>");


                    HTML.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
                    HTML.Append("<head runat='server'>");
                    HTML.Append("<meta http-equiv='Content -Type' content='text/html; charset=utf-8'/>");
                    HTML.Append("<title>Cartões para Impressão Plano Familiar</title>");
                    HTML.Append("<style>");
                    HTML.Append("body");
                    HTML.Append("{");
                    HTML.Append("margin: 0px");
                    HTML.Append("}");
                    HTML.Append(".container");
                    HTML.Append("{");
                    HTML.Append("width: 100 %;");
                    HTML.Append("height: 100 %;");
                    HTML.Append("background: #EDEDED;");
                    HTML.Append("display:flex;");
                    HTML.Append("flex-direction:row;");
                    HTML.Append("justify-content:center;");
                    HTML.Append("align-items:center");
                    HTML.Append("}");
                    HTML.Append(".box {");
                    HTML.Append("width:760px;");
                    HTML.Append("height:300px;");
                    HTML.Append("background:#FFFFFF;");
                    HTML.Append("}");
                    HTML.Append(".auto-style1 {");
                    HTML.Append("text-align:left;");
                    HTML.Append("}");
                    HTML.Append("</style>");
                    HTML.Append("</head>");
                    HTML.Append("<body>");
                    HTML.Append("<div class='container'>");
                    HTML.Append("<div class='box'>");
                    HTML.Append("<div style='background-color:#0071C5; font-size: 26px; font-weight: bold; color: #FFFFFF;'>");
                    HTML.Append("Clube Conteza");
                    HTML.Append("</div>");
                    HTML.Append("<br/>");
                    HTML.Append("<div style='background-color:#F3F3F3; text-align: center;' >");
                    HTML.Append("Cartões para Impressão</div>");
                    HTML.Append("<br/>");
                    HTML.Append("<div> ");
                    HTML.Append("Segue lista para impressão de cartões do Plano Familiar</div>");
                    HTML.Append("<br/>");
                    HTML.Append("<div>");
                    HTML.Append("<table style='padding: 2px; margin: inherit; border: medium solid #000000; width:100%; table-layout: auto; border-spacing: inherit; border-collapse: collapse;'>");
                    HTML.Append("<tr>");
                    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Contrato</td>");
                    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>ID Pessoa</td>");
                    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Cartão</td>");
                    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Lote</td>");
                    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Nome Completo</td>");
                    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Celular</td>");
                    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Ponto de Venda</td>");
                    HTML.Append("</tr>");
                    int Linha = 0;

                    foreach (PessoaController Cartao in Cartoes)
                    {
                        HTML.Append("<tr>");
                        if (Linha == 0)
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }
                        HTML.Append(Cartao.TB012_Id);
                        HTML.Append("</td>");

                        if (Linha == 0)
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }
                        HTML.Append(Cartao.TB013_id);
                        HTML.Append("</td>");

                        if (Linha == 0)
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }
                        HTML.Append(Cartao.TB013_Cartao);
                        HTML.Append("</td>");

                        if (Linha == 0)
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }

                        /**/
                        HTML.Append(Cartao.TB013_CartaoLote);
                        HTML.Append("</td>");

                        if (Linha == 0)
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }

                        /**/
                        HTML.Append(Cartao.TB013_NomeCompleto);
                        //HTML.Append("</td>");
                        //HTML.Append("</tr>");
                        /**/
                        if (Linha == 0)
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }
                        HTML.Append(Cartao.Contato.TB009_Contato);
                        HTML.Append("</td>");
                        /*Ponto de Venda*/
                        if (Linha == 0)
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }
                        HTML.Append(Cartao.PontoDeVenda.TB002_Ponto.TrimEnd());
                        HTML.Append("</td>");

                        /*Fim ponto de venda*/

                        HTML.Append("</tr>");

                        /*Rotina para o Log*/
                        LogController Log_C = new LogController();

                        Log_C.TB012_Id = Cartao.TB012_Id;
                        Log_C.TB011_Id = 0;
                        Log_C.TB000_IdTabela = 13;
                        Log_C.TB000_Tabela = "Cartao";
                        Log_C.TB000_Data = DateTime.Now;
                        Log_C.TB000_Descricao = string.Format(MensagensLog.L0021.ToString(), Cartao.TB013_Cartao);
                        Log_N.LogInsert(Log_C);
                        /**/
                    }


                    HTML.Append("</table>");
                    HTML.Append("</div>");
                    HTML.Append("<div>");
                    HTML.Append("</div>");
                    HTML.Append("</div>");
                    HTML.Append("</div>");
                    HTML.Append("</body>");
                    HTML.Append("</html>");

                    email_msg.IsBodyHtml = true;
                    email_msg.Body = HTML.ToString();//body
                                                     //Create an object for SmtpClient class
                    SmtpClient mail_client = new SmtpClient();

                    //Providing Credentials (Username & password)
                    NetworkCredential network_cdr = new NetworkCredential();
                    network_cdr.UserName = str_from_address;
                    network_cdr.Password = ParametrosNegocios.EmailSaidaSenha;

                    mail_client.Credentials = network_cdr;

                    //Specify the SMTP Port
                    mail_client.Port = ParametrosNegocios.EmailSaidaPorta;

                    //Specify the name/IP address of Host
                    mail_client.Host = ParametrosNegocios.EmailSaidaSMTP;

                    //Uses Secure Sockets Layer(SSL) to encrypt the connection
                    mail_client.EnableSsl = true;

                    //Now Send the message
                    mail_client.Send(email_msg);
                    /*Apos Enviar Email Atualizar Status do Cartão*/
                    DAO.CartoesUpdateStatus(CartaoLote, 2);

                }
                /*Fim Email*/

                if (Cartoes.Count == 0)
                { CartaoLote = 0; }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return CartaoLote;
        }

        [WebMethod(Description = "Enviar cartões gerados para impressão (Plano Corporativo)")]
        public Int16 CartaoParaImpressaoCorporativo()
        {
            Int16 CartaoLote = 100;

            try
            {
                PessoaDAO DAO = new PessoaDAO();
                LogNegocios Log_N = new LogNegocios();

                //Definir Novo Lote
                CartaoLote = DAO.RecuperarCartaoLote();
                CartaoLote++;

                //Recupera Cartoes Gerados;
                List<PessoaController> Cartoes = DAO.CartoesParaImpressao(CartaoLote, 4);

                /*Enviar Email*/
                if (Cartoes.Count > 0)
                {
                    string str_from_address = ParametrosNegocios.EmailSaidaLogin;//sender
                    string str_name = ParametrosNegocios.NomeConta;
                    //The To address (Email ID)
                    string str_to_address = "impressaocartoescorporativo@clubeconteza.com.br";//recipient
                    //string str_to_address = "ti@clubeconteza.com.br";//recipient

                    System.Net.Mail.MailMessage email_msg = new MailMessage();

                    //Specifying From,Sender & Reply to address
                    email_msg.From = new MailAddress(str_from_address, str_name);
                    email_msg.Sender = new MailAddress(str_from_address, str_name);
                    //email_msg.ReplyTo = new MailAddress(str_from_address, str_name);

                    //The To Email id
                    email_msg.To.Add(str_to_address);

                    email_msg.Subject = "Cartões para impressão (Plano Corporativo)";//Subject of email
                    StringBuilder HTML = new StringBuilder();

                    HTML.Append("<!DOCTYPE html>");


                    HTML.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
                    HTML.Append("<head runat='server'>");
                    HTML.Append("<meta http-equiv='Content -Type' content='text/html; charset=utf-8'/>");
                    HTML.Append("<title>Cartões para Impressão Plano Familiar</title>");
                    HTML.Append("<style>");
                    HTML.Append("body");
                    HTML.Append("{");
                    HTML.Append("margin: 0px");
                    HTML.Append("}");
                    HTML.Append(".container");
                    HTML.Append("{");
                    HTML.Append("width: 100 %;");
                    HTML.Append("height: 100 %;");
                    HTML.Append("background: #EDEDED;");
                    HTML.Append("display:flex;");
                    HTML.Append("flex-direction:row;");
                    HTML.Append("justify-content:center;");
                    HTML.Append("align-items:center");
                    HTML.Append("}");
                    HTML.Append(".box {");
                    HTML.Append("width:760px;");
                    HTML.Append("height:300px;");
                    HTML.Append("background:#FFFFFF;");
                    HTML.Append("}");
                    HTML.Append(".auto-style1 {");
                    HTML.Append("text-align:left;");
                    HTML.Append("}");
                    HTML.Append("</style>");
                    HTML.Append("</head>");
                    HTML.Append("<body>");
                    HTML.Append("<div class='container'>");
                    HTML.Append("<div class='box'>");
                    HTML.Append("<div style='background-color:#FEC832; font-size: 26px; font-weight: bold; color: #07568C;'>");
                    HTML.Append("Clube Conteza");
                    HTML.Append("</div>");
                    HTML.Append("<br/>");
                    HTML.Append("<div style='background-color:#FEC832; text-align: center;' >");
                    HTML.Append("Cartões para Impressao</div>");
                    HTML.Append("<br/>");
                    HTML.Append("<div> ");
                    HTML.Append("Segue lista para impressão de cartões do Plano Corporativo</div>");
                    HTML.Append("<br/>");
                    HTML.Append("<div>");
                    HTML.Append("<table style='padding: 2px; margin: inherit; border: medium solid #000000; width:100%; table-layout: auto; border-spacing: inherit; border-collapse: collapse;'>");
                    HTML.Append("<tr>");
                    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Contrato</td>");
                    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>ID Pessoa</td>");
                    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Cartão</td>");
                    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Lote</td>");
                    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Nome Completo</td>");
                    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Celular</td>");
                    HTML.Append("</tr>");
                    int Linha = 0;

                    foreach (PessoaController Cartao in Cartoes)
                    {


                        HTML.Append("<tr>");
                        if (Linha == 0)
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }
                        HTML.Append(Cartao.TB012_Id);
                        HTML.Append("</td>");

                        if (Linha == 0)
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }
                        HTML.Append(Cartao.TB013_id);
                        HTML.Append("</td>");

                        if (Linha == 0)
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }
                        HTML.Append(Cartao.TB013_Cartao);
                        HTML.Append("</td>");

                        if (Linha == 0)
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }

                        /**/
                        HTML.Append(Cartao.TB013_CartaoLote);
                        HTML.Append("</td>");

                        if (Linha == 0)
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }

                        /**/
                        HTML.Append(Cartao.TB013_NomeCompleto);
                        //HTML.Append("</td>");
                        //HTML.Append("</tr>");
                        /**/
                        if (Linha == 0)
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }
                        HTML.Append(Cartao.Contato.TB009_Contato);
                        HTML.Append("</td>");
                        HTML.Append("</tr>");

                        /*Rotina para o Log*/
                        LogController Log_C = new LogController();

                        Log_C.TB012_Id = Cartao.TB012_Id;
                        Log_C.TB011_Id = 0;
                        Log_C.TB000_IdTabela = 13;
                        Log_C.TB000_Tabela = "Cartao";
                        Log_C.TB000_Data = DateTime.Now;
                        Log_C.TB000_Descricao = string.Format(MensagensLog.L0021.ToString(), Cartao.TB013_Cartao);
                        Log_N.LogInsert(Log_C);
                        /**/
                    }


                    HTML.Append("</table>");
                    HTML.Append("</div>");
                    HTML.Append("<div>");
                    HTML.Append("</div>");
                    HTML.Append("</div>");
                    HTML.Append("</div>");
                    HTML.Append("</body>");
                    HTML.Append("</html>");

                    email_msg.IsBodyHtml = true;
                    email_msg.Body = HTML.ToString();//body
                                                     //Create an object for SmtpClient class
                    SmtpClient mail_client = new SmtpClient();

                    //Providing Credentials (Username & password)
                    NetworkCredential network_cdr = new NetworkCredential();
                    network_cdr.UserName = str_from_address;
                    network_cdr.Password = ParametrosNegocios.EmailSaidaSenha;

                    mail_client.Credentials = network_cdr;

                    //Specify the SMTP Port
                    mail_client.Port = ParametrosNegocios.EmailSaidaPorta;

                    //Specify the name/IP address of Host
                    mail_client.Host = ParametrosNegocios.EmailSaidaSMTP;

                    //Uses Secure Sockets Layer(SSL) to encrypt the connection
                    mail_client.EnableSsl = true;

                    //Now Send the message
                    mail_client.Send(email_msg);
                    /*Apos Enviar Email Atualizar Status do Cartão*/
                    DAO.CartoesUpdateStatus(CartaoLote, 2);

                }
                /*Fim Email*/

                if (Cartoes.Count == 0)
                { CartaoLote = 0; }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return CartaoLote;
        }

        [WebMethod(Description = "Seta parcelas vencidas")]
        public Boolean SetarParcelasVencidas()
        {
            ParcelaDAO DAO = new ParcelaDAO();

            return DAO.SP_U_TB016_SetarParcelaVencida();
        }

        [WebMethod(Description = "Grava logo da unidade no servidor")]
        public Boolean GravarLogoUnidade(long Unidade)
        {
            //public UnidadeController (long TB012_id)
            ParceiroDAO DAO = new ParceiroDAO();
            UnidadeController Parceiro = DAO.UnidadeRecuperarLogo(Unidade);

            byte[] vetorImagem = Parceiro.TB020_logo;
            string Path = @"C:\\inetpub\\wwwroot\\unidades\";
            string strNomeArquivo = Convert.ToString(Parceiro.TB020_id) + ".jpg";

            //FileInfo LogoUnidade = new FileInfo(strNomeArquivo);
            if (!IsFileOpen(Path + strNomeArquivo))
            {
                File.Delete(Path + strNomeArquivo);

                using (var imageFile = new FileStream(Path + strNomeArquivo, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    imageFile.Write(vetorImagem, 0, vetorImagem.Length);
                    imageFile.Flush();
                    imageFile.Close();
                }
            }

            //pctLogoContrato.Image = Image.FromFile(strNomeArquivo);

            return true;
        }

        public bool IsFileOpen(string filePath)
        {
            bool fileOpened = false;
            try
            {
                System.IO.FileStream fs = System.IO.File.OpenWrite(filePath);
                fs.Close();
            }
            catch (System.IO.IOException)
            {
                fileOpened = true;
            }

            return fileOpened;
        }

        [WebMethod(Description = "Busca por sessao)")]
        public List<UnidadeController> ParceirosBuscaSessaoCidade(Int64 Sessao, Int64 Cidade, int Linhas, int Pagina, string Buscar)
        {
            ParceiroDAO DAO = new ParceiroDAO();
            List<UnidadeController> Parceiros = DAO.SP_S_Parceiro_Busca_Sessao_Cidade_Todos(Sessao, Cidade, Linhas, Pagina, Buscar);
            return Parceiros;
        }


        [WebMethod(Description = "Listar contezinos)")]
        public List<PessoaController> ListarContezinos(string cnpjParceiro, string senhaParceiro)
        {
            return new PessoaDAO().ListarContezinos(cnpjParceiro, senhaParceiro);
        }

        [WebMethod(Description = "Listar parceiros)")]
        public List<UnidadeController> ParceirosAtivos(string cnpjParceiro, string senhaParceiro)
        {
            return new ParceiroDAO().ParceirosAtivos(cnpjParceiro, senhaParceiro);
        }


        [WebMethod(Description = "Consulta contezino pelo Cartão)")]
        public PessoaController ContezinoCartao(string cnpjParceiro, string senhaParceiro, string Cartao)
        {
            return new PessoaDAO().contezinoCartao(cnpjParceiro, senhaParceiro, Cartao);
        }

        [WebMethod(Description = "Consulta contezino pelo CPF)")]
        public PessoaController contezinoCPF(string cnpjParceiro, string senhaParceiro, string cpf)
        {
            return new PessoaDAO().contezinoCPF(cnpjParceiro, senhaParceiro, cpf);
        }

        //[WebMethod(Description = "Enviar SMS's agendados)")]
        //public bool smsEnvioAgendado(DateTime dataReferencia)
        //{
        //    return new MensagemNegocios().smsAgendados(dataReferencia);
        //}


        [WebMethod(Description = "Consulta acessos multiplos ao contrato)")]
        public PessoaController acessosmultiplos(string cnpjParceiro, string senhaParceiro)
        {
            return new PessoaDAO().acessosmultiplos(cnpjParceiro, senhaParceiro);
        }

        [WebMethod(Description = "Contezino consulta Status)")]
        public string  contezinoConsultaStatus(string cnpjParceiro, string senhaParceiro,string variavel)
        {
            return new PessoaDAO().contezinoConsultaStatus(cnpjParceiro, senhaParceiro, variavel);
        }


        [WebMethod(Description = "Emitir Mensagens liberadas")]
        public bool emitirMensagens()
        {
            try
            {
                bool sms = new MensagemNegocios().smsLiberados();
                bool email = new MensagemNegocios().emailLiberados();
              
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return true;
        }

         [WebMethod(Description = "Emitir Mensagens liberadas Fornecedor 2")]
        public bool emitirMensagens02()
        {
            try
            {
                //bool sms = new MensagemNegocios().smsLiberados();
                //bool email = new MensagemNegocios().emailLiberados();

                List<MensagemController> emailsLiberados = new MensagemDAO().emailsLiberados(DateTime.Now);

                for (int i = 0; i < emailsLiberados.Count; i++)
                {
                    EnviaSmsController sms = new EnviaSmsController();
                    //sms.MensagemSms.IdMensagem = emailsLiberados[i].TB039_id;
                    //sms.MensagemSms.Remetente = "4141984082926";
                    //sms.MensagemSms.NumeroCelular = emailsLiberados[i].TB009_id;
                    //sms.MensagemSms.Mensagem = emailsLiberados[i].TB039_Mensagem;


                    var servico = new ServicoEnvioUnicoSms(sms);
                    servico.Enviador = new RequisicaoSms();
                    servico.Envia();

                    //var temp = servico.Retorno;


                    // sms.EnviaSmsLista.MensagemVariosSms
                }
                //foreach (EnviaSmsMultiController sms in emailsLiberados)
                //{
                //    //var servico = new ServicoEnvioVariosSms(sms);
                //    //servico.Enviador = new RequisicaoSms();
                //    //servico.Envia();

                //    //var temp = servico.Retorno;
                //}

          //      < Remetente > string </ Remetente >
          //< NumeroCelular > long </ NumeroCelular >
          //< DataMensagemEnvia > dateTime </ DataMensagemEnvia >
          //< Mensagem > string </ Mensagem >
          //< IdMensagem > long </ IdMensagem >


            }
            catch (Exception ex)
            {
                throw ex;
            }


            return true;
        }
    }
}

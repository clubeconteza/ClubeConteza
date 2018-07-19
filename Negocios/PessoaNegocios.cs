using Controller;
using DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocios
{
    public class PessoaNegocios
    {
        public bool PessoaUpdateTitularContratoFamiliar(PessoaController Titular)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();


                LogController Log_C = new LogController();
                LogNegocios Log_N = new LogNegocios();
                Log_C.TB000_Descricao = PessoaAlteracaoParaLog(Titular);

                Log_C.TB012_Id = Titular.TB012_Id;
                Log_C.TB011_Id = Titular.TB013_AlteradoPor;
                Log_C.TB000_IdTabela = 13;
                Log_C.TB000_Tabela = "Pessoa";
                Log_C.TB000_Data = DateTime.Now;

                if (DAO.PessoaUpdateTitularContratoFamiliar(Titular))
                {
                    

                    Log_N.LogInsert(Log_C);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PessoaController> pessoaSelect(String Query)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.PessoaSelect(Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PessoaController pessoaSelectId(long TB013_id)
        {
            try
            {
                PessoaDao DAOPessoa = new PessoaDao();

                PessoaController Pessoa = new PessoaController();
                Pessoa = DAOPessoa.PessoaSelectId(TB013_id);

                ContatoDao DAOContato = new ContatoDao();
                Pessoa.Contatos = DAOContato.ContatosDaPessoa(TB013_id);


                return Pessoa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PessoaController PessoaInsert(PessoaController Pessoa)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                PessoaController PessoaR = DAO.PessoaInsert(Pessoa);

                LogNegocios Log_N = new LogNegocios();
                LogController Log_C = new LogController();

                Log_C.TB012_Id = PessoaR.TB013_id;
                Log_C.TB011_Id = Pessoa.TB013_CadastradoPor;
                Log_C.TB000_IdTabela = 13;
                Log_C.TB000_Tabela = "Pessoa";
                Log_C.TB000_Data = DateTime.Now;
                Log_C.TB000_Descricao = MensagensLog.L0002.ToString().Replace("$NomeCompleto", Pessoa.TB013_NomeCompleto).Replace("$ID", PessoaR.TB013_id.ToString());
                Log_N.LogInsert(Log_C);

                return PessoaR;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool PessoaFamiliarUpdate(PessoaController Pessoa)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();

                LogController Log_C = new LogController();
                LogNegocios Log_N = new LogNegocios();
                Log_C.TB000_Descricao = PessoaAlteracaoParaLog(Pessoa);

                Log_C.TB012_Id = Pessoa.TB012_Id;
                Log_C.TB011_Id = Pessoa.TB013_AlteradoPor;
                Log_C.TB000_IdTabela = 13;
                Log_C.TB000_Tabela = "Pessoa";
                Log_C.TB000_Data = DateTime.Now;

                if (DAO.PessoaFamiliarUpdate(Pessoa))
                {
                    if (Log_C.TB000_Descricao.Length > 5)
                    {
                        Log_N.LogInsert(Log_C);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long DependenteFamiliarInsert(PessoaController pessoa)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();

                long pessoaR = DAO.DependenteFamiliarInsert(pessoa);

                LogNegocios logN = new LogNegocios();
                LogController logC = new LogController();

                logC.TB012_Id = pessoaR;
                logC.TB011_Id = pessoa.TB013_CadastradoPor;
                logC.TB000_IdTabela = 13;
                logC.TB000_Tabela = "Pessoa";
                logC.TB000_Data = DateTime.Now;
                logC.TB000_Descricao = MensagensLog.L0002.ToString().Replace("$NomeCompleto", pessoa.TB013_NomeCompleto).Replace("$ID", pessoaR.ToString());
                logN.LogInsert(logC);

                return pessoaR;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PessoaController DependenteFamiliarSelect(long TB013_id)
        {
            try
            {
                PessoaController pessoa = new PessoaDao().DependenteFamiliarSelect(TB013_id);

                return pessoa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DependenteUpdate(PessoaController Pessoa, Int64 TB012_Id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();

                LogController Log_C = new LogController();
                LogNegocios Log_N = new LogNegocios();
                Log_C.TB000_Descricao = PessoaAlteracaoParaLog(Pessoa);

                Log_C.TB012_Id = TB012_Id;
                Log_C.TB011_Id = Pessoa.TB013_AlteradoPor;
                Log_C.TB000_IdTabela = 13;
                Log_C.TB000_Tabela = "Pessoa";
                Log_C.TB000_Data = DateTime.Now;

                if (DAO.DependenteUpdate(Pessoa))
                {
                    if (Log_C.TB000_Descricao.Length > 5)
                    {
                        Log_N.LogInsert(Log_C);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PessoaController> DependentesSelect(Int64 TB012_id, Int64 TB013_id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.DependentesSelect(TB012_id, TB013_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool PessoaUpdate(PessoaController Pessoa, long TB012_Id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();

                LogController Log_C = new LogController();
                LogNegocios Log_N = new LogNegocios();
                Log_C.TB000_Descricao = PessoaAlteracaoParaLog(Pessoa);

                Log_C.TB012_Id = TB012_Id;
                Log_C.TB011_Id = Pessoa.TB013_AlteradoPor;
                Log_C.TB000_IdTabela = 13;
                Log_C.TB000_Tabela = "Pessoa";
                Log_C.TB000_Data = DateTime.Now;

                if (DAO.PessoaUpdate(Pessoa))
                {
                    if (Log_C.TB000_Descricao.Length > 5)
                    {
                        Log_N.LogInsert(Log_C);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ParceiroUpdate(PessoaController Pessoa)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.PessoaUpdate(Pessoa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CategoriaIdadeControler> MembrosAtivosDoConrato(Int64 TB012_id, DateTime DataReferencia)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.MembrosAtivosDoConrato(TB012_id, DataReferencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool VincularTitularContato(long TB012_id, long TB013_id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.VincularTitularContato(TB012_id, TB013_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool VincularDependenteContato(long TB012_id, PessoaController Pessoa)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();

                DAO.VincularDependenteContato(TB012_id, Pessoa.TB013_id);

                LogNegocios Log_N = new LogNegocios();
                LogController Log_C = new LogController();

                Log_C.TB012_Id = TB012_id;
                Log_C.TB011_Id = Pessoa.TB013_AlteradoPor;
                Log_C.TB000_IdTabela = 12;
                Log_C.TB000_Tabela = "Contrato";
                Log_C.TB000_Data = DateTime.Now;
                Log_C.TB000_Descricao = MensagensLog.L0004.ToString().Replace("$NomeCompleto", Pessoa.TB013_NomeCompleto).Replace("$ID", Pessoa.TB013_id.ToString());
                Log_C.TB000_Descricao = Log_C.TB000_Descricao.Replace("$Contrato", TB012_id.ToString().PadLeft(6, '0'));
                Log_N.LogInsert(Log_C);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public List<PessoaController> ListaAniversariantesDoMes(Int64 TB012_id, DateTime TB016_Vencimento)
        //{
        //    try
        //    {
        //        PessoaDao DAO = new PessoaDao();
        //        return DAO.ListaAniversariantesDoMes(TB012_id, TB016_Vencimento);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public PessoaController pessoaSelectCPFCNPJ(string CPFCNPJ)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.PessoaSelectCpfcnpj(CPFCNPJ);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public double PessoaCodigoDependenteNovo(long TB012_id)
        {
            Double Retorno = 0;
            try
            {
                PessoaDao DAO = new PessoaDao();
                Retorno = DAO.PessoaCodigoDependenteNovo(TB012_id);

                if (Retorno == 0)
                {
                    Retorno = DAO.PessoaManiorCodDependente(TB012_id);
                }

                Retorno = Retorno + 1;
                return Retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PessoaController> GerarCartoes(long TB012_id, long TB011_id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                List<PessoaController> Retorno_L = new List<PessoaController>();
                List<PessoaController> Cartao_L = DAO.PessoasParaGerarCartao(TB012_id);

                foreach (var Pessoa in Cartao_L)
                {
                    string ContratoCartao;
                    /*Devido contratos legados*/
                    if (Pessoa.Contrato.TB012_Id < 100000)
                    {
                        ContratoCartao = Pessoa.Contrato.TB012_Id.ToString("100000");
                    }
                    else { ContratoCartao = Pessoa.Contrato.TB012_Id.ToString(); }

                    string NovoCartao = Pessoa.Municipio.TB006_Codigo + "-" + ContratoCartao + "-" + Pessoa.TB013_CodigoDependente;


                    if (DAO.PessoaVincularCartao(Pessoa.TB013_id, NovoCartao.Trim(), TB011_id))
                    {
                        PessoaController Cartao = new PessoaController();
                        Cartao.TB013_id = Pessoa.TB013_id;
                        Cartao.TB013_NomeCompleto = Pessoa.TB013_NomeCompleto;
                        Cartao.TB013_Cartao = NovoCartao;// Pessoa.TB013_Cartao;
                        Retorno_L.Add(Cartao);

                        LogNegocios Log_N = new LogNegocios();
                        LogController Log_C = new LogController();

                        Log_C.TB012_Id = Pessoa.Contrato.TB012_Id;
                        Log_C.TB011_Id = TB011_id;
                        Log_C.TB011_Ref = 0;
                        Log_C.TB000_IdTabela = 13;
                        Log_C.TB000_Tabela = "Pessoa";
                        Log_C.TB000_Data = DateTime.Now;
                        Log_C.TB000_Descricao = MensagensLog.L0012.ToString().Replace("$CARTAO", Cartao.TB013_Cartao.ToString());
                        Log_N.LogInsert(Log_C);

                    }
                }
                return Retorno_L;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string PessoaAlteracaoParaLog(PessoaController Pessoa)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.PessoaAlteracaoParaLog(Pessoa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean DependenteAlterarStatus(Int64 TB013_id, int Status, Int64 TB012_id, Int64 TB011_Id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();

                if (DAO.DependenteAlterarStatus(TB013_id, Status, TB012_id))
                {

                    LogNegocios Log_N = new LogNegocios();
                    LogController Log_C = new LogController();

                    Log_C.TB012_Id = TB012_id;
                    Log_C.TB011_Id = TB011_Id;
                    Log_C.TB000_IdTabela = 13;
                    Log_C.TB000_Tabela = "Pessoa";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = string.Format(MensagensLog.L0019.ToString(), TB013_id, Enum.GetName(typeof(PessoaController.TB013_StatusE), Convert.ToInt16(Status)));
                    Log_N.LogInsert(Log_C);
                    /*Alterar campo TB012_VSContratoNova em TB012_VSContratoNova*/
                    ContratosDao Contrato_D = new ContratosDao();
                    if(Contrato_D.ContratoInformarEdicao(TB012_id, TB011_Id))
                    {
                        Log_C.TB000_Data = DateTime.Now;
                        Log_C.TB000_Descricao = string.Format(MensagensLog.L0035.ToString(), TB012_id);
                        Log_N.LogInsert(Log_C);
                    }

                    return true;
                }
                else
                { return false; }
                //return DAO.DependenteCancelar(Pessoa, TB011_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PessoaController> Cartoes(string Query)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.Cartoes(Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PessoaController> CartoesContrato(long TB012_id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.CartoesContrato(TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PessoaController CartaoPessoa(Int64 TB013_id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.CartaoPessoa(TB013_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PessoaController Cartao(string TB013_Cartao)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.Cartao(TB013_Cartao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean CartaoConfirmarRecebimento(Int64 TB013_id, Int64 TB011_id, Int64 TB012_id, string Cartao)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                if (DAO.CartaoConfirmarRecebimento(TB013_id, TB011_id))
                {
                    LogNegocios Log_N = new LogNegocios();
                    LogController Log_C = new LogController();
                    Log_C.TB012_Id = TB012_id;
                    Log_C.TB011_Id = TB011_id;
                    Log_C.TB000_IdTabela = 13;
                    Log_C.TB000_Tabela = "Cartao";
                    Log_C.TB000_Data = DateTime.Now;

                    Log_C.TB000_Descricao = string.Format(MensagensLog.L0022.ToString(), Cartao);
                    Log_N.LogInsert(Log_C);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        public Boolean CartaoConfirmarEntrega(Int64 TB013_id, Int64 TB012_id, string EntreguePara, Int64 TB011_id, string Cartao)

        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                if (DAO.CartaoConfirmarEntrega(TB013_id, EntreguePara, TB011_id))
                {
                    LogNegocios Log_N = new LogNegocios();
                    LogController Log_C = new LogController();
                    Log_C.TB012_Id = TB012_id;
                    Log_C.TB011_Id = TB011_id;
                    Log_C.TB000_IdTabela = 13;
                    Log_C.TB000_Tabela = "Cartao";
                    Log_C.TB000_Data = DateTime.Now;

                    Log_C.TB000_Descricao = string.Format(MensagensLog.L0023.ToString(), Cartao, EntreguePara);
                    Log_N.LogInsert(Log_C);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        public Boolean CartaoManual(Int64 TB013_id, string TB013_Cartao, int TB013_CarteirinhaStatus, Int64 TB013_AlteradoPor, Int64 TB012_id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();

                if (DAO.CartaoManual(TB013_id, TB013_Cartao, TB013_CarteirinhaStatus, TB013_AlteradoPor))
                {
                    LogNegocios Log_N = new LogNegocios();
                    LogController Log_C = new LogController();

                    Log_C.TB012_Id = TB012_id;
                    Log_C.TB011_Id = TB013_AlteradoPor;
                    Log_C.TB000_IdTabela = 13;
                    Log_C.TB000_Tabela = "Cartao";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = string.Format(MensagensLog.L0024.ToString(), TB013_Cartao);
                    Log_N.LogInsert(Log_C);

                    return true;
                }
                else
                { return false; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GerarCartaoParceiro(Int64 TB012_id, Int64 TB013_AlteradoPor)
        {
            string Cartao = "";
            try
            {
                PessoaDao DAO = new PessoaDao();


                PessoaController TitularParceiro = DAO.RecuperarDadosParceiroParaCartao(TB012_id);

                if(TitularParceiro.TB013_id>0)
                {
                    string  v1 = "1" + Convert.ToInt16( TitularParceiro.Municipio.TB006_Codigo).ToString();
                    Int64   v2 = TB012_id;
                    double v3 = Convert.ToDouble( TitularParceiro.TB012_ProximoCodDependente) + 1;
                     Cartao = v1 + "-" + v2 + "-" + v3;

                    //double TB012_ProximoCodDependente = v3 + 1;


                    if (DAO.GravarCartaoParceiro(TitularParceiro.TB013_id, Cartao.Trim(),1, TB013_AlteradoPor))
                    {

                        ContratosDao Contrato_D = new ContratosDao();

                        Contrato_D.ContratoCodDependente(TB012_id, v3);

                        LogNegocios Log_N = new LogNegocios();
                        LogController Log_C = new LogController();

                        Log_C.TB012_Id = TB012_id;
                        Log_C.TB011_Id = TB013_AlteradoPor;
                        Log_C.TB000_IdTabela = 13;
                        Log_C.TB000_Tabela = "Cartao";
                        Log_C.TB000_Data = DateTime.Now;
                        Log_C.TB000_Descricao = string.Format(MensagensLog.L0024.ToString(), Cartao);
                        Log_N.LogInsert(Log_C);
                        /*Enviar e-mail de impressão*/
                        

                    }
                }
                else
                {
                    return "";
                }


                    return Cartao;
          
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public Boolean GeracaoCartaoBaixaPagamentoManual(Int64 TB016_id, Int64 TB013_AlteradoPor)
        //{

        //    try
        //    {
        //        PessoaDao DAO = new PessoaDao();

        //        List<PessoaController> Pessoas = DAO.GeracaoCartaoBaixaPagamentoManual_RetornarListaParaGeracao(TB016_id);
        //        List<PessoaController> Cartoes = new List<PessoaController>();
        //        //1º Localiza dados do contrato;
        

        //        foreach (PessoaController Pessoa in Pessoas)
        //        {
        //            Pessoa.TB013_Cartao = "1" + Convert.ToInt32(Pessoa.Municipio.TB006_Codigo) + "-" + Pessoa.TB012_Id.ToString() + "-" + Pessoa.TB013_CodigoDependente;
        //            Pessoa.TB013_CarteirinhaStatusS = "1";
        //            //Atualizar Contrato
           
        //            PessoaController Cartao = new PessoaController();
                    
        //            Cartao.Contato = new ContatoController();
        //            Cartao.TB012_Id = Pessoa.TB012_Id;
        //            Cartao.TB013_id = Pessoa.TB013_id;
        //            Cartao.TB013_Cartao = Pessoa.TB013_Cartao;
        //            Cartao.TB013_NomeCompleto = Pessoa.TB013_NomeCompleto;
        //            Cartao.Contato.TB009_Contato = Pessoa.Contato.TB009_Contato;
        //            Cartoes.Add(Cartao);
        //            //Atualizar Pessoa
        //            if (DAO.GravarCartaoParceiro(Pessoa.TB013_id, Pessoa.TB013_Cartao.Trim(), Convert.ToInt16(Pessoa.TB013_CarteirinhaStatusS), TB013_AlteradoPor))
        //                {
            
        //                    LogNegocios Log_N = new LogNegocios();
        //                    LogController Log_C = new LogController();

        //                    Log_C.TB012_Id = Pessoas[0].TB012_Id;
        //                    Log_C.TB011_Id = TB013_AlteradoPor;
        //                    Log_C.TB000_IdTabela = 13;
        //                    Log_C.TB000_Tabela = "Cartao";
        //                    Log_C.TB000_Data = DateTime.Now;
        //                    Log_C.TB000_Descricao = string.Format(MensagensLog.L0024.ToString(), Pessoa.TB013_Cartao);
        //                    Log_N.LogInsert(Log_C);                    
        //            }
        //            else
        //                {
        //                    return false;
        //                }
        //        }


        //        /*Enviar Email*/
        //        //if (Cartoes.Count > 0)
        //        //{
        //        //    string str_from_address = ParametrosNegocios.EmailSaidaLogin;//sender
        //        //    string str_name = ParametrosNegocios.NomeConta;
        //        //    //The To address (Email ID)
        //        //    string str_to_address = "impressaocartoes@clubeconteza.com.br ";//recipient

        //        //    System.Net.Mail.MailMessage email_msg = new MailMessage();

        //        //    //Specifying From,Sender & Reply to address
        //        //    email_msg.From = new MailAddress(str_from_address, str_name);
        //        //    email_msg.Sender = new MailAddress(str_from_address, str_name);
        //        //    //email_msg.ReplyTo = new MailAddress(str_from_address, str_name);

        //        //    //The To Email id
        //        //    email_msg.To.Add(str_to_address);

        //        //    email_msg.Subject = "Cartões para impressão";//Subject of email
        //        //    StringBuilder HTML = new StringBuilder();

        //        //    HTML.Append("<!DOCTYPE html>");


        //        //    HTML.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
        //        //    HTML.Append("<head runat='server'>");
        //        //    HTML.Append("<meta http-equiv='Content -Type' content='text/html; charset=utf-8'/>");
        //        //    HTML.Append("<title>Cancelamento Parcela </title>");
        //        //    HTML.Append("<style>");
        //        //    HTML.Append("body");
        //        //    HTML.Append("{");
        //        //    HTML.Append("margin: 0px");
        //        //    HTML.Append("}");
        //        //    HTML.Append(".container");
        //        //    HTML.Append("{");
        //        //    HTML.Append("width: 100 %;");
        //        //    HTML.Append("height: 100 %;");
        //        //    HTML.Append("background: #EDEDED;");
        //        //    HTML.Append("display:flex;");
        //        //    HTML.Append("flex-direction:row;");
        //        //    HTML.Append("justify-content:center;");
        //        //    HTML.Append("align-items:center");
        //        //    HTML.Append("}");
        //        //    HTML.Append(".box {");
        //        //    HTML.Append("width:760px;");
        //        //    HTML.Append("height:300px;");
        //        //    HTML.Append("background:#FFFFFF;");
        //        //    HTML.Append("}");
        //        //    HTML.Append(".auto-style1 {");
        //        //    HTML.Append("text-align:left;");
        //        //    HTML.Append("}");
        //        //    HTML.Append("</style>");
        //        //    HTML.Append("</head>");
        //        //    HTML.Append("<body>");
        //        //    HTML.Append("<div class='container'>");
        //        //    HTML.Append("<div class='box'>");
        //        //    HTML.Append("<div style='background-color:#0071C5; font-size: 26px; font-weight: bold; color: #FFFFFF;'>");
        //        //    HTML.Append("Clube Conteza");
        //        //    HTML.Append("</div>");
        //        //    HTML.Append("<br/>");
        //        //    HTML.Append("<div style='background-color:#F3F3F3; text-align: center;' >");
        //        //    HTML.Append("Cartões para Impressao</div>");
        //        //    HTML.Append("<br/>");
        //        //    HTML.Append("<div> ");
        //        //    HTML.Append("Segue lista para impressão de cartões</div>");
        //        //    HTML.Append("<br/>");
        //        //    HTML.Append("<div>");
        //        //    HTML.Append("<table style='padding: 2px; margin: inherit; border: medium solid #000000; width:100%; table-layout: auto; border-spacing: inherit; border-collapse: collapse;'>");
        //        //    HTML.Append("<tr>");
        //        //    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Contrato</td>");
        //        //    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>ID Pessoa</td>");
        //        //    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Cartão</td>");
        //        //    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Nome Completo</td>");
        //        //    HTML.Append("<td style='border-style: solid; background-color: #4472C4;'>Celular</td>");
        //        //    HTML.Append("</tr>");
        //        //    int Linha = 0;

        //        //    foreach (PessoaController Cartao in Cartoes)
        //        //    {
        //        //        HTML.Append("<tr>");
        //        //        if (Linha == 0)
        //        //        {
        //        //            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
        //        //        }
        //        //        else
        //        //        {
        //        //            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
        //        //        }
        //        //        HTML.Append(Cartao.TB012_Id);
        //        //        HTML.Append("</td>");

        //        //        if (Linha == 0)
        //        //        {
        //        //            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
        //        //        }
        //        //        else
        //        //        {
        //        //            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
        //        //        }
        //        //        HTML.Append(Cartao.TB013_id);
        //        //        HTML.Append("</td>");

        //        //        if (Linha == 0)
        //        //        {
        //        //            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
        //        //        }
        //        //        else
        //        //        {
        //        //            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
        //        //        }
        //        //        HTML.Append(Cartao.TB013_Cartao);
        //        //        HTML.Append("</td>");

        //        //        if (Linha == 0)
        //        //        {
        //        //            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
        //        //        }
        //        //        else
        //        //        {
        //        //            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
        //        //        }
        //        //        HTML.Append(Cartao.TB013_NomeCompleto);
        //        //        //HTML.Append("</td>");
        //        //        //HTML.Append("</tr>");
        //        //        /**/
        //        //        if (Linha == 0)
        //        //        {
        //        //            HTML.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
        //        //        }
        //        //        else
        //        //        {
        //        //            HTML.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
        //        //        }
        //        //        HTML.Append(Cartao.Contato.TB009_Contato);
        //        //        HTML.Append("</td>");
        //        //        HTML.Append("</tr>");
        //        //    }


        //        //    HTML.Append("</table>");
        //        //    HTML.Append("</div>");
        //        //    HTML.Append("<div>");
        //        //    HTML.Append("</div>");
        //        //    HTML.Append("</div>");
        //        //    HTML.Append("</div>");
        //        //    HTML.Append("</body>");
        //        //    HTML.Append("</html>");

        //        //    email_msg.IsBodyHtml = true;
        //        //    email_msg.Body = HTML.ToString();//body
        //        //                                     //Create an object for SmtpClient class
        //        //    SmtpClient mail_client = new SmtpClient();

        //        //    //Providing Credentials (Username & password)
        //        //    NetworkCredential network_cdr = new NetworkCredential();
        //        //    network_cdr.UserName = str_from_address;
        //        //    network_cdr.Password = ParametrosNegocios.EmailSaidaSenha;

        //        //    mail_client.Credentials = network_cdr;

        //        //    //Specify the SMTP Port
        //        //    mail_client.Port = ParametrosNegocios.EmailSaidaPorta;

        //        //    //Specify the name/IP address of Host
        //        //    mail_client.Host = ParametrosNegocios.EmailSaidaSMTP;

        //        //    //Uses Secure Sockets Layer(SSL) to encrypt the connection
        //        //    mail_client.EnableSsl = true;

        //        //    //Now Send the message
        //        //    mail_client.Send(email_msg);
        //        //}
        //        //    /*Fim Email*/

        //            return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public PessoaController ColaboradorInsert(PessoaController Colaborador)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();

                PessoaController PessoaR = new PessoaController();
                  PessoaR=  DAO.ColaboradorInsert(Colaborador);

                if(PessoaR.TB013_id>0)
                { 
                    LogNegocios Log_N = new LogNegocios();
                    LogController Log_C = new LogController();

                    Log_C.TB012_Id = PessoaR.TB013_id;
                    Log_C.TB011_Id = Colaborador.TB013_CadastradoPor;
                    Log_C.TB000_IdTabela = 13;
                    Log_C.TB000_Tabela = "Pessoa";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = MensagensLog.L0002.ToString().Replace("$NomeCompleto", Colaborador.TB013_NomeCompleto).Replace("$ID", PessoaR.TB013_id.ToString());
                    Log_N.LogInsert(Log_C);
                }
                return PessoaR;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean VincularFamiliarCorporativo(Int64 TB012_id, Int64 TB013_id, Int64 TB011_id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();

              

                if (DAO.VincularFamiliarCorporativo(TB012_id, TB013_id))
                {
                    LogNegocios Log_N = new LogNegocios();
                    LogController Log_C = new LogController();

                    Log_C.TB012_Id = TB012_id;
                    Log_C.TB011_Id = TB011_id;
                    Log_C.TB000_IdTabela = 13;
                    Log_C.TB000_Tabela = "Pessoa";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = string.Format(MensagensLog.L0029.ToString(), TB013_id.ToString(), TB012_id.ToString());
                    Log_N.LogInsert(Log_C);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PessoaController> ColaboradoresCorporativo(long TB012_Corporativo)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.ColaboradoresCorporativo(TB012_Corporativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PessoaController ColaboradorCorporativo(long TB013_id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.ColaboradorCorporativo(TB013_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean ColaboradorUpdate(PessoaController Colaborador, Int64 TB012_Id)

        {
            try
            {
                PessoaDao DAO = new PessoaDao();

                LogController Log_C = new LogController();
                LogNegocios Log_N = new LogNegocios();
                Log_C.TB000_Descricao = PessoaAlteracaoParaLog(Colaborador);

                Log_C.TB012_Id = TB012_Id;
                Log_C.TB011_Id = Colaborador.TB013_AlteradoPor;
                Log_C.TB000_IdTabela = 13;
                Log_C.TB000_Tabela = "Pessoa";
                Log_C.TB000_Data = DateTime.Now;

                if (DAO.ColaboradorUpdate(Colaborador))
                {
                    if (Log_C.TB000_Descricao.Length > 5)
                    {
                        Log_N.LogInsert(Log_C);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PessoaController> DependentesParaInativacao(long TB012_id, Int64 TB011_Id, Int16 TB012_VSContrato)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                LogNegocios Log_N = new LogNegocios();
                List<PessoaController> DependentesParaInativacao = DAO.DependentesParaInativacao(TB012_id);
                int AtualizarVSContrato = 0;

                foreach (PessoaController Pessoa in DependentesParaInativacao)
                {
                    if (DAO.DependenteAlterarStatus(Pessoa.TB013_id, 2, TB012_id))
                    {
                        AtualizarVSContrato = 1;                     
                        LogController Log_C = new LogController();
                        Log_C.TB012_Id = TB012_id;
                        Log_C.TB011_Id = TB011_Id;
                        Log_C.TB000_IdTabela = 13;
                        Log_C.TB000_Tabela = "Pessoa";
                        Log_C.TB000_Data = DateTime.Now;
                        Log_C.TB000_Descricao = string.Format(MensagensLog.L0033.ToString(), Pessoa.TB013_NomeCompleto);
                        Log_N.LogInsert(Log_C);
                    }
                }
                if(AtualizarVSContrato>0)
                {
                    ContratosDao Contrato_D = new ContratosDao();
                    //Int16 TB012_VSContrato = Contrato_D.ContratoVSAtual(TB012_id);

                    //TB012_VSContrato++;
                    if(Contrato_D.ContratoVsAtualizar(TB012_id, TB012_VSContrato, TB011_Id))
                    {
                        LogController Log_C = new LogController();
                        Log_C.TB012_Id = TB012_id;
                        Log_C.TB011_Id = TB011_Id;
                        Log_C.TB000_IdTabela = 13;
                        Log_C.TB000_Tabela = "Pessoa";
                        Log_C.TB000_Data = DateTime.Now;
                        Log_C.TB000_Descricao = string.Format(MensagensLog.L0034.ToString(), TB012_VSContrato);
                        Log_N.LogInsert(Log_C);
                    }
                }

                    return DependentesParaInativacao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PessoaController> CorporativoPessoasNaoAtivadas(long TB012_id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.CorporativoPessoasNaoAtivadas(TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PessoaController ColaboradorCorporativoTitular(Int64 TB013_id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.ColaboradorCorporativoTitular(TB013_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool PessoaFisicaUpdate(PessoaController pessoa, int TB026_Negociacao)
        {
            try
            {
                PessoaController pessoaOriginal = pessoaSelectId(pessoa.TB013_id);
                if (new PessoaDao().PessoaFisicaUpdate(pessoa))
                {
                    StringBuilder sEdicao = new StringBuilder();
                    sEdicao.Append(" CPF/CNPJ de ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoaOriginal.TB013_CPFCNPJ);
                    sEdicao.Append("'");
                    sEdicao.Append(" Para ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoa.TB013_CPFCNPJ);
                    sEdicao.Append("'");
                    sEdicao.Append(Environment.NewLine);
                    sEdicao.Append(" Nome completo de ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoaOriginal.TB013_NomeCompleto);
                    sEdicao.Append("'");
                    sEdicao.Append(" Para ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoa.TB013_NomeCompleto);
                    sEdicao.Append("'");
                    sEdicao.Append(Environment.NewLine);
                    sEdicao.Append(" Nome exibição ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoaOriginal.TB013_NomeExibicao);
                    sEdicao.Append("'");
                    sEdicao.Append(" Para ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoa.TB013_NomeExibicao);
                    sEdicao.Append("'");
                    sEdicao.Append(Environment.NewLine);
                    sEdicao.Append(" Data nascimento ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoaOriginal.TB013_DataNascimento.ToString("dd/MM/yyyy"));
                    sEdicao.Append("'");
                    sEdicao.Append(" Para ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoa.TB013_DataNascimento.ToString("dd/MM/yyyy"));
                    sEdicao.Append("'");
                    sEdicao.Append(Environment.NewLine);
                    sEdicao.Append(" CEP de ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoaOriginal.TB004_Cep);
                    sEdicao.Append("'");
                    sEdicao.Append(" Para ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoa.TB004_Cep);
                    sEdicao.Append("'");
                    sEdicao.Append(Environment.NewLine);
                    sEdicao.Append(" Logradouro de ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoaOriginal.TB013_Logradouro);
                    sEdicao.Append("'");
                    sEdicao.Append(" Para ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoa.TB013_Logradouro);
                    sEdicao.Append("'");
                    sEdicao.Append(Environment.NewLine);
                    sEdicao.Append(" Numero de ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoaOriginal.TB013_Numero);
                    sEdicao.Append("'");
                    sEdicao.Append(" Para ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoa.TB013_Numero);
                    sEdicao.Append("'");
                    sEdicao.Append(Environment.NewLine);
                    sEdicao.Append(" Bairro de ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoaOriginal.TB013_Bairro);
                    sEdicao.Append("'");
                    sEdicao.Append(" Para ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoa.TB013_Bairro);
                    sEdicao.Append("'");
                    sEdicao.Append(Environment.NewLine);
                    sEdicao.Append(" Complemento de ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoaOriginal.TB013_Complemento);
                    sEdicao.Append("'");
                    sEdicao.Append(" Para ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoa.TB013_Complemento);
                    sEdicao.Append("'");
                    sEdicao.Append(Environment.NewLine);
                    sEdicao.Append(" Nome da mãe de ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoaOriginal.TB013_MaeNome);
                    sEdicao.Append("'");
                    sEdicao.Append(" Para ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoa.TB013_MaeNome);
                    sEdicao.Append("'");
                    sEdicao.Append(Environment.NewLine);
                    sEdicao.Append(" Data nascimento da mãe de  ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoaOriginal.TB013_MaeDataNascimento.ToString("dd/MM/yyyy"));
                    sEdicao.Append("'");
                    sEdicao.Append(" Para ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoa.TB013_MaeDataNascimento.ToString("dd/MM/yyyy"));
                    sEdicao.Append("'");
                    sEdicao.Append(Environment.NewLine);
                    sEdicao.Append(" Nome do pai de");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoaOriginal.TB013_PaiNome);
                    sEdicao.Append("'");
                    sEdicao.Append(" Para ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoa.TB013_PaiNome);
                    sEdicao.Append("'");
                    sEdicao.Append(Environment.NewLine);
                    sEdicao.Append(" Data nascimento do pai de  ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoaOriginal.TB013_PaiDataNascimento.ToString("dd/MM/yyyy"));
                    sEdicao.Append("'");
                    sEdicao.Append(" Para ");
                    sEdicao.Append("'");
                    sEdicao.Append(pessoa.TB013_PaiDataNascimento.ToString("dd/MM/yyyy"));
                    sEdicao.Append("'");

                    AnotacoesController Log_C = new AnotacoesController();
                    Log_C.Tb012Id = pessoa.TB012_Id;
                    Log_C.TB026_Negociacao = TB026_Negociacao;
                    Log_C.Tb011Id = pessoa.TB013_AlteradoPor;
                    Log_C.Tb026Data = DateTime.Now;
                    Log_C.Tb026Cod =  "L0072";

                    Log_C.Tb026Anotacao = string.Format(MensagensLog.L0072.ToString(), pessoa.TB013_id.ToString(), pessoa.TB013_NomeCompleto, sEdicao.ToString());
                    new AnotacoesDao().Anotacaoinsert(Log_C);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        public bool ContezinoSenha(PessoaController usuario)
        {
            try
            {
                LogNegocios Log_N = new LogNegocios();
                LogController Log_C = new LogController();


                long id = new PessoaDao().Senha(usuario.TB013_id);

                if(id>0)
                {
                    if (new PessoaDao().alterarSenha(usuario))
                    {
                        Log_C.TB012_Id = usuario.TB012_Id;
                        Log_C.TB011_Id = usuario.TB013_CadastradoPor;
                        Log_C.TB000_IdTabela = 13;
                        Log_C.TB000_Tabela = "Pessoa";
                        Log_C.TB000_Data = DateTime.Now;
                        Log_C.TB000_Descricao = string.Format(MensagensLog.L0077, usuario.TB013_id.ToString());
                        Log_N.LogInsert(Log_C);
                    }
                }
                else
                {

                    if(new PessoaDao().cadastrarSenha(usuario))
                    {
                        Log_C.TB012_Id = usuario.TB012_Id;
                        Log_C.TB011_Id = usuario.TB013_CadastradoPor;
                        Log_C.TB000_IdTabela = 13;
                        Log_C.TB000_Tabela = "Pessoa";
                        Log_C.TB000_Data = DateTime.Now;
                        Log_C.TB000_Descricao = string.Format(MensagensLog.L0076, usuario.TB013_id.ToString());
                        Log_N.LogInsert(Log_C);
                    }
                }

                


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //cadastrarSenha
        public List<PessoaController> AcessosContrato(long tb012Id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.AcessosContrato(tb012Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long AcessoLiberar(PessoaController Acesso)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();

                long retorno = new PessoaDao().AcessoLiberar(Acesso);

                if (retorno>0)
                {
                    LogNegocios Log_N = new LogNegocios();
                    LogController Log_C = new LogController();

                    Log_C.TB012_Id = Acesso.TB012_Id;
                    Log_C.TB011_Id = Acesso.TB013_AlteradoPor;
                    Log_C.TB000_IdTabela = 40;
                    Log_C.TB000_Tabela = "TB040_TB013_TB012";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = string.Format(MensagensLog.L0078.ToString(), retorno.ToString(), Acesso.TB012_Id.ToString());
                    Log_N.LogInsert(Log_C);
                }
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PessoaController AcessoPessoaFiltrar(long tb013Id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.AcessoPessoaFiltrar(tb013Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool AtualizarDadosPessoaAcesso(PessoaController pessoa)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();

                LogController Log_C = new LogController();
                LogNegocios Log_N = new LogNegocios();
                Log_C.TB000_Descricao = PessoaAlteracaoParaLog(pessoa);

                Log_C.TB012_Id = pessoa.TB012_Id;
                Log_C.TB011_Id = pessoa.TB013_AlteradoPor;
                Log_C.TB000_IdTabela = 13;
                Log_C.TB000_Tabela = "Pessoa";
                Log_C.TB000_Data = DateTime.Now;

                if (DAO.AtualizarDadosPessoaAcesso(pessoa))
                {
                    if (Log_C.TB000_Descricao.Length > 5)
                    {
                        Log_N.LogInsert(Log_C);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool acessoVincular(PessoaController Acesso)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();

                LogController Log_C = new LogController();
                LogNegocios Log_N = new LogNegocios();
                Log_C.TB000_Descricao = string.Format(MensagensLog.L0078.ToString(), Acesso.TB013_id.ToString(), Acesso.TB012_Id.ToString());

                Log_C.TB012_Id = Acesso.TB012_Id;
                Log_C.TB011_Id = Acesso.TB013_AlteradoPor;
                Log_C.TB000_IdTabela = 40;
                Log_C.TB000_Tabela = "TB040_TB013_TB012";
                Log_C.TB000_Data = DateTime.Now;

                if (DAO.acessoVincular(Acesso))
                {
                    if (Log_C.TB000_Descricao.Length > 5)
                    {
                        Log_N.LogInsert(Log_C);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PessoaController> dependentesNaoAtivos(long tb012Id)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.dependentesNaoAtivos(tb012Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PessoaController> comercialFiltroEmail(long TB006_id, DateTime inicio, DateTime fim, string demaisfiltros)
        {
            try
            {
                PessoaDao DAO = new PessoaDao();
                return DAO.comercialFiltroEmail(TB006_id, inicio, fim, demaisfiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

using Controller;
using DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Negocios
{
    public class ContratoNegocios
    {

        #region Plano Familiar
        public ContratosController ContratoFamiliarInserir(ContratosController Contrato)
        {
            try
            {
                bool Numeroexistente = true;
                long numerodasorte = 0;

                while (Numeroexistente == true)
                {
                    numerodasorte = new Util().numeroDaSorteGerar();
                    Numeroexistente = new ContratoNegocios().NumeroDaSorte(numerodasorte);

                }

                LogNegocios Log_N = new LogNegocios();
                LogController Log_C = new LogController();
                /*Gerar Numero da Sorte*/
                Contrato.TB012_NumeroDaSorte = numerodasorte;
                numerodasorte = 0;


                ContratosController Retorno = new ContratoFamiliarDao().ContratoFamiliarInserir(Contrato);

                if (Retorno.TB012_Id > 0)
                {
                    Log_C.TB012_Id = Contrato.TB012_Id;
                    Log_C.TB011_Id = Retorno.TB012_CadastradorPor;
                    Log_C.TB000_IdTabela = 12;
                    Log_C.TB000_Tabela = "Contratos";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = MensagensLog.L0001.ToString().Replace("$Contrato", Contrato.TB012_Id.ToString().PadLeft(6, '0'));
                    Log_N.LogInsert(Log_C);
                }

                if(Contrato.Titular.TB013_id==0)
                {
                    Log_C.TB012_Id = Retorno.TB012_Id;
                    Log_C.TB011_Id = Retorno.TB012_CadastradorPor;
                    Log_C.TB000_IdTabela = 13;
                    Log_C.TB000_Tabela = "Pessoa";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = MensagensLog.L0002.ToString().Replace("$NomeCompleto", Retorno.Titular.TB013_NomeCompleto).Replace("$ID", Retorno.TB013_id.ToString());
                    Log_N.LogInsert(Log_C);
                }
             
                //Vincular Titular ao Contrato
                Log_C.TB012_Id = Contrato.TB012_Id;
                Log_C.TB011_Id = Retorno.TB012_CadastradorPor;
                Log_C.TB000_IdTabela = 12;
                Log_C.TB000_Tabela = "Contratos";
                Log_C.TB000_Data = DateTime.Now;
                Log_C.TB000_Descricao = MensagensLog.L0003.ToString().Replace("$NomeCompleto", Retorno.Titular.TB013_NomeCompleto).Replace("$ID", Retorno.Titular.TB013_id.ToString());
                Log_C.TB000_Descricao = Log_C.TB000_Descricao.Replace("$Contrato", Contrato.TB012_Id.ToString().PadLeft(6, '0'));
                Log_N.LogInsert(Log_C);

                return Contrato;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        public int EdicaoContrato(long tb012Id)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();
                return DAO.EdicaoContrato(tb012Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ContratosController InsertContrato(ContratosController contrato)
        {
            try
            {
                int ContezinoNovo = 0;

                LogNegocios     Log_N = new LogNegocios();
                LogController   Log_C = new LogController();

                ContratosDao        ADOContrato = new ContratosDao();
                ContratosController Contrato    = new ContratosController();
                PessoaController    Titular     = new PessoaController();
                PessoaDao           ADOPessoa   = new PessoaDao();

                if (contrato.Titular.TB013_id == 0)
                {
                    ContezinoNovo = 1;
                    Titular = ADOPessoa.PessoaInsert(contrato.Titular);
                    contrato.Titular.TB013_id = Titular.TB013_id;
                    contrato.Titular.TB013_id = contrato.Titular.TB013_id;

                    Util NovoCartao = new Util();
                    Contrato.TB012_CodCartao = NovoCartao.NumeroAleatorio();
                }
                else
                {
                    Titular = contrato.Titular;
                }
                bool Numeroexistente = true;
                long numerodasorte = 0;

                while (Numeroexistente == true)
                {
                    numerodasorte = new Util().numeroDaSorteGerar();
                    Numeroexistente = new ContratoNegocios().NumeroDaSorte(numerodasorte);

                }

                contrato.TB012_NumeroDaSorte = numerodasorte;

                Contrato                = ADOContrato.ContratoInsert(contrato);
                Contrato.Titular        = Titular;
                Log_C.TB012_Id          = Contrato.TB012_Id;
                Log_C.TB011_Id          = contrato.TB012_CadastradorPor;
                Log_C.TB000_IdTabela    = 12;
                Log_C.TB000_Tabela      = "Contratos";
                Log_C.TB000_Data        = DateTime.Now;
                Log_C.TB000_Descricao   = MensagensLog.L0001.ToString().Replace("$Contrato", Contrato.TB012_Id.ToString().PadLeft(6, '0'));
                Log_N.LogInsert(Log_C);

                if(ContezinoNovo == 1)
                {
                    Log_C.TB012_Id          = Contrato.TB012_Id;
                    Log_C.TB011_Id          = contrato.TB012_CadastradorPor;
                    Log_C.TB000_IdTabela    = 13;
                    Log_C.TB000_Tabela      = "Pessoa";
                    Log_C.TB000_Data        = DateTime.Now;
                    Log_C.TB000_Descricao   = MensagensLog.L0002.ToString().Replace("$NomeCompleto", contrato.Titular.TB013_NomeCompleto).Replace("$ID", Titular.TB013_id.ToString());
                    Log_N.LogInsert(Log_C);
                }

                //Vincular Titular ao Contrato
                ADOPessoa.VincularTitularContato(Contrato.TB012_Id, Contrato.Titular.TB013_id);

                Log_C.TB012_Id          = Contrato.TB012_Id;
                Log_C.TB011_Id          = contrato.TB012_CadastradorPor;
                Log_C.TB000_IdTabela    = 12;
                Log_C.TB000_Tabela      = "Contratos";
                Log_C.TB000_Data        = DateTime.Now;
                Log_C.TB000_Descricao   = MensagensLog.L0003.ToString().Replace("$NomeCompleto", contrato.Titular.TB013_NomeCompleto).Replace("$ID", Titular.TB013_id.ToString());
                Log_C.TB000_Descricao   = Log_C.TB000_Descricao.Replace("$Contrato", Contrato.TB012_Id.ToString().PadLeft(6, '0'));
                Log_N.LogInsert(Log_C);

                return Contrato;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ContratosController> ContratoParceirosSelect(String Query)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();
                return DAO.ContratoParceirosSelect(Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Plano Parceiro
            public ContratosController contratoParceiroInsert(ContratosController contrato)
            {
                try
                {
                    bool Numeroexistente = true;
                    long numerodasorte = 0;

                    while (Numeroexistente == true)
                    {
                        numerodasorte = new Util().numeroDaSorteGerar();
                        Numeroexistente = new ContratoNegocios().NumeroDaSorte(numerodasorte);
                    }

                    contrato.TB012_NumeroDaSorte = numerodasorte;
                    return new ContratoParceiroDAO().ContratoParceiroInsert(contrato);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            public ContratosController contratoParceiroSelect(long TB012_id)
                {
                    try
                    {
                        return new ContratoParceiroDAO().ContratoParceiroSelect(TB012_id);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            public bool ContratoParceiroAlteracao(ContratosController Contrato_C)
            {
                try
                {
                    return new ContratoParceiroDAO().ContratoParceiroAlteracao(Contrato_C);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        #endregion

        public bool contratoCodDependente(long TB012_id, double CodDependente)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();
                return DAO.ContratoCodDependente(TB012_id, CodDependente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PlanoController ContratoPlano(long TB012_id)
        {
            try
            {
                ParcelaDao DAOParcela = new ParcelaDao();

                List<ParcelaController> PrimeiraParcelaPendente = DAOParcela.ParcelasContratoPrimeiraPendente(TB012_id);

                PlanoController ContratoPlano   = new PlanoController();
                ParcelaController ObjParcela = new ParcelaController();

                ContratoPlano.TB015_id          = PrimeiraParcelaPendente[0].Plano.TB015_id;
                ContratoPlano.TB015_Plano       = PrimeiraParcelaPendente[0].Plano.TB015_Plano;
                ObjParcela.TB016_id             = PrimeiraParcelaPendente[0].TB016_id;
                ObjParcela.TB016_Valor          = PrimeiraParcelaPendente[0].TB016_Valor;
                ObjParcela.TB016_Vencimento     = PrimeiraParcelaPendente[0].TB016_Vencimento;
                ContratoPlano.Parcela           = ObjParcela;

                
                //List < ParcelaController > ContratoPlano
                //return DAO.ParcelasContratoPrimeiraPendente(TB015_id);
                return ContratoPlano;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ContratosController contratoSelect(long vID)
        {
            try
            {
                var contrato = new ContratosDao().ContratoSelect(vID);
                var pessoa = new PessoaDao().PessoaSelectId(contrato.Titular.TB013_id);

                pessoa.Contatos         = new ContatoDao().ContatosDaPessoa(contrato.Titular.TB013_id);

                contrato.Dependentes    = new PessoaDao().DependentesSelect(vID, contrato.Titular.TB013_id);

                contrato.Titular        = pessoa;

                return contrato;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool contratoAtivar(long TB012_id, long TB011_id)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();



                ///return DAO.contratoAtivar(TB012_id, TB011_id);

                if(DAO.ContratoAtivar(TB012_id, TB011_id))
                {

                    LogNegocios Log_N = new LogNegocios();
                    LogController Log_C = new LogController();

                    Log_C.TB012_Id = TB012_id;
                    Log_C.TB011_Id = TB011_id;
                    Log_C.TB000_IdTabela = 12;
                    Log_C.TB000_Tabela = "Contratos";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = MensagensLog.L0015.ToString().Replace("$Contrato", TB012_id.ToString());
                    Log_N.LogInsert(Log_C);
                    return true;
                }
                else { return false; }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public bool contratocancelar(long TB012_id, long TB011_id)
        //{
        //    try
        //    {
        //        ContratosDAO DAO = new ContratosDAO();



        //        ///return DAO.contratoAtivar(TB012_id, TB011_id);

        //        if (DAO.contratocancelar(TB012_id, TB011_id))
        //        {

        //            LogNegocios Log_N = new LogNegocios();
        //            LogController Log_C = new LogController();

        //            Log_C.TB012_Id = TB012_id;
        //            Log_C.TB011_Id = TB011_id;
        //            Log_C.TB000_IdTabela = 12;
        //            Log_C.TB000_Tabela = "Contratos";
        //            Log_C.TB000_Data = DateTime.Now;
        //            Log_C.TB000_Descricao = MensagensLog.L0059.ToString().Replace("$Contrato", TB012_id.ToString());
        //            Log_N.LogInsert(Log_C);
        //            return true;
        //        }
        //        else { return false; }


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}




        //public bool contratoInativar(long TB012_id, int TB012_ContratoCancelarMotivo, string TB012_ContratoCancelarDescricao, long TB011_id)
        //{
        //    try
        //    {
        //        ContratosDAO DAO = new ContratosDAO();
        //        ParcelaDAO Parcela_D = new ParcelaDAO();
        //        Parcela_D.ParcelasCancelas(TB012_id);

        //        //if(DAO.contratoInativar(TB012_id, TB012_ContratoCancelarMotivo, TB012_ContratoCancelarDescricao, TB011_id))
        //        //{
        //        //    LogNegocios Log_N = new LogNegocios();
        //        //    LogController Log_C = new LogController();

        //        //    Log_C.TB012_Id = TB012_id;
        //        //    Log_C.TB011_Id = TB011_id;
        //        //    Log_C.TB000_IdTabela = 12;
        //        //    Log_C.TB000_Tabela = "Contratos";
        //        //    Log_C.TB000_Data = DateTime.Now;
        //        //    Log_C.TB000_Descricao = MensagensLog.L0008.ToString().Replace("$Contrato", TB012_id.ToString());
        //        //    Log_N.LogInsert(Log_C);

        //        //    return true;
        //        //}
        //        //else
        //        //{
        //        //    return false;
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public Int64 AlteracaoContratoGerarDocumento(long TB012_id, long TB011_Id)
        //{
        //    Int64 Retorno = 0;
        //    try
        //    {
        //        ContratosDAO DAO = new ContratosDAO();
        //        List<ContratosController> DadosRelatorio = DAO.AlteracaoContratoRecuperarIncluidosCancelados(TB012_id);

           
        //        if(DadosRelatorio.Count>1)
        //        { 
        //            /*Criando documento*/
        //            StringBuilder HTML = new StringBuilder();
        //            HTML.Append("<html xmlns='http://www.w3.org/1999/xhtml'><head>");
        //            HTML.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>");
        //            HTML.Append("<title>Alteração de Contrato</title>");
        //            HTML.Append("<style type='text/css'>");
        //            HTML.Append(".auto-style1{width:27cm;");
        //            HTML.Append("}");
        //            HTML.Append(".auto-style2{text-align:left;");
        //            HTML.Append("}");
        //            HTML.Append(".auto-style3{text-decoration:underline;");
        //            HTML.Append("}");
        //            HTML.Append(".auto-style4{text-align:center;");
        //            HTML.Append("}");
        //            HTML.Append(".auto-style5{");
        //            HTML.Append("width:611px;");
        //            HTML.Append("}");
        //            HTML.Append(".auto-style6 {");
        //            HTML.Append("width:4608px;");
        //            HTML.Append("}");
        //            HTML.Append(".auto-style7{");
        //            HTML.Append("width:195px;");
        //            HTML.Append("}");
        //            HTML.Append("</style>");
        //            HTML.Append("</head>");
        //            HTML.Append("<body style='width: 1022px'>");
        //            HTML.Append("<div id='divGeral' style='border: 1px solid #000000; text-align: center;' class='auto-style1'>");
        //            HTML.Append("<img alt=''src='http://www.clubeconteza.com.br/img/sis/LogoRPT.png' /><br/>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<div id='DivCabecalho' style='background-color:#C0C0C0'>");
        //            HTML.Append("<strong>TERMO DE RESPONSABILIDADE");
        //            HTML.Append("<br/>");
        //            HTML.Append("ALTERAÇÃO DE CARNÊ DE MENSALIDADE DO CLUBE</strong>");
        //            HTML.Append("</div>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<div id ='divTexto01' lass='auto-style2'>");
        //            HTML.Append("Eu, ");
        //            HTML.Append(DadosRelatorio[0].Titular.TB013_NomeCompleto);
        //            HTML.Append(", CPF N.º ");
        //            HTML.Append(DadosRelatorio[0].Titular.TB013_CPFCNPJ);
        //            HTML.Append(", RG N.º ");
        //            HTML.Append(DadosRelatorio[0].Titular.TB013_RG);
        //            HTML.Append(" emitido por ");
        //            HTML.Append(DadosRelatorio[0].Titular.TB013_RGOrgaoEmissor);
        //            HTML.Append(" , titular do plano do Clube Conteza, abaixo assinado, declaro que estou ciente das informações elencadas:");
        //            HTML.Append("</div>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<div id = 'divTexto02' class='auto-style2'>");
        //            HTML.Append("1. Foi emitido em meu nome, carnê com todos as parcelas referente ao meu plano do Clube Conteza, no valor mensal de R$ ");
        //            HTML.Append(DadosRelatorio[0].PlanoAnterior.ValorTotalProdutos);
        //            HTML.Append(".");
        //            HTML.Append("</div>");
        //            HTML.Append("<div id='divTexto03' class='auto-style2'>");
        //            HTML.Append("2. Que eu desejo alterar meu plano, do seguinte modo: ");
        //            HTML.Append("</div>");
        //            HTML.Append("<div id='divTexto04'>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<div id='divTexto00'>");               
        //            HTML.Append("<table style='width:100%;'>");
        //            HTML.Append("<tr>");
        //            HTML.Append("<td class='auto-style7'>Id</td>");
        //            HTML.Append("<td class='auto-style6'>Dependente</td>");
        //            HTML.Append("<td class='auto-style5'>Data Nascimento</td>");
        //            HTML.Append("<td>Alteração</td>");
        //            HTML.Append("</tr>");
        //                foreach (ContratosController RetornoContrato in DadosRelatorio)
        //                {
        //                if(RetornoContrato.Dependente.TB013_NomeCompleto.TrimEnd() != DadosRelatorio[0].Titular.TB013_NomeCompleto.TrimEnd())
        //                { 
        //                    HTML.Append("<tr>");
        //                        HTML.Append("<td class='auto-style7'>");
        //                        HTML.Append(RetornoContrato.Dependente.TB013_id.ToString());
        //                        HTML.Append("</td>");
        //                        HTML.Append("<td class='auto-style6'>");
        //                        HTML.Append(RetornoContrato.Dependente.TB013_NomeCompleto);
        //                        HTML.Append("</td>");
        //                        HTML.Append("<td class='auto-style5'>");
        //                        HTML.Append(RetornoContrato.Dependente.TB013_DataNascimento.ToString("dd/MM/yyyy"));
        //                        HTML.Append("</td>");
        //                        HTML.Append("<td>");
        //                        if(RetornoContrato.Dependente.TB013_StatusS=="0")
        //                        {
        //                            HTML.Append("INCLUIR");
        //                        }
        //                        else
        //                        {
        //                            if (RetornoContrato.Dependente.TB013_StatusS == "3")
        //                            {
        //                                HTML.Append("CANCELAR");
        //                            }
        //                            else
        //                            {
        //                                HTML.Append("NÃO ALTERAR");
        //                            }
        //                        }
                        
        //                        HTML.Append("</td>");
        //                    HTML.Append("</tr>");
        //                }
        //            }
        //            HTML.Append("</table>");
        //            HTML.Append("</div>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<div id='divTexto05' class='auto-style2'>");
        //            HTML.Append("Deste modo, alterando o valor descrito no item 1 para o valor de R$ ");
        //            HTML.Append(DadosRelatorio[0].Plano.ValorTotalProdutos);
        //            HTML.Append(".");
        //            HTML.Append("</div>");
        //            HTML.Append("<div id='divTexto06' class='auto-style2'>");
        //            HTML.Append("3. Com a alteração que solicitei, fico ciente de que o carnê enviado anteriormente para mim tornase, a partir da data da assinatura do presente termo, <span class='auto -style3'><strong>sem validade, ou seja, não devo pagar nenhum boleto contino naquele carnê</strong></span>.");
        //            HTML.Append("</div>");
        //            HTML.Append("<div id='divTexto07' class='auto-style2'>");
        //            HTML.Append("4. Fico ciente que algumas instituições financeiras aceitam o pagamento de boletos cancelados, sendo de minha responsabilidade o pagamento do documento correto e vigente.");
        //            HTML.Append("</div>");
        //            HTML.Append("<div id = 'divTexto08' class='auto-style2'>");
        //            HTML.Append("5. Em caso de pagamento do carnê cancelado, as conseguências deverão ser arcadas por mim, devendo buscar eventuais restituições de valores diretamente com a instituiçã financeira na qual o pagamento foi efetuado.");
        //            HTML.Append("</div>");
        //            HTML.Append("<div id = 'divTexto09' class='auto-style2'>");
        //            HTML.Append("6. O pagamento de boletos cancelados não exime, de qualquer maneira, a responsabilidade do pagamento dos boletos vigentes pelo Clube Conteza.");
        //            HTML.Append("</div>");
        //            HTML.Append("<div id = 'divTexto10' class='auto-style2'>");
        //            HTML.Append("7. O Clube Conteza fez o cancelamento do carnê solicitado por mim, sendo responsável apenas pelo novo carnê emitido.");
        //            HTML.Append("</div>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<div id = 'divTexto11' class='auto-style2'>");
        //            HTML.Append("Diante do exposto, declaro a responsabilidade por todos as informações acima elencadas, assinando o presente termo de ciência em 02 (duas) vias.");
        //            HTML.Append("</div>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<div id = 'divTexto12' class='auto-style4'>");
        //            HTML.Append("Guarapuava,");
        //            HTML.Append(DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo("pt-BR")));
        //            HTML.Append(".");
        //            HTML.Append("</div>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<br/>");
        //            HTML.Append("<div id='divTexto13' class='auto-style4'>");
        //            HTML.Append("______________________________________<br />");
        //            HTML.Append("Assinatura do titular do plano");
        //            HTML.Append("</div>");
        //            HTML.Append("</div>");
        //            HTML.Append("</body>");
        //            HTML.Append("</html>");

        //            /*Salvar relatorio no banco*/
        //            ContratosController Documento = new ContratosController();
        //            Documento.TB012_id = TB012_id;
        //            Documento.TB026_Relatorio = HTML.ToString();
        //            Documento.TB026_CadastradoPor = TB011_Id;
                    
        //            Retorno = DAO.contratorelatorioalteracaoinsert(Documento);


        //            /*Apos cadastro do relatorio no banco, retornar para tela*/
        //        }
        //        return Retorno;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<ContratosController> RelatoriosDocumentosContrato(long TB012_id)
        //{
        //    try
        //    {
        //        ContratosDAO DAO = new ContratosDAO();
        //        return DAO.RelatoriosDocumentosContrato(TB012_id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public string ContratoDocumentoSelect(long TB026_id)
        //{
        //    try
        //    {
        //        ContratosDAO DAO = new ContratosDAO();
        //        return DAO.ContratoDocumentoSelect(TB026_id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public List<ContratosController> ContratoCodCartaoUtilizados()
        {
            try
            {
                ContratosDao DAO = new ContratosDao();
                return DAO.ContratoCodCartaoUtilizados();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<UnidadeController> ContratosCorporativoSelect(String Query)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();
                return DAO.ContratosCorporativoSelect(Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ContratosController ContratoCorporativoSelect(Int64 TB012_id, int TB020_Matriz)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();
                return DAO.ContratoCorporativoSelect(TB012_id, TB020_Matriz);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

        public bool ContratoCorporativoUpdade(ContratosController ContratoCorporativo, Int64  TB011_id)
        {
            try
            {
                LogNegocios     Log_N = new LogNegocios();
                LogController   Log_C = new LogController();

                ContratosDao DAO = new ContratosDao();

                /*Atualizar dados do Contrato*/
                if (DAO.ContratoCorporativoUpdade(ContratoCorporativo, TB011_id))
                {
                    Log_C.TB012_Id          = ContratoCorporativo.TB012_Id;
                    Log_C.TB011_Id          = TB011_id;
                    Log_C.TB000_IdTabela    = 12;
                    Log_C.TB000_Tabela      = "Contratos";
                    Log_C.TB000_Data        = DateTime.Now;
                    Log_C.TB000_Descricao   = MensagensLog.L0025.ToString().Replace("$Contrato ", ContratoCorporativo.TB012_Id.ToString());
                    Log_N.LogInsert(Log_C);

                    /*Atualizar dados da Unidade*/
                    UnidadeNegocios unidade_N = new UnidadeNegocios();

                    UnidadeController UnidadeRetorno = unidade_N.UnidadeAtualizarCorporativo(ContratoCorporativo.Unidade);
                    if (UnidadeRetorno.TB020_id > 0)
                    {
                        Log_C.TB000_IdTabela = 20;
                        Log_C.TB000_Tabela = "Unidade";
                        Log_C.TB000_Data = DateTime.Now;
                        Log_C.TB000_Descricao = MensagensLog.L0026.ToString().Replace("$Unidade ", UnidadeRetorno.TB020_id.ToString());
                        Log_N.LogInsert(Log_C);

                        /*Atualizar dados do Titular*/
                        PessoaNegocios Titular_N = new PessoaNegocios();
                        if (Titular_N.PessoaUpdate(ContratoCorporativo.Titular, ContratoCorporativo.TB012_Id))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return true;
        }

        //public bool ContratoAlterarPontoDeVenda(long TB012_id, long TB002_id, long TB011_id)
        //{
        //    try
        //    {
        //        ContratosDao DAO = new ContratosDao();
        //       // return DAO.ContratoAlterarPontoDeVenda(TB012_id, TB002_id);

        //        if(DAO.ContratoAlterarPontoDeVenda(TB012_id, TB002_id))
        //        {

        //            LogNegocios Log_N = new LogNegocios();
        //            LogController Log_C = new LogController();

        //            Log_C.TB012_Id = TB012_id;
        //            Log_C.TB011_Id = TB011_id;
        //            Log_C.TB000_IdTabela = 12;
        //            Log_C.TB000_Tabela = "Contrato";
        //            Log_C.TB000_Data = DateTime.Now;
        //            Log_C.TB000_Descricao = string.Format(MensagensLog.L0031.ToString(), TB002_id.ToString());
        //            Log_N.LogInsert(Log_C);

        //            //ContratosDAO Contrato_D = new ContratosDAO();
        //            //if(Contrato_D.ContratoInformarEdicao(TB012_id, TB011_id))
        //            //{
        //            //    Log_C.TB000_Data = DateTime.Now;
        //            //    Log_C.TB000_Descricao = string.Format(MensagensLog.L0035.ToString(), TB002_id.ToString());
        //            //    Log_N.LogInsert(Log_C);
        //            //}

        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool ParcelasCancelar(long TB012_id, long TB011_id)
        //{
        //    try
        //    {              
        //        ParcelaDAO Parcela_D = new ParcelaDAO();
        //        //Parcela_D.ParcelasCancelas(TB012_id);

        //        if (Parcela_D.ParcelasCancelas(TB012_id))
        //        {
        //            LogNegocios Log_N = new LogNegocios();
        //            LogController Log_C = new LogController();

        //            Log_C.TB012_Id = TB012_id;
        //            Log_C.TB011_Id = TB011_id;
        //            Log_C.TB000_IdTabela = 12;
        //            Log_C.TB000_Tabela = "Contratos";
        //            Log_C.TB000_Data = DateTime.Now;
        //            Log_C.TB000_Descricao = MensagensLog.L0027.ToString().Replace("$Contrato", TB012_id.ToString());
        //            Log_N.LogInsert(Log_C);                  
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public Int64 ContratoColaboradorInsert(ContratosController contrato)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();
                //Log_C.TB000_Descricao = string.Format(MensagensLog.L0023.ToString(), Cartao, EntreguePara);
                Int64 Retorno = DAO.ContratoColaboradorInsert(contrato);


                if(Retorno>0)
                {
                    LogNegocios Log_N = new LogNegocios();
                    LogController Log_C = new LogController();

                    Log_C.TB012_Id = Retorno;
                    Log_C.TB011_Id = contrato.TB026_CadastradoPor;
                    Log_C.TB000_IdTabela = 12;
                    Log_C.TB000_Tabela = "Contrato";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = string.Format(MensagensLog.L0029.ToString(), Retorno.ToString(), contrato.TB012_Corporativo.ToString());
                    Log_N.LogInsert(Log_C);

                }
                return Retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public bool ContratoVerificaEdicao(long TB012_id)
        //{
        //    try
        //    {
        //        ContratosDAO DAO = new ContratosDAO();
        //        return DAO.ContratoVerificaEdicao(TB012_id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public short ContratoVSAtual(long TB012_id)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();
                return DAO.ContratoVsAtual(TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContratosController> ListarParcelasParaRenovacao(Int32 TB012_TipoContrato, Int32 TB012_CicloContrato, Int32 Top)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();
                return DAO.ListarParcelasParaRenovacao(TB012_TipoContrato, TB012_CicloContrato, Top);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CicloContratoAtualizar(long TB012_id, Int32 TB012_CicloContrato, long TB011_Id, DateTime TB012_Fim)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();
                return DAO.CicloContratoAtualizar(TB012_id, TB012_CicloContrato, TB011_Id, TB012_Fim);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContratosController> ListarParcelasParaExportacao(Int32 TB012_TipoContrato, Int32 TB012_CicloContrato, Int32 Top)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();
                return DAO.ListarParcelasParaExportacao(TB012_TipoContrato, TB012_CicloContrato, Top);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public bool AlterarValidadeContrato(long TB012_id, DateTime TB012_Inicio, DateTime TB012_Fim, long TB011_Id, Int32 TB012_CicloContrato)
        //{
        //    try
        //    {
        //        ContratosDAO DAO = new ContratosDAO();


        //        if (DAO.AlterarValidadeContrato(TB012_id, TB012_Inicio, TB012_Fim, TB011_Id, TB012_CicloContrato))
        //        {

        //            LogNegocios Log_N = new LogNegocios();
        //            LogController Log_C = new LogController();

        //            Log_C.TB012_Id = TB012_id;
        //            Log_C.TB011_Id = TB011_Id;
        //            Log_C.TB000_IdTabela = 12;
        //            Log_C.TB000_Tabela = "Contratos";
        //            Log_C.TB000_Data = DateTime.Now;
        //            Log_C.TB000_Descricao = MensagensLog.L0050.ToString().Replace("$Contrato", TB012_id.ToString());
        //            Log_N.LogInsert(Log_C);
        //            return true;
        //        }
        //        else { return false; }


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public bool CorporativoCancelarColaborador(long TB012_id, long TB011_Id, double ValorUnitario, long Corporativo)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();


                if (DAO.CorporativoCancelarColaborador(TB012_id, TB011_Id, ValorUnitario, Corporativo))
                {
                    LogNegocios Log_N = new LogNegocios();
                    LogController Log_C = new LogController();

                    Log_C.TB012_Id = TB012_id;
                    Log_C.TB011_Id = TB011_Id;
                    Log_C.TB000_IdTabela = 12;
                    Log_C.TB000_Tabela = "Contratos";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = MensagensLog.L0053;
                    Log_N.LogInsert(Log_C);
                    return true;
                }
                else { return false; }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long ContadorContratoCorporativoFamiliar(long TB012_id)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();             
                return DAO.ContadorContratoCorporativoFamiliar(TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long ContadorCicloAtual(long TB012_id)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();
                return DAO.ContadorCicloAtual(TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean ContratoCoprorativoAoTitularFamiliar(Int64 TB012_id, Int64 TB013_id)
        {
            try
            {
                ContratosDao DAO = new ContratosDao();
                return DAO.ContratoCoprorativoAoTitularFamiliar(TB012_id, TB013_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool FamiliarCancelarContrato(long tb012Id, long tb011Id, string descricao, string tb012ContratoCancelarMotivoS)
        {
            try
            {
                if (!new ContratoFamiliarDao().FamiliarCancelarContrato(tb012Id, tb011Id, descricao, tb012ContratoCancelarMotivoS)) return false;
                var logC = new LogController
                {
                    TB012_Id = tb012Id,
                    TB011_Id = tb011Id,
                    TB000_IdTabela = 12,
                    TB000_Tabela = "Contratos",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = string.Format(MensagensLog.L0058, tb012Id, tb012ContratoCancelarMotivoS, descricao)
                };

                new LogNegocios().LogInsert(logC);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ContratosController> ContratosFinanceiro(string query)
        {
            try
            {
                return new ContratosDao().ContratosFinanceiro(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CicloContratoAlterar(long tb012Id, Int32 tb012CicloContrato, long tb011Id)
        {
            try
            {
                if (!new ContratosDao().CicloContratoAlterar(tb012Id, tb012CicloContrato, tb011Id)) return false;
                var logC = new LogController
                {
                    TB012_Id = tb012Id,
                    TB011_Id = tb011Id,
                    TB011_Ref = 0,
                    TB000_IdTabela = 12,
                    TB000_Tabela = @"Contrato",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = string.Format(MensagensLog.L0064, tb012Id)
                };

                new LogNegocios().LogInsert(logC);
                return true;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public bool ContratoAlterarInicioFim(long tb012Id, DateTime inicio, DateTime fim, long tb011Id)
        {
            try
            {
                if (!new ContratosDao().ContratoAlterarInicioFim(tb012Id, inicio, fim, tb011Id)) return false;
                var logC = new LogController
                {
                    TB012_Id = tb012Id,
                    TB011_Id = tb011Id,
                    TB011_Ref = 0,
                    TB000_IdTabela = 12,
                    TB000_Tabela = @"Contrato",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = string.Format(MensagensLog.L0065, inicio, fim)
                };

                new LogNegocios().LogInsert(logC);
                return true;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public bool ContratoNegociado(long tb012Id, long tb011Id,long TB037_Id)
        {
            try
            {
                AnotacoesController Log_C = new AnotacoesController();
                Log_C.Tb012Id = tb012Id;
                Log_C.TB026_Negociacao = 1;
                Log_C.Tb011Id = tb011Id;
                Log_C.Tb026Data = DateTime.Now;
                Log_C.Tb026Cod = "L0073";

                Log_C.Tb026Anotacao = string.Format(MensagensLog.L0073.ToString(), tb012Id.ToString());
                new AnotacoesDao().Anotacaoinsert(Log_C);

                new ParcelaNegocios().ParcelasVencidas(tb012Id);
                return new ContratosDao().ContratoNegociado(tb012Id, tb011Id, TB037_Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public bool NumeroDaSorte(long numero)
        {
            try
            {
                return new ContratosDao().NumeroDaSorte(numero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool finalizarProcessoEdicaoContrato(long tb012Id, long tb011Id)
        {
            try
            {
                LogController Log_C = new LogController();

                if (new ContratoFamiliarDao().finalizarProcessoEdicaoContrato(tb012Id, tb011Id))
                    Log_C.TB012_Id = tb012Id;
                Log_C.TB011_Id = tb011Id;
                Log_C.TB000_IdTabela = 9;
                Log_C.TB000_Tabela = "Contratos";
                Log_C.TB000_Data = DateTime.Now;
                //Log_C.TB000_Descricao = MensagensLog.L0006.ToString().Replace("$Contato", contato.TB009_Contato.Trim()).Replace("$ID", contato.TB009_id.ToString());
                Log_C.TB000_Descricao = string.Format(MensagensLog.L0079.ToString(), tb012Id.ToString(), tb011Id.ToString());
                new LogNegocios().LogInsert(Log_C);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public bool familiarReativarContrato(ContratosController contrato)
        {
            try
            {
                if (!new ContratoFamiliarDao().familiarReativarContrato(contrato)) return false;
                var logC = new LogController
                {
                    TB012_Id = contrato.TB012_Id,
                    TB011_Id = contrato.TB012_AlteradoPor,
                    TB000_IdTabela = 12,
                    TB000_Tabela = "Contratos",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = string.Format(MensagensLog.L0080, contrato.TB012_Id.ToString())
                };

                new LogNegocios().LogInsert(logC);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContratosController> corporativoListaParaExportacao(string query)
        {
            try
            {
                return new ContratoCorporativoDAO().corporativoListaParaExportacao(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool corporativoArquivoExportar()
        {
            try
            {

                if (Directory.Exists(@"c:\Temp"))
                {
                    var dir = new DirectoryInfo(@"c:\Temp");

                    foreach (var fi in dir.GetFiles())
                    {
                        fi.Delete();
                    }
                }


                var empresas                = new ContratoCorporativoDAO().corporativoArquivoEmpresa();
                var empresascontato         = new ContatoDao().contatosCorporativoExportar(); 
                var familiarcontato         = new ContatoDao().contatosFamiliarCorporativoExportar();
                var familiarcorporativo     = new PessoaDao().corporativoPessoas();

                var _arquivo1 = File.AppendText(@"C:\Temp\" + "CorporativoEmpresa.txt");
                var _arquivo2 = File.AppendText(@"C:\Temp\" + "CorporativoContatos.txt");
                var _arquivo3 = File.AppendText(@"C:\Temp\" + "CorporativoFamiliar.txt");

                #region Corporativo
                /*Gerar arquivo de empresas corporativo*/
                if (empresas.Count>0)
                {
                    var titulo = new StringBuilder();
                    titulo.Append(@"Contrato".ToString().PadLeft(11, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Inicio".ToString().PadLeft(11, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Valido".ToString().PadLeft(11, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Ciclo".ToString().PadLeft(7, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Vencimento".ToString().PadLeft(15, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Status".ToString().PadLeft(15, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Unidade ID".ToString().PadLeft(14, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Unidade Razão Social".ToString().PadLeft(300, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Unidade Nome Fantasia".ToString().PadLeft(300, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Unidade CNPJ".ToString().PadLeft(25, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Unidade CEP".ToString().PadLeft(12, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Unidade Logradouro".ToString().PadLeft(300, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Unidade N.º".ToString().PadLeft(11, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Unidade Bairro".ToString().PadLeft(15, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Unidade Status".ToString().PadLeft(15, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Municipio Id".ToString().PadLeft(15, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Municipio".ToString().PadLeft(200, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Municipio IBGE".ToString().PadLeft(15, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Estado Id".ToString().PadLeft(15, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Estado Sigla".ToString().PadLeft(15, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Estado".ToString().PadLeft(200, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Estado Código".ToString().PadLeft(15, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Pais Id".ToString().PadLeft(15, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Pais DDI".ToString().PadLeft(15, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Pais".ToString().PadLeft(100, ' '));
                    titulo.Append("#");
                    _arquivo1.WriteLine(titulo);


                    foreach (ContratosController empresa in empresas)
                    {

                        var linha_arquivo1 = new StringBuilder();

                        linha_arquivo1.Append(empresa.TB012_Id.ToString().PadLeft(11, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.TB012_Inicio.ToString("dd/MM/yyyy").PadLeft(11, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.TB012_Fim.ToString("dd/MM/yyyy").PadLeft(11, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.TB012_CicloContrato.ToString().PadLeft(7, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.TB012_DiaVencimento.ToString().PadLeft(15, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.TB012_StatusS.ToString().PadLeft(15, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Unidade.TB020_id.ToString().PadLeft(14, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Unidade.TB020_RazaoSocial.ToString().PadLeft(300, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Unidade.TB020_NomeFantasia.ToString().PadLeft(300, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Unidade.TB020_Documento.ToString().PadLeft(25, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Unidade.TB020_Cep.ToString().PadLeft(12, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Unidade.TB020_Logradouro.ToString().PadLeft(300, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Unidade.TB020_Numero.ToString().PadLeft(11, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Unidade.TB020_Bairro.ToString().PadLeft(15, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Unidade.TB020_StatusS.ToString().PadLeft(15, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Municipio.TB006_id.ToString().PadLeft(15, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Municipio.TB006_Municipio.ToString().PadLeft(200, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Municipio.TB006_IBGE.ToString().PadLeft(15, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Estado.TB005_Id.ToString().PadLeft(15, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Estado.TB005_Sigla.ToString().PadLeft(15, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Estado.TB005_Estado.ToString().PadLeft(200, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Estado.TB005_Codigo.ToString().PadLeft(15, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Pais.TB003_id.ToString().PadLeft(15, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Pais.TB003_DDI.ToString().PadLeft(15, ' '));
                        linha_arquivo1.Append("#");
                        linha_arquivo1.Append(empresa.Pais.TB003_Pais.ToString().PadLeft(100, ' '));
                        linha_arquivo1.Append("#");


                        _arquivo1.WriteLine(linha_arquivo1);
                    }
                    _arquivo1.Close();
                }

                #endregion

                #region Contato
                /*Incluir contatos da empresa*/
                if(empresascontato.Count>0)
                {
                    var titulo = new StringBuilder();
                    titulo.Append(@"Id".ToString().PadLeft(11, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Tipo".ToString().PadLeft(10, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Unidade".ToString().PadLeft(11, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Pessoa".ToString().PadLeft(11, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Contrato".ToString().PadLeft(11, ' '));
                    titulo.Append("#");
                    titulo.Append(@"Contato".ToString().PadLeft(100, ' '));
                    titulo.Append("#");

                    _arquivo2.WriteLine(titulo);

                    foreach (ContatoController contato in empresascontato)
                    {
                        var linha_arquivo2 = new StringBuilder();

                        linha_arquivo2.Append(contato.TB009_id.ToString().PadLeft(11, ' '));
                        linha_arquivo2.Append("#");
                        linha_arquivo2.Append(contato.TB009_TipoS.ToString().PadLeft(10, ' '));
                        linha_arquivo2.Append("#");
                        linha_arquivo2.Append(contato.TB020_id.ToString().PadLeft(11, ' '));
                        linha_arquivo2.Append("#");
                        linha_arquivo2.Append(contato.TB013_id.ToString().PadLeft(11, ' '));
                        linha_arquivo2.Append("#");
                        linha_arquivo2.Append(contato.TB012_id.ToString().PadLeft(11, ' '));
                        linha_arquivo2.Append("#");
                        linha_arquivo2.Append(contato.TB009_Contato.ToString().PadLeft(100, ' '));
                        linha_arquivo2.Append("#");


                        _arquivo2.WriteLine(linha_arquivo2);
                    }
                }

                /*Incluir contatos do plano familiar*/
                if (familiarcontato.Count > 0)
                {
                 
                    foreach (ContatoController contato in familiarcontato)
                    {
                        var linha_arquivo2 = new StringBuilder();

                        linha_arquivo2.Append(contato.TB009_id.ToString().PadLeft(11, ' '));
                        linha_arquivo2.Append("#");
                        linha_arquivo2.Append(contato.TB009_TipoS.ToString().PadLeft(10, ' '));
                        linha_arquivo2.Append("#");
                        linha_arquivo2.Append(contato.TB020_id.ToString().PadLeft(11, ' '));
                        linha_arquivo2.Append("#");
                        linha_arquivo2.Append(contato.TB013_id.ToString().PadLeft(11, ' '));
                        linha_arquivo2.Append("#");
                        linha_arquivo2.Append(contato.TB012_id.ToString().PadLeft(11, ' '));
                        linha_arquivo2.Append("#");
                        linha_arquivo2.Append(contato.TB009_Contato.ToString().PadLeft(100, ' '));
                        linha_arquivo2.Append("#");

                        _arquivo2.WriteLine(linha_arquivo2);
                    }

                  
                }
                _arquivo2.Close();
                #endregion

                #region Familiar Corporativo
                    if (familiarcorporativo.Count > 0)
                    {
                        var titulo = new StringBuilder();
                        titulo.Append(@"Contrato".ToString().PadLeft(11, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Corporativo".ToString().PadLeft(20, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Inicio".ToString().PadLeft(15, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Fim".ToString().PadLeft(15, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Contrato Status".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Id".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Cartão".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Cod.".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Tipo.".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa CPF".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Lista Negra".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Nome Completo".ToString().PadLeft(300, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Nome Exibição".ToString().PadLeft(300, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Sexo".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa RG".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Org. Emissor".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Data Nascimento".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa CEP".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Logradouro".ToString().PadLeft(300, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa N.º".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Bairro".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Complemento".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Status".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Matricula".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Mãe".ToString().PadLeft(300, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Mãe Data Nascimento".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Pai".ToString().PadLeft(300, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Pai Data Nascimento".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Cartão Corporativo".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Cartão Corporativo Status".ToString().PadLeft(50, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Cartão Corporativo Solicitado".ToString().PadLeft(50, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Cidade Id".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Cidade Cod.".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Cidade IBGE.".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Cidade".ToString().PadLeft(300, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Estado Id".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Estado Cód.".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Estado UF".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Estado".ToString().PadLeft(300, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Pais Id".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Pais DDI".ToString().PadLeft(30, ' '));
                        titulo.Append("#");
                        titulo.Append(@"Pessoa Pais".ToString().PadLeft(300, ' '));
                        titulo.Append("#");


                    _arquivo3.WriteLine(titulo);

                        foreach (PessoaController pessoa in familiarcorporativo)
                        {
                        var linha_arquivo3 = new StringBuilder();

                            linha_arquivo3.Append(pessoa.Contrato.TB012_Id.ToString().PadLeft(11, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.Contrato.TB012_Corporativo.ToString().PadLeft(20, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(Convert.ToDateTime(pessoa.Contrato.TB012_Inicio).ToString("dd/MM/yyyy").PadLeft(15, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(Convert.ToDateTime(pessoa.Contrato.TB012_Fim).ToString("dd/MM/yyyy").PadLeft(15, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.Contrato.TB012_StatusS.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_id.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_Cartao.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_CodigoDependente.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            if(Convert.ToInt16(pessoa.TB013_CodigoDependente)==1001)
                            {
                                linha_arquivo3.Append("Titular".ToString().PadLeft(30, ' '));
                                linha_arquivo3.Append("#");
                            }
                            else
                            {
                                linha_arquivo3.Append("Dependente".ToString().PadLeft(30, ' '));
                                linha_arquivo3.Append("#");
                            }

                            linha_arquivo3.Append(pessoa.TB013_CPFCNPJ.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_ListaNegra.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_NomeCompleto.ToString().PadLeft(300, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_NomeExibicao.ToString().PadLeft(300, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_SexoS.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_RG.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_RGOrgaoEmissor.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(Convert.ToDateTime(pessoa.TB013_DataNascimento).ToString("dd/MM/yyyy").PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB004_Cep.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_Logradouro.ToString().PadLeft(300, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_Numero.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_Bairro.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_Complemento.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_StatusS.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_Matricula.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_MaeNome.ToString().PadLeft(300, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(Convert.ToDateTime(pessoa.TB013_MaeDataNascimento).ToString("dd/MM/yyyy").PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_PaiNome.ToString().PadLeft(300, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(Convert.ToDateTime(pessoa.TB013_PaiDataNascimento).ToString("dd/MM/yyyy").PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_CartaoChip.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_CartaoChipStatusS.ToString().PadLeft(50, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.TB013_CartaoSolicitado.ToString().PadLeft(50, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.Municipio.TB006_id.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.Municipio.TB006_Codigo.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.Municipio.TB006_IBGE.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.Municipio.TB006_Municipio.ToString().PadLeft(300, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.Estado.TB005_Id.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.Estado.TB005_Codigo.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.Estado.TB005_Sigla.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.Estado.TB005_Estado.ToString().PadLeft(300, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.Pais.TB003_id.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.Pais.TB003_DDI.ToString().PadLeft(30, ' '));
                            linha_arquivo3.Append("#");
                            linha_arquivo3.Append(pessoa.Pais.TB003_Pais.ToString().PadLeft(300, ' '));
                            linha_arquivo3.Append("#");

                        _arquivo3.WriteLine(linha_arquivo3);
                    }

                    _arquivo3.Close();
                }
                #endregion


                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool ContratoAlterarLocalCadastro(long tb012Id, long TB002_id, long tb011Id)
        {
            try
            {
                if (!new ContratosDao().ContratoAlterarLocalCadastro(tb012Id, TB002_id, tb011Id)) return false;
                var logC = new LogController
                {
                    TB012_Id = tb012Id,
                    TB011_Id = tb011Id,
                    TB000_IdTabela = 12,
                    TB000_Tabela = "Contratos",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = string.Format(MensagensLog.L0068, TB002_id.ToString())
                };

                new LogNegocios().LogInsert(logC);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

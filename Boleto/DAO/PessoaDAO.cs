using Controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Boleto.DAO
{
    public class PessoaDAO
    {

        public Int16 RecuperarCartaoLote()
        {
            Int16 Retorno = 0;
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append("SELECT MAX(TB013_CartaoLote) AS TB013_CartaoLote FROM dbo.TB013_Pessoa");


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno = Convert.ToInt16(reader["TB013_CartaoLote"]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        /// <summary>
        /// Descrição:  Listar cartoes para impressão
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       07/04/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        /// 
        public List<PessoaController> CartoesParaImpressao(Int16 CartaoLote, Int16 TB012_TipoContrato)
        {
            List<PessoaController> RetornoList = new List<PessoaController>();
            try
            {

                var temp = new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString);

                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" SELECT  dbo.TB013_Pessoa.TB012_id, dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_Cartao, dbo.TB013_Pessoa.TB013_CarteirinhaStatus, dbo.TB013_Pessoa.TB013_NomeCompleto,  ");
                sSql.Append(" ISNULL(dbo.View_Celulares.TB009_Contato, 'SEM CELULAR') AS TB009_Contato, dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB002_PontosDeVenda.TB002_id, dbo.TB002_PontosDeVenda.TB002_Ponto  ");
                sSql.Append(" FROM dbo.TB013_Pessoa INNER JOIN  ");
                sSql.Append(" dbo.TB012_Contratos ON dbo.TB013_Pessoa.TB012_id = dbo.TB012_Contratos.TB012_id INNER JOIN  ");
                sSql.Append(" dbo.TB002_PontosDeVenda ON dbo.TB012_Contratos.TB002_id = dbo.TB002_PontosDeVenda.TB002_id LEFT OUTER JOIN  ");
                sSql.Append(" dbo.View_Celulares ON dbo.TB013_Pessoa.TB013_id = dbo.View_Celulares.TB013_id  ");
                sSql.Append(" GROUP BY dbo.TB013_Pessoa.TB012_id, dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_Cartao, dbo.TB013_Pessoa.TB013_CarteirinhaStatus, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.View_Celulares.TB009_Contato,  ");
                sSql.Append(" ISNULL(dbo.View_Celulares.TB009_Contato, 'SEM CELULAR'), dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB002_PontosDeVenda.TB002_id, dbo.TB002_PontosDeVenda.TB002_Ponto  ");
                sSql.Append(" HAVING  dbo.TB013_Pessoa.TB013_CarteirinhaStatus = 1 AND  dbo.TB012_Contratos.TB012_TipoContrato =  ");
                sSql.Append(TB012_TipoContrato);
                sSql.Append(" ORDER BY dbo.TB002_PontosDeVenda.TB002_Ponto, dbo.TB013_Pessoa.TB012_id, dbo.TB013_Pessoa.TB013_id  ");


                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PessoaController
                    {
                        TB012_Id = Convert.ToInt64(reader["TB012_id"].ToString()),
                        TB013_id = Convert.ToInt64(reader["TB013_id"].ToString()),
                        TB013_Cartao = reader["TB013_Cartao"].ToString(),
                        TB013_CartaoLote = CartaoLote,
                        TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString(),
                        Contato = new ContatoController { TB009_Contato = reader["TB009_Contato"].ToString() },
                        PontoDeVenda = new PontoDeVendaController { TB002_Ponto = reader["TB002_Ponto"].ToString() }
                    };

                    RetornoList.Add(obj);
                    /*Atualiza o Lote de Impressão do Cartão*/
                    CartoesIncluirLote(obj.TB013_id, CartaoLote);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetornoList;
        }


        public Boolean CartoesIncluirLote(Int64 TB013_id, Int16 CartaoLote)
        {
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" update ");
                sSQL.Append(" TB013_Pessoa ");
                sSQL.Append(" set ");
                sSQL.Append(" TB013_CartaoLote = ");
                sSQL.Append(CartaoLote);
                sSQL.Append(" where TB013_id = ");
                sSQL.Append(TB013_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public Boolean CartoesUpdateStatus(Int16 CartaoLote, int CarteirinhaStatus)
        {
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" update ");
                sSQL.Append(" TB013_Pessoa ");
                sSQL.Append(" set ");
                sSQL.Append(" TB013_CarteirinhaStatus = ");
                sSQL.Append(CarteirinhaStatus);
                sSQL.Append(" where TB013_CartaoLote = ");
                sSQL.Append(CartaoLote);



                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// Descrição:  Listar contezinos para importação
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       14/11/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        /// 
        public List<PessoaController> ListarContezinos(string cnpjParceiro, string senhaParceiro)
        {
            var ehParceiro = new ParceiroDAO().ValidarParceiro(cnpjParceiro, senhaParceiro);

            List<PessoaController> RetornoList = new List<PessoaController>();

            if (!ehParceiro)
            {
                throw new Exception("Parceiro não encontrado.");
                //return RetornoList;
            }

            PessoaController ParceiroPrivilegio = new ParceiroDAO().ParceiroPrivilegio(cnpjParceiro, senhaParceiro);

            if (ParceiroPrivilegio.TB034_ImpContezinos < 1)
            {
                throw new Exception("Parceiro sem acesso a funcionalidade");
            }
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" SELECT  dbo.TB012_Contratos.TB012_Status  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Status  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB012_id  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Cartao  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_id  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeExibicao  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_TipoContrato  ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB012_Contratos  ");
                sSql.Append(" INNER JOIN  ");
                sSql.Append(" dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB012_id = dbo.TB013_Pessoa.TB012_id  ");
                sSql.Append(" WHERE  ");
                sSql.Append(" (dbo.TB013_Pessoa.TB013_Status = 1)   ");
                sSql.Append(" AND  ");
                sSql.Append(" (dbo.TB012_Contratos.TB012_Status = 1)   ");
                sSql.Append(" AND  ");
                sSql.Append(" (dbo.TB012_Contratos.TB012_TipoContrato = 1)  ");
                sSql.Append(" ORDER BY   ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Cartao  ");


                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PessoaController
                    {
                        TB013_id            = Convert.ToInt64(reader["TB013_id"].ToString()),
                        TB013_Cartao        = reader["TB013_Cartao"].ToString(),
                        TB013_StatusS       = reader["TB013_Status"].ToString(),
                        TB012_Id            = Convert.ToInt64(reader["TB012_id"].ToString()),
                        //TB013_CartaoLote = CartaoLote,
                        TB013_NomeCompleto  = reader["TB013_NomeCompleto"].ToString(),
                        TB013_NomeExibicao  = reader["TB013_NomeExibicao"].ToString(),
                        Contrato = new ContratosController
                        {
                            TB012_StatusS   = reader["TB012_Status"].ToString()
                        }
                        //PontoDeVenda = new PontoDeVendaController { TB002_Ponto = reader["TB002_Ponto"].ToString() }
                    };

                    RetornoList.Add(obj);
                    /*Atualiza o Lote de Impressão do Cartão*/
                    //CartoesIncluirLote(obj.TB013_id, CartaoLote);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetornoList;
        }

        /// <summary>
        /// Descrição:  Listar contezinos para importação
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       14/11/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        /// 
        public PessoaController contezinoCartao(string cnpjParceiro, string senhaParceiro, string Cartao)
        {
            var ehParceiro = new ParceiroDAO().ValidarParceiro(cnpjParceiro, senhaParceiro);

            PessoaController Retorno = new PessoaController();
            Retorno.Contrato = new ContratosController();

            if (!ehParceiro)
            {
                throw new Exception("Parceiro não encontrado.");
            }

            PessoaController ParceiroPrivilegio = new ParceiroDAO().ParceiroPrivilegio(cnpjParceiro, senhaParceiro);

            if (ParceiroPrivilegio.TB034_ImpContezinos < 1)
            {
                throw new Exception("Parceiro sem acesso a funcionalidade");
            }
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSql = new StringBuilder();


                sSql.Append(" SELECT dbo.TB013_Pessoa.TB013_Cartao  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_id  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_CPFCNPJ  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeExibicao  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Sexo  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_DataNascimento  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Status  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_Status  ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB013_Pessoa   ");
                sSql.Append(" INNER JOIN  ");
                sSql.Append(" dbo.TB012_Contratos   ");
                sSql.Append(" ON   ");
                sSql.Append(" dbo.TB013_Pessoa.TB012_id = dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(" WHERE dbo.TB012_Contratos.TB012_TipoContrato IN (1, 2) ");
                sSql.Append("   AND dbo.TB012_Contratos.TB012_Status NOT IN (5) ");
                sSql.Append("   AND dbo.TB013_Pessoa.TB013_Cartao =  ");
                sSql.Append(" '");
                sSql.Append(Cartao.Trim());
                sSql.Append(" '");


                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Retorno.TB013_Cartao            = reader["TB013_Cartao"].ToString();
                    Retorno.TB013_id                = Convert.ToInt64(reader["TB013_id"].ToString());       
                    Retorno.TB013_StatusS           = reader["TB013_Status"].ToString();
                    Retorno.TB013_NomeCompleto      = reader["TB013_NomeCompleto"].ToString();
                    Retorno.TB013_NomeExibicao      = reader["TB013_NomeExibicao"].ToString();
                    Retorno.TB013_CPFCNPJ           = reader["TB013_CPFCNPJ"].ToString();
                    Retorno.TB013_SexoS             =  reader["TB013_Sexo"].ToString();
                    Retorno.TB013_DataNascimento    = Convert.ToDateTime( reader["TB013_DataNascimento"].ToString());
                    Retorno.Contrato.TB012_Id       = Convert.ToInt64(reader["TB012_id"].ToString());
                    Retorno.Contrato.TB012_StatusS  = reader["TB012_Status"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public PessoaController contezinoCPF(string cnpjParceiro, string senhaParceiro, string cpf)
        {
            var ehParceiro = new ParceiroDAO().ValidarParceiro(cnpjParceiro, senhaParceiro);

            PessoaController Retorno = new PessoaController();
            Retorno.Contrato = new ContratosController();

            if (!ehParceiro)
            {
                throw new Exception("Parceiro não encontrado.");
            }

            PessoaController ParceiroPrivilegio = new ParceiroDAO().ParceiroPrivilegio(cnpjParceiro, senhaParceiro);

            if (ParceiroPrivilegio.TB034_ImpContezinos < 1)
            {
                throw new Exception("Parceiro sem acesso a funcionalidade");
            }
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSql = new StringBuilder();


                sSql.Append(" SELECT dbo.TB013_Pessoa.TB013_Cartao  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_id  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_CPFCNPJ  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeExibicao  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Sexo  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_DataNascimento  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Status  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_Status  ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB013_Pessoa   ");
                sSql.Append(" INNER JOIN  ");
                sSql.Append(" dbo.TB012_Contratos   ");
                sSql.Append(" ON   ");
                sSql.Append(" dbo.TB013_Pessoa.TB012_id = dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(" WHERE dbo.TB012_Contratos.TB012_TipoContrato IN (1, 2) ");
                sSql.Append("   AND dbo.TB012_Contratos.TB012_Status NOT IN (5) ");
                sSql.Append("   AND dbo.TB013_Pessoa.TB013_CPFCNPJ = ");
                sSql.Append(" '");
                sSql.Append(cpf.Trim().Replace(".","").Replace(",", "").Replace("-", "").Replace("/", ""));
                sSql.Append(" '");


                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Retorno.TB013_Cartao = reader["TB013_Cartao"].ToString();
                    Retorno.TB013_id = Convert.ToInt64(reader["TB013_id"].ToString());
                    Retorno.TB013_StatusS = reader["TB013_Status"].ToString();
                    Retorno.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString();
                    Retorno.TB013_NomeExibicao = reader["TB013_NomeExibicao"].ToString();
                    Retorno.TB013_CPFCNPJ = reader["TB013_CPFCNPJ"].ToString();
                    Retorno.TB013_SexoS = reader["TB013_Sexo"].ToString();
                    Retorno.TB013_DataNascimento = Convert.ToDateTime(reader["TB013_DataNascimento"].ToString());
                    Retorno.Contrato.TB012_Id = Convert.ToInt64(reader["TB012_id"].ToString());
                    Retorno.Contrato.TB012_StatusS = reader["TB012_Status"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }


        public PessoaController acessosmultiplos(string cnpjParceiro, string senhaParceiro)
        {
            var ehParceiro = new ParceiroDAO().ValidarParceiro(cnpjParceiro, senhaParceiro);

            PessoaController Retorno = new PessoaController();
            Retorno.Contrato = new ContratosController();

            if (!ehParceiro)
            {
                throw new Exception("Parceiro não encontrado.");
            }

            PessoaController ParceiroPrivilegio = new ParceiroDAO().ParceiroPrivilegio(cnpjParceiro, senhaParceiro);

            if (ParceiroPrivilegio.TB034_ImpContezinos < 1)
            {
                throw new Exception("Parceiro sem acesso a funcionalidade");
            }
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSql = new StringBuilder();


                //sSql.Append(" SELECT dbo.TB013_Pessoa.TB013_Cartao  ");
                //sSql.Append(" , dbo.TB013_Pessoa.TB013_id  ");
                //sSql.Append(" , dbo.TB013_Pessoa.TB013_CPFCNPJ  ");
                //sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto  ");
                //sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeExibicao  ");
                //sSql.Append(" , dbo.TB013_Pessoa.TB013_Sexo  ");
                //sSql.Append(" , dbo.TB013_Pessoa.TB013_DataNascimento  ");
                //sSql.Append(" , dbo.TB013_Pessoa.TB013_Status  ");
                //sSql.Append(" , dbo.TB012_Contratos.TB012_id  ");
                //sSql.Append(" , dbo.TB012_Contratos.TB012_Status  ");
                //sSql.Append(" FROM ");
                //sSql.Append(" dbo.TB013_Pessoa   ");
                //sSql.Append(" INNER JOIN  ");
                //sSql.Append(" dbo.TB012_Contratos   ");
                //sSql.Append(" ON   ");
                //sSql.Append(" dbo.TB013_Pessoa.TB012_id = dbo.TB012_Contratos.TB012_id  ");
                //sSql.Append(" WHERE   ");
                //sSql.Append(" dbo.TB013_Pessoa.TB013_CPFCNPJ=  ");
                //sSql.Append(" '");
                //sSql.Append(cpf.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", ""));
                //sSql.Append(" '");
                sSql.Append("SELECT   ");
                sSql.Append("dbo.TB013_Pessoa.TB013_CPFCNPJ  ");
                sSql.Append(", dbo.TB040_TB013_TB012.TB040_id  ");
                sSql.Append(", dbo.TB013_Pessoa.TB013_id  ");
                sSql.Append(", dbo.TB013_Pessoa.TB013_NomeCompleto  ");
                sSql.Append(", dbo.TB013_Pessoa.TB013_Cartao  ");
                sSql.Append(", dbo.TB013_Pessoa.TB013_Status  ");
                sSql.Append(", dbo.TB013_Pessoa.TB013_NomeExibicao  ");
                sSql.Append(", dbo.TB013_Pessoa.TB013_Sexo  ");
                sSql.Append(", dbo.TB013_Pessoa.TB013_DataNascimento  ");
                sSql.Append(", dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(", dbo.TB012_Contratos.TB012_Status  ");
                sSql.Append("FROM ");
                sSql.Append("dbo.TB040_TB013_TB012   ");
                sSql.Append("INNER JOIN  ");
                sSql.Append("dbo.TB013_Pessoa   ");
                sSql.Append("ON   ");
                sSql.Append("dbo.TB040_TB013_TB012.TB013_id = dbo.TB013_Pessoa.TB013_id   ");
                sSql.Append("INNER JOIN  ");
                sSql.Append(" dbo.TB012_Contratos   ");
                sSql.Append("ON   ");
                sSql.Append("dbo.TB040_TB013_TB012.TB012_id = dbo.TB012_Contratos.TB012_id  ");
                sSql.Append("ORDER BY   ");
                sSql.Append("dbo.TB013_Pessoa.TB013_id  ");
                sSql.Append(", dbo.TB040_TB013_TB012.TB012_id  ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Retorno.TB013_Cartao = reader["TB013_Cartao"].ToString();
                    Retorno.TB013_id = Convert.ToInt64(reader["TB013_id"].ToString());
                    Retorno.TB013_StatusS = reader["TB013_Status"].ToString();
                    Retorno.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString();
                    Retorno.TB013_NomeExibicao = reader["TB013_NomeExibicao"].ToString();
                    Retorno.TB013_CPFCNPJ = reader["TB013_CPFCNPJ"].ToString();
                    Retorno.TB013_SexoS = reader["TB013_Sexo"].ToString();
                    Retorno.TB013_DataNascimento = Convert.ToDateTime(reader["TB013_DataNascimento"].ToString());
                    Retorno.Contrato.TB012_Id = Convert.ToInt64(reader["TB012_id"].ToString());
                    Retorno.Contrato.TB012_StatusS = reader["TB012_Status"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }


        public string contezinoConsultaStatus(string cnpjParceiro, string senhaParceiro,string variavel)
        {
            var ehParceiro = new ParceiroDAO().ValidarParceiro(cnpjParceiro, senhaParceiro);

            PessoaController obj = new PessoaController();
            obj.Contrato = new ContratosController();

            string retorno = "NÃO ENCONTRATO";

            if (!ehParceiro)
            {
                throw new Exception("Parceiro não encontrado.");
            }

            PessoaController ParceiroPrivilegio = new ParceiroDAO().ParceiroPrivilegio(cnpjParceiro, senhaParceiro);

            if (ParceiroPrivilegio.TB034_ImpContezinos < 1)
            {
                throw new Exception("Parceiro sem acesso a funcionalidade");
            }
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSql = new StringBuilder();



    

                sSql.Append(" SELECT  ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_id ");
                sSql.Append(" ,dbo.TB012_Contratos.TB012_TipoContrato ");
                sSql.Append(" ,dbo.TB013_Pessoa.TB013_CPFCNPJ ");
                sSql.Append(" ,dbo.TB013_Pessoa.TB013_NomeCompleto ");
                sSql.Append(" ,dbo.TB013_Pessoa.TB013_NomeExibicao ");
                sSql.Append(" ,dbo.TB013_Pessoa.TB013_Status ");
                sSql.Append(" ,dbo.TB012_Contratos.TB012_id ");
                sSql.Append(" ,dbo.TB012_Contratos.TB012_Status ");
                sSql.Append(" ,dbo.TB013_Pessoa.TB013_Cartao ");
                sSql.Append(" FROM  ");
                sSql.Append(" dbo.TB013_Pessoa  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB012_Contratos  ");
                sSql.Append(" ON  ");
                sSql.Append(" dbo.TB013_Pessoa.TB012_id = dbo.TB012_Contratos.TB012_id ");
        
                variavel = variavel.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim();
                if(variavel.Length==11)
                {
                    sSql.Append(" WHERE ");
                    sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato = 1 ");
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB013_Pessoa.TB013_CPFCNPJ = ");
                    sSql.Append("'");
                    sSql.Append(variavel);
                    sSql.Append("'");
                    sSql.Append(" OR ");
                    sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato = 4 ");
                    sSql.Append(" AND ");
                    sSql.Append(" dbo.TB013_Pessoa.TB013_CPFCNPJ = ");
                    sSql.Append("'");
                    sSql.Append(variavel);
                    sSql.Append("'");
                }
                else
                {
                    if (variavel.Length == 13)
                    {
                        sSql.Append(" WHERE ");
                        sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato = 1 ");
                        sSql.Append(" AND ");
                        sSql.Append(" replace(dbo.TB013_Pessoa.TB013_Cartao, '-', '') = ");
                        sSql.Append("'");
                        sSql.Append(variavel);
                        sSql.Append("'");
                        sSql.Append(" OR ");
                        sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato = 4 ");
                        sSql.Append(" AND ");
                        sSql.Append(" replace(dbo.TB013_Pessoa.TB013_Cartao, '-', '') = ");
                        sSql.Append("'");
                        sSql.Append(variavel);
                        sSql.Append("'");
                    }
                }



                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
;
                    obj.TB013_id = Convert.ToInt64(reader["TB013_id"].ToString());
                    obj.TB013_StatusS = reader["TB013_Status"].ToString();
                    obj.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString();    
                    obj.Contrato.TB012_StatusS = reader["TB012_Status"].ToString();
                }

                con.Close();


                if (obj.TB013_id==0)
                {
                    return "Contezino NÃO encontrato";
                }
                else
                {
                    if (Convert.ToInt16(obj.Contrato.TB012_StatusS) == 0)
                    {
                        return "Contrato NÃO ativado";
                    }
                    else
                    {
                        if (Convert.ToInt16(obj.Contrato.TB012_StatusS) == 1)
                        {
                            if (Convert.ToInt16(obj.TB013_StatusS) == 0)
                            {
                                return obj.TB013_NomeCompleto.TrimEnd() + " [Contezino não Ativo]";
                            }
                            else
                            {
                                if (Convert.ToInt16(obj.TB013_StatusS) == 1)
                                {
                                    return obj.TB013_NomeCompleto.TrimEnd() + " [Contezino Ativo]";
                                }
                                else
                                {
                                    if (Convert.ToInt16(obj.TB013_StatusS) > 1)
                                    {
                                        return obj.TB013_NomeCompleto.TrimEnd() + " [Contezino desativado pelo Titular do contrato]";
                                    }


                                }
                            }

                        }
                        else
                        {
                            if (Convert.ToInt16(obj.Contrato.TB012_StatusS) > 1)
                            {
                                return "Contrato NÃO ativado";
                            }
                        }
                    }
                }


            



               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

    }
}
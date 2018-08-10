using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace DAO
{
    public class ContratosDao
    {
        /// <summary>
        /// Descrição:  Incluir Novo Contrato
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public ContratosController ContratoInsert(ContratosController contrato)
        {
            ContratosController retorno = new ContratosController();
            try
            {
                const string insertSql = "INSERT INTO TB012_Contratos (TB002_id,TB013_id,TB012_Inicio,TB012_Fim,TB012_AceiteContrato,TB012_DataAceiteContrato,TB012_CadastradoEm,TB012_CadastradorPor,TB012_AlteradoEm,TB012_AlteradoPor,TB012_Status,TB004_Cep,TB006_id,TB012_Logradouro,TB012_Numero,TB012_Bairro,TB012_Complemento,TB012_TipoContrato,TB012_ProximoCodDependente,TB012_CicloContrato,TB012_CodCartao,TB012_VSContrato,TB012_DiaVencimento,TB012_NumeroDaSorte) VALUES (@TB002_id,@TB013_id,@TB012_Inicio,@TB012_Fim,@TB012_AceiteContrato,@TB012_DataAceiteContrato,@TB012_CadastradoEm,@TB012_CadastradorPor,@TB012_AlteradoEm,@TB012_AlteradoPor,@TB012_Status,@TB004_Cep,@TB006_id,@TB012_Logradouro,@TB012_Numero,@TB012_Bairro,@TB012_Complemento,@TB012_TipoContrato,@TB012_ProximoCodDependente,@TB012_CicloContrato,@TB012_CodCartao,@TB012_VSContrato,@TB012_DiaVencimento,@TB012_NumeroDaSorte) SELECT SCOPE_IDENTITY()";

                using (var con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();

                    var command = new SqlCommand(insertSql, con);
                    command.CommandTimeout = 300;

                    command.Parameters.AddWithValue("@TB002_id", contrato.PontoDeVenda.TB002_id);
                    command.Parameters.AddWithValue("@TB013_id", contrato.Titular.TB013_id); //Titular do Contrato
                    command.Parameters.AddWithValue("@TB012_Inicio", contrato.TB012_Inicio);
                    command.Parameters.AddWithValue("@TB012_Fim", contrato.TB012_Fim);
                    command.Parameters.AddWithValue("@TB012_AceiteContrato", contrato.TB012_AceiteContrato);
                    command.Parameters.AddWithValue("@TB012_DataAceiteContrato", contrato.TB012_DataAceiteContrato);
                    command.Parameters.AddWithValue("@TB012_CadastradoEm", DateTime.Now);
                    command.Parameters.AddWithValue("@TB012_CadastradorPor", contrato.TB012_CadastradorPor);
                    command.Parameters.AddWithValue("@TB012_AlteradoEm", DateTime.Now);
                    command.Parameters.AddWithValue("@TB012_AlteradoPor", contrato.TB012_AlteradoPor);
                    command.Parameters.AddWithValue("@TB012_Status", contrato.TB012_StatusS);
                    command.Parameters.AddWithValue("@TB004_Cep", contrato.TB004_Cep.Replace("-", ""));
                    command.Parameters.AddWithValue("@TB006_id", contrato.TB006_id);
                    command.Parameters.AddWithValue("@TB012_Logradouro", contrato.TB012_Logradouro);
                    command.Parameters.AddWithValue("@TB012_Numero", contrato.TB012_Numero);
                    command.Parameters.AddWithValue("@TB012_Bairro", contrato.TB012_Bairro);
                    command.Parameters.AddWithValue("@TB012_Complemento", contrato.TB012_Complemento);
                    command.Parameters.AddWithValue("@TB012_TipoContrato", contrato.TB012_TipoContrato);
                    command.Parameters.AddWithValue("@TB012_ProximoCodDependente", 1001);
                    command.Parameters.AddWithValue("@TB012_CicloContrato", contrato.TB012_CicloContrato);
                    command.Parameters.AddWithValue("@TB012_CodCartao", contrato.TB012_CodCartao);
                    command.Parameters.AddWithValue("@TB012_VSContrato", 1);
                    command.Parameters.AddWithValue("@TB012_DiaVencimento", contrato.TB012_DiaVencimento);
                    command.Parameters.AddWithValue("@TB012_NumeroDaSorte", contrato.TB012_NumeroDaSorte);
                    
                    retorno.TB012_Id = Convert.ToInt32(command.ExecuteScalar());

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;

        }


        #region Familiar
   
        #endregion

        public int EdicaoContrato(long tb012Id)
        {
            int retorno = 0;

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();
                sSql.Append("SELECT TOP (1) TB012_id, TB012_Edicao FROM dbo.TB012_Contratos WHERE    TB012_id = ");
                sSql.Append(tb012Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    retorno = reader["TB012_Edicao"] is DBNull ? 0 : Convert.ToInt16(reader["TB012_Edicao"].ToString());
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }

        /// <summary>
        /// Descrição:  Incluir Novo Contrato
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<ContratosController> ContratoParceirosSelect(String query)
        {
            List<ContratosController> retornoList = new List<ContratosController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" SELECT   ");

                sSql.Append(" dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_TipoContrato  ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_Matriz  ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_TipoPessoa  ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_Documento  ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_RazaoSocial  ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_NomeFantasia  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_CadastradoEm  ");
                sSql.Append(" , CadastradroPor.TB011_NomeExibicao AS CadastradoPor  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_AlteradoEm  ");
                sSql.Append(" , AlteradoPor.TB011_NomeExibicao AS AlteradoPor  ");
                sSql.Append(" , dbo.TB002_PontosDeVenda.TB002_id  ");
                sSql.Append(" , dbo.TB002_PontosDeVenda.TB002_Ponto  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_Status  ");
                sSql.Append(" , dbo.TB011_TB002.TB011_Id  ");
                sSql.Append(" FROM             ");
                sSql.Append(" dbo.TB012_Contratos   ");
                sSql.Append(" INNER JOIN  ");
                sSql.Append(" dbo.TB020_Unidades ON dbo.TB012_Contratos.TB012_id = dbo.TB020_Unidades.TB012_id INNER JOIN  ");
                sSql.Append(" dbo.TB011_APPUsuarios AS CadastradroPor ON dbo.TB012_Contratos.TB012_CadastradorPor = CadastradroPor.TB011_Id INNER JOIN  ");
                sSql.Append("  dbo.TB011_APPUsuarios AS AlteradoPor ON dbo.TB012_Contratos.TB012_AlteradoPor = AlteradoPor.TB011_Id INNER JOIN  ");
                sSql.Append(" dbo.TB002_PontosDeVenda ON dbo.TB012_Contratos.TB002_id = dbo.TB002_PontosDeVenda.TB002_id INNER JOIN  ");
                sSql.Append("  dbo.TB011_TB002 ON dbo.TB012_Contratos.TB002_id = dbo.TB011_TB002.TB002_id  ");
                sSql.Append(" GROUP BY  ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_TipoContrato  ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_Matriz  ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_TipoPessoa  ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_Documento  ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_RazaoSocial  ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_NomeFantasia  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_CadastradoEm  ");
                sSql.Append(" , CadastradroPor.TB011_NomeExibicao  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_AlteradoEm  ");
                sSql.Append(" , AlteradoPor.TB011_NomeExibicao  ");
                sSql.Append(" , dbo.TB002_PontosDeVenda.TB002_id  ");
                sSql.Append(" , dbo.TB002_PontosDeVenda.TB002_Ponto  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_Status  ");
                sSql.Append(" , dbo.TB011_TB002.TB011_Id  ");
                sSql.Append(" HAVING                     ");
                sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato = 2  ");
                sSql.Append(" AND  ");
                sSql.Append(" dbo.TB020_Unidades.TB020_Matriz = 1  ");
                sSql.Append(query);





                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ContratosController obj = new ContratosController();


                    obj.TB012_Id = Convert.ToInt64(reader["TB012_id"]);
                    obj.TB020_RazaoSocial = reader["TB020_RazaoSocial"].ToString();
                    obj.TB012_StatusS = Enum.GetName(typeof(ContratosController.TB012_StatusE), Convert.ToInt16(reader["TB012_Status"]));
                    obj.TB020_NomeFantasia = reader["TB020_NomeFantasia"].ToString();

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        
        /// <summary>     
        /// Descrição:  Incluir Novo Contrato de Parceiro
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       09/02/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public ContratosController ContratoSelect(Int64 vId)
        {
            ContratosController retorno = new ContratosController();
            PontoDeVendaController objPontoDeVenda = new PontoDeVendaController();
            PessoaController objTitular = new PessoaController();

            retorno.PontoDeVenda = objPontoDeVenda;
            retorno.Titular = objTitular;

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();
                //sSQL.Append("SELECT * from TB012_Contratos where TB012_id = ");

                //TB012_NumeroDaSorte
                sSql.Append("SELECT dbo.TB012_Contratos.TB012_NumeroDaSorte,dbo.TB012_Contratos.TB012_DiaVencimento,dbo.TB012_Contratos.TB012_id, dbo.TB012_Contratos.TB012_CicloContrato, dbo.TB012_Contratos.TB012_Inicio, dbo.TB012_Contratos.TB012_Fim, dbo.TB012_Contratos.TB012_Status, dbo.TB012_Contratos.TB012_DataAceiteContrato, ");
                sSql.Append("dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB012_Contratos.TB012_AceiteContrato, dbo.TB012_Contratos.TB002_id, dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB012_Contratos.TB012_Edicao ");
                sSql.Append("FROM dbo.TB012_Contratos INNER JOIN ");
                sSql.Append("dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id ");
                sSql.Append(" WHERE dbo.TB012_Contratos.TB012_id = ");
                sSql.Append(vId);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    retorno.TB012_Id = Convert.ToInt64(reader["TB012_Id"]);
                    //retorno.TB012_Edicao = Convert.ToInt16(reader["TB012_Edicao"]);
                    retorno.TB012_Edicao = reader["TB012_Edicao"] is DBNull ? 0 : Convert.ToInt16(reader["TB012_Edicao"].ToString());
                    retorno.TB012_CicloContrato = reader["TB012_CicloContrato"].ToString();
                    retorno.TB012_Inicio = Convert.ToDateTime(reader["TB012_Inicio"]);
                    retorno.TB012_Fim = Convert.ToDateTime(reader["TB012_Fim"]);
                    retorno.TB012_StatusS = reader["TB012_Status"].ToString();
                    retorno.TB012_DataAceiteContrato = Convert.ToDateTime(reader["TB012_DataAceiteContrato"]);
                    retorno.TB012_AceiteContrato = Convert.ToInt16(reader["TB012_AceiteContrato"]);
                    retorno.TB012_TipoContrato = Convert.ToInt16(reader["TB012_TipoContrato"]);
                    retorno.PontoDeVenda.TB002_id = Convert.ToInt64(reader["TB002_id"]);
                    retorno.Titular.TB013_id = Convert.ToInt64(reader["TB013_id"]);
                    retorno.Titular.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString();
                    retorno.TB012_DiaVencimento = reader["TB012_DiaVencimento"] is DBNull ? 5 : Convert.ToInt16(reader["TB012_DiaVencimento"].ToString());
                    retorno.TB012_NumeroDaSorte = reader["TB012_NumeroDaSorte"] is DBNull ? 0 : Convert.ToInt64(reader["TB012_NumeroDaSorte"].ToString());

                  

                }
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }    
        /// <summary>
        /// Descrição:  Ativa Contrato
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       02/02/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool ContratoAtivar(long tb012Id, long tb011Id)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" UPDATE TB012_Contratos SET ");
                sSql.Append(" TB012_Status = 1");
                sSql.Append(" ,TB012_AlteradoEm = ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(",TB012_AlteradoPor=");
                sSql.Append(tb011Id);
                sSql.Append(" WHERE TB012_id =");

                sSql.Append(tb012Id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }
        public bool ContratoNegociado(long tb012Id, long tb011Id, long TB037_Id)
        {

            //ParametrosInterface.objUsuarioLogado.TB037_Id
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" UPDATE TB012_Contratos SET ");
                sSql.Append(" TB012_Status = 6");
                sSql.Append(" ,TB037_Id = ");
                sSql.Append(TB037_Id);
                sSql.Append(" ,TB012_AlteradoEm = ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(",TB012_AlteradoPor=");
                sSql.Append(tb011Id);
                sSql.Append(" WHERE TB012_id =");

                sSql.Append(tb012Id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }
        /// <summary>
        /// Descrição:  Inativar Contrato
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       02/02/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool ContratoInativar(long tb012Id, long tb011Id, double multa)
        {
            try
            {
                var sSql = new StringBuilder();
                sSql.Append(" UPDATE TB012_Contratos SET ");
                sSql.Append(" TB012_Status = 3");
                sSql.Append(" ,TB012_AlteradoEm = ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(",TB012_AlteradoPor=");
                sSql.Append(tb011Id);
                sSql.Append(" ,TB012_MultaCancelamento = ");
                sSql.Append(multa.ToString("N2").Replace(".", "").Replace(",", "."));
                sSql.Append(" WHERE TB012_id =");

                sSql.Append(tb012Id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }
        /*Somente vinculado com parcela de cancelamento*/
        public bool Contratocancelar(long tb012Id, long tb011Id)
        {
            try
            {
                var sSql = new StringBuilder();
                sSql.Append(" UPDATE TB012_Contratos SET ");
                sSql.Append(" TB012_Status = 5");
                sSql.Append(" ,TB012_AlteradoEm = ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(",TB012_AlteradoPor=");
                sSql.Append(tb011Id);
                sSql.Append(" WHERE TB012_id =");
                sSql.Append(tb012Id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }
        public bool ContratoCodDependente(long tb012Id, double codDependente)
        {
            try
            {
                var sSql = new StringBuilder();
                sSql.Append(" UPDATE TB012_Contratos SET ");
                sSql.Append(" TB012_ProximoCodDependente = ");
                sSql.Append(codDependente);
                sSql.Append(" WHERE TB012_id =");
                sSql.Append(tb012Id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }
        public List<ContratosController> ContratoCodCartaoUtilizados()
        {
            List<ContratosController> retorno = new List<ContratosController>();

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();
                sSql.Append("select coalesce(TB012_CodCartao, TB012_id) as TB012_CodCartao from TB012_Contratos");
                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ContratosController obj = new ContratosController();
                    obj.TB012_CodCartao = Convert.ToInt64(reader["TB012_CodCartao"]);
                    retorno.Add(obj);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }
        /// <summary>
        /// Descrição:  Incluir Novo Contrato
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<UnidadeController> ContratosCorporativoSelect(string query)
        {
            List<UnidadeController> retornoList = new List<UnidadeController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append("SELECT  dbo.TB020_Unidades.TB020_id, dbo.TB012_Contratos.TB012_id, dbo.TB020_Unidades.TB020_NomeFantasia, dbo.TB020_Unidades.TB020_Documento, ");
                sSql.Append("dbo.TB020_Unidades.TB020_AlteradoEm, dbo.TB020_Unidades.TB020_Status, dbo.TB011_APPUsuarios.TB011_NomeExibicao ");
                sSql.Append("FROM dbo.TB012_Contratos INNER JOIN ");
                sSql.Append(" dbo.TB020_Unidades ON dbo.TB012_Contratos.TB012_id = dbo.TB020_Unidades.TB012_idCorporativo INNER JOIN ");
                sSql.Append("dbo.TB006_Municipio ON dbo.TB020_Unidades.TB006_id = dbo.TB006_Municipio.TB006_id INNER JOIN ");
                sSql.Append("dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id INNER JOIN ");
                sSql.Append("dbo.TB011_APPUsuarios ON dbo.TB020_Unidades.TB020_AlteradoPor = dbo.TB011_APPUsuarios.TB011_Id ");
                sSql.Append(" WHERE dbo.TB012_Contratos.TB012_TipoContrato = 3 ");
                sSql.Append(query);
                sSql.Append("ORDER BY dbo.TB020_Unidades.TB020_id, dbo.TB020_Unidades.TB020_Matriz ");


                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UnidadeController obj = new UnidadeController();

                    obj.TB012_id = Convert.ToInt64(reader["TB012_id"]);
                    obj.TB020_id = Convert.ToInt64(reader["TB020_id"]);
                    obj.TB020_NomeFantasia = reader["TB020_NomeFantasia"].ToString();
                    obj.TB020_Documento = reader["TB020_Documento"] is DBNull ? "SEM CNPJ" : Convert.ToUInt64(reader["TB020_Documento"].ToString().TrimEnd().TrimStart()).ToString(@"000\.000\.000\-00");
                    obj.TB020_AlteradoEm = Convert.ToDateTime(reader["TB020_AlteradoEm"].ToString());
                    obj.TB020_AlteradoPorNome = reader["TB011_NomeExibicao"].ToString().TrimEnd().TrimStart().ToUpper();
                    obj.TB020_StatusS = Enum.GetName(typeof(UnidadeController.TB020_StatusE), Convert.ToInt16(reader["TB020_Status"]));

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        public ContratosController ContratoCorporativoSelect(long tb012Id, int tb020Matriz)
        {
            var retorno = new ContratosController
            {
                PontoDeVenda = new PontoDeVendaController(),
                Unidade = new UnidadeController { Estado = new EstadoController() }
            };
            retorno.Pais = new PaisController();
            retorno.Unidade.Municipio = new MunicipioController();
            retorno.Titular = new PessoaController
            {
                Pais = new PaisController(),
                Estado = new EstadoController(),
                Municipio = new MunicipioController()
            };

            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" SELECT  dbo.TB012_Contratos.TB012_CicloContrato,dbo.TB012_Contratos.TB012_id, dbo.TB002_PontosDeVenda.TB002_id, dbo.TB002_PontosDeVenda.TB002_Ponto, dbo.TB012_Contratos.TB012_Inicio, Pais_Contrato.TB003_id AS Expr8,  ");
                sSql.Append(" Pais_Contrato.TB003_Pais AS Expr9, Pais_Contrato.TB003_DDI AS Expr10, dbo.TB012_Contratos.TB012_Logradouro, dbo.TB012_Contratos.TB012_Numero, dbo.TB012_Contratos.TB012_Bairro, ");
                sSql.Append(" dbo.TB012_Contratos.TB012_Complemento, dbo.TB012_Contratos.TB012_InformacoesPortal, dbo.TB012_Contratos.TB012_ContratoCancelarMotivo, dbo.TB012_Contratos.TB012_ContratoCancelarDescricao, ");
                sSql.Append(" dbo.TB012_Contratos.TB012_ProximoCodDependente, dbo.TB012_Contratos.TB012_CicloContrato, dbo.TB012_Contratos.TB012_CodCartao, dbo.TB012_Contratos.TB012_Fim, ");
                sSql.Append(" dbo.TB012_Contratos.TB012_AceiteContrato, dbo.TB012_Contratos.TB012_DataAceiteContrato, dbo.TB012_Contratos.TB012_CadastradoEm, dbo.TB012_Contratos.TB012_CadastradorPor, ");
                sSql.Append(" dbo.TB012_Contratos.TB012_AlteradoEm, dbo.TB012_Contratos.TB012_AlteradoPor, dbo.TB012_Contratos.TB004_Cep AS ContratoCep, Municipio_Contrato.TB006_id AS Expr1, ");
                sSql.Append(" Municipio_Contrato.TB006_Municipio AS Expr2, Municipio_Contrato.TB006_Codigo AS Expr3, Municipio_Contrato.TB006_IBGE, Estado_Contrato.TB005_Id AS Expr4, Estado_Contrato.TB005_Sigla AS Expr5, ");
                sSql.Append(" Estado_Contrato.TB005_Estado AS Expr6, Estado_Contrato.TB005_Codigo AS Expr7, dbo.TB013_Pessoa.TB013_id AS Titular_TB013_id, dbo.TB013_Pessoa.TB012_EraContezino AS Titular_TB012_EraContezino, ");
                sSql.Append("   dbo.TB013_Pessoa.TB012_id AS Titular_TB012_id, dbo.TB013_Pessoa.TB013_IdProtheus AS Titular_TB013_IdProtheus, dbo.TB013_Pessoa.TB013_Tipo AS Titular_TB013_Tipo, ");
                sSql.Append("  dbo.TB013_Pessoa.TB013_Cartao AS Titular_Cartao, dbo.TB013_Pessoa.TB013_CarteirinhaStatus AS Titular_TB013_CarteirinhaStatus, ");
                sSql.Append("  dbo.TB013_Pessoa.TB013_CodigoDependente AS Titular_TB013_CodigoDependente, dbo.TB013_Pessoa.TB013_CPFCNPJ AS Titular_TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_ListaNegra, ");
                sSql.Append("  dbo.TB013_Pessoa.TB013_NomeCompleto AS Titular_TB013_NomeCompleto, dbo.TB013_Pessoa.TB013_NomeExibicao, dbo.TB013_Pessoa.TB013_Sexo AS Titular_TB013_Sexo, ");
                sSql.Append("  dbo.TB013_Pessoa.TB013_RG AS Titular_TB013_RG, dbo.TB013_Pessoa.TB013_RGOrgaoEmissor AS Titular_TB013_RGOrgaoEmissor, ");
                sSql.Append("  dbo.TB013_Pessoa.TB013_DataNascimento AS Titular_TB013_DataNascimento, dbo.TB013_Pessoa.TB013_DeclaroSerMaiorCapaz, dbo.TB013_Pessoa.TB004_Cep AS Titular_TB004_Cep, ");
                sSql.Append("  dbo.TB013_Pessoa.TB013_Logradouro AS Titular_TB013_Logradouro, dbo.TB013_Pessoa.TB013_Numero AS Titular_TB013_Numero, dbo.TB013_Pessoa.TB013_Bairro AS Titular_TB013_Bairro, ");
                sSql.Append("  dbo.TB013_Pessoa.TB013_Complemento, dbo.TB013_Pessoa.TB013_Status, Municipio_Titular.TB006_id AS Titular_TB006_id, Municipio_Titular.TB006_Municipio AS Expr14, Municipio_Titular.TB006_Capital, ");
                sSql.Append("  Municipio_Titular.TB006_Codigo AS Expr15, Municipio_Titular.TB006_IBGE AS Expr16, Estado_Titular.TB005_Id AS Titular_TB005_Id, Estado_Titular.TB005_Sigla AS Expr18, ");
                sSql.Append("  Estado_Titular.TB005_Estado AS Expr19, Estado_Titular.TB005_Codigo AS Titular_TB005_Codigo, Pais_Titular.TB003_id AS Titular_TB003_id, Pais_Titular.TB003_Pais AS Titular_TB003_Pais, ");
                sSql.Append("  Pais_Titular.TB003_DDI AS Titular_TB003_DDI, dbo.TB020_Unidades.TB020_id, dbo.TB020_Unidades.TB020_Matriz, dbo.TB012_Contratos.TB012_Status, dbo.TB020_Unidades.TB020_RazaoSocial, ");
                sSql.Append("  dbo.TB020_Unidades.TB020_NomeFantasia, dbo.TB020_Unidades.TB020_TipoPessoa, dbo.TB020_Unidades.TB020_Documento, Municipio_Unidade.TB006_id AS Unidade_TB006_id, ");
                sSql.Append("  Municipio_Unidade.TB006_Municipio, Municipio_Unidade.TB006_Codigo, Estado_unidade.TB005_Id AS Unidade_TB005_Id, Estado_unidade.TB005_Sigla, Estado_unidade.TB005_Estado, ");
                sSql.Append("  Estado_unidade.TB005_Codigo, Pais_Unidade.TB003_id, Pais_Unidade.TB003_Pais, Pais_Unidade.TB003_DDI, dbo.TB020_Unidades.TB020_Cep AS Unidade_CEP, ");
                sSql.Append("  dbo.TB020_Unidades.TB020_NomeExibicaoDetalhes,dbo.TB020_Unidades.TB020_CategoriaExibicao,  ");
                sSql.Append("  dbo.TB020_Unidades.TB020_Logradouro AS Unidade_Logradouro, dbo.TB020_Unidades.TB020_Numero AS Unidade_Numero, dbo.TB020_Unidades.TB020_Bairro AS Unidade_Bairro, ");
                sSql.Append("   dbo.TB020_Unidades.TB020_Complemento AS Unidade_Complemento ");
                sSql.Append(" FROM dbo.TB012_Contratos INNER JOIN ");
                sSql.Append(" dbo.TB002_PontosDeVenda ON dbo.TB012_Contratos.TB002_id = dbo.TB002_PontosDeVenda.TB002_id INNER JOIN ");
                sSql.Append("  dbo.TB020_Unidades ON dbo.TB012_Contratos.TB012_id = dbo.TB020_Unidades.TB012_idCorporativo INNER JOIN ");
                sSql.Append("  dbo.TB006_Municipio AS Municipio_Unidade ON dbo.TB020_Unidades.TB006_id = Municipio_Unidade.TB006_id INNER JOIN ");
                sSql.Append("  dbo.TB005_Estado AS Estado_unidade ON Municipio_Unidade.TB005_Id = Estado_unidade.TB005_Id INNER JOIN ");
                sSql.Append("  dbo.TB003_Pais AS Pais_Unidade ON Estado_unidade.TB003_Id = Pais_Unidade.TB003_id INNER JOIN ");
                sSql.Append("  dbo.TB006_Municipio AS Municipio_Contrato ON dbo.TB012_Contratos.TB006_id = Municipio_Contrato.TB006_id INNER JOIN ");
                sSql.Append("  dbo.TB005_Estado AS Estado_Contrato ON Municipio_Contrato.TB005_Id = Estado_Contrato.TB005_Id INNER JOIN ");
                sSql.Append("  dbo.TB003_Pais AS Pais_Contrato ON Estado_Contrato.TB003_Id = Pais_Contrato.TB003_id INNER JOIN ");
                sSql.Append("  dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id INNER JOIN ");
                sSql.Append("  dbo.TB006_Municipio AS Municipio_Titular ON dbo.TB013_Pessoa.TB006_id = Municipio_Titular.TB006_id INNER JOIN ");
                sSql.Append("  dbo.TB005_Estado AS Estado_Titular ON Municipio_Titular.TB005_Id = Estado_Titular.TB005_Id INNER JOIN ");
                sSql.Append("  dbo.TB003_Pais AS Pais_Titular ON Estado_Titular.TB003_Id = Pais_Titular.TB003_id ");
                sSql.Append(" WHERE dbo.TB012_Contratos.TB012_id =   ");
                sSql.Append(tb012Id);
                sSql.Append("  AND dbo.TB020_Unidades.TB020_Matriz =  ");
                sSql.Append(tb020Matriz);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    retorno.TB012_Id = Convert.ToInt64(reader["TB012_Id"]);
                    retorno.TB012_Inicio = Convert.ToDateTime(reader["TB012_Inicio"]);
                    retorno.TB012_Fim = Convert.ToDateTime(reader["TB012_Fim"]);
                    retorno.TB012_StatusS = Convert.ToString(reader["TB012_Status"]);
                    retorno.TB012_CicloContrato = reader["TB012_CicloContrato"].ToString();
                    retorno.Unidade.TB020_id = Convert.ToInt64(reader["TB020_id"]);
                    retorno.Unidade.TB020_Documento = reader["TB020_Documento"] is DBNull ? "SEM CNPJ" : reader["TB020_Documento"].ToString().TrimEnd().TrimStart();
                    retorno.Unidade.TB020_NomeFantasia = reader["TB020_NomeFantasia"].ToString();
                    retorno.Unidade.TB020_RazaoSocial = reader["TB020_RazaoSocial"].ToString();
                    retorno.Unidade.TB020_Cep = reader["Unidade_CEP"].ToString();
                    retorno.Unidade.Estado.TB005_Id = Convert.ToInt64(reader["Unidade_TB005_Id"]);
                    retorno.Unidade.Municipio.TB006_id = Convert.ToInt64(reader["Unidade_TB006_id"]);
                    retorno.Unidade.TB020_Bairro = reader["Unidade_Bairro"].ToString();
                    retorno.Unidade.TB020_Logradouro = reader["Unidade_Logradouro"].ToString();
                    retorno.Unidade.TB020_Numero = reader["Unidade_Numero"].ToString();
                    retorno.Unidade.TB020_NomeExibicaoDetalhes = reader["TB020_NomeExibicaoDetalhes"].ToString();
                    retorno.Unidade.TB020_CategoriaExibicao = reader["TB020_CategoriaExibicao"].ToString();
                    retorno.Titular.TB013_id = Convert.ToInt64(reader["Titular_TB013_id"]);
                    retorno.Titular.TB013_CPFCNPJ = reader["Titular_TB013_CPFCNPJ"].ToString();
                    retorno.Titular.TB013_DataNascimento = Convert.ToDateTime(reader["Titular_TB013_DataNascimento"]);
                    retorno.Titular.TB013_Cartao = reader["Titular_Cartao"] is DBNull ? " " : reader["Titular_Cartao"].ToString();
                    retorno.Titular.TB013_RG = reader["Titular_TB013_RG"].ToString();
                    retorno.Titular.TB013_RGOrgaoEmissor = reader["Titular_TB013_RGOrgaoEmissor"].ToString();
                    retorno.Titular.TB013_SexoS = reader["Titular_TB013_Sexo"].ToString();
                    retorno.Titular.Pais.TB003_id = Convert.ToInt64(reader["Titular_TB003_id"]);
                    retorno.Titular.TB012_EraContezino = Convert.ToInt16(reader["Titular_TB012_EraContezino"]);
                    retorno.Titular.TB013_NomeCompleto = reader["Titular_TB013_NomeCompleto"].ToString();
                    retorno.Titular.TB013_IdProtheus = reader["Titular_TB013_IdProtheus"].ToString();
                    retorno.Titular.TB004_Cep = reader["Titular_TB004_Cep"].ToString();
                    retorno.Titular.Estado.TB005_Id = Convert.ToInt64(reader["Titular_TB005_Id"]);
                    retorno.Titular.Municipio.TB006_id = Convert.ToInt64(reader["Titular_TB006_id"]);
                    retorno.Titular.TB013_Bairro = reader["Titular_TB013_Bairro"].ToString();
                    retorno.Titular.TB013_Logradouro = reader["Titular_TB013_Logradouro"].ToString();
                    retorno.Titular.TB013_Numero = reader["Titular_TB013_Numero"].ToString();
                    retorno.Pais.TB003_id = Convert.ToInt64(reader["TB003_id"]);
                    retorno.PontoDeVenda.TB002_id = Convert.ToInt64(reader["TB002_id"]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }
        public bool ContratoCorporativoUpdade(ContratosController contratoCorporativo, long alteradoPor)
        {
            try
            {

                var sSql = new StringBuilder();

                sSql.Append(" UPDATE[dbo].[TB012_Contratos] SET ");
                sSql.Append(" [TB012_Inicio] = ");
                sSql.Append("'");
                sSql.Append(contratoCorporativo.TB012_Inicio.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ,[TB012_Fim] =  ");
                sSql.Append("'");
                sSql.Append(contratoCorporativo.TB012_Fim.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append("  ,[TB012_AlteradoEm] =  ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                sSql.Append("'");
                sSql.Append("  ,[TB012_AlteradoPor] = ");
                sSql.Append(alteradoPor);
                sSql.Append(" ,[TB004_Cep] =  ");
                sSql.Append("'");
                sSql.Append(contratoCorporativo.TB004_Cep.Replace("-", ""));
                sSql.Append("'");
                sSql.Append("  ,[TB006_id] = ");
                sSql.Append(contratoCorporativo.TB006_id);
                sSql.Append("  ,[TB012_Logradouro] =  ");
                sSql.Append("'");
                sSql.Append(contratoCorporativo.TB012_Logradouro.TrimEnd());
                sSql.Append("'");
                sSql.Append("  ,[TB012_Numero] = ");
                sSql.Append("'");
                sSql.Append(contratoCorporativo.TB012_Numero.TrimEnd());
                sSql.Append("'");
                sSql.Append(" ,[TB012_Bairro] = ");
                sSql.Append("'");
                sSql.Append(contratoCorporativo.TB012_Bairro.TrimEnd());
                sSql.Append("'");
                sSql.Append(" ,[TB012_Complemento] = ");
                sSql.Append("'");
                sSql.Append(contratoCorporativo.TB012_Complemento.TrimEnd());
                sSql.Append("'");
                sSql.Append("  ,[TB012_Status] = ");
                sSql.Append(contratoCorporativo.TB012_StatusS);
                sSql.Append(" WHERE TB012_id = ");
                sSql.Append(contratoCorporativo.TB012_Id);


                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }

            return true;
        }
        public bool ContratoAlterarPontoDeVenda(long tb012Id, long tb002Id)
        {
            try
            {
                var sSql = new StringBuilder();
                sSql.Append(" UPDATE TB012_Contratos SET ");
                sSql.Append(" TB002_id = ");
                sSql.Append(tb002Id);
                sSql.Append(" WHERE TB012_id =");
                sSql.Append(tb012Id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }
        /*Corporativo*/
        /// <summary>
        /// Descrição:  Incluir Novo Contrato
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public Int64 ContratoColaboradorInsert(ContratosController contrato)
        {
            int retorno;
            try
            {
                string insertSql = "INSERT INTO TB012_Contratos (TB002_id,TB012_Corporativo,TB013_id,TB012_Inicio,TB012_Fim,TB012_AceiteContrato,TB012_DataAceiteContrato,TB012_CadastradoEm,TB012_CadastradorPor,TB012_AlteradoEm,TB012_AlteradoPor,TB012_Status,TB004_Cep,TB006_id,TB012_Logradouro,TB012_Numero,TB012_Bairro,TB012_Complemento,TB012_TipoContrato,TB012_ProximoCodDependente,TB012_CicloContrato) VALUES (@TB002_id,@TB012_Corporativo,@TB013_id,@TB012_Inicio,@TB012_Fim,@TB012_AceiteContrato,@TB012_DataAceiteContrato,@TB012_CadastradoEm,@TB012_CadastradorPor,@TB012_AlteradoEm,@TB012_AlteradoPor,@TB012_Status,@TB004_Cep,@TB006_id,@TB012_Logradouro,@TB012_Numero,@TB012_Bairro,@TB012_Complemento,@TB012_TipoContrato,@TB012_ProximoCodDependente,@TB012_CicloContrato) SELECT SCOPE_IDENTITY()";

                using (var con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();

                    var command = new SqlCommand(insertSql, con);
                    command.CommandTimeout = 300;

                    command.Parameters.AddWithValue("@TB002_id", contrato.PontoDeVenda.TB002_id);
                    command.Parameters.AddWithValue("@TB012_Corporativo", contrato.TB012_Corporativo);
                    command.Parameters.AddWithValue("@TB013_id", contrato.Titular.TB013_id); //Titular do Contrato
                    command.Parameters.AddWithValue("@TB012_Inicio", contrato.TB012_Inicio);
                    command.Parameters.AddWithValue("@TB012_Fim", contrato.TB012_Fim);
                    command.Parameters.AddWithValue("@TB012_AceiteContrato", contrato.TB012_AceiteContrato);
                    command.Parameters.AddWithValue("@TB012_DataAceiteContrato", contrato.TB012_DataAceiteContrato);
                    command.Parameters.AddWithValue("@TB012_CadastradoEm", DateTime.Now);
                    command.Parameters.AddWithValue("@TB012_CadastradorPor", contrato.TB012_CadastradorPor);
                    command.Parameters.AddWithValue("@TB012_AlteradoEm", DateTime.Now);
                    command.Parameters.AddWithValue("@TB012_AlteradoPor", contrato.TB012_AlteradoPor);
                    command.Parameters.AddWithValue("@TB012_Status", contrato.TB012_StatusS);
                    command.Parameters.AddWithValue("@TB004_Cep", contrato.TB004_Cep.Replace("-", ""));
                    command.Parameters.AddWithValue("@TB006_id", contrato.TB006_id);
                    command.Parameters.AddWithValue("@TB012_Logradouro", contrato.TB012_Logradouro);
                    command.Parameters.AddWithValue("@TB012_Numero", contrato.TB012_Numero);
                    command.Parameters.AddWithValue("@TB012_Bairro", contrato.TB012_Bairro);
                    command.Parameters.AddWithValue("@TB012_Complemento", contrato.TB012_Complemento);
                    command.Parameters.AddWithValue("@TB012_TipoContrato", contrato.TB012_TipoContrato);
                    command.Parameters.AddWithValue("@TB012_ProximoCodDependente", 1001);
                    command.Parameters.AddWithValue("@TB012_CicloContrato", DateTime.Now.Year.ToString());

                    retorno = Convert.ToInt32(command.ExecuteScalar());

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;

        }
        public short ContratoVsAtual(long tb012Id)
        {
            short retorno = 0;

            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("SELECT TB012_VSContrato FROM dbo.TB012_Contratos WHERE TB012_id =");
                sSql.Append(tb012Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    retorno = Convert.ToInt16(reader["TB012_VSContrato"]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }
        public bool ContratoVsAtualizar(long tb012Id, short tb012VsContrato, long tb011Id)
        {
            try
            {
                var sSql = new StringBuilder();
                sSql.Append(" UPDATE TB012_Contratos SET ");
                sSql.Append("TB012_VSContrato = ");
                sSql.Append(tb012VsContrato);
                sSql.Append(",TB012_Edicao = 0");
                sSql.Append(",TB012_AlteradoEm = ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                sSql.Append("'");
                sSql.Append(",TB012_AlteradoPor = ");
                sSql.Append(tb011Id);
                sSql.Append(" WHERE TB012_id =");
                sSql.Append(tb012Id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }
        public bool ContratoInformarEdicao(long tb012Id, long tb011Id)
        {
            try
            {
                var sSql = new StringBuilder();
                sSql.Append(" UPDATE TB012_Contratos SET ");
                sSql.Append("TB012_Edicao  = ");
                sSql.Append(1);
                sSql.Append(",TB012_AlteradoEm  = ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                sSql.Append("'");
                sSql.Append(",TB012_AlteradoPor  = ");
                sSql.Append(tb011Id);
                sSql.Append(" WHERE TB012_id =");
                sSql.Append(tb012Id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }
        public List<ContratosController> ListarParcelasParaRenovacao(Int32 tb012TipoContrato, Int32 tb012CicloContrato, Int32 top)
        {
            List<ContratosController> retorno = new List<ContratosController>();
            ParcelaDao parcelaD = new ParcelaDao();

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSqlAtivos = new StringBuilder();


                sSqlAtivos.Append(" SELECT TOP (");
                sSqlAtivos.Append(top);
                sSqlAtivos.Append(" ) dbo.TB012_Contratos.TB012_id ");
                sSqlAtivos.Append(" , dbo.TB013_Pessoa.TB013_CPFCNPJ ");
                sSqlAtivos.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto ");
                sSqlAtivos.Append(" , dbo.TB013_Pessoa.TB013_Tipo ");
                sSqlAtivos.Append(" , dbo.TB002_PontosDeVenda.TB002_id ");
                sSqlAtivos.Append(" , dbo.TB002_PontosDeVenda.TB002_Ponto ");
                sSqlAtivos.Append(" , dbo.TB012_Contratos.TB013_id ");
                sSqlAtivos.Append("  , dbo.TB012_Contratos.TB012_Inicio ");
                sSqlAtivos.Append(" , dbo.TB012_Contratos.TB012_Status ");
                sSqlAtivos.Append(" FROM     ");
                sSqlAtivos.Append(" dbo.TB012_Contratos  ");
                sSqlAtivos.Append(" INNER JOIN ");
                sSqlAtivos.Append(" dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id  ");
                sSqlAtivos.Append(" INNER JOIN ");
                sSqlAtivos.Append(" dbo.TB002_PontosDeVenda ON dbo.TB012_Contratos.TB002_id = dbo.TB002_PontosDeVenda.TB002_id ");
                sSqlAtivos.Append(" WHERE dbo.TB012_Contratos.TB012_TipoContrato = ");
                sSqlAtivos.Append(tb012TipoContrato);
                sSqlAtivos.Append(" AND dbo.TB012_Contratos.TB012_CicloContrato = ");
                sSqlAtivos.Append(tb012CicloContrato);
                sSqlAtivos.Append(" AND dbo.TB012_Contratos.TB012_Status = 1  ");
                sSqlAtivos.Append(" GROUP BY dbo.TB012_Contratos.TB012_id ");
                sSqlAtivos.Append(" , dbo.TB013_Pessoa.TB013_CPFCNPJ ");
                sSqlAtivos.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto ");
                sSqlAtivos.Append(" , dbo.TB013_Pessoa.TB013_Tipo ");
                sSqlAtivos.Append(" , dbo.TB002_PontosDeVenda.TB002_id ");
                sSqlAtivos.Append(" , dbo.TB002_PontosDeVenda.TB002_Ponto ");
                sSqlAtivos.Append(" , dbo.TB012_Contratos.TB013_id ");
                sSqlAtivos.Append(" , dbo.TB012_Contratos.TB012_Inicio ");
                sSqlAtivos.Append(" , dbo.TB012_Contratos.TB012_Status ");
                sSqlAtivos.Append(" ORDER BY dbo.TB012_Contratos.TB012_Inicio ");
               
                SqlCommand command = new SqlCommand(sSqlAtivos.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var obj = new ContratosController
                    {
                        TB012_Id = Convert.ToInt64(reader["TB012_Id"]),
                        TB013_id = Convert.ToInt64(reader["TB013_id"]),
                        TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString(),
                        TB013_CPFCNPJ = Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart())
                            .ToString(Convert.ToInt16(reader["TB013_Tipo"]) == 1
                                ? @"000\.000\.000\-00"
                                : @"00\.000\.000\/0000\-00"),
                        TB002_id = Convert.ToInt64(reader["TB002_id"]),
                        TB002_Ponto = reader["TB002_Ponto"].ToString(),
                        TB012_Inicio = Convert.ToDateTime(reader["TB012_Inicio"])
                    };

                    var ultimaParcela = parcelaD.UltimaParcelaRenovacao(obj.TB012_Id);
                    obj.TB006_id = ultimaParcela.TB016_id;
                    obj.TB016_Vencimento = ultimaParcela.TB016_Vencimento;
                    var plano = parcelaD.ValorParcela(ultimaParcela.TB016_id);

                    obj.TB015_Plano = plano.TB015_Plano;
                    obj.TB016_Valor = plano.TB016_Valor;

                    retorno.Add(obj);
                }
                con.Close();


                StringBuilder sSqlVencidos = new StringBuilder();
                DateTime DataReferencia = DateTime.Now.AddDays(-60);


                sSqlVencidos.Append(" SELECT TOP ( ");
                sSqlVencidos.Append(top);
                sSqlVencidos.Append(" ) dbo.TB012_Contratos.TB012_id ");
                sSqlVencidos.Append(" , dbo.TB013_Pessoa.TB013_CPFCNPJ ");
                sSqlVencidos.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto ");
                sSqlVencidos.Append(" , dbo.TB013_Pessoa.TB013_Tipo ");
                sSqlVencidos.Append(" , dbo.TB002_PontosDeVenda.TB002_id ");
                sSqlVencidos.Append(" , dbo.TB002_PontosDeVenda.TB002_Ponto ");
                sSqlVencidos.Append(" , dbo.TB012_Contratos.TB013_id ");
                sSqlVencidos.Append(" , dbo.TB012_Contratos.TB012_Inicio ");
                sSqlVencidos.Append(" , dbo.TB012_Contratos.TB012_Status ");
                sSqlVencidos.Append(" , MIN(dbo.TB016_Parcela.TB016_Vencimento) AS Vencimento ");
                sSqlVencidos.Append(" FROM  ");
                sSqlVencidos.Append(" dbo.TB012_Contratos  ");
                sSqlVencidos.Append(" INNER JOIN ");
                sSqlVencidos.Append(" dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id  ");
                sSqlVencidos.Append("  INNER JOIN ");
                sSqlVencidos.Append("  dbo.TB002_PontosDeVenda ON dbo.TB012_Contratos.TB002_id = dbo.TB002_PontosDeVenda.TB002_id  ");
                sSqlVencidos.Append(" INNER JOIN ");
                sSqlVencidos.Append(" dbo.TB016_Parcela ON dbo.TB012_Contratos.TB012_id = dbo.TB016_Parcela.TB012_id ");
                sSqlVencidos.Append(" WHERE dbo.TB012_Contratos.TB012_CicloContrato =  ");
                sSqlVencidos.Append(tb012CicloContrato);
                sSqlVencidos.Append(" AND dbo.TB012_Contratos.TB012_TipoContrato = ");
                sSqlVencidos.Append(tb012TipoContrato);
                sSqlVencidos.Append(" AND dbo.TB016_Parcela.TB016_Status = 4 ");
                sSqlVencidos.Append(" AND dbo.TB012_Contratos.TB012_Status = 4 ");
                sSqlVencidos.Append(" GROUP BY dbo.TB012_Contratos.TB012_id ");
                sSqlVencidos.Append(" , dbo.TB013_Pessoa.TB013_CPFCNPJ ");
                sSqlVencidos.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto ");
                sSqlVencidos.Append(" , dbo.TB013_Pessoa.TB013_Tipo ");
                sSqlVencidos.Append(" , dbo.TB002_PontosDeVenda.TB002_id ");
                sSqlVencidos.Append(" , dbo.TB002_PontosDeVenda.TB002_Ponto ");
                sSqlVencidos.Append(" , dbo.TB012_Contratos.TB013_id ");
                sSqlVencidos.Append(" , dbo.TB012_Contratos.TB012_Inicio ");
                sSqlVencidos.Append(" , dbo.TB012_Contratos.TB012_Status ");
                sSqlVencidos.Append(" ORDER BY dbo.TB012_Contratos.TB012_Inicio ");

                command = new SqlCommand(sSqlVencidos.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime VencimentoParcela = Convert.ToDateTime(reader["Vencimento"]);
                    if (VencimentoParcela>= DataReferencia)
                    { 
                        var obj = new ContratosController
                        {
                            TB012_Id = Convert.ToInt64(reader["TB012_Id"]),
                            TB013_id = Convert.ToInt64(reader["TB013_id"]),
                            TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString(),
                            TB013_CPFCNPJ = Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart())
                                .ToString(Convert.ToInt16(reader["TB013_Tipo"]) == 1
                                    ? @"000\.000\.000\-00"
                                    : @"00\.000\.000\/0000\-00"),
                            TB002_id = Convert.ToInt64(reader["TB002_id"]),
                            TB002_Ponto = reader["TB002_Ponto"].ToString(),
                            TB012_Inicio = Convert.ToDateTime(reader["TB012_Inicio"])
                        };

                        var ultimaParcela = parcelaD.UltimaParcelaRenovacao(obj.TB012_Id);
                        obj.TB006_id = ultimaParcela.TB016_id;
                        obj.TB016_Vencimento = ultimaParcela.TB016_Vencimento;
                        var plano = parcelaD.ValorParcela(ultimaParcela.TB016_id);

                        obj.TB015_Plano = plano.TB015_Plano;
                        obj.TB016_Valor = plano.TB016_Valor;

                        retorno.Add(obj);
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }
        /// <summary>
        /// Descrição:  Atualizafr Ciclo do contrato
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       25/07/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool CicloContratoAtualizar(long tb012Id, Int32 tb012CicloContrato, long tb011Id, DateTime tb012Fim)
        {
            try
            {
                var sSql = new StringBuilder();
                sSql.Append(" UPDATE TB012_Contratos SET ");
                sSql.Append(" TB012_CicloContrato = ");
                sSql.Append(tb012CicloContrato);
                sSql.Append(" ,TB012_AlteradoEm = ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(" ,TB012_Fim = ");
                sSql.Append("'");
                sSql.Append(tb012Fim.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(",TB012_AlteradoPor=");
                sSql.Append(tb011Id);
                sSql.Append(" WHERE TB012_id =");
                sSql.Append(tb012Id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }
        public List<ContratosController> ListarParcelasParaExportacao(Int32 tb012TipoContrato, Int32 tb012CicloContrato, Int32 top)
        {
            List<ContratosController> retorno = new List<ContratosController>();

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" SELECT TOP (");
                sSql.Append(top);
                sSql.Append(" ) dbo.TB012_Contratos.TB012_CicloContrato, dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB013_Pessoa.TB013_Status, dbo.TB012_Contratos.TB012_id, COUNT(dbo.TB016_Parcela.TB016_id) ");
                sSql.Append(" AS TB012_NParcelas, dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB013_Pessoa.TB013_Tipo, dbo.TB006_Municipio.TB006_id, dbo.TB006_Municipio.TB006_Lote ");
                sSql.Append(" FROM dbo.TB012_Contratos INNER JOIN ");
                sSql.Append(" dbo.TB016_Parcela ON dbo.TB012_Contratos.TB012_id = dbo.TB016_Parcela.TB012_id INNER JOIN ");
                sSql.Append(" dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio ON dbo.TB012_Contratos.TB006_id = dbo.TB006_Municipio.TB006_id ");
                sSql.Append(" WHERE dbo.TB016_Parcela.TB012_CicloContrato =  ");
                sSql.Append(tb012CicloContrato);
                sSql.Append(" AND dbo.TB016_Parcela.TB016_Status = 2 ");
                sSql.Append(" AND dbo.TB016_Parcela.TB016_LoteExportacao = 0 ");
                sSql.Append(" GROUP BY dbo.TB012_Contratos.TB012_id, dbo.TB012_Contratos.TB012_CicloContrato, dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB013_Pessoa.TB013_Status,  ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_Tipo, dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB006_Municipio.TB006_id, dbo.TB006_Municipio.TB006_Lote ");
                sSql.Append(" HAVING dbo.TB012_Contratos.TB012_CicloContrato =  ");
                sSql.Append(tb012CicloContrato);
                sSql.Append(" AND dbo.TB013_Pessoa.TB013_Status < 2  AND dbo.TB012_Contratos.TB012_TipoContrato =  ");
                sSql.Append(tb012TipoContrato);
                sSql.Append(" ORDER BY dbo.TB012_Contratos.TB012_id, dbo.TB006_Municipio.TB006_id, dbo.TB006_Municipio.TB006_Lote ");

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var obj = new ContratosController
                    {
                        TB012_Id = Convert.ToInt64(reader["TB012_Id"]),
                        TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString(),
                        TB013_CPFCNPJ = Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart())
                            .ToString(Convert.ToInt16(reader["TB013_Tipo"]) == 1
                                ? @"000\.000\.000\-00"
                                : @"00\.000\.000\/0000\-00"),
                        TB012_NParcelas = Convert.ToInt64(reader["TB012_NParcelas"])
                    };



                    retorno.Add(obj);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }
        public bool CorporativoCancelarColaborador(long tb012Id, long tb011Id, double valorUnitario, long corporativo)
        {
            var con = new SqlConnection(ParametrosDAO.StringConexao);

            var cmdTb012 = con.CreateCommand();
            var cmdTb013 = con.CreateCommand();
            var cmdTb016 = con.CreateCommand();
            var cmdTb017 = con.CreateCommand();

            var sSqltb012 = new StringBuilder();
            sSqltb012.Append("UPDATE TB013_Pessoa SET ");
            sSqltb012.Append("TB012_Corporativo = 0");
            sSqltb012.Append(",TB013_CadastradoEm = ");
            sSqltb012.Append("'");
            sSqltb012.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            sSqltb012.Append("'");
            sSqltb012.Append(",TB013_CadastradoPor = ");
            sSqltb012.Append(tb011Id);
            sSqltb012.Append(" where TB012_id=");
            sSqltb012.Append(tb012Id);

            var sSqltb013 = new StringBuilder();
            sSqltb013.Append(" UPDATE TB012_Contratos SET ");
            sSqltb013.Append(" TB012_Corporativo = 0 ");
            sSqltb013.Append(",TB012_AlteradoEm =");
            sSqltb013.Append("'");
            sSqltb013.Append(DateTime.Now.ToString("MM/dd/yyy hh:mm"));
            sSqltb013.Append("'");
            sSqltb013.Append(",TB012_AlteradoPor =");
            sSqltb013.Append(tb011Id);
            sSqltb013.Append(",TB012_Status = 5");
            sSqltb013.Append(" where TB012_Corporativo = ");
            sSqltb013.Append(tb012Id);

            var sSqltb016 = new StringBuilder();
            sSqltb016.Append(" UPDATE TB016_Parcela SET ");
            sSqltb016.Append(" TB016_Valor = TB016_Valor -");
            sSqltb016.Append(valorUnitario.ToString(CultureInfo.InvariantCulture).Replace(".", "").Replace(",", "."));
            sSqltb016.Append(" where  TB016_Status = 0 and TB012_id = ");
            sSqltb016.Append(corporativo);

            var sSqltb017 = new StringBuilder();
            sSqltb017.Append(" UPDATE TB017_ParcelaProduto  ");
            sSqltb017.Append(" SET  TB017_ValorFinal = TB017_ValorFinal - ");
            sSqltb017.Append(valorUnitario.ToString(CultureInfo.InvariantCulture).Replace(".", "").Replace(",", "."));
            sSqltb017.Append(" SELECT ");
            sSqltb017.Append(" dbo.TB017_ParcelaProduto.TB017_id ");
            sSqltb017.Append(" FROM ");
            sSqltb017.Append(" dbo.TB016_Parcela ");
            sSqltb017.Append(" INNER JOIN ");
            sSqltb017.Append(" dbo.TB017_ParcelaProduto ");
            sSqltb017.Append(" ON dbo.TB016_Parcela.TB016_id = dbo.TB017_ParcelaProduto.TB016_id ");
            sSqltb017.Append(" WHERE        dbo.TB016_Parcela.TB016_Status = 0 ");
            sSqltb017.Append(" AND ");
            sSqltb017.Append(" dbo.TB016_Parcela.TB012_id = ");
            sSqltb017.Append(corporativo);

            cmdTb012.CommandText = sSqltb012.ToString();
            cmdTb013.CommandText = sSqltb013.ToString();
            cmdTb016.CommandText = sSqltb016.ToString();
            cmdTb017.CommandText = sSqltb017.ToString();

            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmdTb012.Transaction = tran;
                cmdTb012.ExecuteNonQuery();
                //Comando 1 executado com sucesso!

                cmdTb013.Transaction = tran;
                cmdTb013.ExecuteNonQuery();
                //Comando 2 executado com sucesso!

                if (valorUnitario > 0)
                {
                    cmdTb016.Transaction = tran;
                    cmdTb016.ExecuteNonQuery();
                    //Comando 3 executado com sucesso!

                    cmdTb017.Transaction = tran;
                    cmdTb017.ExecuteNonQuery();
                    //Comando 4 executado com sucesso!

                }

                tran.Commit();
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return true;
        }
        public long ContadorContratoCorporativoFamiliar(long tb012Id)
        {
            long contador = 0;

            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT TB012_Corporativo FROM dbo.TB013_Pessoa WHERE TB012_Corporativo = ");
                sSql.Append(tb012Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contador++;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return contador;
        }
        public long ContadorCicloAtual(long tb012Id)
        {
            long ciclo = 0;

            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT TB012_id, TB012_CicloContrato FROM dbo.TB012_Contratos WHERE TB012_id = ");
                sSql.Append(tb012Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ciclo = Convert.ToInt64(reader["TB012_CicloContrato"]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return ciclo;
        }
        public bool ContratoCoprorativoAoTitularFamiliar(long tb012Id, long tb013Id)
        {
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("update ");
                sSql.Append(" TB012_Contratos");
                sSql.Append(" set ");
                sSql.Append(" TB013_id = ");
                sSql.Append(tb013Id);
                sSql.Append(" WHERE TB012_id = ");
                sSql.Append(tb012Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                command.ExecuteReader();



                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;
        }
        public bool SetarContratoInadimplente()
        {
            //  var pessoaControllers = new List<PessoaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" update  ");
                sSql.Append(" TB012_Contratos ");
                sSql.Append(" set TB012_Status = 4 ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB016_Parcela ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB012_Contratos ");
                sSql.Append(" ON ");
                sSql.Append(" dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id ");
                sSql.Append(" WHERE ");
                sSql.Append(" dbo.TB016_Parcela.TB016_Status = 4 ");
                sSql.Append(" AND ");
                sSql.Append(" dbo.TB012_Contratos.TB012_Status = 1 ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                command.ExecuteReader();

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;
        }
        /// <summary>
        /// Descrição: Verifica se o numero da sorte já existe
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool NumeroDaSorte(long numero)
        {
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("SELECT TB012_NumeroDaSorte FROM dbo.TB012_Contratos WHERE TB012_NumeroDaSorte =");
                sSql.Append(numero);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return true;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return false;
        }
        #region Financeiro
        /// <summary>
        /// Descrição:  Lista os contratos para o financeiro
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       06/10/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<ContratosController> ContratosFinanceiro(string query)
        {
            var retornoList = new List<ContratosController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();


                sSql.Append(" SELECT dbo.TB012_Contratos.TB012_CicloContrato, dbo.TB012_Contratos.TB012_id, dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB012_Contratos.TB012_Inicio,  ");
                sSql.Append(" dbo.TB012_Contratos.TB012_Fim, dbo.TB012_Contratos.TB012_CicloContrato, dbo.TB012_Contratos.TB012_Status, dbo.TB012_Contratos.TB012_DiaVencimento, dbo.TB012_Contratos.TB012_AlteradoEm, ");
                sSql.Append(" dbo.TB011_APPUsuarios.TB011_NomeExibicao, dbo.TB013_Pessoa.TB013_Tipo, dbo.TB011_APPUsuarios.TB011_NomeCompleto ");
                sSql.Append(" FROM dbo.TB012_Contratos INNER JOIN ");
                sSql.Append(" dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id INNER JOIN ");
                sSql.Append(" dbo.TB011_APPUsuarios ON dbo.TB012_Contratos.TB012_AlteradoPor = dbo.TB011_APPUsuarios.TB011_Id");
                sSql.Append(" WHERE ");
                sSql.Append(query);
                sSql.Append(" ORDER BY dbo.TB011_APPUsuarios.TB011_NomeCompleto, dbo.TB012_Contratos.TB012_id ");

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new ContratosController
                    {
                        TB012_Id = Convert.ToInt64(reader["TB012_id"]),
                        TB012_CicloContrato = reader["TB012_CicloContrato"].ToString(),
                        TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd().TrimStart().ToUpper(),
                        TB012_AlteradoEm = Convert.ToDateTime(reader["TB012_AlteradoEm"].ToString()),
                        TB011_NomeExibicao = reader["TB011_NomeExibicao"].ToString().TrimEnd().TrimStart().ToUpper(),
                        TB012_StatusS = reader["TB012_Status"] is DBNull
                            ? "0"
                            : Enum.GetName(typeof(ContratosController.TB012_StatusE),
                                Convert.ToInt16(reader["TB012_Status"]))
                    };



                    var temp = reader["TB013_CPFCNPJ"].ToString();


                    if (Convert.ToInt16(reader["TB013_Tipo"]) == 1)
                    {
                        obj.TB012_TipoContratoS = @"Familiar";
                    }
                    if (Convert.ToInt16(reader["TB013_Tipo"]) == 2)
                    {
                        obj.TB012_TipoContratoS = @"Parceiro";
                    }
                    if (Convert.ToInt16(reader["TB013_Tipo"]) == 3)
                    {
                        obj.TB012_TipoContratoS = @"Corporativo";
                    }
                    if (Convert.ToInt16(reader["TB013_Tipo"]) == 4)
                    {
                        obj.TB012_TipoContratoS = @"Familiar Corporativo";
                    }
                    if (Convert.ToInt16(reader["TB013_Tipo"]) == 5)
                    {
                        obj.TB012_TipoContratoS = @"Familiar Parceiro";
                    }

                    if (reader["TB013_CPFCNPJ"].ToString().Trim() == string.Empty)
                    {
                        obj.TB013_CPFCNPJ = "SEM CPF";
                    }
          
                    else
                    {
                        if (Convert.ToInt16(reader["TB013_Tipo"]) == 1)
                        {
                            if (reader["TB013_CPFCNPJ"].ToString().Trim() == "SEM CPF")
                            {
                                obj.TB013_CPFCNPJ = reader["TB013_CPFCNPJ"].ToString();
                            }
                            else
                            {
                                obj.TB013_CPFCNPJ = reader["TB013_CPFCNPJ"] is DBNull
                                    ? "SEM CPF"
                                    : Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart())
                                        .ToString(@"000\.000\.000\-00");
                            }
                        }
                        else
                        {
                            obj.TB013_CPFCNPJ = Convert
                                .ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart())
                                .ToString(@"00\.000\.000\/0000\-00");
                        }
                    }

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        public bool CicloContratoAlterar(long tb012Id, Int32 tb012CicloContrato, long tb011Id)
        {
            try
            {
                var sSql = new StringBuilder();
                sSql.Append(" UPDATE TB012_Contratos SET ");
                sSql.Append(" TB012_CicloContrato = ");
                sSql.Append(tb012CicloContrato);
                sSql.Append(" ,TB012_AlteradoEm = ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(",TB012_AlteradoPor=");
                sSql.Append(tb011Id);
                sSql.Append(" WHERE TB012_id =");
                sSql.Append(tb012Id);

                using (var con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    var myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }
        public bool ContratoAlterarInicioFim(long tb012Id, DateTime inicio, DateTime fim, long tb011Id)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" UPDATE TB012_Contratos SET ");
                sSql.Append("TB012_Inicio  = ");
                sSql.Append("'");
                sSql.Append(inicio.ToString("MM/dd/yyy"));
                sSql.Append("'");
                sSql.Append(",TB012_Fim  = ");
                sSql.Append("'");
                sSql.Append(fim.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(",TB012_AlteradoEm  = ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                sSql.Append("'");
                sSql.Append(",TB012_AlteradoPor  = ");
                sSql.Append(tb011Id);
                sSql.Append(" WHERE TB012_id =");
                sSql.Append(tb012Id);

                using (var con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    var myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }

        public bool ContratoAlterarLocalCadastro(long tb012Id, long TB002_id, long tb011Id)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" UPDATE TB012_Contratos SET ");
                sSql.Append("TB002_id  = ");           
                sSql.Append(TB002_id);
                sSql.Append(",TB012_AlteradoEm  = ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                sSql.Append("'");
                sSql.Append(",TB012_AlteradoPor  = ");
                sSql.Append(tb011Id);
                sSql.Append(" WHERE TB012_id =");
                sSql.Append(tb012Id);

                using (var con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    var myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }
        #endregion
    }
}
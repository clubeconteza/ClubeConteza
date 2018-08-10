using Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace DAO
{
    public class PontoDeVendaDao
    {
        /// <summary>
        /// Descrição:  Retorna lista Pontos de Venda por Empresa a qual o usuário possui acesso
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       08/11/2015
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public DataSet PontosDeVendaLiberadosParaUsuario(UsuarioAPPController filtro)
        {
            var dsRetorno = new DataSet();
            try
            {
                var sSql = new StringBuilder();
                sSql.Append("SELECT ");
                sSql.Append("dbo.TB011_TB002.TB002_id,");
                sSql.Append("dbo.TB002_PontosDeVenda.TB002_Ponto");
                sSql.Append(" FROM ");
                sSql.Append("dbo.TB011_TB002");
                sSql.Append(" INNER JOIN ");
                sSql.Append("dbo.TB002_PontosDeVenda ON dbo.TB011_TB002.TB002_id = dbo.TB002_PontosDeVenda.TB002_id");
                sSql.Append(" WHERE ");
                sSql.Append("dbo.TB011_TB002.TB011_Id =");
                sSql.Append(filtro.TB011_Id);
                sSql.Append(" AND ");
                sSql.Append("dbo.TB002_PontosDeVenda.TB002_Status = 1"); //Somente Pontos de Venda Ativos
                sSql.Append(" ORDER BY ");
                sSql.Append("dbo.TB002_PontosDeVenda.TB001_id,"); //Crescente 1
                sSql.Append("dbo.TB011_TB002.TB002_id");          //Crescente 2

                var con = new SqlConnection(ParametrosDAO.StringConexao);

                con.Open();
                var da = new SqlDataAdapter(sSql.ToString(), con);
                dsRetorno.Tables.Add("TB002_id");
                dsRetorno.Tables.Add("TB002_Ponto");
                dsRetorno.EnforceConstraints = false;

                dsRetorno.Tables["TB002_id"].BeginLoadData();
                da.Fill(dsRetorno.Tables["TB002_id"]);
                dsRetorno.Tables["TB002_id"].EndLoadData();

                dsRetorno.Tables["TB002_Ponto"].BeginLoadData();
                da.Fill(dsRetorno.Tables["TB002_Ponto"]);
                dsRetorno.Tables["TB002_Ponto"].EndLoadData();
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return dsRetorno;
        }

        public PontoDeVendaController PontoDeVendaEmpresa(long tb002Id)
        {
            var retorno = new PontoDeVendaController { Empresa = new EmpresaController() };

            try
            {
                var sSql = new StringBuilder();

                sSql.Append(" SELECT  ");
                sSql.Append(" dbo.TB002_PontosDeVenda.TB002_id ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB001_id ");
                sSql.Append(" ,dbo.TB001_Empresa.TB001_RazaoSocial ");
                sSql.Append(" ,dbo.TB001_Empresa.TB001_CNPJ ");
                sSql.Append(" ,dbo.TB001_Empresa.TB001_Cep ");
                sSql.Append(" ,dbo.TB001_Empresa.TB001_Municipio_ID ");
                sSql.Append(" ,dbo.TB001_Empresa.TB001_Logradouro ");
                sSql.Append(" ,dbo.TB001_Empresa.TB001_Numero ");
                sSql.Append(" ,dbo.TB006_Municipio.TB006_Municipio ");
                sSql.Append(" ,dbo.TB005_Estado.TB005_Sigla ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_FamiliarAdesaoForma ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_FamiliarAdesaoValor ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_FamiliarAdesaoAliquota  ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_FamiliarMensalidadeForma ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_FamiliarMensalidadeValor ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_FamiliarMensalidadeAliquota  ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_ParceiroAdesaoForma ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_ParceiroAdesaoValor ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_ParceiroAdesaoAliquota  ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_ParceiroMensalidadeForma ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_ParceiroMensalidadeValor ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_ParceiroMensalidadeAliquota ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_CorporativoAdesaoForma ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_CorporativoAdesaoValor ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_CorporativoAdesaoAliquota ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_CorporativoMensalidadeForma ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_CorporativoMensalidadeValor ");
                sSql.Append(" ,dbo.TB002_PontosDeVenda.TB002_CorporativoMensalidadeAliquota ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB002_PontosDeVenda ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB001_Empresa  ");
                sSql.Append(" ON  ");
                sSql.Append(" dbo.TB002_PontosDeVenda.TB001_id = dbo.TB001_Empresa.TB001_id ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio ");
                sSql.Append(" ON  ");
                sSql.Append(" dbo.TB001_Empresa.TB001_Municipio_ID = dbo.TB006_Municipio.TB006_id  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" WHERE ");
                sSql.Append(" dbo.TB002_PontosDeVenda.TB002_id = ");
                sSql.Append(tb002Id);

                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.Empresa.TB001_id                    = Convert.ToInt64(reader["TB001_id"]);
                    retorno.Empresa.TB001_RazaoSocial           = reader["TB001_RazaoSocial"].ToString().TrimEnd();
                    retorno.Empresa.Cidade                      = reader["TB006_Municipio"].ToString().TrimEnd();
                    retorno.Empresa.TB001_CNPJ                  = Convert.ToUInt64(reader["TB001_CNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"00\.000\.000\/0000\-00");
                    retorno.Empresa.TB001_Logradouro            = reader["TB001_Logradouro"].ToString().TrimEnd() + ", " + reader["TB001_Numero"].ToString().TrimEnd();
                    retorno.Empresa.TB001_UF                    = reader["TB005_Sigla"].ToString().TrimEnd();
                    retorno.Tb002FamiliarAdesaoFormaS           = reader["TB002_FamiliarAdesaoForma"].ToString();
                    retorno.Tb002FamiliarAdesaoValor            = Convert.ToDouble( reader["TB002_FamiliarAdesaoValor"].ToString());
                    retorno.Tb002FamiliarAdesaoAliquota         = Convert.ToDouble(reader["TB002_FamiliarAdesaoAliquota"].ToString());
                    retorno.Tb002FamiliarMensalidadeFormaS      = reader["TB002_FamiliarMensalidadeForma"].ToString();
                    retorno.Tb002FamiliarMensalidadeValor       = Convert.ToDouble(reader["TB002_FamiliarMensalidadeValor"].ToString());
                    retorno.Tb002FamiliarMensalidadeAliquota    = Convert.ToDouble(reader["TB002_FamiliarMensalidadeAliquota"].ToString());
                    retorno.Tb002ParceiroAdesaoFormaS           = reader["TB002_ParceiroAdesaoForma"].ToString();
                    retorno.Tb002ParceiroAdesaoValor            = Convert.ToDouble(reader["TB002_ParceiroAdesaoValor"].ToString());
                    retorno.Tb002ParceiroAdesaoAliquota         = Convert.ToDouble(reader["TB002_ParceiroAdesaoAliquota"].ToString());
                    retorno.Tb002ParceiroMensalidadeFormaS      = reader["TB002_ParceiroMensalidadeForma"].ToString();
                    retorno.Tb002ParceiroMensalidadeValor       = Convert.ToDouble(reader["TB002_ParceiroMensalidadeValor"].ToString());
                    retorno.Tb002ParceiroMensalidadeAliquota    = Convert.ToDouble(reader["TB002_ParceiroMensalidadeAliquota"].ToString());
                    retorno.Tb002CorporativoAdesaoFormaS        = reader["TB002_CorporativoAdesaoForma"].ToString();
                    retorno.Tb002CorporativoAdesaoValor         = Convert.ToDouble(reader["TB002_CorporativoAdesaoValor"].ToString());
                    retorno.Tb002CorporativoAdesaoAliquota      = Convert.ToDouble(reader["TB002_CorporativoAdesaoAliquota"].ToString());
                    retorno.Tb002CorporativoMensalidadeFormaS   = reader["TB002_CorporativoMensalidadeForma"].ToString();
                    retorno.Tb002CorporativoMensalidadeValor    = Convert.ToDouble(reader["TB002_CorporativoMensalidadeValor"].ToString());
                    retorno.Tb002CorporativoMensalidadeAliquota = Convert.ToDouble(reader["TB002_CorporativoMensalidadeAliquota"].ToString());
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
        /// Descrição:  Retorna lista com detalhes dos pontos de venda
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       30/10/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<PontoDeVendaController> PontoDeVendaLista()
        {
            var retornoList = new List<PontoDeVendaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT dbo.TB002_PontosDeVenda.TB002_id, dbo.TB001_Empresa.TB001_NomeFantasia, dbo.TB002_PontosDeVenda.TB002_Ponto, dbo.TB002_PontosDeVenda.TB002_FamiliarAdesaoForma,  ");
                sSql.Append(" dbo.TB002_PontosDeVenda.TB002_FamiliarAdesaoValor, dbo.TB002_PontosDeVenda.TB002_FamiliarAdesaoAliquota, dbo.TB002_PontosDeVenda.TB002_FamiliarMensalidadeForma, dbo.TB002_PontosDeVenda.TB002_FamiliarMensalidadeValor, ");
                sSql.Append(" dbo.TB002_PontosDeVenda.TB002_FamiliarMensalidadeAliquota, dbo.TB002_PontosDeVenda.TB002_CadastradoEm, dbo.TB002_PontosDeVenda.TB002_CadastradoPor, dbo.TB002_PontosDeVenda.TB002_AlteradoEm, ");
                sSql.Append(" dbo.TB002_PontosDeVenda.TB002_AlteradoPor, dbo.TB002_PontosDeVenda.TB002_Status, CadastradoPor.TB011_NomeExibicao AS NomeCadastradoPor, AlteradoPor.TB011_NomeExibicao AS NomeAlteradoPor ");
                sSql.Append(" FROM dbo.TB002_PontosDeVenda INNER JOIN ");
                sSql.Append(" dbo.TB001_Empresa ON dbo.TB002_PontosDeVenda.TB001_id = dbo.TB001_Empresa.TB001_id INNER JOIN ");
                sSql.Append(" dbo.TB011_APPUsuarios AS CadastradoPor ON dbo.TB002_PontosDeVenda.TB002_CadastradoPor = CadastradoPor.TB011_Id INNER JOIN ");
                sSql.Append(" dbo.TB011_APPUsuarios AS AlteradoPor ON dbo.TB002_PontosDeVenda.TB002_Status = AlteradoPor.TB011_Id ");
                sSql.Append(" ORDER BY dbo.TB001_Empresa.TB001_NomeFantasia, dbo.TB002_PontosDeVenda.TB002_Ponto ");

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PontoDeVendaController
                    {
                        Empresa = new EmpresaController
                        {
                            TB001_NomeFantasia = Convert.ToString(reader["TB001_NomeFantasia"])
                        },
                        TB002_id = Convert.ToInt64(reader["TB002_id"]),
                        Tb002FamiliarAdesaoAliquota = Convert.ToDouble(reader["TB002_FamiliarAdesaoAliquota"]),
                        Tb002StatusS = Enum.GetName(typeof(PontoDeVendaController.Tb002StatusE),
                            Convert.ToInt16(reader["TB002_Status"])),
                        TB002_Ponto = Convert.ToString(reader["TB002_Ponto"]),
                        Tb002FamiliarAdesaoValor = Convert.ToDouble(reader["TB002_FamiliarAdesaoValor"]),
                        Tb002FamiliarMensalidadeValor = Convert.ToDouble(reader["TB002_FamiliarMensalidadeValor"]),
                        Tb002FamiliarMensalidadeAliquota = Convert.ToDouble(reader["TB002_FamiliarMensalidadeAliquota"]),
                        Tb002CadastradoEm = Convert.ToDateTime(reader["TB002_CadastradoEm"]),
                        Tb002CadastradoPor = Convert.ToString(reader["NomeCadastradoPor"]),
                        Tb002AlteradoEm = Convert.ToDateTime(reader["TB002_AlteradoEm"]),
                        Tb002AlteradoPor = Convert.ToString(reader["NomeAlteradoPor"]),
                        Tb002FamiliarAdesaoFormaS = Enum.GetName(typeof(PontoDeVendaController.Tb002FamiliarAdesaoFormaE),
                            Convert.ToInt16(reader["TB002_FamiliarAdesaoForma"])),
                        Tb002FamiliarMensalidadeFormaS = Enum.GetName(typeof(PontoDeVendaController.Tb002FamiliarMensalidadeFormaE),
                            Convert.ToInt16(reader["TB002_FamiliarMensalidadeForma"]))
                    };

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

        public PontoDeVendaController PontoDeVenda(long tb002Id)
        {
            var retorno = new PontoDeVendaController { Empresa = new EmpresaController() };

            try
            {
                var sSql = new StringBuilder();

                sSql.Append("SELECT * ");
                sSql.Append(" FROM dbo.TB002_PontosDeVenda ");
                sSql.Append(" WHERE ");
                sSql.Append(" TB002_id = ");
                sSql.Append(tb002Id);

                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB002_id                                = Convert.ToInt64(reader["TB002_id"]);
                    retorno.Empresa.TB001_id                        = Convert.ToInt64(reader["TB001_id"]);
                    retorno.TB002_Ponto                             = reader["TB002_Ponto"].ToString().TrimEnd();
                    retorno.Tb002StatusS                            = reader["TB002_Status"].ToString().TrimEnd();
                    retorno.Tb002FamiliarAdesaoValor                = Convert.ToDouble(reader["TB002_FamiliarAdesaoValor"]);
                    retorno.Tb002FamiliarAdesaoAliquota             = Convert.ToDouble(reader["TB002_FamiliarAdesaoAliquota"]);
                    retorno.Tb002FamiliarMensalidadeValor           = Convert.ToDouble(reader["TB002_FamiliarMensalidadeValor"]);
                    retorno.Tb002FamiliarMensalidadeAliquota        = Convert.ToDouble(reader["TB002_FamiliarMensalidadeAliquota"]);
                    retorno.Tb002FamiliarAdesaoFormaS               = reader["TB002_FamiliarAdesaoForma"].ToString();
                    retorno.Tb002FamiliarMensalidadeFormaS          = reader["TB002_FamiliarMensalidadeForma"].ToString();
                    retorno.Tb002ParceiroAdesaoFormaS               = reader["TB002_ParceiroAdesaoForma"].ToString();
                    retorno.Tb002ParceiroAdesaoValor                = Convert.ToDouble(reader["TB002_ParceiroAdesaoValor"]);
                    retorno.Tb002ParceiroAdesaoAliquota             = Convert.ToDouble(reader["TB002_ParceiroAdesaoAliquota"]);
                    retorno.Tb002ParceiroMensalidadeFormaS          = reader["TB002_ParceiroMensalidadeForma"].ToString();
                    retorno.Tb002ParceiroMensalidadeValor           = Convert.ToDouble(reader["TB002_ParceiroMensalidadeValor"]);
                    retorno.Tb002ParceiroMensalidadeAliquota        = Convert.ToDouble(reader["TB002_ParceiroMensalidadeAliquota"]);
                    retorno.Tb002CorporativoAdesaoFormaS            = reader["TB002_CorporativoAdesaoForma"].ToString();
                    retorno.Tb002CorporativoAdesaoValor             = Convert.ToDouble(reader["TB002_CorporativoAdesaoValor"]);
                    retorno.Tb002CorporativoAdesaoAliquota          = Convert.ToDouble(reader["TB002_CorporativoAdesaoAliquota"]);
                    retorno.Tb002CorporativoMensalidadeFormaS       = reader["TB002_CorporativoMensalidadeForma"].ToString();
                    retorno.Tb002CorporativoMensalidadeValor        = Convert.ToDouble(reader["TB002_CorporativoMensalidadeValor"]);
                    retorno.Tb002CorporativoMensalidadeAliquota     = Convert.ToDouble(reader["TB002_CorporativoMensalidadeAliquota"]);
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

        public bool PontoDeVendaAtualizar(PontoDeVendaController ponto)
        {
            var con = new SqlConnection(ParametrosDAO.StringConexao);

            var cmdTb002 = con.CreateCommand();

            var sSqltb002 = new StringBuilder();
            sSqltb002.Append("update TB002_PontosDeVenda set ");
            sSqltb002.Append("TB002_Ponto =  ");
            sSqltb002.Append("'");
            sSqltb002.Append(ponto.TB002_Ponto.ToUpper().TrimEnd());
            sSqltb002.Append("'");
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_FamiliarAdesaoForma = ");
            sSqltb002.Append(ponto.Tb002FamiliarAdesaoFormaS);
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_FamiliarAdesaoValor = ");
            sSqltb002.Append(ponto.Tb002FamiliarAdesaoValor.ToString(CultureInfo.InvariantCulture).Replace(".", "").Replace(",", "."));
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_FamiliarAdesaoAliquota = ");
            sSqltb002.Append(ponto.Tb002FamiliarAdesaoAliquota);
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_FamiliarMensalidadeForma = ");
            sSqltb002.Append(ponto.Tb002FamiliarMensalidadeFormaS);
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_FamiliarMensalidadeValor = ");
            sSqltb002.Append(ponto.Tb002FamiliarMensalidadeValor.ToString(CultureInfo.InvariantCulture).Replace(".", "").Replace(",", "."));
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_FamiliarMensalidadeAliquota = ");
            sSqltb002.Append(ponto.Tb002FamiliarMensalidadeAliquota);
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_ParceiroAdesaoForma = ");
            sSqltb002.Append(ponto.Tb002ParceiroAdesaoFormaS);
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_ParceiroAdesaoValor = ");
            sSqltb002.Append(ponto.Tb002ParceiroAdesaoValor.ToString(CultureInfo.InvariantCulture).Replace(".", "").Replace(",", "."));
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_ParceiroAdesaoAliquota = ");
            sSqltb002.Append(ponto.Tb002ParceiroAdesaoAliquota);
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_ParceiroMensalidadeForma = ");
            sSqltb002.Append(ponto.Tb002ParceiroMensalidadeFormaS);
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_ParceiroMensalidadeValor = ");
            sSqltb002.Append(ponto.Tb002ParceiroMensalidadeValor.ToString(CultureInfo.InvariantCulture).Replace(".", "").Replace(",", "."));
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_ParceiroMensalidadeAliquota = ");
            sSqltb002.Append(ponto.Tb002ParceiroMensalidadeAliquota);
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_CorporativoAdesaoForma = ");
            sSqltb002.Append(ponto.Tb002CorporativoAdesaoFormaS);
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_CorporativoAdesaoValor = ");
            sSqltb002.Append(ponto.Tb002CorporativoAdesaoValor.ToString(CultureInfo.InvariantCulture).Replace(".", "").Replace(",", "."));
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_CorporativoAdesaoAliquota = ");
            sSqltb002.Append(ponto.Tb002CorporativoAdesaoAliquota);
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_CorporativoMensalidadeForma = ");
            sSqltb002.Append(ponto.Tb002CorporativoMensalidadeFormaS);
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_CorporativoMensalidadeValor = ");
            sSqltb002.Append(ponto.Tb002CorporativoMensalidadeValor.ToString(CultureInfo.InvariantCulture).Replace(".", "").Replace(",", "."));
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_CorporativoMensalidadeAliquota = ");
            sSqltb002.Append(ponto.Tb002CorporativoMensalidadeAliquota);
            sSqltb002.Append(",");
            sSqltb002.Append(" TB002_AlteradoEm =  ");
            sSqltb002.Append("'");
            sSqltb002.Append(DateTime.Now.ToString("MM/dd/yyyy"));
            sSqltb002.Append("'");
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_Status = ");
            sSqltb002.Append(ponto.Tb002StatusS);
            sSqltb002.Append(",");
            sSqltb002.Append("TB001_id = ");
            sSqltb002.Append(ponto.Empresa.TB001_id);
            sSqltb002.Append(",");
            sSqltb002.Append("TB002_AlteradoPor = ");
            sSqltb002.Append(ponto.Tb002AlteradoPorI);
            sSqltb002.Append(" where  TB002_id= ");
            sSqltb002.Append(ponto.TB002_id);
            cmdTb002.CommandText = sSqltb002.ToString();

            con.Open();
            var tran = con.BeginTransaction();
            try
            {
                cmdTb002.Transaction = tran;
                cmdTb002.ExecuteNonQuery();
                //Comando 1 executado com sucesso!
                tran.Commit();
            }
            catch (SqlException)
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                con.Close();

            }

            return true;
        }

        public List<UsuarioAPPController> PontoDeVendaUsuariosComAcesso(long tb002Id)
        {
            var retornoList = new List<UsuarioAPPController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT dbo.TB011_TB002.TB002_id, dbo.TB011_APPUsuarios.TB011_Id, dbo.TB011_APPUsuarios.TB011_NomeExibicao, dbo.TB010_Perfil.TB010_Perfil ");
                sSql.Append("  FROM  dbo.TB011_TB002 INNER JOIN ");
                sSql.Append(" dbo.TB011_APPUsuarios ON dbo.TB011_TB002.TB011_Id = dbo.TB011_APPUsuarios.TB011_Id INNER JOIN ");
                sSql.Append(" dbo.TB010_Perfil ON dbo.TB011_APPUsuarios.TB010_id = dbo.TB010_Perfil.TB010_id ");
                sSql.Append(" WHERE dbo.TB011_TB002.TB002_id =  ");
                sSql.Append(tb002Id);
                sSql.Append(" ORDER BY dbo.TB010_Perfil.TB010_Perfil, dbo.TB011_APPUsuarios.TB011_NomeExibicao ");
                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new UsuarioAPPController
                    {
                        TB011_Id = Convert.ToInt64(reader["TB011_Id"]),
                        TB011_NomeExibicao = Convert.ToString(reader["TB011_NomeExibicao"]).ToUpper().TrimEnd(),
                        TB010_Perfil = Convert.ToString(reader["TB010_Perfil"]).ToUpper().TrimEnd()
                    };

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

        public bool PontoDeVendaDeletarUsuariosComAcesso(long tb002Id, long tb011Id)
        {
            var con = new SqlConnection(ParametrosDAO.StringConexao);

            var cmdTb002 = con.CreateCommand();

            var sSqltb002 = new StringBuilder();
            sSqltb002.Append("delete from  TB011_TB002  ");
            sSqltb002.Append(" where  TB002_id= ");
            sSqltb002.Append(tb002Id);
            sSqltb002.Append(" and  TB011_Id= ");
            sSqltb002.Append(tb011Id);
            cmdTb002.CommandText = sSqltb002.ToString();

            con.Open();
            var tran = con.BeginTransaction();
            try
            {
                cmdTb002.Transaction = tran;
                cmdTb002.ExecuteNonQuery();
                //Comando 1 executado com sucesso!
                tran.Commit();
            }
            catch (SqlException)
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                con.Close();
            }

            return true;
        }

    }
}
using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class ContratoCorporativoDAO
    {
        /// <summary>
        /// Descrição:  Listar contratos corporativos para exportação
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       15/01/2018
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<ContratosController> corporativoListaParaExportacao(string query)
        {
            List<ContratosController> retornoList = new List<ContratosController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT  ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id ");
                sSql.Append(" ,dbo.TB012_Contratos.TB012_CicloContrato ");
                sSql.Append(" ,dbo.TB012_Contratos.TB012_Status ");
                sSql.Append(" ,dbo.TB012_Contratos.TB012_Edicao ");
                sSql.Append(" ,dbo.TB012_Contratos.TB012_AlteradoEm ");
                sSql.Append(" ,dbo.TB011_APPUsuarios.TB011_NomeExibicao ");
                sSql.Append(" ,dbo.TB020_Unidades.TB020_Documento ");
                sSql.Append(" ,dbo.TB020_Unidades.TB020_NomeFantasia ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB012_Contratos INNER JOIN ");
                sSql.Append(" dbo.TB020_Unidades ON dbo.TB012_Contratos.TB012_id = dbo.TB020_Unidades.TB012_id  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB011_APPUsuarios ON dbo.TB012_Contratos.TB012_AlteradoPor = dbo.TB011_APPUsuarios.TB011_Id ");
                sSql.Append(" WHERE ");
                sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato = 3 ");
                sSql.Append(query);
                sSql.Append(" ORDER BY dbo.TB020_Unidades.TB020_NomeFantasia ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new ContratosController
                    {
                        TB012_Id                = Convert.ToInt64(reader["TB012_Id"])
                        , TB012_StatusS         = reader["TB012_Status"] is DBNull
                            ? "0"
                            : Enum.GetName(typeof(ContratosController.TB012_StatusE),
                                                  Convert.ToInt16(reader["TB012_Status"]))
                        , TB012_Edicao          = Convert.ToInt16(reader["TB012_Edicao"])
                        , TB012_CicloContrato   = reader["TB012_CicloContrato"].ToString()
                        , TB020_NomeFantasia    = reader["TB020_NomeFantasia"].ToString()
                        , TB011_NomeExibicao    = reader["TB011_NomeExibicao"].ToString()
           
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

        /// <summary>
        /// Descrição:  Gerar arquivo de exportação / Empresa Corporativa
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       15/01/2018
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<ContratosController> corporativoArquivoEmpresa()
        {
            List<ContratosController> retornoList = new List<ContratosController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT  ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_id ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_RazaoSocial ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_NomeFantasia ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_Documento ");
                sSql.Append(" , dbo.TB006_Municipio.TB006_id ");
                sSql.Append(" , dbo.TB006_Municipio.TB006_Municipio ");
                sSql.Append(" , dbo.TB006_Municipio.TB006_IBGE ");
                sSql.Append(" , dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" , dbo.TB005_Estado.TB005_Sigla ");
                sSql.Append(" , dbo.TB005_Estado.TB005_Estado ");
                sSql.Append(" , dbo.TB005_Estado.TB005_Codigo ");
                sSql.Append(" , dbo.TB003_Pais.TB003_id ");
                sSql.Append(" , dbo.TB003_Pais.TB003_Pais ");
                sSql.Append(" , dbo.TB003_Pais.TB003_DDI ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_Inicio ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_Fim ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_Cep ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_Logradouro ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_Numero ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_Bairro ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_Complemento ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_Status ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_CicloContrato ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_DiaVencimento ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_Status ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB012_Contratos ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB020_Unidades ");
                sSql.Append(" ON  ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id = dbo.TB020_Unidades.TB012_id  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio  ");
                sSql.Append(" ON  ");
                sSql.Append(" dbo.TB020_Unidades.TB006_id = dbo.TB006_Municipio.TB006_id  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB005_Estado  ");
                sSql.Append(" ON ");
                sSql.Append(" dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB003_Pais  ");
                sSql.Append(" ON  ");
                sSql.Append(" dbo.TB005_Estado.TB003_Id = dbo.TB003_Pais.TB003_id ");
                sSql.Append(" WHERE  ");
                sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato = 3 ");
                sSql.Append(" ORDER BY  ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_id ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new ContratosController
                    {
                        TB012_Id            = Convert.ToInt64(reader["TB012_Id"])
                        ,
                        TB012_Inicio        = Convert.ToDateTime(reader["TB012_Inicio"])
                        ,
                        TB012_Fim           = Convert.ToDateTime(reader["TB012_Fim"])
                        ,
                        TB012_CicloContrato = reader["TB012_CicloContrato"].ToString().TrimEnd()
                        ,
                        TB012_DiaVencimento = reader["TB012_DiaVencimento"] is DBNull ? 5: Convert.ToInt16(reader["TB012_DiaVencimento"])
                        ,
                        TB012_StatusS       = reader["TB012_Status"].ToString() 
                                                  , Unidade = new UnidadeController
                                                  {
                                                      TB020_id              = Convert.ToInt64(reader["TB020_id"])
                                                      ,
                                                      TB020_RazaoSocial     = reader["TB020_RazaoSocial"].ToString().TrimEnd()
                                                      ,
                                                      TB020_NomeFantasia    = reader["TB020_NomeFantasia"].ToString().TrimEnd()
                                                      ,
                                                      TB020_Documento       = reader["TB020_Documento"].ToString().Replace(".", "").Replace("-", "").Replace("/", "").Replace(",", "").Trim()
                                                      ,
                                                      TB020_Cep             = reader["TB020_Cep"].ToString().Replace(".","").Replace("-", "").Trim()
                                                      ,
                                                      TB020_Logradouro      = reader["TB020_Logradouro"].ToString().TrimEnd()
                                                       ,
                                                      TB020_Numero          = reader["TB020_Numero"].ToString().TrimEnd()
                                                      ,
                                                      TB020_Bairro          = reader["TB020_Bairro"].ToString().TrimEnd()
                                                      ,
                                                      TB020_StatusS         = reader["TB012_Status"].ToString()
                                                  }
                                                  ,
                                                  Municipio= new MunicipioController
                                                  {
                                                      TB006_id              = Convert.ToInt64(reader["TB006_id"])
                                                      ,
                                                      TB006_Municipio       = reader["TB006_Municipio"].ToString().TrimEnd()
                                                      ,
                                                      TB006_IBGE            = reader["TB006_IBGE"].ToString().TrimEnd()

                                                  }
                                                  ,
                                                  Estado=new EstadoController
                                                  {
                                                      TB005_Id              = Convert.ToInt64(reader["TB005_Id"])
                                                      ,
                                                      TB005_Sigla           = reader["TB005_Sigla"].ToString().TrimEnd()
                                                      ,
                                                      TB005_Estado          = reader["TB005_Estado"].ToString().TrimEnd()
                                                      ,
                                                      TB005_Codigo          = reader["TB005_Codigo"].ToString().TrimEnd()
                                                  }
                                                  , Pais = new PaisController
                                                  {
                                                      TB003_id              = Convert.ToInt64(reader["TB003_id"])
                                                      ,
                                                      TB003_Pais            = reader["TB003_Pais"].ToString().TrimEnd()
                                                      ,
                                                      TB003_DDI             = reader["TB003_DDI"].ToString().TrimEnd()
                                                  }
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
    }
}
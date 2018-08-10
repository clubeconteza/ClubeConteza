using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Controller;
using static System.String;

namespace DAO
{
    public class AnotacoesDao
    {
        /// <summary>
        /// Descrição:  Incluir nova anotação
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       05/10/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public long Anotacaoinsert(AnotacoesController anotacao)
        {
            long retorno;
            //ContratosController Retorno = new ContratosController();
            try
            {
                var insertSql = "INSERT INTO TB026_ContratoAnotacoes (TB012_id,TB011_Id,TB026_Data,TB026_Anotacao,TB026_Cod,TB026_Negociacao) VALUES (@TB012_id,@TB011_Id,@TB026_Data,@TB026_Anotacao,@TB026_Cod,@TB026_Negociacao) SELECT SCOPE_IDENTITY()";
                using (var con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();

                    var command = new SqlCommand(insertSql, con);
                    command.CommandTimeout = 300;

                    command.Parameters.AddWithValue("@TB012_id", anotacao.Tb012Id);
                    command.Parameters.AddWithValue("@TB011_Id", anotacao.Tb011Id);
                    command.Parameters.AddWithValue("@TB026_Data", anotacao.Tb026Data);
                    command.Parameters.AddWithValue("@TB026_Anotacao", anotacao.Tb026Anotacao.ToUpper().TrimEnd());
                    command.Parameters.AddWithValue("@TB026_Cod", anotacao.Tb026Cod.TrimEnd());
                    command.Parameters.AddWithValue("@TB026_Negociacao", anotacao.TB026_Negociacao);
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

        /// <summary>
        /// Descrição:  Listar anotações do contrato
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       05/10/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<AnotacoesController> AnotacoesDoContrato(long tb012Id, string Tb026Cod, int TB026_Negociacao)
        {
            var retornoList = new List<AnotacoesController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT dbo.TB026_ContratoAnotacoes.TB026_id, dbo.TB026_ContratoAnotacoes.TB012_id, dbo.TB011_APPUsuarios.TB011_Id, dbo.TB011_APPUsuarios.TB011_NomeExibicao, dbo.TB026_ContratoAnotacoes.TB026_Data,  ");
                sSql.Append(" dbo.TB026_ContratoAnotacoes.TB026_Anotacao,dbo.TB026_ContratoAnotacoes.TB026_Cod,dbo.TB026_ContratoAnotacoes.TB026_Negociacao ");
                sSql.Append(" FROM dbo.TB026_ContratoAnotacoes INNER JOIN ");
                sSql.Append(" dbo.TB011_APPUsuarios ON dbo.TB026_ContratoAnotacoes.TB011_Id = dbo.TB011_APPUsuarios.TB011_Id ");
                sSql.Append(" WHERE ");
                sSql.Append(" TB012_id = ");
                sSql.Append(tb012Id);

                if(Tb026Cod!="00000")
                {
                    sSql.Append(" and  TB026_Cod= ");
                    sSql.Append("'");
                    sSql.Append(Tb026Cod);
                    sSql.Append("'");
                }


                if (TB026_Negociacao < 2)
                {
                    sSql.Append(" and  TB026_Negociacao= ");
                    sSql.Append(TB026_Negociacao);
                }

                sSql.Append(" ORDER BY dbo.TB026_ContratoAnotacoes.TB026_id DESC ");             
                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AnotacoesController obj = new AnotacoesController
                    {
                        Tb026Id = Convert.ToInt64(reader["TB026_id"]),
                        Tb012Id = Convert.ToInt64(reader["TB012_id"]),
                        Tb011Id = Convert.ToInt64(reader["TB011_Id"]),
                        Tb026Anotacao = Convert.ToString(reader["TB026_Anotacao"]),
                        Tb026Cod = reader["TB026_Cod"] is DBNull ?  "00000" :  Convert.ToString(reader["TB026_Cod"]),
                        Tb026Data = Convert.ToDateTime(reader["TB026_Data"]),
                        TB026_Negociacao = reader["TB026_Negociacao"] is DBNull ? 0 : Convert.ToInt16(reader["TB026_Negociacao"]),
                        Tb011NomeExibicao = Convert.ToString(reader["TB011_NomeExibicao"])
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
        /// Descrição:  Pesquisar anotação pelo id
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       05/10/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public AnotacoesController AnotacaoSelect(long tb026Id)
        {
            var retorno = new AnotacoesController();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT dbo.TB026_ContratoAnotacoes.TB026_id, dbo.TB026_ContratoAnotacoes.TB012_id, dbo.TB011_APPUsuarios.TB011_Id, dbo.TB011_APPUsuarios.TB011_NomeExibicao, dbo.TB026_ContratoAnotacoes.TB026_Data,  ");
                sSql.Append(" dbo.TB026_ContratoAnotacoes.TB026_Anotacao ");
                sSql.Append(" FROM dbo.TB026_ContratoAnotacoes INNER JOIN ");
                sSql.Append(" dbo.TB011_APPUsuarios ON dbo.TB026_ContratoAnotacoes.TB011_Id = dbo.TB011_APPUsuarios.TB011_Id ");
                sSql.Append(" WHERE ");
                sSql.Append(" TB026_id = ");
                sSql.Append(tb026Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.Tb026Id = Convert.ToInt64(reader["TB026_id"]);
                    retorno.Tb012Id = Convert.ToInt64(reader["TB012_id"]);
                    retorno.Tb011Id = Convert.ToInt64(reader["TB011_Id"]);
                    retorno.Tb026Anotacao = Convert.ToString(reader["TB026_Anotacao"]);
                    retorno.Tb026Data = Convert.ToDateTime(reader["TB026_Data"]);
                    retorno.Tb011NomeExibicao = Convert.ToString(reader["TB011_NomeExibicao"]);
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
    }
}
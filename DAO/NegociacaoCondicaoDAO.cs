using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAO
{
    public class NegociacaoCondicaoDAO
    {
        public List<NegociacaoCondicaoController> NegociacaoCondicaoPorUsuario(Int64 TB011_Id)
        {
            List<NegociacaoCondicaoController> Retorno = new List<NegociacaoCondicaoController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT  ");
                sSQL.Append(" dbo.TB036_NegociacaoCondicao.TB036_Id ");
                sSQL.Append(" ,dbo.TB036_NegociacaoCondicao.TB036_Nome ");
                sSQL.Append(" FROM             ");
                sSQL.Append(" dbo.TB038_TB037_TB036  ");
                sSQL.Append(" INNER JOIN ");
                sSQL.Append(" dbo.TB036_NegociacaoCondicao ON dbo.TB038_TB037_TB036.TB036_id = dbo.TB036_NegociacaoCondicao.TB036_Id  ");
                sSQL.Append(" INNER JOIN ");
                sSQL.Append(" dbo.TB011_APPUsuarios  ");
                sSQL.Append(" ON dbo.TB038_TB037_TB036.TB037_Id = dbo.TB011_APPUsuarios.TB037_Id ");
                sSQL.Append(" WHERE dbo.TB036_NegociacaoCondicao.TB036_Status = 1  ");
                sSQL.Append("  AND dbo.TB011_APPUsuarios.TB011_Id =  ");
                sSQL.Append(TB011_Id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                NegociacaoCondicaoController obj1 = new NegociacaoCondicaoController();
                obj1.TB036_Id = 0;
                obj1.TB036_Nome = "<SELECIONE>";
                Retorno.Add(obj1);

                while (reader.Read())
                {
                    NegociacaoCondicaoController obj = new NegociacaoCondicaoController();
                    obj.TB036_Id = Convert.ToInt64(reader["TB036_Id"]);
                    obj.TB036_Nome = reader["TB036_Nome"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    Retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }
        public NegociacaoCondicaoController NegociacaoCondicaoId(long TB036_Id)
        {
            var retorno = new NegociacaoCondicaoController();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT * from  TB036_NegociacaoCondicao");

                sSql.Append(" WHERE ");
                sSql.Append(" TB036_Id = ");
                sSql.Append(TB036_Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB036_Id                        = Convert.ToInt64(reader["TB036_Id"]);
                    retorno.TB036_Nome                      = reader["TB036_Nome"].ToString().ToUpper();
                    retorno.TB036_Descricao                 = reader["TB036_Descricao"].ToString();
                    retorno.TB036_NParcelasMinimo           = Convert.ToInt16(reader["TB036_NParcelasMinimo"]);
                    retorno.TB036_NParcelasMaximo           = Convert.ToInt16(reader["TB036_NParcelasMaximo"]);
                    retorno.TB036_DescontoJurosAdesao       = Convert.ToDouble(reader["TB036_DescontoJurosAdesao"]);
                    retorno.TB036_DescontoMultaAdesao       = Convert.ToDouble(reader["TB036_DescontoMultaAdesao"]);
                    retorno.TB036_DescontoMultaMensalidade  = Convert.ToDouble(reader["TB036_DescontoMultaMensalidade"]);
                    retorno.TB036_DescontoJurosMensalidade  = Convert.ToDouble(reader["TB036_DescontoJurosMensalidade"]);
                    retorno.TB036_DescontoParcela           = Convert.ToDouble(reader["TB036_DescontoParcela"]);
                    retorno.TB036_DescontoAdesao = Convert.ToDouble(reader["TB036_DescontoAdesao"]);
                    retorno.TB036_ValorMinimoParcela        = Convert.ToDouble(reader["TB036_ValorMinimoParcela"]);
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

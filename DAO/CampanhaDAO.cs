using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAO
{
    public class CampanhaDAO
    {
        public List<CampanhaController> campanhas(string filtro)
        {
            List<CampanhaController> retornoList = new List<CampanhaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" SELECT  ");
                sSql.Append(" dbo.TB041_Campanhas.TB041_id ");
                sSql.Append(" , dbo.TB041_Campanhas.TB041_Campanha ");
                sSql.Append(" , dbo.TB041_Campanhas.TB041_Inicio ");
                sSql.Append(" , dbo.TB041_Campanhas.TB041_Fim ");
                sSql.Append(" , dbo.TB041_Campanhas.TB041_CadastradoEm ");
                sSql.Append(" , TBCadastradoPor.TB011_Id AS CadastradoPorId ");
                sSql.Append(" , TBCadastradoPor.TB011_NomeExibicao AS CadastradoPorNome ");
                sSql.Append(" , dbo.TB041_Campanhas.TB041_AlteradoEm ");
                sSql.Append(" , AlteradoPor.TB011_Id AS AlteradoPorId ");
                sSql.Append(" , AlteradoPor.TB011_NomeExibicao AS AlteradoPorNome ");
                sSql.Append(" , dbo.TB041_Campanhas.TB041_Status ");
                sSql.Append(" FROM   ");
                sSql.Append(" dbo.TB041_Campanhas  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB011_APPUsuarios AS TBCadastradoPor  ");
                sSql.Append(" ON  ");
                sSql.Append(" dbo.TB041_Campanhas.TB041_CadastradoPor = TBCadastradoPor.TB011_Id  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB011_APPUsuarios AS AlteradoPor ");
                sSql.Append(" ON  ");
                sSql.Append(" dbo.TB041_Campanhas.TB041_AlteradoPor = AlteradoPor.TB011_Id ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new CampanhaController
                    {
                        TB041_id            = Convert.ToInt64(reader["TB041_id"]),
                        TB041_Campanha      = Convert.ToString(reader["TB041_Campanha"]),
                        TB041_Inicio        = Convert.ToDateTime(reader["TB041_Inicio"]),
                        TB041_Fim           = Convert.ToDateTime(reader["TB041_Fim"]),
                        TB041_AlteradoEm    = Convert.ToDateTime(reader["TB041_AlteradoEm"]),
                        AlteradoPor         = Convert.ToString(reader["AlteradoPorNome"]).TrimEnd(),
                        TB041_StatusS       = Enum.GetName(typeof(CampanhaController.TB041_StatusE), Convert.ToInt16(reader["TB041_Status"]))
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
        public CampanhaController campanha(long TB041_id)
        {
            CampanhaController retorno = new CampanhaController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" SELECT ");
                sSql.Append(" * ");
                sSql.Append(" FROM   ");
                sSql.Append(" dbo.TB041_Campanhas  ");
                sSql.Append(" where  ");
                sSql.Append(" TB041_id =  ");
                sSql.Append(TB041_id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new CampanhaController
                    {
                        TB041_id                = Convert.ToInt64(reader["TB041_id"]),
                        TB041_Campanha          = Convert.ToString(reader["TB041_Campanha"]),
                        TB041_Inicio            = Convert.ToDateTime(reader["TB041_Inicio"]),
                        TB041_Fim               = Convert.ToDateTime(reader["TB041_Fim"]),
                        TB041_AlteradoEm        = Convert.ToDateTime(reader["TB041_AlteradoEm"]),
                        TB012_id                = Convert.ToInt64(reader["TB012_id"]),
                        TB041_SmsAssunto        = reader["TB041_SmsAssunto"] is DBNull      ? "-"                                       : Convert.ToString(reader["TB041_SmsAssunto"]),
                        TB041_SmsConteudo       = reader["TB041_SmsConteudo"] is DBNull     ? "-"                                       : Convert.ToString(reader["TB041_SmsConteudo"]),          
                        TB041_SmsAgendamento    = reader["TB041_SmsAgendamento"] is DBNull  ? Convert.ToDateTime(reader["TB041_Fim"])   : Convert.ToDateTime(reader["TB041_SmsAgendamento"]),
                        TB041_StatusS           = Enum.GetName(typeof(CampanhaController.TB041_StatusE), Convert.ToInt16(reader["TB041_Status"]))
                    };

                    retorno = obj;
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
        public bool campanhaUpdateCanalSms(CampanhaController sms)
        {
            {
                try
                {
                    StringBuilder sSql = new StringBuilder();

                    sSql.Append(" UPDATE ");
                    sSql.Append(" TB041_Campanhas ");
                    sSql.Append(" SET ");
                    sSql.Append(" TB041_Sms = ");
                    sSql.Append(sms.TB041_Sms);
                    sSql.Append(" , ");
                    sSql.Append(" TB041_SmsAssunto = ");
                    sSql.Append("'");
                    sSql.Append(sms.TB041_SmsAssunto.TrimEnd());
                    sSql.Append("'");
                    sSql.Append(" , ");
                    sSql.Append(" TB041_SmsConteudo = ");
                    sSql.Append("'");
                    sSql.Append(sms.TB041_SmsConteudo.TrimEnd());
                    sSql.Append("'");
                    sSql.Append(" , ");
                    sSql.Append(" TB041_SmsAgendamento = ");
                    sSql.Append("'");
                    sSql.Append(sms.TB041_SmsAgendamento.ToString("MM/dd/yyyy hh:mm"));
                    sSql.Append("'");
                    sSql.Append(" , ");
                    sSql.Append(" TB041_AlteradoEm = ");
                    sSql.Append("'");
                    sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                    sSql.Append("'");
                    sSql.Append(" , ");
                    sSql.Append(" TB041_AlteradoPor = ");
                    sSql.Append(sms.TB041_AlteradoPor);
                    sSql.Append(" where ");
                    sSql.Append(" TB041_id = ");
                    sSql.Append(sms.TB041_id);

                    using (SqlConnection myConnection = new SqlConnection(ParametrosDAO.StringConexao))
                    {
                        myConnection.Open();
                        SqlCommand myCommand = new SqlCommand(sSql.ToString(), myConnection);
                        myCommand.ExecuteScalar();
                        myConnection.Close();
                    }
                }
                catch (Exception ex)
                {
                    // ReSharper disable once PossibleIntendedRethrow
                    throw ex;
                }
                return true;
            }
        }
    }
}

using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class MensagemDAO
    {
        /// <summary>
        /// Descrição:  Listar mensagens via SMS para envio agendado
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       07/04/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        /// 
        public List<MensagemController> smsAgendados(DateTime dataReferencia)
        {
            List<MensagemController> RetornoList = new List<MensagemController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" SELECT ");
                sSql.Append(" TB039_id ");
                sSql.Append(" ,TB039_Tipo ");
                sSql.Append(" ,TB039_Destino ");
                sSql.Append(" ,TB039_Assunto ");
                sSql.Append(" ,TB039_Conteudo ");
                sSql.Append(" ,TB039_DataAgendamento ");
                sSql.Append(" ,TB039_Status ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB039_Mensagem  ");
                sSql.Append(" WHERE ");
                sSql.Append(" TB039_Tipo = 1 ");
                sSql.Append(" AND  ");
                sSql.Append(" TB039_Status = 1 ");
                sSql.Append(" AND ");
                sSql.Append(" TB039_DataAgendamento =  ");
                sSql.Append("'");
                sSql.Append(dataReferencia.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ORDER BY TB039_id ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new MensagemController
                    {
                        TB039_id = Convert.ToInt64(reader["TB039_id"].ToString()),
                        TB039_Destino = reader["TB039_Destino"].ToString().Replace("(", "").Replace(")", "").Replace("-", "").Trim(),
                        TB039_Assunto = reader["TB039_Assunto"].ToString().TrimEnd(),
                        TB039_Conteudo = reader["TB039_Conteudo"].ToString().TrimEnd(),
                        TB039_StatusI = Convert.ToInt16(reader["TB039_Status"].ToString())
                    };

                    RetornoList.Add(obj);
                    /*Atualiza o Lote de Impressão do Cartão*/
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetornoList;
        }

        public List<MensagemController> smsListar(DateTime dataReferencia)
        {
            List<MensagemController> RetornoList = new List<MensagemController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" SELECT   ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_id  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_Destino  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_Assunto  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_Conteudo  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_DataCriacao  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_DataAgendamento  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_EviadoEm  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_RetornoCod  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_RetornoDef  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_Status  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_Status  ");
                sSql.Append(" FROM              ");
                sSql.Append(" dbo.TB039_Mensagem   ");
                sSql.Append(" INNER JOIN  ");
                sSql.Append(" dbo.TB013_Pessoa ON dbo.TB039_Mensagem.TB013_id = dbo.TB013_Pessoa.TB013_id   ");
                sSql.Append("  INNER JOIN  ");
                sSql.Append(" dbo.TB012_Contratos ON dbo.TB013_Pessoa.TB012_id = dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(" WHERE ");
                sSql.Append(" TB039_DataAgendamento =  ");
                sSql.Append("'");
                sSql.Append(dataReferencia.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ORDER BY   ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_id  ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new MensagemController
                    {
                        TB012_id                = Convert.ToInt64(reader["TB012_id"].ToString()),
                        TB039_id                = Convert.ToInt64(reader["TB039_id"].ToString()),
                        TB013_NomeCompleto      = reader["TB013_NomeCompleto"].ToString().TrimEnd(),
                        TB039_Destino           = reader["TB039_Destino"].ToString().Replace("(", "").Replace(")", "").Replace("-", "").Trim(),
                        TB039_Assunto           = reader["TB039_Assunto"].ToString().TrimEnd(),
                        TB039_Conteudo          = reader["TB039_Conteudo"].ToString().TrimEnd(),
                        TB039_DataCriacao       = Convert.ToDateTime( reader["TB039_DataCriacao"]),
                        TB039_Agendamento       = Convert.ToDateTime(reader["TB039_DataAgendamento"]),
                        TB039_EviadoEm          = reader["TB039_EviadoEm"] is DBNull ? Convert.ToDateTime( "01/01/1900") : Convert.ToDateTime(reader["TB039_EviadoEm"].ToString()),
                        TB039_RetornoCod        = reader["TB039_RetornoCod"] is DBNull ? 0 : Convert.ToInt16(reader["TB039_RetornoCod"].ToString()),
                        TB039_RetornoDef        = reader["TB039_RetornoDef"] is DBNull ? "--" : reader["TB039_RetornoDef"].ToString(),
                        TB039_StatusS           = Enum.GetName(typeof(MensagemController.TB039_StatusE), Convert.ToInt16(reader["TB039_Status"])),
                        TB012_StatusS           = Enum.GetName(typeof(MensagemController.TB012_StatusE), Convert.ToInt16(reader["TB012_Status"]))
                    };

                    RetornoList.Add(obj);
                    /*Atualiza o Lote de Impressão do Cartão*/
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetornoList;
        }


        public bool MensagemExcluir(long TB039_id,long TB009_id)
        {
        

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" delete from ");
                sSql.Append(" TB039_Mensagem ");
                sSql.Append(" where TB039_id = ");
                sSql.Append(TB039_id );
                sSql.Append(" and  TB009_id = ");
                sSql.Append(TB039_id);


                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

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


        public bool MensagemIncluir(List<MensagemController> mensagens)
        {
            try
            {
                CriptografiaDAO Cript = new CriptografiaDAO();


                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("INSERT INTO ");
                sSql.Append(" TB039_Mensagem ");
                sSql.Append(" ( ");
                sSql.Append(" TB041_id ");
                sSql.Append(" , TB012_id ");
                sSql.Append(" , TB013_id");
                sSql.Append(" , TB009_id");
                sSql.Append(" , TB039_Tipo");
                sSql.Append(" , TB039_Assunto");
                sSql.Append(" , TB039_Conteudo");
                sSql.Append(" , TB039_DataCriacao");
                sSql.Append(" , TB039_DataAgendamento");
                sSql.Append(" , TB039_Status");
                sSql.Append("  ) VALUES ");


                con.Open();
                for (int i = 0; i < mensagens.Count; i++)
                {
                    var sSqlI = new StringBuilder();
                    sSqlI.Append(" (");
                    sSqlI.Append(mensagens[i].TB041_id);
                    sSqlI.Append(",");
                    sSqlI.Append(mensagens[i].TB012_id);
                    sSqlI.Append(",");
                    sSqlI.Append(mensagens[i].TB013_id);
                    sSqlI.Append(",");
                    sSqlI.Append(mensagens[i].TB009_id);
                    sSqlI.Append(",");
                    sSqlI.Append(mensagens[i].TB009_TipoI);
                    sSqlI.Append(",");
                    sSqlI.Append("'");
                    sSqlI.Append(mensagens[i].TB039_Assunto);
                    sSqlI.Append("'");
                    sSqlI.Append(",");
                    sSqlI.Append("'");
                    sSqlI.Append(mensagens[i].TB039_Conteudo);
                    sSqlI.Append("'");
                    sSqlI.Append(",");
                    sSqlI.Append("'");
                    sSqlI.Append(DateTime.Now.ToString("MM/dd/yyy hh:mm"));
                    sSqlI.Append("'");
                    sSqlI.Append(",");
                    sSqlI.Append("'");
                    sSqlI.Append(mensagens[i].TB039_Agendamento.ToString("MM/dd/yyy hh:mm"));
                    sSqlI.Append("'");
                    sSqlI.Append(",");
                    sSqlI.Append(mensagens[i].TB012_StatusI);
                    sSqlI.Append(")");


                    string insert = sSql.ToString() + sSqlI.ToString();
                    var command = new SqlCommand(insert, con);
                    command.CommandTimeout = 300;


                    command.ExecuteReader();
                }
                con.Close();
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

using Boleto.Controller;
using Controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Boleto.DAO
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
        public List<SmsDataSetController> smsLiberados()
        {
            List<SmsDataSetController> RetornoList = new List<SmsDataSetController>();
            try
            {
                var temp = new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString);

                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSql = new StringBuilder();

                sSql.Append("  SELECT  ");
                sSql.Append(" dbo.TB009_Contato.TB009_Contato  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_id  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB041_id  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_Status  ");
                sSql.Append(" , dbo.TB009_Contato.TB009_id  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_Tipo  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_DataAgendamento  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_Status  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_Conteudo  ");
                sSql.Append(" FROM   ");
                sSql.Append(" dbo.TB039_Mensagem  ");
                sSql.Append(" INNER JOIN dbo.TB009_Contato  ");
                sSql.Append(" ON   dbo.TB039_Mensagem.TB009_id = dbo.TB009_Contato.TB009_id  ");
                sSql.Append(" INNER JOIN  dbo.TB012_Contratos  ");
                sSql.Append(" ON   dbo.TB039_Mensagem.TB012_id = dbo.TB012_Contratos.TB012_id  ");
                sSql.Append(" WHERE  ");
                sSql.Append(" dbo.TB039_Mensagem.TB039_Status = 2  ");
                sSql.Append(" AND  ");
                sSql.Append(" dbo.TB039_Mensagem.TB039_Tipo = 1  ");
                sSql.Append(" AND  ");
                sSql.Append(" dbo.TB012_Contratos.TB012_Status = 1  ");
                sSql.Append(" ORDER BY  ");
                sSql.Append(" dbo.TB039_Mensagem.TB041_id  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB012_id  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB013_id  ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new SmsDataSetController
                    {
                        TB039_id                = Convert.ToInt64(reader["TB039_id"].ToString()),
                        TB012_id                = Convert.ToInt64(reader["TB012_id"].ToString()),
                        TB009_id                = Convert.ToInt64(reader["TB009_id"].ToString()),
                        TB039_Mensagem          = reader["TB039_Conteudo"].ToString().ToString().TrimEnd(),
                        TB009_Contato           = reader["TB009_Contato"].ToString().Replace("(", "").Replace(")", "").Replace("-", "").Trim(),
                        TB039_Agendamento       = Convert.ToDateTime(reader["TB039_DataAgendamento"].ToString())
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


        public bool confirmarenviomensagem(long TB039_id)
        {
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" update ");
                sSql.Append(" TB039_Mensagem ");
                sSql.Append(" set ");
                sSql.Append(" TB039_Status = 3");
                sSql.Append(" ,");
                sSql.Append(" TB039_EviadoEm = ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(" where TB039_id = ");
                sSql.Append(TB039_id);



                SqlCommand command = new SqlCommand(sSql.ToString(), con);
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


        public List<MensagemController> emailsLiberados(DateTime agendamento)
        {
            List<MensagemController> RetornoList = new List<MensagemController>();
            try
            {
                var temp = new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString);

                var temp2 = new CriptografiaDAO().EncryptInterna("Data Source=FGE\\FGE;Initial Catalog=DBClubeConteza_Local;User ID =sa;Password=root;Persist Security Info=True");

                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSql = new StringBuilder();

      
                sSql.Append(" SELECT   ");
                sSql.Append(" dbo.TB039_Mensagem.TB039_id  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB041_id  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB012_id  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_Tipo  ");
                sSql.Append(" , dbo.TB009_Contato.TB009_id  ");
                sSql.Append(" , dbo.TB009_Contato.TB009_Contato  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_Status  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_id  ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_DataAgendamento  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_Conteudo  ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_Assunto  ");
                sSql.Append(" FROM  ");
                sSql.Append(" dbo.TB039_Mensagem ");
                sSql.Append(" INNER JOIN  ");
                sSql.Append(" dbo.TB009_Contato ");
                sSql.Append(" ON   ");
                sSql.Append(" dbo.TB039_Mensagem.TB009_id = dbo.TB009_Contato.TB009_id   ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB013_Pessoa ON dbo.TB039_Mensagem.TB013_id = dbo.TB013_Pessoa.TB013_id  ");
                sSql.Append("  WHERE ");
                sSql.Append(" dbo.TB039_Mensagem.TB039_Tipo = 1 ");
                sSql.Append(" AND ");
                sSql.Append(" dbo.TB039_Mensagem.TB039_Status = 2 ");
                sSql.Append(" AND ");
                sSql.Append(" dbo.TB039_Mensagem.TB039_DataAgendamento >= ");
                sSql.Append("'");
                sSql.Append(agendamento.ToString("MM/dd/yyyy 00:00"));
                sSql.Append("'");
                sSql.Append(" AND ");
                sSql.Append(" dbo.TB039_Mensagem.TB039_DataAgendamento <= ");
                sSql.Append("'");
                sSql.Append(agendamento.ToString("MM/dd/yyyy 23:59"));
                sSql.Append("'");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new MensagemController
                    {
                        TB039_id            = Convert.ToInt64(reader["TB039_id"].ToString()),
                        TB012_id            = Convert.ToInt64(reader["TB012_id"].ToString()),
                        TB009_id            = Convert.ToInt64(reader["TB009_id"].ToString()),
                        TB039_Assunto       = reader["TB039_Assunto"].ToString().ToString().TrimEnd(),
                        TB039_Mensagem      = reader["TB039_Conteudo"].ToString().ToString().TrimEnd(),
                        TB009_Contato       = reader["TB009_Contato"].ToString().Replace("(", "").Replace(")", "").Trim(),
                        TB039_Agendamento   = Convert.ToDateTime(reader["TB039_DataAgendamento"].ToString())
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

    }
}
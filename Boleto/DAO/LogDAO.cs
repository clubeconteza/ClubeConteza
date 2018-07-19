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
    public class LogDAO
    {
        /// <summary>
        /// Descrição:  Incluir Log
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public LogController LogInsert(LogController Log)
        {
            LogController Retorno = new LogController();
            try
            {
                string insertSql = "INSERT INTO TB000_Log(TB012_id,TB011_Id,TB000_IdTabela,TB000_Tabela,TB000_Data,TB000_Descricao,TB025_Id) VALUES (@TB012_id,@TB011_Id,@TB000_IdTabela,@TB000_Tabela,@TB000_Data,@TB000_Descricao,@TB025_Id) SELECT SCOPE_IDENTITY()";

                using (SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString)))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand(insertSql, con);

                    command.Parameters.AddWithValue("@TB012_Id", Log.TB012_Id);
                    command.Parameters.AddWithValue("@TB025_Id", Log.TB025_Id);
                    command.Parameters.AddWithValue("@TB011_Id", Log.TB011_Id);
                    command.Parameters.AddWithValue("@TB000_IdTabela", Log.TB000_IdTabela);
                    command.Parameters.AddWithValue("@TB000_Tabela", Log.TB000_Tabela);
                    command.Parameters.AddWithValue("@TB000_Data", Log.TB000_Data);
                    command.Parameters.AddWithValue("@TB000_Descricao", Log.TB000_Descricao);

                    Retorno.TB000_Id = Convert.ToInt32(command.ExecuteScalar());

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;

        }

        public List<LogController> LogContratoSelect(Int64 TB012_Id)
        {
            List<LogController> RetornoList = new List<LogController>();
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append("SELECT ");
                sSQL.Append("dbo.TB000_Log.TB000_Id, ");
                sSQL.Append("dbo.TB000_Log.TB000_Data, ");
                sSQL.Append("dbo.TB000_Log.TB000_Descricao, ");
                sSQL.Append("dbo.TB011_APPUsuarios.TB011_NomeExibicao, ");
                sSQL.Append("dbo.TB000_Log.TB012_Id ");
                sSQL.Append("FROM  ");
                sSQL.Append("dbo.TB000_Log ");
                sSQL.Append("INNER JOIN ");
                sSQL.Append("dbo.TB011_APPUsuarios ");
                sSQL.Append("ON ");
                sSQL.Append("dbo.TB000_Log.TB011_Id = dbo.TB011_APPUsuarios.TB011_Id ");
                sSQL.Append("WHERE ");
                sSQL.Append("dbo.TB000_Log.TB012_Id =  ");
                sSQL.Append(TB012_Id);
                sSQL.Append("ORDER BY dbo.TB000_Log.TB000_Id ");


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    LogController obj = new LogController();

                    obj.TB000_Data = Convert.ToDateTime(reader["TB000_Data"]);
                    obj.TB000_Descricao = reader["TB000_Descricao"].ToString();
                    obj.TB011_NomeExibicao = reader["TB011_NomeExibicao"].ToString().TrimEnd().TrimStart().ToUpper();

                    RetornoList.Add(obj);
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
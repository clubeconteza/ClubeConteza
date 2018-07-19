using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class LogDao
    {
        /// <summary>
        /// Descrição:  Incluir Log
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public LogController LogInsert(LogController log)
        {
            LogController retorno = new LogController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

                SqlCommand cmdInsertTb026 = con.CreateCommand();

                StringBuilder sInsertTb026 = new StringBuilder();
                sInsertTb026.Append("INSERT INTO ");
                sInsertTb026.Append("TB026_ContratoAnotacoes");
                sInsertTb026.Append("(");
                sInsertTb026.Append("TB012_id");
                sInsertTb026.Append(", TB011_Id");
                sInsertTb026.Append(", TB026_Data");
                sInsertTb026.Append(", TB026_Anotacao");
                sInsertTb026.Append(")");
                sInsertTb026.Append("VALUES");
                sInsertTb026.Append("(");
                sInsertTb026.Append(log.TB012_Id);
                sInsertTb026.Append(",");
                sInsertTb026.Append(log.TB011_Id);
                sInsertTb026.Append(",");
                sInsertTb026.Append("'");
                sInsertTb026.Append(log.TB000_Data.ToString("MM/dd/yyy hh:mm"));
                sInsertTb026.Append("'");
                sInsertTb026.Append(", ");
                sInsertTb026.Append("'");
                sInsertTb026.Append(log.TB000_Descricao);
                sInsertTb026.Append("'");
                sInsertTb026.Append(")");

                cmdInsertTb026.CommandText = sInsertTb026.ToString();

                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    cmdInsertTb026.Transaction = tran;
                    cmdInsertTb026.ExecuteNonQuery();
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
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;

        }

        //public List<LogController> LogContratoSelect(Int64 tb012Id)
        //{
        //    List<LogController> retornoList = new List<LogController>();
        //    try
        //    {
        //        SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
        //        StringBuilder sSql = new StringBuilder();

        //        sSql.Append("SELECT ");
        //        sSql.Append("dbo.TB000_Log.TB000_Id, ");
        //        sSql.Append("dbo.TB000_Log.TB000_Data, ");
        //        sSql.Append("dbo.TB000_Log.TB000_Descricao, ");
        //        sSql.Append("dbo.TB011_APPUsuarios.TB011_NomeExibicao, ");
        //        sSql.Append("dbo.TB000_Log.TB012_Id ");
        //        sSql.Append("FROM  ");
        //        sSql.Append("dbo.TB000_Log ");
        //        sSql.Append("INNER JOIN ");
        //        sSql.Append("dbo.TB011_APPUsuarios ");
        //        sSql.Append("ON ");
        //        sSql.Append("dbo.TB000_Log.TB011_Id = dbo.TB011_APPUsuarios.TB011_Id ");
        //        sSql.Append("WHERE ");
        //        sSql.Append("dbo.TB000_Log.TB012_Id =  ");
        //        sSql.Append(tb012Id);
        //        sSql.Append("ORDER BY dbo.TB000_Log.TB000_Id ");


        //        SqlCommand command = new SqlCommand(sSql.ToString(), con);

        //        con.Open();
        //        SqlDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            LogController obj = new LogController();

        //            obj.TB000_Data          = Convert.ToDateTime(reader["TB000_Data"]);
        //            obj.TB000_Descricao     = reader["TB000_Descricao"].ToString();
        //            obj.TB011_NomeExibicao  = reader["TB011_NomeExibicao"].ToString().TrimEnd().TrimStart().ToUpper();
         
        //            retornoList.Add(obj);
        //        }

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        // ReSharper disable once PossibleIntendedRethrow
        //        throw ex;
        //    }
        //    return retornoList;
        //}
    }
}

using Controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Boleto.DAO
{
    public class BannerDAO
    {
        /// <summary>
        /// Descrição:  Retorna lista de Banner liberados por sessão
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       27/01/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor               Descrição
        /// 27/01/2017          Fabiano Elias       Simplificado parametros de entrada
        /// </summary>
        public List<BannerController> SP_S_TB019_BannerPortal(int Sessao, int StatusContrato, int StatusBanner, Int64 Cidade)
        {
            List<BannerController> retorno_L = new List<BannerController>();
            try
            {
                using (SqlConnection connection = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString)))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SP_S_TB019_BannerPortal";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@TB019_Sessao", Sessao));
                    command.Parameters.Add(new SqlParameter("@TB012_Status", StatusContrato));
                    command.Parameters.Add(new SqlParameter("@TB019_Status", StatusBanner));
                    command.Parameters.Add(new SqlParameter("@TB006_id", Cidade));
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        BannerController obj = new BannerController();

                        obj.TB019_Id = Convert.ToInt64(reader["TB019_Id"].ToString());
                        obj.TB019_URLImagem = reader["TB019_URLImagem"].ToString().TrimEnd().TrimStart();
                        obj.TB019_NomeArquivo = reader["TB019_NomeArquivo"].ToString().TrimEnd().TrimStart();
                        obj.TB019_Titulo = reader["TB019_Titulo"].ToString().TrimEnd().TrimStart();
                        obj.TB019_Link = reader["TB019_Link"].ToString().TrimEnd().TrimStart();
                        obj.TB019_ToolTip = reader["TB019_ToolTip"].ToString().TrimEnd().TrimStart();
                        obj.TB019_LinkFormaAberturaLink = Convert.ToInt16(reader["TB019_LinkFormaAberturaLink"].ToString());
                        obj.TB019_NomeArquivo = reader["TB019_NomeArquivo"].ToString().TrimEnd().TrimStart();

                        retorno_L.Add(obj);
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno_L;
        }
    }
}
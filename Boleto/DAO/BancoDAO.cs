﻿using Boleto.Controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Boleto.DAO
{
    public class BancoDAO
    {
        public BancoController SP_S_TB018_BancosBoleto(BancoController Banco)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString)))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SP_S_TB018_BancosBoleto";
                    command.CommandType = CommandType.StoredProcedure;
                    /*Parametros da Store Procedure*/
                    command.Parameters.Add(new SqlParameter("@TB018_Banco", Banco.TB018_Banco));
                    command.Parameters.Add(new SqlParameter("@TB018_Tipo", Banco.TB018_Tipo));
                    command.Parameters.Add(new SqlParameter("@TB018_EmpresaId", Banco.TB018_EmpresaId));

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Banco.TB018_id              = Convert.ToInt16(reader["TB018_id"]);
                        Banco.TB018_url             = reader["TB018_url"].ToString().TrimEnd();
                        Banco.TB018_ContaCorrente   = reader["TB018_ContaCorrente"].ToString().TrimEnd();
                        Banco.TB018_Agencia         = reader["TB018_Agencia"].ToString().TrimEnd();
                        Banco.TB018_Cartao          = reader["TB018_Cartao"].ToString().TrimEnd();
                        Banco.TB018_Cliente         = reader["TB018_Cliente"].ToString().TrimEnd();
                        Banco.TB018_chaveAcesso     = reader["TB018_chaveAcesso"].ToString();

                    }

                    reader.Close();
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Banco;
        }
    }
}
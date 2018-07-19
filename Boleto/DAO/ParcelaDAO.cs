using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Boleto.DAO
{
    public class ParcelaDAO
    {

        public Boolean SP_U_TB016_SetarParcelaVencida()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString)))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SP_U_TB016_SetarParcelaVencida";
                    command.CommandType = CommandType.StoredProcedure;
                    /*Parametros da Store Procedure*/
                   
                    command.Parameters.Add(new SqlParameter("@Vencimento", DateTime.Now.ToString("MM/dd/yyyy")));

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
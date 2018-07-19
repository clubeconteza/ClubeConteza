using PortalClubeConteza.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace PortalClubeConteza.DAO
{
    public class EnderecoDAO
    {
        public List<Municipio> CidadesAtivas()
        {
            List<Municipio> RetornoList = new List<Municipio>();
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["EntidadesContext"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append("SELECT dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB006_Municipio.TB006_id, dbo.TB006_Municipio.TB006_Municipio, dbo.TB005_Estado.TB005_Id, dbo.TB005_Estado.TB005_Sigla ");
                sSQL.Append("FROM dbo.TB012_Contratos INNER JOIN ");
                sSQL.Append("dbo.TB006_Municipio ON dbo.TB012_Contratos.TB006_id = dbo.TB006_Municipio.TB006_id INNER JOIN ");
                sSQL.Append("dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id ");
                sSQL.Append("GROUP BY dbo.TB012_Contratos.TB012_Status, dbo.TB006_Municipio.TB006_id, dbo.TB006_Municipio.TB006_Municipio, dbo.TB005_Estado.TB005_Id, dbo.TB005_Estado.TB005_Sigla, ");
                sSQL.Append("dbo.TB012_Contratos.TB012_TipoContrato ");
                sSQL.Append("HAVING(dbo.TB012_Contratos.TB012_Status = 1) AND(dbo.TB012_Contratos.TB012_TipoContrato = 2) ");
                sSQL.Append("ORDER BY dbo.TB005_Estado.TB005_Sigla, dbo.TB006_Municipio.TB006_Municipio ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Municipio obj = new Municipio();

                    obj.Id = Convert.ToInt64(reader["TB006_id"]);
                    obj.Descricao = Convert.ToString(reader["TB005_Sigla"]).TrimEnd().Trim() + " - " + Convert.ToString(reader["TB006_Municipio"]).TrimEnd();
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
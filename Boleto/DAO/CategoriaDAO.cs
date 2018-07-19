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
    public class CategoriaDAO
    {
        public List<CategoriaController> RetornoCategoriaNivel1(long TB024_Id)
        {
            List<CategoriaController> RetornoList = new List<CategoriaController>();
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append("SELECT dbo.TB024_Sessao.TB024_Status, dbo.TB024_Sessao.TB024_Id, dbo.TB024_Sessao.TB024_Sessao, dbo.TB021_CategoriaNivel1.TB021_id,  ");
                sSQL.Append("dbo.TB021_CategoriaNivel1.TB021_Descricao ");
                sSQL.Append("FROM dbo.TB024_Sessao INNER JOIN ");
                sSQL.Append(" dbo.TB021_TB024 ON dbo.TB024_Sessao.TB024_Id = dbo.TB021_TB024.TB024_Id INNER JOIN ");
                sSQL.Append(" dbo.TB021_CategoriaNivel1 ON dbo.TB021_TB024.TB021_id = dbo.TB021_CategoriaNivel1.TB021_id ");
                sSQL.Append("WHERE dbo.TB024_Sessao.TB024_Id =  ");
                sSQL.Append(TB024_Id);
                sSQL.Append("AND dbo.TB024_Sessao.TB024_Status = 1 ");
                sSQL.Append("ORDER BY dbo.TB021_CategoriaNivel1.TB021_Descricao ");
                

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaController obj = new CategoriaController();

                    obj.TB021_id = Convert.ToInt64(reader["TB021_id"]);                  
                    obj.TB021_Descricao = Convert.ToString(reader["TB021_Descricao"]).TrimEnd();
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

        public List<CategoriaController> RetornoCategoriaNivel2(string  TB021_id)
        {
            List<CategoriaController> RetornoList = new List<CategoriaController>();
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();
                StringBuilder sClausula = new StringBuilder();

                string[] Parametros = TB021_id.Split(';');

                sClausula.Append(" WHERE TB021_id = ");
                sClausula.Append(Parametros[0]);

                if (Parametros.Length > 1)
                {
                    for (int i = 1; i < Parametros.Length; i++)
                    {
                        sClausula.Append(" or TB021_id = ");
                        sClausula.Append(Parametros[i]);
                    }
                }

                sSQL.Append(" SELECT TB021_id, TB022_id, TB022_Descricao ");
                sSQL.Append(" FROM dbo.TB022_CategoriaNivel2 ");

                //sSQL.Append("WHERE TB021_id = ");
                //sSQL.Append(TB021_id);
                sSQL.Append(sClausula);
                sSQL.Append(" ORDER BY TB021_id,TB022_Descricao");


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaController obj = new CategoriaController();
                    obj.TB021_id = Convert.ToInt64(reader["TB021_id"]);
                    obj.TB022_id = Convert.ToInt64(reader["TB022_id"]);
                    obj.TB022_Descricao = Convert.ToString(reader["TB022_Descricao"]).TrimEnd();
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

        public List<CategoriaController> RetornoCategoriaNivel3(string TB022_id)
        {
            List<CategoriaController> RetornoList = new List<CategoriaController>();
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();
                StringBuilder sClausula = new StringBuilder();

                string[] Parametros = TB022_id.Split(';');

                sClausula.Append(" WHERE TB022_id = ");
                sClausula.Append(Parametros[0]);

                if (Parametros.Length > 1)
                {
                    for (int i = 1; i < Parametros.Length; i++)
                    {
                        sClausula.Append(" or TB022_id = ");
                        sClausula.Append(Parametros[i]);
                    }
                }

                sSQL.Append("SELECT TB022_id, TB023_id, TB023_Descricao ");
                sSQL.Append("FROM dbo.TB023_CategoriaNivel3 ");

                sSQL.Append(sClausula);
                //sSQL.Append("WHERE TB022_id =  ");
                //sSQL.Append(44);
                sSQL.Append("ORDER BY TB022_id,TB023_Descricao ");


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaController obj = new CategoriaController();
                    obj.TB022_id = Convert.ToInt64(reader["TB022_id"]);
                    obj.TB023_id = Convert.ToInt64(reader["TB023_id"]);
                    obj.TB023_Descricao = Convert.ToString(reader["TB023_Descricao"]).TrimEnd();
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


        public List<CategoriaController> ParceiroTodosOsNiveis(long TB020_id)
        {
            List<CategoriaController> RetornoList = new List<CategoriaController>();
            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append("SELECT dbo.TB020_Unidades.TB020_id, dbo.TB020_Unidades.TB020_NomeFantasia, dbo.TB021_CategoriaNivel1.TB021_id, dbo.TB021_CategoriaNivel1.TB021_Descricao, ");
                sSQL.Append("dbo.TB022_CategoriaNivel2.TB022_id, dbo.TB022_CategoriaNivel2.TB022_Descricao, dbo.TB023_CategoriaNivel3.TB023_id, dbo.TB023_CategoriaNivel3.TB023_Descricao ");
                sSQL.Append("FROM dbo.TB022_CategoriaNivel2 INNER JOIN ");
                sSQL.Append("dbo.TB022_TB012 ON dbo.TB022_CategoriaNivel2.TB022_id = dbo.TB022_TB012.TB022_id RIGHT OUTER JOIN ");
                sSQL.Append("dbo.TB023_CategoriaNivel3 INNER JOIN ");
                sSQL.Append("dbo.TB023_TB012 ON dbo.TB023_CategoriaNivel3.TB023_id = dbo.TB023_TB012.TB023_id RIGHT OUTER JOIN ");
                sSQL.Append("dbo.TB020_Unidades INNER JOIN ");
                sSQL.Append("dbo.TB021_CategoriaNivel1 ON dbo.TB020_Unidades.TB021_id = dbo.TB021_CategoriaNivel1.TB021_id ON dbo.TB023_TB012.TB012_id = dbo.TB020_Unidades.TB012_id ON ");
                sSQL.Append("dbo.TB022_TB012.TB012_id = dbo.TB020_Unidades.TB012_id ");
                sSQL.Append("WHERE dbo.TB020_Unidades.TB020_id = ");
                sSQL.Append(TB020_id);
                sSQL.Append("ORDER BY dbo.TB022_CategoriaNivel2.TB021_id, dbo.TB023_TB012.TB023_id ");


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaController obj = new CategoriaController();

                    obj.TB021_id = reader["TB021_id"] is DBNull ? 0 : Convert.ToInt64(reader["TB021_id"]);
                              
                    obj.TB021_Descricao     = reader["TB021_Descricao"] is DBNull ? "-" : reader["TB021_Descricao"].ToString().TrimEnd(); 

                    obj.TB022_id            = reader["TB022_id"] is DBNull ? 0 : Convert.ToInt64(reader["TB022_id"]);
                    obj.TB022_Descricao     = reader["TB022_Descricao"] is DBNull ? "-" : reader["TB022_Descricao"].ToString().TrimEnd();

                    obj.TB023_id            = reader["TB023_id"] is DBNull ? 0 : Convert.ToInt64(reader["TB023_id"]);

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
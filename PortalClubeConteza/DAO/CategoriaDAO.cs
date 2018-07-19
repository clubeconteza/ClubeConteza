using PortalClubeConteza.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace PortalClubeConteza.DAO
{
    public class CategoriaDAO
    {
        public List<CategoriaNivelUm> RetornoCategoriaNivelUm(long Id_T024)
        {
            List<CategoriaNivelUm> RetornoList = new List<CategoriaNivelUm>();
            try
            {

            
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["EntidadesContext"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append("SELECT dbo.TB024_Sessao.TB024_Status, dbo.TB024_Sessao.TB024_Id, dbo.TB024_Sessao.TB024_Sessao, dbo.TB021_CategoriaNivel1.TB021_id,  ");
                sSQL.Append("dbo.TB021_CategoriaNivel1.TB021_Descricao ");
                sSQL.Append("FROM dbo.TB024_Sessao INNER JOIN ");
                sSQL.Append(" dbo.TB021_TB024 ON dbo.TB024_Sessao.TB024_Id = dbo.TB021_TB024.TB024_Id INNER JOIN ");
                sSQL.Append(" dbo.TB021_CategoriaNivel1 ON dbo.TB021_TB024.TB021_id = dbo.TB021_CategoriaNivel1.TB021_id ");
                sSQL.Append("WHERE dbo.TB024_Sessao.TB024_Id =  ");
                sSQL.Append(Id_T024);
                sSQL.Append("AND dbo.TB024_Sessao.TB024_Status = 1 ");
                sSQL.Append("ORDER BY dbo.TB021_CategoriaNivel1.TB021_Descricao ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaNivelUm obj = new CategoriaNivelUm();

                    obj.Id = Convert.ToInt64(reader["TB021_id"]);
                    obj.Descricao = Convert.ToString(reader["TB021_Descricao"]).TrimEnd();
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

        public List<CategoriaNivelDois> RetornoCategoriaNivelDois(string Id_T021)
        {
            List<CategoriaNivelDois> RetornoList = new List<CategoriaNivelDois>();

            if (string.IsNullOrEmpty(Id_T021))
            {
                return RetornoList;
            }

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["EntidadesContext"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();
                StringBuilder sClausula = new StringBuilder();

                string[] Parametros = Id_T021.Split(';');

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
                sSQL.Append(sClausula);
                sSQL.Append(" ORDER BY TB021_id,TB022_Descricao");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaNivelDois obj = new CategoriaNivelDois();
                    obj.Id_T021 = Convert.ToInt64(reader["TB021_id"]);
                    obj.Id_T022 = Convert.ToInt64(reader["TB022_id"]);
                    obj.Descricao = Convert.ToString(reader["TB022_Descricao"]).TrimEnd();
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

        public List<CategoriaNivelTres> RetornoCategoriaNivelTres(string Id_T022)
        {
            List<CategoriaNivelTres> RetornoList = new List<CategoriaNivelTres>();

            if (string.IsNullOrEmpty(Id_T022))
            {
                return RetornoList;
            }

            try
            {
                SqlConnection con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["EntidadesContext"].ConnectionString));
                StringBuilder sSQL = new StringBuilder();
                StringBuilder sClausula = new StringBuilder();

                string[] Parametros = Id_T022.Split(';');

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
                sSQL.Append("ORDER BY TB022_id,TB023_Descricao ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaNivelTres obj = new CategoriaNivelTres();
                    obj.Id_T022 = Convert.ToInt64(reader["TB022_id"]);
                    obj.Id_T023 = Convert.ToInt64(reader["TB023_id"]);
                    obj.Descricao = Convert.ToString(reader["TB023_Descricao"]).TrimEnd();
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
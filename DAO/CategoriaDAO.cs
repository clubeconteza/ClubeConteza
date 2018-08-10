
using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class CategoriaDao
    {

        public List<CategoriaController> RetoranarLista()
        {
            List<CategoriaController> retorno = new List<CategoriaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append("SELECT ");
                sSql.Append("dbo.TB021_CategoriaNivel1.TB021_id,  ");
                sSql.Append("ISNULL(dbo.TB021_CategoriaNivel1.TB021_Descricao, '---') AS Nivel1, ");
                sSql.Append("ISNULL(dbo.TB022_CategoriaNivel2.TB022_Descricao, '---')  AS Nivel2, ");
                sSql.Append("ISNULL(dbo.TB023_CategoriaNivel3.TB023_Descricao, '---')  AS Nivel3 ");
                sSql.Append("FROM ");
                sSql.Append("dbo.TB021_CategoriaNivel1 LEFT OUTER JOIN ");
                sSql.Append("dbo.TB022_CategoriaNivel2 ON dbo.TB021_CategoriaNivel1.TB021_id = dbo.TB022_CategoriaNivel2.TB021_id LEFT OUTER JOIN ");
                sSql.Append("dbo.TB023_CategoriaNivel3 ON dbo.TB022_CategoriaNivel2.TB022_id = dbo.TB023_CategoriaNivel3.TB022_id ");
                sSql.Append("ORDER BY dbo.TB021_CategoriaNivel1.TB021_id, dbo.TB022_CategoriaNivel2.TB022_id, dbo.TB023_CategoriaNivel3.TB023_id ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaController obj = new CategoriaController();              
                    obj.TB021_id = Convert.ToInt64(reader["TB021_id"]);
                    obj.TB021_Descricao = reader["Nivel1"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    obj.TB022_Descricao = reader["Nivel2"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    obj.TB023_Descricao = reader["Nivel3"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }
        public List<CategoriaController> ListarSessao()
        {
            var retorno = new List<CategoriaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("SELECT * FROM TB024_Sessao ");
      
                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new CategoriaController
                    {
                        TB024_Id = Convert.ToInt64(reader["TB024_Id"]),
                        TB024_Sessao = reader["TB024_Sessao"].ToString().TrimEnd().TrimStart().ToUpper()
                    };
                    retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }
        public List<CategoriaController> ListarSessaoNivel1(long tb021Id)
        {
            List<CategoriaController> retorno = new List<CategoriaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" SELECT dbo.TB021_TB024.TB021_id, dbo.TB024_Sessao.TB024_Id, dbo.TB024_Sessao.TB024_Sessao FROM dbo.TB021_TB024 INNER JOIN dbo.TB024_Sessao ON dbo.TB021_TB024.TB024_Id = dbo.TB024_Sessao.TB024_Id ");
                sSql.Append(" WHERE ");
                sSql.Append(" TB021_id = ");
                sSql.Append(tb021Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new CategoriaController
                    {
                        TB021_id = Convert.ToInt64(reader["TB021_id"]),
                        TB024_Id = Convert.ToInt64(reader["TB024_Id"]),
                        TB024_Sessao = reader["TB024_Sessao"].ToString().TrimEnd().TrimStart().ToUpper()
                    };
                    retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }
        public long SelectSessaoNivel1(long tb021Id, long tb024Id)
        {
            long retorno = 0;

            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" SELECT TB021_id, TB024_Id, id FROM dbo.TB021_TB024 ");
                sSql.Append(" WHERE ");
                sSql.Append(" TB021_id = ");
                sSql.Append(tb021Id);
                sSql.Append(" AND TB024_Id = ");
                sSql.Append(tb024Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno = Convert.ToInt64(reader["id"]);
                    
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }
        public long VincularSessaoNivel1(long tb021Id, long tb024Id)
        {
            long vid;

            try
            {
                var Insert = "INSERT INTO TB021_TB024 ( " +
                                        " TB021_id " +
                                        ", TB024_Id " +
                                        " ) VALUES ( " +
                                        " @TB021_id " +
                                        ",@TB024_Id " +
                                        " ) SELECT SCOPE_IDENTITY()";

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(Insert, con);
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@TB021_id", tb021Id);
                    command.Parameters.AddWithValue("@TB024_Id", tb024Id);



                    vid = Convert.ToInt64(command.ExecuteScalar());
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }

            return vid;
        }
        public List<CategoriaController> RetoranarTodasAsCategorias()
        {
            List<CategoriaController> retorno = new List<CategoriaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();
                sSql.Append("SELECT TB021_id, TB021_Descricao ");
                sSql.Append("FROM dbo.TB021_CategoriaNivel1 ");
                sSql.Append("ORDER BY TB021_Descricao ");
                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();


                CategoriaController obj0 = new CategoriaController();
                obj0.TB021_id = 0;
                obj0.TB021_Descricao = "SELECIONE";
                retorno.Add(obj0);

                while (reader.Read())
                {
                    CategoriaController obj = new CategoriaController();
                    obj.TB021_id = Convert.ToInt64(reader["TB021_id"]);
                    obj.TB021_Descricao = reader["TB021_Descricao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
        public CategoriaController RetoranarCategoriaNivel1(long TB021_id)
        {
            CategoriaController Retorno = new CategoriaController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append("SELECT  TB021_id, TB021_Descricao FROM dbo.TB021_CategoriaNivel1 ");
                sSQL.Append("WHERE ");
                sSQL.Append("TB021_id = ");
                sSQL.Append(TB021_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                   
                    Retorno.TB021_id = Convert.ToInt64(reader["TB021_id"]);
                    Retorno.TB021_Descricao = reader["TB021_Descricao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();                 
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }
        public long IncluirNivel1(CategoriaController Nivel_C)
        {
            Int64 vid = 0;
            try
            {
                string Insert = "INSERT INTO TB021_CategoriaNivel1 ( " +
                                        " TB021_Descricao " +                                     
                                        " ) VALUES ( " +
                                        " @TB021_Descricao " +                               
                                        " ) SELECT SCOPE_IDENTITY()";

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(Insert, con);
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@TB021_Descricao", Nivel_C.TB021_Descricao);
           
                    vid = Convert.ToInt64(command.ExecuteScalar());
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vid;
        }
        public bool AtualizarNivel1(CategoriaController Nivel_C)
        {
            try
            {
                string UpdateSql = " UPDATE TB021_CategoriaNivel1 SET " +
                                    " TB021_Descricao   = '" + Nivel_C.TB021_Descricao + "'" +
                                    " where TB021_id    =  " + Nivel_C.TB021_id;

                using (SqlConnection myConnection = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    myConnection.Open();

                    SqlCommand myCommand = new SqlCommand(UpdateSql, myConnection);
                    myCommand.CommandTimeout = 300;

                    myCommand.ExecuteScalar();
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
            return true;
        }
        public long IncluirNivel2(CategoriaController Nivel_C)
        {
            Int64 vid = 0;
            try
            {
                string Insert = "INSERT INTO TB022_CategoriaNivel2 ( " +
                                        " TB021_id " +
                                        ",TB022_Descricao " +
                                        " ) VALUES ( " +
                                        " @TB021_id " +
                                        ", @TB022_Descricao " +
                                        " ) SELECT SCOPE_IDENTITY()";

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(Insert, con);
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@TB021_id"         , Nivel_C.TB021_id);
                    command.Parameters.AddWithValue("@TB022_Descricao"  , Nivel_C.TB022_Descricao);



                    vid = Convert.ToInt64(command.ExecuteScalar());
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vid;
        }
        public CategoriaController RetornoItemNivel2(long TB022_id)
        {
            CategoriaController Retorno = new CategoriaController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append("SELECT  * from TB022_CategoriaNivel2 ");
                sSQL.Append("WHERE ");
                sSQL.Append("TB022_id = ");
                sSQL.Append(TB022_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB022_id        = Convert.ToInt64(reader["TB022_id"]);
                    Retorno.TB022_Descricao = reader["TB022_Descricao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }
        public bool AtualizarNivel2(CategoriaController Nivel_C)
        {
            try
            {

                string UpdateSql = " UPDATE TB022_CategoriaNivel2 SET " +
                                    " TB022_Descricao   = '" + Nivel_C.TB022_Descricao + "'" +
                                    " where TB022_id    =  " + Nivel_C.TB022_id;

                using (SqlConnection myConnection = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    myConnection.Open();

                    SqlCommand myCommand = new SqlCommand(UpdateSql, myConnection);
                    myCommand.CommandTimeout = 300;

                    myCommand.ExecuteScalar();
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
            return true;
        }
        public long IncluirNivel3(CategoriaController Nivel_C)
        {
            long vid = 0;
            try
            {
                string Insert = "INSERT INTO TB023_CategoriaNivel3 ( " +
                                        " TB022_id " +
                                        ",TB023_Descricao " +
                                        " ) VALUES ( " +
                                        " @TB022_id " +
                                        ",@TB023_Descricao " +
                                        " ) SELECT SCOPE_IDENTITY()";

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(Insert, con);
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@TB022_id", Nivel_C.TB022_id);
                    command.Parameters.AddWithValue("@TB023_Descricao", Nivel_C.TB023_Descricao);



                    vid = Convert.ToInt64(command.ExecuteScalar());
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vid;
        }
        public CategoriaController RetornoItemNivel3(long TB023_id)
        {
            CategoriaController Retorno = new CategoriaController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append("SELECT  * from TB023_CategoriaNivel3 ");
                sSQL.Append("WHERE ");
                sSQL.Append("TB023_id = ");
                sSQL.Append(TB023_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB023_id = Convert.ToInt64(reader["TB023_id"]);
                    Retorno.TB023_Descricao = reader["TB023_Descricao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }
        public bool AtualizarNivel3(CategoriaController Nivel_C)
        {
            try
            {

                string UpdateSql = " UPDATE TB023_CategoriaNivel3 SET " +
                                    " TB023_Descricao   = '" + Nivel_C.TB023_Descricao + "'" +
                                    " where TB023_id    =  " + Nivel_C.TB023_id;

                using (SqlConnection myConnection = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    myConnection.Open();

                    SqlCommand myCommand = new SqlCommand(UpdateSql, myConnection);
                    myCommand.CommandTimeout = 300;

                    myCommand.ExecuteScalar();
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
            return true;
        }
        public List<CategoriaController> RetoranarcCategoriaNivel2(long TB021_id)
        {
            List<CategoriaController> Retorno = new List<CategoriaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" SELECT  TB022_id, TB021_id, TB022_Descricao ");
                sSQL.Append(" FROM dbo.TB022_CategoriaNivel2  ");
                sSQL.Append(" WHERE TB021_id =  ");
                sSQL.Append(TB021_id);
                sSQL.Append(" ORDER BY TB022_Descricao");
        
                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaController obj = new CategoriaController();
                    obj.TB022_id            = Convert.ToInt64(reader["TB022_id"]);
                    obj.TB022_Descricao     = reader["TB022_Descricao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    
                    Retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }
        public List<CategoriaController> RetoranarcCategoriaNivel1DoContrato(long TB012_id)
        {
            List<CategoriaController> Retorno = new List<CategoriaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" SELECT dbo.TB021_TB012.TB012_id, dbo.TB021_CategoriaNivel1.TB021_id, dbo.TB021_CategoriaNivel1.TB021_Descricao ");
                sSQL.Append(" FROM dbo.TB021_TB012 INNER JOIN ");
                sSQL.Append(" dbo.TB021_CategoriaNivel1 ON dbo.TB021_TB012.TB021_id = dbo.TB021_CategoriaNivel1.TB021_id ");
                sSQL.Append(" WHERE dbo.TB021_TB012.TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append(" ORDER BY dbo.TB021_CategoriaNivel1.TB021_Descricao");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaController obj = new CategoriaController();
                    obj.TB021_id = Convert.ToInt64(reader["TB021_id"]);
                    obj.TB021_Descricao = reader["TB021_Descricao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper();

                    Retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }
        public List<CategoriaController> RetoranarcCategoriaNivel2DoContrato(Int64 TB021_id, Int64 TB012_id)
        {
            List<CategoriaController> Retorno = new List<CategoriaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" SELECT dbo.TB022_CategoriaNivel2.TB022_id, dbo.TB022_CategoriaNivel2.TB021_id, dbo.TB022_CategoriaNivel2.TB022_Descricao, dbo.TB022_TB012.id, dbo.TB022_TB012.TB012_id ");
                sSQL.Append(" FROM dbo.TB022_CategoriaNivel2 INNER JOIN ");
                sSQL.Append("dbo.TB022_TB012 ON dbo.TB022_CategoriaNivel2.TB022_id = dbo.TB022_TB012.TB022_id ");
                sSQL.Append(" WHERE dbo.TB022_CategoriaNivel2.TB021_id = ");
                sSQL.Append(TB021_id);
                sSQL.Append(" AND dbo.TB022_TB012.TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append(" ORDER BY dbo.TB022_CategoriaNivel2.TB022_Descricao");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaController obj = new CategoriaController();
                    obj.TB022_id = Convert.ToInt64(reader["TB022_id"]);
                    obj.TB022_Descricao = reader["TB022_Descricao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();

                    Retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }
        public long IncluirNivel1Contrato(long TB021_id, long TB012_id)
        {
            Int64 vid = 0;
            try
            {
                string inserParcela = "INSERT INTO TB021_TB012 ( " +
                                        " TB021_id, " +
                                        " TB012_id" +
                                        " ) VALUES ( " +
                                        " @TB021_id, " +
                                        " @TB012_id " +
                                        " ) SELECT SCOPE_IDENTITY()";

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(inserParcela, con);
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@TB021_id", TB021_id);
                    command.Parameters.AddWithValue("@TB012_id", TB012_id);

                    vid = Convert.ToInt64(command.ExecuteScalar());
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vid;
        }
        public bool ExcluirNivel1Contrato(long TB021_id, long TB012_id)
        {

            try
            {
                string DeleteParcela = "delete from TB021_TB012 where TB021_id= " + TB021_id + " and TB012_id=" + TB012_id;

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(DeleteParcela, con);
                    command.CommandTimeout = 300;
                    Convert.ToInt64(command.ExecuteScalar());
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        public long IncluirNivel2Contrato(long TB022_id, long TB012_id)
        {
            long vid = 0;
            try
            {
                string inserParcela = "INSERT INTO TB022_TB012 ( " +
                                        " TB022_id, " +
                                        " TB012_id" +   
                                        " ) VALUES ( " +
                                        " @TB022_id, " +
                                        " @TB012_id " +
                                        " ) SELECT SCOPE_IDENTITY()";

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(inserParcela, con);
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@TB022_id", TB022_id);
                    command.Parameters.AddWithValue("@TB012_id", TB012_id);



                    vid = Convert.ToInt64(command.ExecuteScalar());
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vid;
        }
        public bool ExcluirNivel2Contrato(long TB022_id, long TB012_id)
        {
       
            try
            {
                string DeleteParcela = "delete from TB022_TB012 where TB022_id= " + TB022_id + " and TB012_id=" + TB012_id;

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(DeleteParcela, con);
                    command.CommandTimeout = 300;
                    Convert.ToInt64(command.ExecuteScalar());
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        public List<CategoriaController> RetoranarcCategoriaNivel3(long TB022_id)
        {
            List<CategoriaController> Retorno = new List<CategoriaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" SELECT TB022_id, TB023_id, TB023_Descricao ");
                sSQL.Append(" FROM   dbo.TB023_CategoriaNivel3 ");
                sSQL.Append(" WHERE TB022_id =  ");
                sSQL.Append(TB022_id);
                sSQL.Append(" ORDER BY TB023_Descricao ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaController obj = new CategoriaController();
                    obj.TB023_id = Convert.ToInt64(reader["TB023_id"]);
                    obj.TB023_Descricao = reader["TB023_Descricao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();

                    Retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }
        public List<CategoriaController> RetoranarcCategoriaNivel3DoContrato(long TB022_id, Int64 TB012_id)
        {
            List<CategoriaController> Retorno = new List<CategoriaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB023_TB012.TB012_id, dbo.TB023_CategoriaNivel3.TB022_id, dbo.TB023_CategoriaNivel3.TB023_id, dbo.TB023_CategoriaNivel3.TB023_Descricao ");
                sSQL.Append(" FROM            dbo.TB023_TB012 INNER JOIN ");
                sSQL.Append(" dbo.TB023_CategoriaNivel3 ON dbo.TB023_TB012.TB023_id = dbo.TB023_CategoriaNivel3.TB023_id ");
                sSQL.Append(" WHERE dbo.TB023_TB012.TB012_id =  ");
                sSQL.Append(TB012_id);
                sSQL.Append(" AND  ");
                sSQL.Append(" dbo.TB023_CategoriaNivel3.TB022_id =  ");
                sSQL.Append(TB022_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoriaController obj = new CategoriaController();
                    obj.TB023_id = Convert.ToInt64(reader["TB023_id"]);
                    obj.TB023_Descricao = reader["TB023_Descricao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();

                    Retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }
        public long IncluirNivel3Contrato(long TB023_id, Int64 TB012_id)
        {
            Int64 vid = 0;
            try
            {
                string inserParcela = "INSERT INTO TB023_TB012 ( " +
                                        " TB023_id, " +
                                        " TB012_id" +
                                        " ) VALUES ( " +
                                        " @TB023_id, " +
                                        " @TB012_id " +
                                        " ) SELECT SCOPE_IDENTITY()";

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(inserParcela, con);
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@TB023_id", TB023_id);
                    command.Parameters.AddWithValue("@TB012_id", TB012_id);



                    vid = Convert.ToInt64(command.ExecuteScalar());
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vid;
        }
        public bool ExcluirNivel3Contrato(Int64 TB023_id, Int64 TB012_id)
        {

            try
            {
                string DeleteParcela = "delete from TB023_TB012 where TB023_id= " + TB023_id + " and TB012_id=" + TB012_id;

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(DeleteParcela, con);
                    command.CommandTimeout = 300;
                    Convert.ToInt64(command.ExecuteScalar());
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        public bool DesvincularSessaoNivel1(long TB021_id, long TB024_Id)
        {
            try
            {
                string DeleteParcela = "delete from TB021_TB024 where TB021_id= " + TB021_id + " and TB024_Id=" + TB024_Id;

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(DeleteParcela, con);
                    command.CommandTimeout = 300;
                    Convert.ToInt64(command.ExecuteScalar());
                    con.Close();
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
using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class ContatoDao
    {
        /// <summary>
        /// Descrição:  Incluir Novo Contrato
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public long ContatosContratoInsert(ContatoController contato)
        {
            long retorno;
            try
            {
                string insertSql = "INSERT INTO TB009_Contato (TB013_id,TB009_Tipo,TB009_Contato,TB009_ExibirPortal,TB009_Nota,TB009_CadastradoEm,TB009_CadastradoPor,TB009_AlteradoEm,TB009_AlteradoPor,TB020_id) VALUES (@TB013_id,@TB009_Tipo,@TB009_Contato,@TB009_ExibirPortal,@TB009_Nota,@TB009_CadastradoEm,@TB009_CadastradoPor,@TB009_AlteradoEm,@TB009_AlteradoPor,@TB020_id) SELECT SCOPE_IDENTITY()";

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand(insertSql, con);

                    command.Parameters.AddWithValue("@TB013_id", contato.Pessoa.TB013_id);
                    command.Parameters.AddWithValue("@TB020_id", contato.TB020_id);
                    command.Parameters.AddWithValue("@TB009_Tipo", contato.TB009_TipoS); 
                    command.Parameters.AddWithValue("@TB009_Contato", contato.TB009_Contato);
                    command.Parameters.AddWithValue("@TB009_ExibirPortal", contato.TB009_ExibirPortal);
                    command.Parameters.AddWithValue("@TB009_Nota", contato.TB009_Nota);
                    command.Parameters.AddWithValue("@TB009_CadastradoEm" , DateTime.Now);
                    command.Parameters.AddWithValue("@TB009_CadastradoPor", contato.TB009_CadastradoPor);
                    command.Parameters.AddWithValue("@TB009_AlteradoEm", DateTime.Now);
                    command.Parameters.AddWithValue("@TB009_AlteradoPor", contato.TB009_CadastradoPor);

                    retorno = Convert.ToInt64(command.ExecuteScalar());

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
        public Boolean ContatosContratoUpdate(ContatoController contato)
        {
            try
            {
                string updateSql =  " UPDATE TB009_Contato SET " +
                                    " TB009_Tipo            = " + contato.TB009_TipoS           +
                                    ", TB009_Contato        ='" + contato.TB009_Contato.Replace("(","").Replace(")", "").Replace("-", "").Trim() + "'" +
                                    ", TB009_ExibirPortal   = " + contato.TB009_ExibirPortal    +
                                    ", TB009_Nota           ='" + contato.TB009_Nota            + "'" +
                                    ", TB009_AlteradoEm     = '" + DateTime.Now.ToString("MM/dd/yyyy hh:ss") + "'" +
                                    ", TB009_AlteradoPor     = " + contato.TB009_AlteradoPor     +
                                    " where TB009_id        = " + contato.TB009_id +
                                    " and TB020_id        = " + contato.TB020_id;

                using (SqlConnection myConnection = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    myConnection.Open();

                    SqlCommand myCommand = new SqlCommand(updateSql, myConnection);

                    myCommand.ExecuteScalar();
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
                //return false;
            }
            return true;
        }
        public Boolean ContatosContratoExcluir(ContatoController contato)
        {
            try
            {
                string updateSql = " delete from TB009_Contato where TB009_id = " + contato.TB009_id;

                using (SqlConnection myConnection = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    myConnection.Open();

                    SqlCommand myCommand = new SqlCommand(updateSql, myConnection);

                    myCommand.ExecuteScalar();
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
                //return false;
            }
            return true;
        }
        public List<ContatoController> ContatosDaPessoa(long tb013Id)
        {
            List<ContatoController> retornoList = new List<ContatoController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append("SELECT ");
                sSql.Append("dbo.TB009_Contato.TB009_id, ");
                sSql.Append("dbo.TB009_Contato.TB013_id, ");
                sSql.Append("dbo.TB009_Contato.TB011_Id, ");
                sSql.Append("dbo.TB009_Contato.TB009_Tipo, ");
                sSql.Append("dbo.TB009_Contato.TB009_Contato, ");
                sSql.Append("dbo.TB009_Contato.TB009_ExibirPortal, ");
                sSql.Append("dbo.TB009_Contato.TB009_Nota, ");
                sSql.Append("dbo.TB009_Contato.TB009_CadastradoEm, ");
                sSql.Append("CadastradroPor.TB011_NomeExibicao AS TB009_CadastradoPor, ");
                sSql.Append("dbo.TB009_Contato.TB009_AlteradoEm, ");
                sSql.Append("AlteradorPor.TB011_NomeExibicao AS TB009_AlteradoPor ");
                sSql.Append("FROM  ");
                sSql.Append("dbo.TB009_Contato ");
                sSql.Append("INNER JOIN ");
                sSql.Append("dbo.TB011_APPUsuarios AS CadastradroPor ON dbo.TB009_Contato.TB009_CadastradoPor = CadastradroPor.TB011_Id ");
                sSql.Append("INNER JOIN ");
                sSql.Append("dbo.TB011_APPUsuarios AS AlteradorPor ON dbo.TB009_Contato.TB009_AlteradoPor = AlteradorPor.TB011_Id ");
                sSql.Append("WHERE ");
                sSql.Append("dbo.TB009_Contato.TB013_id = ");
                sSql.Append(tb013Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new ContatoController
                    {
                        TB009_id = Convert.ToInt64(reader["TB009_id"]),
                        TB009_TipoS = Convert.ToString(reader["TB009_Tipo"]),
                        TB009_Contato = Convert.ToString(reader["TB009_Contato"]),
                        TB009_ExibirPortal = Convert.ToInt16(reader["TB009_ExibirPortal"]),
                        TB009_Nota = Convert.ToString(reader["TB009_Nota"]),
                        TB009_CadastradoEm = Convert.ToDateTime(reader["TB009_CadastradoEm"]),
                        TB009_CadastradoPorNome = Convert.ToString(reader["TB009_CadastradoPor"]),
                        TB009_AlteradoEm = Convert.ToDateTime(reader["TB009_AlteradoEm"]),
                        TB009_AlteradoPorNome = Convert.ToString(reader["TB009_AlteradoPor"])
                    };

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        //public List<ContatoController> ContatosTitularContrato(long tb012Id)
        //{
        //    List<ContatoController> retornoList = new List<ContatoController>();
        //    try
        //    {
        //        SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
        //        StringBuilder sSql = new StringBuilder();

        //        sSql.Append("SELECT dbo.TB013_Pessoa.TB012_id, dbo.TB009_Contato.TB009_Tipo, dbo.TB009_Contato.TB009_Contato ");
        //        sSql.Append("FROM dbo.TB009_Contato INNER JOIN ");
        //        sSql.Append("dbo.TB013_Pessoa ON dbo.TB009_Contato.TB013_id = dbo.TB013_Pessoa.TB013_id ");
        //        sSql.Append("WHERE ");
        //        sSql.Append("dbo.TB013_Pessoa.TB012_id = ");
        //        sSql.Append(tb012Id);
        //        sSql.Append(" AND dbo.TB009_Contato.TB009_Tipo < 3  ");

        //        SqlCommand command = new SqlCommand(sSql.ToString(), con);

        //        con.Open();
        //        SqlDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            ContatoController obj = new ContatoController();
        //            obj.TB009_TipoS = Convert.ToString(reader["TB009_Tipo"]); 
        //            obj.TB009_Contato = Convert.ToString(reader["TB009_Contato"]);
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
        public List<ContatoController> ContatosTitularContratoEmail(long tb012Id)
        {
            List<ContatoController> retornoList = new List<ContatoController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append("SELECT dbo.TB013_Pessoa.TB012_id, dbo.TB009_Contato.TB009_Tipo, dbo.TB009_Contato.TB009_Contato ");
                sSql.Append("FROM dbo.TB009_Contato INNER JOIN ");
                sSql.Append("dbo.TB013_Pessoa ON dbo.TB009_Contato.TB013_id = dbo.TB013_Pessoa.TB013_id ");
                sSql.Append("WHERE ");
                sSql.Append("dbo.TB013_Pessoa.TB012_id = ");
                sSql.Append(tb012Id);
                sSql.Append(" AND dbo.TB009_Contato.TB009_Tipo = 3  ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ContatoController obj = new ContatoController();
                    obj.TB009_Contato = Convert.ToString(reader["TB009_Contato"]);
                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        public List<ContatoController> ContatosUnidade(long tb020Id)
        {
            List<ContatoController> retornoList = new List<ContatoController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append("SELECT ");
                sSql.Append("dbo.TB009_Contato.TB009_id, ");
                sSql.Append("dbo.TB009_Contato.TB020_id, ");
                sSql.Append("dbo.TB009_Contato.TB011_Id, ");
                sSql.Append("dbo.TB009_Contato.TB009_Tipo, ");
                sSql.Append("dbo.TB009_Contato.TB009_Contato, ");
                sSql.Append("dbo.TB009_Contato.TB009_ExibirPortal, ");
                sSql.Append("dbo.TB009_Contato.TB009_Nota, ");
                sSql.Append("dbo.TB009_Contato.TB009_CadastradoEm, ");
                sSql.Append("CadastradroPor.TB011_NomeExibicao AS TB009_CadastradoPor, ");
                sSql.Append("dbo.TB009_Contato.TB009_AlteradoEm, ");
                sSql.Append("AlteradorPor.TB011_NomeExibicao AS TB009_AlteradoPor ");
                sSql.Append("FROM  ");
                sSql.Append("dbo.TB009_Contato ");
                sSql.Append("INNER JOIN ");
                sSql.Append("dbo.TB011_APPUsuarios AS CadastradroPor ON dbo.TB009_Contato.TB009_CadastradoPor = CadastradroPor.TB011_Id ");
                sSql.Append("INNER JOIN ");
                sSql.Append("dbo.TB011_APPUsuarios AS AlteradorPor ON dbo.TB009_Contato.TB009_AlteradoPor = AlteradorPor.TB011_Id ");
                sSql.Append("WHERE ");
                sSql.Append("dbo.TB009_Contato.TB020_id = ");
                sSql.Append(tb020Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ContatoController obj = new ContatoController();

                    obj.TB009_id = Convert.ToInt64(reader["TB009_id"]);
                    obj.TB009_TipoS = Convert.ToString(reader["TB009_Tipo"]);
                    obj.TB009_Contato = Convert.ToString(reader["TB009_Contato"]);
                    obj.TB009_ExibirPortal = Convert.ToInt16(reader["TB009_ExibirPortal"]);
                    obj.TB009_Nota = Convert.ToString(reader["TB009_Nota"]);

                    obj.TB009_CadastradoEm = Convert.ToDateTime(reader["TB009_CadastradoEm"]);
                    obj.TB009_CadastradoPorNome = Convert.ToString(reader["TB009_CadastradoPor"]);

                    obj.TB009_AlteradoEm = Convert.ToDateTime(reader["TB009_AlteradoEm"]);
                    obj.TB009_AlteradoPorNome = Convert.ToString(reader["TB009_AlteradoPor"]);

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        public List<ContatoController> contatoTipoEmailPessoa(long tb013Id)
        {
            List<ContatoController> retornoList = new List<ContatoController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append("SELECT  ");
                sSql.Append("*");
                sSql.Append(" FROM  ");
                sSql.Append(" dbo.TB009_Contato ");
                sSql.Append(" WHERE ");
                sSql.Append(" TB009_Tipo = 3  ");
                sSql.Append(" AND ");
                sSql.Append(" TB013_id =  ");
                sSql.Append(tb013Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new ContatoController
                    {
                        TB009_id                    = Convert.ToInt64(reader["TB009_id"]),
                        TB009_TipoS                 = Convert.ToString(reader["TB009_Tipo"]),
                        TB009_Contato               = Convert.ToString(reader["TB009_Contato"]),
                        TB009_ExibirPortal          = Convert.ToInt16(reader["TB009_ExibirPortal"]),
                        TB009_Nota                  = Convert.ToString(reader["TB009_Nota"]),
                        TB009_CadastradoEm          = Convert.ToDateTime(reader["TB009_CadastradoEm"]),
                        TB009_CadastradoPorNome     = Convert.ToString(reader["TB009_CadastradoPor"]),
                        TB009_AlteradoEm            = Convert.ToDateTime(reader["TB009_AlteradoEm"]),
                        TB009_AlteradoPorNome       = Convert.ToString(reader["TB009_AlteradoPor"])
                    };

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }

        public List<ContatoController> contatosCorporativoExportar()
        {
            List<ContatoController> retornoList = new List<ContatoController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" SELECT  ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_TipoContrato ");
                sSql.Append(" , dbo.TB009_Contato.TB009_id ");
                sSql.Append(" , dbo.TB009_Contato.TB009_Tipo ");
                sSql.Append(" , dbo.TB009_Contato.TB009_Contato ");
                sSql.Append(" , dbo.TB009_Contato.TB013_id ");
                sSql.Append(" , dbo.TB009_Contato.TB020_id ");
                sSql.Append(" FROM     ");
                sSql.Append(" dbo.TB020_Unidades  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB012_Contratos ON dbo.TB020_Unidades.TB012_id = dbo.TB012_Contratos.TB012_id ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB009_Contato ON dbo.TB020_Unidades.TB020_id = dbo.TB009_Contato.TB020_id ");
                sSql.Append(" WHERE ");
                sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato = 3 ");
                sSql.Append(" ORDER BY ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id ");
                sSql.Append(" , dbo.TB020_Unidades.TB020_id ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new ContatoController
                    {
                        TB009_id        = Convert.ToInt64(reader["TB009_id"]),
                        TB009_TipoS     = Convert.ToString(reader["TB009_Tipo"]),
                        TB009_Contato   = Convert.ToString(reader["TB009_Contato"]).Replace("-","").Replace("/", "").Replace("(", "").Replace(")", "").Trim(),
                        TB020_id        = Convert.ToInt64(reader["TB020_id"]),
                        TB012_id        = Convert.ToInt64(reader["TB012_id"]),
                        TB013_id        = Convert.ToInt64(reader["TB013_id"])
                    };

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        public List<ContatoController> contatosFamiliarCorporativoExportar()
        {
            List<ContatoController> retornoList = new List<ContatoController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" SELECT  ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_TipoContrato ");
                sSql.Append(" , dbo.TB009_Contato.TB009_id ");
                sSql.Append(" , dbo.TB009_Contato.TB009_Tipo ");
                sSql.Append(" , dbo.TB009_Contato.TB009_Contato ");
                sSql.Append(" , dbo.TB009_Contato.TB013_id ");
                sSql.Append(" , dbo.TB009_Contato.TB020_id ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB012_Contratos ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB012_id = dbo.TB013_Pessoa.TB012_id  ");
                sSql.Append(" LEFT OUTER JOIN ");
                sSql.Append(" dbo.TB009_Contato ON dbo.TB013_Pessoa.TB013_id = dbo.TB009_Contato.TB013_id ");
                sSql.Append(" WHERE ");
                sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato = 4 ");
                sSql.Append(" AND ");
                sSql.Append(" NOT(dbo.TB009_Contato.TB009_Contato IS NULL) ");
                sSql.Append(" ORDER BY  ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new ContatoController
                    {
                        TB009_id = Convert.ToInt64(reader["TB009_id"]),
                        TB009_TipoS = Convert.ToString(reader["TB009_Tipo"]),
                        TB009_Contato = Convert.ToString(reader["TB009_Contato"]).Replace("-", "").Replace("/", "").Replace("(", "").Replace(")", "").Trim(),
                        TB020_id = Convert.ToInt64(reader["TB020_id"]),
                        TB012_id = Convert.ToInt64(reader["TB012_id"]),
                        TB013_id = Convert.ToInt64(reader["TB013_id"])
                    };

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
    }
}

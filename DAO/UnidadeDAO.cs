using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace DAO
{
    public class UnidadeDAO
    {
        /// <summary>
        /// Descrição:  Incluir nova Unidade
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public UnidadeController UnidadeInsert(UnidadeController Unidade)
        {
            UnidadeController Retorno = new UnidadeController();
            try
            {
                string insertSql = "INSERT INTO TB020_Unidades (TB020_NomeExibicaoDetalhes,TB020_CategoriaExibicao,TB020_Matriz,TB012_id,TB020_RazaoSocial,TB020_NomeFantasia,TB020_TipoPessoa,TB020_Documento,TB006_id,TB020_Cep,TB020_Logradouro,TB020_Numero,TB020_Bairro,TB020_Complemento,TB020_TextoPortal,TB020_CadastradoEm,TB020_CadastradoPor,TB020_AlteradoEm,TB020_AlteradoPor,TB020_Status,TB020_Desconto) VALUES (@TB020_NomeExibicaoDetalhes,@TB020_CategoriaExibicao,@TB020_Matriz,@TB012_id,@TB020_RazaoSocial,@TB020_NomeFantasia,@TB020_TipoPessoa,@TB020_Documento,@TB006_id,@TB020_Cep,@TB020_Logradouro,@TB020_Numero,@TB020_Bairro,@TB020_Complemento,@TB020_TextoPortal,@TB020_CadastradoEm,@TB020_CadastradoPor,@TB020_AlteradoEm,@TB020_AlteradoPor,@TB020_Status,@TB020_Desconto) SELECT SCOPE_IDENTITY()";

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand(insertSql, con);
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@TB012_id", Unidade.TB012_id);
                    command.Parameters.AddWithValue("@TB020_Matriz", Unidade.TB020_Matriz);
                    command.Parameters.AddWithValue("@TB020_RazaoSocial", Unidade.TB020_RazaoSocial);
                    command.Parameters.AddWithValue("@TB020_NomeFantasia", Unidade.TB020_NomeFantasia);
                    command.Parameters.AddWithValue("@TB020_NomeExibicaoDetalhes", Unidade.TB020_NomeExibicaoDetalhes.TrimEnd().TrimStart());
                    command.Parameters.AddWithValue("@TB020_CategoriaExibicao", Unidade.TB020_CategoriaExibicao.TrimEnd().TrimStart());
                    command.Parameters.AddWithValue("@TB020_TipoPessoa", Unidade.TB020_TipoPessoa);
                    command.Parameters.AddWithValue("@TB020_Documento", Unidade.TB020_Documento.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", ""));
                    command.Parameters.AddWithValue("@TB006_id", Unidade.TB006_id);
                    command.Parameters.AddWithValue("@TB020_Cep", Unidade.TB020_Cep);
                    command.Parameters.AddWithValue("@TB020_Logradouro", Unidade.TB020_Logradouro);
                    command.Parameters.AddWithValue("@TB020_Numero", Unidade.TB020_Numero);
                    command.Parameters.AddWithValue("@TB020_Bairro", Unidade.TB020_Bairro);
                    command.Parameters.AddWithValue("@TB020_Complemento", Unidade.TB020_Complemento);
                    command.Parameters.AddWithValue("@TB020_TextoPortal", Unidade.TB020_TextoPortal);
                    command.Parameters.AddWithValue("@TB020_CadastradoEm", DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                    command.Parameters.AddWithValue("@TB020_CadastradoPor", Unidade.TB020_CadastradoPor);
                    command.Parameters.AddWithValue("@TB020_AlteradoEm", DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                    command.Parameters.AddWithValue("@TB020_AlteradoPor", Unidade.TB020_AlteradoPor);
                    command.Parameters.AddWithValue("@TB020_Status", Unidade.TB020_StatusS);
                    command.Parameters.AddWithValue("@TB020_Desconto", "-");



                    Retorno.TB020_id = Convert.ToInt32(command.ExecuteScalar());

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;

        }

        /// <summary>
        /// Descrição:  Pesquisar dados da Pessoa pelo TB013_id
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       30/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public UnidadeController UnidadeMatriz(long TB012_id)
        {
            UnidadeController Unidade_C = new UnidadeController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB020_Unidades.TB020_CategoriaExibicao, dbo.TB020_Unidades.TB020_id, dbo.TB020_Unidades.TB012_id, dbo.TB020_Unidades.TB020_Matriz, dbo.TB020_Unidades.TB020_NomeExibicaoDetalhes  ,                      dbo.TB020_Unidades.TB020_RazaoSocial, dbo.TB020_Unidades.TB020_NomeFantasia,  ");
                sSQL.Append(" dbo.TB020_Unidades.TB020_TipoPessoa, dbo.TB020_Unidades.TB020_Documento, dbo.TB006_Municipio.TB006_id, dbo.TB006_Municipio.TB006_Municipio, dbo.TB005_Estado.TB005_Id,  ");
                sSQL.Append(" dbo.TB005_Estado.TB005_Estado, dbo.TB003_Pais.TB003_id, dbo.TB003_Pais.TB003_Pais, dbo.TB020_Unidades.TB020_Cep, dbo.TB020_Unidades.TB020_Logradouro, dbo.TB020_Unidades.TB020_Numero,  ");
                sSQL.Append(" dbo.TB020_Unidades.TB020_Bairro,dbo.TB020_Unidades.TB020_logo,dbo.TB020_Unidades.TB020_AlteradoEm,dbo.TB020_Unidades.TB020_TextoPortal, dbo.TB020_Unidades.TB020_CategoriaExibicao ");
                sSQL.Append(" FROM dbo.TB006_Municipio INNER JOIN ");
                sSQL.Append(" dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id INNER JOIN ");
                sSQL.Append(" dbo.TB003_Pais ON dbo.TB005_Estado.TB003_Id = dbo.TB003_Pais.TB003_id INNER JOIN ");
                sSQL.Append(" dbo.TB020_Unidades ON dbo.TB006_Municipio.TB006_id = dbo.TB020_Unidades.TB006_id ");
                sSQL.Append(" WHERE dbo.TB020_Unidades.TB020_Matriz = 1 ");
                sSQL.Append(" AND dbo.TB020_Unidades.TB012_id = ");

                sSQL.Append(TB012_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Unidade_C.TB020_id = Convert.ToInt64(reader["TB020_id"]);
                    Unidade_C.TB012_id = Convert.ToInt64(reader["TB012_id"]);
                    Unidade_C.TB020_RazaoSocial = reader["TB020_RazaoSocial"].ToString();
                    Unidade_C.TB020_NomeFantasia = reader["TB020_NomeFantasia"].ToString();

                    Unidade_C.TB020_NomeExibicaoDetalhes = reader["TB020_NomeExibicaoDetalhes"] is DBNull ? reader["TB020_NomeFantasia"].ToString() : reader["TB020_NomeExibicaoDetalhes"].ToString().TrimEnd().TrimStart();
                    Unidade_C.TB020_Documento = reader["TB020_Documento"].ToString();
                    Unidade_C.TB020_AlteradoEm = Convert.ToDateTime(reader["TB020_AlteradoEm"].ToString());
                    Unidade_C.TB020_TextoPortal = reader["TB020_TextoPortal"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Unidade_C;
        }

        public List<UnidadeController> UnidadesContrato(Int64 TB012_id)
        {
            List<UnidadeController> Retorno = new List<UnidadeController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append("SELECT TB020_id, TB020_Matriz, TB012_id, TB020_NomeFantasia, TB020_Status ");
                sSQL.Append(" FROM dbo.TB020_Unidades ");
                sSQL.Append(" WHERE TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append(" ORDER BY TB020_Matriz");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UnidadeController obj = new UnidadeController();
                    obj.TB020_id = Convert.ToInt64(reader["TB020_id"]);
                    obj.TB020_NomeFantasia = reader["TB020_NomeFantasia"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    obj.TB020_StatusS = Enum.GetName(typeof(UnidadeController.TB020_StatusE), Convert.ToInt16(reader["TB020_Status"]));
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

        public UnidadeController UnidadeSelect(long TB020_id)
        {
            UnidadeController Unidade_C = new UnidadeController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB020_Unidades.TB020_id, dbo.TB020_Unidades.TB020_Matriz, dbo.TB020_Unidades.TB012_id, dbo.TB020_Unidades.TB020_RazaoSocial, ");
                sSQL.Append(" dbo.TB020_Unidades.TB020_NomeFantasia, dbo.TB020_Unidades.TB020_TipoPessoa, dbo.TB020_Unidades.TB020_Documento, dbo.TB006_Municipio.TB006_id, dbo.TB006_Municipio.TB006_Municipio, ");
                sSQL.Append(" dbo.TB005_Estado.TB005_Id, dbo.TB005_Estado.TB005_Estado, dbo.TB003_Pais.TB003_id, dbo.TB003_Pais.TB003_Pais, dbo.TB020_Unidades.TB020_Cep, dbo.TB020_Unidades.TB020_Logradouro, ");
                sSQL.Append(" dbo.TB020_Unidades.TB020_Numero, dbo.TB020_Unidades.TB020_Bairro, dbo.TB020_Unidades.TB020_Complemento, dbo.TB020_Unidades.TB020_TextoPortal, dbo.TB020_Unidades.TB020_logo, ");
                sSQL.Append(" dbo.TB020_Unidades.TB020_NomeExibicaoDetalhes,dbo.TB020_Unidades.TB020_CategoriaExibicao, ");
                sSQL.Append(" dbo.TB020_Unidades.TB020_Status, dbo.TB005_Estado.TB005_Sigla ");
                sSQL.Append(" FROM dbo.TB005_Estado INNER JOIN ");
                sSQL.Append(" dbo.TB003_Pais ON dbo.TB005_Estado.TB003_Id = dbo.TB003_Pais.TB003_id INNER JOIN ");
                sSQL.Append(" dbo.TB006_Municipio ON dbo.TB005_Estado.TB005_Id = dbo.TB006_Municipio.TB005_Id INNER JOIN ");
                sSQL.Append(" dbo.TB020_Unidades ON dbo.TB006_Municipio.TB006_id = dbo.TB020_Unidades.TB006_id ");
                sSQL.Append(" WHERE dbo.TB020_Unidades.TB020_id =  ");
                sSQL.Append(TB020_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Unidade_C.TB020_id = Convert.ToInt64(reader["TB020_id"]);
                    Unidade_C.TB020_RazaoSocial = reader["TB020_RazaoSocial"].ToString();
                    Unidade_C.TB020_NomeFantasia = reader["TB020_NomeFantasia"].ToString();

                    Unidade_C.TB020_NomeExibicaoDetalhes = reader["TB020_NomeExibicaoDetalhes"] is DBNull ? reader["TB020_NomeFantasia"].ToString() : reader["TB020_NomeExibicaoDetalhes"].ToString();
                    Unidade_C.TB020_CategoriaExibicao = reader["TB020_CategoriaExibicao"] is DBNull ? "SEM CATEGORIA" : reader["TB020_CategoriaExibicao"].ToString();
                    Unidade_C.TB020_Documento = reader["TB020_Documento"].ToString();
                    Unidade_C.TB020_Cep = reader["TB020_Cep"].ToString();
                    Unidade_C.TB020_Logradouro = reader["TB020_Logradouro"].ToString();
                    Unidade_C.TB020_Numero = reader["TB020_Numero"].ToString();
                    Unidade_C.TB020_Bairro = reader["TB020_Bairro"].ToString();
                    Unidade_C.TB020_Complemento = reader["TB020_Complemento"].ToString();
                    Unidade_C.TB020_TextoPortal = reader["TB020_TextoPortal"].ToString();
                    Unidade_C.TB020_TipoPessoa = Convert.ToInt16(reader["TB020_TipoPessoa"].ToString());
                    Unidade_C.TB020_StatusS = reader["TB020_Status"].ToString();
                    PaisController objPais = new PaisController();
                    Unidade_C.Pais = objPais;
                    Unidade_C.Pais.TB003_id = Convert.ToInt64(reader["TB003_id"]);

                    EstadoController objEstado = new EstadoController();
                    Unidade_C.Estado = objEstado;
                    Unidade_C.Estado.TB005_Id = Convert.ToInt64(reader["TB005_Id"]);
                    Unidade_C.Estado.TB005_Estado = reader["TB005_Estado"].ToString().TrimEnd();
                    Unidade_C.Estado.TB005_Sigla = reader["TB005_Sigla"].ToString().TrimEnd();

                    MunicipioController objMunicipio = new MunicipioController();
                    Unidade_C.Municipio = objMunicipio;
                    Unidade_C.Municipio.TB006_id = Convert.ToInt64(reader["TB006_id"]);
                    Unidade_C.Municipio.TB006_Municipio= reader["TB006_Municipio"].ToString().TrimEnd();

                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Unidade_C;
        }

        public UnidadeController UnidadeAtualizar(UnidadeController Unidade_C)
        {
            UnidadeController Unidade_R = new UnidadeController();
            try
            {
                SqlConnection conexaoSQLServer = conexaoSQLServer = new SqlConnection(ParametrosDAO.StringConexao);
                SqlCommand sqlcmd = default(SqlCommand);

                conexaoSQLServer.Open();
                sqlcmd = new SqlCommand();
                sqlcmd.CommandTimeout = 300;
                sqlcmd.Connection = conexaoSQLServer;
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append("update TB020_Unidades set ");
                sSQL.Append("TB020_RazaoSocial = @TB020_RazaoSocial, ");
                sSQL.Append("TB020_CategoriaExibicao = @TB020_CategoriaExibicao, ");
                sSQL.Append("TB020_NomeFantasia = @TB020_NomeFantasia, ");
                sSQL.Append("TB020_NomeExibicaoDetalhes = @TB020_NomeExibicaoDetalhes, ");
                sSQL.Append("TB020_Documento = @TB020_Documento, ");
                sSQL.Append("TB006_id = @TB006_id, ");
                sSQL.Append("TB020_Cep = @TB020_Cep, ");
                sSQL.Append("TB020_Logradouro = @TB020_Logradouro, ");
                sSQL.Append("TB020_Numero = @TB020_Numero, ");
                sSQL.Append("TB020_Bairro = @TB020_Bairro, ");
                sSQL.Append("TB020_Complemento = @TB020_Complemento, ");
                sSQL.Append("TB020_TextoPortal = @TB020_TextoPortal, ");
                sSQL.Append("TB020_AlteradoPor = @TB020_AlteradoPor ");
                sSQL.Append("where ");
                sSQL.Append("TB020_id = @TB020_id ");
                sqlcmd.CommandText = sSQL.ToString();
                sqlcmd.Parameters.Add("@TB020_id", System.Data.SqlDbType.BigInt);
                sqlcmd.Parameters.Add("@TB020_RazaoSocial", System.Data.SqlDbType.VarChar, 150);
                sqlcmd.Parameters.Add("@TB020_NomeFantasia", System.Data.SqlDbType.VarChar, 150);
                sqlcmd.Parameters.Add("@TB020_NomeExibicaoDetalhes", System.Data.SqlDbType.VarChar, 60);
                sqlcmd.Parameters.Add("@TB020_CategoriaExibicao", System.Data.SqlDbType.VarChar, 32);
                sqlcmd.Parameters.Add("@TB020_Documento", System.Data.SqlDbType.VarChar, 14);
                sqlcmd.Parameters.Add("@TB006_id", System.Data.SqlDbType.BigInt);
                sqlcmd.Parameters.Add("@TB020_Cep", System.Data.SqlDbType.VarChar, 10);
                sqlcmd.Parameters.Add("@TB020_Logradouro", System.Data.SqlDbType.VarChar, 150);
                sqlcmd.Parameters.Add("@TB020_Numero", System.Data.SqlDbType.VarChar, 10);
                sqlcmd.Parameters.Add("@TB020_Bairro", System.Data.SqlDbType.VarChar, 50);
                sqlcmd.Parameters.Add("@TB020_Complemento", System.Data.SqlDbType.VarChar, 50);
                sqlcmd.Parameters.Add("@TB020_TextoPortal", System.Data.SqlDbType.VarChar, 700);
                sqlcmd.Parameters.Add("@TB020_AlteradoPor", System.Data.SqlDbType.BigInt);
                sqlcmd.Parameters["@TB020_id"].Value = Unidade_C.TB020_id;
                sqlcmd.Parameters["@TB020_RazaoSocial"].Value = Unidade_C.TB020_RazaoSocial;
                sqlcmd.Parameters["@TB020_NomeFantasia"].Value = Unidade_C.TB020_NomeFantasia;
                sqlcmd.Parameters["@TB020_NomeExibicaoDetalhes"].Value = Unidade_C.TB020_NomeExibicaoDetalhes;
                sqlcmd.Parameters["@TB020_CategoriaExibicao"].Value = Unidade_C.TB020_CategoriaExibicao.TrimStart().TrimEnd();
                sqlcmd.Parameters["@TB020_Documento"].Value = Unidade_C.TB020_Documento.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "");
                sqlcmd.Parameters["@TB006_id"].Value = Unidade_C.TB006_id;
                sqlcmd.Parameters["@TB020_Cep"].Value = Unidade_C.TB020_Cep.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "");
                sqlcmd.Parameters["@TB020_Logradouro"].Value = Unidade_C.TB020_Logradouro;
                sqlcmd.Parameters["@TB020_Numero"].Value = Unidade_C.TB020_Numero.TrimEnd();
                sqlcmd.Parameters["@TB020_Bairro"].Value = Unidade_C.TB020_Bairro.TrimEnd();
                sqlcmd.Parameters["@TB020_Complemento"].Value = Unidade_C.TB020_Complemento;
                sqlcmd.Parameters["@TB020_TextoPortal"].Value = Unidade_C.TB020_TextoPortal;
                sqlcmd.Parameters["@TB020_AlteradoPor"].Value = Unidade_C.TB020_AlteradoPor;

                int iresultado = sqlcmd.ExecuteNonQuery();

                Unidade_R.TB020_id = Unidade_C.TB020_id;
                conexaoSQLServer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Unidade_R;
        }

        public UnidadeController UnidadeAtualizarCorporativo(UnidadeController Unidade_C)
        {
            UnidadeController Unidade_R = new UnidadeController();
            try
            {
                SqlConnection conexaoSQLServer = conexaoSQLServer = new SqlConnection(ParametrosDAO.StringConexao);
                SqlCommand sqlcmd = default(SqlCommand);

                conexaoSQLServer.Open();
                sqlcmd = new SqlCommand();
                sqlcmd.CommandTimeout = 300;
                sqlcmd.Connection = conexaoSQLServer;
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append("update TB020_Unidades set ");
                sSQL.Append("TB020_RazaoSocial = @TB020_RazaoSocial, ");
                sSQL.Append("TB020_CategoriaExibicao = @TB020_CategoriaExibicao, ");
                sSQL.Append("TB020_NomeFantasia = @TB020_NomeFantasia, ");
                sSQL.Append("TB020_NomeExibicaoDetalhes = @TB020_NomeExibicaoDetalhes, ");
                sSQL.Append("TB020_Documento = @TB020_Documento, ");
                sSQL.Append("TB006_id = @TB006_id, ");
                sSQL.Append("TB020_Cep = @TB020_Cep, ");
                sSQL.Append("TB020_Logradouro = @TB020_Logradouro, ");
                sSQL.Append("TB020_Numero = @TB020_Numero, ");
                sSQL.Append("TB020_Bairro = @TB020_Bairro, ");
                sSQL.Append("TB020_Complemento = @TB020_Complemento, ");
                sSQL.Append("TB020_TextoPortal = @TB020_TextoPortal, ");
                sSQL.Append("TB020_Status = @TB020_Status, ");
                sSQL.Append("TB020_AlteradoPor = @TB020_AlteradoPor ");
                sSQL.Append("where ");
                sSQL.Append("TB020_id = @TB020_id ");
                sqlcmd.CommandText = sSQL.ToString();
                sqlcmd.Parameters.Add("@TB020_id", System.Data.SqlDbType.BigInt);
                sqlcmd.Parameters.Add("@TB020_RazaoSocial", System.Data.SqlDbType.VarChar, 150);
                sqlcmd.Parameters.Add("@TB020_NomeFantasia", System.Data.SqlDbType.VarChar, 150);
                sqlcmd.Parameters.Add("@TB020_NomeExibicaoDetalhes", System.Data.SqlDbType.VarChar, 60);
                sqlcmd.Parameters.Add("@TB020_CategoriaExibicao", System.Data.SqlDbType.VarChar, 32);
                sqlcmd.Parameters.Add("@TB020_Documento", System.Data.SqlDbType.VarChar, 14);
                sqlcmd.Parameters.Add("@TB006_id", System.Data.SqlDbType.BigInt);
                sqlcmd.Parameters.Add("@TB020_Cep", System.Data.SqlDbType.VarChar, 10);
                sqlcmd.Parameters.Add("@TB020_Logradouro", System.Data.SqlDbType.VarChar, 150);
                sqlcmd.Parameters.Add("@TB020_Numero", System.Data.SqlDbType.VarChar, 10);
                sqlcmd.Parameters.Add("@TB020_Bairro", System.Data.SqlDbType.VarChar, 50);
                sqlcmd.Parameters.Add("@TB020_Complemento", System.Data.SqlDbType.VarChar, 50);
                sqlcmd.Parameters.Add("@TB020_TextoPortal", System.Data.SqlDbType.VarChar, 700);
                sqlcmd.Parameters.Add("@TB020_Status", System.Data.SqlDbType.Int);
                sqlcmd.Parameters.Add("@TB020_AlteradoPor", System.Data.SqlDbType.BigInt);
                sqlcmd.Parameters["@TB020_id"].Value = Unidade_C.TB020_id;
                sqlcmd.Parameters["@TB020_RazaoSocial"].Value = Unidade_C.TB020_RazaoSocial;
                sqlcmd.Parameters["@TB020_NomeFantasia"].Value = Unidade_C.TB020_NomeFantasia;
                sqlcmd.Parameters["@TB020_NomeExibicaoDetalhes"].Value = Unidade_C.TB020_NomeExibicaoDetalhes;
                sqlcmd.Parameters["@TB020_CategoriaExibicao"].Value = Unidade_C.TB020_CategoriaExibicao.TrimStart().TrimEnd();
                sqlcmd.Parameters["@TB020_Documento"].Value = Unidade_C.TB020_Documento.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "");
                sqlcmd.Parameters["@TB006_id"].Value = Unidade_C.TB006_id;
                sqlcmd.Parameters["@TB020_Cep"].Value = Unidade_C.TB020_Cep.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "");
                sqlcmd.Parameters["@TB020_Logradouro"].Value = Unidade_C.TB020_Logradouro;
                sqlcmd.Parameters["@TB020_Numero"].Value = Unidade_C.TB020_Numero.TrimEnd();
                sqlcmd.Parameters["@TB020_Bairro"].Value = Unidade_C.TB020_Bairro.TrimEnd();
                sqlcmd.Parameters["@TB020_Complemento"].Value = Unidade_C.TB020_Complemento;
                sqlcmd.Parameters["@TB020_TextoPortal"].Value = Unidade_C.TB020_TextoPortal;
                sqlcmd.Parameters["@TB020_Status"].Value = Unidade_C.TB020_StatusS;
                sqlcmd.Parameters["@TB020_AlteradoPor"].Value = Unidade_C.TB020_AlteradoPor;

                int iresultado = sqlcmd.ExecuteNonQuery();

                Unidade_R.TB020_id = Unidade_C.TB020_id;
                conexaoSQLServer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Unidade_R;
        }

        public UnidadeController UnidadeDescontoSelect(long TB020_id)
        {
            UnidadeController Unidade_C = new UnidadeController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT TB020_id, TB020_Desconto FROM dbo.TB020_Unidades ");
                sSQL.Append(" WHERE dbo.TB020_Unidades.TB020_id =  ");
                sSQL.Append(TB020_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Unidade_C.TB020_Desconto = (byte[])reader["TB020_Desconto"];
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Unidade_C;
        }

        public Boolean UnidadeAtualizarDesconto(long TB020_id, byte[] Desconto)
        {
            try
            {
                SqlConnection conexaoSQLServer = conexaoSQLServer = new SqlConnection(ParametrosDAO.StringConexao);
                string update = "update TB020_Unidades set TB020_Desconto = @TB020_Desconto where TB020_id = @TB020_id ";


                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand(update, con);
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@TB020_id", TB020_id);

                    command.Parameters.AddWithValue("@TB020_Desconto", Desconto);
                    command.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                //con.Close();
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// Descrição:  Verifica se o CPF da Unidade ja esta cadastrado
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       30/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public UnidadeController UnidadeCNPJJaCadastrado(string CNPJ, int TB012_TipoContrato)
        {
            UnidadeController Retorno = new UnidadeController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB020_Unidades.TB020_id, dbo.TB020_Unidades.TB020_Documento, dbo.TB012_Contratos.TB012_id, dbo.TB012_Contratos.TB012_TipoContrato ");
                sSQL.Append(" FROM dbo.TB020_Unidades INNER JOIN ");
                sSQL.Append(" dbo.TB012_Contratos ON dbo.TB020_Unidades.TB012_id = dbo.TB012_Contratos.TB012_id ");
                sSQL.Append("  WHERE dbo.TB020_Unidades.TB020_Documento =  ");
                sSQL.Append(" '");
                sSQL.Append(CNPJ.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim());
                sSQL.Append("'");
                sSQL.Append(" AND dbo.TB012_Contratos.TB012_TipoContrato = ");
                sSQL.Append(TB012_TipoContrato);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB012_idCorporativo = Convert.ToInt64(reader["TB012_id"]);
                    Retorno.TB020_id = Convert.ToInt64(reader["TB020_id"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public UnidadeController UnidadeCNPJJaCadastradoCorporativo(string CNPJ)
        {
            UnidadeController Retorno = new UnidadeController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT        dbo.TB020_Unidades.TB020_id, dbo.TB020_Unidades.TB020_Documento, dbo.TB012_Contratos.TB012_id, dbo.TB020_Unidades.TB012_idCorporativo FROM   dbo.TB020_Unidades INNER JOIN dbo.TB012_Contratos ON dbo.TB020_Unidades.TB012_idCorporativo = dbo.TB012_Contratos.TB012_id ");
                sSQL.Append("  WHERE dbo.TB020_Unidades.TB020_Documento =   ");
                sSQL.Append("'");
                sSQL.Append(CNPJ.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim());
                sSQL.Append("'");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB012_idCorporativo = Convert.ToInt64(reader["TB012_idCorporativo"]);
                    Retorno.TB020_id = Convert.ToInt64(reader["TB020_id"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public Boolean CorporativoVinculaUnidadeContrato(long TB020_id, long TB012_idCorporativo)
        {
            try
            {
                SqlConnection conexaoSQLServer = conexaoSQLServer = new SqlConnection(ParametrosDAO.StringConexao);
                SqlCommand sqlcmd = default(SqlCommand);

                conexaoSQLServer.Open();
                sqlcmd = new SqlCommand();
                sqlcmd.CommandTimeout = 300;
                sqlcmd.Connection = conexaoSQLServer;
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append("update TB020_Unidades set ");
                sSQL.Append("TB012_idCorporativo = @TB012_idCorporativo ");
                sSQL.Append("where ");
                sSQL.Append("TB020_id = @TB020_id ");
                sqlcmd.CommandText = sSQL.ToString();
                sqlcmd.Parameters.Add("@TB020_id", System.Data.SqlDbType.BigInt);
                sqlcmd.Parameters.Add("@TB012_idCorporativo", System.Data.SqlDbType.BigInt);
                sqlcmd.Parameters["@TB020_id"].Value = TB020_id;
                sqlcmd.Parameters["@TB012_idCorporativo"].Value = TB012_idCorporativo;
                sqlcmd.ExecuteNonQuery();

                conexaoSQLServer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }



    }
}

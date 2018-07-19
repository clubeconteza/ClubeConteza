using Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class EnderecoDao
    {
        /// <summary>
        /// Descrição:  Retorna lista de Paises
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       07/10/2015
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public DataSet PaisController()
        {
            var dsRetorno = new DataSet();
            try
            {
                var sSql = new StringBuilder();
                sSql.Append("SELECT ");
                sSql.Append("TB003_id,");
                sSql.Append("TB003_Pais,");
                sSql.Append("TB003_DDI");
                sSql.Append(" FROM ");
                sSql.Append("dbo.TB003_Pais");
                sSql.Append(" ORDER BY  ");
                sSql.Append("TB003_Pais");
 
                var con = new SqlConnection(ParametrosDAO.StringConexao);

                con.Open();
                    var da = new SqlDataAdapter(sSql.ToString(), con);
                    dsRetorno.Tables.Add("TB003_id");
                    dsRetorno.EnforceConstraints = false;
                    dsRetorno.Tables["TB003_id"].BeginLoadData();
                    da.Fill(dsRetorno.Tables["TB003_id"]);
                    dsRetorno.Tables["TB003_id"].EndLoadData();
                    dsRetorno.Tables.Add("TB003_Pais");
                    dsRetorno.EnforceConstraints = false;
                    dsRetorno.Tables["TB003_Pais"].BeginLoadData();
                    da.Fill(dsRetorno.Tables["TB003_Pais"]);
                    dsRetorno.Tables["TB003_Pais"].EndLoadData();
                    dsRetorno.Tables.Add("TB003_DDI");
                    dsRetorno.EnforceConstraints = false;
                    dsRetorno.Tables["TB003_DDI"].BeginLoadData();
                    da.Fill(dsRetorno.Tables["TB003_DDI"]);
                    dsRetorno.Tables["TB003_DDI"].EndLoadData();
                con.Close();

            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return dsRetorno;
        }
        /// <summary>
        /// Descrição:  Retorna lista de Estados ligados a um pais expecifico
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       07/10/2015
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public DataSet EstadosController(PaisController filtro)
        {
            var dsRetorno = new DataSet();
            try
            {
                var sSql = new StringBuilder();
                sSql.Append("SELECT ");
                sSql.Append("TB005_Id,");
                sSql.Append("TB005_Estado");
                sSql.Append(" FROM ");
                sSql.Append("dbo.TB005_Estado");
                sSql.Append(" WHERE ");
                sSql.Append("TB003_Id =");
                sSql.Append(filtro.TB003_id);
                sSql.Append(" ORDER BY ");
                sSql.Append("TB005_Estado");

                var con = new SqlConnection(ParametrosDAO.StringConexao);

                con.Open();
                var da = new SqlDataAdapter(sSql.ToString(), con);
                dsRetorno.Tables.Add("TB005_Id");
                dsRetorno.EnforceConstraints = false;
                dsRetorno.Tables["TB005_Id"].BeginLoadData();
                da.Fill(dsRetorno.Tables["TB005_Id"]);
                dsRetorno.Tables["TB005_Id"].EndLoadData();
                dsRetorno.Tables.Add("TB005_Estado");
                dsRetorno.EnforceConstraints = false;
                dsRetorno.Tables["TB005_Estado"].BeginLoadData();
                da.Fill(dsRetorno.Tables["TB005_Estado"]);
                dsRetorno.Tables["TB005_Estado"].EndLoadData();
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return dsRetorno;
        }
        /// <summary>
        /// Descrição:  Retorna lista de Estados ligados a um pais expecifico
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       07/10/2015
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public DataSet MunicipioController(EstadoController filtro)
        {
            var dsRetorno = new DataSet();
            try
            {
                var sSql = new StringBuilder();
                sSql.Append("SELECT ");
                sSql.Append("TB006_id,");
                sSql.Append("TB006_Municipio");
                sSql.Append(" FROM ");
                sSql.Append("dbo.TB006_Municipio");
                sSql.Append(" WHERE ");
                sSql.Append("TB005_Id =");
                sSql.Append(filtro.TB005_Id);
                sSql.Append(" ORDER BY ");
                sSql.Append("TB006_Municipio");

                var con = new SqlConnection(ParametrosDAO.StringConexao);

                con.Open();
                var da = new SqlDataAdapter(sSql.ToString(), con);
                dsRetorno.Tables.Add("TB006_id");
                dsRetorno.EnforceConstraints = false;
                dsRetorno.Tables["TB006_id"].BeginLoadData();
                da.Fill(dsRetorno.Tables["TB006_id"]);
                dsRetorno.Tables["TB006_id"].EndLoadData();
                dsRetorno.Tables.Add("TB006_Municipio");
                dsRetorno.EnforceConstraints = false;
                dsRetorno.Tables["TB006_Municipio"].BeginLoadData();
                da.Fill(dsRetorno.Tables["TB006_Municipio"]);
                dsRetorno.Tables["TB006_Municipio"].EndLoadData();

                con.Close();

            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return dsRetorno;
        }
        /// <summary>
        /// Descrição:  Retorna dados do CEP informado
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       07/10/2015
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public DataSet Cep(CEPController filtro)
        {
            var dsRetorno = new DataSet();
            try
            {
                var sSql = new StringBuilder();
                sSql.Append("SELECT ");
                sSql.Append("dbo.TB004_Cep.TB004_id,");
                sSql.Append("dbo.TB004_Cep.TB004_Cep,");
                sSql.Append("dbo.TB005_Estado.TB005_Id,");
                sSql.Append("dbo.TB005_Estado.TB005_Estado,");
                sSql.Append("dbo.TB006_Municipio.TB006_id,");
                sSql.Append("dbo.TB006_Municipio.TB006_Municipio,");
                sSql.Append("dbo.TB004_Cep.TB004_Logradouro,");
                sSql.Append("dbo.TB004_Cep.TB004_Bairro ");
                sSql.Append(" FROM ");
                sSql.Append("dbo.TB005_Estado");
                sSql.Append(" INNER JOIN ");
                sSql.Append("dbo.TB006_Municipio ON dbo.TB005_Estado.TB005_Id = dbo.TB006_Municipio.TB005_Id");
                sSql.Append(" INNER JOIN ");
                sSql.Append("dbo.TB004_Cep ON dbo.TB006_Municipio.TB006_id = dbo.TB004_Cep.TB006_id ");
                sSql.Append(" WHERE ");
                sSql.Append("dbo.TB004_Cep.TB004_Cep = ");
                sSql.Append(filtro.TB004_Cep);
               
                var con = new SqlConnection(ParametrosDAO.StringConexao);

                con.Open();
                var da = new SqlDataAdapter(sSql.ToString(), con);

                dsRetorno.Tables.Add("TB004_id");
                dsRetorno.EnforceConstraints = false;
                dsRetorno.Tables["TB004_id"].BeginLoadData();
                da.Fill(dsRetorno.Tables["TB004_id"]);
                dsRetorno.Tables["TB004_id"].EndLoadData();
                dsRetorno.Tables.Add("TB005_Id");
                dsRetorno.EnforceConstraints = false;
                dsRetorno.Tables["TB005_Id"].BeginLoadData();
                da.Fill(dsRetorno.Tables["TB005_Id"]);
                dsRetorno.Tables["TB005_Id"].EndLoadData();
                dsRetorno.Tables.Add("TB006_id");
                dsRetorno.EnforceConstraints = false;
                dsRetorno.Tables["TB006_id"].BeginLoadData();
                da.Fill(dsRetorno.Tables["TB006_id"]);
                dsRetorno.Tables["TB006_id"].EndLoadData();
                dsRetorno.Tables.Add("TB004_Logradouro");
                dsRetorno.EnforceConstraints = false;
                dsRetorno.Tables["TB004_Logradouro"].BeginLoadData();
                da.Fill(dsRetorno.Tables["TB004_Logradouro"]);
                dsRetorno.Tables["TB004_Logradouro"].EndLoadData();
                dsRetorno.Tables.Add("TB004_Bairro");
                dsRetorno.EnforceConstraints = false;
                dsRetorno.Tables["TB004_Bairro"].BeginLoadData();
                da.Fill(dsRetorno.Tables["TB004_Bairro"]);
                dsRetorno.Tables["TB004_Bairro"].EndLoadData();

                con.Close();

            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return dsRetorno;
        }
        public int DddMunicipio(long tb006Id)
        {
            var retorno = 0;          

            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("SELECT TB006_Codigo FROM dbo.TB006_Municipio WHERE TB006_id =");
                sSql.Append(tb006Id);

                var command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    retorno = Convert.ToInt32(reader["TB006_Codigo"]);
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
        public MunicipioController Municipio(long tb006Id)
        {
            var retorno = new MunicipioController();

            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("SELECT * FROM dbo.TB006_Municipio ");
                sSql.Append(" WHERE TB006_id = ");
                sSql.Append(tb006Id);
                var command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    retorno.TB006_id = Convert.ToInt64(reader["TB006_id"]);
                    retorno.TB006_Municipio = reader["TB006_Municipio"].ToString();
                    retorno.TB006_Codigo = reader["TB006_Codigo"].ToString();
                    retorno.TB006_IBGE = reader["TB006_IBGE"].ToString();
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
        public EstadoController Estado(long tb005Id)
        {
            var retorno = new EstadoController();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("SELECT * FROM dbo.TB005_Estado ");
                sSql.Append(" WHERE TB005_Id = ");
                sSql.Append(tb005Id);
                var command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    retorno.TB005_Id = Convert.ToInt64(reader["TB005_Id"]);
                    retorno.TB005_Sigla = reader["TB005_Sigla"].ToString();
                    retorno.TB005_Codigo = reader["TB005_Codigo"].ToString();
                    retorno.TB005_Estado = reader["TB005_Estado"].ToString();
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
        /// <summary>
        /// Descrição:  Lista cidades que possuem usuarios ativos
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       20/10/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<MunicipioController> MunicipiosAtivos()
        {
            var retorno = new List<MunicipioController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" SELECT dbo.TB006_Municipio.TB006_id, dbo.TB006_Municipio.TB006_Municipio, dbo.TB005_Estado.TB005_Sigla ");
                sSql.Append(" FROM dbo.TB013_Pessoa INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio ON dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id INNER JOIN ");
                sSql.Append(" dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" WHERE(dbo.TB013_Pessoa.TB013_Status < 2) ");
                sSql.Append(" GROUP BY dbo.TB006_Municipio.TB006_Municipio, dbo.TB005_Estado.TB005_Sigla, dbo.TB006_Municipio.TB006_id ");
                sSql.Append(" ORDER BY dbo.TB005_Estado.TB005_Sigla, dbo.TB006_Municipio.TB006_Municipio ");

                var command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var obj = new MunicipioController
                    {
                        TB006_id = Convert.ToInt64(reader["TB006_id"]),
                        TB006_Municipio = reader["TB005_Sigla"].ToString().ToUpper() + " - " +
                                          reader["TB006_Municipio"].ToString().ToUpper()
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
        public List<EstadoController> estadoAtivos()
        {
            var retorno = new List<EstadoController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT  ");
                sSql.Append(" dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" ,dbo.TB005_Estado.TB005_Estado ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB013_Pessoa ");
                sSql.Append(" INNER JOIN  ");
                sSql.Append(" dbo.TB006_Municipio ");
                sSql.Append(" ON ");
                sSql.Append(" dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id ");
                sSql.Append(" INNER JOIN  ");
                sSql.Append(" dbo.TB005_Estado ");
                sSql.Append(" ON ");
                sSql.Append(" dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" GROUP BY  ");
                sSql.Append(" dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" ,dbo.TB005_Estado.TB005_Estado ");
                sSql.Append(" ORDER BY  ");
                sSql.Append(" dbo.TB005_Estado.TB005_Estado ");

                var command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var obj = new EstadoController
                    {
                        TB005_Id        = Convert.ToInt64(reader["TB005_Id"]),
                        TB005_Estado    = reader["TB005_Estado"].ToString().ToUpper().TrimEnd()
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
        public List<MunicipioController>municipiosAtivosPorEstado(long TB005_Id)
        {
            var retorno = new List<MunicipioController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
   
                sSql.Append(" SELECT  ");
                sSql.Append(" dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" ,dbo.TB006_Municipio.TB006_id ");
                sSql.Append(" ,dbo.TB006_Municipio.TB006_Municipio ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB013_Pessoa  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio  ");
                sSql.Append(" ON ");
                sSql.Append(" dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB005_Estado  ");
                sSql.Append(" ON ");
                sSql.Append(" dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" GROUP BY ");
                sSql.Append(" dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" ,dbo.TB006_Municipio.TB006_id ");
                sSql.Append(" ,dbo.TB006_Municipio.TB006_Municipio ");
                sSql.Append(" HAVING ");
                sSql.Append(" dbo.TB005_Estado.TB005_Id =  ");
                sSql.Append(TB005_Id);
                sSql.Append(" ORDER BY  ");
                sSql.Append(" dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" ,dbo.TB006_Municipio.TB006_Municipio ");

                var command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var obj = new MunicipioController
                    {
                        TB006_id        =   Convert.ToInt64(reader["TB006_id"]),
                        TB006_Municipio =   reader["TB006_Municipio"].ToString().ToUpper()
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
    }
}

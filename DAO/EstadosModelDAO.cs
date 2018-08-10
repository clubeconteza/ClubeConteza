using Controller;
using DAO.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class EstadosModelDAO
    {
        public EstadosModelController Estados { get; set; }

        private UnidadeTrabalho unidadeTrabalho;

        public EstadosModelDAO(IUnidadeTrabalho unidadeTrabalho)
        {
            this.unidadeTrabalho = unidadeTrabalho as UnidadeTrabalho;
            Estados = new EstadosModelController();
        }

        public DataTable ListarEstadosPorPais(long idPais)
        {
            var sql = new StringBuilder();
            sql.Append(" SELECT E.TB005_Id    AS Id, ");
            sql.Append(" E.TB005_Sigla AS Estado ");
            sql.Append(" FROM TB005_Estado E ");
            sql.Append(" WHERE E.TB003_Id = @Pais ");
            sql.Append(" ORDER BY E.TB005_Estado ");

            unidadeTrabalho.Conexao.Open();

            try
            {
                var comando = new SqlCommand(sql.ToString(), (SqlConnection)unidadeTrabalho.Conexao);
                comando.CommandTimeout = 300;
                comando.Parameters.AddWithValue("@Pais", idPais);
                comando.CommandType = CommandType.Text;

                var ds = new DataSet();
                var dados = new SqlDataAdapter(comando);
                dados.Fill(ds);

                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                unidadeTrabalho.Conexao.Close();
            }
        }
    }
}

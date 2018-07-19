using Controller;
using DAO.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class PaisModelDAO
    {
        public PaisModelController Pais { get; set; }

        private UnidadeTrabalho unidadeTrabalho;

        public PaisModelDAO(IUnidadeTrabalho unidadeTrabalho)
        {
            this.unidadeTrabalho = unidadeTrabalho as UnidadeTrabalho;
            Pais = new PaisModelController();
        }

        public DataTable ListarPais()
        {
            var sql = new StringBuilder();
            sql.Append(" SELECT P.TB003_id   AS Id, ");
            sql.Append("        P.TB003_Pais AS Pais ");
            sql.Append("   FROM TB003_Pais P ");
            sql.Append("  ORDER BY P.TB003_Pais ");

            unidadeTrabalho.Conexao.Open();

            try
            {
                var comando = new SqlCommand(sql.ToString(), (SqlConnection)unidadeTrabalho.Conexao);
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

using Controller;
using DAO.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAO
{
    public class MunicipiosModelDAO
    {
        public MunicipiosModelController Municipios { get; set; }

        private UnidadeTrabalho unidadeTrabalho;

        public MunicipiosModelDAO(IUnidadeTrabalho unidadeTrabalho)
        {
            this.unidadeTrabalho = unidadeTrabalho as UnidadeTrabalho;
            Municipios = new MunicipiosModelController();
        }

        public DataTable ListarMunicipiosPorEstado(long idEstado)
        {
            var sql = new StringBuilder();
            sql.Append(" SELECT M.TB006_id        AS Id, ");
            sql.Append("        M.TB006_Municipio AS Municipio ");
            sql.Append("   FROM TB006_Municipio M ");
            sql.Append("  WHERE M.TB005_Id = @Estado ");
            sql.Append("  ORDER BY M.TB006_Municipio ");

            unidadeTrabalho.Conexao.Open();

            try
            {
                var comando = new SqlCommand(sql.ToString(), (SqlConnection)unidadeTrabalho.Conexao);
                comando.CommandTimeout = 300;
                comando.Parameters.AddWithValue("@Estado", idEstado);
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

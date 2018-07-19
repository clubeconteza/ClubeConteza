using Controller;
using Controller.Enums;
using DAO.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class UnidadesModelDAO
    {
        public UnidadesModelController Unidades { get; set; }

        private UnidadeTrabalho unidadeTrabalho;

        public UnidadesModelDAO(IUnidadeTrabalho unidadeTrabalho)
        {
            this.unidadeTrabalho = unidadeTrabalho as UnidadeTrabalho;
            Unidades = new UnidadesModelController();
        }

        public DataTable ListarCorporativo(CorporativoFiltro filtro, string pesquisa)
        {
            var sql = new StringBuilder();
            sql.Append(" SELECT U.TB012_idCorporativo               AS Contrato, ");
            sql.Append("        U.TB020_NomeFantasia                AS NomeFantasia, ");
            sql.Append("        U.TB020_Documento                   AS Cnpj, ");
            sql.Append("        CONVERT(VARCHAR(1), U.TB020_Status) AS Status ");
            sql.Append("   FROM TB020_Unidades  U ");
            sql.Append("   JOIN TB012_Contratos C ON C.TB012_id = U.TB012_idCorporativo ");
            sql.Append("  WHERE C.TB012_TipoContrato = @TipoContrato ");
            if (!string.IsNullOrEmpty(pesquisa))
            {
                switch (filtro)
                {
                    case CorporativoFiltro.Contrato:
                        sql.Append(" AND U.TB012_idCorporativo LIKE @Pesquisa + '%' ");
                        break;
                    case CorporativoFiltro.NomeFantasia:
                        sql.Append(" AND U.TB020_NomeFantasia LIKE @Pesquisa + '%' ");
                        break;
                    case CorporativoFiltro.Cnpj:
                        sql.Append(" AND U.TB020_Documento LIKE @Pesquisa + '%' ");
                        break;
                    case CorporativoFiltro.Status:
                        sql.Append(" AND U.TB020_Status = @Pesquisa ");
                        break;
                }
            }
            sql.Append("  ORDER BY U.TB020_NomeFantasia ");

            unidadeTrabalho.Conexao.Open();

            try
            {
                var comando = new SqlCommand(sql.ToString(), (SqlConnection)unidadeTrabalho.Conexao);
                comando.Parameters.AddWithValue("@TipoContrato", (int)ContratosTipoContrato.Corporativo);
                comando.Parameters.AddWithValue("@Pesquisa", pesquisa);
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

        public bool SalvarCorporativo()
        {
            var sql = new StringBuilder();
            sql.Append(" INSERT INTO TB020_Unidades ");
            sql.Append(" (TB020_NomeFantasia, TB020_Documento) ");
            sql.Append(" VALUES ");
            sql.Append(" (@NomeFantasia, @Documento) ");

            try
            {
                var comando = new SqlCommand(sql.ToString(), (SqlConnection)unidadeTrabalho.Conexao);
                comando.Parameters.AddWithValue("@NomeFantasia", Unidades.NomeFantasia);
                comando.Parameters.AddWithValue("@Documento", Unidades.Documento);
                comando.CommandType = CommandType.Text;
                comando.Transaction = (SqlTransaction)unidadeTrabalho.Transacao;

                comando.ExecuteNonQuery();
                unidadeTrabalho.Transacao.Rollback();

                return true;
            }
            catch (Exception)
            {
                unidadeTrabalho.Transacao.Rollback();
                return false;
            }
            finally
            {
                unidadeTrabalho.Conexao.Close();
            }
        }
    }
}

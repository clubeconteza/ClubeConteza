using System.Data.SqlClient;

namespace DAO.Infrastructure
{
    public class ConexaoFabrica
    {
        public IUnidadeTrabalho CriarConexao()
        {
            var conexao = new SqlConnection(ParametrosDAO.StringConexao);
            return new UnidadeTrabalho(conexao);
        }
    }
}

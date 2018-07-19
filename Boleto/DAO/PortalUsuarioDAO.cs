using Boleto.Controller;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Boleto.DAO
{
    public class PortalUsuarioDAO
    {
        public PessoaController ConsultaUsuario(string cpfCnpj)
        {
            try
            {
                var pessoa = new PessoaController();
                using (var con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString)))
                {
                    con.Open();

                    var sql = new StringBuilder();
                    sql.Append("SELECT TB013_Pessoa.TB013_id, ");
                    sql.Append("       TB013_Pessoa.TB013_CPFCNPJ, ");
                    sql.Append("       TB013_Pessoa.TB013_NomeCompleto ");
                    sql.Append("  FROM TB033_PortalUsuario ");
                    sql.Append("  JOIN TB013_Pessoa ON TB033_PortalUsuario.TB013_id = TB013_Pessoa.TB013_id ");
                    sql.Append(" WHERE TB013_Pessoa.TB013_CPFCNPJ = '" + cpfCnpj + "'");

                    using (var comando = new SqlCommand(sql.ToString(), con))
                    {
                        using (var leitor = comando.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                pessoa.TB013_id = Convert.ToInt64(leitor["TB013_id"]);
                                pessoa.TB013_CPFCNPJ = leitor["TB013_CPFCNPJ"] != null ? leitor["TB013_CPFCNPJ"].ToString().Trim() : string.Empty;
                                pessoa.TB013_NomeCompleto = leitor["TB013_NomeCompleto"] != null ? leitor["TB013_NomeCompleto"].ToString().Trim() : string.Empty;
                            }
                        }
                    }
                }
                return pessoa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
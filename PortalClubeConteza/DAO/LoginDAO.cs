using PortalClubeConteza.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace PortalClubeConteza.DAO
{
    public class LoginDAO
    {
        public Contrato AcessoUsuarioPlanoFamiliar(string chave)
        {
            var retorno = new Contrato();

            var cript = new CriptografiaDAO();

            var valida = cript.ValidarChave(chave);
            if (valida != "Erro")
            {
                try
                {
                    var sSQL = new StringBuilder();

                    sSQL.Append(" SELECT dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB012_Contratos.TB012_Status, dbo.TB013_Pessoa.TB013_ListaNegra, dbo.TB012_Contratos.TB012_TipoContrato, ");
                    sSQL.Append(" dbo.TB012_Contratos.TB012_id ");
                    sSQL.Append(" FROM dbo.TB013_Pessoa INNER JOIN ");
                    sSQL.Append(" dbo.TB012_Contratos ON dbo.TB013_Pessoa.TB012_id = dbo.TB012_Contratos.TB012_id ");
                    sSQL.Append(" WHERE dbo.TB013_Pessoa.TB013_CPFCNPJ = ");
                    sSQL.Append("'");
                    sSQL.Append(valida);
                    sSQL.Append("'");
                    sSQL.Append(" AND(dbo.TB012_Contratos.TB012_Status = 1) AND(dbo.TB013_Pessoa.TB013_ListaNegra = 0) AND(dbo.TB012_Contratos.TB012_TipoContrato = 1)");

                    var con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["EntidadesContext"].ConnectionString));
                    var command = new SqlCommand(sSQL.ToString(), con);

                    con.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        retorno.Id = Convert.ToInt64(reader["TB012_id"]);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return retorno;
        }
    }
}
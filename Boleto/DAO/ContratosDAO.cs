using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Boleto.DAO
{
    public class ContratosDAO
    {
        public List<string> ConsultaPlanoCorporativoUsuario(long id)
        {
            try
            {
                var cnpjPlanos = new List<string>();
                using (var con = new SqlConnection(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["BoletoConnection"].ConnectionString)))
                {
                    con.Open();

                    var sql = new StringBuilder();
                    sql.Append("SELECT Unidade.TB020_Documento AS CNPJ ");
                    sql.Append("  FROM TB012_Contratos  AS Corporativo ");
                    sql.Append("  JOIN TB012_Contratos  AS FamiliarCorporativo ON Corporativo.TB012_id = FamiliarCorporativo.TB012_Corporativo ");
                    sql.Append("  JOIN TB013_Pessoa     AS Pessoa              ON FamiliarCorporativo.TB013_id = Pessoa.TB013_id ");
                    sql.Append("  JOIN TB020_Unidades   AS Unidade             ON Corporativo.TB012_id = Unidade.TB012_id ");
                    sql.Append(" WHERE Corporativo.TB012_TipoContrato = 3 ");
                    sql.Append("   AND Unidade.TB020_Matriz = 1 ");
                    sql.Append("   AND Pessoa.TB013_id = " + id);

                    using (var comando = new SqlCommand(sql.ToString(), con))
                    {
                        using (var leitor = comando.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                cnpjPlanos.Add(Convert.ToString(leitor["CNPJ"]).Trim());
                            }
                        }
                    }
                }
                return cnpjPlanos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
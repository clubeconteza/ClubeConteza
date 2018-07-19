using Controller;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Portal.DAO
{
    public class PortalContratoDAO
    {
        CriptografiaDAO Cript = new CriptografiaDAO();
        ValidaChave Validar = new ValidaChave();

        /// <summary>
        /// Descrição:  Valida acesso do usuario ao plano familiar
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       06/07/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public ContratosController AcessoUsuarioPlanoFamiliar(string Chave)
        {
            ContratosController Retorno = new ContratosController();

            string Valida = Validar.ValidarChave(Chave);
            if (Valida != "Erro")
            {
                try
                {
                    StringBuilder sSQL = new StringBuilder();

                    sSQL.Append(" SELECT dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB012_Contratos.TB012_Status, dbo.TB013_Pessoa.TB013_ListaNegra, dbo.TB012_Contratos.TB012_TipoContrato, ");
                    sSQL.Append(" dbo.TB012_Contratos.TB012_id ");
                    sSQL.Append(" FROM dbo.TB013_Pessoa INNER JOIN ");
                    sSQL.Append(" dbo.TB012_Contratos ON dbo.TB013_Pessoa.TB012_id = dbo.TB012_Contratos.TB012_id ");
                    sSQL.Append(" WHERE dbo.TB013_Pessoa.TB013_CPFCNPJ = ");
                    sSQL.Append("'");
                    sSQL.Append(Valida);
                    sSQL.Append("'");
                    sSQL.Append(" AND(dbo.TB012_Contratos.TB012_Status = 1) AND(dbo.TB013_Pessoa.TB013_ListaNegra = 0) AND(dbo.TB012_Contratos.TB012_TipoContrato = 1)");

                    SqlConnection con = new SqlConnection(Cript.Decrypt(ConfigurationManager.ConnectionStrings["ClubeContezaConnection"].ConnectionString));
                    SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Retorno.TB012_Id= Convert.ToInt64(reader["TB012_id"]);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return Retorno;
        }
    }
}

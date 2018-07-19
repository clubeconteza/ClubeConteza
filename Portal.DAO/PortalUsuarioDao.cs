using Controller;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Net;

namespace Portal.DAO
{
    public class PortalUsuarioDao
    {
        CriptografiaDAO Cript = new CriptografiaDAO();

        /// <summary>
        /// Descrição:  Valida login do usuário no portal
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       06/07/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public UsuarioPortalController LoginUsuarioPortal(String CPF, string Senha)
        {
            UsuarioPortalController Usuario = new UsuarioPortalController();
            Usuario.Pessoa = new PessoaController();
            

            Usuario.TB033_ChaveTemporaria = "-1";
            try
            {
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB013_Pessoa.TB013_id, dbo.TB033_PortalUsuario.TB033_Senha, dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB033_PortalUsuario.TB033_CadastradoEm, dbo.TB033_PortalUsuario.TB033_CadastradoPor, ");
                sSQL.Append(" dbo.TB033_PortalUsuario.TB033_AlteradoEm, dbo.TB033_PortalUsuario.TB033_AlteradoPor, dbo.TB033_PortalUsuario.TB033_Status, dbo.TB033_PortalUsuario.TB033_UltimoAcesso,dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB013_Pessoa.TB012_Corporativo ");
                sSQL.Append(" FROM dbo.TB033_PortalUsuario INNER JOIN ");
                sSQL.Append(" dbo.TB013_Pessoa ON dbo.TB033_PortalUsuario.TB013_id = dbo.TB013_Pessoa.TB013_id ");
                sSQL.Append(" WHERE ");
                sSQL.Append(" dbo.TB033_PortalUsuario.TB033_Senha = ");
                sSQL.Append(" '");
                sSQL.Append(Cript.Encrypt(Senha));
                sSQL.Append("'");
                sSQL.Append(" AND ");
                sSQL.Append("dbo.TB013_Pessoa.TB013_CPFCNPJ = ");
                sSQL.Append("'");
                sSQL.Append(CPF.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim());
                sSQL.Append("'");

                SqlConnection con = new SqlConnection(Cript.Decrypt( ConfigurationManager.ConnectionStrings["ClubeContezaConnection"].ConnectionString));
                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Usuario.Pessoa.TB013_Complemento = reader["TB013_NomeCompleto"].ToString().Trim();
                    if (Convert.ToInt16(reader["TB033_Status"]) == 0)
                    {
                        Usuario.TB033_ChaveTemporaria = "0";
                    }
                    else
                    {
                        if (Convert.ToInt16(reader["TB033_Status"]) == 1)
                        {
                            //IPHostEntry Host    = Dns.GetHostEntry(Dns.GetHostName());
                            //IPAddress   Ip      = Host.AddressList[0];

                            Usuario.TB033_ChaveTemporaria = Cript.EncryptInterna(reader["TB013_CPFCNPJ"].ToString().Trim() + ";" + DateTime.Now.ToString("dd/MM/yyyy hh:mm"));
                            Usuario.Pessoa.TB012_Corporativo = Convert.ToInt64(reader["TB012_Corporativo"]);

                            if (!ChaveTemporariaUpdate(Convert.ToInt64(reader["TB013_id"]), Usuario.TB033_ChaveTemporaria))
                            {
                                Usuario.TB033_ChaveTemporaria = "-1";
                                Usuario.Pessoa.TB013_Complemento = "ERRO";
                            }
                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Usuario;
        }


        public bool ChaveTemporariaUpdate(Int64 TB013_id, string ChaveTemporaria)
        {
            //UnidadeController Unidade_R = new UnidadeController();
            try
            {
                SqlConnection con = new SqlConnection(Cript.Decrypt(ConfigurationManager.ConnectionStrings["ClubeContezaConnection"].ConnectionString));
                SqlCommand sqlcmd = default(SqlCommand);

                con.Open();
                sqlcmd = new SqlCommand();
                sqlcmd.Connection = con;
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append("update TB033_PortalUsuario set ");
                sSQL.Append("TB033_ChaveTemporaria = @TB033_ChaveTemporaria, ");
                sSQL.Append("TB033_UltimoAcesso = @TB033_UltimoAcesso ");
                sSQL.Append("where ");
                sSQL.Append("TB013_id = @TB013_id ");


                sqlcmd.CommandText = sSQL.ToString();
                sqlcmd.Parameters.Add("@TB013_id", System.Data.SqlDbType.BigInt);
                sqlcmd.Parameters.Add("@TB033_ChaveTemporaria", System.Data.SqlDbType.VarChar, 500);
                sqlcmd.Parameters.Add("@TB033_UltimoAcesso", System.Data.SqlDbType.DateTime);
                // sqlcmd.Parameters["@TB033_UltimoAcesso"].Value = DateTime.Now.ToString("MM/dd/yyyy HH:MM");


                sqlcmd.Parameters["@TB013_id"].Value = TB013_id;
      
                sqlcmd.Parameters["@TB033_ChaveTemporaria"].Value = ChaveTemporaria;

                sqlcmd.Parameters["@TB033_UltimoAcesso"].Value = DateTime.Now;


                sqlcmd.ExecuteNonQuery();


                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
            return true;
        }

    }
}

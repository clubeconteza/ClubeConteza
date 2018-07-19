using Controller;
using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class UsuarioAppdao
    {
        CriptografiaDAO Cript = new CriptografiaDAO();
        /// <summary>
        /// Descrição:  Retorna valida credenciais do usuário para login
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       07/10/2015
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public UsuarioAPPController LoginUsuarioAppdao(UsuarioAPPController filtro)
        {
            //Recuperar Parametros do Banco
         
            var registroUsuarioCorrente = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\ClubeConteza");
            if (registroUsuarioCorrente != null)
            {
                var senhaBanco = registroUsuarioCorrente.GetValue("Senha");
                ParametrosDAO.StringConexao = "Data Source=" + registroUsuarioCorrente.GetValue("Servidor") + ";Initial Catalog=" + registroUsuarioCorrente.GetValue("Banco") + ";User ID =" + registroUsuarioCorrente.GetValue("Usuario") + ";Password=" + Cript.Decrypt(senhaBanco.ToString()) + ";Persist Security Info=" + "True";
            }


           // var temp = "Data Source=srvdbclubeconteza.database.windows.net;Initial Catalog=DBClubeConteza;User ID =db_clubeconteza;Password=eBOX1T52;Persist Security Info=True";
            //temp = Cript.EncryptInterna(temp);

            var   objUsuario      = new UsuarioAPPController();
            var   objPerfil       = new AcessoController();
            var   objPais         = new PaisController();
            var   objEstado       = new EstadoController();
            var   oblMunicipio    = new MunicipioController();
            objUsuario.Perfil       = objPerfil;
            objUsuario.Pais         = objPais;
            objUsuario.Estado       = objEstado;
            objUsuario.Municipio    = oblMunicipio;

            try
            {
                var sSql = new StringBuilder();

                sSql.Append(" SELECT ");
                sSql.Append(" dbo.TB011_APPUsuarios.TB011_Id ");
                sSql.Append(" ,dbo.TB011_APPUsuarios.TB010_id ");
                sSql.Append(" ,dbo.TB011_APPUsuarios.TB011_CPF ");
                sSql.Append(" ,dbo.TB011_APPUsuarios.TB011_NomeExibicao ");
                sSql.Append(" ,dbo.TB011_APPUsuarios.TB011_Status ");
                sSql.Append(" ,dbo.TB006_Municipio.TB006_id ");
                sSql.Append(" ,dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" ,dbo.TB005_Estado.TB003_Id ");
                sSql.Append(" ,dbo.TB_VS.VS ");
                sSql.Append(" ,dbo.TB_VS.ftpServidor ");
                sSql.Append(" ,dbo.TB_VS.ftpUsuario ");
                sSql.Append(" ,dbo.TB_VS.ftpSenha ");
                sSql.Append(" ,dbo.TB037_NegociacaoEntidade.TB037_Id ");
                sSql.Append(" ,dbo.TB037_NegociacaoEntidade.TB037_TipoComissao ");
                sSql.Append(" ,dbo.TB037_NegociacaoEntidade.TB037_Aliquota ");
                sSql.Append(", dbo.TB037_NegociacaoEntidade.TB037_Valor ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB011_APPUsuarios ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio ON dbo.TB011_APPUsuarios.TB006_id = dbo.TB006_Municipio.TB006_id ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB037_NegociacaoEntidade ON dbo.TB011_APPUsuarios.TB037_Id = dbo.TB037_NegociacaoEntidade.TB037_Id ");
                sSql.Append(" CROSS JOIN ");
                sSql.Append(" dbo.TB_VS ");
                sSql.Append(" WHERE ");
                sSql.Append(" dbo.TB011_APPUsuarios.TB011_CPF =");
                sSql.Append("'" + filtro.TB011_CPF + "'");
                sSql.Append(" AND ");
                sSql.Append(" dbo.TB011_APPUsuarios.TB011_Senha =");
                sSql.Append("'" + Cript.EncryptInterna(filtro.TB011_Senha) + "'");

                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (registroUsuarioCorrente != null)
                        objUsuario.Banco = registroUsuarioCorrente.GetValue("Banco").ToString();
                    objUsuario.TB011_Id             = Convert.ToInt64(reader["TB011_Id"]);
                    objUsuario.TB011_NomeExibicao   = reader["TB011_NomeExibicao"].ToString().TrimEnd().TrimStart();
                    objUsuario.TB011_CPF            = reader["TB011_CPF"].ToString().TrimEnd().TrimStart();
                    objUsuario.TB011_StatusS        = Enum.GetName(typeof(UsuarioAPPController.TB011_StatusE), Convert.ToInt16(reader["TB011_Status"]));
                    objUsuario.Perfil.TB010_id      = Convert.ToInt64(reader["TB010_Id"]);
                    objUsuario.Pais.TB003_id        = Convert.ToInt64(reader["TB003_id"]);
                    objUsuario.Estado.TB005_Id      = Convert.ToInt64(reader["TB005_Id"]);
                    objUsuario.Municipio.TB006_id   = Convert.ToInt64(reader["TB006_id"]);
                    objUsuario.VS                   = reader["VS"].ToString().Trim();
                    objUsuario.TB011_ftpServidor    = reader["ftpServidor"].ToString();
                    objUsuario.TB011_ftpUsuario     = reader["ftpUsuario"].ToString();
                    objUsuario.TB011_ftpSenha       = reader["ftpSenha"].ToString();
                    objUsuario.TB037_Id             = Convert.ToInt64(reader["TB037_Id"]);
                    objUsuario.TB037_TipoComissao   = Convert.ToInt16(reader["TB037_TipoComissao"]);
                    objUsuario.TB037_Aliquota       = Convert.ToDouble(reader["TB037_Aliquota"]);
                    objUsuario.TB037_Valor          = Convert.ToDouble(reader["TB037_Valor"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return objUsuario;
        }
        public string  BancoDeDados(string servidor,string usuario,string senha,string banco)
        {
            var Retorno = "";
            
            try
            {    
                    var criar = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\ClubeConteza");
                if (criar != null)
                {
                    criar.SetValue("Servidor", servidor.TrimEnd());
                    criar.SetValue("Usuario", usuario.TrimEnd());
                    criar.SetValue("Senha", Cript.EncryptInterna(senha.TrimEnd()));
                    criar.SetValue("Banco", banco.TrimEnd());
                    criar.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }

            return Retorno;

        }
        public string Conecxao()
        {
            string retorno;
 
            try
            {
                retorno = ParametrosDAO.StringConexao;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }
        public string RetornoConexao()
        {
            var retorno="";
     

            var registroUsuarioCorrente = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\ClubeConteza");
            if (registroUsuarioCorrente != null)
            {
                var senhaBanco = registroUsuarioCorrente.GetValue("Senha");
                //ParametrosDAO.StringConexao = "Data Source=" + RegistroUsuarioCorrente.GetValue("Servidor") + ";Initial Catalog=" + RegistroUsuarioCorrente.GetValue("Banco") + ";User ID =" + RegistroUsuarioCorrente.GetValue("Usuario") + ";Password=" + Cript.Decrypt(SenhaBanco.ToString()) + ";Persist Security Info=" + "True";
                retorno = registroUsuarioCorrente.GetValue("Servidor") +"#" + registroUsuarioCorrente.GetValue("Banco") + "#" + registroUsuarioCorrente.GetValue("Usuario") + "#" + Cript.Decrypt(senhaBanco.ToString());
            }

            return retorno;
        }

        /// <summary>
        /// Descrição:  Pesquisar dados da Pessoa pelo TB013_id
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       30/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public string Vs()
        {
            var retorno = "0.0.0.0";

            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("SELECT ");
                sSql.Append(" * ");
                sSql.Append("FROM dbo.TB_VS ");
     
                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    retorno = reader["VS"].ToString().Trim();

                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;
        }

        /// <summary>
        /// Descrição:  Valida acesso especifico
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       01/03/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool VerificaPrivilario(long tb010Id, long tb008Id)
        {
            var retorno = false;
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" SELECT dbo.TB010_TB008.TB010_id, dbo.TB010_TB008.TB008_id, dbo.TB008_Privilegio.TB008_Privilegio ");
                sSql.Append(" FROM dbo.TB010_TB008 INNER JOIN ");
                sSql.Append(" dbo.TB008_Privilegio ON dbo.TB010_TB008.TB008_id = dbo.TB008_Privilegio.TB008_id ");
                sSql.Append(" WHERE dbo.TB010_TB008.TB010_id =  ");
                sSql.Append(tb010Id);
                sSql.Append(" AND dbo.TB010_TB008.TB008_id =  ");
                sSql.Append(tb008Id);

                var command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    retorno =true;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }

            return retorno;
        }

        public long VerificaPrivilarioAcaoPontual(long tb008Id, string tb011Cpf, string tb011Senha)
        {
            long retorno = 0;
            try
            {

                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" SELECT dbo.TB011_APPUsuarios.TB011_Id, dbo.TB010_TB008.TB008_id, dbo.TB011_APPUsuarios.TB011_CPF, dbo.TB011_APPUsuarios.TB011_Senha ");
                sSql.Append(" FROM dbo.TB010_TB008 INNER JOIN ");
                sSql.Append(" dbo.TB011_APPUsuarios ON dbo.TB010_TB008.TB010_id = dbo.TB011_APPUsuarios.TB010_id ");
                sSql.Append(" WHERE dbo.TB010_TB008.TB008_id = ");
                sSql.Append(tb008Id);
                sSql.Append(" AND dbo.TB011_APPUsuarios.TB011_CPF = ");
                sSql.Append("'");
                sSql.Append(tb011Cpf.Replace(".","").Replace(",", "").Replace("-", ""));
                sSql.Append("'");
                sSql.Append(" AND dbo.TB011_APPUsuarios.TB011_Senha = ");
                sSql.Append("'");
                sSql.Append(Cript.EncryptInterna(tb011Senha));
                sSql.Append("'");
                
                var command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno = Convert.ToInt64(reader["TB011_Id"]);
                }


                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }

            return retorno;
        }

        public bool AlterarMinhaSenha(long tb011Id, string tb011Senha)
        {
            try
            {
                var updateSql = " UPDATE TB011_APPUsuarios SET " +
                                   " TB011_Senha               ='" + Cript.EncryptInterna(tb011Senha.TrimEnd()) +
                                   "' where TB011_Id             =" + tb011Id;

                using (var myConnection = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    myConnection.Open();

                    var myCommand = new SqlCommand(updateSql, myConnection);

                    myCommand.ExecuteScalar();
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
                //return false;
            }

            return true;
        }
    }
}

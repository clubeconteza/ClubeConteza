using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class mensalidadePremiadaDAO
    {
        public List<mensalidadePremiadaController> listarpremios(DateTime ano)
        {
            List<mensalidadePremiadaController> Retorno = new List<mensalidadePremiadaController>();
            try
            {

                DateTime Inicio = new DateTime(ano.Year, 1, 1);
                DateTime Fim = new DateTime(ano.Year, 12, 31);
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();


                sSQL.Append(" SELECT  ");
                sSQL.Append(" dbo.TB042_SorteioMensalidadePremiada.TB042_id ");
                sSQL.Append(" ,dbo.TB042_SorteioMensalidadePremiada.TB042_DataSorteio ");
                sSQL.Append(" ,dbo.TB042_SorteioMensalidadePremiada.TB042_Descricao ");
                sSQL.Append(" ,dbo.TB042_SorteioMensalidadePremiada.TB042_AlteradoEm ");
                sSQL.Append(" ,dbo.TB011_APPUsuarios.TB011_NomeExibicao ");
                sSQL.Append(" ,dbo.TB042_SorteioMensalidadePremiada.TB042_PontosMinimo ");
                sSQL.Append(" ,dbo.TB042_SorteioMensalidadePremiada.TB042_PontosMaximo ");
                sSQL.Append(" ,dbo.TB042_SorteioMensalidadePremiada.TB042_Status ");
                sSQL.Append(" ,dbo.TB042_SorteioMensalidadePremiada.TB042_DataSorteio AS Inicio ");
                sSQL.Append(" ,dbo.TB042_SorteioMensalidadePremiada.TB042_DataSorteio AS Fim ");
                sSQL.Append(" FROM ");
                sSQL.Append(" dbo.TB042_SorteioMensalidadePremiada  ");
                sSQL.Append(" INNER JOIN ");
                sSQL.Append(" dbo.TB011_APPUsuarios  ");
                sSQL.Append(" ON ");
                sSQL.Append(" dbo.TB042_SorteioMensalidadePremiada.TB042_AlteradoPor = dbo.TB011_APPUsuarios.TB011_Id ");
                sSQL.Append(" WHERE ");
                sSQL.Append(" dbo.TB042_SorteioMensalidadePremiada.TB042_DataSorteio >= ");
                sSQL.Append("'");
                sSQL.Append(Inicio.ToString("MM/dd/yyyy"));
                sSQL.Append("'");
                sSQL.Append(" AND ");
                sSQL.Append(" dbo.TB042_SorteioMensalidadePremiada.TB042_DataSorteio <=  ");
                sSQL.Append("'");
                sSQL.Append(Fim.ToString("MM/dd/yyyy"));
                sSQL.Append("'");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();        

                while (reader.Read())
                {
                    mensalidadePremiadaController obj = new mensalidadePremiadaController();
                    obj.TB042_id                = Convert.ToInt64(reader["TB042_id"]);
                    obj.TB042_DataSorteio       = Convert.ToDateTime(reader["TB042_DataSorteio"]);
                    obj.TB042_Descricao         = reader["TB042_Descricao"].ToString().TrimEnd();
                    obj.TB042_AlteradoEm        = Convert.ToDateTime(reader["TB042_AlteradoEm"]);
                    obj.TB042_AlteradoPorNome   = reader["TB011_NomeExibicao"].ToString().TrimEnd();
                    obj.TB042_PontosMinimo      = Convert.ToInt64(reader["TB042_PontosMinimo"]);
                    obj.TB042_PontosMaximo      = Convert.ToInt64(reader["TB042_PontosMaximo"]);
                    obj.TB042_DataSorteio       = Convert.ToDateTime(reader["TB042_DataSorteio"]);        
                    obj.TB042_StatusS           = Enum.GetName(typeof(mensalidadePremiadaController.TB042_StatusE), Convert.ToInt16(reader["TB042_Status"]));
                    
                    Retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public mensalidadePremiadaController item(long TB042_id)
        {
            mensalidadePremiadaController retorno = new mensalidadePremiadaController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();


                sSQL.Append(" SELECT  ");
                sSQL.Append(" * ");
                sSQL.Append(" FROM ");
                sSQL.Append(" dbo.TB042_SorteioMensalidadePremiada  ");               
                sSQL.Append(" WHERE ");
                sSQL.Append(" TB042_id = ");
                sSQL.Append(TB042_id);
                

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB042_id                = Convert.ToInt64(reader["TB042_id"]);
                    retorno.TB012_id                = reader["TB012_id"] is DBNull ? 0 : Convert.ToInt64(reader["TB012_id"].ToString());
                    //Convert.ToInt64(reader["TB012_id"]);
                    retorno.TB042_DataSorteio       = Convert.ToDateTime(reader["TB042_DataSorteio"]);
                    retorno.TB042_VlrUni            = Convert.ToDouble(reader["TB042_VlrUni"]);
                    retorno.TB042_Quantidade        = Convert.ToInt16(reader["TB042_Quantidade"]);
                    retorno.TB042_VlrTotal          = Convert.ToDouble(reader["TB042_VlrTotal"]);
                    retorno.TB042_PontosMinimo      = Convert.ToInt64(reader["TB042_PontosMinimo"]);
                    retorno.TB042_PontosMaximo      = Convert.ToInt64(reader["TB042_PontosMaximo"]);
                    retorno.TB042_Descricao         = reader["TB042_Descricao"].ToString().TrimEnd();
                    retorno.TB042_StatusS           = reader["TB042_Status"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public mensalidadePremiadaController sorteio(long numeroDaSorte, long min, long max)
        {
            mensalidadePremiadaController retorno = new mensalidadePremiadaController();
            retorno.titular             = new PessoaController();
            retorno.titular.Estado      = new EstadoController();
            retorno.titular.Municipio   = new MunicipioController();
            retorno.contrato            = new ContratosController();

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                //sSQL.Append(" SELECT  ");
                //sSQL.Append(" dbo.TB012_Contratos.TB012_id ");
                //sSQL.Append(" , dbo.TB012_Contratos.TB012_Sorteado ");
                //sSQL.Append(" , dbo.TB012_Contratos.TB012_Status ");
                //sSQL.Append(" , dbo.TB012_Contratos.TB012_NumeroDaSorte ");
                //sSQL.Append(" , dbo.TB012_Contratos.TB012_TotalCupons ");
                //sSQL.Append(" , dbo.TB012_Contratos.TB012_TotalVoucher ");
                //sSQL.Append(" , dbo.TB012_Contratos.TB012_TotalVoucher + dbo.TB012_Contratos.TB012_TotalCupons AS Pontos ");
                //sSQL.Append(" , dbo.TB013_Pessoa.TB013_id ");
                //sSQL.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB013_Pessoa.TB013_CPFCNPJ ");
                //sSQL.Append(" , ISNULL(dbo.View_Contato_Tipo1.Expr2, 'SEM CELULAR') AS Celular ");
                //sSQL.Append(" , ISNULL(dbo.View_Contato_Tipo2.Expr2, 'SEM FIXO') AS Fixo ");
                //sSQL.Append(" , ISNULL(dbo.View_Contato_Tipo3.Expr2, 'SEM EMAIL') AS Email ");
                //sSQL.Append(" , dbo.TB006_Municipio.TB006_Municipio ");
                //sSQL.Append(" , dbo.TB005_Estado.TB005_Sigla ");
                //sSQL.Append(" , dbo.TB013_Pessoa.TB013_Logradouro ");
                //sSQL.Append(" , dbo.TB013_Pessoa.TB004_Cep ");
                //sSQL.Append(" , dbo.TB013_Pessoa.TB013_Numero ");
                //sSQL.Append(" , dbo.TB013_Pessoa.TB013_Bairro ");
                //sSQL.Append(" , dbo.TB013_Pessoa.TB013_Complemento ");
                //sSQL.Append(" FROM             ");
                //sSQL.Append(" dbo.TB012_Contratos  ");
                //sSQL.Append(" INNER JOIN ");
                //sSQL.Append("  dbo.TB013_Pessoa ");
                //sSQL.Append(" ON  ");
                //sSQL.Append(" dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id  ");
                //sSQL.Append(" INNER JOIN ");
                //sSQL.Append(" dbo.TB006_Municipio ");
                //sSQL.Append(" ON  ");
                //sSQL.Append(" dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id  ");
                //sSQL.Append(" INNER JOIN ");
                //sSQL.Append(" dbo.TB005_Estado  ");
                //sSQL.Append(" ON  ");
                //sSQL.Append(" dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id  ");
                //sSQL.Append(" LEFT OUTER JOIN ");
                //sSQL.Append(" dbo.View_Contato_Tipo3  ");
                //sSQL.Append(" ON  ");
                //sSQL.Append(" dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo3.TB013_id  ");
                //sSQL.Append(" LEFT OUTER JOIN ");
                //sSQL.Append(" dbo.View_Contato_Tipo2  ");
                //sSQL.Append("  ON  ");
                //sSQL.Append(" dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo2.TB013_id  ");
                //sSQL.Append(" LEFT OUTER JOIN ");
                //sSQL.Append(" dbo.View_Contato_Tipo1  ");
                //sSQL.Append(" ON  ");
                //sSQL.Append(" dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo1.TB013_id ");
                //sSQL.Append(" WHERE ");
                //sSQL.Append(" dbo.TB012_Contratos.TB012_Sorteado = 0 ");
                //sSQL.Append(" AND ");
                //sSQL.Append(" dbo.TB012_Contratos.TB012_Status = 1 ");
                //sSQL.Append(" AND ");
                //sSQL.Append(" dbo.TB012_Contratos.TB012_NumeroDaSorte = ");
                //sSQL.Append(numeroDaSorte);
                //sSQL.Append("  AND ");
                //sSQL.Append(" dbo.TB012_Contratos.TB012_TotalVoucher + dbo.TB012_Contratos.TB012_TotalCupons >= ");
                //sSQL.Append(min);
                //sSQL.Append(" OR ");
                //sSQL.Append(" dbo.TB012_Contratos.TB012_Sorteado = 0 ");
                //sSQL.Append(" AND ");
                //sSQL.Append(" dbo.TB012_Contratos.TB012_Status = 1 ");
                //sSQL.Append(" AND ");
                //sSQL.Append(" dbo.TB012_Contratos.TB012_NumeroDaSorte = ");
                //sSQL.Append(numeroDaSorte);
                //sSQL.Append(" AND ");
                //sSQL.Append(" dbo.TB012_Contratos.TB012_TotalVoucher + dbo.TB012_Contratos.TB012_TotalCupons <= ");
                //sSQL.Append(max);

                sSQL.Append(" SELECT  ");
                sSQL.Append(" dbo.TB012_Contratos.TB012_id ");
                sSQL.Append(" , dbo.TB012_Contratos.TB012_Sorteado ");
                sSQL.Append(" , dbo.TB012_Contratos.TB012_Status ");
                sSQL.Append(" , dbo.TB012_Contratos.TB012_NumeroDaSorte ");
                sSQL.Append(" , dbo.TB012_Contratos.TB012_TotalCupons ");
                sSQL.Append(" , dbo.TB012_Contratos.TB012_TotalVoucher ");
                sSQL.Append(" , dbo.TB012_Contratos.TB012_TotalVoucher + dbo.TB012_Contratos.TB012_TotalCupons AS Pontos ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_NomeCompleto ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_CPFCNPJ ");
                sSQL.Append(" , ISNULL(dbo.View_Contato_Tipo1.Expr2, 'SEM CELULAR') AS Celular ");
                sSQL.Append(" , ISNULL(dbo.View_Contato_Tipo2.Expr2, 'SEM FIXO') AS Fixo ");
                sSQL.Append(" , ISNULL(dbo.View_Contato_Tipo3.Expr2, 'SEM EMAIL')AS Email ");
                sSQL.Append(" , dbo.TB006_Municipio.TB006_Municipio ");
                sSQL.Append(" , dbo.TB005_Estado.TB005_Sigla ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_Logradouro ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB004_Cep ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_Numero ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_Bairro ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_Complemento ");
                sSQL.Append(" , dbo.TB012_Contratos.TB012_TotalVoucher + dbo.TB012_Contratos.TB012_TotalCupons AS Inicio ");
                sSQL.Append(" , dbo.TB012_Contratos.TB012_TotalVoucher + dbo.TB012_Contratos.TB012_TotalCupons AS Fim ");
                sSQL.Append(" FROM  ");
                sSQL.Append(" dbo.TB012_Contratos  ");
                sSQL.Append(" INNER JOIN ");
                sSQL.Append(" dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id  ");
                sSQL.Append(" INNER JOIN ");
                sSQL.Append(" dbo.TB006_Municipio ON dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id  ");
                sSQL.Append(" INNER JOIN ");
                sSQL.Append(" dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id  ");
                sSQL.Append(" LEFT OUTER JOIN ");
                sSQL.Append(" dbo.View_Contato_Tipo3 ON dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo3.TB013_id  ");
                sSQL.Append(" LEFT OUTER JOIN ");
                sSQL.Append(" dbo.View_Contato_Tipo2 ON dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo2.TB013_id  ");
                sSQL.Append(" LEFT OUTER JOIN ");
                sSQL.Append(" dbo.View_Contato_Tipo1 ON dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo1.TB013_id ");
                sSQL.Append(" WHERE ");
                sSQL.Append(" dbo.TB012_Contratos.TB012_Sorteado = 0 ");
                sSQL.Append(" AND ");

                sSQL.Append(" dbo.TB012_Contratos.TB012_Status = 1 ");
                sSQL.Append(" AND ");
                sSQL.Append(" dbo.TB012_Contratos.TB012_NumeroDaSorte =  ");
                sSQL.Append(numeroDaSorte);
                sSQL.Append("AND ");
                sSQL.Append(" dbo.TB012_Contratos.TB012_TotalVoucher + dbo.TB012_Contratos.TB012_TotalCupons >=  ");
                sSQL.Append(min);
                sSQL.Append(" AND ");
                sSQL.Append("  dbo.TB012_Contratos.TB012_TotalVoucher + dbo.TB012_Contratos.TB012_TotalCupons <=  ");
                sSQL.Append(max);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB012_id                            = Convert.ToInt64(reader["TB012_Id"]);
                    retorno.contrato.TB012_TotalVoucher         = Convert.ToInt64(reader["TB012_TotalVoucher"]);
                    retorno.contrato.TB012_TotalCupons          = Convert.ToInt64(reader["TB012_TotalCupons"]);
                    retorno.contrato.Pontos                     = retorno.contrato.TB012_TotalVoucher + retorno.contrato.TB012_TotalCupons;
                    retorno.titular.TB013_id                    = Convert.ToInt64(reader["TB013_id"]);
                    retorno.titular.TB013_NomeCompleto          = reader["TB013_NomeCompleto"].ToString().TrimEnd().ToUpper();
                    retorno.titular.TB013_CPFCNPJ               = reader["TB013_CPFCNPJ"].ToString().TrimEnd().ToUpper();
                    retorno.titular.Celular                     = reader["Celular"].ToString().TrimEnd().ToUpper();
                    retorno.titular.fixo                        = reader["Fixo"].ToString().TrimEnd().ToUpper();
                    retorno.titular.email                       = reader["Email"].ToString().TrimEnd().ToUpper();
                    retorno.titular.TB013_Logradouro            = reader["TB013_Logradouro"].ToString().TrimEnd().ToUpper();
                    retorno.titular.TB004_Cep                   = reader["TB004_Cep"].ToString().TrimEnd().ToUpper();
                    retorno.titular.TB013_Numero                = reader["TB013_Numero"].ToString().TrimEnd().ToUpper();
                    retorno.titular.TB013_Bairro                = reader["TB013_Bairro"].ToString().TrimEnd().ToUpper();
                    retorno.titular.TB013_Complemento           = reader["TB013_Complemento"].ToString().TrimEnd().ToUpper();
                    retorno.titular.Estado.TB005_Sigla          = reader["TB005_Sigla"].ToString().TrimEnd().ToUpper();
                    retorno.titular.Municipio.TB006_Municipio   = reader["TB006_Municipio"].ToString().TrimEnd().ToUpper();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public bool contemplar(mensalidadePremiadaController contemplado)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" UPDATE TB042_SorteioMensalidadePremiada SET ");
                sSql.Append(" TB042_AlteradoEm= ");
                sSql.Append("'");
                sSql.Append(contemplado.TB042_AlteradoEm.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ,TB042_AlteradoPor= ");              
                sSql.Append(contemplado.TB042_AlteradoPor);
                sSql.Append(" ,TB042_Status= ");
                sSql.Append(" 3");
                sSql.Append(" ,TB042_Concurso= ");
                sSql.Append("'");
                sSql.Append(contemplado.TB042_Concurso.Trim());
                sSql.Append("'");
                sSql.Append(" ,TB042_Bilhete1= ");
                sSql.Append("'");
                sSql.Append(contemplado.TB042_Bilhete1.Trim());
                sSql.Append("'");
                sSql.Append(" ,TB042_Bilhete2= ");
                sSql.Append("'");
                sSql.Append(contemplado.TB042_Bilhete2.Trim());
                sSql.Append("'");
                sSql.Append(" ,TB042_Bilhete3= ");
                sSql.Append("'");
                sSql.Append(contemplado.TB042_Bilhete3.Trim());
                sSql.Append("'");
                sSql.Append(" ,TB042_Bilhete4= ");
                sSql.Append("'");
                sSql.Append(contemplado.TB042_Bilhete4.Trim());
                sSql.Append("'");
                sSql.Append(" ,TB042_Bilhete5= ");
                sSql.Append("'");
                sSql.Append(contemplado.TB042_Bilhete5.Trim());
                sSql.Append("'");
                sSql.Append(" ,TB042_NumeroDaSorte= ");
                sSql.Append("'");
                sSql.Append(contemplado.TB042_NumeroDaSorte.Trim());
                sSql.Append("'");
                sSql.Append(" ,TB012_id= ");
                sSql.Append(contemplado.TB012_id);
                sSql.Append(" where ");
                sSql.Append(" TB042_id = ");
                sSql.Append(contemplado.TB042_id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.ExecuteScalar();
                    con.Close();
                }

                StringBuilder sSqlContenplado = new StringBuilder();

                sSqlContenplado.Append(" UPDATE TB012_Contratos SET TB012_Sorteado = 1");
              
                sSqlContenplado.Append(" where ");
                sSqlContenplado.Append(" TB012_id = ");
                sSqlContenplado.Append(contemplado.TB012_id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSqlContenplado.ToString(), con);
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;
        }

        public PessoaController recuperacontemplado(long contrato)
        {
            PessoaController retorno    = new PessoaController();
            retorno.Municipio           = new MunicipioController();
            retorno.Estado              = new EstadoController();

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT");
                sSQL.Append(" dbo.TB013_Pessoa.TB012_id");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto");
                sSQL.Append(" , ISNULL(dbo.View_Contato_Tipo1.Expr2, 'SEM CELULAR') AS Celular");
                sSQL.Append(" , ISNULL(dbo.View_Contato_Tipo3.Expr2, 'SEM FIXO') AS Fixo");
                sSQL.Append(" , ISNULL(dbo.View_Contato_Tipo2.Expr2, 'SEM EMAIL') AS Email");
                sSQL.Append(" , dbo.TB006_Municipio.TB006_Municipio");
                sSQL.Append(" , dbo.TB005_Estado.TB005_Sigla");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_Logradouro");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_Numero");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_Bairro");
                sSQL.Append(" , ISNULL(dbo.TB013_Pessoa.TB013_Complemento, '-') AS TB013_Complemento");
                sSQL.Append(" , dbo.TB013_Pessoa.TB004_Cep");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_CPFCNPJ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_id");
                sSQL.Append(" FROM");
                sSQL.Append(" dbo.TB013_Pessoa");
                sSQL.Append(" INNER JOIN");
                sSQL.Append(" dbo.TB012_Contratos");
                sSQL.Append(" ON");
                sSQL.Append(" dbo.TB013_Pessoa.TB013_id = dbo.TB012_Contratos.TB013_id");
                sSQL.Append(" AND");
                sSQL.Append(" dbo.TB013_Pessoa.TB012_id = dbo.TB012_Contratos.TB012_id");
                sSQL.Append(" INNER JOIN");
                sSQL.Append(" dbo.TB006_Municipio");
                sSQL.Append(" ON");
                sSQL.Append(" dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id");
                sSQL.Append(" INNER JOIN");
                sSQL.Append(" dbo.TB005_Estado");
                sSQL.Append(" ON");
                sSQL.Append(" dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id");
                sSQL.Append(" LEFT OUTER JOIN");
                sSQL.Append(" dbo.View_Contato_Tipo2");
                sSQL.Append(" ON");
                sSQL.Append(" dbo.TB012_Contratos.TB013_id = dbo.View_Contato_Tipo2.TB013_id");
                sSQL.Append(" LEFT OUTER JOIN");
                sSQL.Append(" dbo.View_Contato_Tipo3");
                sSQL.Append(" ON");
                sSQL.Append(" dbo.TB012_Contratos.TB013_id = dbo.View_Contato_Tipo3.TB013_id");
                sSQL.Append(" LEFT OUTER JOIN");
                sSQL.Append(" dbo.View_Contato_Tipo1");
                sSQL.Append(" ON");
                sSQL.Append(" dbo.TB012_Contratos.TB013_id = dbo.View_Contato_Tipo1.TB013_id");
                sSQL.Append(" WHERE");
                sSQL.Append(" dbo.TB013_Pessoa.TB012_id =");
                sSQL.Append(contrato);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB012_Id                    = Convert.ToInt64(reader["TB012_Id"]);
                    retorno.TB013_id                    = Convert.ToInt64(reader["TB013_id"]);
                    retorno.TB013_NomeCompleto          = reader["TB013_NomeCompleto"].ToString().TrimEnd().ToUpper();
                    retorno.TB013_CPFCNPJ               = reader["TB013_CPFCNPJ"].ToString().TrimEnd().ToUpper();
                    retorno.Celular                     = reader["Celular"].ToString().TrimEnd().ToUpper();
                    retorno.fixo                        = reader["Fixo"].ToString().TrimEnd().ToUpper();
                    retorno.email                       = reader["Email"].ToString().TrimEnd().ToUpper();
                    retorno.TB013_Logradouro            = reader["TB013_Logradouro"].ToString().TrimEnd().ToUpper();
                    retorno.TB004_Cep                   = reader["TB004_Cep"].ToString().TrimEnd().ToUpper();
                    retorno.TB013_Numero                = reader["TB013_Numero"].ToString().TrimEnd().ToUpper();
                    retorno.TB013_Bairro                = reader["TB013_Bairro"].ToString().TrimEnd().ToUpper();
                    retorno.TB013_Complemento           = reader["TB013_Complemento"].ToString().TrimEnd().ToUpper();
                    retorno.Estado.TB005_Sigla          = reader["TB005_Sigla"].ToString().TrimEnd().ToUpper();
                    retorno.Municipio.TB006_Municipio   = reader["TB006_Municipio"].ToString().TrimEnd().ToUpper();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }


        public bool atualizarconsumo(string consumo )
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(consumo, con);
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;
        }

        public bool prepararsorteio()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand("update TB012_Contratos set TB012_Sorteado = 0", con);
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;
        }

    }
}
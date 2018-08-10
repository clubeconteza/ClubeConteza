using Controller;
using System;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class ContratoFamiliarDao
    {
        public ContratosController ContratoFamiliarInserir(ContratosController contrato)
        {
            var retorno = contrato;

            var con = new SqlConnection(ParametrosDAO.StringConexao);

            var cmdTb012Insert = con.CreateCommand();
            var cmdTb013Insert = con.CreateCommand();
            var cmdTb012Update = con.CreateCommand();
            var cmdTb013Update = con.CreateCommand();

            var sSqltb012Insert = new StringBuilder();
            sSqltb012Insert.Append("INSERT INTO TB012_Contratos ");
            sSqltb012Insert.Append("(");
            sSqltb012Insert.Append("TB002_id ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_Inicio ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_Fim ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_AceiteContrato ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_DataAceiteContrato ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_CadastradoEm ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_CadastradorPor ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_AlteradoEm ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_AlteradoPor ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_Status ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB004_Cep ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB006_id ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_Logradouro ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_Numero ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_Bairro ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_Complemento ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_TipoContrato ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_ProximoCodDependente ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_CicloContrato ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_CodCartao ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_VSContrato ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_DiaVencimento ");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("TB012_NumeroDaSorte ");
            sSqltb012Insert.Append(") VALUES(");
            sSqltb012Insert.Append(contrato.PontoDeVenda.TB002_id);
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(contrato.TB012_Inicio.ToString("MM/dd/yyyy"));
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(contrato.TB012_Fim.ToString("MM/dd/yyyy"));
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append(contrato.TB012_AceiteContrato);
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(contrato.TB012_Inicio.ToString("MM/dd/yyyy hh:mm"));
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append(contrato.TB012_CadastradorPor);
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append(contrato.TB012_AlteradoPor);
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("0");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append(contrato.TB004_Cep.Replace("-",""));
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append(contrato.Municipio.TB006_id);
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(contrato.TB012_Logradouro.TrimStart().TrimEnd().Replace("'","").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(contrato.TB012_Numero.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(contrato.TB012_Bairro.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(contrato.TB012_Complemento.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append(1);
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append(1001);
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append(contrato.TB012_CicloContrato);
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append("0");
            sSqltb012Insert.Append("'");
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append(1);
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append(contrato.TB012_DiaVencimento);
            sSqltb012Insert.Append(", ");
            sSqltb012Insert.Append(contrato.TB012_NumeroDaSorte);
            sSqltb012Insert.Append(") SELECT SCOPE_IDENTITY()");

                StringBuilder sSqltb013Insert = new StringBuilder();
                sSqltb013Insert.Append(" INSERT INTO TB013_Pessoa ");
                sSqltb013Insert.Append("(");
                sSqltb013Insert.Append(" TB012_EraContezino ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_Tipo ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_CarteirinhaStatus ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_CodigoDependente ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_CPFCNPJ ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_ListaNegra ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_NomeCompleto ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_NomeExibicao ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_Sexo ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_RG ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_RGOrgaoEmissor ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_DataNascimento ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_DeclaroSerMaiorCapaz ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB004_Cep ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB006_id ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_Logradouro ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_Numero ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_Bairro ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_Complemento ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_CadastradoEm ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_CadastradoPor ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_AlteradoEm ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_AlteradoPor ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_Status ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_MaeNome ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_MaeDataNascimento ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_PaiNome ");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(" TB013_PaiDataNascimento ");
                sSqltb013Insert.Append(") VALUES(");
                sSqltb013Insert.Append(contrato.Titular.TB012_EraContezino);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(contrato.Titular.TB013_TipoS);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(0);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(contrato.Titular.TB013_CodigoDependente);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.Titular.TB013_CPFCNPJ.Replace(",","").Replace(".", "").Replace("-", "").Replace("/", "").Trim());
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(0);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.Titular.TB013_NomeCompleto.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.Titular.TB013_NomeCompleto.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(contrato.Titular.TB013_SexoS);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.Titular.TB013_RG.TrimEnd().TrimStart());
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.Titular.TB013_RGOrgaoEmissor.TrimEnd().TrimStart());
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.Titular.TB013_DataNascimento.ToString("MM/dd/yyyy"));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(contrato.Titular.TB013_DeclaroSerMaiorCapaz);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.TB004_Cep.Replace("-", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(contrato.Municipio.TB006_id);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.TB012_Logradouro.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.TB012_Numero.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.TB012_Bairro.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.TB012_Complemento.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");   
                sSqltb013Insert.Append(contrato.TB012_CadastradorPor);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(contrato.TB012_CadastradorPor);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(0);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.Titular.TB013_MaeNome.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.Titular.TB013_MaeDataNascimento.ToString("MM/dd/yyyy hh:mm"));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.Titular.TB013_PaiNome.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(contrato.Titular.TB013_PaiDataNascimento.ToString("MM/dd/yyyy hh:mm"));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(") SELECT SCOPE_IDENTITY()");

            string sTB012Update = "update TB012_Contratos set TB013_id = {0} where TB012_id = {1} ";
            string sTB013Update = "update TB013_Pessoa    set TB012_id = {0} where TB013_id = {1} ";

            cmdTb012Insert.CommandText = sSqltb012Insert.ToString();
            cmdTb013Insert.CommandText = sSqltb013Insert.ToString();
            

            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmdTb012Insert.Transaction = tran;
                retorno.TB012_Id = Convert.ToInt64(cmdTb012Insert.ExecuteScalar());
                //Comando 1 executado com sucesso!

                if (contrato.Titular.TB013_id < 1)
                {
                    cmdTb013Insert.Transaction = tran;
                    contrato.Titular.TB013_id= Convert.ToInt64(cmdTb013Insert.ExecuteScalar());
                    //Comando 2 executado com sucesso!

                }

                cmdTb012Update.CommandText = string.Format(sTB012Update, contrato.Titular.TB013_id.ToString(), retorno.TB012_Id.ToString());
                cmdTb012Update.Transaction = tran;
                cmdTb012Update.ExecuteNonQuery();
                cmdTb013Update.CommandText = string.Format(sTB013Update, retorno.TB012_Id.ToString(), contrato.Titular.TB013_id.ToString());
                cmdTb013Update.Transaction = tran;
                cmdTb013Update.ExecuteNonQuery();
                tran.Commit();
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return retorno;
        }
        /// <summary>
        /// Descrição:  Cancelar contrato do plano familiar
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       03/10/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool FamiliarCancelarContrato(long tb012Id, long tb011Id, string descricao, string tb012ContratoCancelarMotivoS)
        {
            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

            SqlCommand cmdTb012 = con.CreateCommand();
            SqlCommand cmdTb013 = con.CreateCommand();
            SqlCommand cmdTb016 = con.CreateCommand();
            SqlCommand cmdTb016_2 = con.CreateCommand();

            StringBuilder sSqltb012 = new StringBuilder();
            sSqltb012.Append("update [dbo].[TB012_Contratos] set [TB012_Status]= 5  ");
            sSqltb012.Append(",TB012_AlteradoEm =  ");
            sSqltb012.Append("'");
            sSqltb012.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            sSqltb012.Append("'");
            sSqltb012.Append(",TB012_ContratoCancelarData =  ");
            sSqltb012.Append("'");
            sSqltb012.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            sSqltb012.Append("'");
            sSqltb012.Append(" ,TB012_Fim =  ");
            sSqltb012.Append("'");
            sSqltb012.Append(DateTime.Now.ToString("MM/dd/yyyy"));
            sSqltb012.Append("'");
            sSqltb012.Append(",TB012_AlteradoPor =  ");
            sSqltb012.Append(tb011Id);
            sSqltb012.Append(",TB012_ContratoCancelarDescricao =  ");
            sSqltb012.Append("'");
            sSqltb012.Append(descricao);
            sSqltb012.Append("'");
            sSqltb012.Append(",TB012_ContratoCancelarMotivo =  ");
            sSqltb012.Append(tb012ContratoCancelarMotivoS);
            sSqltb012.Append(" where [TB012_id] =");
            sSqltb012.Append(tb012Id);
            StringBuilder sSqltb013 = new StringBuilder();
            sSqltb013.Append("update [dbo].[TB013_Pessoa] set [TB013_Status]= 2 ");
            sSqltb013.Append(",TB013_AlteradoEm =  ");
            sSqltb013.Append("'");
            sSqltb013.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            sSqltb013.Append("'");
            sSqltb013.Append(",TB013_AlteradoPor =  ");
            sSqltb013.Append(tb011Id);
            sSqltb013.Append(" where [TB012_id]=");
            sSqltb013.Append(tb012Id);
            StringBuilder sSqltb016 = new StringBuilder();
            sSqltb016.Append(" update [dbo].[TB016_Parcela] set [TB016_Status]= 3 ");
            sSqltb016.Append(" ,TB016_AlteradoEm =  ");
            sSqltb016.Append("'");
            sSqltb016.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            sSqltb016.Append("'");
            sSqltb016.Append(" ,TB016_AlteradoPor =  ");
            sSqltb016.Append(tb011Id);
            sSqltb016.Append(" where TB012_id= ");
            sSqltb016.Append(tb012Id);
            sSqltb016.Append(" and TB016_Status < 3 ");
            sSqltb016.Append(" and TB016_Vencimento >= ");
            sSqltb016.Append("'");
            sSqltb016.Append(DateTime.Now.ToString("MM/dd/yyyy"));
            sSqltb016.Append("'");
            StringBuilder sSqltb016_2 = new StringBuilder();
            sSqltb016_2.Append(" update [dbo].[TB016_Parcela] set [TB016_Status]= 4 ");
            sSqltb016_2.Append(" ,TB016_AlteradoEm =  ");
            sSqltb016_2.Append("'");
            sSqltb016_2.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            sSqltb016_2.Append("'");
            sSqltb016_2.Append(" ,TB016_AlteradoPor =  ");
            sSqltb016_2.Append(tb011Id);
            sSqltb016_2.Append(" where TB012_id= ");
            sSqltb016_2.Append(tb012Id);
            sSqltb016_2.Append(" and TB016_Status < 3 ");
            sSqltb016_2.Append(" and TB016_Vencimento <= ");
            sSqltb016_2.Append("'");
            sSqltb016_2.Append(DateTime.Now.ToString("MM/dd/yyyy"));
            sSqltb016_2.Append("'");
            cmdTb012.CommandText = sSqltb012.ToString();
            cmdTb013.CommandText = sSqltb013.ToString();
            cmdTb016.CommandText = sSqltb016.ToString();
            cmdTb016_2.CommandText = sSqltb016_2.ToString();


            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmdTb012.Transaction = tran;
                cmdTb012.ExecuteNonQuery();
                //Comando 1 executado com sucesso!

                cmdTb013.Transaction = tran;
                cmdTb013.ExecuteNonQuery();
                //Comando 2 executado com sucesso!


                cmdTb016.Transaction = tran;
                cmdTb016.ExecuteNonQuery();
                //Comando 3 executado com sucesso!

                cmdTb016_2.Transaction = tran;
                cmdTb016_2.ExecuteNonQuery();
                //Comando 4 executado com sucesso!

                tran.Commit();
            }
            catch (SqlException)
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                con.Close();
            }

            return true;
        }
        public ContratosController ContratoFamiliarDadosParcela(long tb012Id)
        {
            var retorno = new ContratosController {Titular = new PessoaController()};

            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT TB012_id, TB013_id, TB002_id,TB012_Inicio,TB012_DiaVencimento FROM dbo.TB012_Contratos");
                sSql.Append(" WHERE dbo.TB012_Contratos.TB012_id =  ");
                sSql.Append(tb012Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    retorno.Titular.TB013_id = Convert.ToInt64(reader["TB013_id"]);
                    retorno.TB002_id = Convert.ToInt64(reader["TB002_id"]);
                    retorno.TB012_Inicio = Convert.ToDateTime(reader["TB012_Inicio"]);
                    retorno.TB012_DiaVencimento = reader["TB012_DiaVencimento"] is DBNull ? 5 : Convert.ToInt16(reader["TB012_DiaVencimento"]);
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
        public bool finalizarProcessoEdicaoContrato(long tb012Id,long tb011Id)
        {
            {
                try
                {
                    StringBuilder sSql = new StringBuilder();
                    sSql.Append(" UPDATE TB012_Contratos SET ");
                    sSql.Append(" TB012_Edicao = 0");
                    sSql.Append(" ,TB012_AlteradoEm = ");
                    sSql.Append("'");
                    sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                    sSql.Append("'");
                    sSql.Append(",TB012_AlteradoPor=");
                    sSql.Append(tb011Id);
                    sSql.Append(" WHERE TB012_id =");
                    sSql.Append(tb012Id);

                    using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                    {
                        con.Open();
                        SqlCommand myCommand = new SqlCommand(sSql.ToString(), con);
                        myCommand.CommandTimeout = 300;
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
        /// <summary>
        /// Descrição:  Reativar contrato do plano familiar
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       15/01/2018
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool familiarReativarContrato(ContratosController contrato)
        {
            DateTime novoLimiteContrato = contrato.TB012_AlteradoEm.AddYears(1);

            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

            SqlCommand cmdTb013 = con.CreateCommand();
            SqlCommand cmdTb012 = con.CreateCommand();
            SqlCommand cmdTb016 = con.CreateCommand();
    

            StringBuilder sSqltb012 = new StringBuilder();
            sSqltb012.Append("update [dbo].[TB012_Contratos] set [TB012_Status]= 1  ");
            sSqltb012.Append(",TB012_CicloContrato =  ");
            sSqltb012.Append(contrato.TB012_CicloContrato);
            sSqltb012.Append(",TB012_AlteradoEm =  ");
            sSqltb012.Append("'");
            sSqltb012.Append(contrato.TB012_AlteradoEm.ToString("MM/dd/yyyy hh:mm"));
            sSqltb012.Append("'");
            sSqltb012.Append(",TB012_VSContrato =  ");
            sSqltb012.Append(contrato.TB012_VSContrato);
            sSqltb012.Append(" ,TB012_Fim =  ");
            sSqltb012.Append("'");
            sSqltb012.Append(novoLimiteContrato.ToString("MM/dd/yyyy"));
            sSqltb012.Append("'");
            sSqltb012.Append(",TB012_AlteradoPor =  ");
            sSqltb012.Append(contrato.TB012_AlteradoPor);        
            sSqltb012.Append(" where [TB012_id] =");
            sSqltb012.Append(contrato.TB012_Id);
            StringBuilder sSqltb013 = new StringBuilder();
            sSqltb013.Append("update [dbo].[TB013_Pessoa] set [TB013_Status]= 1 ");
            sSqltb013.Append(",TB013_AlteradoEm =  ");
            sSqltb013.Append("'");
            sSqltb013.Append(contrato.TB012_AlteradoEm.ToString("MM/dd/yyyy hh:mm"));
            sSqltb013.Append("'");
            sSqltb013.Append(",TB013_AlteradoPor =  ");
            sSqltb013.Append(contrato.TB012_AlteradoPor);
            sSqltb013.Append(" where [TB013_id]=");
            sSqltb013.Append(contrato.TB013_id);
            StringBuilder sSqltb016 = new StringBuilder();
            sSqltb016.Append(" update [dbo].[TB016_Parcela] set ");
            sSqltb016.Append(" TB016_AlteradoEm =  ");
            sSqltb016.Append("'");
            sSqltb016.Append(contrato.TB012_AlteradoEm.ToString("MM/dd/yyyy hh:mm"));
            sSqltb016.Append("'");
            sSqltb016.Append(" ,TB016_AlteradoPor =  ");
            sSqltb016.Append(contrato.TB012_AlteradoPor);
            sSqltb016.Append(" ,TB012_CicloContrato =  ");
            sSqltb016.Append(contrato.TB012_CicloContrato);
            sSqltb016.Append(" where TB012_id= ");
            sSqltb016.Append(contrato.TB012_Id);
            sSqltb016.Append(" and TB016_Status < 3 ");
            sSqltb016.Append(" and TB016_Vencimento >= ");
            sSqltb016.Append("'");
            sSqltb016.Append(contrato.TB012_AlteradoEm.ToString("MM/dd/yyyy"));
            sSqltb016.Append("'");
            cmdTb012.CommandText = sSqltb012.ToString();
            cmdTb013.CommandText = sSqltb013.ToString();
            cmdTb016.CommandText = sSqltb016.ToString();
      
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmdTb012.Transaction = tran;
                cmdTb012.ExecuteNonQuery();
                //Comando 1 executado com sucesso!

                cmdTb013.Transaction = tran;
                cmdTb013.ExecuteNonQuery();
                //Comando 2 executado com sucesso!

                cmdTb016.Transaction = tran;
                cmdTb016.ExecuteNonQuery();
                //Comando 3 executado com sucesso!          

                tran.Commit();
            }
            catch (SqlException)
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                con.Close();
            }

            return true;
        }
    }
}

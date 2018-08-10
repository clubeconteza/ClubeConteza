using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class PessoaDao
    {
        /// <summary>
        /// Descrição:  Incluir nova Pessoa
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public PessoaController PessoaInsert(PessoaController pessoa)
        {
            PessoaController retorno = new PessoaController();
            try
            {
                string insertSql = "INSERT INTO TB013_Pessoa(TB013_NomeExibicaoDetalhes,TB013_IdProtheus,TB013_Tipo,TB013_CPFCNPJ,TB013_ListaNegra,TB013_NomeCompleto,TB013_NomeExibicao,TB013_Sexo,TB013_RG,TB013_RGOrgaoEmissor,TB013_DataNascimento,TB013_DeclaroSerMaiorCapaz,TB004_Cep,TB006_id,TB013_Logradouro,TB013_Numero,TB013_Bairro,TB013_Complemento,TB013_CadastradoEm,TB013_CadastradoPor,TB013_AlteradoEm,TB013_AlteradoPor,TB013_CodigoDependente,TB013_Status,TB013_CarteirinhaStatus,TB012_EraContezino,TB013_MaeNome,TB013_MaeDataNascimento,TB013_PaiNome,TB013_PaiDataNascimento) VALUES (@TB013_NomeExibicaoDetalhes,@TB013_IdProtheus,@TB013_Tipo,@TB013_CPFCNPJ,@TB013_ListaNegra,@TB013_NomeCompleto,@TB013_NomeExibicao,@TB013_Sexo,@TB013_RG,@TB013_RGOrgaoEmissor,@TB013_DataNascimento,@TB013_DeclaroSerMaiorCapaz,@TB004_Cep,@TB006_id,@TB013_Logradouro,@TB013_Numero,@TB013_Bairro,@TB013_Complemento,@TB013_CadastradoEm,@TB013_CadastradoPor,@TB013_AlteradoEm,@TB013_AlteradoPor,@TB013_CodigoDependente,@TB013_Status,@TB013_CarteirinhaStatus,@TB012_EraContezino    ,@TB013_MaeNome,@TB013_MaeDataNascimento,@TB013_PaiNome,@TB013_PaiDataNascimento) SELECT SCOPE_IDENTITY()";

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand(insertSql, con);
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@TB013_IdProtheus", pessoa.TB013_IdProtheus);
                    command.Parameters.AddWithValue("@TB013_Tipo", Convert.ToInt16(pessoa.TB013_TipoS));


                    string vCpfcnjp = pessoa.TB013_CPFCNPJ.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "").Trim();
                    if (pessoa.TB013_TipoS == "1")
                    {
                        vCpfcnjp = vCpfcnjp.PadLeft(11, '0');
                    }

                    if (vCpfcnjp.Trim() == "00000000000")
                    {
                        vCpfcnjp = "";
                    }
                    if (vCpfcnjp.Trim() == string.Empty)
                    {


                        command.Parameters.AddWithValue("@TB013_CPFCNPJ", "SEM CPF");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@TB013_CPFCNPJ", vCpfcnjp);
                    }


                    if (pessoa.TB013_RG == null)
                    {
                        pessoa.TB013_RG = "SEM RG";
                    }

                    if (pessoa.TB013_RG.Trim() == string.Empty)
                    {
                        command.Parameters.AddWithValue("@TB013_RG", "SEM RG");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@TB013_RG", pessoa.TB013_RG);
                    }


                    if (pessoa.TB013_RGOrgaoEmissor == null)
                    {
                        pessoa.TB013_RGOrgaoEmissor = "SEM RG";
                    }

                    if (pessoa.TB013_RGOrgaoEmissor.ToUpper().Trim() == string.Empty)
                    {
                        command.Parameters.AddWithValue("@TB013_RGOrgaoEmissor", "SEM RG");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@TB013_RGOrgaoEmissor", pessoa.TB013_RGOrgaoEmissor.ToUpper().Trim());
                    }

                    command.Parameters.AddWithValue("@TB012_EraContezino", pessoa.TB012_EraContezino);
                    command.Parameters.AddWithValue("@TB013_ListaNegra", pessoa.TB013_ListaNegra);
                    command.Parameters.AddWithValue("@TB013_NomeCompleto", pessoa.TB013_NomeCompleto.ToUpper().Trim());


                    if(pessoa.TB013_NomeExibicaoDetalhes.ToUpper().Trim()== string.Empty)
                    {
                        command.Parameters.AddWithValue("@TB013_NomeExibicaoDetalhes", pessoa.TB013_NomeCompleto.ToUpper().Trim());
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@TB013_NomeExibicaoDetalhes", pessoa.TB013_NomeExibicaoDetalhes.ToUpper().Trim());
                    }

                    command.Parameters.AddWithValue("@TB013_Status", pessoa.TB013_StatusS);
                    command.Parameters.AddWithValue("@TB013_NomeExibicao", pessoa.TB013_NomeExibicao.ToUpper().Trim());
                    command.Parameters.AddWithValue("@TB013_Sexo", Convert.ToInt16(pessoa.TB013_SexoS));
                    command.Parameters.AddWithValue("@TB013_DataNascimento", pessoa.TB013_DataNascimento);
                    command.Parameters.AddWithValue("@TB013_DeclaroSerMaiorCapaz", pessoa.TB013_DeclaroSerMaiorCapaz);
                    command.Parameters.AddWithValue("@TB004_Cep", pessoa.TB004_Cep.Replace("-", ""));
                    command.Parameters.AddWithValue("@TB006_id", pessoa.Municipio.TB006_id);
                    command.Parameters.AddWithValue("@TB013_Logradouro", pessoa.TB013_Logradouro.ToUpper().Trim());
                    command.Parameters.AddWithValue("@TB013_Numero", pessoa.TB013_Numero.ToUpper().Trim());
                    command.Parameters.AddWithValue("@TB013_Bairro", pessoa.TB013_Bairro.ToUpper().Trim());
                    command.Parameters.AddWithValue("@TB013_Complemento", pessoa.TB013_Complemento.ToUpper().Trim());
                    command.Parameters.AddWithValue("@TB013_CadastradoEm", pessoa.TB013_CadastradoEm);
                    command.Parameters.AddWithValue("@TB013_CadastradoPor", pessoa.TB013_CadastradoPor);
                    command.Parameters.AddWithValue("@TB013_AlteradoEm", pessoa.TB013_AlteradoEm);
                    command.Parameters.AddWithValue("@TB013_AlteradoPor", pessoa.TB013_AlteradoPor);
                    command.Parameters.AddWithValue("@TB013_CodigoDependente", pessoa.TB013_CodigoDependente);
                    command.Parameters.AddWithValue("@TB013_CarteirinhaStatus", 0);

                    command.Parameters.AddWithValue("@TB013_MaeNome", pessoa.TB013_MaeNome);
                    command.Parameters.AddWithValue("@TB013_MaeDataNascimento", pessoa.TB013_MaeDataNascimento.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@TB013_PaiNome", pessoa.TB013_PaiNome);
                    command.Parameters.AddWithValue("@TB013_PaiDataNascimento", pessoa.TB013_PaiDataNascimento.ToString("MM/dd/yyyy"));

                    retorno.TB013_id = Convert.ToInt32(command.ExecuteScalar());

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;

        }
        /// <summary>
        /// Descrição:  Incluir nova Pessoa
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public long DependenteFamiliarInsert(PessoaController pessoa)
        {
            long retorno;
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                int tb012Edicao = 0;
                /*Verificar se ja existiam parcelas*/
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" SELECT COUNT(TB016_id) AS Parcelas FROM dbo.TB016_Parcela WHERE TB016_Status < 3 GROUP BY TB012_id HAVING TB012_id = ");
                sSql.Append(pessoa.TB012_Id);
                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (Convert.ToInt16(reader["Parcelas"]) > 0)
                    {
                        tb012Edicao = 1;
                    }

                }

                con.Close();

                SqlCommand cmdTb013Insert = con.CreateCommand();
                SqlCommand cmdTb016Update = con.CreateCommand();
                SqlCommand cmdTb012Update = con.CreateCommand();
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
                sSqltb013Insert.Append("TB013_MaeNome");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("TB013_MaeDataNascimento");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("TB013_PaiNome");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("TB013_PaiDataNascimento");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("TB012_Corporativo");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("TB012_Parceiro");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("TB013_CartaoSolicitado");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("TB012_id");
                sSqltb013Insert.Append(") VALUES(");
                sSqltb013Insert.Append(pessoa.TB012_EraContezino);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(pessoa.TB013_TipoS);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(0);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(pessoa.TB013_CodigoDependente);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB013_CPFCNPJ.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "").Trim().Replace(" ","").Replace("_", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(0);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB013_NomeCompleto.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB013_NomeExibicao.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(pessoa.TB013_SexoS);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB013_RG.TrimEnd().TrimStart());
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB013_RGOrgaoEmissor.TrimEnd().TrimStart());
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB013_DataNascimento.ToString("MM/dd/yyyy"));
                sSqltb013Insert.Append("'");               
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB004_Cep.Replace("-", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(pessoa.Municipio.TB006_id);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB013_Logradouro.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB013_Numero.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB013_Bairro.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB013_Complemento.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(pessoa.TB013_CadastradoPor);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(pessoa.TB013_AlteradoPor);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(0);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB013_MaeNome.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB013_MaeDataNascimento.ToString("MM/dd/yyyy"));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB013_PaiNome.TrimStart().TrimEnd().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("@", "").Replace("§", ""));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(pessoa.TB013_PaiDataNascimento.ToString("MM/dd/yyyy"));
                sSqltb013Insert.Append("'");
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(pessoa.TB012_Corporativo);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(pessoa.TB012_Parceiro);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(pessoa.TB013_CartaoSolicitado);
                sSqltb013Insert.Append(", ");
                sSqltb013Insert.Append(pessoa.TB012_Id);
                sSqltb013Insert.Append(") SELECT SCOPE_IDENTITY()");

                StringBuilder sSqltb016Update = new StringBuilder();
                sSqltb016Update.Append("update [dbo].[TB016_Parcela] set TB016_Status = 3 WHERE TB016_Status < 3 and TB012_id = ");
                sSqltb016Update.Append(pessoa.TB012_Id);
                sSqltb016Update.Append(" AND  TB016_Vencimento >= ");
                sSqltb016Update.Append("'");
                sSqltb016Update.Append(DateTime.Now.ToString("MM/dd/yyyy"));
                sSqltb016Update.Append("'");

                StringBuilder sSqltb011Update = new StringBuilder();
                sSqltb011Update.Append("update [dbo].[TB012_Contratos] set TB012_ProximoCodDependente = TB012_ProximoCodDependente + 1 ");
                sSqltb011Update.Append(" ,TB012_Edicao  = ");
                sSqltb011Update.Append(tb012Edicao);
                sSqltb011Update.Append(" where TB012_id = ");
                sSqltb011Update.Append(pessoa.TB012_Id);

                cmdTb013Insert.CommandText = sSqltb013Insert.ToString();
                cmdTb016Update.CommandText = sSqltb016Update.ToString();
                cmdTb012Update.CommandText = sSqltb011Update.ToString();

                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    cmdTb013Insert.Transaction = tran;
                    retorno = Convert.ToInt64(cmdTb013Insert.ExecuteScalar());
                    //Comando 1 executado com sucesso!

                    cmdTb016Update.Transaction = tran;
                    cmdTb016Update.ExecuteNonQuery();
                    //Comando 2 executado com sucesso!

                    cmdTb012Update.Transaction = tran;
                    cmdTb012Update.ExecuteNonQuery();
                    //Comando 3 executado com sucesso!


                    tran.Commit();
                    //Retorno = true;
                }
                catch (SqlException)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;

        }
        public PessoaController DependenteFamiliarSelect(long tb013Id)
        {
            var retorno = new PessoaController();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();


                sSql.Append(" SELECT dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_Tipo, dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_RG, dbo.TB013_Pessoa.TB013_RGOrgaoEmissor, dbo.TB013_Pessoa.TB013_Cartao,  ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_CarteirinhaStatus, dbo.TB013_Pessoa.TB013_CartaoChip, dbo.TB013_Pessoa.TB013_CartaoChipStatus, dbo.TB013_Pessoa.TB013_CartaoSolicitado, dbo.TB013_Pessoa.TB013_DataNascimento,  ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_Sexo, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB013_Pessoa.TB013_NomeExibicao, dbo.TB006_Municipio.TB006_id, dbo.TB005_Estado.TB005_Id, dbo.TB005_Estado.TB003_Id,  ");
                sSql.Append(" dbo.TB013_Pessoa.TB004_Cep, dbo.TB013_Pessoa.TB013_Logradouro, dbo.TB013_Pessoa.TB013_Numero, dbo.TB013_Pessoa.TB013_Bairro, dbo.TB013_Pessoa.TB013_Complemento,  ");
                sSql.Append(" dbo.TB013_Pessoa.TB012_Corporativo, dbo.TB013_Pessoa.TB012_Parceiro, dbo.TB013_Pessoa.TB013_MaeNome, dbo.TB013_Pessoa.TB013_MaeDataNascimento, dbo.TB013_Pessoa.TB013_PaiNome,  ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_PaiDataNascimento, dbo.TB013_Pessoa.TB013_Status ");
                sSql.Append(" FROM dbo.TB013_Pessoa INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio ON dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id INNER JOIN ");
                sSql.Append(" dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" WHERE dbo.TB013_Pessoa.TB013_id = ");
                sSql.Append(tb013Id);
   

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB013_id = Convert.ToInt64(reader["TB013_id"]);
                    retorno.TB013_TipoS = Convert.ToString(reader["TB013_Tipo"]);

                    if (!reader.IsDBNull(reader.GetOrdinal("TB013_CPFCNPJ")))
                    {
                        if (retorno.TB013_TipoS == "1")
                        {
                            if (reader["TB013_CPFCNPJ"].ToString().Trim() == "SEM CPF")
                            {
                                retorno.TB013_CPFCNPJ = reader["TB013_CPFCNPJ"].ToString();
                            }
                            else
                            {

                                if (reader["TB013_CPFCNPJ"].ToString().Trim() == string.Empty)
                                {
                                    retorno.TB013_CPFCNPJ = "SEM CPF";
                                }
                                else
                                {
                                    retorno.TB013_CPFCNPJ = reader["TB013_CPFCNPJ"] is DBNull ? "SEM CPF" : Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"000\.000\.000\-00");
                                }                              
                            }
                        }
                        else
                        {
                            retorno.TB013_CPFCNPJ = Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"00\.000\.000\/0000\-00");
                        }
                    }

                    retorno.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    retorno.TB013_NomeExibicao = reader["TB013_NomeExibicao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    retorno.TB013_MaeNome = reader["TB013_MaeNome"] is DBNull ? "NÃO INFORMADO" : reader["TB013_MaeNome"].ToString();
                    retorno.TB013_MaeDataNascimento = reader["TB013_MaeDataNascimento"] is DBNull ? DateTime.Now.AddYears(-18) : Convert.ToDateTime(reader["TB013_MaeDataNascimento"].ToString());
                    retorno.TB013_PaiNome = reader["TB013_PaiNome"] is DBNull ? "NÃO INFORMADO" : reader["TB013_PaiNome"].ToString();
                    retorno.TB013_PaiDataNascimento = reader["TB013_PaiDataNascimento"] is DBNull ? DateTime.Now.AddYears(-18) : Convert.ToDateTime(reader["TB013_PaiDataNascimento"].ToString());
                    retorno.TB013_Cartao = reader["TB013_Cartao"].ToString().TrimEnd().TrimStart().ToUpper();
                    retorno.TB013_CarteirinhaStatusS = reader["TB013_CarteirinhaStatus"] is DBNull ? Enum.GetName(typeof(PessoaController.TB013_CarteirinhaStatusE), 0) : Enum.GetName(typeof(PessoaController.TB013_CarteirinhaStatusE), Convert.ToInt16(reader["TB013_CarteirinhaStatus"]));
                    retorno.TB013_StatusS = reader["TB013_Status"] is DBNull ? "0" : reader["TB013_Status"].ToString();
                    retorno.TB013_SexoS = Convert.ToString(reader["TB013_Sexo"]);
                    retorno.TB013_RG = Convert.ToString(reader["TB013_RG"]).ToUpper().Trim();
                    retorno.TB013_RGOrgaoEmissor = Convert.ToString(reader["TB013_RGOrgaoEmissor"]).ToUpper().Trim();
                    retorno.TB013_IdProtheus = reader["TB013_id"].ToString();
                    retorno.TB013_DataNascimento = Convert.ToDateTime(reader["TB013_DataNascimento"]);
                    retorno.TB004_Cep = Convert.ToString(reader["TB004_Cep"]);
                    retorno.TB013_Logradouro = Convert.ToString(reader["TB013_Logradouro"]).ToUpper().Trim();
                    retorno.TB013_Numero = Convert.ToString(reader["TB013_Numero"]).ToUpper().Trim();
                    retorno.TB013_Bairro = Convert.ToString(reader["TB013_Bairro"]).ToUpper().Trim();
                    retorno.TB013_Complemento = reader["TB013_Complemento"] is DBNull ? "-" : Convert.ToString(reader["TB013_Complemento"]).ToUpper().Trim();

                    MunicipioController objMunicipio = new MunicipioController();
                    objMunicipio.TB006_id = Convert.ToInt64(reader["TB006_id"]);

                    EstadoController objEstado = new EstadoController();
                    objEstado.TB005_Id = Convert.ToInt64(reader["TB005_Id"]);

                    PaisController objPais = new PaisController();
                    objPais.TB003_id = Convert.ToInt64(reader["TB003_id"]);

                    objEstado.Pais = objPais;
                    objMunicipio.Estado = objEstado;

                    retorno.Municipio = objMunicipio;
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
        /// Descrição:  Atualizar cadastro Pessoa
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool PessoaFamiliarUpdate(PessoaController pessoa)
        {

            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" UPDATE TB013_Pessoa SET ");
                sSql.Append(" TB013_IdProtheus= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_id.ToString());
                sSql.Append("'");
                sSql.Append(" ,TB013_Tipo= ");
                sSql.Append(Convert.ToInt16(pessoa.TB013_TipoS));
                sSql.Append(" ,TB013_CPFCNPJ=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_CPFCNPJ.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "").Replace("_", "").Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_NomeCompleto= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_NomeCompleto.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_NomeExibicao= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_NomeExibicao.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(",TB013_Sexo=");
                sSql.Append(Convert.ToInt16(pessoa.TB013_SexoS));
                sSql.Append(",TB013_RG= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_RG);
                sSql.Append("'");
                sSql.Append(" ,TB013_RGOrgaoEmissor=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_RGOrgaoEmissor.ToUpper().Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_DataNascimento= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_DataNascimento.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ,TB004_Cep=");
                sSql.Append(pessoa.TB004_Cep.Replace("-", ""));
                sSql.Append(" ,TB006_id= ");
                sSql.Append(pessoa.Municipio.TB006_id);
                sSql.Append(" ,TB013_Logradouro= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Logradouro.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(",TB013_Numero= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Numero.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_Bairro= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Bairro.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_Complemento=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Complemento.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_MaeNome=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_MaeNome.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_PaiNome=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_PaiNome.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_MaeDataNascimento=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_MaeDataNascimento.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ,TB013_PaiDataNascimento=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_PaiDataNascimento.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ,TB013_CartaoSolicitado= ");
                sSql.Append(pessoa.TB013_CartaoSolicitado);
                sSql.Append(" ,TB012_Corporativo= ");
                sSql.Append(pessoa.TB012_Corporativo);
                sSql.Append(" ,TB012_Parceiro= ");
                sSql.Append(pessoa.TB012_Parceiro);
                sSql.Append(" ,TB013_AlteradoEm=");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(" ,TB013_AlteradoPor= ");
                sSql.Append(pessoa.TB013_AlteradoPor);
                sSql.Append(" where TB013_id = ");
                sSql.Append(pessoa.TB013_id);

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
        /// <summary>
        /// Descrição:  Atualizar cadastro Pessoa
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool PessoaUpdate(PessoaController pessoa)
        {
            try
            {
                string verificaCpfCnpj = pessoa.TB013_CPFCNPJ.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "").Trim();

                if (pessoa.TB013_TipoS == "1")
                {
                    verificaCpfCnpj = verificaCpfCnpj.PadLeft(11, '0');
                }

                if (verificaCpfCnpj.Trim() == "00000000000")
                {
                    verificaCpfCnpj = "";
                }

                if (verificaCpfCnpj.Trim() == string.Empty)
                {
                    verificaCpfCnpj = "SEM CPF";
                }

                StringBuilder sSql = new StringBuilder();
                sSql.Append(" UPDATE TB013_Pessoa SET ");
                sSql.Append(" TB013_IdProtheus= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_IdProtheus.TrimEnd());
                sSql.Append("'");
                sSql.Append(" ,TB013_Tipo= ");
                sSql.Append(Convert.ToInt16(pessoa.TB013_TipoS));
                sSql.Append(" ,TB013_CPFCNPJ=");
                sSql.Append("'");
                sSql.Append(verificaCpfCnpj);
                sSql.Append("'");
                sSql.Append(" ,TB013_NomeCompleto= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_NomeCompleto.ToUpper().Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_NomeExibicao= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_NomeExibicao.ToUpper().Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_NomeExibicaoDetalhes= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_NomeExibicaoDetalhes.ToUpper().TrimEnd());
                sSql.Append("'");
                sSql.Append(",TB013_Sexo=");
                sSql.Append(Convert.ToInt16(pessoa.TB013_SexoS));
                sSql.Append(",TB013_RG= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_RG);
                sSql.Append("'");
                sSql.Append(" ,TB013_RGOrgaoEmissor= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_RGOrgaoEmissor.ToUpper().Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_DataNascimento= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_DataNascimento.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ,TB004_Cep=");
                sSql.Append(pessoa.TB004_Cep.Replace("-", ""));
                sSql.Append(" ,TB006_id= ");
                sSql.Append(pessoa.Municipio.TB006_id);
                sSql.Append(" ,TB013_Logradouro= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Logradouro.ToUpper().Trim());
                sSql.Append("'");
                sSql.Append(",TB013_Numero= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Numero.ToUpper().Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_Bairro= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Bairro.ToUpper().Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_Complemento=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Complemento.ToUpper().Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_AlteradoEm=");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(" ,TB013_AlteradoPor= ");
                sSql.Append(pessoa.TB013_AlteradoPor);
                sSql.Append(" where TB013_id = ");
                sSql.Append(pessoa.TB013_id);

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
        public bool PessoaUpdateTitularContratoFamiliar(PessoaController titular)
        {

            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" UPDATE TB013_Pessoa SET ");
                sSql.Append(" TB013_NomeCompleto= ");
                sSql.Append("'");
                sSql.Append(titular.TB013_NomeCompleto.ToUpper().TrimEnd().TrimStart().Replace("'","").Replace("#", "").Replace("!", "").Replace("$", "").Replace("%", "").Replace("¨", "").Replace("&", "").Replace("*", ""));
                sSql.Append("'");
                sSql.Append(" ,TB013_NomeExibicao= ");
                sSql.Append("'");
                sSql.Append(titular.TB013_NomeCompleto.ToUpper().TrimEnd().TrimStart().Replace("'", "").Replace("#", "").Replace("!", "").Replace("$", "").Replace("%", "").Replace("¨", "").Replace("&", "").Replace("*", ""));
                sSql.Append("'");
                sSql.Append(",TB013_Sexo=");
                sSql.Append(Convert.ToInt16(titular.TB013_SexoS));
                sSql.Append(",TB013_RG= ");
                sSql.Append("'");
                sSql.Append(titular.TB013_RG.ToUpper().TrimEnd().TrimStart().Replace("'", "").Replace("#", "").Replace("!", "").Replace("$", "").Replace("%", "").Replace("¨", "").Replace("&", "").Replace("*", ""));
                sSql.Append("'");
                sSql.Append(" ,TB013_RGOrgaoEmissor= ");
                sSql.Append("'");
                sSql.Append(titular.TB013_RGOrgaoEmissor.ToUpper().TrimEnd().TrimStart().Replace("'", "").Replace("#", "").Replace("!", "").Replace("$", "").Replace("%", "").Replace("¨", "").Replace("&", "").Replace("*", ""));
                sSql.Append("'");
                sSql.Append(" ,TB013_DataNascimento= ");
                sSql.Append("'");
                sSql.Append(titular.TB013_DataNascimento.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ,TB004_Cep=");
                sSql.Append(titular.TB004_Cep.Replace("-", ""));
                sSql.Append(" ,TB006_id= ");
                sSql.Append(titular.Municipio.TB006_id);
                sSql.Append(" ,TB013_Logradouro= ");
                sSql.Append("'");
                sSql.Append(titular.TB013_Logradouro.TrimEnd().TrimStart().Replace("'", "").Replace("#", "").Replace("!", "").Replace("$", "").Replace("%", "").Replace("¨", "").Replace("&", "").Replace("*", ""));
                sSql.Append("'");
                sSql.Append(",TB013_Numero= ");
                sSql.Append("'");
                sSql.Append(titular.TB013_Numero.TrimEnd().TrimStart().Replace("'", "").Replace("#", "").Replace("!", "").Replace("$", "").Replace("%", "").Replace("¨", "").Replace("&", "").Replace("*", ""));
                sSql.Append("'");
                sSql.Append(" ,TB013_Bairro= ");
                sSql.Append("'");
                sSql.Append(titular.TB013_Bairro.TrimEnd().TrimStart().Replace("'", "").Replace("#", "").Replace("!", "").Replace("$", "").Replace("%", "").Replace("¨", "").Replace("&", "").Replace("*", ""));
                sSql.Append("'");
                sSql.Append(" ,TB013_Complemento=");
                sSql.Append("'");
                sSql.Append(titular.TB013_Complemento.TrimEnd().TrimStart().Replace("'", "").Replace("#", "").Replace("!", "").Replace("$", "").Replace("%", "").Replace("¨", "").Replace("&", "").Replace("*", ""));
                sSql.Append("'");
                sSql.Append(" ,TB013_MaeNome=");
                sSql.Append("'");
                sSql.Append(titular.TB013_MaeNome.ToUpper().TrimEnd().TrimStart().Replace("'", "").Replace("#", "").Replace("!", "").Replace("$", "").Replace("%", "").Replace("¨", "").Replace("&", "").Replace("*", ""));
                sSql.Append("'");
                sSql.Append(" ,TB013_PaiNome=");
                sSql.Append("'");
                sSql.Append(titular.TB013_PaiNome.ToUpper().TrimEnd().TrimStart().Replace("'", "").Replace("#", "").Replace("!", "").Replace("$", "").Replace("%", "").Replace("¨", "").Replace("&", "").Replace("*", ""));
                sSql.Append("'");
                sSql.Append(" ,TB013_MaeDataNascimento=");
                sSql.Append("'");
                sSql.Append(titular.TB013_MaeDataNascimento.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ,TB013_PaiDataNascimento=");
                sSql.Append("'");
                sSql.Append(titular.TB013_PaiDataNascimento.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ,TB013_CartaoSolicitado= ");
                sSql.Append(titular.TB013_CartaoSolicitado);
                sSql.Append(" ,TB013_AlteradoPor= ");
                sSql.Append(titular.TB013_AlteradoPor);
                sSql.Append(" where TB013_id = ");
                sSql.Append(titular.TB013_id);

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
        /// <summary>
        /// Descrição:  Atualizar cadastro Dependente
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       29/12/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool DependenteUpdate(PessoaController pessoa)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" UPDATE TB013_Pessoa SET ");
                sSql.Append(" TB013_IdProtheus= '");
                sSql.Append(pessoa.TB013_IdProtheus);
                sSql.Append("' ,TB013_Tipo= ");
                sSql.Append(Convert.ToInt16(pessoa.TB013_TipoS));
                sSql.Append(" ,TB013_CPFCNPJ=");
                string vCpfcnjp = pessoa.TB013_CPFCNPJ.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "").Trim();
                sSql.Append("'");
                if (vCpfcnjp.Trim() == string.Empty)
                {
                    sSql.Append("SEM CPF");
                }
                else
                {
                    sSql.Append(vCpfcnjp);
                }
                sSql.Append("'");
                sSql.Append(" ,TB013_NomeCompleto= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_NomeCompleto.ToUpper().Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_NomeExibicao= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_NomeExibicao.ToUpper().Trim());
                sSql.Append("'");
                sSql.Append(",TB013_Sexo=");
                sSql.Append(Convert.ToInt16(pessoa.TB013_SexoS));

                if (pessoa.TB013_RG != null)
                {
                    sSql.Append(",TB013_RG= ");
                    sSql.Append("'");
                    sSql.Append(pessoa.TB013_RG);
                    sSql.Append("'");
                }


                if (pessoa.TB013_RGOrgaoEmissor != null)
                {
                    sSql.Append(" ,TB013_RGOrgaoEmissor= ");
                    sSql.Append("'");
                    sSql.Append(pessoa.TB013_RGOrgaoEmissor.ToUpper().Trim());
                    sSql.Append("'");
                }
                sSql.Append(" ,TB013_DataNascimento= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_DataNascimento.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ,TB004_Cep=");
                sSql.Append(pessoa.TB004_Cep.Replace("-", ""));
                sSql.Append(" ,TB006_id= ");
                sSql.Append(pessoa.Municipio.TB006_id);
                sSql.Append(" ,TB013_Logradouro= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Logradouro.ToUpper().Trim());
                sSql.Append("'");
                sSql.Append(",TB013_Numero= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Numero.ToUpper().Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_Bairro= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Bairro.ToUpper().Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_Complemento=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Complemento.ToUpper().Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_AlteradoEm=");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                sSql.Append("'");
                sSql.Append(" ,TB013_AlteradoPor= ");
                sSql.Append(pessoa.TB013_AlteradoPor);
                sSql.Append(" ,TB013_Status= ");
                sSql.Append(Convert.ToInt16(pessoa.TB013_StatusS));
                sSql.Append(" where TB013_id = ");
                sSql.Append(pessoa.TB013_id);

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
        /// <summary>
        /// Descrição:  Incluir Novo Contrato
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<PessoaController> PessoaSelect(string query)
        {
            var retornoList = new List<PessoaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();


                sSql.Append(" SELECT dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_Cartao, dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_Tipo, dbo.TB013_Pessoa.TB013_NomeCompleto, ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_CadastradoEm, dbo.TB013_Pessoa.TB013_Status, dbo.TB012_Contratos.TB012_id, dbo.TB012_Contratos.TB012_Inicio, dbo.TB012_Contratos.TB012_CadastradoEm, ");
                sSql.Append(" dbo.TB012_Contratos.TB012_AlteradoEm, AlteradoPor.TB011_NomeCompleto AS AlteradoPor, dbo.TB002_PontosDeVenda.TB002_id, dbo.TB002_PontosDeVenda.TB002_Ponto, dbo.TB011_TB002.TB011_Id, ");
                sSql.Append(" dbo.TB012_Contratos.TB012_Status, dbo.TB013_Pessoa.TB013_CodigoDependente ");
                sSql.Append(" FROM dbo.TB013_Pessoa INNER JOIN ");
                sSql.Append(" dbo.TB012_Contratos ON dbo.TB013_Pessoa.TB012_id = dbo.TB012_Contratos.TB012_id INNER JOIN ");
                sSql.Append(" dbo.TB011_APPUsuarios AS AlteradoPor ON dbo.TB012_Contratos.TB012_AlteradoPor = AlteradoPor.TB011_Id INNER JOIN ");
                sSql.Append(" dbo.TB002_PontosDeVenda ON dbo.TB012_Contratos.TB002_id = dbo.TB002_PontosDeVenda.TB002_id INNER JOIN ");
                sSql.Append(" dbo.TB011_TB002 ON dbo.TB002_PontosDeVenda.TB002_id = dbo.TB011_TB002.TB002_id ");
                sSql.Append(" WHERE ");
                sSql.Append(query);
                sSql.Append(" ORDER BY dbo.TB012_Contratos.TB012_id, dbo.TB013_Pessoa.TB013_CodigoDependente ");

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PessoaController
                    {
                        TB013_id = Convert.ToInt64(reader["TB013_id"]),
                        TB013_TipoS = Convert.ToString(reader["TB013_Tipo"])
                    };
                    if (reader["TB013_CPFCNPJ"].ToString().Trim() == string.Empty)
                    {
                        obj.TB013_CPFCNPJ = "SEM CPF";
                    }

                    else
                    {
                        if (obj.TB013_TipoS == "1")
                        {
                            if (reader["TB013_CPFCNPJ"].ToString().Trim() == "SEM CPF")
                            {
                                obj.TB013_CPFCNPJ = reader["TB013_CPFCNPJ"].ToString();
                            }
                            else
                            {
                                obj.TB013_CPFCNPJ = reader["TB013_CPFCNPJ"] is DBNull ? "SEM CPF" : Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"000\.000\.000\-00");
                            }
                        }
                        else
                        {
                            obj.TB013_CPFCNPJ = Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"00\.000\.000\/0000\-00");
                        }

                    }
                   

                    obj.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd().TrimStart().ToUpper();
                    obj.TB013_Cartao = reader["TB013_Cartao"].ToString().TrimEnd().TrimStart().ToUpper();
                    obj.TB013_AlteradoEm = Convert.ToDateTime(reader["TB012_AlteradoEm"].ToString());
                    obj.TB013_AlteradoPorNome = reader["AlteradoPor"].ToString().TrimEnd().TrimStart().ToUpper();
                    obj.TB002_Ponto = reader["TB002_Ponto"].ToString().TrimEnd().TrimStart().ToUpper();
                    obj.TB013_StatusS = reader["TB013_Status"] is DBNull ? "0" : Enum.GetName(typeof(PessoaController.TB013_StatusE), Convert.ToInt16(reader["TB013_Status"]));
                    obj.TB013_CodigoDependente = Convert.ToInt64(reader["TB013_CodigoDependente"]);
                    obj.TB012_Id = Convert.ToInt64(reader["TB012_Id"].ToString());

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        /// <summary>
        /// Descrição:  Pesquisar dados da Pessoa pelo TB013_id
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       30/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public PessoaController PessoaSelectId(long tb013Id)
        {
            var retorno = new PessoaController();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("SELECT ");
                sSql.Append("TB013_MaeNome,TB013_MaeDataNascimento,TB013_PaiNome,TB013_PaiDataNascimento, ");
                sSql.Append("dbo.TB013_Pessoa.TB012_EraContezino,    dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_IdProtheus, dbo.TB013_Pessoa.TB013_Tipo, dbo.TB013_Pessoa.TB013_Cartao, dbo.TB013_Pessoa.TB013_CarteirinhaStatus,  ");
                sSql.Append("dbo.TB013_Pessoa.TB013_CodigoDependente,          dbo.TB013_Pessoa.TB013_NomeExibicaoDetalhes             ,dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_ListaNegra, dbo.TB013_Pessoa.TB013_NomeCompleto, ");
                sSql.Append("dbo.TB013_Pessoa.TB013_NomeExibicao, dbo.TB013_Pessoa.TB013_Sexo, dbo.TB013_Pessoa.TB013_RG, dbo.TB013_Pessoa.TB013_RGOrgaoEmissor, dbo.TB013_Pessoa.TB013_DataNascimento, ");
                sSql.Append("dbo.TB013_Pessoa.TB013_DeclaroSerMaiorCapaz, dbo.TB013_Pessoa.TB004_Cep, dbo.TB013_Pessoa.TB013_Logradouro, dbo.TB013_Pessoa.TB013_Numero, dbo.TB013_Pessoa.TB013_Bairro, ");
                sSql.Append("dbo.TB013_Pessoa.TB013_Complemento, dbo.TB013_Pessoa.TB013_CadastradoEm, dbo.TB013_Pessoa.TB013_AlteradoEm, dbo.TB013_Pessoa.TB013_Status, dbo.TB006_Municipio.TB006_id, ");
                sSql.Append("dbo.TB005_Estado.TB005_Id, dbo.TB003_Pais.TB003_id, CadastradoPor.TB011_NomeExibicao AS TB013_CadastradoPor, AlteradoPor.TB011_NomeExibicao AS TB013_AlteradoPor, ");
                sSql.Append("dbo.TB005_Estado.TB005_Sigla, dbo.TB006_Municipio.TB006_Municipio, dbo.TB005_Estado.TB005_Estado ");
                sSql.Append("FROM dbo.TB005_Estado INNER JOIN ");
                sSql.Append("dbo.TB006_Municipio ON dbo.TB005_Estado.TB005_Id = dbo.TB006_Municipio.TB005_Id INNER JOIN ");
                sSql.Append("dbo.TB003_Pais ON dbo.TB005_Estado.TB003_Id = dbo.TB003_Pais.TB003_id INNER JOIN ");
                sSql.Append("dbo.TB013_Pessoa ON dbo.TB006_Municipio.TB006_id = dbo.TB013_Pessoa.TB006_id INNER JOIN ");
                sSql.Append("dbo.TB011_APPUsuarios AS CadastradoPor ON dbo.TB013_Pessoa.TB013_CadastradoPor = CadastradoPor.TB011_Id INNER JOIN ");
                sSql.Append("dbo.TB011_APPUsuarios AS AlteradoPor ON dbo.TB013_Pessoa.TB013_AlteradoPor = AlteradoPor.TB011_Id ");
                sSql.Append("WHERE dbo.TB013_Pessoa.TB013_id = ");
                sSql.Append(tb013Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB013_id = Convert.ToInt64(reader["TB013_id"]);
                    retorno.TB013_TipoS = Convert.ToString(reader["TB013_Tipo"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("TB013_CPFCNPJ")))
                    {
                        if (retorno.TB013_TipoS == "1")
                        {
                            if (reader["TB013_CPFCNPJ"].ToString().Trim() == "SEM CPF" || string.IsNullOrEmpty(reader["TB013_CPFCNPJ"].ToString().Trim()))
                            {
                                retorno.TB013_CPFCNPJ = reader["TB013_CPFCNPJ"].ToString();
                            }
                            else
                            {
                                retorno.TB013_CPFCNPJ = reader["TB013_CPFCNPJ"] is DBNull ? "SEM CPF" : Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"000\.000\.000\-00");
                            }
                        }
                        else
                        {
                            retorno.TB013_CPFCNPJ = Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"00\.000\.000\/0000\-00");
                        }
                    }



                    retorno.TB012_EraContezino = reader["TB012_EraContezino"] is DBNull ? 0 : Convert.ToInt16(reader["TB012_EraContezino"].ToString());
                    retorno.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    retorno.TB013_NomeExibicao = reader["TB013_NomeExibicao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    retorno.TB013_NomeExibicaoDetalhes = reader["TB013_NomeExibicaoDetalhes"] is DBNull ? reader["TB013_NomeCompleto"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim() : reader["TB013_NomeExibicaoDetalhes"].ToString();
                    retorno.TB013_MaeNome = reader["TB013_MaeNome"] is DBNull ? "NÃO INFORMADO" : reader["TB013_MaeNome"].ToString();
                    retorno.TB013_MaeDataNascimento = reader["TB013_MaeDataNascimento"] is DBNull ? DateTime.Now.AddYears(-18) : Convert.ToDateTime( reader["TB013_MaeDataNascimento"].ToString());
                    retorno.TB013_PaiNome = reader["TB013_PaiNome"] is DBNull ? "NÃO INFORMADO" : reader["TB013_PaiNome"].ToString();
                    retorno.TB013_PaiDataNascimento = reader["TB013_PaiDataNascimento"] is DBNull ? DateTime.Now.AddYears(-18) : Convert.ToDateTime(reader["TB013_PaiDataNascimento"].ToString());
                    retorno.TB013_Cartao = reader["TB013_Cartao"].ToString().TrimEnd().TrimStart().ToUpper();
                    retorno.TB013_CarteirinhaStatusS = reader["TB013_CarteirinhaStatus"] is DBNull ? Enum.GetName(typeof(PessoaController.TB013_CarteirinhaStatusE), 0) : Enum.GetName(typeof(PessoaController.TB013_CarteirinhaStatusE), Convert.ToInt16(reader["TB013_CarteirinhaStatus"]));
                    retorno.TB013_CadastradoEm = Convert.ToDateTime(reader["TB013_CadastradoEm"].ToString());
                    retorno.TB013_CadastradoPorNome = reader["TB013_CadastradoPor"].ToString().TrimEnd().TrimStart().ToUpper();
                    retorno.TB013_AlteradoEm = Convert.ToDateTime(reader["TB013_AlteradoEm"].ToString());
                    retorno.TB013_AlteradoPorNome = reader["TB013_AlteradoPor"].ToString().TrimEnd().TrimStart().ToUpper();
                    retorno.TB013_StatusS = reader["TB013_Status"] is DBNull ? "0" : reader["TB013_Status"].ToString();
                    retorno.TB013_SexoS = Convert.ToString(reader["TB013_Sexo"]);
                    retorno.TB013_RG = Convert.ToString(reader["TB013_RG"]).ToUpper().Trim();
                    retorno.TB013_RGOrgaoEmissor = Convert.ToString(reader["TB013_RGOrgaoEmissor"]).ToUpper().Trim();
                    retorno.TB013_IdProtheus = reader["TB013_IdProtheus"].ToString();
                    retorno.TB013_DataNascimento = Convert.ToDateTime(reader["TB013_DataNascimento"]);
                    retorno.TB013_DeclaroSerMaiorCapaz = Convert.ToInt16(reader["TB013_DeclaroSerMaiorCapaz"]);
                    retorno.TB004_Cep = Convert.ToString(reader["TB004_Cep"]);
                    retorno.TB013_Logradouro = Convert.ToString(reader["TB013_Logradouro"]).ToUpper().Trim();
                    retorno.TB013_Numero = Convert.ToString(reader["TB013_Numero"]).ToUpper().Trim();
                    retorno.TB013_Bairro = Convert.ToString(reader["TB013_Bairro"]).ToUpper().Trim();
                    retorno.TB013_Complemento = reader["TB013_Complemento"] is DBNull ? "-" : Convert.ToString(reader["TB013_Complemento"]).ToUpper().Trim();
                    retorno.TB013_DeclaroSerMaiorCapaz = Convert.ToInt16(reader["TB013_DeclaroSerMaiorCapaz"]);

                    MunicipioController objMunicipio = new MunicipioController();
                    objMunicipio.TB006_id = Convert.ToInt64(reader["TB006_id"]);
                    objMunicipio.TB006_Municipio = Convert.ToString(reader["TB006_Municipio"]).ToUpper().Trim();

                    EstadoController objEstado = new EstadoController();
                    objEstado.TB005_Id = Convert.ToInt64(reader["TB005_Id"]);
                    objEstado.TB005_Sigla = Convert.ToString(reader["TB005_Sigla"]).ToUpper().Trim();
                    objEstado.TB005_Estado = Convert.ToString(reader["TB005_Estado"]).ToUpper().Trim();

                    PaisController objPais = new PaisController();
                    objPais.TB003_id = Convert.ToInt64(reader["TB003_id"]);

                    objEstado.Pais = objPais;
                    objMunicipio.Estado = objEstado;

                    retorno.Municipio = objMunicipio;
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
        /// Descrição:  Pesquisar dados da Pessoa pelo CPF CNPJ
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       30/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public PessoaController PessoaSelectCpfcnpj(string cpfcnpj)
        {
            PessoaController retorno = new PessoaController();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT        dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB012_EraContezino, dbo.TB013_Pessoa.TB013_IdProtheus, dbo.TB013_Pessoa.TB013_Tipo,  ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_Cartao, dbo.TB013_Pessoa.TB013_CarteirinhaStatus, dbo.TB013_Pessoa.TB013_CodigoDependente, dbo.TB013_Pessoa.TB013_ListaNegra, ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB013_Pessoa.TB013_NomeExibicaoDetalhes, dbo.TB013_Pessoa.TB013_NomeExibicao, dbo.TB013_Pessoa.TB013_Sexo, dbo.TB013_Pessoa.TB013_RG, ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_RGOrgaoEmissor, dbo.TB013_Pessoa.TB013_DataNascimento, dbo.TB013_Pessoa.TB013_DeclaroSerMaiorCapaz, dbo.TB013_Pessoa.TB004_Cep, dbo.TB006_Municipio.TB006_id, ");
                sSql.Append(" dbo.TB006_Municipio.TB006_Municipio, dbo.TB005_Estado.TB005_Id, dbo.TB005_Estado.TB005_Estado, dbo.TB003_Pais.TB003_id, dbo.TB003_Pais.TB003_Pais, dbo.TB013_Pessoa.TB013_Logradouro, ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_Numero, dbo.TB013_Pessoa.TB013_Bairro, dbo.TB013_Pessoa.TB013_Complemento, dbo.TB013_Pessoa.TB013_InformacoesPortal, dbo.TB013_Pessoa.TB013_CadastradoEm, ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_CadastradoPor, dbo.TB013_Pessoa.TB013_AlteradoEm, dbo.TB013_Pessoa.TB013_AlteradoPor, dbo.TB013_Pessoa.TB013_Status, dbo.TB013_Pessoa.TB013_CancelamentoMotivo, ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_CancelamentoDescricao, dbo.TB013_Pessoa.TB012_Corporativo, dbo.TB013_Pessoa.TB013_CartaoEntregueEm, dbo.TB013_Pessoa.TB013_CartaoEntreguePara, ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id, dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB012_Contratos.TB012_Status, dbo.TB012_Contratos.TB012_Corporativo AS TB012_CorporativoPai ");
                sSql.Append(" FROM            dbo.TB013_Pessoa INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio ON dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id INNER JOIN ");
                sSql.Append(" dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id INNER JOIN ");
                sSql.Append(" dbo.TB003_Pais ON dbo.TB005_Estado.TB003_Id = dbo.TB003_Pais.TB003_id LEFT OUTER JOIN ");
                sSql.Append(" dbo.TB012_Contratos ON dbo.TB013_Pessoa.TB012_id = dbo.TB012_Contratos.TB012_id ");
                sSql.Append("WHERE dbo.TB013_Pessoa.TB013_CPFCNPJ = ");
                sSql.Append("'");
                sSql.Append(cpfcnpj.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "").Replace("_", ""));
                sSql.Append("'");
                sSql.Append(" or dbo.TB013_Pessoa.TB013_CPFCNPJ =");
                sSql.Append("'");
                sSql.Append(cpfcnpj.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "").Replace("_", "").TrimStart('0'));
                sSql.Append("'");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB013_id = Convert.ToInt64(reader["TB013_id"]);
                    retorno.TB013_TipoS = Convert.ToString(reader["TB013_Tipo"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("TB013_CPFCNPJ")))
                    {
                        if (retorno.TB013_TipoS == "1")
                        {
                            retorno.TB013_CPFCNPJ = Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"000\.000\.000\-00");
                        }
                        else
                        {
                            retorno.TB013_CPFCNPJ = Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"00\.000\.000\/0000\-00");
                        }
                    }

                    retorno.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    retorno.TB013_NomeExibicao = reader["TB013_NomeExibicao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    retorno.TB013_NomeExibicaoDetalhes= reader["TB013_NomeExibicaoDetalhes"] is DBNull ? reader["TB013_NomeExibicao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim() : reader["TB013_NomeExibicaoDetalhes"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    retorno.TB013_Cartao = reader["TB013_Cartao"].ToString().TrimEnd().TrimStart().ToUpper();
                    retorno.TB013_CarteirinhaStatusS = Enum.GetName(typeof(PessoaController.TB013_CarteirinhaStatusE), Convert.ToInt16(reader["TB013_CarteirinhaStatus"]));
                    retorno.TB013_CadastradoEm = Convert.ToDateTime(reader["TB013_CadastradoEm"].ToString());
                    retorno.TB013_CadastradoPorNome = reader["TB013_CadastradoPor"].ToString().TrimEnd().TrimStart().ToUpper();
                    retorno.TB013_AlteradoEm = Convert.ToDateTime(reader["TB013_AlteradoEm"].ToString());
                    retorno.TB013_AlteradoPorNome = reader["TB013_AlteradoPor"].ToString().TrimEnd().TrimStart().ToUpper();
                    retorno.TB013_StatusS = reader["TB013_Status"].ToString();
                    retorno.TB013_SexoS = Convert.ToString(reader["TB013_Sexo"]);
                    retorno.TB013_RG = Convert.ToString(reader["TB013_RG"]).ToUpper().Trim();
                    retorno.TB013_RGOrgaoEmissor = Convert.ToString(reader["TB013_RGOrgaoEmissor"]).ToUpper().Trim();
                    retorno.TB013_IdProtheus = reader["TB013_IdProtheus"].ToString();
                    retorno.TB013_DataNascimento = Convert.ToDateTime(reader["TB013_DataNascimento"]);
                    retorno.TB013_DeclaroSerMaiorCapaz = Convert.ToInt16(reader["TB013_DeclaroSerMaiorCapaz"]);
                    retorno.TB004_Cep = Convert.ToString(reader["TB004_Cep"]);
                    retorno.TB013_Logradouro = Convert.ToString(reader["TB013_Logradouro"]).ToUpper().Trim();
                    retorno.TB013_Numero = Convert.ToString(reader["TB013_Numero"]).ToUpper().Trim();
                    retorno.TB013_Bairro = Convert.ToString(reader["TB013_Bairro"]).ToUpper().Trim();
                    retorno.TB013_Complemento = Convert.ToString(reader["TB013_Complemento"]).ToUpper().Trim();
                    retorno.TB013_DeclaroSerMaiorCapaz = Convert.ToInt16(reader["TB013_DeclaroSerMaiorCapaz"]);
                    retorno.TB012_EraContezino = Convert.ToInt16(reader["TB012_EraContezino"]);
                    retorno.TB012_Corporativo  = reader["TB012_CorporativoPai"] is DBNull ? 0 : Convert.ToInt64(reader["TB012_CorporativoPai"]);

                    MunicipioController objMunicipio = new MunicipioController();
                    objMunicipio.TB006_id = Convert.ToInt64(reader["TB006_id"]);

                    EstadoController objEstado = new EstadoController();
                    objEstado.TB005_Id = Convert.ToInt64(reader["TB005_Id"]);

                    PaisController objPais = new PaisController();
                    objPais.TB003_id = Convert.ToInt64(reader["TB003_id"]);

                    objEstado.Pais = objPais;
                    objMunicipio.Estado = objEstado;

                    retorno.Municipio = objMunicipio;


                    retorno.TB012_Id = reader["TB012_Id"] is DBNull ? 0 : Convert.ToInt64(reader["TB012_Id"].ToString());

                    ContratosController objContrato = new ContratosController();
                    retorno.Contrato = objContrato;

                    if (retorno.TB012_Id > 0)
                    {
                        retorno.Contrato.TB012_TipoContrato = Convert.ToInt16(reader["TB012_TipoContrato"]);
                        retorno.Contrato.TB012_StatusS = Enum.GetName(typeof(ContratosController.TB012_StatusE), Convert.ToInt16(reader["TB012_Status"]));
                    }
                    else
                    {
                        retorno.Contrato.TB012_TipoContrato = 0;
                        retorno.Contrato.TB012_StatusS = "0";
                    }

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
        public List<PessoaController> DependentesSelect(Int64 tb012Id, Int64 tb013Id)
        {
            List<PessoaController> retorno = new List<PessoaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("SELECT ");
                sSql.Append("TB013_id,  ");
                sSql.Append("TB012_id,TB013_Cartao,  ");
                sSql.Append("TB013_NomeExibicao,  ");
                sSql.Append("TB013_DataNascimento,  ");
                sSql.Append("TB013_Status ");
                sSql.Append("FROM  ");
                sSql.Append("dbo.TB013_Pessoa ");
                sSql.Append("WHERE TB012_id = ");
                sSql.Append(tb012Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    if (Convert.ToInt64(reader["TB013_id"]) != tb013Id)
                    {
                        PessoaController obj = new PessoaController();
                        obj.TB013_id = Convert.ToInt64(reader["TB013_id"]);
                        obj.TB013_NomeExibicao = reader["TB013_NomeExibicao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                        obj.TB013_DataNascimento = Convert.ToDateTime(reader["TB013_DataNascimento"]);
                        obj.TB013_StatusS = Enum.GetName(typeof(PessoaController.TB013_StatusE), Convert.ToInt16(reader["TB013_Status"]));
                        obj.TB013_Cartao = reader["TB013_Cartao"] is DBNull ? "---" : reader["TB013_Cartao"].ToString();
                        //Calculando a Idade
                        DateTime dttFromDate = DateTime.Now;
                        DateTime dttToDate = Convert.ToDateTime(reader["TB013_DataNascimento"]);
                        TimeSpan tsDuration;
                        tsDuration = dttFromDate - dttToDate;
                        obj.TB013_Idade = Convert.ToInt32((tsDuration.Days) / 365);

                        retorno.Add(obj);
                    }
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
        public List<CategoriaIdadeControler> MembrosAtivosDoConrato(Int64 tb012Id, DateTime dataReferencia)
        {
            List<CategoriaIdadeControler> retorno = new List<CategoriaIdadeControler>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("SELECT ");
                sSql.Append("TB013_id, ");
                sSql.Append("TB012_id, ");
                sSql.Append("TB013_DataNascimento, ");
                sSql.Append("TB013_Status ");
                sSql.Append("FROM  ");
                sSql.Append("dbo.TB013_Pessoa ");
                sSql.Append("WHERE ");
                sSql.Append("TB012_id =  ");
                sSql.Append(tb012Id);
                sSql.Append("AND ");
                sSql.Append("TB013_Status <2 ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                DateTime dttFromDate = dataReferencia;// DateTime.Now;
                while (reader.Read())
                {
                    CategoriaIdadeControler obj = new CategoriaIdadeControler();
                    obj.DataNascimento = Convert.ToDateTime(reader["TB013_DataNascimento"]);
                    //Calculando a Idade
                    DateTime dttToDate = obj.DataNascimento;
                    TimeSpan idade;
                    idade = dttFromDate - dttToDate;                   
                    obj.idade = Convert.ToInt32((idade.Days) / 365);
                    retorno.Add(obj);
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
        public bool VincularTitularContato(long tb012Id, long tb013Id)
        {
           
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("update ");
                sSql.Append(" TB013_Pessoa");
                sSql.Append(" set ");
                sSql.Append(" TB012_id = ");
                sSql.Append(tb012Id);
                sSql.Append(" WHERE TB013_id = ");
                sSql.Append(tb013Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
               command.ExecuteReader();



                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;
        }
        public bool VincularDependenteContato(long tb012Id, long tb013Id)
        {
   
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("update ");
                sSql.Append(" TB013_Pessoa");
                sSql.Append(" set ");
                sSql.Append(" TB012_id = ");
                sSql.Append(tb012Id);
                sSql.Append(" WHERE TB013_id = ");
                sSql.Append(tb013Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
              command.ExecuteReader();



                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;
        }
        /// <summary>
        /// Descrição:  Filtra os dependentes que fazem aniverssario no mes do vencimento, para unir parcelas com planos diferentes
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       07/12/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<PessoaController> ListaAniversariantesDoMes(Int64 tb012Id, DateTime tb016Vencimento)
        {
            List<PessoaController> retorno = new List<PessoaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" SELECT ");
                sSql.Append(" TB013_id, ");
                sSql.Append(" TB012_id,  ");
                sSql.Append(" TB013_NomeCompleto, ");
                sSql.Append(" TB013_CodigoDependente, ");
                sSql.Append(" TB013_DataNascimento ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB013_Pessoa ");
                sSql.Append(" WHERE ");
                sSql.Append(" TB012_id = ");
                sSql.Append(tb012Id);//Somente do plano informado
                sSql.Append(" AND ");
                sSql.Append(" TB013_CodigoDependente > 1001 "); //Somente quem não for titular
                sSql.Append(" AND ");
                sSql.Append(" MONTH(TB013_DataNascimento) =  ");
                sSql.Append(tb016Vencimento.Month); //Somente do mes do vencimento
                sSql.Append(" ORDER BY  ");
                sSql.Append(" TB013_DataNascimento ");
               
                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PessoaController
                    {
                        TB013_id = Convert.ToInt64(reader["TB013_id"]),
                        TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd().TrimStart().ToUpper()
                            .ToUpper().Trim(),
                        TB013_DataNascimento = Convert.ToDateTime(reader["TB013_DataNascimento"])
                    };

                    retorno.Add(obj);
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
        public double PessoaCodigoDependenteNovo(long tb012Id)
        {
            double retorno = 1000;
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append("SELECT TB012_id, TB012_ProximoCodDependente ");
                sSql.Append("FROM dbo.TB012_Contratos ");
                sSql.Append("WHERE TB012_id =  ");
                sSql.Append(tb012Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno = reader["TB012_ProximoCodDependente"] is DBNull ? 0 : Convert.ToDouble(reader["TB012_ProximoCodDependente"]);
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
        public double PessoaManiorCodDependente(long tb012Id)
        {
            double retorno = 1001;
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append("SELECT TB012_id, MAX(TB013_CodigoDependente) AS TB013_CodigoDependenteMax ");
                sSql.Append(" FROM dbo.TB013_Pessoa ");
                sSql.Append(" GROUP BY TB012_id ");
                sSql.Append(" HAVING TB012_id = ");
                sSql.Append(tb012Id);
                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno = reader["TB013_CodigoDependenteMax"] is DBNull ? 0 : Convert.ToDouble(reader["TB013_CodigoDependenteMax"]) + 1;
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
        public List<PessoaController> PessoasParaGerarCartao(long tb012Id)
        {
            List<PessoaController> retorno = new List<PessoaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("SELECT dbo.TB012_Contratos.TB012_id, dbo.TB012_Contratos.TB012_Status, dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_Cartao, dbo.TB006_Municipio.TB006_Codigo, ");
                sSql.Append("dbo.TB013_Pessoa.TB013_CarteirinhaStatus, dbo.TB013_Pessoa.TB013_CodigoDependente, dbo.TB013_Pessoa.TB013_NomeCompleto ");
                sSql.Append("FROM dbo.TB012_Contratos INNER JOIN ");
                sSql.Append("dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB012_id = dbo.TB013_Pessoa.TB012_id INNER JOIN ");
                sSql.Append("dbo.TB006_Municipio ON dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id ");
                sSql.Append("WHERE(NOT(dbo.TB006_Municipio.TB006_Codigo IS NULL)) AND (dbo.TB013_Pessoa.TB013_CarteirinhaStatus = 0) AND (dbo.TB013_Pessoa.TB013_Status < 2)  ");
                sSql.Append(" and dbo.TB012_Contratos.TB012_id = ");
                sSql.Append(tb012Id);
    

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PessoaController();
                    var objContrato = new ContratosController();
                    var objMunicipio = new MunicipioController();


                    obj.TB013_id = Convert.ToInt64(reader["TB013_id"]);

                    
                    obj.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd().TrimStart();
                    obj.TB013_CodigoDependente = Convert.ToDouble(reader["TB013_CodigoDependente"]);
                    objContrato.TB012_Id = Convert.ToInt64(reader["TB012_id"]);
                    objMunicipio.TB006_Codigo = "1" + reader["TB006_Codigo"].ToString().TrimStart('0').Trim();
                    obj.TB013_id = Convert.ToInt64(reader["TB013_id"]);
                    obj.Contrato = objContrato;
                    obj.Municipio = objMunicipio;

                    retorno.Add(obj);
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
        public bool PessoaVincularCartao(long tb013Id, string tb013Cartao, long tb013AlteradoPor)
        {
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("update ");
                sSql.Append(" TB013_Pessoa ");
                sSql.Append(" set ");
                sSql.Append(" TB013_CarteirinhaStatus = 1, ");
                sSql.Append(" TB013_Cartao= ");
                sSql.Append(" '");
                sSql.Append(tb013Cartao.Trim());
                sSql.Append("', ");
                sSql.Append(" TB013_AlteradoEm= ");
                sSql.Append(" '");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyy hh:mm"));
                sSql.Append("',");
                sSql.Append(" TB013_AlteradoPor= ");
                sSql.Append(tb013AlteradoPor);
                sSql.Append(" WHERE TB013_id = ");
                sSql.Append(tb013Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
                 command.ExecuteReader();
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;
        }    
        /// <summary>
        /// Descrição:  Cancelar dependente do contrao
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       03/02/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public string PessoaAlteracaoParaLog(PessoaController pessoa)
        {
            string retorno;
            var camposAlterados = new StringBuilder();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);

                var sSql = new StringBuilder();
                sSql.Append(" SELECT TB013_id, TB013_NomeCompleto, TB013_NomeExibicao, TB013_Sexo, TB013_CPFCNPJ, TB013_RG, TB013_RGOrgaoEmissor, TB013_DataNascimento, TB006_id, TB013_Logradouro, TB013_Numero, ");
                sSql.Append(" TB013_Bairro, TB013_Complemento,TB013_Status ");
                sSql.Append(" FROM dbo.TB013_Pessoa ");
                sSql.Append(" WHERE TB013_id = ");
                sSql.Append(pessoa.TB013_id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    camposAlterados.Append("REGISTRO [");
                    camposAlterados.Append(pessoa.TB013_id);
                    camposAlterados.Append("] Referente a  [");
                    camposAlterados.Append(reader["TB013_NomeCompleto"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                    camposAlterados.Append("] Editado, segue alterações: ");
                    if (Convert.ToInt16(reader["TB013_Status"].ToString()) != Convert.ToInt16(pessoa.TB013_StatusS))
                    {

                        camposAlterados.Append("Status");
                        camposAlterados.Append(" de: [");
                        camposAlterados.Append(Enum.GetName(typeof(PessoaController.TB013_StatusE), Convert.ToInt16(reader["TB013_Status"].ToString())));
                        camposAlterados.Append("] para: [");
                        camposAlterados.Append(Enum.GetName(typeof(PessoaController.TB013_StatusE), Convert.ToInt16(pessoa.TB013_StatusS)));

                        camposAlterados.Append("];");
                    }

                    if (reader["TB013_NomeCompleto"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim() != pessoa.TB013_NomeCompleto.TrimEnd().TrimStart().ToUpper().ToUpper().Trim())
                    {
                        camposAlterados.Append("Nome Completo");
                        camposAlterados.Append(" de: [");
                        camposAlterados.Append(reader["TB013_NomeCompleto"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                        camposAlterados.Append("] para: [");
                        camposAlterados.Append(pessoa.TB013_NomeCompleto.TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                        camposAlterados.Append("];");
                    }
                        if (reader["TB013_NomeExibicao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim() != pessoa.TB013_NomeExibicao.TrimEnd().TrimStart().ToUpper().ToUpper().Trim())
                        {
                            camposAlterados.Append("Nome Exibição");
                            camposAlterados.Append(" de: [");
                            camposAlterados.Append(reader["TB013_NomeExibicao"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                            camposAlterados.Append("] para: [");
                            camposAlterados.Append(pessoa.TB013_NomeCompleto.TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                            camposAlterados.Append("];");
                        }
                 

                    

                    if (reader["TB013_Sexo"].ToString() != pessoa.TB013_SexoS)
                    {
                        camposAlterados.Append("Sexo");
                        camposAlterados.Append(" de: [");
                        camposAlterados.Append(reader["TB013_Sexo"]);
                        camposAlterados.Append("] para: [");
                        camposAlterados.Append(pessoa.TB013_SexoS);
                        camposAlterados.Append("];");
                    }

                    string validaCpfNull = pessoa.TB013_CPFCNPJ.TrimEnd().TrimStart().ToUpper().ToUpper().Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "");
                    if (validaCpfNull != string.Empty)
                    {
                        if (reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "") != pessoa.TB013_CPFCNPJ.TrimEnd().TrimStart().ToUpper().ToUpper().Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", ""))
                        {
                            camposAlterados.Append("CPF/CNPJ");
                            camposAlterados.Append(" de: [");
                            camposAlterados.Append(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                            camposAlterados.Append("] para: [");
                            camposAlterados.Append(pessoa.TB013_CPFCNPJ.TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                            camposAlterados.Append("];");
                        }
                    }

                    if (pessoa.TB013_RG != null)
                    {
                        if (reader["TB013_RG"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim() != pessoa.TB013_RG.TrimEnd().TrimStart().ToUpper().ToUpper().Trim())
                        {
                            camposAlterados.Append("RG");
                            camposAlterados.Append(" de: [");
                            camposAlterados.Append(reader["TB013_RG"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                            camposAlterados.Append("] para: [");
                            camposAlterados.Append(pessoa.TB013_RG.TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                            camposAlterados.Append("];");
                        }
                    }




                    if (pessoa.TB013_RGOrgaoEmissor != null)
                    {
                        if (reader["TB013_RGOrgaoEmissor"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim() != pessoa.TB013_RGOrgaoEmissor.TrimEnd().TrimStart().ToUpper().ToUpper().Trim())
                        {
                            camposAlterados.Append("Orgão Emissor");
                            camposAlterados.Append(" de: [");
                            camposAlterados.Append(reader["TB013_RGOrgaoEmissor"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                            camposAlterados.Append("] para: [");
                            camposAlterados.Append(pessoa.TB013_RGOrgaoEmissor.TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                            camposAlterados.Append("];");
                        }
                    }



                    int result = DateTime.Compare(Convert.ToDateTime(reader["TB013_DataNascimento"]), pessoa.TB013_DataNascimento);
                    if (result != 0)
                    {
                        camposAlterados.Append("Data Nascimento");
                        camposAlterados.Append(" de: [");
                        DateTime vData = Convert.ToDateTime(reader["TB013_DataNascimento"].ToString());

                        camposAlterados.Append(vData.ToString("dd/MM/yyyy"));
                        camposAlterados.Append("] para: [");
                        camposAlterados.Append(pessoa.TB013_DataNascimento.ToString("dd/MM/yyyy").TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                        camposAlterados.Append("];");
                    }

                    if (reader["TB013_Logradouro"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim() != pessoa.TB013_Logradouro.TrimEnd().TrimStart().ToUpper().ToUpper().Trim())
                    {
                        camposAlterados.Append("Logradouro");
                        camposAlterados.Append(" de: [");
                        camposAlterados.Append(reader["TB013_Logradouro"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                        camposAlterados.Append("] para: [");
                        camposAlterados.Append(pessoa.TB013_Logradouro.TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                        camposAlterados.Append("];");
                    }

                    if (reader["TB013_Numero"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim() != pessoa.TB013_Numero.TrimEnd().TrimStart().ToUpper().ToUpper().Trim())
                    {
                        camposAlterados.Append("Numero");
                        camposAlterados.Append(" de: [");
                        camposAlterados.Append(reader["TB013_Numero"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                        camposAlterados.Append("] para: [");
                        camposAlterados.Append(pessoa.TB013_Numero.TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                        camposAlterados.Append("];");
                    }

                    if (reader["TB013_Bairro"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim() != pessoa.TB013_Bairro.TrimEnd().TrimStart().ToUpper().ToUpper().Trim())
                    {
                        camposAlterados.Append("Bairro");
                        camposAlterados.Append(" de: [");
                        camposAlterados.Append(reader["TB013_Bairro"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                        camposAlterados.Append("] para: [");
                        camposAlterados.Append(pessoa.TB013_Bairro.TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                        camposAlterados.Append("];");
                    }

                    if (reader["TB013_Complemento"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim() != pessoa.TB013_Complemento.TrimEnd().TrimStart().ToUpper().ToUpper().Trim())
                    {
                        camposAlterados.Append("TB013_Complemento");
                        camposAlterados.Append(" de: [");
                        camposAlterados.Append(reader["TB013_Complemento"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                        camposAlterados.Append("] para: [");
                        camposAlterados.Append(pessoa.TB013_Complemento?.TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                        camposAlterados.Append("];");
                    }

                    if (Convert.ToInt64(reader["TB006_id"].ToString()) != pessoa.Municipio.TB006_id)
                    {
                        camposAlterados.Append("TB013_Complemento");
                        camposAlterados.Append(" de: [");
                        camposAlterados.Append(reader["TB006_id"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim());
                        camposAlterados.Append("] para: [");
                        camposAlterados.Append(pessoa.Municipio.TB006_id);
                        camposAlterados.Append("];");
                    }

                }

                con.Close();

                retorno = camposAlterados.ToString();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;

        }
        public bool DependenteAlterarStatus(long tb013Id, int status, long TB012_id)
        {
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("update ");
                sSql.Append(" TB013_Pessoa ");
                sSql.Append(" set ");
                sSql.Append(" TB013_Status = ");
                sSql.Append(status);
                sSql.Append("where  TB013_id = ");
                sSql.Append(tb013Id);
                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
                command.ExecuteReader();
                con.Close();

                if(status==3)
                {
                    con = new SqlConnection(ParametrosDAO.StringConexao);
                    var sSqlAlter = new StringBuilder();
                    sSqlAlter.Append("update ");
                    sSqlAlter.Append(" TB012_Contratos ");
                    sSqlAlter.Append(" set ");
                    sSqlAlter.Append(" TB012_Edicao = 1");
                    sSqlAlter.Append(status);
                    sSqlAlter.Append("where  TB012_id = ");
                    sSqlAlter.Append(TB012_id);

                    command = new SqlCommand(sSqlAlter.ToString(), con);
                    command.CommandTimeout = 300;
                    con.Open();
                    command.ExecuteReader();
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
        /// <summary>
        /// Descrição:  Listar cartoes
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       07/04/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<PessoaController> Cartoes(String query)
        {
            List<PessoaController> retornoList = new List<PessoaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" SELECT dbo.TB013_Pessoa.TB012_id, dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB013_Pessoa.TB013_Cartao, dbo.TB013_Pessoa.TB013_CarteirinhaStatus, ");
                sSql.Append(" dbo.View_Celulares.TB009_Contato ");
                sSql.Append(" FROM  dbo.TB013_Pessoa LEFT OUTER JOIN ");
                sSql.Append(" dbo.View_Celulares ON dbo.TB013_Pessoa.TB013_id = dbo.View_Celulares.TB013_id ");
                sSql.Append(" WHERE(NOT(dbo.TB013_Pessoa.TB013_Cartao IS NULL)) ");
                sSql.Append(query);
                sSql.Append(" ORDER BY dbo.TB013_Pessoa.TB012_id, dbo.TB013_Pessoa.TB013_id ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PessoaController
                    {
                        TB012_Id = Convert.ToInt64(reader["TB012_id"].ToString()),
                        TB013_id = Convert.ToInt64(reader["TB013_id"]),
                        TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString(),
                        TB013_Cartao = reader["TB013_Cartao"].ToString(),
                        TB013_CarteirinhaStatusS = Enum.GetName(typeof(PessoaController.TB013_CarteirinhaStatusE),
                            Convert.ToInt16(reader["TB013_CarteirinhaStatus"])),
                        Celular = reader["TB009_Contato"] is DBNull
                            ? "SEM CELULAR"
                            : reader["TB009_Contato"].ToString().TrimEnd().TrimStart()
                    };

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        /// <summary>
        /// Descrição:  Listar cartoes do Contrato
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       07/04/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<PessoaController> CartoesContrato(long tb012Id)
        {
            List<PessoaController> retornoList = new List<PessoaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" SELECT dbo.TB013_Pessoa.TB012_id, dbo.TB013_Pessoa.TB012_Corporativo, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_Cartao, dbo.TB013_Pessoa.TB013_CartaoEntregueEm,  ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_CartaoEntreguePara, dbo.TB013_Pessoa.TB013_CarteirinhaStatus, dbo.TB012_Contratos.TB012_Status ");
                sSql.Append(" FROM dbo.TB013_Pessoa INNER JOIN ");
                sSql.Append(" dbo.TB012_Contratos ON dbo.TB013_Pessoa.TB012_id = dbo.TB012_Contratos.TB012_id ");
                sSql.Append(" WHERE dbo.TB013_Pessoa.TB012_id =  ");
                sSql.Append(tb012Id);
                sSql.Append(" OR ");
                sSql.Append(" dbo.TB013_Pessoa.TB012_Corporativo =  ");
                sSql.Append(tb012Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PessoaController
                    {
                        TB013_id = Convert.ToInt64(reader["TB013_id"].ToString()),
                        TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString(),
                        TB013_CartaoEntregueEm = reader["TB013_CartaoEntregueEm"] is DBNull
                            ? DateTime.Now
                            : Convert.ToDateTime(reader["TB013_CartaoEntregueEm"]),
                        TB013_Cartao = reader["TB013_Cartao"].ToString(),
                        TB013_CartaoEntreguePara = reader["TB013_CartaoEntreguePara"] is DBNull
                            ? "----"
                            : reader["TB013_CartaoEntreguePara"].ToString(),
                        TB013_CarteirinhaStatusS = Enum.GetName(typeof(PessoaController.TB013_CarteirinhaStatusE),
                            Convert.ToInt16(reader["TB013_CarteirinhaStatus"]))
                    };


                    var objContrato =
                        new ContratosController
                        {
                            TB012_Id = Convert.ToInt64(reader["TB012_id"].ToString()),
                            TB012_StatusS = Enum.GetName(typeof(ContratosController.TB012_StatusE),
                                Convert.ToInt16(reader["TB012_Status"]))
                        };

                    obj.Contrato = objContrato;

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        public PessoaController CartaoPessoa(long tb013Id)
        {
            var retorno = new PessoaController();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("SELECT TB013_id, TB013_NomeCompleto, TB013_Status, TB013_Cartao, TB013_CartaoEntregueEm, TB013_CartaoEntreguePara, TB013_CarteirinhaStatus ");
                sSql.Append("FROM dbo.TB013_Pessoa ");
                sSql.Append("WHERE TB013_id =  ");
                sSql.Append(tb013Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB013_id = Convert.ToInt64(reader["TB013_id"]);
                    retorno.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    retorno.TB013_Cartao = reader["TB013_Cartao"].ToString().TrimEnd().TrimStart().ToUpper();
                    retorno.TB013_CartaoEntregueEm = reader["TB013_CartaoEntregueEm"] is DBNull ? DateTime.Now : Convert.ToDateTime(reader["TB013_CartaoEntregueEm"]);
                    retorno.TB013_CartaoEntreguePara = reader["TB013_CartaoEntreguePara"] is DBNull ? "----" : reader["TB013_CartaoEntreguePara"].ToString();
                    retorno.TB013_CarteirinhaStatusS = Enum.GetName(typeof(PessoaController.TB013_CarteirinhaStatusE), Convert.ToInt16(reader["TB013_CarteirinhaStatus"]));
                    retorno.TB013_StatusS = Enum.GetName(typeof(PessoaController.TB013_StatusE), Convert.ToInt16(reader["TB013_Status"]));
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
        public Boolean CartaoConfirmarRecebimento(Int64 tb013Id, Int64 tb011Id)
        {
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();
                sSql.Append("update ");
                sSql.Append(" TB013_Pessoa ");
                sSql.Append(" set ");
                sSql.Append(" TB013_CarteirinhaStatus = 5, ");
                sSql.Append(" TB013_AlteradoEm = '");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                sSql.Append("',");
                sSql.Append(" TB013_AlteradoPor = ");
                sSql.Append(tb011Id);
                sSql.Append(" where  TB013_id = ");
                sSql.Append(tb013Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
                 command.ExecuteReader();
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;
        }
        public bool CartaoConfirmarEntrega(long tb013Id, string entreguePara, long tb011Id)
        {
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();
                sSql.Append("update ");
                sSql.Append(" TB013_Pessoa ");
                sSql.Append(" set ");
                sSql.Append(" TB013_CarteirinhaStatus = 4, ");
                sSql.Append(" TB013_CadastradoEm = '");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                sSql.Append("',");
                sSql.Append(" TB013_CadastradoPor = ");
                sSql.Append(tb011Id);
                sSql.Append(",");
                sSql.Append(" TB013_CartaoEntregueEm = '");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                sSql.Append("',");
                sSql.Append(" TB013_CartaoEntreguePara = ");
                sSql.Append("'");
                sSql.Append(entreguePara);
                sSql.Append("'");
                sSql.Append(" where  TB013_id = ");
                sSql.Append(tb013Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
                 command.ExecuteReader();
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;
        }
        /// <summary>
        /// Descrição:  Verifica se o cartão já foi gerado
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       24/04/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public PessoaController Cartao(string tb013Cartao)
        {
            PessoaController retorno = new PessoaController();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("SELECT TB013_id, TB013_Cartao, TB013_NomeCompleto ");
                sSql.Append("FROM dbo.TB013_Pessoa ");
                sSql.Append("WHERE  TB013_Cartao = ");
                sSql.Append("'");
                sSql.Append(tb013Cartao.Trim());
                sSql.Append("'");

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB013_id = Convert.ToInt64(reader["TB013_id"]);
                    retorno.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd().TrimStart().ToUpper().ToUpper().Trim();
                    retorno.TB013_Cartao = reader["TB013_Cartao"].ToString().TrimEnd().TrimStart().ToUpper();
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
        /// Descrição:  Verifica se o cartão já foi gerado
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       24/04/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool CartaoManual(Int64 tb013Id, string tb013Cartao, int tb013CarteirinhaStatus, long tb013AlteradoPor)
        {
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" update ");
                sSql.Append(" TB013_Pessoa ");
                sSql.Append(" set ");
                sSql.Append(" TB013_Cartao = ");
                sSql.Append("'");
                sSql.Append(tb013Cartao);
                sSql.Append("'");
                sSql.Append(",");
                sSql.Append(" TB013_CarteirinhaStatus= ");
                sSql.Append(tb013CarteirinhaStatus);
                sSql.Append(",");
                sSql.Append(" TB013_AlteradoEm= ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                sSql.Append("'");
                sSql.Append(",");
                sSql.Append(" TB013_AlteradoPor= ");
                sSql.Append(tb013AlteradoPor);
                sSql.Append(" where TB013_id = ");
                sSql.Append(tb013Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
               command.ExecuteReader();
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;
        }
        /// <summary>
        /// Descrição:  REcupera informações do titular do contrato parceiro para geração do cartão
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       25/04/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public PessoaController RecuperarDadosParceiroParaCartao(Int64 tb012Id)
        {
            PessoaController retorno = new PessoaController();
            MunicipioController objMunicipio = new MunicipioController();
            retorno.Municipio = objMunicipio;
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" SELECT        dbo.TB012_Contratos.TB012_id, dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB006_Municipio.TB006_Codigo, dbo.TB012_Contratos.TB012_ProximoCodDependente FROM dbo.TB012_Contratos INNER JOIN dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id INNER JOIN dbo.TB006_Municipio ON dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id");
                sSql.Append(" WHERE dbo.TB012_Contratos.TB012_id = ");
                sSql.Append(tb012Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB013_id = Convert.ToInt64(reader["TB013_id"]);
                    retorno.TB012_ProximoCodDependente = reader["TB012_ProximoCodDependente"] is DBNull ? 1000 : Convert.ToInt32(reader["TB013_CPFCNPJ"]);
                    retorno.Municipio.TB006_Codigo = reader["TB006_Codigo"].ToString().TrimEnd();
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
        public bool GravarCartaoParceiro(long tb013Id, string tb013Cartao, int tb013CarteirinhaStatus, long tb013AlteradoPor)
        {
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" update ");
                sSql.Append(" TB013_Pessoa ");
                sSql.Append(" set ");
                sSql.Append(" TB013_Cartao = ");
                sSql.Append("'");
                sSql.Append(tb013Cartao);
                sSql.Append("'");
                sSql.Append(",");
                sSql.Append(" TB013_CarteirinhaStatus= ");
                sSql.Append(tb013CarteirinhaStatus);
                sSql.Append(",");
                sSql.Append(" TB013_AlteradoEm= ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                sSql.Append("'");
                sSql.Append(",");
                sSql.Append(" TB013_AlteradoPor= ");
                sSql.Append(tb013AlteradoPor);
                sSql.Append(" where TB013_id = ");
                sSql.Append(tb013Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
                command.ExecuteReader();
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;
        }
        //public List<PessoaController> GeracaoCartaoBaixaPagamentoManual_RetornarListaParaGeracao(long tb016Id)
        //{
        //    List<PessoaController> retornoList = new List<PessoaController>();
        //    try
        //    {
        //        SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
        //        StringBuilder sSql = new StringBuilder();

        //        sSql.Append(" SELECT  dbo.TB012_Contratos.TB012_id, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_CarteirinhaStatus, ");
        //        sSql.Append(" dbo.TB012_Contratos.TB012_Status, dbo.TB006_Municipio.TB006_Codigo, dbo.TB013_Pessoa.TB013_CodigoDependente, dbo.View_Celulares.TB009_Contato ");
        //        sSql.Append(" FROM dbo.TB016_Parcela INNER JOIN ");
        //        sSql.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id INNER JOIN ");
        //        sSql.Append(" dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB012_id  = dbo.TB013_Pessoa.TB012_id INNER JOIN ");
        //        sSql.Append(" dbo.TB006_Municipio ON dbo.TB013_Pessoa.TB006_id  = dbo.TB006_Municipio.TB006_id LEFT OUTER JOIN ");
        //        sSql.Append(" dbo.View_Celulares ON dbo.TB013_Pessoa.TB013_id   = dbo.View_Celulares.TB013_id ");
        //        sSql.Append(" WHERE dbo.TB016_Parcela.TB016_id =  ");
        //        sSql.Append(tb016Id);
        //        sSql.Append(" AND dbo.TB013_Pessoa.TB013_CarteirinhaStatus = 0 ");
        //        sSql.Append(" ORDER BY dbo.TB013_Pessoa.TB013_id ");


        //        var command = new SqlCommand(sSql.ToString(), con);

        //        con.Open();
        //        var reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var obj = new PessoaController
        //            {
        //                Contato = new ContatoController(),
        //                TB013_id = Convert.ToInt64(reader["TB013_id"].ToString()),
        //                TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString(),
        //                TB012_Id = Convert.ToInt64(reader["TB012_id"].ToString()),
        //                Municipio = new MunicipioController {TB006_Codigo = reader["TB006_Codigo"].ToString()}
        //            };
        //            //ContatoController objContato = new ContatoController();

        //            obj.TB013_CodigoDependente = Convert.ToInt64(reader["TB013_CodigoDependente"].ToString());
        //            obj.Contato.TB009_Contato = reader["TB009_Contato"].ToString().Replace("-", "").Trim();
        //            retornoList.Add(obj);
        //        }

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        // ReSharper disable once PossibleIntendedRethrow
        //        throw ex;
        //    }
        //    return retornoList;
        //}
       
        /// <summary>
        /// Descrição:  Incluir nova Pessoa
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       11/11/2016
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public PessoaController ColaboradorInsert(PessoaController colaborador)
        {
            PessoaController retorno = new PessoaController();
            string insertSql = "INSERT INTO TB013_Pessoa( " +
                               "TB013_RGOrgaoEmissor,TB013_RG,TB012_EraContezino,TB013_IdProtheus,TB013_Matricula,TB013_Tipo, " +
                               "TB013_CPFCNPJ, " +
                               "TB013_NomeCompleto, " +
                               "TB013_NomeExibicao, " +
                               "TB013_Sexo, " +
                               "TB013_DataNascimento, " +
                               "TB013_DeclaroSerMaiorCapaz, " +
                               "TB004_Cep, " +
                               "TB006_id, " +
                               "TB013_Logradouro, " +
                               "TB013_Numero, " +
                               "TB013_Bairro, " +
                               "TB013_Complemento, " +
                               "TB013_CadastradoEm, " +
                               "TB013_CadastradoPor, " +
                               "TB013_AlteradoEm, " +
                               "TB013_AlteradoPor, " +
                               "TB013_CodigoDependente, " +
                               "TB013_Status, " +
                               "TB013_CarteirinhaStatus, " +
                               "TB012_Corporativo " +
                               ") VALUES ( " +
                               "@TB013_RGOrgaoEmissor,@TB013_RG,@TB012_EraContezino,@TB013_IdProtheus,@TB013_Matricula,@TB013_Tipo, " +
                               "@TB013_CPFCNPJ, " +
                               "@TB013_NomeCompleto, " +
                               "@TB013_NomeExibicao, " +
                               "@TB013_Sexo, " +
                               "@TB013_DataNascimento, " +
                               "@TB013_DeclaroSerMaiorCapaz, " +
                               "@TB004_Cep, " +
                               "@TB006_id, " +
                               "@TB013_Logradouro, " +
                               "@TB013_Numero, " +
                               "@TB013_Bairro, " +
                               "@TB013_Complemento, " +
                               "@TB013_CadastradoEm, " +
                               "@TB013_CadastradoPor, " +
                               "@TB013_AlteradoEm, " +
                               "@TB013_AlteradoPor, " +
                               "@TB013_CodigoDependente, " +
                               "@TB013_Status, " +
                               "@TB013_CarteirinhaStatus, " +
                               "@TB012_Corporativo " +
                               ") SELECT SCOPE_IDENTITY()";
            try
            {
                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand(insertSql, con);
                    command.CommandTimeout = 300;

                    command.Parameters.AddWithValue("@TB013_Tipo", Convert.ToInt16(colaborador.TB013_TipoS));


                    string vCpfcnjp = colaborador.TB013_CPFCNPJ.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "").Trim();
                    if (colaborador.TB013_TipoS == "1")
                    {
                        vCpfcnjp = vCpfcnjp.PadLeft(11, '0');
                    }

                    if (vCpfcnjp.Trim() == string.Empty)
                    {
                        command.Parameters.AddWithValue("@TB013_CPFCNPJ", "SEM CPF");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@TB013_CPFCNPJ", vCpfcnjp);
                    }

                    command.Parameters.AddWithValue("@TB012_EraContezino", 0);
                    command.Parameters.AddWithValue("@TB012_Corporativo", colaborador.TB012_Corporativo);                   
                    command.Parameters.AddWithValue("@TB013_RG", "SEM RG");
                    command.Parameters.AddWithValue("@TB013_RGOrgaoEmissor", "SEM");
                    command.Parameters.AddWithValue("@TB013_IdProtheus", "Id Protheus");
                    command.Parameters.AddWithValue("@TB013_Matricula", colaborador.TB013_Matricula.ToUpper().TrimEnd());
                    command.Parameters.AddWithValue("@TB013_NomeCompleto", colaborador.TB013_NomeCompleto.ToUpper().TrimEnd());
                    command.Parameters.AddWithValue("@TB013_Status", colaborador.TB013_StatusS);
                    command.Parameters.AddWithValue("@TB013_NomeExibicao", colaborador.TB013_NomeExibicao.ToUpper().TrimEnd());
                    command.Parameters.AddWithValue("@TB013_Sexo", Convert.ToInt16(colaborador.TB013_SexoS));
                    command.Parameters.AddWithValue("@TB013_DataNascimento", colaborador.TB013_DataNascimento);
                    command.Parameters.AddWithValue("@TB013_DeclaroSerMaiorCapaz", colaborador.TB013_DeclaroSerMaiorCapaz);
                    command.Parameters.AddWithValue("@TB004_Cep", colaborador.TB004_Cep.Replace("-", ""));
                    command.Parameters.AddWithValue("@TB006_id", colaborador.Municipio.TB006_id);
                    command.Parameters.AddWithValue("@TB013_Logradouro", colaborador.TB013_Logradouro.ToUpper().TrimEnd());
                    command.Parameters.AddWithValue("@TB013_Numero", colaborador.TB013_Numero.ToUpper().Trim());
                    command.Parameters.AddWithValue("@TB013_Bairro", colaborador.TB013_Bairro.ToUpper().TrimEnd());
                    command.Parameters.AddWithValue("@TB013_Complemento", colaborador.TB013_Complemento.ToUpper().TrimEnd());
                    command.Parameters.AddWithValue("@TB013_CadastradoEm", colaborador.TB013_CadastradoEm);
                    command.Parameters.AddWithValue("@TB013_CadastradoPor", colaborador.TB013_CadastradoPor);
                    command.Parameters.AddWithValue("@TB013_AlteradoEm", colaborador.TB013_AlteradoEm);
                    command.Parameters.AddWithValue("@TB013_AlteradoPor", colaborador.TB013_AlteradoPor);
                    command.Parameters.AddWithValue("@TB013_CodigoDependente", colaborador.TB013_CodigoDependente);
                    command.Parameters.AddWithValue("@TB013_CarteirinhaStatus", 0);

                    retorno.TB013_id = Convert.ToInt32(command.ExecuteScalar());

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retorno;

        }
        public Boolean VincularFamiliarCorporativo(Int64 tb012Id, Int64 tb013Id)
        {
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("update ");
                sSql.Append(" TB013_Pessoa");
                sSql.Append(" set ");
                sSql.Append(" TB012_Corporativo = ");
                sSql.Append(tb012Id);
                sSql.Append(" WHERE TB013_id = ");
                sSql.Append(tb013Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                command.ExecuteReader();

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;
        }
        /// <summary>
        /// Descrição:  Listar Colaboradores ligados ao contrato Corporativo
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       24/04/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<PessoaController> ColaboradoresCorporativo(long tb012Corporativo)
        {
            List<PessoaController> retornoList = new List<PessoaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" SELECT TB013_id, TB013_Status, TB013_Matricula, TB013_NomeCompleto FROM dbo.TB013_Pessoa ");
                sSql.Append(" WHERE TB012_Corporativo =  ");
                sSql.Append(tb012Corporativo);
                sSql.Append("  AND dbo.TB013_Pessoa.TB013_CodigoDependente = 1001 and TB013_Status < 2");
                sSql.Append(" ORDER BY TB013_NomeCompleto ");

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PessoaController
                    {
                        Contato = new ContatoController(),
                        TB013_id = Convert.ToInt64(reader["TB013_id"].ToString()),
                        TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString(),
                        TB013_StatusS = reader["TB013_Status"] is DBNull
                            ? "0"
                            : Enum.GetName(typeof(PessoaController.TB013_StatusE),
                                Convert.ToInt16(reader["TB013_Status"])),
                        TB013_Matricula = reader["TB013_Matricula"].ToString()
                    };

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        /// <summary>
        /// Descrição:  Recuperar Colaborador Titular do Plano Familiar Corporativo
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       06/06/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public PessoaController ColaboradorCorporativo(Int64 tb013Id)
        {
            PessoaController retorno = new PessoaController
            {
                Contrato = new ContratosController
                {
                    PontoDeVenda = new PontoDeVendaController(),
                    Unidade = new UnidadeController {Municipio = new MunicipioController()}
                }
            };
            retorno.Pais = new PaisController();
            retorno.Estado = new EstadoController();
            retorno.Municipio = new MunicipioController();
            //MunicipioController objMunicipio = new MunicipioController();
            //Retorno.Municipio = objMunicipio;
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" SELECT dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB012_id, dbo.TB013_Pessoa.TB012_Corporativo, dbo.TB013_Pessoa.TB013_CodigoDependente, dbo.TB013_Pessoa.TB013_CPFCNPJ,  ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_Matricula, dbo.TB013_Pessoa.TB013_Cartao, dbo.TB013_Pessoa.TB013_CarteirinhaStatus, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB013_Pessoa.TB013_DataNascimento, ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_Sexo, dbo.TB013_Pessoa.TB013_Status, dbo.TB013_Pessoa.TB004_Cep, dbo.TB006_Municipio.TB006_id, dbo.TB005_Estado.TB005_Id, dbo.TB003_Pais.TB003_id, ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_Logradouro, dbo.TB013_Pessoa.TB013_Numero, dbo.TB013_Pessoa.TB013_Bairro, dbo.TB013_Pessoa.TB013_Complemento, ContratoCorporativo.TB012_id AS Expr1, ");
                sSql.Append(" ContratoCorporativoUnidade.TB020_id, ContratoCorporativoUnidade.TB020_NomeFantasia, ContratoCorporativoMunicipio.TB006_Municipio, ContratoCorporativoUnidade.TB020_Bairro, ");
                sSql.Append(" ContratoCorporativoUnidade.TB012_idCorporativo, ContratoCorporativoUnidade.TB020_Logradouro, ContratoCorporativoUnidade.TB020_Numero, ContratoCorporativo.TB002_id ");
                sSql.Append(" FROM dbo.TB013_Pessoa INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio ON dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id INNER JOIN ");
                sSql.Append(" dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id INNER JOIN ");
                sSql.Append(" dbo.TB003_Pais ON dbo.TB005_Estado.TB003_Id = dbo.TB003_Pais.TB003_id INNER JOIN ");
                sSql.Append(" dbo.TB012_Contratos AS ContratoColaborador ON dbo.TB013_Pessoa.TB012_id = ContratoColaborador.TB012_id INNER JOIN ");
                sSql.Append(" dbo.TB012_Contratos AS ContratoCorporativo ON ContratoColaborador.TB012_Corporativo = ContratoCorporativo.TB012_id INNER JOIN ");
                sSql.Append(" dbo.TB020_Unidades AS ContratoCorporativoUnidade ON ContratoCorporativo.TB012_id = ContratoCorporativoUnidade.TB012_idCorporativo INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio AS ContratoCorporativoMunicipio ON ContratoCorporativoUnidade.TB006_id = ContratoCorporativoMunicipio.TB006_id ");
                sSql.Append(" WHERE dbo.TB013_Pessoa.TB013_id = ");
                sSql.Append(tb013Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB013_id = Convert.ToInt64(reader["TB013_id"]);
                    retorno.TB013_CPFCNPJ = reader["TB013_CPFCNPJ"] is DBNull ? "SEM CPF" : Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"000\.000\.000\-00");
                    retorno.TB013_CodigoDependente = Convert.ToDouble(reader["TB013_CodigoDependente"]);
                    retorno.TB013_Matricula = reader["TB013_Matricula"].ToString().Trim();
                    retorno.TB013_Cartao = reader["TB013_Cartao"].ToString().Trim();
                    retorno.TB013_CarteirinhaStatusS = reader["TB013_CarteirinhaStatus"] is DBNull ? Enum.GetName(typeof(PessoaController.TB013_CarteirinhaStatusE), 0) : Enum.GetName(typeof(PessoaController.TB013_CarteirinhaStatusE), Convert.ToInt16(reader["TB013_CarteirinhaStatus"]));
                    retorno.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().Trim().ToUpper();
                    retorno.TB013_DataNascimento = Convert.ToDateTime(reader["TB013_DataNascimento"]);
                    retorno.TB013_SexoS = Convert.ToString(reader["TB013_Sexo"]);
                    retorno.TB013_StatusS = reader["TB013_Status"] is DBNull ? "0" : reader["TB013_Status"].ToString();
                    retorno.TB004_Cep = Convert.ToString(reader["TB004_Cep"]);
                    retorno.TB013_Logradouro = Convert.ToString(reader["TB013_Logradouro"]).ToUpper().TrimEnd();
                    retorno.TB013_Numero = Convert.ToString(reader["TB013_Numero"]).ToUpper().TrimEnd();
                    retorno.TB013_Bairro = Convert.ToString(reader["TB013_Bairro"]).ToUpper().TrimEnd();
                    retorno.TB013_Complemento = reader["TB013_Complemento"] is DBNull ? "-" : Convert.ToString(reader["TB013_Complemento"]).ToUpper().TrimEnd();
                    retorno.Contrato.TB012_Id = Convert.ToInt64(reader["TB012_id"]);
                    retorno.Contrato.TB012_Corporativo = Convert.ToInt64(reader["TB012_Corporativo"]);
                    retorno.Contrato.PontoDeVenda.TB002_id = Convert.ToInt64(reader["TB002_id"]);
                    retorno.Municipio.TB006_id = Convert.ToInt64(reader["TB006_id"]);
                    retorno.Estado.TB005_Id = Convert.ToInt64(reader["TB005_Id"]);
                    retorno.Pais.TB003_id = Convert.ToInt64(reader["TB003_id"]);
                    retorno.Contrato.Unidade.TB020_id = Convert.ToInt64(reader["TB020_id"]);
                    retorno.Contrato.Unidade.TB020_NomeFantasia = reader["TB020_NomeFantasia"].ToString().TrimEnd().ToUpper();
                    retorno.Contrato.Unidade.Municipio.TB006_Municipio = reader["TB006_Municipio"].ToString().TrimEnd().ToUpper();
                    retorno.Contrato.Unidade.TB020_Bairro = reader["TB020_Bairro"].ToString().TrimEnd().ToUpper();
                    retorno.Contrato.Unidade.TB020_Logradouro = reader["TB020_Logradouro"].ToString().TrimEnd().ToUpper();
                    retorno.Contrato.Unidade.TB020_Numero = reader["TB020_Numero"].ToString().TrimEnd().ToUpper();
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
        public PessoaController ColaboradorCorporativoTitular(Int64 tb013Id)
        {
            PessoaController retorno = new PessoaController();
            retorno.Contrato = new ContratosController();
            retorno.Contrato.PontoDeVenda = new PontoDeVendaController();
            retorno.Contrato.Unidade = new UnidadeController();
            retorno.Contrato.Unidade.Municipio = new MunicipioController();
            retorno.Pais = new PaisController();
            retorno.Estado = new EstadoController();
            retorno.Municipio = new MunicipioController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" SELECT TOP (100) PERCENT dbo.TB012_Contratos.TB012_id, dbo.TB013_Pessoa.TB013_Cartao, dbo.TB013_Pessoa.TB013_CarteirinhaStatus, dbo.TB013_Pessoa.TB013_CodigoDependente, dbo.TB013_Pessoa.TB013_CPFCNPJ,  ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB013_Pessoa.TB013_Sexo, dbo.TB013_Pessoa.TB013_DataNascimento, dbo.TB013_Pessoa.TB013_Logradouro, dbo.TB013_Pessoa.TB013_Bairro, ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_Numero, dbo.TB013_Pessoa.TB013_Complemento, dbo.TB013_Pessoa.TB013_Status, dbo.TB013_Pessoa.TB013_Matricula, dbo.TB006_Municipio.TB006_id, dbo.TB003_Pais.TB003_id, ");
                sSql.Append(" dbo.TB020_Unidades.TB020_id, dbo.TB020_Unidades.TB020_NomeFantasia, dbo.TB020_Unidades.TB020_Logradouro, dbo.TB020_Unidades.TB020_Numero, dbo.TB020_Unidades.TB020_Bairro, ");
                sSql.Append(" dbo.TB020_Unidades.TB012_idCorporativo, TB006_Municipio_1.TB006_Municipio, dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB004_Cep, TB012_Contratos_1.TB012_id AS TB012_Corporativo, dbo.TB012_Contratos.TB002_id, dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" FROM dbo.TB012_Contratos INNER JOIN ");
                sSql.Append(" dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id AND dbo.TB012_Contratos.TB012_id <> dbo.TB013_Pessoa.TB012_Corporativo INNER JOIN ");
                sSql.Append(" dbo.TB012_Contratos AS TB012_Contratos_1 ON dbo.TB012_Contratos.TB012_Corporativo = TB012_Contratos_1.TB012_id INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio ON dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id INNER JOIN ");
                sSql.Append(" dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id INNER JOIN ");
                sSql.Append(" dbo.TB003_Pais ON dbo.TB005_Estado.TB003_Id = dbo.TB003_Pais.TB003_id INNER JOIN ");
                sSql.Append(" dbo.TB020_Unidades ON TB012_Contratos_1.TB012_id = dbo.TB020_Unidades.TB012_idCorporativo INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio AS TB006_Municipio_1 ON dbo.TB020_Unidades.TB006_id = TB006_Municipio_1.TB006_id ");
                sSql.Append(" WHERE dbo.TB012_Contratos.TB013_id = ");
                sSql.Append(tb013Id);

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB013_id = Convert.ToInt64(reader["TB013_id"]);
                    retorno.TB013_CPFCNPJ = reader["TB013_CPFCNPJ"] is DBNull ? "SEM CPF" : Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"000\.000\.000\-00");
                    retorno.TB013_CodigoDependente = Convert.ToDouble(reader["TB013_CodigoDependente"]);
                    retorno.TB013_Matricula = reader["TB013_Matricula"].ToString().Trim();
                    retorno.TB013_Cartao = reader["TB013_Cartao"].ToString().Trim();
                    retorno.TB013_CarteirinhaStatusS = reader["TB013_CarteirinhaStatus"] is DBNull ? Enum.GetName(typeof(PessoaController.TB013_CarteirinhaStatusE), 0) : Enum.GetName(typeof(PessoaController.TB013_CarteirinhaStatusE), Convert.ToInt16(reader["TB013_CarteirinhaStatus"]));
                    retorno.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().Trim().ToUpper();
                    retorno.TB013_DataNascimento = Convert.ToDateTime(reader["TB013_DataNascimento"]);
                    retorno.TB013_SexoS = Convert.ToString(reader["TB013_Sexo"]);
                    retorno.TB013_StatusS = reader["TB013_Status"] is DBNull ? "0" : reader["TB013_Status"].ToString();
                    retorno.TB004_Cep = Convert.ToString(reader["TB004_Cep"]);
                    retorno.TB013_Logradouro = Convert.ToString(reader["TB013_Logradouro"]).ToUpper().TrimEnd();
                    retorno.TB013_Numero = Convert.ToString(reader["TB013_Numero"]).ToUpper().TrimEnd();
                    retorno.TB013_Bairro = Convert.ToString(reader["TB013_Bairro"]).ToUpper().TrimEnd();
                    retorno.TB013_Complemento = reader["TB013_Complemento"] is DBNull ? "-" : Convert.ToString(reader["TB013_Complemento"]).ToUpper().TrimEnd();
                    retorno.Contrato.TB012_Id = Convert.ToInt64(reader["TB012_id"]);
                    retorno.Contrato.TB012_Corporativo = Convert.ToInt64(reader["TB012_Corporativo"]);
                    retorno.Contrato.PontoDeVenda.TB002_id = Convert.ToInt64(reader["TB002_id"]);
                    retorno.Municipio.TB006_id = Convert.ToInt64(reader["TB006_id"]);
                    retorno.Estado.TB005_Id = Convert.ToInt64(reader["TB005_Id"]);
                    retorno.Pais.TB003_id = Convert.ToInt64(reader["TB003_id"]);
                    retorno.Contrato.Unidade.TB020_id = Convert.ToInt64(reader["TB020_id"]);
                    retorno.Contrato.Unidade.TB020_NomeFantasia = reader["TB020_NomeFantasia"].ToString().TrimEnd().ToUpper();
                    retorno.Contrato.Unidade.Municipio.TB006_Municipio = reader["TB006_Municipio"].ToString().TrimEnd().ToUpper();
                    retorno.Contrato.Unidade.TB020_Bairro = reader["TB020_Bairro"].ToString().TrimEnd().ToUpper();
                    retorno.Contrato.Unidade.TB020_Logradouro = reader["TB020_Logradouro"].ToString().TrimEnd().ToUpper();
                    retorno.Contrato.Unidade.TB020_Numero = reader["TB020_Numero"].ToString().TrimEnd().ToUpper();
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
        /// Descrição:  Atualizar Colaborador Titular do Plano Familiar Corporativo
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       06/06/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public Boolean ColaboradorUpdate(PessoaController colaborador)
        {
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" update ");
                sSql.Append(" TB013_Pessoa ");
                sSql.Append(" set ");
                sSql.Append(" TB013_Matricula = ");
                sSql.Append("'");
                sSql.Append(colaborador.TB013_Matricula.TrimEnd().ToUpper());
                sSql.Append("',");
                sSql.Append(" TB013_NomeCompleto = ");
                sSql.Append("'");
                sSql.Append(colaborador.TB013_NomeCompleto.TrimEnd().ToUpper());
                sSql.Append("',");
                sSql.Append(" TB013_CPFCNPJ = ");
                sSql.Append("'");
                sSql.Append(colaborador.TB013_CPFCNPJ.TrimEnd().ToUpper().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim());
                sSql.Append("',");
                sSql.Append(" TB013_Sexo = ");
                sSql.Append(colaborador.TB013_SexoS);
                sSql.Append(",");
                sSql.Append(" TB013_DataNascimento = ");
                sSql.Append("'");
                sSql.Append(colaborador.TB013_DataNascimento.ToString("MM/dd/yyyy"));
                sSql.Append("',");
                sSql.Append(" TB004_Cep = ");
                sSql.Append("'");
                sSql.Append(colaborador.TB004_Cep.TrimEnd().ToUpper().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim());
                sSql.Append("',");
                sSql.Append(" TB006_id = ");
                sSql.Append(colaborador.Municipio.TB006_id);
                sSql.Append(", TB013_Logradouro = ");
                sSql.Append("'");
                sSql.Append(colaborador.TB013_Logradouro.TrimEnd().ToUpper());
                sSql.Append("',");
                sSql.Append(" TB013_Numero = ");
                sSql.Append("'");
                sSql.Append(colaborador.TB013_Numero.TrimEnd());
                sSql.Append("',");
                sSql.Append(" TB013_Bairro = ");
                sSql.Append("'");
                sSql.Append(colaborador.TB013_Bairro.TrimEnd());
                sSql.Append("',");
                sSql.Append(" TB013_Complemento = ");
                sSql.Append("'");
                sSql.Append(colaborador.TB013_Complemento.TrimEnd());
                sSql.Append("',");
                sSql.Append(" TB013_AlteradoEm = ");
                sSql.Append("'");
                sSql.Append(colaborador.TB013_AlteradoEm.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("',");
                sSql.Append(" TB013_AlteradoPor = ");              
                sSql.Append(colaborador.TB013_AlteradoPor);              
                sSql.Append(" where TB013_id = ");
                sSql.Append(colaborador.TB013_id);
                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
               command.ExecuteReader();
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }
        public List<PessoaController> DependentesParaInativacao(long tb012Id)
        {
            var retornoList = new List<PessoaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT  TB013_id, TB013_NomeCompleto FROM dbo.TB013_Pessoa WHERE TB012_id = ");
                sSql.Append(tb012Id);
                sSql.Append(" AND TB013_Status = 3");

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PessoaController
                    {
                        Contato = new ContatoController(),
                        TB013_id = Convert.ToInt64(reader["TB013_id"].ToString()),
                        TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd().ToUpper()
                    };
                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        public Boolean AtivarMembrosDoContrato(long tb012Id)
        {
         
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("update ");
                sSql.Append(" TB013_Pessoa ");
                sSql.Append(" set ");
                sSql.Append(" TB013_Status = ");
                sSql.Append(1);
                sSql.Append("where  TB012_id = ");
                sSql.Append(tb012Id);
                sSql.Append("and  TB013_Status = 0");
               
                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
                command.ExecuteReader();
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;
        }
        public List<PessoaController> CorporativoPessoasNaoAtivadas(long tb012Id)
        {
            var retornoList = new List<PessoaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT TB012_Corporativo, TB013_CorporativoAtivado, TB012_id, TB013_id, TB013_NomeCompleto ");
                sSql.Append(" FROM            dbo.TB013_Pessoa ");
                sSql.Append(" WHERE TB012_Corporativo =  ");
                sSql.Append(tb012Id);
                sSql.Append(" AND TB013_CorporativoAtivado = 0  ");
               
                sSql.Append(" ORDER BY TB012_id, TB013_id ");

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PessoaController
                    {
                        Contato = new ContatoController(),
                        TB013_id = Convert.ToInt64(reader["TB013_id"].ToString()),
                        TB012_Id = Convert.ToInt64(reader["TB012_id"].ToString()),
                        TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd().ToUpper()
                    };

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        /// <summary>
        /// Descrição:  Atualizar cadastro Pessoa Fisica
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       28/11/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool PessoaFisicaUpdate(PessoaController pessoa)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" UPDATE TB013_Pessoa SET ");
                sSql.Append(" TB013_IdProtheus= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_id.ToString());
                sSql.Append("'");
                sSql.Append(" ,TB013_CPFCNPJ=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_CPFCNPJ.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Replace(" ", "").Replace("_", "").Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_NomeCompleto= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_NomeCompleto.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_NomeExibicao= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_NomeExibicao.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(",TB013_Sexo=");
                sSql.Append(Convert.ToInt16(pessoa.TB013_SexoS));
                sSql.Append(" ,TB013_DataNascimento= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_DataNascimento.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ,TB004_Cep=");
                sSql.Append(pessoa.TB004_Cep.Replace("-", ""));
                sSql.Append(" ,TB006_id= ");
                sSql.Append(pessoa.Municipio.TB006_id);
                sSql.Append(" ,TB013_Logradouro= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Logradouro.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(",TB013_Numero= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Numero.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_Bairro= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Bairro.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_Complemento=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_Complemento.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_MaeNome=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_MaeNome.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_PaiNome=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_PaiNome.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(" ,TB013_MaeDataNascimento=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_MaeDataNascimento.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ,TB013_PaiDataNascimento=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_PaiDataNascimento.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ,TB013_CartaoSolicitado= ");
                sSql.Append(pessoa.TB013_CartaoSolicitado);
                sSql.Append(" ,TB012_Corporativo= ");
                sSql.Append(pessoa.TB012_Corporativo);
                sSql.Append(" ,TB012_Parceiro= ");
                sSql.Append(pessoa.TB012_Parceiro);
                sSql.Append(" ,TB013_AlteradoEm=");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(" ,TB013_AlteradoPor= ");
                sSql.Append(pessoa.TB013_AlteradoPor);
                sSql.Append(" where TB013_id = ");
                sSql.Append(pessoa.TB013_id);

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
        public long Senha  (long TB013_id)
        {
            long retorno =0;
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("SELECT TB013_id ");
                sSql.Append("FROM dbo.TB033_PortalUsuario ");
                sSql.Append("WHERE  TB013_id = ");
         
                sSql.Append(TB013_id);
               

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno = Convert.ToInt64(reader["TB013_id"]);
                 
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
        public bool cadastrarSenha(PessoaController pessoa)
        {
            try
            {
                CriptografiaDAO Cript = new CriptografiaDAO();


                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("INSERT INTO ");
                sSql.Append(" TB033_PortalUsuario ");
                sSql.Append(" ( ");
                sSql.Append(" TB013_id ");
                sSql.Append(" , TB033_Senha ");
                sSql.Append(" , TB033_CadastradoEm ");
                sSql.Append(" , TB033_CadastradoPor ");
                sSql.Append(" , TB033_AlteradoEm ");
                sSql.Append(" , TB033_AlteradoPor ");
                sSql.Append("  ) VALUES( ");
                sSql.Append(pessoa.TB013_id);
                sSql.Append(" , ");
                sSql.Append("'");
                sSql.Append(Cript.Encrypt(pessoa.TB033_Senha));
                sSql.Append("'");
                sSql.Append(" , ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(" , ");
                sSql.Append(pessoa.TB013_CadastradoPor);
                sSql.Append(" , ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(" , ");
                sSql.Append(pessoa.TB013_AlteradoPor);
                sSql.Append("  ) SELECT SCOPE_IDENTITY()");
                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
                command.ExecuteReader();
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }
        public bool alterarSenha(PessoaController pessoa)
        {
            try
            {
                CriptografiaDAO Cript = new CriptografiaDAO();

                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append("update ");
                sSql.Append(" TB033_PortalUsuario ");
                sSql.Append(" set ");
                sSql.Append(" TB033_Senha = ");
                sSql.Append("'");
                sSql.Append(Cript.Encrypt(pessoa.TB033_Senha));
                sSql.Append("'");
                sSql.Append(" ,TB033_AlteradoEm = ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(" ,TB033_AlteradoPor = ");
                sSql.Append(Convert.ToInt64(pessoa.TB013_AlteradoPor));
                sSql.Append(" where TB013_id =  ");
                sSql.Append(Convert.ToInt64(pessoa.TB013_id));
               
                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
                command.ExecuteReader();
                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return true;

        }
        public List<PessoaController> AcessosContrato(long tb012Id)
        {
            var retornoList = new List<PessoaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT ");
                sSql.Append(" dbo.TB040_TB013_TB012.TB040_id");
                sSql.Append(" , dbo.TB040_TB013_TB012.TB012_id");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_id");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto");
                sSql.Append(" , dbo.View_Contato_Tipo3.Expr2 AS Email");
                sSql.Append(" FROM            ");
                sSql.Append(" dbo.TB040_TB013_TB012 ");
                sSql.Append(" INNER JOIN");
                sSql.Append(" dbo.TB013_Pessoa ");
                sSql.Append(" ON ");
                sSql.Append(" dbo.TB040_TB013_TB012.TB013_id = dbo.TB013_Pessoa.TB013_id ");
                sSql.Append(" LEFT OUTER JOIN");
                sSql.Append(" dbo.View_Contato_Tipo3 ");
                sSql.Append(" ON ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo3.TB013_id");
                sSql.Append(" WHERE");
                sSql.Append(" dbo.TB040_TB013_TB012.TB012_id = ");
                sSql.Append(tb012Id);
                sSql.Append(" ORDER BY dbo.TB013_Pessoa.TB013_NomeCompleto");

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PessoaController
                    {
                        TB040_id = Convert.ToInt64(reader["TB040_id"].ToString()),
                        TB013_id = Convert.ToInt64(reader["TB013_id"].ToString()),
                        TB009_Contato = reader["Email"] is DBNull ? "---" : reader["Email"].ToString(),
                        TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd().ToUpper()
                    };

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        public long VerificarAcesso(long TB013_id, long TB012_id)
        {
            long retorno = 0;
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" SELECT * ");
                sSql.Append(" FROM dbo.TB040_TB013_TB012 ");
                sSql.Append(" WHERE  TB013_id = ");
                sSql.Append(TB013_id);
                sSql.Append(" and  TB012_id = ");
                sSql.Append(TB012_id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno = Convert.ToInt64(reader["TB040_id"]);
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
        public long AcessoLiberar(PessoaController Acesso)
        {
            
            try
            {
                CriptografiaDAO Cript = new CriptografiaDAO();
                SqlConnection con           = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sTB013Insert  = new StringBuilder();
                StringBuilder sTB009Insert        = new StringBuilder();


                if (Acesso.TB013_id==0)
                {
                    SqlCommand cmdTB013Insert = con.CreateCommand();
                    SqlCommand cmdTB009Insert = con.CreateCommand();

                    sTB013Insert.Append(" INSERT INTO  ");
                    sTB013Insert.Append(" TB013_Pessoa ");
                    sTB013Insert.Append(" ( ");
                    sTB013Insert.Append(" TB013_Tipo ");
                    sTB013Insert.Append(" ,TB013_CPFCNPJ ");
                    sTB013Insert.Append(" ,TB013_ListaNegra ");
                    sTB013Insert.Append(" ,TB013_NomeCompleto ");
                    sTB013Insert.Append(" ,TB013_NomeExibicao ");
                    sTB013Insert.Append(" ,TB013_Sexo ");
                    sTB013Insert.Append(" ,TB013_RG ");
                    sTB013Insert.Append(" ,TB013_RGOrgaoEmissor ");
                    sTB013Insert.Append(" ,TB013_DataNascimento ");
                    sTB013Insert.Append(" ,TB013_DeclaroSerMaiorCapaz ");
                    sTB013Insert.Append(" ,TB004_Cep ");
                    sTB013Insert.Append(" ,TB006_id ");
                    sTB013Insert.Append(" ,TB013_Logradouro ");
                    sTB013Insert.Append(" ,TB013_Numero ");
                    sTB013Insert.Append(" ,TB013_Bairro ");
                    sTB013Insert.Append(" ,TB013_Complemento ");
                    sTB013Insert.Append(" ,TB013_CadastradoEm ");
                    sTB013Insert.Append(" ,TB013_CadastradoPor ");
                    sTB013Insert.Append(" ,TB013_AlteradoEm ");
                    sTB013Insert.Append(" ,TB013_AlteradoPor ");
                    sTB013Insert.Append(" ,TB013_CodigoDependente ");
                    sTB013Insert.Append(" ,TB013_Status ");
                    sTB013Insert.Append(" ,TB013_CarteirinhaStatus ");
                    sTB013Insert.Append(" ,TB012_EraContezino ");
                    sTB013Insert.Append(" ,TB013_MaeNome ");
                    sTB013Insert.Append(" ,TB013_MaeDataNascimento ");
                    sTB013Insert.Append(" ,TB013_PaiNome ");
                    sTB013Insert.Append(" ,TB013_PaiDataNascimento ");
                    sTB013Insert.Append(" ) VALUES ( ");               
                    sTB013Insert.Append(Acesso.TB013_TipoS);
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB013_CPFCNPJ.Replace(".","").Replace(",", "").Replace("-", "").Replace("/", "").Trim());
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append(Acesso.TB013_ListaNegra);
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB013_NomeCompleto.TrimEnd());
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB013_NomeExibicao);
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append(Acesso.TB013_SexoS);
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB013_RG);
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB013_RGOrgaoEmissor);
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB013_DataNascimento.ToString("MM/dd/yyyy"));
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append(Acesso.TB013_DeclaroSerMaiorCapaz);
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB004_Cep.Replace("-","").Trim());
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append(Acesso.Municipio.TB006_id);
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB013_Logradouro.Replace("'","/").Replace("*", " ").Replace("&", "E").TrimEnd());
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB013_Numero.TrimEnd());
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB013_Bairro.Replace("'", "/").Replace("*", " ").Replace("&", "E").TrimEnd());
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB013_Complemento.Replace("'", "/").Replace("*", " ").Replace("&", "E").TrimEnd());
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append(Acesso.TB013_CadastradoPor);
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append(Acesso.TB013_AlteradoPor);
                    sTB013Insert.Append(" ,");                    
                    sTB013Insert.Append(Acesso.TB013_CodigoDependente);                    
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append(Acesso.TB013_StatusS);
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append(0);
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append(0);
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB013_MaeNome);
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB013_MaeDataNascimento.ToString("MM/dd/yyyy"));
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB013_PaiNome);
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ,");
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(Acesso.TB013_PaiDataNascimento.ToString("MM/dd/yyyy"));
                    sTB013Insert.Append("'");
                    sTB013Insert.Append(" ) SELECT SCOPE_IDENTITY() ");

                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    cmdTB013Insert.CommandText = sTB013Insert.ToString();
                    
                    cmdTB013Insert.Transaction = tran;
                    Acesso.TB013_id = Convert.ToInt64(cmdTB013Insert.ExecuteScalar());
                    tran.Commit();
                    con.Close();

                    /*Cadastrar E-mail de contato*/
                    sTB009Insert.Append("INSERT INTO  ");
                    sTB009Insert.Append("TB009_Contato  ");
                    sTB009Insert.Append("( ");
                    sTB009Insert.Append("TB013_id ");
                    sTB009Insert.Append(", TB009_Tipo ");
                    sTB009Insert.Append(", TB009_Contato ");
                    sTB009Insert.Append(", TB009_ExibirPortal ");
                    sTB009Insert.Append(", TB009_Nota ");
                    sTB009Insert.Append(", TB009_CadastradoEm ");
                    sTB009Insert.Append(", TB009_CadastradoPor ");
                    sTB009Insert.Append(", TB009_AlteradoEm ");
                    sTB009Insert.Append(", TB009_AlteradoPor ");
                    sTB009Insert.Append(", TB020_id ");
                    sTB009Insert.Append(") VALUES ( ");
                    sTB009Insert.Append(Acesso.TB013_id);
                    sTB009Insert.Append(", ");
                    sTB009Insert.Append(Acesso.Contato.TB009_TipoS);
                    sTB009Insert.Append(", ");
                    sTB009Insert.Append("'");
                    sTB009Insert.Append(Acesso.Contato.TB009_Contato);
                    sTB009Insert.Append("'");
                    sTB009Insert.Append(", ");
                    sTB009Insert.Append(Acesso.Contato.TB009_ExibirPortal);
                    sTB009Insert.Append(", ");
                    sTB009Insert.Append("'");
                    sTB009Insert.Append(Acesso.Contato.TB009_Nota);
                    sTB009Insert.Append("'");
                    sTB009Insert.Append(", ");
                    sTB009Insert.Append("'");
                    sTB009Insert.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                    sTB009Insert.Append("'");
                    sTB009Insert.Append(", ");
                    sTB009Insert.Append(Acesso.Contato.TB009_CadastradoPor);
                    sTB009Insert.Append(", ");
                    sTB009Insert.Append("'");
                    sTB009Insert.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                    sTB009Insert.Append("'");
                    sTB009Insert.Append(", ");
                    sTB009Insert.Append(Acesso.Contato.TB009_AlteradoPor);
                    sTB009Insert.Append(", ");
                    sTB009Insert.Append(Acesso.Contato.TB020_id);
                    sTB009Insert.Append(")  ");

                    con.Open();
                    cmdTB009Insert.CommandText = sTB009Insert.ToString();

                    cmdTB009Insert.Transaction = tran;
                    cmdTB009Insert.ExecuteScalar();

                    con.Close();



                }

                long TB040_id = VerificarAcesso(Acesso.TB013_id, Acesso.TB012_Id);
                StringBuilder sTB040 = new StringBuilder();

                if (TB040_id==0)
                {
                    sTB040.Append(" INSERT INTO  ");
                    sTB040.Append(" TB040_TB013_TB012 ");
                    sTB040.Append(" ( ");
                    sTB040.Append(" TB013_id ");
                    sTB040.Append(" ,TB012_id ");
                    sTB040.Append(" ) ");
                    sTB040.Append(" VALUES ");
                    sTB040.Append(" ( ");
                    sTB040.Append(Acesso.TB013_id);
                    sTB040.Append(",");
                    sTB040.Append(Acesso.TB012_Id);
                    sTB040.Append(" ) SELECT SCOPE_IDENTITY()");
                }
 
                SqlCommand cmdTB040 = con.CreateCommand();


                cmdTB040.CommandText        = sTB040.ToString();

                con.Open();
                SqlTransaction tranc = con.BeginTransaction();
                try
                {
                    if(TB040_id == 0)
                    {
                        cmdTB040.Transaction = tranc;
                        cmdTB040.ExecuteNonQuery();

                    }

                    tranc.Commit();
                }
                catch (SqlException)
                {
                    tranc.Rollback();
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return Acesso.TB013_id;

        }
        public PessoaController AcessoPessoaFiltrar(long tb013Id)
        {
            PessoaController retorno = new PessoaController();
            retorno.Contato = new ContatoController();
            retorno.Municipio = new MunicipioController();

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" SELECT ");
                sSql.Append(" dbo.TB040_TB013_TB012.TB040_id ");
                sSql.Append(",dbo.TB040_TB013_TB012.TB013_id ");
                sSql.Append(",dbo.TB040_TB013_TB012.TB012_id ");
                sSql.Append(",dbo.TB013_Pessoa.TB013_NomeCompleto ");
                sSql.Append(",dbo.TB013_Pessoa.TB013_CPFCNPJ ");
                sSql.Append(",dbo.View_Contato_Tipo3.Expr1 AS IdContato ");
                sSql.Append(",dbo.View_Contato_Tipo3.Expr2 AS Email ");
                sSql.Append(",dbo.TB013_Pessoa.TB006_id ");
                sSql.Append(",dbo.TB013_Pessoa.TB013_Sexo ");
                sSql.Append(",dbo.TB013_Pessoa.TB013_DataNascimento ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB040_TB013_TB012  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB013_Pessoa ON dbo.TB040_TB013_TB012.TB013_id = dbo.TB013_Pessoa.TB013_id ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.View_Contato_Tipo3 ON dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo3.TB013_id ");
                sSql.Append(" WHERE ");
                sSql.Append(" dbo.TB040_TB013_TB012.TB013_id =  ");
                sSql.Append(tb013Id);
                sSql.Append(" ORDER BY ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_NomeCompleto ");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB013_id                = Convert.ToInt64(reader["TB013_id"]);
                    retorno.TB040_id                = Convert.ToInt64(reader["TB040_id"]);
                    retorno.TB013_CPFCNPJ           = reader["TB013_CPFCNPJ"] is DBNull ? "SEM CPF" : Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"000\.000\.000\-00");
                    retorno.TB013_NomeCompleto      = reader["TB013_NomeCompleto"].ToString().Trim().ToUpper();
                    retorno.TB013_DataNascimento    = Convert.ToDateTime(reader["TB013_DataNascimento"]);
                    retorno.TB013_SexoS             = Convert.ToString(reader["TB013_Sexo"]);
                    retorno.Municipio.TB006_id      = Convert.ToInt64(reader["TB006_id"]);
                    retorno.Contato.TB009_id        = Convert.ToInt64(reader["IdContato"]);
                    retorno.Contato.TB009_Contato   = reader["Email"].ToString();

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
        public bool AtualizarDadosPessoaAcesso(PessoaController pessoa)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" UPDATE TB013_Pessoa SET ");
                sSql.Append(" TB013_IdProtheus= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_id.ToString());
                sSql.Append("'");
                sSql.Append(" ,TB013_NomeCompleto= ");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_NomeCompleto.ToUpper().Replace("'", " /").Replace("@", "").Replace("*", "").Replace("%", "").Trim());
                sSql.Append("'");
                sSql.Append(",TB013_Sexo=");
                sSql.Append(Convert.ToInt16(pessoa.TB013_SexoS));
                sSql.Append(" ,TB013_DataNascimento=");
                sSql.Append("'");
                sSql.Append(pessoa.TB013_DataNascimento.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" ,TB013_AlteradoEm=");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(" ,TB013_AlteradoPor= ");
                sSql.Append(pessoa.TB013_AlteradoPor);
                sSql.Append(" where TB013_id = ");
                sSql.Append(pessoa.TB013_id);

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
        public bool acessoVincular(PessoaController Acesso)
        {
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sTB040 = new StringBuilder();
       
                    sTB040.Append(" INSERT INTO  ");
                    sTB040.Append(" TB040_TB013_TB012 ");
                    sTB040.Append(" ( ");
                    sTB040.Append(" TB013_id ");
                    sTB040.Append(" ,TB012_id ");
                    sTB040.Append(" ) ");
                    sTB040.Append(" VALUES ");
                    sTB040.Append(" ( ");
                    sTB040.Append(Acesso.TB013_id);
                    sTB040.Append(",");
                    sTB040.Append(Acesso.TB012_Id);
                    sTB040.Append(" ) SELECT SCOPE_IDENTITY()");
          
                    SqlCommand cmdTB040 = con.CreateCommand();
                    cmdTB040.CommandText = sTB040.ToString();

                    con.Open();
                    SqlTransaction tranc = con.BeginTransaction();
                try
                {
                   
                   cmdTB040.Transaction = tranc;
                   cmdTB040.ExecuteNonQuery();

                   tranc.Commit();
                }
                catch (SqlException)
                {
                    tranc.Rollback();
                    throw;
                }
                finally
                {
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
        public List<PessoaController> dependentesNaoAtivos(long tb012Id)
        {
            var retornoList = new List<PessoaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT  ");
                sSql.Append(" TB012_id ");
                sSql.Append(" , TB013_id ");
                sSql.Append(" , TB013_NomeCompleto ");
                sSql.Append(" , TB013_Status ");
                sSql.Append(" , TB013_CodigoDependente ");
                sSql.Append(" FROM  ");
                sSql.Append("  dbo.TB013_Pessoa ");
                sSql.Append(" WHERE ");
                sSql.Append(" TB013_Status <> 1 ");
                sSql.Append(" AND ");
                sSql.Append(" TB013_CodigoDependente <> 1001 ");
                sSql.Append(" AND ");
                sSql.Append(" TB012_id =  ");
                sSql.Append(tb012Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PessoaController
                    {
                        
                        TB013_id = Convert.ToInt64(reader["TB013_id"].ToString()),
                        TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd().ToUpper()
                    };

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }

        /// <summary>
        /// Descrição:  Listar pessoas do corporativo
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       16/01/2018
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<PessoaController> corporativoPessoas()
        {
            var retornoList = new List<PessoaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT ");
                sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_id ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_Corporativo ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_Inicio ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_Fim ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_Status ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_id ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Tipo ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Cartao ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_CodigoDependente ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_CPFCNPJ ");
                sSql.Append(" , ISNULL(dbo.TB013_Pessoa.TB013_ListaNegra, 0) AS TB013_ListaNegra ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeExibicao ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Sexo ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_RG ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_RGOrgaoEmissor ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_DataNascimento ");
                sSql.Append(" , dbo.TB013_Pessoa.TB004_Cep ");
                sSql.Append(" , dbo.TB006_Municipio.TB006_id ");
                sSql.Append(" , dbo.TB006_Municipio.TB006_Codigo ");
                sSql.Append(" , dbo.TB006_Municipio.TB006_IBGE ");
                sSql.Append(" , dbo.TB006_Municipio.TB006_Municipio ");
                sSql.Append(" , dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" , dbo.TB005_Estado.TB005_Codigo ");
                sSql.Append(" , dbo.TB005_Estado.TB005_Sigla ");
                sSql.Append(" , dbo.TB005_Estado.TB005_Estado ");
                sSql.Append(" , dbo.TB003_Pais.TB003_id ");
                sSql.Append(" , dbo.TB003_Pais.TB003_DDI ");
                sSql.Append(" , dbo.TB003_Pais.TB003_Pais ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Logradouro ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Numero ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Bairro ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Complemento ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Status ");
                sSql.Append(" , ISNULL(dbo.TB013_Pessoa.TB013_Matricula, 0) AS TB013_Matricula ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_CorporativoAtivado ");
                sSql.Append(" , ISNULL(dbo.TB013_Pessoa.TB013_MaeNome, 'NÃO INFORMADO') AS TB013_MaeNome ");
                sSql.Append(" , ISNULL(dbo.TB013_Pessoa.TB013_MaeDataNascimento, '01/01/1900') AS TB013_MaeDataNascimento ");
                sSql.Append(" , ISNULL(dbo.TB013_Pessoa.TB013_PaiNome, 'NÃO INFORMADO') AS TB013_PaiNome ");
                sSql.Append(" , ISNULL(dbo.TB013_Pessoa.TB013_PaiDataNascimento, '01/01/1900') AS TB013_PaiDataNascimento ");
                sSql.Append(" , ISNULL(dbo.TB013_Pessoa.TB013_CartaoChip, 0) AS TB013_CartaoChip ");
                sSql.Append(" , ISNULL(dbo.TB013_Pessoa.TB013_CartaoChipStatus, -1) AS TB013_CartaoChipStatus ");
                sSql.Append(" , ISNULL(dbo.TB013_Pessoa.TB013_CartaoSolicitado, -1) AS TB013_CartaoSolicitado ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB012_Contratos ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB013_Pessoa ");
                sSql.Append(" ON ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id = dbo.TB013_Pessoa.TB012_id ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio ");
                sSql.Append(" ON ");
                sSql.Append(" dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB005_Estado ");
                sSql.Append(" ON ");
                sSql.Append(" dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB003_Pais ");
                sSql.Append(" ON ");
                sSql.Append(" dbo.TB005_Estado.TB003_Id = dbo.TB003_Pais.TB003_id ");
                sSql.Append(" WHERE ");
                sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato = 4 ");
                sSql.Append(" AND ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_Tipo = 1 ");
                sSql.Append(" ORDER BY ");
                sSql.Append(" dbo.TB012_Contratos.TB012_Corporativo ");
                sSql.Append(" , dbo.TB012_Contratos.TB012_id ");


                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PessoaController
                    {
                        Contrato = new ContratosController
                        {
                            TB012_Id                = Convert.ToInt64(reader["TB012_id"].ToString())
                            ,TB012_Corporativo      = Convert.ToInt64(reader["TB012_Corporativo"].ToString())
                            ,TB012_Inicio           = Convert.ToDateTime(reader["TB012_Inicio"])
                            ,TB012_Fim              = Convert.ToDateTime(reader["TB012_Fim"])
                            ,TB012_StatusS          = reader["TB012_Status"].ToString()
                        }
                        ,TB013_id                   = Convert.ToInt64(reader["TB013_id"].ToString())
                        ,TB013_Cartao               = reader["TB013_Cartao"].ToString().TrimEnd().ToUpper().Trim()
                        ,TB013_CodigoDependente     = Convert.ToInt64( reader["TB013_CodigoDependente"].ToString())
                        ,TB013_CPFCNPJ              = reader["TB013_CPFCNPJ"].ToString().Replace("-","").Replace(".", "").Replace("/", "").Trim()
                        ,TB013_ListaNegra           = Convert.ToInt16(reader["TB013_ListaNegra"].ToString())
                        ,TB013_NomeCompleto         = reader["TB013_NomeCompleto"].ToString().TrimEnd()
                        ,TB013_NomeExibicao         = reader["TB013_NomeExibicao"].ToString().TrimEnd()
                        ,TB013_SexoS                = reader["TB013_Sexo"].ToString()
                        ,TB013_RG                   = reader["TB013_RG"].ToString().TrimEnd()
                        ,TB013_RGOrgaoEmissor       = reader["TB013_RGOrgaoEmissor"].ToString().TrimEnd()
                        ,TB013_DataNascimento       = Convert.ToDateTime(reader["TB013_DataNascimento"].ToString())
                        ,TB004_Cep                  = reader["TB004_Cep"].ToString().Replace("-","").Trim()
                        ,TB013_Logradouro           = reader["TB013_Logradouro"].ToString().TrimEnd()
                        ,TB013_Numero               = reader["TB013_Numero"].ToString().TrimEnd()
                        ,TB013_Bairro               = reader["TB013_Bairro"].ToString().TrimEnd()
                        ,TB013_Complemento          = reader["TB013_Complemento"].ToString().TrimEnd()
                        ,TB013_StatusS              = reader["TB013_Status"].ToString()
                        ,TB013_Matricula            = reader["TB013_Matricula"].ToString().TrimEnd()
                        ,TB013_MaeNome              = reader["TB013_MaeNome"].ToString().TrimEnd()
                        ,TB013_MaeDataNascimento    = Convert.ToDateTime(reader["TB013_MaeDataNascimento"].ToString())
                        ,TB013_PaiNome              = reader["TB013_PaiNome"].ToString().TrimEnd()
                        ,TB013_PaiDataNascimento    = Convert.ToDateTime(reader["TB013_PaiDataNascimento"].ToString())
                        ,TB013_CartaoChip           = reader["TB013_CartaoChip"].ToString().TrimEnd()
                        ,TB013_CartaoChipStatusS    = reader["TB013_CartaoChipStatus"].ToString()
                        ,TB013_CartaoSolicitado     = Convert.ToInt16( reader["TB013_CartaoSolicitado"].ToString())
                        
                        , Municipio = new MunicipioController()
                        {
                            TB006_id            = Convert.ToInt64(reader["TB006_id"].ToString())
                            , TB006_Codigo      = reader["TB006_Codigo"].ToString()
                            , TB006_IBGE        = reader["TB006_IBGE"].ToString()
                            , TB006_Municipio   = reader["TB006_Municipio"].ToString().TrimEnd()
                        }

                        ,Estado = new EstadoController()
                        {
                            TB005_Id        = Convert.ToInt64(reader["TB005_Id"].ToString())
                            , TB005_Codigo  = reader["TB005_Codigo"].ToString().TrimEnd()
                            , TB005_Sigla   = reader["TB005_Sigla"].ToString().TrimEnd()
                            , TB005_Estado  = reader["TB005_Estado"].ToString().TrimEnd()
                        }
                        
                        , Pais = new PaisController()
                        {
                            TB003_id        = Convert.ToInt64(reader["TB003_id"].ToString())
                            , TB003_DDI     = reader["TB003_DDI"].ToString().TrimEnd()
                            , TB003_Pais    = reader["TB003_Pais"].ToString().TrimEnd()
                        }
                    };
                   
                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }
        public List<PessoaController> comercialFiltroEmail(long TB006_id,DateTime inicio, DateTime fim, string demaisfiltros )
        {
            var retornoList = new List<PessoaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();


                //sSql.Append("  SELECT ");
                //sSql.Append(" dbo.TB013_Pessoa.TB012_id ");
                //sSql.Append(" , dbo.TB013_Pessoa.TB013_id ");
                //sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto ");
                //sSql.Append(" , dbo.TB006_Municipio.TB006_id ");
                //sSql.Append(" , dbo.TB006_Municipio.TB006_Municipio ");
                //sSql.Append(" , dbo.TB005_Estado.TB005_Sigla ");
                //sSql.Append(" , dbo.TB013_Pessoa.TB013_Sexo ");
                //sSql.Append(" , dbo.TB013_Pessoa.TB013_DataNascimento ");
                //sSql.Append(" , dbo.View_Contato_Tipo3.Expr1 AS IdEmail ");
                //sSql.Append(" , dbo.View_Contato_Tipo1.Expr1 AS IdCelular ");
                //sSql.Append(" , dbo.View_Contato_Tipo2.Expr1 AS IdFixo ");
                //sSql.Append(" , dbo.TB013_Pessoa.TB013_CodigoDependente ");
                //sSql.Append(" FROM ");
                //sSql.Append(" dbo.TB013_Pessoa INNER JOIN ");
                //sSql.Append(" dbo.TB006_Municipio ON dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id INNER JOIN ");
                //sSql.Append(" dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id LEFT OUTER JOIN ");
                //sSql.Append(" dbo.View_Contato_Tipo1 ON dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo1.TB013_id LEFT OUTER JOIN ");
                //sSql.Append(" dbo.View_Contato_Tipo2 ON dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo2.TB013_id LEFT OUTER JOIN ");
                //sSql.Append(" dbo.View_Contato_Tipo3 ON dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo3.TB013_id ");
                //sSql.Append(" WHERE ");
                //sSql.Append(" dbo.TB006_Municipio.TB006_id =  ");
                //sSql.Append(TB006_id);
                //sSql.Append(" AND ");
                //sSql.Append(" NOT(dbo.TB013_Pessoa.TB012_id IS NULL) ");
                //sSql.Append(" AND ");
                //sSql.Append(" dbo.TB013_Pessoa.TB013_DataNascimento  ");
                //sSql.Append(" >= ");
                //sSql.Append("'");
                //sSql.Append(inicio.ToString("MM/dd/yyyy"));
                //sSql.Append("'");
                //sSql.Append(" AND ");
                //sSql.Append(" dbo.TB013_Pessoa.TB013_DataNascimento  ");
                //sSql.Append("<= ");
                //sSql.Append("'");
                //sSql.Append(fim.ToString("MM/dd/yyyy"));
                //sSql.Append("' ");
                //sSql.Append(demaisfiltros);
                //sSql.Append(" ORDER BY ");
                //sSql.Append(" dbo.TB005_Estado.TB005_Sigla ");
                //sSql.Append(" , dbo.TB006_Municipio.TB006_Municipio ");
                //sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto ");

                sSql.Append("  SELECT ");

                sSql.Append(" dbo.TB013_Pessoa.TB012_id ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_id ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto ");
                sSql.Append(" , dbo.TB006_Municipio.TB006_id ");
                sSql.Append(" , dbo.TB006_Municipio.TB006_Municipio ");
                sSql.Append(" , dbo.TB005_Estado.TB005_Sigla ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_Sexo ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_DataNascimento ");
                sSql.Append(" , dbo.View_Contato_Tipo3.Expr1 AS IdEmail ");
                sSql.Append(" , dbo.View_Contato_Tipo1.Expr1 AS IdCelular ");
                sSql.Append(" , dbo.View_Contato_Tipo2.Expr1 AS IdFixo ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_CodigoDependente ");
                sSql.Append(" , dbo.TB039_Mensagem.TB039_id AS Selecionado ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB039_Mensagem LEFT OUTER JOIN ");
                sSql.Append("  dbo.View_Contato_Tipo1 ON dbo.TB039_Mensagem.TB009_id = dbo.View_Contato_Tipo1.Expr1 ");
                sSql.Append(" RIGHT OUTER JOIN ");
                sSql.Append(" dbo.TB013_Pessoa INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio ON dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB005_Estado ON dbo.TB006_Municipio.TB005_Id = dbo.TB005_Estado.TB005_Id ON dbo.View_Contato_Tipo1.TB013_id = dbo.TB013_Pessoa.TB013_id ");
                sSql.Append(" LEFT OUTER JOIN ");
                sSql.Append("  dbo.View_Contato_Tipo2 ON dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo2.TB013_id ");
                sSql.Append(" LEFT OUTER JOIN ");
                sSql.Append(" dbo.View_Contato_Tipo3 ON dbo.TB013_Pessoa.TB013_id = dbo.View_Contato_Tipo3.TB013_id ");
                sSql.Append(" WHERE ");
                sSql.Append(" dbo.TB006_Municipio.TB006_id =  ");
                sSql.Append(TB006_id);
                sSql.Append(" AND ");
                sSql.Append(" NOT(dbo.TB013_Pessoa.TB012_id IS NULL) ");
                sSql.Append(" AND ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_DataNascimento  ");
                sSql.Append(" >= ");
                sSql.Append("'");
                sSql.Append(inicio.ToString("MM/dd/yyyy"));
                sSql.Append("'");
                sSql.Append(" AND ");
                sSql.Append(" dbo.TB013_Pessoa.TB013_DataNascimento  ");
                sSql.Append("<= ");
                sSql.Append("'");
                sSql.Append(fim.ToString("MM/dd/yyyy"));
                sSql.Append("' ");
                sSql.Append(demaisfiltros);
                sSql.Append(" ORDER BY ");
                sSql.Append(" dbo.TB005_Estado.TB005_Sigla ");
                sSql.Append(" , dbo.TB006_Municipio.TB006_Municipio ");
                sSql.Append(" , dbo.TB013_Pessoa.TB013_NomeCompleto ");

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new PessoaController
                    {
                        TB013_id                = Convert.ToInt64(reader["TB013_id"].ToString()),
                        TB012_Id                = Convert.ToInt64(reader["TB012_Id"].ToString()),
                        TB013_NomeCompleto      = reader["TB013_NomeCompleto"].ToString().TrimEnd(),
                        TB013_DataNascimento    = Convert.ToDateTime(reader["TB013_DataNascimento"].ToString()),
                        IdCelular               = reader["IdCelular"]   is DBNull ? 0 : Convert.ToInt64(reader["IdCelular"]),
                        IdFixo                  = reader["IdFixo"]      is DBNull ? 0 : Convert.ToInt64(reader["IdFixo"]),
                        IdEmail                 = reader["IdEmail"]     is DBNull ? 0 : Convert.ToInt64(reader["IdEmail"]),
                        Selecionado             = reader["Selecionado"] is DBNull ? 0 : 1

                        //retorno.TB012_Corporativo = reader["TB012_CorporativoPai"] is DBNull ? 0 : Convert.ToInt64(reader["TB012_CorporativoPai"]);
                    };

                    retornoList.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            return retornoList;
        }

    }
}

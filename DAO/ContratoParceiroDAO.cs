using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace DAO
{
    public class ContratoParceiroDAO
    {
        public ContratosController ContratoParceiroInsert(ContratosController contrato)
        {
            ContratosController retorno = new ContratosController();
            try
            {
                string insertSql = "INSERT INTO TB012_Contratos (TB002_id,TB013_id,TB012_Inicio,TB012_Fim,TB012_AceiteContrato,TB012_DataAceiteContrato,TB012_CadastradoEm,TB012_CadastradorPor,TB012_AlteradoEm,TB012_AlteradoPor,TB012_Status,TB004_Cep,TB006_id,TB012_Logradouro,TB012_Numero,TB012_Bairro,TB012_Complemento,TB012_TipoContrato,TB012_NumeroDaSorte,TB012_CicloContrato,TB012_DiaVencimento) VALUES (@TB002_id,@TB013_id,@TB012_Inicio,@TB012_Fim,@TB012_AceiteContrato,@TB012_DataAceiteContrato,@TB012_CadastradoEm,@TB012_CadastradorPor,@TB012_AlteradoEm,@TB012_AlteradoPor,@TB012_Status,@TB004_Cep,@TB006_id,@TB012_Logradouro,@TB012_Numero,@TB012_Bairro,@TB012_Complemento,@TB012_TipoContrato,@TB012_NumeroDaSorte,@TB012_CicloContrato,@TB012_DiaVencimento) SELECT SCOPE_IDENTITY()";

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand(insertSql, con);

                    command.Parameters.AddWithValue("@TB002_id", contrato.PontoDeVenda.TB002_id);
                    command.Parameters.AddWithValue("@TB013_id", contrato.Titular.TB013_id); //Titular do Contrato
                    command.Parameters.AddWithValue("@TB012_Inicio", contrato.TB012_Inicio);
                    command.Parameters.AddWithValue("@TB012_Fim", contrato.TB012_Fim);
                    command.Parameters.AddWithValue("@TB012_AceiteContrato", contrato.TB012_AceiteContrato);
                    command.Parameters.AddWithValue("@TB012_DataAceiteContrato", contrato.TB012_DataAceiteContrato);
                    command.Parameters.AddWithValue("@TB012_CadastradoEm", DateTime.Now);
                    command.Parameters.AddWithValue("@TB012_CadastradorPor", contrato.TB012_CadastradorPor);
                    command.Parameters.AddWithValue("@TB012_AlteradoEm", DateTime.Now);
                    command.Parameters.AddWithValue("@TB012_AlteradoPor", contrato.TB012_AlteradoPor);
                    command.Parameters.AddWithValue("@TB012_Status", contrato.TB012_StatusS);
                    command.Parameters.AddWithValue("@TB004_Cep", contrato.TB004_Cep.Replace("-", ""));
                    command.Parameters.AddWithValue("@TB006_id", contrato.TB006_id);
                    command.Parameters.AddWithValue("@TB012_Logradouro", contrato.TB012_Logradouro);
                    command.Parameters.AddWithValue("@TB012_Numero", contrato.TB012_Numero);
                    command.Parameters.AddWithValue("@TB012_Bairro", contrato.TB012_Bairro);
                    command.Parameters.AddWithValue("@TB012_Complemento", contrato.TB012_Complemento);
                    command.Parameters.AddWithValue("@TB012_TipoContrato", contrato.TB012_TipoContrato);
                    command.Parameters.AddWithValue("@TB012_NumeroDaSorte", contrato.TB012_NumeroDaSorte);
                    command.Parameters.AddWithValue("@TB012_CicloContrato", contrato.TB012_CicloContrato);
                    command.Parameters.AddWithValue("@TB012_DiaVencimento", contrato.TB012_DiaVencimento);


                    retorno.TB012_Id = Convert.ToInt32(command.ExecuteScalar());

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
        public ContratosController ContratoParceiroSelect(long tb012Id)
        {
            ContratosController retorno = new ContratosController();
            PontoDeVendaController objPontoDeVenda = new PontoDeVendaController();
            PessoaController objTitular = new PessoaController();

            retorno.PontoDeVenda = objPontoDeVenda;
            retorno.Titular = objTitular;
            retorno.Unidade = new UnidadeController();
            retorno.Unidade.Pais = new PaisController();
            retorno.Unidade.Estado = new EstadoController();
            retorno.Unidade.Municipio = new MunicipioController();

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();

                sSql.Append(" SELECT dbo.TB012_Contratos.TB012_CicloContrato, dbo.TB012_Contratos.TB012_DiaVencimento, dbo.TB012_Contratos.TB012_id, dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB012_Contratos.TB002_id, dbo.TB012_Contratos.TB012_Inicio, dbo.TB012_Contratos.TB012_Fim, ");
                sSql.Append(" dbo.TB012_Contratos.TB012_AceiteContrato, dbo.TB012_Contratos.TB012_DataAceiteContrato, dbo.TB012_Contratos.TB012_CadastradoEm, dbo.TB012_Contratos.TB012_CadastradorPor, ");
                sSql.Append(" dbo.TB012_Contratos.TB012_AlteradoEm, dbo.TB012_Contratos.TB012_AlteradoPor, dbo.TB012_Contratos.TB004_Cep AS CEPContrato, TB006_Municipio_Contrato.TB006_id AS MunicipioContrato, ");
                sSql.Append(" TB006_Municipio_Contrato.TB006_Municipio, dbo.TB005_Estado.TB005_Id, dbo.TB005_Estado.TB005_Estado, dbo.TB003_Pais.TB003_id, dbo.TB003_Pais.TB003_Pais, ");
                sSql.Append(" dbo.TB012_Contratos.TB012_Logradouro, dbo.TB012_Contratos.TB012_Numero, dbo.TB012_Contratos.TB012_Bairro, dbo.TB012_Contratos.TB012_Complemento, ");
                sSql.Append(" dbo.TB012_Contratos.TB012_InformacoesPortal, dbo.TB012_Contratos.TB012_ContratoCancelarMotivo, dbo.TB012_Contratos.TB012_ContratoCancelarDescricao, dbo.TB012_Contratos.TB012_Status, ");
                sSql.Append(" dbo.TB012_Contratos.TB013_id,   dbo.TB020_Unidades.TB020_CategoriaExibicao,                   dbo.TB020_Unidades.TB020_Matriz, dbo.TB020_Unidades.TB020_Desconto, dbo.TB020_Unidades.TB006_id, dbo.TB006_Municipio.TB006_id AS Unidade_TB006_id, ");
                sSql.Append(" TB005_Estado_1.TB005_Id AS Unidade_TB005_Id, TB005_Estado_1.TB003_Id AS Unidade_TB003_Id, dbo.TB020_Unidades.TB020_TipoPessoa");
                sSql.Append(" FROM dbo.TB006_Municipio AS TB006_Municipio_Contrato INNER JOIN");
                sSql.Append(" dbo.TB012_Contratos ON TB006_Municipio_Contrato.TB006_id = dbo.TB012_Contratos.TB006_id INNER JOIN");
                sSql.Append(" dbo.TB005_Estado ON TB006_Municipio_Contrato.TB005_Id = dbo.TB005_Estado.TB005_Id INNER JOIN");
                sSql.Append(" dbo.TB003_Pais ON dbo.TB005_Estado.TB003_Id = dbo.TB003_Pais.TB003_id INNER JOIN");
                sSql.Append(" dbo.TB020_Unidades ON dbo.TB012_Contratos.TB012_id = dbo.TB020_Unidades.TB012_id INNER JOIN");
                sSql.Append(" dbo.TB006_Municipio ON dbo.TB020_Unidades.TB006_id = dbo.TB006_Municipio.TB006_id INNER JOIN");
                sSql.Append(" dbo.TB005_Estado AS TB005_Estado_1 ON dbo.TB006_Municipio.TB005_Id = TB005_Estado_1.TB005_Id");
                sSql.Append(" WHERE dbo.TB012_Contratos.TB012_id = ");
                sSql.Append(tb012Id);
                sSql.Append("  AND dbo.TB020_Unidades.TB020_Matriz = 1");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    retorno.TB012_Id                    = Convert.ToInt64(reader["TB012_Id"]);
                    retorno.TB012_Inicio                = Convert.ToDateTime(reader["TB012_Inicio"]);
                    retorno.TB012_Fim                   = Convert.ToDateTime(reader["TB012_Fim"]);
                    retorno.TB012_StatusS               = reader["TB012_Status"].ToString();
                    retorno.TB004_Cep                   = reader["CEPContrato"].ToString();
                    retorno.TB012_Logradouro            = reader["TB012_Logradouro"].ToString();
                    retorno.TB012_Numero                = reader["TB012_Numero"].ToString();
                    retorno.TB012_Bairro                = reader["TB012_Bairro"].ToString();
                    retorno.TB012_Complemento           = reader["TB012_Complemento"].ToString();
                    retorno.TB012_DiaVencimento         = reader["TB012_DiaVencimento"] is DBNull ? 5 : Convert.ToInt16(reader["TB012_DiaVencimento"]);
                    retorno.TB012_CicloContrato         = reader["TB012_CicloContrato"] is DBNull ? retorno.TB012_Inicio.Month.ToString() + retorno.TB012_Inicio.Year.ToString() : reader["TB012_CicloContrato"].ToString();
                    var bojPontoDeVenda                 = new PontoDeVendaController();
                    retorno.PontoDeVenda                = bojPontoDeVenda;
                    retorno.PontoDeVenda.TB002_id       = Convert.ToInt64(reader["TB002_id"]);
                    var objPais                         = new PaisController();
                    retorno.Pais                        = objPais;
                    retorno.Pais.TB003_id               = Convert.ToInt64(reader["TB003_id"]);
                    var objEstado                       = new EstadoController();
                    retorno.Estado                      = objEstado;
                    retorno.Estado.TB005_Id             = Convert.ToInt64(reader["TB005_Id"]);

                    var objMunicipio                    = new MunicipioController();
                    retorno.Municipio                   = objMunicipio;
                    retorno.Municipio.TB006_id          = Convert.ToInt64(reader["MunicipioContrato"]);
                    retorno.Titular                     = new PessoaController { TB013_id = Convert.ToInt64(reader["TB013_id"]) };

                    retorno.Unidade.TB020_TipoPessoa = Convert.ToInt16(reader["TB020_TipoPessoa"]);
                    retorno.Unidade.Pais.TB003_id             = Convert.ToInt64(reader["Unidade_TB003_Id"]);
                    retorno.Unidade.Estado.TB005_Id           = Convert.ToInt64(reader["Unidade_TB005_Id"]);
                    retorno.Unidade.Municipio.TB006_id        = Convert.ToInt64(reader["Unidade_TB006_id"]);
                    retorno.Unidade.TB020_CategoriaExibicao   = reader["TB020_CategoriaExibicao"].ToString().TrimEnd();
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
        /// Descrição:  Alterar dados de contato do contrato Parceiro
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       23/05/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool ContratoParceiroAlteracao(ContratosController contratoC)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" UPDATE TB012_Contratos SET ");
                sSql.Append("TB012_Logradouro  = ");
                sSql.Append("'");
                sSql.Append(contratoC.TB012_Logradouro.TrimEnd());
                sSql.Append("'");
                sSql.Append(",TB012_Numero  = ");
                sSql.Append("'");
                sSql.Append(contratoC.TB012_Numero.TrimEnd());
                sSql.Append("'");
                sSql.Append(",TB012_Bairro  = ");
                sSql.Append("'");
                sSql.Append(contratoC.TB012_Bairro.TrimEnd());
                sSql.Append("'");
                sSql.Append(",TB004_Cep  = ");
                sSql.Append("'");
                sSql.Append(contratoC.TB004_Cep.Replace(".", "").Replace(",", "").Replace("-", ""));
                sSql.Append("'");
                sSql.Append(",TB012_Complemento  = ");
                sSql.Append("'");
                sSql.Append(contratoC.TB012_Complemento.TrimEnd());
                sSql.Append("'");
                sSql.Append(",TB012_Status  = ");
                sSql.Append(contratoC.TB012_StatusS);
                sSql.Append(",TB012_DiaVencimento  = ");
                sSql.Append(contratoC.TB012_DiaVencimento);
                sSql.Append(" WHERE TB012_id =");
                sSql.Append(contratoC.TB012_Id);

                using (var con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    var myCommand = new SqlCommand(sSql.ToString(), con);
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
        public ContratosController contratoParceiroDadosParcela(long tb012Id)
        {
            var retorno = new ContratosController { Unidade = new UnidadeController() };

            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT   ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id,   ");
                sSql.Append(" dbo.TB012_Contratos.TB012_DiaVencimento, ");
                sSql.Append(" dbo.TB012_Contratos.TB002_id, ");
                sSql.Append(" dbo.TB012_Contratos.TB012_CicloContrato, ");
                sSql.Append(" dbo.TB020_Unidades.TB020_id,  ");
                sSql.Append(" dbo.TB020_Unidades.TB020_Matriz ");
                sSql.Append(" FROM  ");
                sSql.Append(" dbo.TB012_Contratos  ");
                sSql.Append(" INNER JOIN ");
                sSql.Append(" dbo.TB020_Unidades ");
                sSql.Append(" ON  ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id = dbo.TB020_Unidades.TB012_id ");
                sSql.Append(" WHERE ");
                sSql.Append(" dbo.TB020_Unidades.TB020_Matriz = 1 ");
                sSql.Append(" AND ");
                sSql.Append(" dbo.TB012_Contratos.TB012_id =  ");
                sSql.Append(tb012Id);

                var command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    retorno.TB002_id = Convert.ToInt64(reader["TB002_id"]);
                    retorno.Unidade.TB020_id = Convert.ToInt64(reader["TB020_id"]);
                    retorno.TB012_CicloContrato = reader["TB012_CicloContrato"].ToString();
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
    }
}
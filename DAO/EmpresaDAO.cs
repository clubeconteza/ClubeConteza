using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class EmpresaDao
    {

        public EmpresaController Empresa(long tb001Id)
        {
            var retorno = new EmpresaController();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append("SELECT ");
                sSql.Append(" dbo.TB001_Empresa.TB001_id, dbo.TB001_Empresa.TB001_EMatriz, dbo.TB001_Empresa.TB001_Matriz, dbo.TB001_Empresa.TB001_RazaoSocial, dbo.TB001_Empresa.TB001_NomeFantasia,  ");
                sSql.Append(" dbo.TB001_Empresa.TB001_CNPJ, dbo.TB001_Empresa.TB001_Cep, dbo.TB006_Municipio.TB006_Municipio, dbo.TB006_Municipio.TB006_Codigo, dbo.TB005_Estado.TB005_Estado, ");
                sSql.Append(" dbo.TB005_Estado.TB005_Sigla, dbo.TB001_Empresa.TB001_Logradouro, dbo.TB001_Empresa.TB001_Numero, dbo.TB001_Empresa.TB001_Complemento, dbo.TB001_Empresa.TB001_Logo, ");
                sSql.Append(" dbo.TB001_Empresa.TB018_id, dbo.TB001_Empresa.TB001_CadastradoEm, dbo.TB006_Municipio.TB006_id, dbo.TB005_Estado.TB005_Id, dbo.TB001_Empresa.TB001_CadastradoPor, ");
                sSql.Append(" dbo.TB001_Empresa.TB001_AlteradoEm, dbo.TB001_Empresa.TB001_AlteradoPor ");
                sSql.Append(" FROM dbo.TB005_Estado INNER JOIN ");
                sSql.Append(" dbo.TB006_Municipio ON dbo.TB005_Estado.TB005_Id = dbo.TB006_Municipio.TB005_Id INNER JOIN ");
                sSql.Append(" dbo.TB001_Empresa ON dbo.TB006_Municipio.TB006_id = dbo.TB001_Empresa.TB001_Municipio_ID ");
                sSql.Append(" WHERE dbo.TB001_Empresa.TB001_id =  ");
                sSql.Append(tb001Id);

                var command = new SqlCommand(sSql.ToString(), con);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.TB001_id            = Convert.ToInt64(reader["TB001_id"]);
                    retorno.TB001_RazaoSocial   = reader["TB001_RazaoSocial"].ToString();
                    retorno.TB001_NomeFantasia  = reader["TB001_NomeFantasia"].ToString();
                    retorno.TB001_CNPJ          = Convert.ToUInt64(reader["TB001_CNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"00\.000\.000\/0000\-00");
                    retorno.TB001_Cep           = reader["TB001_Cep"].ToString();
                    retorno.TB001_Logradouro    = reader["TB001_Logradouro"].ToString();
                    retorno.TB001_Numero        = reader["TB001_Numero"].ToString();
                    retorno.TB001_Complemento   = reader["TB001_Complemento"].ToString();
                    retorno.Cidade              = reader["TB006_Municipio"].ToString();
                    retorno.UF                  = reader["TB005_Sigla"].ToString(); 
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
        /// Descrição:  Listar empresa
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       30/10/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public List<EmpresaController> Empresas()
        {
            var retornoList = new List<EmpresaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" SELECT * from TB001_Empresa ");
                var command = new SqlCommand(sSql.ToString(), con);
                con.Open();
                var reader = command.ExecuteReader();

                var obj = new EmpresaController();
               
                while (reader.Read())
                {
                    obj.TB001_id = Convert.ToInt64(reader["TB001_id"]);
                    obj.TB001_NomeFantasia = Convert.ToString(reader["TB001_NomeFantasia"]);

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

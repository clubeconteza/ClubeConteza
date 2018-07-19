using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class ContratoDocDAO
    {
        /// <summary>
        /// Descrição:  Verifica se o contrato inicial já esta gravado em TB029_ContratoDoc
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       13/06/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public long VerificaExistenciaDocumento(long TB012_id, int TB029_Tipo)
        {
            Int64 Retorno =0;
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT TB029_Id, TB012_id, TB029_Tipo FROM dbo.TB029_ContratoDoc WHERE TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append(" AND TB029_Tipo = ");
                sSQL.Append(TB029_Tipo);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Retorno = Convert.ToInt64(reader["TB029_Id"]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }


        /// <summary>
        /// Descrição:  Incluir documento original
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       13/06/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public ContratoDocController DocImpressaoInserir(ContratoDocController Documento)
        {
            ContratoDocController Retorno = new ContratoDocController();
            try
            {
                string insertSql = "INSERT INTO TB029_ContratoDoc(TB029_Tipo,TB029_DocImpressao,TB029_DocImpressaoEm,TB029_DocImpressaoPor,TB012_VSContrato,TB012_id) VALUES (@TB029_Tipo,@TB029_DocImpressao,@TB029_DocImpressaoEm,@TB029_DocImpressaoPor,@TB012_VSContrato,@TB012_id) SELECT SCOPE_IDENTITY()";
                string update = "update TB012_Contratos set TB012_Edicao = 0 where TB012_id = @TB012_id";



                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();

                    SqlCommand command1 = new SqlCommand(update, con);
                    command1.Parameters.AddWithValue("@TB012_id", Documento.TB012_id);
                    command1.ExecuteScalar();
                    con.Close();
                }


                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand(insertSql, con);
                    command.Parameters.AddWithValue("@TB029_Tipo", Convert.ToInt16(Documento.TB029_TipoS));
                    command.Parameters.AddWithValue("@TB012_id", Documento.TB012_id);
                    command.Parameters.AddWithValue("@TB029_DocImpressao", Documento.TB029_DocImpressao);
                    command.Parameters.AddWithValue("@TB029_DocImpressaoEm", DateTime.Now);
                    command.Parameters.AddWithValue("@TB029_DocImpressaoPor", Documento.TB029_DocImpressaoPor);
                    command.Parameters.AddWithValue("@TB012_VSContrato", Documento.TB012_VSContrato);
                    Retorno.TB029_Id = Convert.ToInt32(command.ExecuteScalar());

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;

        }

        public ContratoDocController DocImpressaoSelect(ContratoDocController Doc)
        {
            ContratoDocController Retorno = new ContratoDocController();

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append("SELECT TB029_Id, TB029_Tipo, TB029_DocImpressao FROM dbo.TB029_ContratoDoc WHERE TB029_Id =  ");
                sSQL.Append(Doc.TB029_Id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Retorno.TB029_Id = Convert.ToInt64(reader["TB029_Id"]);
                    Retorno.TB029_DocImpressao = (byte[])reader["TB029_DocImpressao"];
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;

        }


        public List<ContratoDocController> DocContratoLista(long TB012_id)
        {
            List<ContratoDocController> Retorno = new List<ContratoDocController>();

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" SELECT * FROM dbo.TB029_ContratoDoc ");
                sSQL.Append(" WHERE dbo.TB029_ContratoDoc.TB012_id =  ");
                sSQL.Append(TB012_id);
                sSQL.Append(" ORDER BY TB012_VSContrato, TB029_Tipo, TB029_Id ");
                SqlCommand command = new SqlCommand(sSQL.ToString(), con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ContratoDocController Obj = new ContratoDocController();

                    Obj.TB029_Id = Convert.ToInt64(reader["TB029_Id"]);
                    Obj.TB029_TipoS = Enum.GetName(typeof(ContratoDocController.TB029_TipoE), Convert.ToInt16(reader["TB029_Tipo"]));
                    Obj.TB012_VSContrato = Convert.ToInt16(reader["TB012_VSContrato"]);
                    Retorno.Add(Obj);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }
    }
}

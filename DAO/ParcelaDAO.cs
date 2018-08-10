using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;

using System.Text;

namespace DAO
{
    public class ParcelaDao
    {

        public short parcelasGeradasContrato(long tb012Id)
        {
            short retorno = 0;
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();

                sSql.Append(" SELECT ");
                sSql.Append(" COUNT(TB016_id) AS TB016_id ");
                sSql.Append(" FROM ");
                sSql.Append(" dbo.TB016_Parcela ");
                sSql.Append(" GROUP BY  ");
                sSql.Append(" TB012_id ");
                sSql.Append(" HAVING  ");
                sSql.Append(" (TB012_id =  ");
                sSql.Append(tb012Id);
                sSql.Append(" ) ");


                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno = Convert.ToInt16(reader["TB016_id"]);
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

        #region Familiar
        public List<ParcelaController> FamiliarListaParcelasContrato(long tb012Id, long tb012CicloContrato, int tb016Status)
        {
            List<ParcelaController> retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();


                sSQL.Append(" SELECT dbo.TB016_Parcela.TB016_ValorPago,  dbo.TB016_Parcela.TB016_NossoNumero,  dbo.TB016_Parcela.TB016_DataPagamento,  dbo.TB016_Parcela.TB012_CicloContrato,dbo.TB016_Parcela.TB016_id, dbo.TB015_Planos.TB015_id, dbo.TB015_Planos.TB015_Plano, dbo.TB016_Parcela.TB016_Parcela, dbo.TB016_Parcela.TB016_TotalParcelas, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Emissao, dbo.TB016_Parcela.TB016_Vencimento, dbo.TB016_Parcela.TB016_Pagador, dbo.TB016_Parcela.TB016_CPFCNPJ, dbo.TB016_Parcela.TB016_Valor, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_ValorPago, dbo.TB016_Parcela.TB016_FormaPagamento, dbo.TB016_Parcela.TB016_DataPagamento, dbo.TB016_Parcela.TB016_Boleto, dbo.TB016_Parcela.TB016_ValorAdesao, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Status,dbo.TB016_Parcela.TB016_Multa,dbo.TB016_Parcela.TB016_Juros");
                sSQL.Append(" FROM dbo.TB016_Parcela INNER JOIN");
                sSQL.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id INNER JOIN");
                sSQL.Append(" dbo.TB015_Planos ON dbo.TB016_Parcela.TB015_id = dbo.TB015_Planos.TB015_id");
                sSQL.Append(" WHERE");
                sSQL.Append(" dbo.TB012_Contratos.TB012_id = ");
                sSQL.Append(tb012Id);
                //sSQL.Append(" AND");
                //sSQL.Append(" dbo.TB012_Contratos.TB012_TipoContrato = 1");
                sSQL.Append(" AND");
                sSQL.Append(" dbo.TB016_Parcela.TB012_CicloContrato = ");
                sSQL.Append(tb012CicloContrato);


                if (tb016Status > -1)
                {
                    sSQL.Append(" AND dbo.TB016_Parcela.TB016_Status <> 3");
                }

                sSQL.Append(" ORDER BY dbo.TB016_Parcela.TB016_id,dbo.TB016_Parcela.TB016_Parcela");



                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB016_NossoNumero = reader["TB016_NossoNumero"] is DBNull ? "------" : reader["TB016_NossoNumero"].ToString();

                    // reader["TB015_Plano"].ToString();
                    //// obj.TB016_ParcelaCodigo     = reader["TB016_ParcelaCodigo"] is DBNull ? "-" : reader["TB016_ParcelaCodigo"].ToString();

                    obj.TB016_Parcela = Convert.ToInt16(reader["TB016_Parcela"]);
                    obj.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);

                    obj.TB016_DataPagamento = reader["TB016_DataPagamento"] is DBNull ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["TB016_DataPagamento"].ToString());


                    obj.TB012_CicloContrato = Convert.ToInt32(reader["TB012_CicloContrato"]);
                    obj.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);
                    obj.TB016_ValorPago = reader["TB016_ValorPago"] is DBNull ? 0 : Convert.ToDouble(reader["TB016_ValorPago"]);
                    obj.TB016_Multa = reader["TB016_Multa"] is DBNull ? 2 : Convert.ToDouble(reader["TB016_Multa"]);
                    obj.TB016_Juros = reader["TB016_Juros"] is DBNull ? 1 : Convert.ToDouble(reader["TB016_Juros"]);


                    //obj.TB016_ValorPago = reader["TB025_ValorCobrado"] is DBNull ? 0 : Convert.ToDouble(reader["TB025_ValorCobrado"].ToString());

                    obj.TB016_FormaPagamentoS = reader["TB016_FormaPagamento"].ToString();

                    if (obj.TB016_FormaPagamentoS == "4")
                    {
                        obj.TB016_FormaPagamentoS = "6";
                    }
                    else
                    {
                        if (obj.TB016_FormaPagamentoS == "5")
                        {
                            obj.TB016_FormaPagamentoS = "6";
                        }
                    }

                    obj.TB016_StatusS = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(reader["TB016_Status"]));
                    obj.TB015_Plano = reader["TB015_Plano"].ToString();


                    retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }


        public List<ParcelaController> FamiliarListaTodasContrato(long tb012Id)
        {
            List<ParcelaController> retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();


                sSQL.Append(" SELECT dbo.TB016_Parcela.TB016_ValorPago,  dbo.TB016_Parcela.TB016_NossoNumero,  dbo.TB016_Parcela.TB016_DataPagamento,  dbo.TB016_Parcela.TB012_CicloContrato,dbo.TB016_Parcela.TB016_id, dbo.TB015_Planos.TB015_id, dbo.TB015_Planos.TB015_Plano, dbo.TB016_Parcela.TB016_Parcela, dbo.TB016_Parcela.TB016_TotalParcelas, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Emissao, dbo.TB016_Parcela.TB016_Vencimento, dbo.TB016_Parcela.TB016_Pagador, dbo.TB016_Parcela.TB016_CPFCNPJ, dbo.TB016_Parcela.TB016_Valor, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_ValorPago, dbo.TB016_Parcela.TB016_FormaPagamento, dbo.TB016_Parcela.TB016_DataPagamento, dbo.TB016_Parcela.TB016_Boleto, dbo.TB016_Parcela.TB016_ValorAdesao, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Status");
                sSQL.Append(" FROM dbo.TB016_Parcela INNER JOIN");
                sSQL.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id INNER JOIN");
                sSQL.Append(" dbo.TB015_Planos ON dbo.TB016_Parcela.TB015_id = dbo.TB015_Planos.TB015_id");
                sSQL.Append(" WHERE");
                sSQL.Append(" dbo.TB012_Contratos.TB012_id = ");
                sSQL.Append(tb012Id);

             

                sSQL.Append(" ORDER BY dbo.TB016_Parcela.TB016_id,dbo.TB016_Parcela.TB016_Parcela");



                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB016_NossoNumero = reader["TB016_NossoNumero"] is DBNull ? "------" : reader["TB016_NossoNumero"].ToString();

                    // reader["TB015_Plano"].ToString();
                    //// obj.TB016_ParcelaCodigo     = reader["TB016_ParcelaCodigo"] is DBNull ? "-" : reader["TB016_ParcelaCodigo"].ToString();

                    obj.TB016_Parcela = Convert.ToInt16(reader["TB016_Parcela"]);
                    obj.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);

                    obj.TB016_DataPagamento = reader["TB016_DataPagamento"] is DBNull ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["TB016_DataPagamento"].ToString());


                    obj.TB012_CicloContrato = Convert.ToInt32(reader["TB012_CicloContrato"]);
                    obj.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);
                    obj.TB016_ValorPago = reader["TB016_ValorPago"] is DBNull ? 0 : Convert.ToDouble(reader["TB016_ValorPago"]);
                    //Convert.ToDouble(reader["TB016_ValorPago"]);


                    //obj.TB016_ValorPago = reader["TB025_ValorCobrado"] is DBNull ? 0 : Convert.ToDouble(reader["TB025_ValorCobrado"].ToString());

                    obj.TB016_FormaPagamentoS = reader["TB016_FormaPagamento"].ToString();

                    if (obj.TB016_FormaPagamentoS == "4")
                    {
                        obj.TB016_FormaPagamentoS = "6";
                    }
                    else
                    {
                        if (obj.TB016_FormaPagamentoS == "5")
                        {
                            obj.TB016_FormaPagamentoS = "6";
                        }
                    }

                    obj.TB016_StatusS = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(reader["TB016_Status"]));
                    obj.TB015_Plano = reader["TB015_Plano"].ToString();


                    retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public bool FamiliarParcelaInsert(List<ParcelaController> Parcelas)
        {
            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

            SqlCommand cmdInsertTB016 = con.CreateCommand();
            SqlCommand cmdInsertTB017 = con.CreateCommand();

            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                int i;
                for (i = 0; i < Parcelas.Count; i++)
                {
                    StringBuilder sInsertTB016 = new StringBuilder();
                    sInsertTB016.Append("INSERT INTO TB016_Parcela ( ");
                    sInsertTB016.Append("TB016_ParcelaCancelamento");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB015_id");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB001_id");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB012_id");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB013_id");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Parcela");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_TotalParcelas");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Emissao");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Vencimento");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Pagador");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_CPFCNPJ");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_PagadorCEP");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_EnderecoPagador");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_PagadorCidade");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_PagadorUF");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_FormaPagamento");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Valor");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_EmitirBoleto");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Beneficiario");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BeneficiarioEndereco");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BeneficiarioCPFCNPJ");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BeneficiarioCidade");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BeneficiarioUF");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_ValorAdesao");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Status");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB012_CicloContrato");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_ValorTitulo");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_DiaVencimento");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_LoteExportacao");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_IOF");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_TipoVencimento");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BoletoDesc1");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BoletoDesc2");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BoletoDesc3");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BoletoDesc4");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BoletoDesc5");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Multa");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Juros");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_EspecieDocumento");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_CadastradoEm");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_CadastradoPor");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_AlteradoEm");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_AlteradoPor");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Entrada");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_TipoSacado");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_ValorBruto");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_ValorOutrosDesconto");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB037_Id");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB037_Comissao");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB002_ComissaoAdesao");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB002_ComissaoMensalidade");
                    sInsertTB016.Append(" ) VALUES (");
                    sInsertTB016.Append(Parcelas[i].TB016_ParcelaCancelamento);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB015_id);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].Empresa.TB001_id);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB012_id);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].Titular.TB013_id);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB033_Parcela);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_TotalParcelas);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_Emissao.ToString("MM/dd/yyyy"));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_Vencimento.ToString("MM/dd/yyyy"));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_Pagador.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_CPFCNPJ.TrimEnd().TrimStart().ToUpper());
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_PagadorCEP.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_EnderecoPagador.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", "").Replace("-", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_PagadorCidade.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_PagadorUF.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_FormaPagamentoS);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_Valor.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_EmitirBoleto);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_Beneficiario);
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BeneficiarioEndereco.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BeneficiarioCPFCNPJ.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BeneficiarioCidade.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BeneficiarioUF.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_ValorAdesao.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("1");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB012_CicloContrato);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_Valor.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_DiaVencimento);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_LoteExportacao);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_IOF.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_TipoVencimento);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BoletoDesc1);
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BoletoDesc2);
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BoletoDesc3);
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BoletoDesc4);
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BoletoDesc5);
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_Multa.ToString("N2").Replace(".", "").Replace(",", "."));                 
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_Juros.ToString("N2").Replace(".", "").Replace(",", "."));          
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_EspecieDocumento.TrimEnd());
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_CadastradoEm.ToString("MM/dd/yyyy hh:mm"));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_CadastradoPor);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_AlteradoEm.ToString("MM/dd/yyyy hh:mm"));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_AlteradoPor);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_Entrada);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(1);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_ValorBruto.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_ValorOutrosDesconto.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB037_Id);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB037_Comissao.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB002_ComissaoAdesao.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB002_ComissaoMensalidade.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(" ) SELECT SCOPE_IDENTITY()");

                    cmdInsertTB016.CommandText = sInsertTB016.ToString();

                    cmdInsertTB016.Transaction = tran;
                    Parcelas[i].TB016_id = Convert.ToInt64(cmdInsertTB016.ExecuteScalar());


                    StringBuilder sInsertTB017 = new StringBuilder();
                    sInsertTB017.Append("INSERT INTO TB017_ParcelaProduto ( ");
                    sInsertTB017.Append("TB017_Tipo");
                    sInsertTB017.Append(",");
                    sInsertTB017.Append("TB016_id");
                    sInsertTB017.Append(",");
                    sInsertTB017.Append("TB017_Item");
                    sInsertTB017.Append(",");
                    sInsertTB017.Append("TB017_ValorUnitario");
                    sInsertTB017.Append(",");
                    sInsertTB017.Append("TB017_ValorDesconto");
                    sInsertTB017.Append(",");
                    sInsertTB017.Append("TB017_ValorFinal");
                    sInsertTB017.Append(" ) VALUES  ");
                    int x;
                    for (x = 0; x < Parcelas[i].ParcelaProduto_L.Count; x++)
                    {
                        if (x > 0)
                        {
                            sInsertTB017.Append(",");
                        }
                        sInsertTB017.Append("(");
                        sInsertTB017.Append(Parcelas[i].ParcelaProduto_L[x].TB017_TipoS.TrimEnd());
                        sInsertTB017.Append(",");
                        sInsertTB017.Append(Parcelas[i].TB016_id);
                        sInsertTB017.Append(",");
                        sInsertTB017.Append("'");
                        sInsertTB017.Append(Parcelas[i].ParcelaProduto_L[x].TB017_Item.TrimEnd());
                        sInsertTB017.Append("'");
                        sInsertTB017.Append(",");
                        sInsertTB017.Append(Parcelas[i].ParcelaProduto_L[x].TB017_ValorUnitario.ToString("N2").Replace(".", "").Replace(",", "."));
                        sInsertTB017.Append(",");
                        sInsertTB017.Append(Parcelas[i].ParcelaProduto_L[x].TB017_ValorDesconto.ToString("N2").Replace(".", "").Replace(",", "."));
                        sInsertTB017.Append(",");
                        sInsertTB017.Append(Parcelas[i].ParcelaProduto_L[x].TB017_ValorFinal.ToString("N2").Replace(".", "").Replace(",", "."));
                        sInsertTB017.Append(")");

                    }


                    cmdInsertTB017.CommandText = sInsertTB017.ToString();
                    cmdInsertTB017.Transaction = tran;
                    cmdInsertTB017.ExecuteNonQuery();
                }

                tran.Commit();
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return true;
        }


        public bool parceiroParcelaInsert(List<ParcelaController> Parcelas)
        {
            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

            SqlCommand cmdInsertTB016 = con.CreateCommand();
            SqlCommand cmdInsertTB017 = con.CreateCommand();

            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                int i;
                for (i = 0; i < Parcelas.Count; i++)
                {
                    StringBuilder sInsertTB016 = new StringBuilder();
                    sInsertTB016.Append("INSERT INTO TB016_Parcela ( ");
                    sInsertTB016.Append("TB016_ParcelaCancelamento");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB015_id");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB001_id");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB012_id");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB013_id");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Parcela");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_TotalParcelas");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Emissao");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Vencimento");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Pagador");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_CPFCNPJ");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_PagadorCEP");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_EnderecoPagador");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_PagadorCidade");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_PagadorUF");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_FormaPagamento");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Valor");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_EmitirBoleto");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Beneficiario");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BeneficiarioEndereco");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BeneficiarioCPFCNPJ");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BeneficiarioCidade");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BeneficiarioUF");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_ValorAdesao");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Status");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB012_CicloContrato");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_ValorTitulo");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_DiaVencimento");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_LoteExportacao");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_IOF");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_TipoVencimento");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BoletoDesc1");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BoletoDesc2");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BoletoDesc3");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BoletoDesc4");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_BoletoDesc5");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Multa");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Juros");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_EspecieDocumento");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_CadastradoEm");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_CadastradoPor");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_AlteradoEm");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_AlteradoPor");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_Entrada");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_TipoSacado");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_ValorBruto");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB016_ValorOutrosDesconto");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB037_Id");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB037_Comissao");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB002_ComissaoAdesao");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("TB002_ComissaoMensalidade");
                    sInsertTB016.Append(" ) VALUES (");
                    sInsertTB016.Append(Parcelas[i].TB016_ParcelaCancelamento);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB015_id);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].Empresa.TB001_id);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB012_id);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].Titular.TB013_id);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB033_Parcela);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_TotalParcelas);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_Emissao.ToString("MM/dd/yyyy"));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_Vencimento.ToString("MM/dd/yyyy"));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_Pagador.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_CPFCNPJ.TrimEnd().TrimStart().ToUpper());
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_PagadorCEP.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_EnderecoPagador.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", "").Replace("-", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_PagadorCidade.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_PagadorUF.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_FormaPagamentoS);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_Valor.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_EmitirBoleto);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_Beneficiario);
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BeneficiarioEndereco.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BeneficiarioCPFCNPJ.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BeneficiarioCidade.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BeneficiarioUF.TrimEnd().TrimStart().ToUpper().Replace("'", "/").Replace("*", "").Replace("%", ""));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_ValorAdesao.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("1");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB012_CicloContrato);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_Valor.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_DiaVencimento);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_LoteExportacao);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_IOF.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_TipoVencimento);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BoletoDesc1);
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BoletoDesc2);
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BoletoDesc3);
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BoletoDesc4);
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_BoletoDesc5);
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_Multa.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_Juros.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_EspecieDocumento.TrimEnd());
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_CadastradoEm.ToString("MM/dd/yyyy hh:mm"));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_CadastradoPor);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(Parcelas[i].TB016_AlteradoEm.ToString("MM/dd/yyyy hh:mm"));
                    sInsertTB016.Append("'");
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_AlteradoPor);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_Entrada);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(1);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_ValorBruto.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB016_ValorOutrosDesconto.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB037_Id);
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB037_Comissao.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB002_ComissaoAdesao.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(",");
                    sInsertTB016.Append(Parcelas[i].TB002_ComissaoMensalidade.ToString("N2").Replace(".", "").Replace(",", "."));
                    sInsertTB016.Append(" ) SELECT SCOPE_IDENTITY()");

                    cmdInsertTB016.CommandText = sInsertTB016.ToString();

                    cmdInsertTB016.Transaction = tran;
                    Parcelas[i].TB016_id = Convert.ToInt64(cmdInsertTB016.ExecuteScalar());


                    StringBuilder sInsertTB017 = new StringBuilder();
                    sInsertTB017.Append("INSERT INTO TB017_ParcelaProduto ( ");
                    sInsertTB017.Append("TB017_Tipo");
                    sInsertTB017.Append(",");
                    sInsertTB017.Append("TB016_id");
                    sInsertTB017.Append(",");
                    sInsertTB017.Append("TB017_Item");
                    sInsertTB017.Append(",");
                    sInsertTB017.Append("TB017_ValorUnitario");
                    sInsertTB017.Append(",");
                    sInsertTB017.Append("TB017_ValorDesconto");
                    sInsertTB017.Append(",");
                    sInsertTB017.Append("TB017_ValorFinal");
                    sInsertTB017.Append(" ) VALUES  ");
                    int x;
                    for (x = 0; x < Parcelas[i].ParcelaProduto_L.Count; x++)
                    {
                        if (x > 0)
                        {
                            sInsertTB017.Append(",");
                        }
                        sInsertTB017.Append("(");
                        sInsertTB017.Append(Parcelas[i].ParcelaProduto_L[x].TB017_TipoS.TrimEnd());
                        sInsertTB017.Append(",");
                        sInsertTB017.Append(Parcelas[i].TB016_id);
                        sInsertTB017.Append(",");
                        sInsertTB017.Append("'");
                        sInsertTB017.Append(Parcelas[i].ParcelaProduto_L[x].TB017_Item.TrimEnd());
                        sInsertTB017.Append("'");
                        sInsertTB017.Append(",");
                        sInsertTB017.Append(Parcelas[i].ParcelaProduto_L[x].TB017_ValorUnitario.ToString("N2").Replace(".", "").Replace(",", "."));
                        sInsertTB017.Append(",");
                        sInsertTB017.Append(Parcelas[i].ParcelaProduto_L[x].TB017_ValorDesconto.ToString("N2").Replace(".", "").Replace(",", "."));
                        sInsertTB017.Append(",");
                        sInsertTB017.Append(Parcelas[i].ParcelaProduto_L[x].TB017_ValorFinal.ToString("N2").Replace(".", "").Replace(",", "."));
                        sInsertTB017.Append(")");

                    }


                    cmdInsertTB017.CommandText = sInsertTB017.ToString();
                    cmdInsertTB017.Transaction = tran;
                    cmdInsertTB017.ExecuteNonQuery();
                }

                tran.Commit();
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return true;
        }

        public List<ParcelaController> parceiroListaParcelasContrato(long tb012Id, long tb012CicloContrato, int tb016Status)
        {
            List<ParcelaController> retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();


                sSQL.Append(" SELECT dbo.TB016_Parcela.TB016_ValorPago,  dbo.TB016_Parcela.TB016_NossoNumero,  dbo.TB016_Parcela.TB016_DataPagamento,  dbo.TB016_Parcela.TB012_CicloContrato,dbo.TB016_Parcela.TB016_id, dbo.TB015_Planos.TB015_id, dbo.TB015_Planos.TB015_Plano, dbo.TB016_Parcela.TB016_Parcela, dbo.TB016_Parcela.TB016_TotalParcelas, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Emissao, dbo.TB016_Parcela.TB016_Vencimento, dbo.TB016_Parcela.TB016_Pagador, dbo.TB016_Parcela.TB016_CPFCNPJ, dbo.TB016_Parcela.TB016_Valor, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_ValorPago, dbo.TB016_Parcela.TB016_FormaPagamento, dbo.TB016_Parcela.TB016_DataPagamento, dbo.TB016_Parcela.TB016_Boleto, dbo.TB016_Parcela.TB016_ValorAdesao, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Status");
                sSQL.Append(" FROM dbo.TB016_Parcela INNER JOIN");
                sSQL.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id INNER JOIN");
                sSQL.Append(" dbo.TB015_Planos ON dbo.TB016_Parcela.TB015_id = dbo.TB015_Planos.TB015_id");
                sSQL.Append(" WHERE");
                sSQL.Append(" dbo.TB012_Contratos.TB012_id = ");
                sSQL.Append(tb012Id);
                sSQL.Append(" AND");
                sSQL.Append(" dbo.TB012_Contratos.TB012_TipoContrato = 2");
                sSQL.Append(" AND");
                sSQL.Append(" dbo.TB016_Parcela.TB012_CicloContrato = ");
                sSQL.Append(tb012CicloContrato);


                if (tb016Status > -1)
                {
                    sSQL.Append(" AND dbo.TB016_Parcela.TB016_Status <> 3");
                }

                sSQL.Append(" ORDER BY dbo.TB016_Parcela.TB016_id,dbo.TB016_Parcela.TB016_Parcela");



                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB016_NossoNumero = reader["TB016_NossoNumero"] is DBNull ? "------" : reader["TB016_NossoNumero"].ToString();

                    // reader["TB015_Plano"].ToString();
                    //// obj.TB016_ParcelaCodigo     = reader["TB016_ParcelaCodigo"] is DBNull ? "-" : reader["TB016_ParcelaCodigo"].ToString();

                    obj.TB016_Parcela = Convert.ToInt16(reader["TB016_Parcela"]);
                    obj.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);

                    obj.TB016_DataPagamento = reader["TB016_DataPagamento"] is DBNull ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["TB016_DataPagamento"].ToString());


                    obj.TB012_CicloContrato = Convert.ToInt32(reader["TB012_CicloContrato"]);
                    obj.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);
                    obj.TB016_ValorPago = reader["TB016_ValorPago"] is DBNull ? 0 : Convert.ToDouble(reader["TB016_ValorPago"]);
                    //Convert.ToDouble(reader["TB016_ValorPago"]);


                    //obj.TB016_ValorPago = reader["TB025_ValorCobrado"] is DBNull ? 0 : Convert.ToDouble(reader["TB025_ValorCobrado"].ToString());

                    obj.TB016_FormaPagamentoS = reader["TB016_FormaPagamento"].ToString();

                    if (obj.TB016_FormaPagamentoS == "4")
                    {
                        obj.TB016_FormaPagamentoS = "6";
                    }
                    else
                    {
                        if (obj.TB016_FormaPagamentoS == "5")
                        {
                            obj.TB016_FormaPagamentoS = "6";
                        }
                    }
                    //obj.TB016_NossoNumero= reader["TB016_NossoNumero"] is DBNull ? "0" : reader["TB016_NossoNumero"].ToString();
                    obj.TB016_StatusS = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(reader["TB016_Status"]));
                    obj.TB015_Plano = reader["TB015_Plano"].ToString();


                    retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
        #endregion

        public List<ParcelaController> ParcelasComissaoNaoProcessadaAdesao()
        {
            var retorno = new List<ParcelaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                //sSql.Append(" SELECT dbo.TB016_Parcela.TB012_id, dbo.TB016_Parcela.TB016_id, dbo.TB017_ParcelaProduto.TB017_id, dbo.TB017_ParcelaProduto.TB017_ValorFinal, dbo.TB017_ParcelaProduto.TB017_Tipo,  ");
                //sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB016_Parcela.TB016_DataPagamento, dbo.TB012_Contratos.TB002_id ");
                //sSql.Append(" FROM dbo.TB016_Parcela INNER JOIN ");
                //sSql.Append(" dbo.TB017_ParcelaProduto ON dbo.TB016_Parcela.TB016_id = dbo.TB017_ParcelaProduto.TB016_id INNER JOIN ");
                //sSql.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id ");
                //sSql.Append(" WHERE(dbo.TB016_Parcela.TB016_ComissaoProcessada = 0) AND(dbo.TB016_Parcela.TB016_Status = 5) AND(dbo.TB016_Parcela.TB016_ValorPago > 0) ");
                //sSql.Append(" ORDER BY dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB016_Parcela.TB012_id, dbo.TB017_ParcelaProduto.TB017_id ");

                sSql.Append(" SELECT TOP (100) PERCENT dbo.TB016_Parcela.TB012_id, dbo.TB016_Parcela.TB016_id, dbo.TB017_ParcelaProduto.TB017_id, dbo.TB017_ParcelaProduto.TB017_ValorFinal, dbo.TB017_ParcelaProduto.TB017_Tipo, ");
                sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB016_Parcela.TB016_DataPagamento, dbo.TB012_Contratos.TB002_id ");
                sSql.Append(" FROM            dbo.TB016_Parcela INNER JOIN ");
                sSql.Append(" dbo.TB017_ParcelaProduto ON dbo.TB016_Parcela.TB016_id = dbo.TB017_ParcelaProduto.TB016_id INNER JOIN ");
                sSql.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id ");
                sSql.Append(" WHERE(dbo.TB016_Parcela.TB016_ComissaoProcessada = 0) AND(dbo.TB016_Parcela.TB016_Status = 5) AND(dbo.TB016_Parcela.TB016_ValorPago > 0) ");
                sSql.Append(" GROUP BY dbo.TB016_Parcela.TB012_id, dbo.TB016_Parcela.TB016_id, dbo.TB017_ParcelaProduto.TB017_id, dbo.TB017_ParcelaProduto.TB017_ValorFinal, dbo.TB017_ParcelaProduto.TB017_Tipo, ");
                sSql.Append(" dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB016_Parcela.TB016_DataPagamento, dbo.TB012_Contratos.TB002_id ");
                sSql.Append(" HAVING(dbo.TB017_ParcelaProduto.TB017_Tipo = 1) ");
                sSql.Append(" ORDER BY dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB016_Parcela.TB012_id, dbo.TB017_ParcelaProduto.TB017_id ");


                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new ParcelaController
                    {
                        Produto = new ParcelaProdutosController
                        {
                            TB017_id = Convert.ToInt64(reader["TB017_id"]),
                            TB017_ValorFinal = Convert.ToDouble(reader["TB017_ValorFinal"]),
                            TB017_TipoS = reader["TB017_Tipo"].ToString()
                        },
                        Contrato = new ContratosController
                        {
                            TB012_Id = Convert.ToInt64(reader["TB012_id"]),
                            TB002_id = Convert.ToInt64(reader["TB002_id"]),
                            TB012_TipoContrato = Convert.ToInt16(reader["TB012_TipoContrato"])
                        },
                        TB016_id = Convert.ToInt64(reader["TB016_id"]),
                        TB016_DataPagamento = Convert.ToDateTime(reader["TB016_DataPagamento"])

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

        public List<ParcelaController> ParcelasComissaoNaoProcessadaMemsalidade()
        {
            var retorno = new List<ParcelaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
 
                sSql.Append(" SELECT TOP (100) PERCENT dbo.TB016_Parcela.TB012_id, dbo.TB016_Parcela.TB016_id, dbo.TB017_ParcelaProduto.TB017_Tipo, dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB016_Parcela.TB016_DataPagamento,  ");
                sSql.Append(" dbo.TB012_Contratos.TB002_id, dbo.TB016_Parcela.TB016_ValorPago, dbo.TB016_Parcela.TB016_ParcelasAgrupadas ");
                sSql.Append(" FROM dbo.TB016_Parcela INNER JOIN ");
                sSql.Append(" dbo.TB017_ParcelaProduto ON dbo.TB016_Parcela.TB016_id = dbo.TB017_ParcelaProduto.TB016_id INNER JOIN ");
                sSql.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id ");
                sSql.Append(" WHERE(dbo.TB016_Parcela.TB016_ComissaoProcessada = 0) AND(dbo.TB016_Parcela.TB016_Status = 5) AND(dbo.TB016_Parcela.TB016_ValorPago > 0) ");
                sSql.Append(" GROUP BY dbo.TB016_Parcela.TB012_id, dbo.TB016_Parcela.TB016_id, dbo.TB017_ParcelaProduto.TB017_Tipo, dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB016_Parcela.TB016_DataPagamento, dbo.TB012_Contratos.TB002_id, ");
                sSql.Append(" dbo.TB016_Parcela.TB016_ValorPago, dbo.TB016_Parcela.TB016_ParcelasAgrupadas ");
                sSql.Append(" HAVING(dbo.TB017_ParcelaProduto.TB017_Tipo = 2) ");
                sSql.Append(" ORDER BY dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB016_Parcela.TB012_id ");


                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new ParcelaController
                    {
                        Produto = new ParcelaProdutosController
                        {
                            TB017_TipoS = reader["TB017_Tipo"].ToString()
                        },
                        Contrato = new ContratosController
                        {
                            TB012_Id = Convert.ToInt64(reader["TB012_id"]),
                            TB002_id = Convert.ToInt64(reader["TB002_id"]),
                            TB012_TipoContrato = Convert.ToInt16(reader["TB012_TipoContrato"])
                        },
                        TB016_id = Convert.ToInt64(reader["TB016_id"]),
                        //TB016_ParcelasAgrupadas= Convert.ToInt16(reader["TB016_ParcelasAgrupadas"]),
                        TB016_ParcelasAgrupadas = reader["TB016_ParcelasAgrupadas"] is DBNull ? 1 : Convert.ToInt16(reader["TB016_ParcelasAgrupadas"]),
                        TB016_DataPagamento = Convert.ToDateTime(reader["TB016_DataPagamento"])

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

        public List<ParcelaController> ListarCiclosAtivosContrato(long tb012Id)
        {
            var retorno = new List<ParcelaController>();
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);
                var sSql = new StringBuilder();
                sSql.Append(" SELECT TB012_id, TB012_CicloContrato FROM  dbo.TB016_Parcela GROUP BY TB012_id, TB012_CicloContrato HAVING TB012_id =");
                sSql.Append(tb012Id);

                var command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new ParcelaController
                    {
                        TB012_CicloContrato = reader["TB012_CicloContrato"] is DBNull ? 0 : Convert.ToInt32(reader["TB012_CicloContrato"]),
                        //TB012_CicloContrato = Convert.ToInt32(reader["TB012_CicloContrato"]),
                    };
                    retorno.Add(obj);
                }
                con.Close();

                var sSqlc = new StringBuilder();
                sSqlc.Append(" SELECT TB012_CicloContrato FROM  dbo.TB012_Contratos where TB012_id =");
                sSqlc.Append(tb012Id);


                var commandc = new SqlCommand(sSqlc.ToString(), con);
                commandc.CommandTimeout = 300;
                con.Open();
                var readerc = commandc.ExecuteReader();

                while (readerc.Read())
                {
                    var conf = retorno.FindAll(l => l.TB012_CicloContrato == Convert.ToInt32(readerc["TB012_CicloContrato"]));
                   
                   
                    if(conf.ToString() == "0")
                    {

                        var obj = new ParcelaController
                        {
                            TB012_CicloContrato = Convert.ToInt32(readerc["TB012_CicloContrato"]),
                        };
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

        public List<ParcelaController> BoletosParaImpressao(long tb012Id, long tb012CicloContrato)
        {
            var retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSql = new StringBuilder();


                sSql.Append(" SELECT TOP (24) TB016_id, TB016_Boleto ");
                sSql.Append(" FROM dbo.TB016_Parcela ");
                sSql.Append(" WHERE TB012_id = ");
                sSql.Append(tb012Id);
                sSql.Append(" AND TB012_CicloContrato = ");
                sSql.Append(tb012CicloContrato);
                sSql.Append("  AND TB016_Status < 3  AND TB016_FormaPagamento = 1");
                sSql.Append(" ORDER BY TB016_id");

                SqlCommand command = new SqlCommand(sSql.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController
                    {
                        TB016_id = Convert.ToInt64(reader["TB016_id"]),
                        TB016_Boleto = reader["TB016_Boleto"].ToString()
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

        public bool ParcelaUnir(ParcelaController Parcela01, long Parcela02)
        {
            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

            SqlCommand cmdUpdateTB017 = con.CreateCommand();
            SqlCommand cmdDeleteTB016 = con.CreateCommand();
            SqlCommand cmdUpdateTB016 = con.CreateCommand();

            StringBuilder sUpateTB017 = new StringBuilder();
            sUpateTB017.Append("update dbo.TB017_ParcelaProduto set");
            sUpateTB017.Append(" TB017_Item = ");
            sUpateTB017.Append(" TB017_Item +");
            sUpateTB017.Append("'");
            sUpateTB017.Append(" [Referente Parcela ");
            sUpateTB017.Append(Parcela01.TB033_Parcela);
            sUpateTB017.Append(" com vencimento em ");
            sUpateTB017.Append(Parcela01.TB016_Vencimento.ToString("dd/MM/yyyy"));
            sUpateTB017.Append("] ");
            sUpateTB017.Append("'");
            sUpateTB017.Append(",");
            sUpateTB017.Append(" TB016_id = ");
            sUpateTB017.Append(Parcela02);
            sUpateTB017.Append(" where  TB016_id =");
            sUpateTB017.Append(Parcela01.TB016_id);

            StringBuilder sDeleteTB016 = new StringBuilder();
            sDeleteTB016.Append("delete  from dbo.TB016_Parcela where TB016_id =");
            sDeleteTB016.Append(Parcela01.TB016_id);


            StringBuilder sUpateTB016 = new StringBuilder();
            sUpateTB016.Append("update TB016_Parcela set");
            sUpateTB016.Append(" TB016_ParcelasAgrupadas =");
            sUpateTB016.Append(Parcela01.TB016_ParcelasAgrupadas +1 );

       
            sUpateTB016.Append(" ,TB016_Valor = TB016_Valor + ");
            sUpateTB016.Append(Parcela01.TB016_Valor.ToString().Replace(",", "."));
            sUpateTB016.Append(" where  TB016_id =");
            sUpateTB016.Append(Parcela02);


            cmdUpdateTB017.CommandText = sUpateTB017.ToString();
            cmdUpdateTB016.CommandText = sUpateTB016.ToString();
            cmdDeleteTB016.CommandText = sDeleteTB016.ToString();
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {

                cmdUpdateTB017.Transaction = tran;
                cmdUpdateTB017.ExecuteNonQuery();

                cmdUpdateTB016.Transaction = tran;
                cmdUpdateTB016.ExecuteNonQuery();

                cmdDeleteTB016.Transaction = tran;
                cmdDeleteTB016.ExecuteNonQuery();


                tran.Commit();
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return true;
        }

        public long ParcelaIdVencimento(long TB012_id, DateTime Vencimento)
        {
            long Retorno = 0;
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();


                sSQL.Append("SELECT TB016_id FROM dbo.TB016_Parcela");
                sSQL.Append(" WHERE ");
                sSQL.Append(" TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append(" AND ");
                sSQL.Append(" TB016_Vencimento = ");
                sSQL.Append("'");
                sSQL.Append(Vencimento.ToString("MM/dd/yyyy"));
                sSQL.Append("'");
                sSQL.Append(" AND ");
                sSQL.Append(" TB016_Status < 3 ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno = Convert.ToInt64(reader["TB016_id"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public List<ParcelaController> ListaContratoCiclo(long TB012_id)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB016_Parcela.TB016_id, dbo.TB015_Planos.TB015_id, dbo.TB015_Planos.TB015_Plano, dbo.TB012_Contratos.TB012_id, dbo.TB001_Empresa.TB001_id, dbo.TB001_Empresa.TB001_RazaoSocial,  ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Parcela, dbo.TB016_Parcela.TB016_TotalParcelas, dbo.TB016_Parcela.TB016_Emissao, dbo.TB016_Parcela.TB016_Vencimento, dbo.TB016_Parcela.TB016_Valor, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_ValorPago, dbo.TB016_Parcela.TB016_FormaPagamento, dbo.TB016_Parcela.TB016_DataPagamento, dbo.TB016_Parcela.TB016_ValorAdesao, dbo.TB016_Parcela.TB016_Entrada, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Status ");
                sSQL.Append(" FROM dbo.TB016_Parcela INNER JOIN ");
                sSQL.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id INNER JOIN ");
                sSQL.Append(" dbo.TB015_Planos ON dbo.TB016_Parcela.TB015_id = dbo.TB015_Planos.TB015_id INNER JOIN ");
                sSQL.Append(" dbo.TB001_Empresa ON dbo.TB016_Parcela.TB001_id = dbo.TB001_Empresa.TB001_id ");
                sSQL.Append(" WHERE dbo.TB016_Parcela.TB012_id =  ");
                sSQL.Append(123768);
                sSQL.Append(" AND dbo.TB012_Contratos.TB012_CicloContrato =  ");
                sSQL.Append(82017);

                sSQL.Append(" ORDER BY dbo.TB016_Parcela.TB016_id ");



                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.Plano = new PlanoController();


                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);

                    obj.Plano.TB015_id = Convert.ToInt64(reader["TB015_id"]);
                    obj.Plano.TB015_Plano = reader["TB015_Plano"].ToString().TrimEnd();

                    //// obj.TB016_ParcelaCodigo     = reader["TB016_ParcelaCodigo"] is DBNull ? "-" : reader["TB016_ParcelaCodigo"].ToString();

                    //obj.TB016_Parcela = Convert.ToInt16(reader["TB016_Parcela"]);
                    //obj.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    //obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    //obj.TB016_Pagador = reader["TB016_Pagador"].ToString();
                    //obj.TB016_CPFCNPJ = reader["TB016_CPFCNPJ"].ToString();
                    //obj.TB016_EnderecoPagador = reader["TB016_EnderecoPagador"].ToString();
                    //obj.TB016_TipoSacadoS = reader["TB016_TipoSacado"].ToString();


                    //obj.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);

                    //obj.TB016_ValorPago = reader["TB025_ValorCobrado"] is DBNull ? 0 : Convert.ToDouble(reader["TB025_ValorCobrado"].ToString());

                    //obj.TB016_FormaPagamentoS = reader["TB016_FormaPagamento"].ToString();

                    //if (obj.TB016_FormaPagamentoS == "4")
                    //{
                    //    obj.TB016_FormaPagamentoS = "6";
                    //}
                    //else
                    //{
                    //    if (obj.TB016_FormaPagamentoS == "5")
                    //    {
                    //        obj.TB016_FormaPagamentoS = "6";
                    //    }
                    //}
                    //obj.TB016_EmitirBoleto = Convert.ToInt16(reader["TB016_EmitirBoleto"]);


                    //if (reader["TB016_DataPagamento"].ToString() == string.Empty)
                    //{
                    //    DateTime DataNull = new DateTime(1900, 1, 1);
                    //    obj.TB016_DataPagamento = DataNull;
                    //}
                    //else
                    //{
                    //    obj.TB016_DataPagamento = Convert.ToDateTime(reader["TB016_DataPagamento"]);
                    //}

                    //obj.TB016_Banco = reader["TB016_Banco"] is DBNull ? "NÃO INFORMADO" : reader["TB016_Banco"].ToString();
                    ////obj.TB016_Descricao         = reader["TB016_Descricao"]         is DBNull ? "NÃO INFORMADO" : reader["TB016_Descricao"].ToString();
                    ////obj.TB016_Agencia           = reader["TB016_Agencia"]           is DBNull ? "NÃO INFORMADO" : reader["TB016_Agencia"].ToString();
                    ////obj.TB016_Especie           = reader["TB016_Especie"]           is DBNull ? "NÃO INFORMADO" : reader["TB016_Especie"].ToString();
                    ////obj.TB016_DocumentoBanco    = reader["TB016_DocumentoBanco"]    is DBNull ? "NÃO INFORMADO" : reader["TB016_DocumentoBanco"].ToString();
                    ////obj.TB016_UrlBoleto         = reader["TB016_UrlBoleto"]         is DBNull ? "NÃO INFORMADO" : reader["TB016_UrlBoleto"].ToString();
                    //obj.TB016_Entrada = Convert.ToInt16(reader["TB016_Entrada"]);
                    //obj.TB016_ValorAdesao = Convert.ToDouble(reader["TB016_ValorAdesao"]);




                    ////obj.TB016_StatusS           = reader["TB016_Status"].ToString();
                    //obj.TB016_StatusS = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(reader["TB016_Status"]));


                    //obj.TB015_id = Convert.ToInt64(reader["TB015_id"]);
                    //obj.TB015_Plano = reader["TB015_Plano"].ToString();




                    //obj.TB012_id = Convert.ToInt64(reader["TB012_id"]);

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
        public List<ParcelaController> ParcelasContratoExistente(long TB012_id)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB016_Parcela.TB016_id, dbo.TB016_Parcela.TB016_Parcela, dbo.TB016_Parcela.TB016_Emissao, dbo.TB016_Parcela.TB016_Vencimento, dbo.TB016_Parcela.TB016_Pagador,  ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_CPFCNPJ, dbo.TB016_Parcela.TB016_EnderecoPagador, dbo.TB016_Parcela.TB016_TipoSacado, dbo.TB016_Parcela.TB016_Valor, dbo.TB016_Parcela.TB016_FormaPagamento, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_EmitirBoleto, dbo.TB016_Parcela.TB016_DataPagamento, dbo.TB016_Parcela.TB016_Banco, dbo.TB016_Parcela.TB016_Entrada, dbo.TB016_Parcela.TB016_ValorAdesao,  ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Status, dbo.TB015_Planos.TB015_id, dbo.TB015_Planos.TB015_Plano, dbo.TB016_Parcela.TB012_id, dbo.TB025_Pagamentos.TB025_ValorCobrado,  ");
                sSQL.Append(" dbo.TB025_Pagamentos.TB025_ValorTitulo, dbo.TB025_Pagamentos.TB025_ValorIOF, dbo.TB025_Pagamentos.TB025_ValorTarifa, dbo.TB025_Pagamentos.TB025_BancoRecebedor,  ");
                sSQL.Append(" dbo.TB025_Pagamentos.TB025_ContaCorrente, dbo.TB025_Pagamentos.TB025_AgenciaRecebedora, dbo.TB025_Pagamentos.TB025_FormaProcessamento, dbo.TB025_Pagamentos.TB025_FormaPagamento,  ");
                sSQL.Append(" dbo.TB025_Pagamentos.TB025_Vencimento, dbo.TB025_Pagamentos.TB025_DataLiquidacao, dbo.TB025_Pagamentos.TB025_DataMovimentacao, dbo.TB025_Pagamentos.TB025_DataLiquidacaoCredito ");
                sSQL.Append(" FROM dbo.TB016_Parcela INNER JOIN ");
                sSQL.Append(" dbo.TB015_Planos ON dbo.TB016_Parcela.TB015_id = dbo.TB015_Planos.TB015_id LEFT OUTER JOIN ");
                sSQL.Append(" dbo.TB025_Pagamentos ON dbo.TB016_Parcela.TB016_id = dbo.TB025_Pagamentos.TB016_id ");
                sSQL.Append(" WHERE TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append(" ORDER BY dbo.TB016_Parcela.TB016_id ");



                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);

                    // obj.TB016_ParcelaCodigo     = reader["TB016_ParcelaCodigo"] is DBNull ? "-" : reader["TB016_ParcelaCodigo"].ToString();

                    obj.TB016_Parcela = Convert.ToInt16(reader["TB016_Parcela"]);
                    obj.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    obj.TB016_Pagador = reader["TB016_Pagador"].ToString();
                    obj.TB016_CPFCNPJ = reader["TB016_CPFCNPJ"].ToString();
                    obj.TB016_EnderecoPagador = reader["TB016_EnderecoPagador"].ToString();
                    obj.TB016_TipoSacadoS = reader["TB016_TipoSacado"].ToString();


                    obj.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);

                    obj.TB016_ValorPago = reader["TB025_ValorCobrado"] is DBNull ? 0 : Convert.ToDouble(reader["TB025_ValorCobrado"].ToString());

                    obj.TB016_FormaPagamentoS = reader["TB016_FormaPagamento"].ToString();

                    if (obj.TB016_FormaPagamentoS == "4")
                    {
                        obj.TB016_FormaPagamentoS = "6";
                    }
                    else
                    {
                        if (obj.TB016_FormaPagamentoS == "5")
                        {
                            obj.TB016_FormaPagamentoS = "6";
                        }
                    }
                    obj.TB016_EmitirBoleto = Convert.ToInt16(reader["TB016_EmitirBoleto"]);


                    if (reader["TB016_DataPagamento"].ToString() == string.Empty)
                    {
                        DateTime DataNull = new DateTime(1900, 1, 1);
                        obj.TB016_DataPagamento = DataNull;
                    }
                    else
                    {
                        obj.TB016_DataPagamento = Convert.ToDateTime(reader["TB016_DataPagamento"]);
                    }

                    obj.TB016_Banco = reader["TB016_Banco"] is DBNull ? "NÃO INFORMADO" : reader["TB016_Banco"].ToString();
                    //obj.TB016_Descricao         = reader["TB016_Descricao"]         is DBNull ? "NÃO INFORMADO" : reader["TB016_Descricao"].ToString();
                    //obj.TB016_Agencia           = reader["TB016_Agencia"]           is DBNull ? "NÃO INFORMADO" : reader["TB016_Agencia"].ToString();
                    //obj.TB016_Especie           = reader["TB016_Especie"]           is DBNull ? "NÃO INFORMADO" : reader["TB016_Especie"].ToString();
                    //obj.TB016_DocumentoBanco    = reader["TB016_DocumentoBanco"]    is DBNull ? "NÃO INFORMADO" : reader["TB016_DocumentoBanco"].ToString();
                    //obj.TB016_UrlBoleto         = reader["TB016_UrlBoleto"]         is DBNull ? "NÃO INFORMADO" : reader["TB016_UrlBoleto"].ToString();
                    obj.TB016_Entrada = Convert.ToInt16(reader["TB016_Entrada"]);
                    obj.TB016_ValorAdesao = Convert.ToDouble(reader["TB016_ValorAdesao"]);




                    //obj.TB016_StatusS           = reader["TB016_Status"].ToString();
                    obj.TB016_StatusS = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(reader["TB016_Status"]));


                    obj.TB015_id = Convert.ToInt64(reader["TB015_id"]);
                    obj.TB015_Plano = reader["TB015_Plano"].ToString();




                    obj.TB012_id = Convert.ToInt64(reader["TB012_id"]);

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

        public List<ParcelaController> ParcelasContratoPrimeiraPendente(long TB012_id)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append("SELECT TOP (1) PERCENT MIN(dbo.TB016_Parcela.TB016_id) AS TB016_id, ");
                sSQL.Append("dbo.TB015_Planos.TB015_id,  ");
                sSQL.Append("dbo.TB015_Planos.TB015_Plano,  ");
                sSQL.Append("dbo.TB016_Parcela.TB016_Valor, ");
                sSQL.Append("dbo.TB016_Parcela.TB016_Vencimento ");
                sSQL.Append("FROM ");
                sSQL.Append("dbo.TB016_Parcela ");
                sSQL.Append("INNER JOIN ");
                sSQL.Append("dbo.TB015_Planos ");
                sSQL.Append("ON  ");
                sSQL.Append("dbo.TB016_Parcela.TB015_id = dbo.TB015_Planos.TB015_id ");
                sSQL.Append("GROUP BY  ");
                sSQL.Append("dbo.TB016_Parcela.TB012_id, ");
                sSQL.Append("dbo.TB016_Parcela.TB016_Status, ");
                sSQL.Append("dbo.TB015_Planos.TB015_id,  ");
                sSQL.Append("dbo.TB015_Planos.TB015_Plano,  ");
                sSQL.Append("dbo.TB016_Parcela.TB016_Valor, ");
                sSQL.Append("dbo.TB016_Parcela.TB016_Vencimento ");
                sSQL.Append("HAVING ");
                sSQL.Append("dbo.TB016_Parcela.TB012_id =  ");
                sSQL.Append(TB012_id);
                //sSQL.Append("AND ");
                //sSQL.Append("dbo.TB016_Parcela.TB016_Status = ");
                //sSQL.Append(1);
                sSQL.Append("ORDER BY  ");
                sSQL.Append("TB016_id ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);
                    obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);

                    PlanoController objPlano = new PlanoController();
                    objPlano.TB015_id = Convert.ToInt64(reader["TB015_id"]);
                    objPlano.TB015_Plano = reader["TB015_Plano"].ToString();


                    obj.Plano = objPlano;

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

        public ParcelaController ParcelaEntrada(long TB012_id)
        {
            ParcelaController Retorno = new ParcelaController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();


                sSQL.Append("SELECT ");
                sSQL.Append("TB012_id, ");
                sSQL.Append("TB016_Valor,  ");
                sSQL.Append("TB016_Entrada,  ");
                sSQL.Append("TB016_ValorAdesao ");
                sSQL.Append("FROM  ");
                sSQL.Append("dbo.TB016_Parcela ");
                sSQL.Append("WHERE ");
                sSQL.Append("TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append("AND ");
                sSQL.Append("TB016_Entrada = ");
                sSQL.Append(1);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB016_ValorAdesao = Convert.ToDouble(reader["TB016_ValorAdesao"]);
                    Retorno.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public ParcelaController GerarCobrancasParcela(ParcelaController Parcela, int TotalParcelas)
        {
            ParcelaController Retorno = new ParcelaController();
            try
            {
                string inserParcela = "INSERT INTO TB016_Parcela ( " +
                                        " TB015_id, " +
                                        " TB001_id, " +
                                        " TB012_id, " +
                                        " TB013_id, " +
                                        " TB016_Parcela, " +
                                        " TB016_TotalParcelas, " +
                                        " TB016_Emissao, " +
                                        " TB016_Vencimento, " +
                                        " TB016_Pagador, " +
                                        " TB016_CPFCNPJ, " +
                                        " TB016_EnderecoPagador, " +
                                        " TB016_TipoSacado, " +
                                        " TB016_Valor, " +
                                        " TB016_FormaPagamento, " +
                                        " TB016_EmitirBoleto, " +
                                        " TB016_Entrada, " +
                                        " TB016_ValorAdesao, " +
                                        " TB016_IOF, " +
                                        " TB016_TipoVencimento, " +
                                        " TB016_BoletoDesc1, " +
                                        " TB016_BoletoDesc2, " +
                                        " TB016_BoletoDesc3, " +
                                        " TB016_BoletoDesc4, " +
                                        " TB016_BoletoDesc5, " +
                                        " TB016_EspecieDocumento ," +
                                        " TB016_Beneficiario ," +
                                        " TB016_BeneficiarioEndereco ," +
                                        " TB016_BeneficiarioCPFCNPJ ," +
                                        " TB016_BeneficiarioCidade ," +
                                        " TB016_BeneficiarioUF ," +
                                        " TB016_PagadorCEP ," +
                                        " TB016_PagadorCidade ," +
                                        " TB016_PagadorUF,TB016_Status,TB012_CicloContrato,TB016_LoteExportacao" +
                                        " ) VALUES ( " +
                                        " @TB015_id, " +
                                        " @TB001_id, " +
                                        " @TB012_id, " +
                                        " @TB013_id, " +
                                        " @TB016_Parcela, " +
                                        " @TB016_TotalParcelas, " +
                                        " @TB016_Emissao, " +
                                        " @TB016_Vencimento, " +
                                        " @TB016_Pagador, " +
                                        " @TB016_CPFCNPJ, " +
                                        " @TB016_EnderecoPagador, " +
                                        " @TB016_TipoSacado, " +
                                        " @TB016_Valor, " +
                                        " @TB016_FormaPagamento, " +
                                        " @TB016_EmitirBoleto, " +
                                        " @TB016_Entrada, " +
                                        " @TB016_ValorAdesao, " +
                                        " @TB016_IOF, " +
                                        " @TB016_TipoVencimento, " +
                                        " @TB016_BoletoDesc1, " +
                                        " @TB016_BoletoDesc2, " +
                                        " @TB016_BoletoDesc3, " +
                                        " @TB016_BoletoDesc4, " +
                                        " @TB016_BoletoDesc5, " +
                                        " @TB016_EspecieDocumento, " +
                                        " @TB016_Beneficiario, " +
                                        " @TB016_BeneficiarioEndereco, " +
                                        " @TB016_BeneficiarioCPFCNPJ, " +
                                        " @TB016_BeneficiarioCidade, " +
                                        " @TB016_BeneficiarioUF, " +
                                        " @TB016_PagadorCEP ," +
                                        " @TB016_PagadorCidade ," +
                                        " @TB016_PagadorUF,@TB016_Status,@TB012_CicloContrato,@TB016_LoteExportacao " +
                                        " ) SELECT SCOPE_IDENTITY()";




                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(inserParcela, con);
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@TB015_id", Parcela.TB015_id);
                    command.Parameters.AddWithValue("@TB001_id", Parcela.Empresa.TB001_id);
                    command.Parameters.AddWithValue("@TB016_Beneficiario", Parcela.Empresa.TB001_RazaoSocial.TrimEnd().TrimStart());
                    command.Parameters.AddWithValue("@TB016_BeneficiarioEndereco", Parcela.Empresa.TB001_Logradouro.TrimEnd().TrimStart() + " ," + Parcela.Empresa.TB001_Numero.TrimEnd().TrimStart());
                    command.Parameters.AddWithValue("@TB016_BeneficiarioCidade", Parcela.Empresa.Cidade);
                    command.Parameters.AddWithValue("@TB016_BeneficiarioUF", Parcela.Empresa.UF.TrimEnd().TrimStart());
                    command.Parameters.AddWithValue("@TB016_BeneficiarioCPFCNPJ", Parcela.Empresa.TB001_CNPJ.TrimEnd().TrimStart());
                    command.Parameters.AddWithValue("@TB012_id", Parcela.TB012_id);
                    command.Parameters.AddWithValue("@TB013_id", Parcela.Pessoa.TB013_id);
                    command.Parameters.AddWithValue("@TB016_Parcela", Parcela.TB016_Parcela);
                    command.Parameters.AddWithValue("@TB016_TotalParcelas", TotalParcelas);
                    command.Parameters.AddWithValue("@TB016_Emissao", Parcela.TB016_Emissao);
                    command.Parameters.AddWithValue("@TB016_Vencimento", Parcela.TB016_Vencimento);
                    command.Parameters.AddWithValue("@TB016_Pagador", Parcela.Pessoa.TB013_NomeCompleto.TrimEnd());
                    command.Parameters.AddWithValue("@TB016_CPFCNPJ", Parcela.Pessoa.TB013_CPFCNPJ);
                    command.Parameters.AddWithValue("@TB016_EnderecoPagador", Parcela.Pessoa.TB013_Logradouro.TrimEnd() + ". Nº." + Parcela.Pessoa.TB013_Numero.TrimEnd() + ". CEP " + Parcela.Pessoa.TB004_Cep.TrimEnd() + ". Bairro " + Parcela.Pessoa.TB013_Bairro.TrimEnd() + ". Municipio " + Parcela.Pessoa.Municipio.TB006_Municipio.TrimEnd() + ". Estado " + Parcela.Pessoa.Municipio.Estado.TB005_Estado.TrimEnd());
                    command.Parameters.AddWithValue("@TB016_PagadorCEP", Parcela.Pessoa.TB004_Cep.TrimEnd());
                    //command.Parameters.AddWithValue("@TB016_PagadorCEP", Parcela.Pessoa.TB004_Cep.Insert(5, "-"));
                    command.Parameters.AddWithValue("@TB016_PagadorCidade", Parcela.Pessoa.Municipio.TB006_Municipio.TrimEnd());
                    command.Parameters.AddWithValue("@TB016_PagadorUF", Parcela.Pessoa.Municipio.Estado.TB005_Sigla.TrimEnd());
                    command.Parameters.AddWithValue("@TB016_TipoSacado", Parcela.Pessoa.TB013_TipoS);
                    command.Parameters.AddWithValue("@TB016_Valor", Parcela.TB016_Valor);
                    command.Parameters.AddWithValue("@TB016_FormaPagamento", Parcela.TB016_FormaPagamentoS);
                    command.Parameters.AddWithValue("@TB016_EmitirBoleto", Parcela.TB016_EmitirBoleto);
                    command.Parameters.AddWithValue("@TB016_Entrada", Parcela.TB016_Entrada);
                    command.Parameters.AddWithValue("@TB016_ValorAdesao", Parcela.TB016_ValorAdesao);
                    command.Parameters.AddWithValue("@TB016_IOF", Parcela.Plano.TB015_IOF);
                    command.Parameters.AddWithValue("@TB016_TipoVencimento", Parcela.Plano.TB015_TipoVencimento);
                    command.Parameters.AddWithValue("@TB016_BoletoDesc1", Parcela.Plano.TB015_BoletoDesc1);
                    command.Parameters.AddWithValue("@TB016_BoletoDesc2", Parcela.Plano.TB015_BoletoDesc2);
                    command.Parameters.AddWithValue("@TB016_BoletoDesc3", Parcela.Plano.TB015_BoletoDesc3);
                    command.Parameters.AddWithValue("@TB016_BoletoDesc4", Parcela.Plano.TB015_BoletoDesc4);
                    command.Parameters.AddWithValue("@TB016_BoletoDesc5", Parcela.Plano.TB015_BoletoDesc5);
                    command.Parameters.AddWithValue("@TB016_EspecieDocumento", Parcela.Plano.TB015_EspecieDocumento);
                    command.Parameters.AddWithValue("@TB012_CicloContrato", Parcela.TB012_CicloContrato);
                    command.Parameters.AddWithValue("@TB016_LoteExportacao", Parcela.TB016_LoteExportacao);



                    if (Parcela.TB016_EmitirBoleto == 1 && Convert.ToInt16(Parcela.TB016_FormaPagamentoS) == 1)
                    {
                        command.Parameters.AddWithValue("@TB016_Status", 1);
                    }
                    else
                    {


                        if (Parcela.TB016_EmitirBoleto == 0 && Convert.ToInt16(Parcela.TB016_FormaPagamentoS) == 1)
                        {
                            command.Parameters.AddWithValue("@TB016_Status", 1);
                        }

                        if (Convert.ToInt16(Parcela.TB016_FormaPagamentoS) > 1)
                        {
                            command.Parameters.AddWithValue("@TB016_Status", 2);
                        }
                    }


                    //    if (Parcela.TB016_EmitirBoleto==1 && Parcela.TB016_FormaPagamentoS=="1")
                    //    {
                    //        command.Parameters.AddWithValue("@TB016_Status", 1);
                    //    }
                    //    else
                    //    {
                    //        if (Parcela.TB016_EmitirBoleto == 1 && Parcela.TB016_FormaPagamentoS == "3")
                    //        {
                    //            command.Parameters.AddWithValue("@TB016_Status", 2);
                    //        }
                    //        else
                    //{
                    //    if (Parcela.TB016_EmitirBoleto == 1 && Parcela.TB016_FormaPagamentoS == "6")
                    //    {
                    //        command.Parameters.AddWithValue("@TB016_Status", 2);
                    //    }
                    //}
                    //    }
                    //{
                    //    if (Parcela.TB016_EmitirBoleto == 1 && Parcela.TB016_FormaPagamentoS == "2")
                    //    {
                    //        command.Parameters.AddWithValue("@TB016_Status", 5);
                    //    }
                    //    else
                    //    {
                    //        if (Parcela.TB016_EmitirBoleto == 1 && Parcela.TB016_FormaPagamentoS == "3")
                    //        {
                    //            command.Parameters.AddWithValue("@TB016_Status", 5);
                    //        }
                    //        else
                    //            if (Parcela.TB016_EmitirBoleto == 1 && Parcela.TB016_FormaPagamentoS == "4")
                    //            {
                    //                command.Parameters.AddWithValue("@TB016_Status", 5);
                    //            }
                    //        else
                    //            {
                    //                if (Parcela.TB016_EmitirBoleto == 1 && Parcela.TB016_FormaPagamentoS == "5")
                    //                {
                    //                    command.Parameters.AddWithValue("@TB016_Status", 5);
                    //                }
                    //    else
                    //    {
                    //        if (Parcela.TB016_EmitirBoleto == 1 && Parcela.TB016_FormaPagamentoS == "6")
                    //        {
                    //            command.Parameters.AddWithValue("@TB016_Status", 5);
                    //        }
                    //    }
                    //            }
                    //    }
                    //}

                    //if (Parcela.TB016_EmitirBoleto == 0)// && Parcela.TB016_FormaPagamentoS == "1")
                    //{
                    //    command.Parameters.AddWithValue("@TB016_Status", 1);
                    //}


                    Retorno.TB016_CPFCNPJ = Parcela.Empresa.TB001_CNPJ.Replace(",", "").Replace(".", "").Replace("/", "").Replace("-", "").Trim();
                    Retorno.TB016_id = Convert.ToInt32(command.ExecuteScalar());
                    Retorno.TB016_Parcela = Parcela.TB016_Parcela;
                    Retorno.TB016_Emissao = Parcela.TB016_Emissao;
                    Retorno.TB016_Vencimento = Parcela.TB016_Vencimento;
                    Retorno.TB012_id = Parcela.TB012_id;
                    Retorno.TB015_id = Parcela.TB015_id;
                    Retorno.TB015_Plano = Parcela.TB015_Plano;
                    Retorno.TB016_Valor = Parcela.TB016_Valor;
                    Retorno.TB016_DataPagamento = Parcela.TB016_DataPagamento;
                    Retorno.TB016_FormaPagamentoS = Parcela.TB016_FormaPagamentoS;
                    //Retorno.TB016_Descricao         = Parcela.TB016_Descricao;
                    Retorno.TB016_EmitirBoleto = Parcela.TB016_EmitirBoleto;
                    Retorno.TB016_EnderecoPagador = Parcela.TB016_EnderecoPagador;
                    Retorno.TB016_Entrada = Parcela.TB016_Entrada;
                    //Retorno.TB016_Especie           = Parcela.TB016_Especie;                       
                    Retorno.TB016_Pagador = Parcela.TB016_Pagador;
                    //         = "1";

                    if (Parcela.TB016_EmitirBoleto > 0)
                    {
                        Retorno.TB016_StatusS = "2";
                    }
                    else
                    {
                        Retorno.TB016_StatusS = "1";
                    }

                    //if (Parcela.TB016_EmitirBoleto == 1 && Parcela.TB016_FormaPagamentoS == "1")
                    //{
                    //    Retorno.TB016_StatusS = "1";
                    //}
                    //else
                    //{
                    //    if (Parcela.TB016_EmitirBoleto == 1 && Parcela.TB016_FormaPagamentoS == "2")
                    //    {
                    //        Retorno.TB016_StatusS = "5";
                    //    }
                    //    else
                    //    {
                    //        if (Parcela.TB016_EmitirBoleto == 1 && Parcela.TB016_FormaPagamentoS == "3")
                    //        {
                    //            Retorno.TB016_StatusS = "5";
                    //        }
                    //        else
                    //            if (Parcela.TB016_EmitirBoleto == 1 && Parcela.TB016_FormaPagamentoS == "4")
                    //        {
                    //            Retorno.TB016_StatusS = "5";
                    //        }
                    //        else
                    //        {
                    //            if (Parcela.TB016_EmitirBoleto == 1 && Parcela.TB016_FormaPagamentoS == "5")
                    //            {
                    //                Retorno.TB016_StatusS = "5";
                    //            }
                    //        }
                    //    }
                    //}
                    // Retorno.TB016_id= Convert.ToInt64(command.ExecuteScalar());

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Retorno;
        }

        public ParcelaProdutosController ParcelaProdutoInsert(ParcelaProdutosController Produto)
        {
            ParcelaProdutosController Retorno = new ParcelaProdutosController();
            try
            {
                string inserParcela = "INSERT INTO TB017_ParcelaProduto (TB016_id,TB017_Item,TB017_ValorUnitario,TB017_ValorDesconto,TB017_ValorFinal,TB017_IdProteus) VALUES (@TB016_id,@TB017_Item,@TB017_ValorUnitario,@TB017_ValorDesconto,@TB017_ValorFinal,@TB017_IdProteus) SELECT SCOPE_IDENTITY()";

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(inserParcela, con);
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@TB016_id", Produto.TB016_id);
                    command.Parameters.AddWithValue("@TB017_IdProteus", Produto.TB017_IdProteus);
                    command.Parameters.AddWithValue("@TB017_Item", Produto.TB017_Item);
                    command.Parameters.AddWithValue("@TB017_ValorUnitario", Produto.TB017_ValorUnitario);
                    command.Parameters.AddWithValue("@TB017_ValorDesconto", Produto.TB017_ValorDesconto);
                    command.Parameters.AddWithValue("@TB017_ValorFinal", Produto.TB017_ValorFinal);

                    Retorno.TB017_id = Convert.ToInt32(command.ExecuteScalar());
                    Retorno.TB017_IdProteus = Produto.TB017_IdProteus;
                    Retorno.TB017_Item = Produto.TB017_Item;
                    Retorno.TB017_ValorUnitario = Produto.TB017_ValorUnitario;
                    Retorno.TB017_ValorDesconto = Produto.TB017_ValorDesconto;
                    Retorno.TB017_ValorFinal = Produto.TB017_ValorFinal;

                    Retorno.TB016_id = Produto.TB016_id;
                    Retorno.TB016_Parcela = Produto.TB016_Parcela;
                    Retorno.TB017_Isento = Produto.TB017_Isento;
                    Retorno.TB017_Maior = Produto.TB017_Maior;
                    Retorno.TB017_Menor = Produto.TB017_Menor;

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Retorno;
        }

        public List<ParcelaController> ParcelasParaEmissaoBoleto(Int64 TB012_id)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                //sSQL.Append("SELECT ");
                sSQL.Append("SELECT dbo.TB006_Municipio.TB006_id, dbo.TB016_Parcela.TB012_id, dbo.TB016_Parcela.TB016_FormaPagamento, dbo.TB016_Parcela.TB016_Status, dbo.TB016_Parcela.TB016_EmitirBoleto,  ");
                sSQL.Append("dbo.TB018_Bancos.TB018_Banco, dbo.TB001_Empresa.TB001_id, dbo.TB001_Empresa.TB001_Matriz, dbo.TB016_Parcela.TB016_Vencimento, dbo.TB016_Parcela.TB016_EspecieDocumento,  ");
                sSQL.Append("dbo.TB016_Parcela.TB016_Valor, dbo.TB016_Parcela.TB016_Abatimento, dbo.TB016_Parcela.TB016_IOF, dbo.TB016_Parcela.TB016_Pagador, dbo.TB006_Municipio.TB006_Municipio,  ");
                sSQL.Append("dbo.TB005_Estado.TB005_Sigla, dbo.TB016_Parcela.TB016_CPFCNPJ, dbo.TB016_Parcela.TB016_TipoVencimento, dbo.TB016_Parcela.TB016_BoletoDesc1, dbo.TB016_Parcela.TB016_BoletoDesc2,  ");
                sSQL.Append("dbo.TB016_Parcela.TB016_BoletoDesc3, dbo.TB016_Parcela.TB016_BoletoDesc4, dbo.TB016_Parcela.TB016_BoletoDesc5, dbo.TB016_Parcela.TB016_Emissao, dbo.TB016_Parcela.TB016_id,  ");
                sSQL.Append("dbo.TB012_Contratos.TB004_Cep, dbo.TB012_Contratos.TB012_Logradouro, dbo.TB012_Contratos.TB012_Numero, dbo.TB012_Contratos.TB012_Bairro, dbo.TB012_Contratos.TB012_Complemento, ");
                sSQL.Append("dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_Tipo ");
                sSQL.Append("FROM dbo.TB012_Contratos INNER JOIN ");
                sSQL.Append("dbo.TB016_Parcela INNER JOIN ");
                sSQL.Append("dbo.TB001_Empresa ON dbo.TB016_Parcela.TB001_id = dbo.TB001_Empresa.TB001_id INNER JOIN ");
                sSQL.Append("dbo.TB018_Bancos ON dbo.TB001_Empresa.TB018_id = dbo.TB018_Bancos.TB018_id INNER JOIN ");
                sSQL.Append("dbo.TB013_Pessoa ON dbo.TB016_Parcela.TB013_id = dbo.TB013_Pessoa.TB013_id ON dbo.TB012_Contratos.TB012_id = dbo.TB016_Parcela.TB012_id INNER JOIN ");
                sSQL.Append("dbo.TB005_Estado INNER JOIN ");
                sSQL.Append("dbo.TB006_Municipio ON dbo.TB005_Estado.TB005_Id = dbo.TB006_Municipio.TB005_Id ON dbo.TB012_Contratos.TB006_id = dbo.TB006_Municipio.TB006_id ");
                sSQL.Append("WHERE ");
                sSQL.Append("dbo.TB016_Parcela.TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append("AND ");
                sSQL.Append("dbo.TB016_Parcela.TB016_FormaPagamento = ");
                sSQL.Append(1);
                sSQL.Append("AND ");
                sSQL.Append("dbo.TB016_Parcela.TB016_Status = ");
                sSQL.Append(1);
                sSQL.Append("AND ");
                sSQL.Append("dbo.TB016_Parcela.TB016_EmitirBoleto = ");
                sSQL.Append(1);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();

                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    obj.TB016_EspecieDocumento = reader["TB016_EspecieDocumento"].ToString().Trim();
                    obj.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);
                    //obj.TB016_Abatimento        = Convert.ToDouble(reader["TB016_Abatimento"]);
                    obj.TB016_IOF = Convert.ToDouble(reader["TB016_IOF"]);
                    obj.TB016_Pagador = reader["TB016_Pagador"].ToString().TrimEnd();

                    if (Convert.ToInt16(reader["TB013_Tipo"]) == 1)
                    {
                        obj.TB016_CPFCNPJ = reader["TB016_CPFCNPJ"].ToString().TrimEnd().PadLeft(11, '0');
                    }
                    else
                    {
                        obj.TB016_CPFCNPJ = reader["TB016_CPFCNPJ"].ToString().TrimEnd().PadLeft(13, '0');
                    }
                    //obj.TB016_CPFCNPJ           = reader["TB016_CPFCNPJ"].ToString().TrimEnd();

                    //if ( Convert.ToInt16( reader["TB016_CPFCNPJ"].ToString().TrimEnd()) ==1)
                    //{
                    //    obj.TB016_CPFCNPJ = Convert.ToUInt64(obj.TB016_CPFCNPJ.PadLeft(11,'0'));
                    //}
                    //else
                    //{
                    //    obj.TB016_CPFCNPJ = Convert.ToUInt64(obj.TB016_CPFCNPJ).ToString(@"00\.000\.000\/0000\-00");
                    //}


                    obj.TB016_TipoVencimento = Convert.ToInt16(reader["TB016_TipoVencimento"]);
                    obj.TB016_BoletoDesc1 = reader["TB016_BoletoDesc1"].ToString().TrimEnd();
                    obj.TB016_BoletoDesc2 = reader["TB016_BoletoDesc2"].ToString().TrimEnd();
                    obj.TB016_BoletoDesc3 = reader["TB016_BoletoDesc3"].ToString().TrimEnd();
                    obj.TB016_BoletoDesc4 = reader["TB016_BoletoDesc4"].ToString().TrimEnd();
                    obj.TB016_BoletoDesc5 = reader["TB016_BoletoDesc5"].ToString().TrimEnd();

                    MunicipioController objMunicipio = new MunicipioController();

                    objMunicipio.TB006_id = Convert.ToInt64(reader["TB006_id"]);
                    objMunicipio.TB006_Municipio = reader["TB006_Municipio"].ToString().TrimEnd();
                    obj.Municipio = objMunicipio;

                    EstadoController objEstado = new EstadoController();
                    objEstado.TB005_Sigla = reader["TB005_Sigla"].ToString().TrimEnd();
                    obj.Estado = objEstado;

                    BancoController objBanco = new BancoController();
                    objBanco.TB018_Banco = Convert.ToInt16(reader["TB018_Banco"]);
                    obj.Banco = objBanco;

                    EmpresaController objEmpresa = new EmpresaController();
                    objEmpresa.TB001_id = Convert.ToInt64(reader["TB001_id"]);
                    obj.Empresa = objEmpresa;

                    ContratosController objContrato = new ContratosController();
                    objContrato.TB012_Logradouro = reader["TB012_Logradouro"].ToString().TrimEnd();
                    objContrato.TB012_Numero = reader["TB012_Numero"].ToString().TrimEnd();
                    objContrato.TB012_Bairro = reader["TB012_Bairro"].ToString().TrimEnd();
                    objContrato.TB004_Cep = reader["TB004_Cep"].ToString().TrimEnd().Replace(".", "").Replace("-", "");
                    obj.Contrato = objContrato;

                    PessoaController objPessoa = new PessoaController();
                    objPessoa.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd();
                    //objPessoa.TB013_CPFCNPJ         = reader["TB013_CPFCNPJ"].ToString().TrimEnd();


                    if (Convert.ToInt16(reader["TB013_Tipo"]) == 1)
                    {
                        objPessoa.TB013_CPFCNPJ = reader["TB016_CPFCNPJ"].ToString().TrimEnd().PadLeft(11, '0');
                    }
                    else
                    {
                        objPessoa.TB013_CPFCNPJ = reader["TB016_CPFCNPJ"].ToString().TrimEnd().PadLeft(13, '0');
                    }

                    obj.Pessoa = objPessoa;


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

        public bool ParcelaVinculaComBoleto(ParcelaController BoletoParcela)
        {
            try
            {

                string UpdateSql = " UPDATE TB016_Parcela SET " +
                                   "  TB016_Status             =" + 2 +
                                   ", TB016_Emissao            ='" + Convert.ToDateTime(BoletoParcela.TB016_Emissao).ToString("yyyy/MM/dd") + "'" +
                                   ", TB016_Agencia            ='" + BoletoParcela.TB016_Agencia.Replace(" ", "") + "'" +
                                   ", TB016_ContaCorrente      ='" + BoletoParcela.TB016_ContaCorrente.Replace(" ", "") + "'" +
                                   ", TB016_Carteira           ='" + BoletoParcela.TB016_Carteira.Replace(" ", "") + "'" +
                                   ", TB016_EnderecoPagador    ='" + BoletoParcela.TB016_EnderecoPagador + "'" +
                                   ", TB016_Banco              ='" + BoletoParcela.TB016_Banco.Replace(" ", "") + "'" +
                                   ", TB016_NBoleto            ='" + BoletoParcela.TB016_NBoleto.Replace("'>", "") + "'" +
                                   ", TB016_NossoNumero        ='" + BoletoParcela.TB016_NossoNumero.Replace(" ", "") + "'" +
                                   ", TB016_Boleto             ='" + BoletoParcela.TB016_Boleto + "'" +
                                   " where TB016_id            =" + BoletoParcela.TB016_id;

                using (SqlConnection myConnection = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    myConnection.Open();

                    SqlCommand myCommand = new SqlCommand(UpdateSql, myConnection);
                    myCommand.CommandTimeout = 300;

                    myCommand.ExecuteScalar();
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
            return true;
        }

        public bool BaixaBoletoImportacao(ParcelaController Boleto)
        {
            try
            {
                string UpdateSql = " UPDATE TB016_Parcela SET " +
                                   " TB016_Status               =" + 5 +
                                   " ,TB016_NossoNumero      ='" + Boleto.TB016_NossoNumero + "'" +
                                   " ,TB016_DataPagamento       ='" + Boleto.TB016_DataPagamento + "'" +
                                   " ,TB016_ValorPago           =" + Boleto.TB016_ValorPago.ToString().Replace(",", ".") +
                                   " where TB016_id             =" + Boleto.TB016_id;

                using (SqlConnection myConnection = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    myConnection.Open();

                    SqlCommand myCommand = new SqlCommand(UpdateSql, myConnection);
                    myCommand.CommandTimeout = 300;

                    myCommand.ExecuteScalar();
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
            return true;
        }


        public bool atualizarnossonumero(long TB016_id, string TB016_NossoNumero, long TB016_AlteradoPor)
        {
            try
            {
                string UpdateSql = " UPDATE TB016_Parcela SET " +
                                   " TB016_NossoNumero      ='" + TB016_NossoNumero.Trim() + "'" +
                                   " ,TB016_AlteradoEm = '" + DateTime.Now.ToString("MM/dd/yyyy hh:mm") + "'" +
                                   " ,TB016_AlteradoPor = " + TB016_AlteradoPor + 
                                   " where TB016_id             =" + TB016_id;

                using (SqlConnection myConnection = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    myConnection.Open();

                    SqlCommand myCommand = new SqlCommand(UpdateSql, myConnection);
                    myCommand.CommandTimeout = 300;

                    myCommand.ExecuteScalar();
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
            return true;
        }

        public ParcelaController ParcelaPesquisaID(long TB016_id)
        {
            ParcelaController Retorno = new ParcelaController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

 

                sSQL.Append(" SELECT  ");
                sSQL.Append(" dbo.TB016_Parcela.TB012_CicloContrato ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_NossoNumero ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_ParcelaCancelamento ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_id ");
                sSQL.Append(" , dbo.TB015_Planos.TB015_id ");
                sSQL.Append(" , dbo.TB015_Planos.TB015_Plano ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Parcela ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Emissao ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Vencimento ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Pagador ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CPFCNPJ ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_EnderecoPagador ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_TipoSacado ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_FormaPagamento ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_ParcelasAgrupadas ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_EmitirBoleto ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_DataPagamento ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Banco ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Entrada ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_ValorAdesao ");
                sSQL.Append(" , dbo.TB016_Parcela.TB012_id ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Boleto ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Valor ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Status ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_Tipo ");
                sSQL.Append(" , dbo.TB013_Pessoa.TB013_id ");
                sSQL.Append(" , dbo.TB012_Contratos.TB002_id ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredNCartao ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredCPFTitularCartaoCartao ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredNomeTitularCartaoCartao ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredBandeira ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredNParcelas ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredValorParcelas ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredAutorizacao ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredCodValidador ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredFormaParamentoDescricao ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredFormaParamentoId ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredValor ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredDataCredito ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredDataCreditado ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_DiaVencimento ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Beneficiario ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_BeneficiarioCidade ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_BeneficiarioCPFCNPJ ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_BeneficiarioEndereco ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_BeneficiarioUF ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_PagadorCidade ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_PagadorUF ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_PagadorCEP ");
                sSQL.Append(" , dbo.TB015_Planos.TB015_Maiores ");
                sSQL.Append(" , dbo.TB015_Planos.TB015_Menores ");
                sSQL.Append(" , dbo.TB015_Planos.TB015_Isentos ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_ValorIOF ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_TipoVencimento ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_EspecieDocumento ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_BoletoDesc1 ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_BoletoDesc2 ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_BoletoDesc3 ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_BoletoDesc4 ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_BoletoDesc5 ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Multa ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Juros ");
                sSQL.Append(" , dbo.TB016_Parcela.TB037_Comissao ");
                sSQL.Append(" , dbo.TB016_Parcela.TB037_Id ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_DiaFechamento ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_ArquivoExportacao ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_LoteExportacao ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredConfirmacaoForma ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredConfirmacaoFeitaPor ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredBaixaFeitaPor ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CredBaixaFeitaEm ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_CodigoMovimento ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_ValorOutrosDesconto ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_ValorBruto ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_ValorTarifa ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_ValorTitulo ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_AgenciaRecebedora ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_BancoRecebedor ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Modalidade ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_DocumentoBanco ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_DataMovimentacao ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_NBoleto ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Carteira ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_ContaCorrente ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Agencia ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_ValorPago ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_Abatimento ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_IOF ");
                sSQL.Append(" , dbo.TB016_Parcela.TB013_id AS Expr1 ");
                sSQL.Append(" , dbo.TB016_Parcela.TB016_TotalParcelas ");
                sSQL.Append(" , dbo.TB016_Parcela.TB001_id ");
                sSQL.Append(" , dbo.TB016_Parcela.TB015_id AS Expr2 ");
                sSQL.Append(" , dbo.TB016_Parcela.TB002_ComissaoMensalidade ");
                sSQL.Append(" , dbo.TB016_Parcela.TB002_ComissaoAdesao ");
                sSQL.Append(" FROM             ");
                sSQL.Append(" dbo.TB016_Parcela  ");
                sSQL.Append(" INNER JOIN ");
                sSQL.Append(" dbo.TB015_Planos ON dbo.TB016_Parcela.TB015_id = dbo.TB015_Planos.TB015_id  ");
                sSQL.Append(" INNER JOIN ");
                sSQL.Append(" dbo.TB013_Pessoa ON dbo.TB016_Parcela.TB013_id = dbo.TB013_Pessoa.TB013_id ");
                sSQL.Append(" INNER JOIN ");
                sSQL.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id ");
                sSQL.Append(" WHERE ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_id =  ");
                sSQL.Append(TB016_id);



                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PessoaController objTitular     = new PessoaController();               
                    Retorno.Titular                 = objTitular;
                    Retorno.Titular.Estado          = new EstadoController();
                    Retorno.Titular.Municipio       = new MunicipioController();
                    Retorno.Titular.PontoDeVenda = new PontoDeVendaController();
                    Retorno.Empresa = new EmpresaController();

                    Retorno.Titular.TB013_id                        = Convert.ToInt64(reader["TB013_id"]);
                    Retorno.Titular.PontoDeVenda.TB002_id           = Convert.ToInt64(reader["TB002_id"]);
                    Retorno.Titular.Municipio.TB006_Municipio       = reader["TB016_PagadorCidade"].ToString().TrimEnd();
                    Retorno.Titular.Estado.TB005_Sigla              = reader["TB016_PagadorUF"].ToString().TrimEnd();
                    Retorno.TB012_CicloContrato                     = Convert.ToInt32(reader["TB012_CicloContrato"]);
                    Retorno.TB016_id                                = Convert.ToInt64(reader["TB016_id"]);                    
                    Retorno.TB016_Parcela                           = Convert.ToInt16(reader["TB016_Parcela"]);
                    Retorno.TB016_PagadorCidade                     = reader["TB016_PagadorCidade"].ToString().TrimEnd();
                    Retorno.TB016_PagadorUF                         = reader["TB016_PagadorUF"].ToString().TrimEnd();
                    Retorno.TB016_DiaVencimento                     = reader["TB016_DiaVencimento"] is DBNull ? Convert.ToInt16( Convert.ToDateTime( reader["TB016_Vencimento"]).Day) : Convert.ToInt16(reader["TB016_DiaVencimento"]);


                    //TB016_Vencimento
                    //   Convert.ToInt16(reader["TB016_DiaVencimento"]);
                    Retorno.TB016_Beneficiario                      = reader["TB016_Beneficiario"].ToString().TrimEnd();
                    Retorno.TB016_BeneficiarioCidade                = reader["TB016_BeneficiarioCidade"].ToString().TrimEnd();
                    Retorno.TB016_BeneficiarioCPFCNPJ               = reader["TB016_BeneficiarioCPFCNPJ"].ToString().Replace(".","").Replace(",", "").Replace("-", "").Replace("/", "").Trim();
                    Retorno.TB016_BeneficiarioEndereco              = reader["TB016_BeneficiarioEndereco"].ToString().TrimEnd();
                    Retorno.TB016_BeneficiarioUF                    = reader["TB016_BeneficiarioUF"].ToString().TrimEnd();                
                    Retorno.TB016_Emissao                           = Convert.ToDateTime(reader["TB016_Emissao"]);
                    Retorno.TB016_Vencimento                        = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    Retorno.TB016_Pagador                           = reader["TB016_Pagador"].ToString();
                    Retorno.TB016_CPFCNPJ                           = reader["TB016_CPFCNPJ"].ToString();
                    Retorno.TB016_EnderecoPagador                   = reader["TB016_EnderecoPagador"].ToString();
                    Retorno.TB016_PagadorCEP                        = reader["TB016_PagadorCEP"].ToString();
                    Retorno.TB016_TipoSacadoS                       = reader["TB016_TipoSacado"].ToString();
                    Retorno.TB016_Valor                             = Convert.ToDouble(reader["TB016_Valor"]);
                    Retorno.TB016_FormaPagamentoS                   = reader["TB016_FormaPagamento"].ToString();
                    Retorno.TB016_NossoNumero                       = reader["TB016_NossoNumero"] is DBNull ? "------" : reader["TB016_NossoNumero"].ToString();
                    Retorno.TB016_DataPagamento                     = reader["TB016_DataPagamento"] is DBNull ? DateTime.Now : Convert.ToDateTime(reader["TB016_DataPagamento"].ToString());
                    Retorno.TB016_ParcelaCancelamento               = reader["TB016_ParcelaCancelamento"] is DBNull ? 0 : Convert.ToInt16(reader["TB016_ParcelaCancelamento"]);
                    Retorno.TB016_ParcelasAgrupadas                 = reader["TB016_ParcelasAgrupadas"] is DBNull ? 1 : Convert.ToInt16(reader["TB016_ParcelasAgrupadas"]);
                    Retorno.TB016_EmitirBoleto                      = Convert.ToInt16(reader["TB016_EmitirBoleto"]);
                    Retorno.TB016_IOF                               = Convert.ToDouble(reader["TB016_IOF"]);
                    Retorno.TB016_TipoVencimento                    = Convert.ToInt16(reader["TB016_TipoVencimento"]);
                    Retorno.TB016_EspecieDocumento                  = reader["TB016_EspecieDocumento"].ToString().Trim();
                    Retorno.TB016_BoletoDesc1                       = reader["TB016_BoletoDesc1"].ToString().TrimEnd();
                    Retorno.TB016_BoletoDesc2                       = reader["TB016_BoletoDesc2"].ToString().TrimEnd();
                    Retorno.TB016_BoletoDesc3                       = reader["TB016_BoletoDesc3"].ToString().TrimEnd();
                    Retorno.TB016_BoletoDesc4                       = reader["TB016_BoletoDesc4"].ToString().TrimEnd();
                    Retorno.TB016_BoletoDesc5                       = reader["TB016_BoletoDesc5"].ToString().TrimEnd();
                    Retorno.TB016_Multa                             = Convert.ToDouble(reader["TB016_Multa"]);
                    Retorno.TB016_Juros                             = Convert.ToDouble(reader["TB016_Juros"]);      
                    Retorno.TB031_TipoVencimento                    = Convert.ToInt16(reader["TB016_TipoVencimento"]);
                    Retorno.TB037_Id                                = reader["TB037_Id"] is DBNull ? 0 : Convert.ToInt16(reader["TB037_Id"]);
                    Retorno.TB037_Comissao                          = reader["TB037_Comissao"] is DBNull ? 0 : Convert.ToDouble(reader["TB037_Comissao"]);
                    Retorno.TB015_Plano                             = reader["TB015_Plano"].ToString().TrimEnd();
                    Retorno.TB016_LoteExportacao                    = Convert.ToInt16(reader["TB016_LoteExportacao"]);
                    Retorno.TB016_Entrada                           = reader["TB016_Entrada"] is DBNull ? 0 : Convert.ToInt16(reader["TB016_Entrada"]);
                    Retorno.TB016_ValorAdesao                       = Convert.ToDouble(reader["TB016_ValorAdesao"]);
                    Retorno.TB016_ValorOutrosDesconto               = reader["TB016_ValorOutrosDesconto"] is DBNull ? 0 : Convert.ToDouble(reader["TB016_ValorOutrosDesconto"]);
                    Retorno.Empresa.TB001_id                        = Convert.ToInt64(reader["TB001_id"]);
                    if (Convert.ToInt16(Retorno.TB016_FormaPagamentoS) == 4)
                    {
                        Retorno.TB016_FormaPagamentoS = "6";
                    }
                    else
                    {
                        if (Convert.ToInt16(Retorno.TB016_FormaPagamentoS) == 5)
                        {
                            Retorno.TB016_FormaPagamentoS = "6";
                        }
                    }
                    Retorno.TB016_EmitirBoleto = Convert.ToInt16(reader["TB016_EmitirBoleto"]);
                    if (reader["TB016_DataPagamento"].ToString() == string.Empty)
                    {
                        DateTime DataNull = new DateTime(1900, 1, 1);
                        Retorno.TB016_DataPagamento = DataNull;
                    }
                    else
                    {
                        Retorno.TB016_DataPagamento = Convert.ToDateTime(reader["TB016_DataPagamento"]);
                    }
                    Retorno.TB016_Banco                 = reader["TB016_Banco"] is DBNull ? "NÃO INFORMADO" : reader["TB016_Banco"].ToString();
                    Retorno.TB016_Boleto                = reader["TB016_Boleto"] is DBNull ? "---" : reader["TB016_Boleto"].ToString();
                    Retorno.TB016_Entrada               = reader["TB016_Entrada"] is DBNull ? 0 : Convert.ToInt16(reader["TB016_Entrada"].ToString());              
                    Retorno.TB016_ValorAdesao           = Convert.ToDouble(reader["TB016_ValorAdesao"]);
                    Retorno.TB002_ComissaoAdesao        = reader["TB002_ComissaoAdesao"] is DBNull ? 0 : Convert.ToDouble(reader["TB002_ComissaoAdesao"]);
                    Retorno.TB016_StatusS                           = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(reader["TB016_Status"]));
                    Retorno.TB015_id                                = Convert.ToInt64(reader["TB015_id"]);
                    Retorno.TB015_Plano                             = reader["TB015_Plano"].ToString();
                    Retorno.TB012_id                                = Convert.ToInt64(reader["TB012_id"]);
                    Retorno.Plano = new PlanoController();
                    Retorno.Plano.TB015_Maiores                     = Convert.ToInt16(reader["TB015_Maiores"]);
                    Retorno.Plano.TB015_Menores                     = Convert.ToInt16(reader["TB015_Menores"]);
                    Retorno.Plano.TB015_Isentos                     = Convert.ToInt16(reader["TB015_Isentos"]);                         
                    Retorno.TB016_CredNCartao                       = reader["TB016_CredNCartao"] is DBNull ? "NÃO INFORMADO" : reader["TB016_CredNCartao"].ToString();
                    Retorno.TB016_CredCPFTitularCartaoCartao        = reader["TB016_CredCPFTitularCartaoCartao"] is DBNull ? reader["TB016_CPFCNPJ"].ToString() : reader["TB016_CredCPFTitularCartaoCartao"].ToString();
                    Retorno.TB016_CredNomeTitularCartaoCartao       = reader["TB016_CredNomeTitularCartaoCartao"] is DBNull ? reader["TB016_Pagador"].ToString() : reader["TB016_CredNomeTitularCartaoCartao"].ToString();
                    Retorno.TB016_CredBandeira                      = reader["TB016_CredBandeira"] is DBNull ? 0 : Convert.ToInt16(reader["TB016_CredBandeira"]);
                    Retorno.TB016_CredNParcelas                     = reader["TB016_CredNParcelas"] is DBNull ? 1 : Convert.ToInt16(reader["TB016_CredNParcelas"]);
                    Retorno.TB016_CredValorParcelas                 = reader["TB016_CredValorParcelas"] is DBNull ? Convert.ToDouble(reader["TB016_Valor"]) : Convert.ToDouble(reader["TB016_CredValorParcelas"]);
                    Retorno.TB016_CredAutorizacao                   = reader["TB016_CredAutorizacao"] is DBNull ? "NÃO INFORMADO" : reader["TB016_CredAutorizacao"].ToString();
                    Retorno.TB016_CredCodValidador                  = reader["TB016_CredCodValidador"] is DBNull ? "NÃO INFORMADO" : reader["TB016_CredCodValidador"].ToString();
                    Retorno.TB016_CredFormaParamentoDescricao       = reader["TB016_CredFormaParamentoDescricao"] is DBNull ? "boleto" : reader["TB016_CredFormaParamentoDescricao"].ToString();
                    Retorno.TB016_CredFormaParamentoId              = reader["TB016_CredFormaParamentoId"] is DBNull ? 0 : Convert.ToInt16(reader["TB016_CredFormaParamentoId"]);
                    Retorno.TB016_CredValor                         = reader["TB016_CredValor"] is DBNull ? Convert.ToDouble(reader["TB016_Valor"]) : Convert.ToDouble(reader["TB016_CredValor"]);
                    Retorno.TB016_CredDataCredito                   = reader["TB016_CredDataCredito"] is DBNull ? Convert.ToDateTime(reader["TB016_Vencimento"]) : Convert.ToDateTime(reader["TB016_CredDataCredito"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }


        public ParcelaController ParcelaProximaParcelaID(long TB012_id, long TB016_id)
        {
            ParcelaController Retorno = new ParcelaController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT TOP (1) TB012_id, TB016_id, TB016_Status FROM dbo.TB016_Parcela WHERE TB016_Status = 1 ");
                sSQL.Append(" and TB012_id =  ");
                sSQL.Append(TB012_id);
                sSQL.Append(" and TB016_id >  ");
                sSQL.Append(TB016_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    Retorno.TB016_StatusS = reader["TB016_Status"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public List<ParcelaController> ParcelasBoletos(string vQuery)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();


                sSQL.Append("SELECT  dbo.TB016_Parcela.TB016_id, dbo.TB016_Parcela.TB016_CPFCNPJ, dbo.TB016_Parcela.TB012_id, dbo.TB016_Parcela.TB016_Vencimento, dbo.TB016_Parcela.TB016_Status, dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_NomeCompleto FROM dbo.TB016_Parcela INNER JOIN dbo.TB013_Pessoa ON dbo.TB016_Parcela.TB013_id = dbo.TB013_Pessoa.TB013_id");
                sSQL.Append(vQuery);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj   = new ParcelaController();
                    obj.TB016_id            = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB016_CPFCNPJ       = reader["TB016_CPFCNPJ"].ToString().Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "");
                    if (obj.TB016_CPFCNPJ.Length == 11)
                    {
                        obj.TB016_CPFCNPJ   = Convert.ToUInt64(obj.TB016_CPFCNPJ).ToString(@"000\.000\.000\-00");
                    }
                    else
                    {
                        obj.TB016_CPFCNPJ   = Convert.ToUInt64(obj.TB016_CPFCNPJ).ToString(@"00\.000\.000\/0000\-00");
                    }

                    obj.TB012_id            = Convert.ToInt64(reader["TB012_id"]);
                    obj.TB016_Vencimento    = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    obj.TB016_Pagador       = reader["TB013_NomeCompleto"].ToString().TrimEnd();
                    obj.TB016_StatusS       = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(reader["TB016_Status"]));

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

        /// <summary>
        /// Descrição:  Cancelar parcelas de um contrato tambem cancelado
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       02/02/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool ParcelasCancelar(long TB016_id)
        {
            try
            {
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" UPDATE TB016_Parcela SET ");
                sSQL.Append(" TB016_Status = 3");
                sSQL.Append(" WHERE TB016_id = ");

                sSQL.Append(TB016_id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSQL.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;

        }

        /// <summary>
        /// Descrição:  Pesquisa do Produto da Parcela
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       02/02/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>

        public ParcelaProdutosController ParcelaProdutoPesquisaId(long TB017_id)
        {
            ParcelaProdutosController Retorno = new ParcelaProdutosController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT * from  TB017_ParcelaProduto ");
                sSQL.Append(" WHERE TB017_id = ");
                sSQL.Append(TB017_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB017_id = Convert.ToInt64(reader["TB017_id"]);
                    Retorno.TB017_Item = reader["TB017_Item"].ToString().TrimEnd();
                    Retorno.TB017_ValorUnitario = Convert.ToDouble(reader["TB017_ValorUnitario"]);
                    Retorno.TB017_ValorDesconto = Convert.ToDouble(reader["TB017_ValorDesconto"]);
                    Retorno.TB017_ValorFinal = Convert.ToDouble(reader["TB017_ValorFinal"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public List<ParcelaController> ParcelasEmAberto(long TB012_id)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append("SELECT TB016_id, ");
                sSQL.Append("TB012_id,  ");
                sSQL.Append("TB016_Parcela,  ");
                sSQL.Append("TB016_TotalParcelas, ");
                sSQL.Append("TB016_Emissao,  ");
                sSQL.Append("TB016_Vencimento,  ");
                sSQL.Append("TB016_Valor ");
                sSQL.Append("FROM  ");
                sSQL.Append("dbo.TB016_Parcela ");
                sSQL.Append("WHERE ");
                sSQL.Append("TB012_id =  ");
                sSQL.Append(TB012_id);
                sSQL.Append("AND ");
                sSQL.Append("TB016_Status < 3 ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    obj.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);
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

        public List<ParcelaController> GravarCodigoBarraCarne(long TB012_id)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT TOP (100) PERCENT TB016_id, TB012_id, TB016_NBoleto, TB016_Status ");
                sSQL.Append(" FROM dbo.TB016_Parcela ");
                sSQL.Append(" WHERE(TB016_Status = 2) AND(NOT(TB016_NBoleto IS NULL)) AND TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append(" ORDER BY TB016_id");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB016_NBoleto = reader["TB016_NBoleto"].ToString();

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
        public List<ParcelaController> ParcelasVencidasContrato(long TB012_id, DateTime Vencimento)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                //DataInicio = DataInicio.AddDays(-2);
                //ateTime DataRefe = Convert.ToDateTime("1/" + DataInicio.Month.ToString() + "/" + DataInicio.Year.ToString() + " 00:00:00");
                sSQL.Append("SELECT TB016_id, TB012_id, TB016_Vencimento, TB016_Status FROM dbo.TB016_Parcela WHERE TB012_id = ");
                sSQL.Append(TB012_id);

                sSQL.Append(" AND TB016_Status < 5");
                sSQL.Append(" AND TB016_Status <> 3");
                sSQL.Append(" AND TB016_Vencimento <= CONVERT(DATETIME, '");
                sSQL.Append(Vencimento.ToString("MM/dd/yyyy"));
                sSQL.Append("', 102)");


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    //obj.TB012_id = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"].ToString());
                    //obj.TB016_Parcela = Convert.ToInt16(reader["TB016_Parcela"]);

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

        /// <summary>
        /// Descrição:  Recupera parcela para pagamento no caixa
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       26/06/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public ParcelaController ParcelaPagamento(long TB016_id)
        {
            ParcelaController Retorno = new ParcelaController();
            PessoaController Pessoa_P = new PessoaController();
            PlanoController Plano_P = new PlanoController();
            EmpresaController Empresa_P = new EmpresaController();

            Retorno.Empresa = Empresa_P;
            Retorno.Pessoa = Pessoa_P;
            Retorno.Plano = Plano_P;

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB016_Parcela.TB016_ParcelaCancelamento, dbo.TB016_Parcela.TB016_id, dbo.TB016_Parcela.TB016_FormaPagamento, dbo.TB016_Parcela.TB016_Pagador, dbo.TB016_Parcela.TB016_CPFCNPJ, dbo.TB016_Parcela.TB016_Parcela,  ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Emissao, dbo.TB016_Parcela.TB016_Vencimento, dbo.TB016_Parcela.TB016_Valor, dbo.TB016_Parcela.TB016_ValorPago, dbo.TB015_Planos.TB015_Plano, ");
                sSQL.Append(" dbo.TB012_Contratos.TB002_id, dbo.TB013_Pessoa.TB013_Tipo, dbo.TB016_Parcela.TB012_id, dbo.TB002_PontosDeVenda.TB001_id ");
                sSQL.Append(" FROM dbo.TB016_Parcela INNER JOIN ");
                sSQL.Append(" dbo.TB015_Planos ON dbo.TB016_Parcela.TB015_id = dbo.TB015_Planos.TB015_id INNER JOIN ");
                sSQL.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id INNER JOIN ");
                sSQL.Append(" dbo.TB013_Pessoa ON dbo.TB016_Parcela.TB013_id = dbo.TB013_Pessoa.TB013_id INNER JOIN ");
                sSQL.Append(" dbo.TB002_PontosDeVenda ON dbo.TB012_Contratos.TB002_id = dbo.TB002_PontosDeVenda.TB002_id");
                sSQL.Append(" WHERE dbo.TB016_Parcela.TB016_id =  ");
                sSQL.Append(TB016_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB016_FormaPagamentoS = reader["TB016_FormaPagamento"].ToString();
                    if (Retorno.TB016_FormaPagamentoS == "4")
                    {
                        Retorno.TB016_FormaPagamentoS = "6";
                    }
                    else
                    {
                        if (Retorno.TB016_FormaPagamentoS == "5")
                        {
                            Retorno.TB016_FormaPagamentoS = "6";
                        }
                    }

                    Retorno.Pessoa.TB013_TipoS = reader["TB013_Tipo"].ToString();

                    Retorno.TB016_ParcelaCancelamento = reader["TB016_ParcelaCancelamento"] is DBNull ? 0 : Convert.ToInt16(reader["TB016_ParcelaCancelamento"].ToString());


                    string TB016_CPFCNPJ = reader["TB016_CPFCNPJ"].ToString().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim();
                    if (Retorno.Pessoa.TB013_TipoS == "1")
                    {
                        Retorno.TB016_CPFCNPJ = reader["TB016_CPFCNPJ"] is DBNull ? "SEM CPF" : Convert.ToUInt64(TB016_CPFCNPJ.ToString().TrimEnd().TrimStart()).ToString(@"000\.000\.000\-00");
                    }
                    else
                    {
                        Retorno.TB016_CPFCNPJ = Convert.ToUInt64(TB016_CPFCNPJ.TrimEnd().TrimStart()).ToString(@"00\.000\.000\/0000\-00");
                    }

                    Retorno.TB016_Pagador = reader["TB016_Pagador"].ToString().ToUpper().TrimEnd();
                    Retorno.Plano.TB015_Plano = reader["TB015_Plano"].ToString().TrimEnd();
                    Retorno.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    Retorno.TB016_Parcela = Convert.ToInt16(reader["TB016_Parcela"]);
                    Retorno.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    Retorno.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    Retorno.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);
                    Retorno.TB012_id = Convert.ToInt64(reader["TB012_id"]);
                    Retorno.Empresa.TB001_id = Convert.ToInt64(reader["TB001_id"]);

                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }
        public List<ParcelaController> ListarBandeiraCartao()
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT * FROM dbo.TB032_BandeiraCartao ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                ParcelaController obj0 = new ParcelaController();
                obj0.TB032_Id = 0;
                obj0.TB032_BandeiraCartao = "SELECIONE";
                Retorno.Add(obj0);

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB032_Id = Convert.ToInt64(reader["TB032_Id"]);
                    obj.TB032_BandeiraCartao = reader["TB032_BandeiraCartao"].ToString().ToUpper().TrimEnd();

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
        public List<ParcelaController> ListaParcelamentoPossivelPorBandeira(long TB032_Id, long TB001_id)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT TB031_Id, TB032_Id, TB001_id, TB031_Status, TB031_NParcelas FROM dbo.TB031_FormaParamento ");
                sSQL.Append(" WHERE TB031_Status = 1");
                sSQL.Append(" and  TB032_Id = ");
                sSQL.Append(TB032_Id);
                sSQL.Append("and  TB001_id = ");
                sSQL.Append(TB001_id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                ParcelaController obj0 = new ParcelaController();
                obj0.TB031_Id = 0;
                obj0.TB031_NParcelas = 0;
                Retorno.Add(obj0);

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB031_Id = Convert.ToInt64(reader["TB031_Id"]);
                    obj.TB031_NParcelas = Convert.ToInt16(reader["TB031_NParcelas"]);

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
        public ParcelaController SelectFormaParamento(long TB031_Id)
        {
            ParcelaController Retorno = new ParcelaController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT * FROM TB031_FormaParamento");
                sSQL.Append(" WHERE TB031_Id =  ");
                sSQL.Append(TB031_Id);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB031_Id = Convert.ToInt64(reader["TB031_Id"]);
                    Retorno.TB031_ValorMinimoParcela = Convert.ToDouble(reader["TB031_ValorMinimoParcela"]);
                    Retorno.TB031_TipoVencimento = Convert.ToInt16(reader["TB031_TipoVencimento"]);
                    Retorno.TB031_DVencimento = Convert.ToInt16(reader["TB031_DVencimento"]);
                    Retorno.TB031_Descricao = reader["TB031_Descricao"].ToString().TrimEnd().ToUpper();
                    Retorno.TB031_Taxa = Convert.ToDouble(reader["TB031_Taxa"]);



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
        /// Descrição:  Computar paramento via cartão da parcela, por segurança esta rotina utiliza Transaction para
        ///             garantir a integridade das duas tabelas envolvidas TB016_Parcela, TB033_PagamentoCartao,
        ///             TB013_Pessoa e TB012_Contratos
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       27/06/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool ParcelaInserirPagamentoCredParcela(ParcelaController Pagamento, int Cancelamento)
        {
            string TB016_CredValorParcelas = Pagamento.TB016_CredValorParcelas.ToString();
            TB016_CredValorParcelas = TB016_CredValorParcelas.Replace(".", "");
            TB016_CredValorParcelas = TB016_CredValorParcelas.Replace(",", ".");

            string TB016_ValorPago = Pagamento.TB016_ValorPago.ToString();
            TB016_ValorPago = TB016_ValorPago.Replace(".", "");
            TB016_ValorPago = TB016_ValorPago.Replace(",", ".");

            string TB016_CredValor = Pagamento.TB016_CredValor.ToString();
            TB016_CredValor = TB016_CredValor.Replace(".", "");
            TB016_CredValor = TB016_CredValor.Replace(",", ".");

            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

            SqlCommand cmdTB012 = con.CreateCommand();
            SqlCommand cmdTB013 = con.CreateCommand();
            SqlCommand cmdTB016 = con.CreateCommand();
          

            StringBuilder sSQLTB012 = new StringBuilder();
            sSQLTB012.Append("UPDATE TB012_Contratos SET ");
            if (Cancelamento == 0)
            {
                sSQLTB012.Append("TB012_Status = 1");
                sSQLTB012.Append(" where TB012_id =");
                sSQLTB012.Append(Pagamento.TB012_id);
                sSQLTB012.Append(" and TB012_Status = 0 or TB012_Status = 4");
            }
            else
            {
                sSQLTB012.Append("TB012_Status = 5");
                sSQLTB012.Append(" where TB012_id =");
                sSQLTB012.Append(Pagamento.TB012_id);
            }

            SqlCommand cmdTB020 = con.CreateCommand();
            StringBuilder sSQLTB020 = new StringBuilder();
            sSQLTB020.Append("UPDATE TB020_Unidades SET ");
            if (Cancelamento == 0)
            {
                sSQLTB020.Append("TB020_Status = 1");
                sSQLTB020.Append(",TB020_AlteradoEm = ");
                sSQLTB020.Append("'");
                sSQLTB020.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSQLTB020.Append("'");
                sSQLTB020.Append(",TB020_AlteradoPor=");
                sSQLTB020.Append(Pagamento.TB016_CredBaixaFeitaPor);
                sSQLTB020.Append(" where TB012_id =");
                sSQLTB020.Append(Pagamento.TB012_id);
                sSQLTB020.Append(" and TB020_Status = 0");
            }


            StringBuilder sSQLTB013 = new StringBuilder();
            sSQLTB013.Append("SELECT dbo.TB012_Contratos.TB012_id, dbo.TB012_Contratos.TB012_Status, dbo.TB013_Pessoa.TB013_id, dbo.TB013_Pessoa.TB013_Cartao, dbo.TB006_Municipio.TB006_Codigo, ");
            sSQLTB013.Append(" dbo.TB013_Pessoa.TB013_CarteirinhaStatus, dbo.TB013_Pessoa.TB013_CodigoDependente ");
            sSQLTB013.Append(" FROM dbo.TB012_Contratos INNER JOIN ");
            sSQLTB013.Append(" dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB012_id = dbo.TB013_Pessoa.TB012_id INNER JOIN ");
            sSQLTB013.Append(" dbo.TB006_Municipio ON dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id ");
            sSQLTB013.Append(" WHERE(NOT(dbo.TB006_Municipio.TB006_Codigo IS NULL)) AND (dbo.TB013_Pessoa.TB013_CarteirinhaStatus = 0) AND (dbo.TB013_Pessoa.TB013_Status = 0) AND ");
            sSQLTB013.Append(" dbo.TB012_Contratos.TB012_id = ");
            sSQLTB013.Append(Pagamento.TB012_id);
            sSQLTB013.Append(" OR ");
            sSQLTB013.Append(" dbo.TB013_Pessoa.TB013_Status = 1");

            StringBuilder sSQLTB016 = new StringBuilder();
            sSQLTB016.Append("UPDATE TB016_Parcela SET ");
            sSQLTB016.Append("TB016_DataPagamento = ");
            sSQLTB016.Append("'");
            sSQLTB016.Append(Pagamento.TB016_DataPagamento.ToString("MM/dd/yyyy"));
            sSQLTB016.Append("'");
            sSQLTB016.Append(",TB016_ValorPago = ");
            sSQLTB016.Append(TB016_ValorPago);
            sSQLTB016.Append(",TB016_CredValor = ");
            sSQLTB016.Append(TB016_CredValor);
            sSQLTB016.Append(",TB016_FormaProcessamentoBaixa = ");
            sSQLTB016.Append(Pagamento.TB016_FormaProcessamentoBaixa);
            sSQLTB016.Append(",TB016_CredNCartao =");
            sSQLTB016.Append("'");
            sSQLTB016.Append(Pagamento.TB016_CredNCartao.Trim());
            sSQLTB016.Append("'");
            sSQLTB016.Append(",TB016_CredCPFTitularCartaoCartao =");
            sSQLTB016.Append("'");
            sSQLTB016.Append(Pagamento.TB016_CredCPFTitularCartaoCartao.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim());
            sSQLTB016.Append("'");
            sSQLTB016.Append(",TB016_CredNomeTitularCartaoCartao =");
            sSQLTB016.Append("'");
            sSQLTB016.Append(Pagamento.TB016_CredNomeTitularCartaoCartao.TrimEnd());
            sSQLTB016.Append("'");
            sSQLTB016.Append(",TB016_CredBandeira =");
            sSQLTB016.Append(Pagamento.TB016_CredBandeira);
            sSQLTB016.Append(",TB016_CredNParcelas =");
            sSQLTB016.Append(Pagamento.TB016_CredNParcelas);
            sSQLTB016.Append(",TB016_CredValorParcelas =");
            sSQLTB016.Append(TB016_CredValorParcelas);
            sSQLTB016.Append(",TB016_CredAutorizacao =");
            sSQLTB016.Append("'");
            sSQLTB016.Append(Pagamento.TB016_CredAutorizacao.Trim());
            sSQLTB016.Append("'");
            sSQLTB016.Append(",TB016_CredCodValidador =");
            sSQLTB016.Append("'");
            sSQLTB016.Append(Pagamento.TB016_CredCodValidador.Trim());
            sSQLTB016.Append("'");
            sSQLTB016.Append(",TB016_CredFormaParamentoId =");
            sSQLTB016.Append(Pagamento.TB016_CredFormaParamentoId);
            sSQLTB016.Append(",TB016_CredFormaParamentoDescricao =");
            sSQLTB016.Append("'");
            sSQLTB016.Append(Pagamento.TB016_CredFormaParamentoDescricao.TrimEnd().ToUpper());
            sSQLTB016.Append("'");
            sSQLTB016.Append(",TB016_AlteradoEm =");
            sSQLTB016.Append("'");
            sSQLTB016.Append(Pagamento.TB016_AlteradoEm.ToString("MM/dd/yyyy hh:mm"));
            sSQLTB016.Append("'");
            sSQLTB016.Append(",TB016_AlteradoPor =");
            sSQLTB016.Append(Pagamento.TB016_AlteradoPor);
            sSQLTB016.Append(",TB016_CredBaixaFeitaEm =");
            sSQLTB016.Append("'");
            sSQLTB016.Append(Pagamento.TB016_CredBaixaFeitaEm.ToString("MM/dd/yyyy hh:mm"));
            sSQLTB016.Append("'");
            sSQLTB016.Append(",TB016_CredBaixaFeitaPor =");
            sSQLTB016.Append(Pagamento.TB016_CredBaixaFeitaPor);
            sSQLTB016.Append(",TB016_Status =");
            sSQLTB016.Append(Pagamento.TB016_StatusS);
            sSQLTB016.Append(",TB016_CredDataCredito =");
            sSQLTB016.Append("'");
            sSQLTB016.Append(Pagamento.TB016_CredDataCredito.ToString("MM/dd/yyyy"));
            sSQLTB016.Append("'");
            sSQLTB016.Append(" where TB016_id = ");
            sSQLTB016.Append(Pagamento.TB016_id);

            cmdTB012.CommandText = sSQLTB012.ToString();
            cmdTB013.CommandText = sSQLTB013.ToString();
            cmdTB016.CommandText = sSQLTB016.ToString();
            cmdTB020.CommandText = sSQLTB020.ToString();

            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmdTB012.Transaction = tran;
                cmdTB012.ExecuteNonQuery();
                //Comando 1 executado com sucesso!

                cmdTB013.Transaction = tran;
                cmdTB013.ExecuteNonQuery();
                //Comando 2 executado com sucesso!

                cmdTB016.Transaction = tran;
                cmdTB016.ExecuteNonQuery();
                //Comando 3 executado com sucesso!

                cmdTB020.Transaction = tran;
                cmdTB020.ExecuteNonQuery();
                //Comando 4 executado com sucesso!

                tran.Commit();
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                return false;
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return true;
        }

        public List<ParcelaController> ParcelasListarParaBaixa(int TB016_Status, long TB012_id)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT TB016_id, TB012_id, TB016_Parcela, TB016_CPFCNPJ, TB016_Pagador, TB016_Emissao, TB016_Vencimento, TB016_Valor, TB016_FormaPagamento, TB016_Status FROM dbo.TB016_Parcela ");
                sSQL.Append(" WHERE TB016_Status = ");
                sSQL.Append(TB016_Status);
                if (TB012_id > 0)
                {
                    sSQL.Append(" AND TB012_id = ");
                    sSQL.Append(TB012_id);
                }
                sSQL.Append(" ORDER BY TB012_id, TB016_id ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB012_id = Convert.ToInt64(reader["TB012_id"]);
                    obj.TB016_Parcela = Convert.ToInt16(reader["TB016_Parcela"]);
                    obj.TB016_CPFCNPJ = reader["TB016_CPFCNPJ"].ToString().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "");

                    if (obj.TB016_CPFCNPJ.Length == 11)
                    {
                        obj.TB016_CPFCNPJ = Convert.ToUInt64(obj.TB016_CPFCNPJ).ToString(@"000\.000\.000\-00");
                    }
                    else
                    {
                        obj.TB016_CPFCNPJ = Convert.ToUInt64(obj.TB016_CPFCNPJ).ToString(@"00\.000\.000\/0000\-00");
                    }
                    obj.TB016_Pagador = reader["TB016_Pagador"].ToString();
                    obj.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    obj.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);
                    obj.TB016_StatusS = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(reader["TB016_Status"]));
                    obj.TB016_FormaPagamentoS = Enum.GetName(typeof(ParcelaController.TB016_FormaPagamentoE), Convert.ToInt16(reader["TB016_FormaPagamento"]));


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

        public List<ParcelaProdutosController> ListaProdutosPorParcela(long TB016_id)
        {
            List<ParcelaProdutosController> Retorno = new List<ParcelaProdutosController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();



                sSQL.Append(" SELECT * ");
                sSQL.Append(" FROM dbo.TB017_ParcelaProduto ");
                sSQL.Append(" WHERE TB016_id = ");
                sSQL.Append(TB016_id);




                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaProdutosController obj = new ParcelaProdutosController();
                    obj.TB017_id = Convert.ToInt64(reader["TB017_id"]);
                    obj.TB017_Item = reader["TB017_Item"].ToString().TrimEnd();
                    obj.TB017_ValorUnitario = Convert.ToDouble(reader["TB017_ValorUnitario"]);
                    obj.TB017_ValorDesconto = Convert.ToDouble(reader["TB017_ValorDesconto"]);
                    obj.TB017_ValorFinal = Convert.ToDouble(reader["TB017_ValorFinal"]);

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

        public ParcelaController ProximaParcela(long TB016_id, long TB012_id, int TB016_Status)
        {
            ParcelaController Retorno = new ParcelaController();
            Retorno.Plano = new PlanoController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT TOP (1) dbo.TB016_Parcela.TB016_id, dbo.TB016_Parcela.TB012_id, dbo.TB016_Parcela.TB016_Status, dbo.TB016_Parcela.TB016_Parcela, dbo.TB016_Parcela.TB016_Emissao, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Vencimento, dbo.TB015_Planos.TB015_id, dbo.TB015_Planos.TB015_Plano ");
                sSQL.Append(" FROM dbo.TB016_Parcela INNER JOIN ");
                sSQL.Append(" dbo.TB015_Planos ON dbo.TB016_Parcela.TB015_id = dbo.TB015_Planos.TB015_id");
                sSQL.Append(" where dbo.TB016_Parcela.TB016_id > ");
                sSQL.Append(TB016_id);
                sSQL.Append(" and dbo.TB016_Parcela.TB012_id =  ");
                sSQL.Append(TB012_id);
                sSQL.Append(" and dbo.TB016_Parcela.TB016_Status =  ");
                sSQL.Append(TB016_Status);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    Retorno.TB016_Parcela = Convert.ToInt16(reader["TB016_Parcela"]);
                    //Retorno.TB031_TipoVencimento    = Convert.ToInt16(reader["TB031_TipoVencimento"]);
                    Retorno.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    Retorno.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    Retorno.Plano.TB015_id = Convert.ToInt64(reader["TB015_id"]);
                    Retorno.Plano.TB015_Plano = reader["TB015_Plano"].ToString().TrimEnd().ToUpper();

                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public bool AlterarPlanoParcela(long TB016_id, long TB015_id, long TB011_Id, List<ProdutoController> Produtos)
        {

            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

            SqlCommand cmdDeleteTB017 = con.CreateCommand();
            SqlCommand cmdInsertTB017 = con.CreateCommand();
            SqlCommand cmdUpdateTB016 = con.CreateCommand();

            StringBuilder sSQLDeleteTB017 = new StringBuilder();
            sSQLDeleteTB017.Append("delete from TB017_ParcelaProduto ");
            sSQLDeleteTB017.Append(" where TB016_id =");
            sSQLDeleteTB017.Append(TB016_id);


            StringBuilder sSQLInsertTB017 = new StringBuilder();
            sSQLInsertTB017.Append("INSERT INTO TB017_ParcelaProduto (TB017_IdProteus,TB016_id,TB017_Item,TB017_ValorUnitario,TB017_ValorDesconto,TB017_ValorFinal) VALUES ");

            int Linha = 0;
            double ValorParcela = 0;
            foreach (ProdutoController Produto in Produtos)
            {
                ValorParcela = ValorParcela + Produto.TB014_ValorUnitario;
                if (Linha > 0)
                {
                    sSQLInsertTB017.Append(",");

                }
                Linha = 1;
                string TB014_ValorUnitario = Produto.TB014_ValorUnitario.ToString().Replace(".", "");
                TB014_ValorUnitario = TB014_ValorUnitario.Replace(",", ".");

                sSQLInsertTB017.Append("(");
                sSQLInsertTB017.Append("'");
                sSQLInsertTB017.Append(Produto.TB014_IdProtheus.TrimEnd());
                sSQLInsertTB017.Append("'");
                sSQLInsertTB017.Append(",");
                sSQLInsertTB017.Append(TB016_id);
                sSQLInsertTB017.Append(",");
                sSQLInsertTB017.Append("'");
                sSQLInsertTB017.Append(Produto.TB014_Descricao.TrimEnd());
                sSQLInsertTB017.Append("'");
                sSQLInsertTB017.Append(",");
                sSQLInsertTB017.Append(TB014_ValorUnitario);
                sSQLInsertTB017.Append(",");
                sSQLInsertTB017.Append(0);
                sSQLInsertTB017.Append(",");
                sSQLInsertTB017.Append(TB014_ValorUnitario);
                sSQLInsertTB017.Append(")");
            }

            StringBuilder sSQLUpdateTB016 = new StringBuilder();

            string vValorParcela = ValorParcela.ToString().Replace(".", "");
            vValorParcela = vValorParcela.Replace(",", ".");

            sSQLUpdateTB016.Append("UPDATE TB016_Parcela SET ");
            sSQLUpdateTB016.Append("TB015_id = ");
            sSQLUpdateTB016.Append(TB015_id);
            sSQLUpdateTB016.Append(",TB016_Valor = ");
            sSQLUpdateTB016.Append(vValorParcela);
            sSQLUpdateTB016.Append(",TB016_AlteradoEm =");
            sSQLUpdateTB016.Append("'");
            sSQLUpdateTB016.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            sSQLUpdateTB016.Append("'");
            sSQLUpdateTB016.Append(",TB016_AlteradoPor =");
            sSQLUpdateTB016.Append(TB011_Id);
            sSQLUpdateTB016.Append(",TB016_CredBaixaFeitaEm =");
            sSQLUpdateTB016.Append("'");
            sSQLUpdateTB016.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            sSQLUpdateTB016.Append("'");
            sSQLUpdateTB016.Append(" where TB016_id =");
            sSQLUpdateTB016.Append(TB016_id);

            cmdDeleteTB017.CommandText = sSQLDeleteTB017.ToString();
            cmdInsertTB017.CommandText = sSQLInsertTB017.ToString();
            cmdUpdateTB016.CommandText = sSQLUpdateTB016.ToString();

            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmdDeleteTB017.Transaction = tran;
                cmdDeleteTB017.ExecuteNonQuery();
                //Comando 1 executado com sucesso!

                cmdInsertTB017.Transaction = tran;
                cmdInsertTB017.ExecuteNonQuery();
                //Comando 2 executado com sucesso!

                cmdUpdateTB016.Transaction = tran;
                cmdUpdateTB016.ExecuteNonQuery();
                //Comando 3 executado com sucesso!

                tran.Commit();
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                return false;
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return true;
        }

        public bool ParcelasUnir(long DeleteTB016_id, long UpdadeTB016_id, double TB016_Valor, long TB011_Id)
        {
            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

            SqlCommand cmdDeleteTB016 = con.CreateCommand();
            StringBuilder sSQLDeleteTB016 = new StringBuilder();
            sSQLDeleteTB016.Append("delete from TB016_Parcela where TB016_id =");
            sSQLDeleteTB016.Append(DeleteTB016_id);

            SqlCommand cmdUpdadeTB017 = con.CreateCommand();
            StringBuilder sSQLUpdadeTB017 = new StringBuilder();
            sSQLUpdadeTB017.Append(" UPDATE  TB017_ParcelaProduto set  ");
            sSQLUpdadeTB017.Append(" TB016_id =");
            sSQLUpdadeTB017.Append(UpdadeTB016_id);
            sSQLUpdadeTB017.Append(" where TB016_id = ");
            sSQLUpdadeTB017.Append(DeleteTB016_id);


            SqlCommand cmdUpdateTB016 = con.CreateCommand();
            StringBuilder sSQLUpdateTB016 = new StringBuilder();
            sSQLUpdateTB016.Append("UPDATE  TB016_Parcela set  ");
            string vTB016_Valor = TB016_Valor.ToString().Replace(".", "");
            vTB016_Valor = vTB016_Valor.Replace(",", ".");
            sSQLUpdateTB016.Append(" TB016_Valor  =");
            sSQLUpdateTB016.Append(vTB016_Valor);
            sSQLUpdateTB016.Append(", TB016_AlteradoEm  =");
            sSQLUpdateTB016.Append("'");
            sSQLUpdateTB016.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            sSQLUpdateTB016.Append("'");
            sSQLUpdateTB016.Append(", TB016_AlteradoPor  =");
            sSQLUpdateTB016.Append(TB011_Id);
            sSQLUpdateTB016.Append(" where TB016_id =");
            sSQLUpdateTB016.Append(UpdadeTB016_id);


            cmdDeleteTB016.CommandText = sSQLDeleteTB016.ToString();
            cmdUpdadeTB017.CommandText = sSQLUpdadeTB017.ToString();
            cmdUpdateTB016.CommandText = sSQLUpdateTB016.ToString();

            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmdDeleteTB016.Transaction = tran;
                cmdDeleteTB016.ExecuteNonQuery();
                //Comando 1 executado com sucesso!

                cmdUpdadeTB017.Transaction = tran;
                cmdUpdadeTB017.ExecuteNonQuery();
                //Comando 2 executado com sucesso!

                cmdUpdateTB016.Transaction = tran;
                cmdUpdateTB016.ExecuteNonQuery();
                //Comando 3 executado com sucesso!




                tran.Commit();
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                return false;
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return true;
        }

        public bool ParcelaAlterarDataEmissao(long TB016_id, long TB011_Id, DateTime TB016_Emissao)
        {
            try
            {
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" UPDATE TB016_Parcela SET ");
                sSQL.Append(" TB016_AlteradoEm = ");
                sSQL.Append("'");
                sSQL.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSQL.Append("'");
                sSQL.Append(", TB016_AlteradoPor = ");
                sSQL.Append(TB011_Id);

                sSQL.Append(", TB016_Emissao = ");
                sSQL.Append("'");
                sSQL.Append(TB016_Emissao.ToString("MM/dd/yyyy hh:mm"));
                sSQL.Append("'");

                sSQL.Append(" WHERE TB016_id = ");
                sSQL.Append(TB016_id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSQL.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool ParcelaAlterarDataVencimento(long TB016_id, long TB011_Id, DateTime TB016_Vencimento)
        {
            try
            {
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" UPDATE TB016_Parcela SET ");
                sSQL.Append(" TB016_AlteradoEm = ");
                sSQL.Append("'");
                sSQL.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSQL.Append("'");
                sSQL.Append(", TB016_AlteradoPor = ");
                sSQL.Append(TB011_Id);

                sSQL.Append(", TB016_Vencimento = ");
                sSQL.Append("'");
                sSQL.Append(TB016_Vencimento.ToString("MM/dd/yyyy hh:mm"));
                sSQL.Append("'");

                sSQL.Append(" WHERE TB016_id = ");
                sSQL.Append(TB016_id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSQL.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool ParcelaAlterarValorAdesao(long TB016_id, long TB011_Id, double TB016_ValorAdesao)
        {
            try
            {


                string vTB016_ValorAdesao = TB016_ValorAdesao.ToString().Replace(".", "");
                vTB016_ValorAdesao = vTB016_ValorAdesao.Replace(",", ".");

                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" UPDATE TB016_Parcela SET ");
                sSQL.Append(" TB016_AlteradoEm = ");
                sSQL.Append("'");
                sSQL.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSQL.Append("'");
                sSQL.Append(", TB016_AlteradoPor = ");
                sSQL.Append(TB011_Id);

                sSQL.Append(", TB016_ValorAdesao = ");
                sSQL.Append(vTB016_ValorAdesao);


                sSQL.Append(" WHERE TB016_id = ");
                sSQL.Append(TB016_id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSQL.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool ParcelaAlterarDataVPagamento(long TB016_id, long TB011_Id, DateTime TB016_DataPagamento)
        {
            try
            {
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" UPDATE TB016_Parcela SET ");
                sSQL.Append(" TB016_AlteradoEm = ");
                sSQL.Append("'");
                sSQL.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSQL.Append("'");
                sSQL.Append(", TB016_AlteradoPor = ");
                sSQL.Append(TB011_Id);

                sSQL.Append(", TB016_DataPagamento = ");
                sSQL.Append("'");
                sSQL.Append(TB016_DataPagamento.ToString("MM/dd/yyyy hh:mm"));
                sSQL.Append("'");

                sSQL.Append(" WHERE TB016_id = ");
                sSQL.Append(TB016_id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSQL.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool ParcelaAlterarFormaPagamento(long TB016_id, long TB011_Id, Int16 TB016_FormaPagamento)
        {
            try
            {
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" UPDATE TB016_Parcela SET ");
                sSQL.Append(" TB016_AlteradoEm = ");
                sSQL.Append("'");
                sSQL.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSQL.Append("'");
                sSQL.Append(", TB016_AlteradoPor = ");
                sSQL.Append(TB011_Id);

                sSQL.Append(", TB016_FormaPagamento = ");
                sSQL.Append(TB016_FormaPagamento);

                sSQL.Append(" WHERE TB016_id = ");
                sSQL.Append(TB016_id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSQL.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool ParcelaProdutoDescricaoItem(long TB017_id, string TB017_Item)
        {
            try
            {
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" UPDATE TB017_ParcelaProduto SET ");


                sSQL.Append(" TB017_Item = ");
                sSQL.Append(TB017_Item.TrimEnd());

                sSQL.Append(" WHERE TB017_id = ");
                sSQL.Append(TB017_id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSQL.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool ParcelaProdutoAlterarDesconto(long TB017_id, double TB017_ValorUnitario, double TB017_ValorDesconto, double TB017_ValorFinal)
        {
            try
            {
                string vTB017_ValorUnitario = TB017_ValorUnitario.ToString().Replace(".", "");
                vTB017_ValorUnitario = vTB017_ValorUnitario.Replace(",", ".");

                string vTB017_ValorFinal = TB017_ValorFinal.ToString().Replace(".", "");
                vTB017_ValorFinal = vTB017_ValorFinal.Replace(",", ".");

                string vTB017_ValorDesconto = TB017_ValorDesconto.ToString().Replace(".", "");
                vTB017_ValorDesconto = vTB017_ValorDesconto.Replace(",", ".");


                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" UPDATE TB017_ParcelaProduto SET ");
                sSQL.Append(" TB017_ValorUnitario = ");
                sSQL.Append(vTB017_ValorUnitario);
                sSQL.Append(", TB017_ValorDesconto = ");
                sSQL.Append(vTB017_ValorDesconto);
                sSQL.Append(",TB017_ValorFinal  = ");
                sSQL.Append(vTB017_ValorFinal);
                sSQL.Append(" WHERE TB017_id = ");
                sSQL.Append(TB017_id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSQL.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool ParcelaAlterarValorParcela(long TB016_id, long TB011_Id, double TB016_Valor)
        {
            try
            {


                string vTB016_Valor = TB016_Valor.ToString().Replace(".", "");
                vTB016_Valor = vTB016_Valor.Replace(",", ".");

                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" UPDATE TB016_Parcela SET ");
                sSQL.Append(" TB016_AlteradoEm = ");
                sSQL.Append("'");
                sSQL.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSQL.Append("'");
                sSQL.Append(", TB016_AlteradoPor = ");
                sSQL.Append(TB011_Id);

                sSQL.Append(", TB016_Valor = ");
                sSQL.Append(vTB016_Valor);


                sSQL.Append(" WHERE TB016_id = ");
                sSQL.Append(TB016_id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSQL.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool ParcelaBaixaFinanceiro(ParcelaController Parcela)
        {

            
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                SqlCommand cmdUpdateTB016 = con.CreateCommand();
                SqlCommand cmdUpdateTB013 = con.CreateCommand();
                SqlCommand cmdUpdateTB012 = con.CreateCommand();
                SqlCommand cmdUpdateTB012_2 = con.CreateCommand();

                string vTB016_IOF = Parcela.TB016_IOF.ToString().Replace(".", "");
                vTB016_IOF = vTB016_IOF.Replace(",", ".");

                string vTB016_ValorTarifa = Parcela.TB016_ValorTarifa.ToString().Replace(".", "");
                vTB016_ValorTarifa = vTB016_ValorTarifa.Replace(",", ".");

                string vTB016_Multa = Parcela.TB016_Multa.ToString().Replace(".", "");
                vTB016_Multa = vTB016_Multa.Replace(",", ".");

                string vTB016_ValorPago = Parcela.TB016_ValorPago.ToString().Replace(".", "");
                vTB016_ValorPago = vTB016_ValorPago.Replace(",", ".");

                string vTB016_CredValorParcelas = Parcela.TB016_CredValorParcelas.ToString().Replace(".", "");
                vTB016_CredValorParcelas = vTB016_CredValorParcelas.Replace(",", ".");

                string vTB016_CredValor = Parcela.TB016_CredValor.ToString().Replace(".", "");
                vTB016_CredValor = vTB016_CredValor.Replace(",", ".");

                StringBuilder sUpdateTB016 = new StringBuilder();
                sUpdateTB016.Append(" UPDATE TB016_Parcela SET ");
                sUpdateTB016.Append(" TB016_AlteradoEm = ");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(", TB016_AlteradoPor = ");
                sUpdateTB016.Append(Parcela.TB016_AlteradoPor);
                sUpdateTB016.Append(", TB016_NossoNumero = ");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(Parcela.TB016_NossoNumero);
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(", TB016_IOF = ");
                sUpdateTB016.Append(vTB016_IOF);
                sUpdateTB016.Append(", TB016_ValorTarifa = ");
                sUpdateTB016.Append(vTB016_ValorTarifa);
                sUpdateTB016.Append(", TB016_Multa = ");
                sUpdateTB016.Append(vTB016_Multa);
                sUpdateTB016.Append(", TB016_ValorPago = ");
                sUpdateTB016.Append(vTB016_ValorPago);
                sUpdateTB016.Append(", TB016_DataPagamento = ");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(Parcela.TB016_DataPagamento.ToString("MM/dd/yyyy"));
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(",TB016_CredValor = ");
                sUpdateTB016.Append(vTB016_CredValor);
                sUpdateTB016.Append(",TB016_FormaProcessamentoBaixa = ");
                sUpdateTB016.Append(Parcela.TB016_FormaProcessamentoBaixa);
                sUpdateTB016.Append(",TB016_CredNCartao =");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(Parcela.TB016_CredNCartao.Trim());
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(",TB016_CredCPFTitularCartaoCartao =");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(Parcela.TB016_CredCPFTitularCartaoCartao.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim());
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(",TB016_CredNomeTitularCartaoCartao =");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(Parcela.TB016_CredNomeTitularCartaoCartao.TrimEnd());
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(",TB016_CredBandeira =");
                sUpdateTB016.Append(Parcela.TB016_CredBandeira);
                sUpdateTB016.Append(",TB016_CredNParcelas =");
                sUpdateTB016.Append(Parcela.TB016_CredNParcelas);
                sUpdateTB016.Append(",TB016_CredValorParcelas =");
                sUpdateTB016.Append(vTB016_CredValorParcelas);
                sUpdateTB016.Append(",TB016_CredAutorizacao =");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(Parcela.TB016_CredAutorizacao.Trim());
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(",TB016_CredCodValidador =");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(Parcela.TB016_CredCodValidador.Trim());
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(",TB016_CredFormaParamentoId =");
                sUpdateTB016.Append(Parcela.TB016_CredFormaParamentoId);
                sUpdateTB016.Append(",TB016_CredFormaParamentoDescricao =");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(Parcela.TB016_CredFormaParamentoDescricao.TrimEnd().ToUpper());
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(",TB016_CredBaixaFeitaEm =");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(Parcela.TB016_CredBaixaFeitaEm.ToString("MM/dd/yyyy hh:mm"));
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(",TB016_CredBaixaFeitaPor =");
                sUpdateTB016.Append(Parcela.TB016_CredBaixaFeitaPor);
                sUpdateTB016.Append(",TB016_Status =");
                sUpdateTB016.Append(Parcela.TB016_StatusS);
                sUpdateTB016.Append(",TB016_CredDataCredito =");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(Parcela.TB016_CredDataCredito.ToString("MM/dd/yyyy"));
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(" WHERE TB016_id = ");
                sUpdateTB016.Append(Parcela.TB016_id);

                StringBuilder sUpdateTB012 = new StringBuilder();
                sUpdateTB012.Append(" UPDATE TB012_Contratos SET  TB012_Status =1 ");
                sUpdateTB012.Append(" where  TB012_id = ");
                sUpdateTB012.Append(Parcela.TB012_id);
                sUpdateTB012.Append(" and   TB012_Status =0 ");
                sUpdateTB012.Append(" or   TB012_Status =4 ");


                StringBuilder sUpdateTB012_2 = new StringBuilder();
                sUpdateTB012_2.Append(" UPDATE TB012_Contratos SET  TB012_Status =1 ");
                sUpdateTB012_2.Append(" where  TB012_id = ");
                sUpdateTB012_2.Append(Parcela.TB012_id);
                sUpdateTB012_2.Append(" and   TB012_Status =4 ");
          

                StringBuilder sUpdateTB013 = new StringBuilder();
                sUpdateTB013.Append(" UPDATE TB013_Pessoa SET  TB013_Status =1 ");
                sUpdateTB013.Append(" ,TB013_AlteradoEm=");
                sUpdateTB013.Append("'");
                sUpdateTB013.Append(DateTime.Now.ToString("MM/dd/yyy hh:mm"));
                sUpdateTB013.Append("'");
                sUpdateTB013.Append(" ,TB013_AlteradoPor=");
                sUpdateTB013.Append(Parcela.TB016_CredBaixaFeitaPor);           
                sUpdateTB013.Append(" where  TB012_id = ");
                sUpdateTB013.Append(Parcela.TB012_id);
                sUpdateTB013.Append(" and  TB013_Status = 0");


            SqlCommand cmdTB020 = con.CreateCommand();
            StringBuilder sSQLTB020 = new StringBuilder();
            sSQLTB020.Append("UPDATE TB020_Unidades SET ");
            //if (Cancelamento == 0)
            //{
                sSQLTB020.Append("TB020_Status = 1");
                sSQLTB020.Append(",TB020_AlteradoEm = ");
                sSQLTB020.Append("'");
                sSQLTB020.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSQLTB020.Append("'");
                sSQLTB020.Append(",TB020_AlteradoPor=");
                sSQLTB020.Append(Parcela.TB016_CredBaixaFeitaPor);
                sSQLTB020.Append(" where TB012_id =");
                sSQLTB020.Append(Parcela.TB012_id);
                sSQLTB020.Append(" and TB020_Status = 0");
            //}


            cmdUpdateTB016.CommandText = sUpdateTB016.ToString();
                cmdUpdateTB013.CommandText = sUpdateTB013.ToString();
                cmdUpdateTB012.CommandText = sUpdateTB012.ToString();
                cmdUpdateTB012_2.CommandText = sUpdateTB012_2.ToString();


            cmdTB020.CommandText = sSQLTB020.ToString();

            con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    cmdUpdateTB016.Transaction = tran;
                    cmdUpdateTB016.ExecuteNonQuery();
                    // Comando 1 executado com sucesso!


                    cmdUpdateTB013.Transaction = tran;
                    cmdUpdateTB013.ExecuteNonQuery();
                    // Comando 2 executado com sucesso!

                    cmdUpdateTB012.Transaction = tran;
                    cmdUpdateTB012.ExecuteNonQuery();
                    // Comando 3 executado com sucesso!

                    cmdUpdateTB012_2.Transaction = tran;
                    cmdUpdateTB012_2.ExecuteNonQuery();
                // Comando 3 executado com sucesso!

                cmdTB020.Transaction = tran;
                cmdTB020.ExecuteNonQuery();
                //Comando 4 executado com sucesso!

                tran.Commit();

                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

            return true;
        }

        public List<ParcelaController> ParcelasEmAbertoPorData(long TB012_id)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT TB016_id,TB012_id, TB016_Vencimento,TB016_Valor FROM dbo.TB016_Parcela ");
                sSQL.Append(" WHERE TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append(" and TB016_Vencimento <= ");
                sSQL.Append("'");
                sSQL.Append(DateTime.Now.ToString("MM/dd/yyyy"));
                sSQL.Append("'");
                sSQL.Append("  AND TB016_Status <> 5 AND TB016_Status <> 3 ");


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);

                    //obj.TB012_id = Convert.ToInt64(reader["TB012_id"]);
                    //obj.TB016_Parcela = Convert.ToInt16(reader["TB016_Parcela"]);
                    //obj.TB016_CPFCNPJ = reader["TB016_CPFCNPJ"].ToString().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "");

                    //if (obj.TB016_CPFCNPJ.Length == 11)
                    //{
                    //    obj.TB016_CPFCNPJ = Convert.ToUInt64(obj.TB016_CPFCNPJ).ToString(@"000\.000\.000\-00");
                    //}
                    //else
                    //{
                    //    obj.TB016_CPFCNPJ = Convert.ToUInt64(obj.TB016_CPFCNPJ).ToString(@"00\.000\.000\/0000\-00");
                    //}
                    //obj.TB016_Pagador = reader["TB016_Pagador"].ToString();
                    //obj.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    obj.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);
                    //obj.TB016_StatusS = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(reader["TB016_Status"]));
                    //obj.TB016_FormaPagamentoS = Enum.GetName(typeof(ParcelaController.TB016_FormaPagamentoE), Convert.ToInt16(reader["TB016_FormaPagamento"]));


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

        public ParcelaController UltimaParcelaContrato(long TB012_id)
        {
            ParcelaController Retorno = new ParcelaController();
            Retorno.Contrato = new ContratosController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB016_Parcela.TB012_id, MAX(dbo.TB016_Parcela.TB016_id) AS UltimaParcela, dbo.TB012_Contratos.TB012_CicloContrato, MAX(dbo.TB016_Parcela.TB016_Vencimento) AS UltimoVencimento, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Status ");
                sSQL.Append(" FROM dbo.TB016_Parcela INNER JOIN ");
                sSQL.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id ");
                sSQL.Append(" GROUP BY dbo.TB016_Parcela.TB012_id, dbo.TB012_Contratos.TB012_CicloContrato, dbo.TB016_Parcela.TB016_Status ");
                sSQL.Append(" HAVING dbo.TB016_Parcela.TB012_id =");
                sSQL.Append(TB012_id);
                /*Validar para erro*/
                sSQL.Append(" AND dbo.TB016_Parcela.TB016_Status <> 3; ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB016_id = Convert.ToInt64(reader["UltimaParcela"]);
                    Retorno.TB016_Vencimento = Convert.ToDateTime(reader["UltimoVencimento"]);
                    Retorno.Contrato.TB012_CicloContrato = reader["TB012_CicloContrato"].ToString().TrimEnd();


                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public List<ParcelaController> ParcelasExportarBoletos(long TB012_id, Int32 TB012_CicloContrato)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT  dbo.TB016_Parcela.TB012_id, dbo.TB016_Parcela.TB012_CicloContrato, dbo.TB016_Parcela.TB016_Status, dbo.TB016_Parcela.TB016_id, dbo.TB016_Parcela.TB016_Parcela, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Emissao, dbo.TB016_Parcela.TB016_Vencimento, dbo.TB016_Parcela.TB016_Pagador, dbo.TB013_Pessoa.TB013_Tipo, dbo.TB016_Parcela.TB016_CPFCNPJ, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_EnderecoPagador, dbo.TB016_Parcela.TB016_PagadorCEP, dbo.TB016_Parcela.TB016_PagadorCidade, dbo.TB016_Parcela.TB016_PagadorUF, dbo.TB016_Parcela.TB016_Valor, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Abatimento, dbo.TB016_Parcela.TB016_IOF, dbo.TB016_Parcela.TB016_BoletoDesc1, dbo.TB016_Parcela.TB016_BoletoDesc2, dbo.TB016_Parcela.TB016_BoletoDesc3, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_BoletoDesc4, dbo.TB016_Parcela.TB016_BoletoDesc5, dbo.TB016_Parcela.TB016_EspecieDocumento, dbo.TB016_Parcela.TB016_Banco, dbo.TB016_Parcela.TB016_Agencia, ");
                sSQL.Append("  dbo.TB016_Parcela.TB016_ContaCorrente, dbo.TB016_Parcela.TB016_Carteira, dbo.TB016_Parcela.TB016_Beneficiario, dbo.TB016_Parcela.TB016_BeneficiarioEndereco, dbo.TB016_Parcela.TB016_BeneficiarioCPFCNPJ, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_BeneficiarioCidade, dbo.TB016_Parcela.TB016_BeneficiarioUF, dbo.TB016_Parcela.TB016_NBoleto, dbo.TB016_Parcela.TB016_NossoNumero, dbo.TB006_Municipio.TB006_Lote, ");
                sSQL.Append(" dbo.TB013_Pessoa.TB013_Bairro, dbo.TB013_Pessoa.TB013_Numero ");
                sSQL.Append(" FROM dbo.TB016_Parcela INNER JOIN");
                sSQL.Append(" dbo.TB013_Pessoa ON dbo.TB016_Parcela.TB013_id = dbo.TB013_Pessoa.TB013_id INNER JOIN ");
                sSQL.Append(" dbo.TB006_Municipio ON dbo.TB013_Pessoa.TB006_id = dbo.TB006_Municipio.TB006_id ");
                sSQL.Append(" WHERE dbo.TB016_Parcela.TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append(" AND dbo.TB016_Parcela.TB016_Status = 2 AND dbo.TB016_Parcela.TB012_CicloContrato = ");
                sSQL.Append(TB012_CicloContrato);
                sSQL.Append(" ORDER BY dbo.TB006_Municipio.TB006_Lote, dbo.TB016_Parcela.TB016_PagadorUF, dbo.TB016_Parcela.TB016_PagadorCidade, dbo.TB013_Pessoa.TB013_Bairro, dbo.TB013_Pessoa.TB013_Numero");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.Titular = new PessoaController();
                    obj.Municipio = new MunicipioController();

                    obj.TB012_id = Convert.ToInt64(reader["TB012_id"]);
                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB016_Parcela = Convert.ToInt16(reader["TB016_Parcela"]);
                    obj.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    obj.Titular.TB013_Bairro = reader["TB013_Bairro"].ToString().TrimEnd().TrimStart();
                    obj.TB016_Pagador = reader["TB016_Pagador"].ToString().TrimEnd().TrimStart();
                    obj.Titular.TB013_TipoS = reader["TB013_Tipo"].ToString().Trim();
                    obj.TB016_CPFCNPJ = reader["TB016_CPFCNPJ"].ToString().TrimEnd().TrimStart().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "");
                    obj.TB016_EnderecoPagador = reader["TB016_EnderecoPagador"].ToString().TrimEnd().TrimStart();
                    obj.TB016_PagadorCEP = reader["TB016_PagadorCEP"].ToString().TrimEnd().TrimStart().Replace("-", "");
                    obj.TB016_PagadorCidade = reader["TB016_PagadorCidade"].ToString().TrimEnd().TrimStart();
                    obj.TB016_PagadorUF = reader["TB016_PagadorUF"].ToString().TrimEnd().TrimStart();
                    obj.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);
                    obj.TB016_Abatimento = reader["TB016_Abatimento"] is DBNull ? 0 : Convert.ToDouble(reader["TB016_Abatimento"]);
                    //Convert.ToDouble(reader["TB016_Abatimento"]);
                    obj.TB016_IOF = reader["TB016_IOF"] is DBNull ? 0 : Convert.ToDouble(reader["TB016_IOF"]);


                    obj.TB016_BoletoDesc1 = reader["TB016_BoletoDesc1"].ToString().TrimEnd().TrimStart();

                    if (obj.TB016_BoletoDesc1 == "NULO")
                    {
                        obj.TB016_BoletoDesc1 = " ";
                    }
                    obj.TB016_BoletoDesc2 = reader["TB016_BoletoDesc2"].ToString().TrimEnd().TrimStart();
                    if (obj.TB016_BoletoDesc2 == "NULO")
                    {
                        obj.TB016_BoletoDesc2 = " ";
                    }
                    obj.TB016_BoletoDesc3 = reader["TB016_BoletoDesc3"].ToString().TrimEnd().TrimStart();
                    if (obj.TB016_BoletoDesc3 == "NULO")
                    {
                        obj.TB016_BoletoDesc3 = " ";
                    }
                    obj.TB016_BoletoDesc4 = reader["TB016_BoletoDesc4"].ToString().TrimEnd().TrimStart();
                    if (obj.TB016_BoletoDesc4 == "NULO")
                    {
                        obj.TB016_BoletoDesc4 = " ";
                    }
                    obj.TB016_BoletoDesc5 = reader["TB016_BoletoDesc5"].ToString().TrimEnd().TrimStart();
                    if (obj.TB016_BoletoDesc5 == "NULO")
                    {
                        obj.TB016_BoletoDesc5 = " ";
                    }
                    obj.TB016_EspecieDocumento = reader["TB016_EspecieDocumento"].ToString().TrimEnd().TrimStart();
                    obj.TB016_Banco = reader["TB016_Banco"].ToString().TrimEnd().TrimStart();
                    obj.TB016_Agencia = reader["TB016_Agencia"].ToString().TrimEnd().TrimStart();
                    obj.TB016_ContaCorrente = reader["TB016_ContaCorrente"].ToString().TrimEnd().TrimStart();
                    obj.TB016_Carteira = reader["TB016_Carteira"].ToString().TrimEnd().TrimStart();
                    obj.TB016_Beneficiario = reader["TB016_Beneficiario"].ToString().TrimEnd().TrimStart();
                    obj.TB016_BeneficiarioEndereco = reader["TB016_BeneficiarioEndereco"].ToString().TrimEnd().TrimStart();
                    obj.TB016_BeneficiarioCPFCNPJ = reader["TB016_BeneficiarioCPFCNPJ"].ToString().TrimEnd().TrimStart().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "");
                    obj.TB016_BeneficiarioCidade = reader["TB016_BeneficiarioCidade"].ToString().TrimEnd().TrimStart();
                    obj.TB016_BeneficiarioUF = reader["TB016_BeneficiarioUF"].ToString().TrimEnd().TrimStart();
                    obj.TB016_NBoleto = reader["TB016_NBoleto"].ToString().TrimEnd().TrimStart();
                    obj.TB016_NossoNumero = reader["TB016_NossoNumero"].ToString().TrimEnd().TrimStart();
                    obj.Municipio.TB006_Lote = Convert.ToInt64(reader["TB006_Lote"]);

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

        public Int64 UltimaLoteExportacao()
        {
            Int64 Retorno = 0;

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT MAX(ISNULL(TB016_LoteExportacao, 0)) AS UltimoLoteExportacao FROM dbo.TB016_Parcela ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno = Convert.ToInt64(reader["UltimoLoteExportacao"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public ParcelaController ValorParcela(Int64 TB016_id)
        {
            ParcelaController Retorno = new ParcelaController();

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT        SUM(dbo.TB017_ParcelaProduto.TB017_ValorFinal) AS Valor, dbo.TB015_Planos.TB015_Plano");
                sSQL.Append(" FROM            dbo.TB017_ParcelaProduto INNER JOIN");
                sSQL.Append("     dbo.TB016_Parcela ON dbo.TB017_ParcelaProduto.TB016_id = dbo.TB016_Parcela.TB016_id INNER JOIN");
                sSQL.Append("    dbo.TB015_Planos ON dbo.TB016_Parcela.TB015_id = dbo.TB015_Planos.TB015_id");
                sSQL.Append(" WHERE dbo.TB017_ParcelaProduto.TB016_id = ");
                sSQL.Append(TB016_id);
                sSQL.Append(" GROUP BY dbo.TB015_Planos.TB015_Plano");


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB016_Valor = Convert.ToDouble(reader["Valor"]);
                    Retorno.TB015_Plano = reader["TB015_Plano"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public ParcelaController UltimaParcelaRenovacao(Int64 TB012_id)
        {
            ParcelaController Retorno = new ParcelaController();
            Retorno.Contrato = new ContratosController();

            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT TOP (1) PERCENT TB012_CicloContrato,TB016_Vencimento,TB012_id, MAX(TB016_id) AS UltimaParcela FROM dbo.TB016_Parcela GROUP BY TB012_CicloContrato,TB016_Vencimento,TB012_id, TB016_Status HAVING TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append(" AND TB016_Status <> 3 ORDER BY UltimaParcela DESC ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB016_id = Convert.ToInt64(reader["UltimaParcela"]);
                    Retorno.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    Retorno.Contrato.TB012_CicloContrato = reader["TB012_CicloContrato"].ToString().TrimEnd();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public bool ParcelaCancela(long TB016_id, long TB011_Id)
        {
            try
            {
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" UPDATE TB016_Parcela SET ");
                sSQL.Append(" TB016_AlteradoEm = ");
                sSQL.Append("'");
                sSQL.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSQL.Append("'");
                sSQL.Append(", TB016_AlteradoPor = ");
                sSQL.Append(TB011_Id);
                sSQL.Append(", TB016_Status = 3");
                sSQL.Append(" WHERE TB016_id = ");
                sSQL.Append(TB016_id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSQL.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public List<ParcelaController> ParcelasListarAtivasPorContrato(long TB012_id, Int32 TB012_CicloContrato)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT * FROM dbo.TB016_Parcela ");
                sSQL.Append(" WHERE TB016_Status < 3 ");
                sSQL.Append(" AND TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append(" AND TB012_CicloContrato = ");
                sSQL.Append(TB012_CicloContrato);
                sSQL.Append(" ORDER BY TB016_id ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    obj.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);
                    obj.TB016_FormaPagamentoS = Enum.GetName(typeof(ParcelaController.TB016_FormaPagamentoE), Convert.ToInt16(reader["TB016_FormaPagamento"]));
                    obj.TB016_NossoNumero = reader["TB016_NossoNumero"] is DBNull ? "0" : reader["TB016_NossoNumero"].ToString();

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

        public ParcelaController ParcelaId(Int64 TB016_id)
        {
            ParcelaController Retorno = new ParcelaController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                //sSQL.Append("SELECT ");
                sSQL.Append("SELECT dbo.TB006_Municipio.TB006_id, dbo.TB016_Parcela.TB012_id, dbo.TB016_Parcela.TB016_FormaPagamento, dbo.TB016_Parcela.TB016_Status, dbo.TB016_Parcela.TB016_EmitirBoleto,  ");
                sSQL.Append("dbo.TB018_Bancos.TB018_Banco, dbo.TB001_Empresa.TB001_id, dbo.TB001_Empresa.TB001_Matriz, dbo.TB016_Parcela.TB016_Vencimento, dbo.TB016_Parcela.TB016_EspecieDocumento,  ");
                sSQL.Append("dbo.TB016_Parcela.TB016_Valor, dbo.TB016_Parcela.TB016_Abatimento, dbo.TB016_Parcela.TB016_IOF, dbo.TB016_Parcela.TB016_Pagador, dbo.TB006_Municipio.TB006_Municipio,  ");
                sSQL.Append("dbo.TB005_Estado.TB005_Sigla, dbo.TB016_Parcela.TB016_CPFCNPJ, dbo.TB016_Parcela.TB016_TipoVencimento, dbo.TB016_Parcela.TB016_BoletoDesc1, dbo.TB016_Parcela.TB016_BoletoDesc2,  ");
                sSQL.Append("dbo.TB016_Parcela.TB016_BoletoDesc3, dbo.TB016_Parcela.TB016_BoletoDesc4, dbo.TB016_Parcela.TB016_BoletoDesc5, dbo.TB016_Parcela.TB016_Emissao, dbo.TB016_Parcela.TB016_id,  ");
                sSQL.Append("dbo.TB012_Contratos.TB004_Cep, dbo.TB012_Contratos.TB012_Logradouro, dbo.TB012_Contratos.TB012_Numero, dbo.TB012_Contratos.TB012_Bairro, dbo.TB012_Contratos.TB012_Complemento, ");
                sSQL.Append("dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_Tipo ");
                sSQL.Append("FROM dbo.TB012_Contratos INNER JOIN ");
                sSQL.Append("dbo.TB016_Parcela INNER JOIN ");
                sSQL.Append("dbo.TB001_Empresa ON dbo.TB016_Parcela.TB001_id = dbo.TB001_Empresa.TB001_id INNER JOIN ");
                sSQL.Append("dbo.TB018_Bancos ON dbo.TB001_Empresa.TB018_id = dbo.TB018_Bancos.TB018_id INNER JOIN ");
                sSQL.Append("dbo.TB013_Pessoa ON dbo.TB016_Parcela.TB013_id = dbo.TB013_Pessoa.TB013_id ON dbo.TB012_Contratos.TB012_id = dbo.TB016_Parcela.TB012_id INNER JOIN ");
                sSQL.Append("dbo.TB005_Estado INNER JOIN ");
                sSQL.Append("dbo.TB006_Municipio ON dbo.TB005_Estado.TB005_Id = dbo.TB006_Municipio.TB005_Id ON dbo.TB012_Contratos.TB006_id = dbo.TB006_Municipio.TB006_id ");
                sSQL.Append("WHERE ");
                sSQL.Append("dbo.TB016_Parcela.TB016_id = ");
                sSQL.Append(TB016_id);
                sSQL.Append("AND ");
                sSQL.Append("dbo.TB016_Parcela.TB016_FormaPagamento = ");
                sSQL.Append(1);
                sSQL.Append("AND ");
                sSQL.Append("dbo.TB016_Parcela.TB016_Status = ");
                sSQL.Append(1);
                sSQL.Append("AND ");
                sSQL.Append("dbo.TB016_Parcela.TB016_EmitirBoleto = ");
                sSQL.Append(1);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {


                    Retorno.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    Retorno.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    Retorno.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    Retorno.TB016_EspecieDocumento = reader["TB016_EspecieDocumento"].ToString().Trim();
                    Retorno.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);
                    //obj.TB016_Abatimento        = Convert.ToDouble(reader["TB016_Abatimento"]);
                    Retorno.TB016_IOF = Convert.ToDouble(reader["TB016_IOF"]);
                    Retorno.TB016_Pagador = reader["TB016_Pagador"].ToString().TrimEnd();

                    if (Convert.ToInt16(reader["TB013_Tipo"]) == 1)
                    {
                        Retorno.TB016_CPFCNPJ = reader["TB016_CPFCNPJ"].ToString().TrimEnd().PadLeft(11, '0');
                    }
                    else
                    {
                        Retorno.TB016_CPFCNPJ = reader["TB016_CPFCNPJ"].ToString().TrimEnd().PadLeft(13, '0');
                    }

                    Retorno.TB016_TipoVencimento = Convert.ToInt16(reader["TB016_TipoVencimento"]);
                    Retorno.TB016_BoletoDesc1 = reader["TB016_BoletoDesc1"].ToString().TrimEnd();
                    Retorno.TB016_BoletoDesc2 = reader["TB016_BoletoDesc2"].ToString().TrimEnd();
                    Retorno.TB016_BoletoDesc3 = reader["TB016_BoletoDesc3"].ToString().TrimEnd();
                    Retorno.TB016_BoletoDesc4 = reader["TB016_BoletoDesc4"].ToString().TrimEnd();
                    Retorno.TB016_BoletoDesc5 = reader["TB016_BoletoDesc5"].ToString().TrimEnd();

                    MunicipioController objMunicipio = new MunicipioController();

                    objMunicipio.TB006_id = Convert.ToInt64(reader["TB006_id"]);
                    objMunicipio.TB006_Municipio = reader["TB006_Municipio"].ToString().TrimEnd();
                    Retorno.Municipio = objMunicipio;

                    EstadoController objEstado = new EstadoController();
                    objEstado.TB005_Sigla = reader["TB005_Sigla"].ToString().TrimEnd();
                    Retorno.Estado = objEstado;

                    BancoController objBanco = new BancoController();
                    objBanco.TB018_Banco = Convert.ToInt16(reader["TB018_Banco"]);
                    Retorno.Banco = objBanco;

                    EmpresaController objEmpresa = new EmpresaController();
                    objEmpresa.TB001_id = Convert.ToInt64(reader["TB001_id"]);
                    Retorno.Empresa = objEmpresa;

                    ContratosController objContrato = new ContratosController();
                    objContrato.TB012_Logradouro = reader["TB012_Logradouro"].ToString().TrimEnd();
                    objContrato.TB012_Numero = reader["TB012_Numero"].ToString().TrimEnd();
                    objContrato.TB012_Bairro = reader["TB012_Bairro"].ToString().TrimEnd();
                    objContrato.TB004_Cep = reader["TB004_Cep"].ToString().TrimEnd().Replace(".", "").Replace("-", "");
                    Retorno.Contrato = objContrato;

                    PessoaController objPessoa = new PessoaController();
                    objPessoa.TB013_NomeCompleto = reader["TB013_NomeCompleto"].ToString().TrimEnd();
                    //objPessoa.TB013_CPFCNPJ         = reader["TB013_CPFCNPJ"].ToString().TrimEnd();


                    if (Convert.ToInt16(reader["TB013_Tipo"]) == 1)
                    {
                        objPessoa.TB013_CPFCNPJ = reader["TB016_CPFCNPJ"].ToString().TrimEnd().PadLeft(11, '0');
                    }
                    else
                    {
                        objPessoa.TB013_CPFCNPJ = reader["TB016_CPFCNPJ"].ToString().TrimEnd().PadLeft(13, '0');
                    }

                    Retorno.Pessoa = objPessoa;



                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public bool AlterarVencimentoParcela(long TB016_id, DateTime TB016_Vencimento, long TB011_Id)
        {
            try
            {

                string UpdateSql = " UPDATE TB016_Parcela SET " +
                                   " TB016_Vencimento      ='" + TB016_Vencimento.ToString("MM/dd/yyyy") + "'" +
                                   " ,TB016_AlteradoEm     ='" + DateTime.Now.ToString("MM/dd/yyyy hh:mm") + "'" +
                                   " ,TB016_AlteradoPor    =" + TB011_Id +
                                   " where TB016_id        =" + TB016_id;

                using (SqlConnection myConnection = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    myConnection.Open();

                    SqlCommand myCommand = new SqlCommand(UpdateSql, myConnection);
                    myCommand.CommandTimeout = 300;

                    myCommand.ExecuteScalar();
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
            return true;
        }

        public long CorporativoParcelaInsert(ParcelaController Parcela)
        {
            long Retorno = 0;

            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

            SqlCommand cmdInsertTB016 = con.CreateCommand();
            SqlCommand cmdInsertTB017 = con.CreateCommand();


            StringBuilder sInsertTB016 = new StringBuilder();
            sInsertTB016.Append("INSERT INTO TB016_Parcela ( ");
            sInsertTB016.Append("TB016_DiaFechamento,TB016_DiaVencimento,TB015_id,");
            sInsertTB016.Append("TB001_id,");
            sInsertTB016.Append("TB012_id,");
            sInsertTB016.Append("TB013_id, ");
            sInsertTB016.Append("TB016_Parcela,");
            sInsertTB016.Append("TB016_TotalParcelas,");
            sInsertTB016.Append("TB016_Emissao,");
            sInsertTB016.Append("TB016_Vencimento,");
            sInsertTB016.Append("TB016_Pagador,");
            sInsertTB016.Append("TB016_CPFCNPJ,");
            sInsertTB016.Append("TB016_EnderecoPagador,");
            sInsertTB016.Append("TB016_TipoSacado,");
            sInsertTB016.Append("TB016_Valor,");
            sInsertTB016.Append("TB016_FormaPagamento,");
            sInsertTB016.Append("TB016_EmitirBoleto,");
            sInsertTB016.Append("TB016_Entrada,");
            sInsertTB016.Append("TB016_ValorAdesao,");
            sInsertTB016.Append("TB016_IOF,");
            sInsertTB016.Append("TB016_TipoVencimento,");
            sInsertTB016.Append("TB016_BoletoDesc1,");
            sInsertTB016.Append("TB016_BoletoDesc2,");
            sInsertTB016.Append("TB016_BoletoDesc3,");
            sInsertTB016.Append("TB016_BoletoDesc4,");
            sInsertTB016.Append("TB016_BoletoDesc5,");
            sInsertTB016.Append("TB016_EspecieDocumento,");
            sInsertTB016.Append("TB016_Beneficiario,");
            sInsertTB016.Append("TB016_BeneficiarioEndereco ,");
            sInsertTB016.Append("TB016_BeneficiarioCPFCNPJ,");
            sInsertTB016.Append("TB016_BeneficiarioCidade ,");
            sInsertTB016.Append("TB016_BeneficiarioUF ,");
            sInsertTB016.Append("TB016_PagadorCEP ,");
            sInsertTB016.Append("TB016_PagadorCidade ,");
            sInsertTB016.Append("TB016_PagadorUF,");
            sInsertTB016.Append("TB016_Status,");
            sInsertTB016.Append("TB012_CicloContrato,");
            sInsertTB016.Append("TB016_CadastradoEm,");
            sInsertTB016.Append("TB016_CadastradoPor,");
            sInsertTB016.Append("TB016_AlteradoEm,");
            sInsertTB016.Append("TB016_AlteradoPor,");
            sInsertTB016.Append("TB016_LoteExportacao");
            sInsertTB016.Append(" ) VALUES ( ");
            sInsertTB016.Append(Parcela.TB016_DiaFechamento);
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB016_DiaVencimento);
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB015_id);
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.Empresa.TB001_id);
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB012_id);
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.Pessoa.TB013_id);
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB016_Parcela);
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB016_TotalParcelas);
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_Emissao.ToString("MM/dd/yyyy"));
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_Vencimento.ToString("MM/dd/yyyy"));
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_Pagador.ToString());
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_CPFCNPJ);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_EnderecoPagador);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_TipoSacadoS);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB016_Valor.ToString().Replace(".", "").Replace(",", "."));
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB016_FormaPagamentoS);
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB016_EmitirBoleto);
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB016_Entrada);
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB016_ValorAdesao.ToString().Replace(".", "").Replace(",", "."));
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB016_IOF.ToString().Replace(".", "").Replace(",", "."));
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB016_TipoVencimento);
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_BoletoDesc1);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_BoletoDesc2);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_BoletoDesc3);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_BoletoDesc4);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_BoletoDesc5);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_EspecieDocumento);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_Beneficiario);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_BeneficiarioEndereco);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_BeneficiarioCPFCNPJ);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_BeneficiarioCidade);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_BeneficiarioUF);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_PagadorCEP);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_PagadorCidade);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_PagadorUF);
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB016_StatusS);
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB012_CicloContrato);
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB016_CadastradoPor);
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            sInsertTB016.Append("'");
            sInsertTB016.Append(",");
            sInsertTB016.Append(Parcela.TB016_AlteradoPor);
            sInsertTB016.Append(",");
            sInsertTB016.Append("'");
            sInsertTB016.Append(Parcela.TB016_LoteExportacao);
            sInsertTB016.Append("'");
            sInsertTB016.Append(" ) SELECT SCOPE_IDENTITY()");


            StringBuilder sInsertTB017 = new StringBuilder();
            sInsertTB017.Append("INSERT INTO TB017_ParcelaProduto ( ");
            sInsertTB017.Append("TB017_IdProteus,");
            sInsertTB017.Append("TB017_Item,");
            sInsertTB017.Append("TB017_ValorUnitario,");
            sInsertTB017.Append("TB017_ValorDesconto,");
            sInsertTB017.Append("TB017_ValorFinal,");
            sInsertTB017.Append("TB016_id");
            sInsertTB017.Append(" ) VALUES ( ");
            sInsertTB017.Append("'");
            sInsertTB017.Append(Parcela.ParcelaProduto_L[0].TB017_IdProteus.TrimEnd());
            sInsertTB017.Append("'");
            sInsertTB017.Append(",");
            sInsertTB017.Append("'");
            sInsertTB017.Append(Parcela.ParcelaProduto_L[0].TB017_Item.TrimEnd());
            sInsertTB017.Append("'");
            sInsertTB017.Append(",");
            sInsertTB017.Append(Parcela.ParcelaProduto_L[0].TB017_ValorUnitario.ToString().Replace(".", "").Replace(",", "."));
            sInsertTB017.Append(",");
            sInsertTB017.Append(Parcela.ParcelaProduto_L[0].TB017_ValorDesconto.ToString().Replace(".", "").Replace(",", "."));
            sInsertTB017.Append(",");
            sInsertTB017.Append(Parcela.ParcelaProduto_L[0].TB017_ValorFinal.ToString().Replace(".", "").Replace(",", "."));
            sInsertTB017.Append(",");





            cmdInsertTB016.CommandText = sInsertTB016.ToString();
            // 


            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmdInsertTB016.Transaction = tran;
                Retorno = Convert.ToInt64(cmdInsertTB016.ExecuteScalar());
                //Comando 1 executado com sucesso!



                sInsertTB017.Append(Retorno);
                sInsertTB017.Append(")");

                cmdInsertTB017.CommandText = sInsertTB017.ToString();

                cmdInsertTB017.Transaction = tran;
                cmdInsertTB017.ExecuteNonQuery();
                // Comando 2 executado com sucesso!



                tran.Commit();

            }
            catch (SqlException ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return Retorno;
        }

        public ParcelaController UltimaParcelaContratoCiclo(Int64 TB012_id, Int32 TB012_CicloContrato)
        {
            ParcelaController Retorno = new ParcelaController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append("SELECT TB016_Parcela, TB016_Vencimento,TB016_DiaFechamento, TB016_DiaVencimento ");
                sSQL.Append(" FROM dbo.TB016_Parcela ");
                sSQL.Append(" WHERE TB012_id =");
                sSQL.Append(TB012_id);
                sSQL.Append(" AND TB012_CicloContrato = ");
                sSQL.Append(TB012_CicloContrato);
                sSQL.Append(" AND TB016_Status <> 3");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno.TB016_Parcela = Convert.ToInt16(reader["TB016_Parcela"]);
                    Retorno.TB016_DiaFechamento = reader["TB016_DiaFechamento"] is DBNull ? 10 : Convert.ToInt16(reader["TB016_DiaFechamento"]);
                    //;
                    Retorno.TB016_DiaVencimento = reader["TB016_DiaVencimento"] is DBNull ? 15 : Convert.ToInt16(reader["TB016_DiaVencimento"]);
                    //Convert.ToInt16(reader["TB016_DiaVencimento"]);
                    Retorno.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public double CorporativoValorUnitarioParcela(Int64 TB012_id)
        {
            double Retorno = 0;
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append("SELECT TOP (1) dbo.TB017_ParcelaProduto.TB017_ValorUnitario ");
                sSQL.Append(" FROM dbo.TB016_Parcela INNER JOIN ");
                sSQL.Append(" dbo.TB017_ParcelaProduto ON dbo.TB016_Parcela.TB016_id = dbo.TB017_ParcelaProduto.TB016_id ");
                sSQL.Append(" GROUP BY dbo.TB016_Parcela.TB012_id, dbo.TB017_ParcelaProduto.TB017_ValorUnitario, dbo.TB016_Parcela.TB016_Status ");
                sSQL.Append(" HAVING dbo.TB016_Parcela.TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append(" AND(dbo.TB016_Parcela.TB016_Status <> 3) ");
                sSQL.Append(" ORDER BY MAX(dbo.TB017_ParcelaProduto.TB017_id) DESC ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno = Convert.ToDouble(reader["TB017_ValorUnitario"]);

                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public double ParcelaValorTotalProdutos(long tb016Id)
        {
            double retorno = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    var sSql = new StringBuilder();

                    sSql.Append("SELECT SUM(TB017_ValorFinal) AS TB017_ValorFinal FROM dbo.TB017_ParcelaProduto WHERE TB016_id =");
                    sSql.Append(tb016Id);


                    var command = new SqlCommand(sSql.ToString(), con);
                    command.CommandTimeout = 300;

                    con.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        retorno = Convert.ToDouble(reader["TB017_ValorFinal"]);
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public bool CorporativoAdesaoParcelaInsert(List<ParcelaProdutosController> Adesao_L, double ValorAdesao, double TB016_id, long TB012_id)
        {
            if (Adesao_L.Count == 0)
            {
                return true;
            }
            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

            SqlCommand cmdUpdateTB016 = con.CreateCommand();
            SqlCommand cmdInsertTB017 = con.CreateCommand();
            SqlCommand cmdUpdateTB013 = con.CreateCommand();

            StringBuilder sUpdateTB016 = new StringBuilder();
            sUpdateTB016.Append("update TB016_Parcela set  TB016_Valor = TB016_Valor + ");
            sUpdateTB016.Append(ValorAdesao.ToString().Replace(".", "").Replace(",", "."));
            sUpdateTB016.Append(" where TB016_id=");
            sUpdateTB016.Append(TB016_id);


            StringBuilder sInsertTB017 = new StringBuilder();
            sInsertTB017.Append("INSERT INTO TB017_ParcelaProduto ( ");
            sInsertTB017.Append("TB017_IdProteus,");
            sInsertTB017.Append("TB017_Item,");
            sInsertTB017.Append("TB017_ValorUnitario,");
            sInsertTB017.Append("TB017_ValorDesconto,");
            sInsertTB017.Append("TB017_ValorFinal,");
            sInsertTB017.Append("TB016_id");
            sInsertTB017.Append(" ) VALUES ");




            for (int i = 0; i < Adesao_L.Count; i++)
            {
                if (i > 0)
                {
                    sInsertTB017.Append(",");
                }
                sInsertTB017.Append("(");
                sInsertTB017.Append("'");
                sInsertTB017.Append(Adesao_L[i].TB017_IdProteus.TrimEnd());
                sInsertTB017.Append("'");
                sInsertTB017.Append(",");
                sInsertTB017.Append("'");
                sInsertTB017.Append(Adesao_L[i].TB017_Item.TrimEnd());
                sInsertTB017.Append("'");
                sInsertTB017.Append(",");
                sInsertTB017.Append(Adesao_L[i].TB017_ValorUnitario.ToString().Replace(".", "").Replace(",", "."));

                sInsertTB017.Append(",");
                sInsertTB017.Append(Adesao_L[i].TB017_ValorDesconto.ToString().Replace(".", "").Replace(",", "."));
                sInsertTB017.Append(",");
                sInsertTB017.Append(Adesao_L[i].TB017_ValorFinal.ToString().Replace(".", "").Replace(",", "."));
                sInsertTB017.Append(",");
                sInsertTB017.Append(TB016_id);



                sInsertTB017.Append(")");

            }

            StringBuilder supdateTB013 = new StringBuilder();
            supdateTB013.Append("update TB013_Pessoa set  TB013_CorporativoAtivado = 1 ");
            supdateTB013.Append(" WHERE TB012_Corporativo =  ");
            supdateTB013.Append(TB012_id);
            supdateTB013.Append(" AND TB013_CorporativoAtivado = 0  ");


            cmdUpdateTB016.CommandText = sUpdateTB016.ToString();
            cmdInsertTB017.CommandText = sInsertTB017.ToString();
            cmdUpdateTB013.CommandText = supdateTB013.ToString();

            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmdUpdateTB016.Transaction = tran;
                cmdUpdateTB016.ExecuteScalar();
                //Comando 1 executado com sucesso!

                cmdInsertTB017.Transaction = tran;
                cmdInsertTB017.ExecuteScalar();
                ////Comando 2 executado com sucesso!

                cmdUpdateTB013.Transaction = tran;
                cmdUpdateTB013.ExecuteScalar();
                ////Comando 3 executado com sucesso!

                tran.Commit();

            }
            catch (SqlException ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return true;
        }

        public bool AlterarFormaPagamento(long tb016Id, Int16 tb016FormaPagamento, long tb011Id, DateTime tb016Vencimento)
        {
            try
            {

                string updateSql = " UPDATE TB016_Parcela SET " +
                                   " TB016_FormaPagamento      =" + tb016FormaPagamento +
                                   " ,TB016_Vencimento     ='" + tb016Vencimento.ToString("MM/dd/yyyy") + "'" +
                                   " ,TB016_AlteradoEm     ='" + DateTime.Now.ToString("MM/dd/yyyy hh:mm") + "'" +
                                   " ,TB016_AlteradoPor    =" + tb011Id +
                                   " where TB016_id        =" + tb016Id;

                using (SqlConnection myConnection = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    myConnection.Open();

                    SqlCommand myCommand = new SqlCommand(updateSql, myConnection);
                    myCommand.CommandTimeout = 300;

                    myCommand.ExecuteScalar();
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
            return true;
        }

        public bool ParcelasAlteracaoDesconto(long tb016Id, long tb017Id, double desconto, double valorfinal, long tb011Id, long tb012Id)
        {

            ParcelaController valor = ValorParcela(tb016Id);

            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

            SqlCommand cmdTb016 = con.CreateCommand();
            SqlCommand cmdTb017 = con.CreateCommand();



            StringBuilder sSqltb017 = new StringBuilder();
            sSqltb017.Append(" UPDATE TB017_ParcelaProduto SET ");
            sSqltb017.Append("TB017_ValorDesconto= ");
            sSqltb017.Append(desconto.ToString(CultureInfo.CurrentCulture).Replace(".", "").Replace(",", "."));
            sSqltb017.Append(",TB017_ValorFinal =");
            sSqltb017.Append(valorfinal.ToString(CultureInfo.CurrentCulture).Replace(".", "").Replace(",", "."));
            sSqltb017.Append(" where TB017_id = ");
            sSqltb017.Append(tb017Id);

            cmdTb017.CommandText = sSqltb017.ToString();


            con.Open();
            SqlTransaction tran = con.BeginTransaction();

            try
            {
                cmdTb017.Transaction = tran;
                cmdTb017.ExecuteNonQuery();
                //Comando 1 executado com sucesso!
                tran.Commit();
                //Comando 2 executado com sucesso!
                ParcelasAlteracaoValorFinal(tb016Id, tb011Id);
                return true;
            }
            catch (SqlException ex)
            {
                tran.Rollback();

                throw ex;
            }
            finally
            {
                con.Close();
            }

        }

        public bool ParcelasAlteracaoValorFinal(long tb016Id, long tb011Id)
        {

            //ParcelaController valor = ValorParcela(tb016Id);

            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

            SqlCommand cmdTb016 = con.CreateCommand();

            var valorfinalparcela = ParcelaValorTotalProdutos(tb016Id);

            //Math.Truncate(valorfinalparcela)
            StringBuilder sSqltb016 = new StringBuilder();
            sSqltb016.Append("UPDATE TB016_Parcela SET TB016_Valor = ");
            sSqltb016.Append(valorfinalparcela.ToString().Replace(",", "."));
            sSqltb016.Append(", TB016_AlteradoPor=");
            sSqltb016.Append(tb011Id);
            sSqltb016.Append(", TB016_AlteradoEm=");
            sSqltb016.Append("'");
            sSqltb016.Append(DateTime.Now.ToString("MM/dd/yyy hh:mm"));
            sSqltb016.Append("'");
            sSqltb016.Append(" WHERE TB016_id = ");
            sSqltb016.Append(tb016Id);

            cmdTb016.CommandText = sSqltb016.ToString();

            con.Open();
            SqlTransaction tran = con.BeginTransaction();

            try
            {
                cmdTb016.Transaction = tran;
                cmdTb016.ExecuteNonQuery();
                //Comando 2 executado com sucesso!

                tran.Commit();
                return true;
            }
            catch (SqlException ex)
            {
                tran.Rollback();

                throw ex;
            }
            finally
            {
                con.Close();
            }


        }

        public bool ParcelaAlterarStauts(long tb016Id, long tb011Id, int status)
        {
            try
            {
                var sSql = new StringBuilder();
                sSql.Append(" UPDATE TB016_Parcela SET ");
                sSql.Append(" TB016_AlteradoEm = ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(", TB016_AlteradoPor = ");
                sSql.Append(tb011Id);
                sSql.Append(", TB016_Status = ");
                sSql.Append(status);
                sSql.Append(" WHERE TB016_id = ");
                sSql.Append(tb016Id);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    var myCommand = new SqlCommand(sSql.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }


        /// <summary>
        /// Descrição:  Alterar ciclo da parcela
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       09/10/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool ParcelasAlterarCiclo(long tb016Id, int ciclo, long tb011Id)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append(" UPDATE TB016_Parcela SET ");
                sSql.Append(" TB012_CicloContrato = ");
                sSql.Append(ciclo);
                sSql.Append(", TB016_AlteradoEm = ");
                sSql.Append("'");
                sSql.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sSql.Append("'");
                sSql.Append(", TB016_AlteradoPor = ");
                sSql.Append(tb011Id);
                sSql.Append(" WHERE TB016_id = ");
                sSql.Append(tb016Id);

                using (var con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    var myCommand = new SqlCommand(sSql.ToString(), con);
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

        public bool ParcelaAlterarVencimento(long tb016Id, long tb011Id, DateTime tb016Vencimento)
        {
            try
            {

                string updateSql = " UPDATE TB016_Parcela SET " +
                                   " TB016_Vencimento     ='" + tb016Vencimento.ToString("MM/dd/yyyy") + "'" +
                                   " ,TB016_AlteradoEm     ='" + DateTime.Now.ToString("MM/dd/yyyy hh:mm") + "'" +
                                   " ,TB016_AlteradoPor    =" + tb011Id +
                                   " where TB016_id        =" + tb016Id;

                using (SqlConnection myConnection = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    myConnection.Open();

                    SqlCommand myCommand = new SqlCommand(updateSql, myConnection);
                    myCommand.CommandTimeout = 300;

                    myCommand.ExecuteScalar();
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
            return true;
        }

        public bool AbonarAdesaoContrato(long TB016_id, long TB017_id, long TB012_id, double ValorAdesao)
        {

            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

            SqlCommand cmdUpdateTB016 = con.CreateCommand();
            SqlCommand cmdDeleteTB017 = con.CreateCommand();

            StringBuilder sUpdateTB016 = new StringBuilder();
            sUpdateTB016.Append("update TB016_Parcela set  TB016_Valor = TB016_Valor - ");
            sUpdateTB016.Append(ValorAdesao.ToString("N2").Replace(".", "").Replace(",", "."));
            sUpdateTB016.Append(", TB016_ValorAdesao =0");
            sUpdateTB016.Append(" where TB016_id=");
            sUpdateTB016.Append(TB016_id);

            StringBuilder sDelTB017 = new StringBuilder();
            sDelTB017.Append("delete from  TB017_ParcelaProduto where TB017_id = ");
            sDelTB017.Append(TB017_id);

            cmdUpdateTB016.CommandText = sUpdateTB016.ToString();
            cmdDeleteTB017.CommandText = sDelTB017.ToString();


            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmdUpdateTB016.Transaction = tran;
                cmdUpdateTB016.ExecuteScalar();
                //Comando 1 executado com sucesso!

                cmdDeleteTB017.Transaction = tran;
                cmdDeleteTB017.ExecuteScalar();
                ////Comando 2 executado com sucesso!


                tran.Commit();

            }
            catch (SqlException ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return true;
        }

        public bool SetarParcelaVencida( long AlteradoPor)
        {

            DateTime datareferencia = DateTime.Now.AddDays(-5);
       
            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
            try
            {

                StringBuilder sSQL = new StringBuilder();
                SqlCommand cmdUpdateTB016 = con.CreateCommand();
                SqlCommand cmdUpdateTB012 = con.CreateCommand();

                StringBuilder sInsertTb026 = new StringBuilder();
                sInsertTb026.Append("INSERT INTO ");
                sInsertTb026.Append("TB026_ContratoAnotacoes");
                sInsertTb026.Append("(");
                sInsertTb026.Append("TB012_id");
                sInsertTb026.Append(", TB011_Id");
                sInsertTb026.Append(", TB026_Data");
                sInsertTb026.Append(", TB026_Anotacao");
                sInsertTb026.Append(")");
                sInsertTb026.Append("VALUES");
                sSQL.Append(" SELECT TB012_id ");
                sSQL.Append(" FROM dbo.TB016_Parcela ");
                sSQL.Append(" WHERE TB016_Status < 3 ");
                sSQL.Append(" AND TB016_Vencimento <= ");
                sSQL.Append("'");
                sSQL.Append(datareferencia.ToString("MM/dd/yyyy"));
                sSQL.Append("'");
                sSQL.Append(" AND TB016_ValorPago IS NULL ");
                sSQL.Append(" GROUP BY TB012_id");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                StringBuilder sUpdateTB016 = new StringBuilder();
                sUpdateTB016.Append(" update TB016_Parcela ");
                sUpdateTB016.Append(" set ");
                sUpdateTB016.Append(" TB016_Status = 4 ");
                sUpdateTB016.Append(" ,TB016_AlteradoEm = ");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(" ,TB016_AlteradoPor = ");
                sUpdateTB016.Append(AlteradoPor);
                sUpdateTB016.Append(" where ");
                sUpdateTB016.Append(" TB016_Status < 3 ");
                sUpdateTB016.Append(" AND ");
                sUpdateTB016.Append(" TB016_Vencimento <= ");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(datareferencia.ToString("MM/dd/yyyy"));
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(" AND ");
                sUpdateTB016.Append(" TB016_ValorPago IS NULL ");

                StringBuilder sUpdateTB012 = new StringBuilder();
                cmdUpdateTB016.CommandText = sUpdateTB016.ToString();

              
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    cmdUpdateTB016.Transaction = tran;
                    cmdUpdateTB016.ExecuteScalar();
                    //Comando 1 executado com sucesso!

                    tran.Commit();

                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

     
            }
            catch (Exception ex)
            {
                throw ex;
            }

            new ContratosDao().SetarContratoInadimplente();
            return true;
        }

        public List<ParcelaController> ParcelasListarInadimplenciaAnual(DateTime DataReferencia)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT COUNT(dbo.TB012_Contratos.TB012_id) AS Total, CONVERT(CHAR(4), dbo.TB016_Parcela.TB016_Vencimento, 120) AS Ano, MONTH(dbo.TB016_Parcela.TB016_Vencimento) AS Mes ");
                sSQL.Append(" FROM dbo.TB016_Parcela INNER JOIN ");
                sSQL.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id ");
                sSQL.Append(" GROUP BY dbo.TB016_Parcela.TB016_Status, MONTH(dbo.TB016_Parcela.TB016_Vencimento), CONVERT(CHAR(4), dbo.TB016_Parcela.TB016_Vencimento, 120), dbo.TB012_Contratos.TB012_Status ");
                sSQL.Append(" HAVING dbo.TB016_Parcela.TB016_Status = 4   ");
                sSQL.Append(" AND dbo.TB012_Contratos.TB012_Status = 4   ");
                sSQL.Append(" AND CONVERT(CHAR(4), dbo.TB016_Parcela.TB016_Vencimento, 120) = ");
                sSQL.Append("'");
                sSQL.Append(DataReferencia.Year);
                sSQL.Append("'");
                sSQL.Append(" ORDER BY Ano, Mes ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.total = Convert.ToInt64(reader["Total"]);
                    obj.ano = Convert.ToInt16(reader["Ano"]);
                    obj.mes = Convert.ToInt16(reader["Mes"]);


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

        public List<ParcelaController> Inadimplencia(long TB037_Id,string query)
        {
            List<ParcelaController> Retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT dbo.TB012_Contratos.TB012_id, dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB012_Contratos.TB012_Inicio,");
                sSQL.Append(" dbo.TB012_Contratos.TB012_Fim, dbo.TB012_Contratos.TB012_CicloContrato, COUNT(dbo.TB016_Parcela.TB016_id) AS NParcelasEmAtraso, SUM(dbo.TB016_Parcela.TB016_Valor) AS ValorAtrazado,");
                sSQL.Append(" dbo.TB013_Pessoa.TB013_Tipo");
                sSQL.Append(" FROM dbo.TB012_Contratos INNER JOIN");
                sSQL.Append(" dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id INNER JOIN");
                sSQL.Append(" dbo.TB016_Parcela ON dbo.TB012_Contratos.TB012_id = dbo.TB016_Parcela.TB012_id");
                sSQL.Append(" WHERE(dbo.TB012_Contratos.TB012_Status = 4)");
                sSQL.Append(" GROUP BY dbo.TB012_Contratos.TB012_id, dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB012_Contratos.TB012_Inicio,");
                sSQL.Append(" dbo.TB012_Contratos.TB012_Fim, dbo.TB012_Contratos.TB012_CicloContrato, dbo.TB016_Parcela.TB016_Status, dbo.TB013_Pessoa.TB013_Tipo");
                sSQL.Append(" HAVING(dbo.TB016_Parcela.TB016_Status = 4) AND(dbo.TB012_Contratos.TB012_TipoContrato = 1) ");
                sSQL.Append(query);

               sSQL.Append(" ORDER BY dbo.TB012_Contratos.TB012_id");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
               
                    obj.TB012_id = Convert.ToInt64(reader["TB012_id"]);
                    var Tipo = reader["TB013_Tipo"].ToString();
                    if (Convert.ToInt16(Tipo) == 1)
                    {
                        if (reader["TB013_CPFCNPJ"].ToString().Trim() == "SEM CPF")
                        {
                            obj.TB016_CPFCNPJ = reader["TB013_CPFCNPJ"].ToString();
                        }
                        else
                        {
                            if (reader["TB013_CPFCNPJ"].ToString().Trim() == string.Empty)
                            {
                                obj.TB016_CPFCNPJ = "SEM CPF";
                            }
                            else
                            {
                                obj.TB016_CPFCNPJ = reader["TB013_CPFCNPJ"] is DBNull ? "SEM CPF" : Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"000\.000\.000\-00");
                            }
                        }
                    }
                    else
                    {
                        obj.TB016_CPFCNPJ = Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"00\.000\.000\/0000\-00");
                    }


                    obj.TB016_Pagador = reader["TB013_NomeCompleto"].ToString();

                    obj.TB012_Inicio = Convert.ToDateTime(reader["TB012_Inicio"].ToString());
                    obj.TB012_Fim = Convert.ToDateTime(reader["TB012_Fim"].ToString());
                    obj.TB012_CicloContrato = Convert.ToInt32( reader["TB012_CicloContrato"].ToString());
                    obj.NParcelasEmAtraso = Convert.ToInt16(reader["NParcelasEmAtraso"]);
                    obj.TB016_Valor = Convert.ToDouble(reader["ValorAtrazado"]);


                    Retorno.Add(obj);
                }

                con.Close();

                /**/
                StringBuilder sSQL1 = new StringBuilder();

                sSQL1.Append(" SELECT TOP (100) PERCENT dbo.TB012_Contratos.TB012_id, dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB012_Contratos.TB012_Inicio, ");
                sSQL1.Append(" dbo.TB012_Contratos.TB012_Fim, dbo.TB012_Contratos.TB012_CicloContrato, COUNT(dbo.TB016_Parcela.TB016_id) AS NParcelasEmAtraso, SUM(dbo.TB016_Parcela.TB016_Valor) AS ValorAtrazado, ");
                sSQL1.Append(" dbo.TB013_Pessoa.TB013_Tipo ");
                sSQL1.Append(" FROM dbo.TB012_Contratos INNER JOIN ");
                sSQL1.Append(" dbo.TB013_Pessoa ON dbo.TB012_Contratos.TB013_id = dbo.TB013_Pessoa.TB013_id INNER JOIN ");
                sSQL1.Append(" dbo.TB016_Parcela ON dbo.TB012_Contratos.TB012_id = dbo.TB016_Parcela.TB012_id ");
                sSQL1.Append(" WHERE(dbo.TB012_Contratos.TB012_Status = 6) ");
                if(TB037_Id>1)
                {

                    sSQL1.Append(" and dbo.TB012_Contratos.TB037_Id = ");

                    sSQL1.Append(TB037_Id);
                }
                sSQL1.Append(query);

                sSQL1.Append(" GROUP BY dbo.TB012_Contratos.TB012_id, dbo.TB012_Contratos.TB012_TipoContrato, dbo.TB013_Pessoa.TB013_CPFCNPJ, dbo.TB013_Pessoa.TB013_NomeCompleto, dbo.TB012_Contratos.TB012_Inicio, ");
                sSQL1.Append(" dbo.TB012_Contratos.TB012_Fim, dbo.TB012_Contratos.TB012_CicloContrato, dbo.TB016_Parcela.TB016_Status, dbo.TB013_Pessoa.TB013_Tipo ");
                sSQL1.Append(" HAVING(dbo.TB016_Parcela.TB016_Status < 3) AND(dbo.TB012_Contratos.TB012_TipoContrato = 1) ");
                sSQL1.Append(" ORDER BY dbo.TB012_Contratos.TB012_id ");

                command = new SqlCommand(sSQL1.ToString(), con);
                command.CommandTimeout = 300;
                con.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();

                    obj.TB012_id = Convert.ToInt64(reader["TB012_id"]);
                    var Tipo = reader["TB013_Tipo"].ToString();
                    if (Convert.ToInt16(Tipo) == 1)
                    {
                        if (reader["TB013_CPFCNPJ"].ToString().Trim() == "SEM CPF")
                        {
                            obj.TB016_CPFCNPJ = reader["TB013_CPFCNPJ"].ToString();
                        }
                        else
                        {
                            if (reader["TB013_CPFCNPJ"].ToString().Trim() == string.Empty)
                            {
                                obj.TB016_CPFCNPJ = "SEM CPF";
                            }
                            else
                            {
                                obj.TB016_CPFCNPJ = reader["TB013_CPFCNPJ"] is DBNull ? "SEM CPF" : Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"000\.000\.000\-00");
                            }
                        }
                    }
                    else
                    {
                        obj.TB016_CPFCNPJ = Convert.ToUInt64(reader["TB013_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"00\.000\.000\/0000\-00");
                    }


                    obj.TB016_Pagador = reader["TB013_NomeCompleto"].ToString();

                    obj.TB012_Inicio = Convert.ToDateTime(reader["TB012_Inicio"].ToString());
                    obj.TB012_Fim = Convert.ToDateTime(reader["TB012_Fim"].ToString());
                    obj.TB012_CicloContrato = Convert.ToInt32(reader["TB012_CicloContrato"].ToString());
                    obj.NParcelasEmAtraso = Convert.ToInt16(reader["NParcelasEmAtraso"]);
                    obj.TB016_Valor = Convert.ToDouble(reader["ValorAtrazado"]);


                    Retorno.Add(obj);
                }

                con.Close();

                /**/
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public List<ParcelaController> ListaParcelasVencidas(long tb012Id)
        {
            List<ParcelaController> retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();


                sSQL.Append(" SELECT dbo.TB016_Parcela.TB016_ValorPago,  dbo.TB016_Parcela.TB016_NossoNumero,  dbo.TB016_Parcela.TB016_DataPagamento,  dbo.TB016_Parcela.TB012_CicloContrato,dbo.TB016_Parcela.TB016_id, dbo.TB015_Planos.TB015_id, dbo.TB015_Planos.TB015_Plano, dbo.TB016_Parcela.TB016_Parcela, dbo.TB016_Parcela.TB016_TotalParcelas, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Emissao, dbo.TB016_Parcela.TB016_Vencimento, dbo.TB016_Parcela.TB016_Pagador, dbo.TB016_Parcela.TB016_CPFCNPJ, dbo.TB016_Parcela.TB016_Valor, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_ValorPago, dbo.TB016_Parcela.TB016_FormaPagamento, dbo.TB016_Parcela.TB016_DataPagamento, dbo.TB016_Parcela.TB016_Boleto, dbo.TB016_Parcela.TB016_ValorAdesao, ");
                sSQL.Append(" dbo.TB016_Parcela.TB016_Status");
                sSQL.Append(" FROM dbo.TB016_Parcela INNER JOIN");
                sSQL.Append(" dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id INNER JOIN");
                sSQL.Append(" dbo.TB015_Planos ON dbo.TB016_Parcela.TB015_id = dbo.TB015_Planos.TB015_id");
                sSQL.Append(" WHERE");
                sSQL.Append(" dbo.TB012_Contratos.TB012_id = ");
                sSQL.Append(tb012Id);
                sSQL.Append(" AND");
                sSQL.Append(" dbo.TB012_Contratos.TB012_TipoContrato = 1");
                //sSQL.Append(" AND");
                //sSQL.Append(" dbo.TB016_Parcela.TB012_CicloContrato = ");
                //sSQL.Append(tb012CicloContrato);


               
                    sSQL.Append(" AND dbo.TB016_Parcela.TB016_Status = 4");
                //sSQL.Append(" or  dbo.TB016_Parcela.TB016_Status = 4");


                sSQL.Append(" ORDER BY dbo.TB016_Parcela.TB016_id,dbo.TB016_Parcela.TB016_Parcela");



                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB016_NossoNumero = reader["TB016_NossoNumero"] is DBNull ? "------" : reader["TB016_NossoNumero"].ToString();

                    // reader["TB015_Plano"].ToString();
                    //// obj.TB016_ParcelaCodigo     = reader["TB016_ParcelaCodigo"] is DBNull ? "-" : reader["TB016_ParcelaCodigo"].ToString();

                    obj.TB016_Parcela = Convert.ToInt16(reader["TB016_Parcela"]);
                    obj.TB016_Emissao = Convert.ToDateTime(reader["TB016_Emissao"]);
                    obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);

                    obj.TB016_DataPagamento = reader["TB016_DataPagamento"] is DBNull ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["TB016_DataPagamento"].ToString());


                    obj.TB012_CicloContrato = Convert.ToInt32(reader["TB012_CicloContrato"]);
                    obj.TB016_Valor = Convert.ToDouble(reader["TB016_Valor"]);
                    obj.TB016_ValorPago = reader["TB016_ValorPago"] is DBNull ? 0 : Convert.ToDouble(reader["TB016_ValorPago"]);
                    //Convert.ToDouble(reader["TB016_ValorPago"]);


                    //obj.TB016_ValorPago = reader["TB025_ValorCobrado"] is DBNull ? 0 : Convert.ToDouble(reader["TB025_ValorCobrado"].ToString());

                    obj.TB016_FormaPagamentoS = reader["TB016_FormaPagamento"].ToString();

                    if (obj.TB016_FormaPagamentoS == "4")
                    {
                        obj.TB016_FormaPagamentoS = "6";
                    }
                    else
                    {
                        if (obj.TB016_FormaPagamentoS == "5")
                        {
                            obj.TB016_FormaPagamentoS = "6";
                        }
                    }

                    obj.TB016_StatusS = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(reader["TB016_Status"]));
                    obj.TB015_Plano = reader["TB015_Plano"].ToString();


                    retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public ParcelaController NegociacaoSimulacao(long tb012Id)
        {
            ParcelaController Retorno = new ParcelaController();
            List<ParcelaProdutosController> produtos = new List<ParcelaProdutosController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT ");
                sSQL.Append(" TB012_id ");
                sSQL.Append(" ,TB016_Status ");
                sSQL.Append(" ,TB016_id ");
                sSQL.Append(" ,TB016_Valor ");
                sSQL.Append(" ,TB016_ValorAdesao ");
                sSQL.Append(" ,ROUND(TB016_Valor - TB016_ValorAdesao, 2) AS Mensalidade ");
                sSQL.Append(" ,TB016_Vencimento ");
                sSQL.Append(" ,TB016_Juros ");
                sSQL.Append(" ,TB016_Multa ");
                sSQL.Append(" FROM ");
                sSQL.Append(" dbo.TB016_Parcela ");
                sSQL.Append(" WHERE ");
                sSQL.Append(" TB016_Status = 4 ");
                sSQL.Append(" AND ");
                sSQL.Append(" TB012_id =  ");
                sSQL.Append(tb012Id);
                sSQL.Append(" ORDER BY TB016_id ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<ParcelaController> lista = new List<ParcelaController>();
                double vValorAdesao = 0;
                double vValorMensalidades = 0;
                //produtos
                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();
                    obj.TB016_Juros                     = Convert.ToDouble(reader["TB016_Juros"]);
                    obj.TB016_Multa                     = Convert.ToDouble(reader["TB016_Multa"]);                 
                    vValorAdesao                        = vValorAdesao + Convert.ToDouble(reader["TB016_ValorAdesao"]);
                    vValorMensalidades                  = vValorMensalidades + Convert.ToDouble(reader["Mensalidade"]);

                    lista.Add(obj);


                    foreach (var item in ListaProdutosPorParcela(Convert.ToInt64(reader["TB016_id"])))
                    {
                        ParcelaProdutosController produto = new ParcelaProdutosController();
                        produto.TB017_id                = item.TB017_id;
                        produto.TB016_id                = Convert.ToInt64(reader["TB016_id"]);
                        produto.TB017_Item              = item.TB017_Item;
                        produto.TB017_ValorUnitario     = item.TB017_ValorUnitario;
                        produto.TB017_ValorDesconto     = item.TB017_ValorDesconto;
                        produto.TB017_ValorFinal        = item.TB017_ValorFinal;

                        produtos.Add(produto);
                    }
                }
                con.Close();

                if(lista.Count>0)
                {
                    Retorno.TB016_NParcelasAtrazo       = lista.Count;
                    Retorno.TB016_Juros                 = lista[0].TB016_Juros;
                    Retorno.TB016_Multa                 = lista[0].TB016_Multa;
                    Retorno.TB016_ValorAdesao           = vValorAdesao;
                    Retorno.TB016_ValorMensalidades     = vValorMensalidades;
                    Retorno.ParcelaProduto_L            = produtos;
                }
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }


        /// <summary>
        /// Descrição:  Cancelar parcelas de um contrato tambem cancelado
        /// Autor:      Fabiano Gonçalves Elias
        /// Data:       02/02/2017
        /// **********************************************************************************************************
        /// Data Alteração      Autor       Descrição
        /// </summary>
        public bool ParcelasVencidas(long TB012_id)
        {
            try
            {
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" UPDATE TB016_Parcela SET ");
                sSQL.Append(" TB016_Status = 3");
                sSQL.Append(" WHERE TB012_id = ");
                sSQL.Append(TB012_id);
                sSQL.Append(" and TB016_Status = 4");

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSQL.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;

        }

        public List<ParcelaController> ListaBoletosParaEnvio(long tb012Id)
        {
            List<ParcelaController> retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();


                sSQL.Append(" SELECT  TB016_id ,TB016_NossoNumero, TB015_id, TB016_Boleto, TB016_Vencimento FROM dbo.TB016_Parcela WHERE(TB016_Status = 2) AND(NOT(TB016_NBoleto IS NULL)) AND(NOT(TB016_Boleto IS NULL))  ");
                sSQL.Append("  AND TB012_id = ");
                sSQL.Append(tb012Id);
                sSQL.Append(" ORDER BY TB015_id, TB016_id ASC");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj = new ParcelaController();

                    //obj.TB031_Tipo = Convert.ToInt64(reader["TB016_id"]);

                    if(Convert.ToInt64(reader["TB015_id"])==0)
                    {
                        obj.DesParcela = "Negociação";
                    }
                    else
                    {
                        obj.DesParcela = "Parcela";
                    }
                    obj.TB016_id = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB016_Boleto = reader["TB016_Boleto"].ToString();
                    obj.TB016_Vencimento = Convert.ToDateTime(reader["TB016_Vencimento"]);


                    retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public ParcelaController ParceladoArquivoRemessa(string cpf,DateTime vencimento)
        {
            ParcelaController obj = new ParcelaController();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT ");
                sSQL.Append(" TB016_id ");
                sSQL.Append(" , TB012_id ");
                sSQL.Append(" , TB016_CPFCNPJ "); 
                sSQL.Append(" , TB016_Vencimento ");
                sSQL.Append(" , TB016_Valor ");
                sSQL.Append(" , TB016_ValorPago ");
                sSQL.Append(" , TB016_Status ");
                sSQL.Append(" FROM  ");
                sSQL.Append(" dbo.TB016_Parcela ");
                sSQL.Append(" WHERE ");
                sSQL.Append(" TB016_Vencimento =  ");
                sSQL.Append("'");
                sSQL.Append(vencimento.ToString("MM/dd/yyyy"));
                sSQL.Append("'");
                sSQL.Append(" AND ");
                sSQL.Append(" REPLACE(REPLACE(REPLACE(REPLACE(TB016_CPFCNPJ, '.', ''), '-', ''), '/', ''), ',', '') = ");
                sSQL.Append("'");
                sSQL.Append(cpf.Replace(",","").Replace(".", "").Replace("-", "").Replace("/", ""));
                sSQL.Append("'");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    obj.TB016_id            = Convert.ToInt64(reader["TB016_id"]);
                    obj.TB012_id            = Convert.ToInt64(reader["TB012_id"]);
                    obj.TB016_Vencimento    = Convert.ToDateTime(reader["TB016_Vencimento"]);
                    obj.TB016_Valor         = Convert.ToDouble( reader["TB016_Valor"]);
                    obj.TB016_ValorPago     = reader["TB016_ValorPago"] is DBNull ? 0 : Convert.ToDouble(reader["TB016_ValorPago"]);
                    obj.TB016_StatusS       = reader["TB016_Status"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public bool parcelaBaixaViaPlanilhaRemessa_AtivarContrato(ParcelaController parcela)
        {

           

            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
            try
            {

                StringBuilder sSQL = new StringBuilder();
                SqlCommand cmdUpdateTB016 = con.CreateCommand();
                SqlCommand cmdUpdateTB012_0 = con.CreateCommand();
                SqlCommand cmdUpdateTB012_4 = con.CreateCommand();
                SqlCommand cmdUpdateTB013 = con.CreateCommand();

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                StringBuilder sUpdateTB016 = new StringBuilder();
                sUpdateTB016.Append(" update TB016_Parcela ");
                sUpdateTB016.Append(" set ");
                sUpdateTB016.Append(" TB016_Banco = ");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(parcela.TB016_Banco.Trim());
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(", TB016_Agencia = ");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(parcela.TB016_Agencia.Trim());
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(", TB016_ContaCorrente = ");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(parcela.TB016_ContaCorrente.Trim());
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(" ,TB016_Status = 5 ");
                sUpdateTB016.Append(" ,TB016_FormaProcessamentoBaixa = 5 ");
                sUpdateTB016.Append(" ,TB016_AlteradoEm = ");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(" ,TB016_AlteradoPor = ");
                sUpdateTB016.Append(parcela.TB016_AlteradoPor);
                sUpdateTB016.Append(" ,TB016_ValorPago = ");
                sUpdateTB016.Append(parcela.TB016_ValorPago.ToString().Replace(",", "."));
                sUpdateTB016.Append(" ,TB016_DataPagamento = ");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(parcela.TB016_DataPagamento.ToString("MM/dd/yyyy"));
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(" where ");
                sUpdateTB016.Append(" TB016_id =");
                sUpdateTB016.Append(parcela.TB016_id);


                StringBuilder sUpdateTB012_0 = new StringBuilder();
                sUpdateTB012_0.Append(" update TB012_Contratos ");
                sUpdateTB012_0.Append(" set ");
                sUpdateTB012_0.Append(" TB012_Status = 1 ");
                sUpdateTB012_0.Append(" ,TB012_AlteradoEm = ");
                sUpdateTB012_0.Append("'");
                sUpdateTB012_0.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sUpdateTB012_0.Append("'");
                sUpdateTB012_0.Append(" ,TB012_AlteradoPor = ");
                sUpdateTB012_0.Append(parcela.TB016_AlteradoPor);
                sUpdateTB012_0.Append(" where TB012_Status =0 ");
                sUpdateTB012_0.Append(" and TB012_id = ");
                sUpdateTB012_0.Append(parcela.TB012_id);



                StringBuilder sUpdateTB012_4 = new StringBuilder();
                sUpdateTB012_4.Append(" update TB012_Contratos ");
                sUpdateTB012_4.Append(" set ");
                sUpdateTB012_4.Append(" TB012_Status = 1 ");
                sUpdateTB012_4.Append(" ,TB012_AlteradoEm = ");
                sUpdateTB012_4.Append("'");
                sUpdateTB012_4.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                sUpdateTB012_4.Append("'");
                sUpdateTB012_4.Append(" ,TB012_AlteradoPor = ");
                sUpdateTB012_4.Append(parcela.TB016_AlteradoPor);
                sUpdateTB012_4.Append(" where TB012_Status =4 ");
                sUpdateTB012_4.Append(" and TB012_id = ");
                sUpdateTB012_4.Append(parcela.TB012_id);

                StringBuilder sUpdateTB013 = new StringBuilder();
                sUpdateTB013.Append(" update TB013_Pessoa set TB013_Status = 1 where TB013_Status= 0 and ");
                sUpdateTB013.Append("  TB012_id = ");
                sUpdateTB013.Append(parcela.TB012_id);


                cmdUpdateTB016.CommandText      = sUpdateTB016.ToString();
                cmdUpdateTB012_0.CommandText    = sUpdateTB012_0.ToString();
                cmdUpdateTB012_4.CommandText    = sUpdateTB012_4.ToString();
                cmdUpdateTB013.CommandText      = sUpdateTB013.ToString();

                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    cmdUpdateTB016.Transaction = tran;
                    cmdUpdateTB016.ExecuteScalar();

                    cmdUpdateTB012_0.Transaction = tran;
                    cmdUpdateTB012_0.ExecuteScalar();

                    cmdUpdateTB012_4.Transaction = tran;
                    cmdUpdateTB012_4.ExecuteScalar();

                    cmdUpdateTB013.Transaction = tran;
                    cmdUpdateTB013.ExecuteScalar();

                    tran.Commit();

                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    con.Close();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            new ContratosDao().SetarContratoInadimplente();
            return true;
        }

        public bool produtoIncluirManualmente(ParcelaProdutosController produto, ParcelaController parcela)
        {
            SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
            try
            {
                StringBuilder sSQL = new StringBuilder();
                SqlCommand cmdInsertTB017 = con.CreateCommand();
                SqlCommand cmdUpdateTB016 = con.CreateCommand();
                

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                StringBuilder sInsertTB017 = new StringBuilder();
                sInsertTB017.Append("INSERT INTO TB017_ParcelaProduto ");
                sInsertTB017.Append("(");
                sInsertTB017.Append("TB016_id");
                sInsertTB017.Append(",");
                sInsertTB017.Append("TB017_ValorUnitario");
                sInsertTB017.Append(",");
                sInsertTB017.Append("TB017_ValorDesconto");
                sInsertTB017.Append(",");
                sInsertTB017.Append("TB017_ValorFinal");
                sInsertTB017.Append(",");
                sInsertTB017.Append("TB017_Item");
                sInsertTB017.Append(",");
                sInsertTB017.Append("TB017_Tipo");
                sInsertTB017.Append(" ) VALUES ( ");
                sInsertTB017.Append(produto.TB016_id);
                sInsertTB017.Append(",");
                sInsertTB017.Append(produto.TB017_ValorUnitario.ToString().Replace(",", "."));
                sInsertTB017.Append(",");
                sInsertTB017.Append(produto.TB017_ValorDesconto);
                sInsertTB017.Append(",");
                sInsertTB017.Append(produto.TB017_ValorFinal.ToString().Replace(",", "."));
                sInsertTB017.Append(",");
                sInsertTB017.Append("'");
                sInsertTB017.Append(produto.TB017_Item.TrimEnd());
                sInsertTB017.Append("'");
                sInsertTB017.Append(",");
                sInsertTB017.Append(produto.TB017_TipoS);
                sInsertTB017.Append(")");


                StringBuilder sUpdateTB016 = new StringBuilder();
                sUpdateTB016.Append(" update TB016_Parcela ");
                sUpdateTB016.Append(" set ");
                sUpdateTB016.Append(" TB016_Valor = TB016_Valor + ");
                sUpdateTB016.Append(produto.TB017_ValorFinal.ToString().Replace(",", "."));
                sUpdateTB016.Append(" ,TB016_AlteradoEm= ");
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(parcela.TB016_AlteradoEm.ToString("MM/dd/yyyy hh:mm"));
                sUpdateTB016.Append("'");
                sUpdateTB016.Append(" ,TB016_AlteradoPor= ");
                sUpdateTB016.Append(parcela.TB016_AlteradoPor );
                sUpdateTB016.Append(" where TB016_id = ");
                sUpdateTB016.Append(parcela.TB016_id);


                cmdInsertTB017.CommandText = sInsertTB017.ToString();
                cmdUpdateTB016.CommandText = sUpdateTB016.ToString();

                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    cmdInsertTB017.Transaction = tran;
                    cmdInsertTB017.ExecuteScalar();

                    cmdUpdateTB016.Transaction = tran;
                    cmdUpdateTB016.ExecuteScalar();

                    tran.Commit();
                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    con.Close();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            new ContratosDao().SetarContratoInadimplente();
            return true;
        }

        public long validarPendenciaFinanceira(long TB012_id)
        {
            long Retorno = 0;
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append(" SELECT ");
                sSQL.Append(" COUNT(TB016_id)AS Pendencia ");
                sSQL.Append(" FROM ");
                sSQL.Append(" dbo.TB016_Parcela ");
                sSQL.Append(" WHERE ");
                sSQL.Append(" TB012_id =  ");
                sSQL.Append(TB012_id);
                sSQL.Append(" AND ");
                sSQL.Append(" TB016_Status = 4 ");


                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Retorno = Convert.ToInt64(reader["Pendencia"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public List<ParcelaController> negociadoresLista()
        {
            List<ParcelaController> retorno = new List<ParcelaController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();


                sSQL.Append(" SELECT * from TB037_NegociacaoEntidade");             
                sSQL.Append(" ORDER BY dbo.TB037_NegociacaoEntidade.TB037_Negociador");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ParcelaController obj   = new ParcelaController();
                    obj.TB037_Id            = Convert.ToInt64(reader["TB037_Id"]);
                    obj.TB037_Negociador    = reader["TB037_Negociador"].ToString().TrimEnd();

              


                    retorno.Add(obj);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public long BuscarNossoNumeroPorIdParcela(long idParcela)
        {
            long retorno = 0;
            try
            {
                var con = new SqlConnection(ParametrosDAO.StringConexao);

                var sSQL = new StringBuilder();
                sSQL.Append(" SELECT TB016_NossoNumero ");
                sSQL.Append("   FROM TB016_Parcela ");
                sSQL.Append("  WHERE TB016_id = ");
                sSQL.Append(idParcela);

                var command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno = reader["TB016_NossoNumero"] is DBNull ? 0 : Convert.ToInt64(reader["TB016_NossoNumero"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public bool AlterarNossoNumeroPorParcela(long idParcela, long nossoNumero)
        {
            try
            {
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append(" UPDATE TB016_Parcela ");
                sSQL.Append("    SET TB016_NossoNumero = ");
                sSQL.Append(nossoNumero);
                sSQL.Append("  WHERE TB016_id = ");
                sSQL.Append(idParcela);

                using (SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    con.Open();
                    SqlCommand myCommand = new SqlCommand(sSQL.ToString(), con);
                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}

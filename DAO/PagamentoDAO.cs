
using Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class PagamentoDAO
    {
        public Int64 ValidaParcelaJaIncluida(Int64 TB025_BancoOrigem, Int64 TB025_DocumentoBanco)
        {
            Int64 TB025_id = 0;
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append("SELECT TB025_BancoOrigem,TB025_id ");
                sSQL.Append("FROM ");
                sSQL.Append("dbo.TB025_Pagamentos ");
                sSQL.Append(" WHERE TB025_BancoOrigem = ");
                sSQL.Append(TB025_BancoOrigem);
                sSQL.Append("AND TB025_DocumentoBanco =  ");
                sSQL.Append(TB025_DocumentoBanco);

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PagamentosController obj = new PagamentosController();

                    TB025_id = Convert.ToInt64(reader["TB025_id"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TB025_id;
        }
        public long PagamentoIncluir(PagamentosController Pagamento)
        {
            Int64 vTB025_id = 0;
            try
            {
                string InsertTB025 = "INSERT INTO TB025_Pagamentos ( " +
                                        " TB025_CPFCNPJ, " +
                                        " TB025_Emissao, " +
                                        " TB025_Vencimento, " +
                                        " TB025_DataLiquidacao, " +
                                        " TB025_DataMovimentacao, " +
                                        " TB025_DataLiquidacaoCredito, " +
                                        " TB016_id, " +
                                        " TB025_DocumentoBanco, " +
                                        " TB025_Modalidade, " +
                                        " TB025_ContaCorrente, " +
                                        " TB025_BancoRecebedor, " +
                                        " TB025_AgenciaRecebedora, " +
                                        " TB025_ValorTitulo, " +
                                        " TB025_ValorIOF, " +
                                        " TB025_ValorTarifa, " +
                                        " TB025_ValorCobrado, " +
                                        " TB025_CodigoMovimento, " +
                                        " TB025_CadastradoEm, " +
                                        " TB025_CadastradoPor, " +
                                        " TB025_AlteradoEm, " +
                                        " TB025_AlteradoPor,TB025_FormaProcessamento,TB025_FormaPagamento,TB025_BancoOrigem,TB025_NossoNumero " +
                                        " ) VALUES ( " +
                                        "'" +
                                        Pagamento.TB025_CPFCNPJ.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim() +
                                        "'" +
                                        ", " +
                                        "'" +
                                        Pagamento.TB025_Emissao.ToString("MM/dd/yyyy") +
                                        "'" +
                                        ", " +
                                        "'" +
                                        Pagamento.TB025_Vencimento.ToString("MM/dd/yyyy") +
                                        "'" +
                                        ", " +
                                        "'" +
                                        Pagamento.TB025_DataLiquidacao.ToString("MM/dd/yyyy") +
                                        "'" +
                                        ", " +
                                        "'" +
                                        Pagamento.TB025_DataMovimentacao.ToString("MM/dd/yyyy") +
                                        "'" +
                                        ", " +
                                        "'" +
                                        Pagamento.TB025_DataLiquidacaoCredito.ToString("MM/dd/yyyy") +
                                        "'" +
                                        ", " +
                                        Pagamento.TB016_id +
                                        ", " +
                                        Pagamento.TB025_DocumentoBanco +
                                        ", " +
                                        Pagamento.TB025_Modalidade +
                                        ", " +
                                        "'" +
                                        Pagamento.TB025_ContaCorrente +
                                        "'" +
                                        ", " +
                                        "'" +
                                        Pagamento.TB025_BancoRecebedor +
                                        "'" +
                                        ", " +
                                        "'" +
                                        Pagamento.TB025_AgenciaRecebedora +
                                        "'" +
                                        ", " +
                                        Pagamento.TB025_ValorTitulo.ToString().Replace(".", "").Replace(",", ".") +
                                        ", " +
                                        Pagamento.TB025_ValorIOF.ToString().Replace(".", "").Replace(",", ".") +
                                        ", " +
                                        Pagamento.TB025_ValorTarifa.ToString().Replace(".", "").Replace(",", ".") +
                                        ", " +
                                        Pagamento.TB025_ValorCobrado.ToString().Replace(".", "").Replace(",", ".") +
                                        ", " +
                                        Pagamento.TB025_CodigoMovimento +
                                        ", " +
                                        "'" +
                                        Pagamento.TB025_CadastradoEm.ToString("MM/dd/yyyy hh:mm") +
                                        "'" +
                                        ", " +
                                        Pagamento.TB025_CadastradoPor +
                                        ", " +
                                        "'" +
                                        Pagamento.TB025_AlteradoEm.ToString("MM/dd/yyyy hh:mm") +
                                        "'" +
                                        ", " +
                                        Pagamento.TB025_AlteradoPor +
                                        ", " +
                                        Pagamento.TB025_FormaProcessamentoS +
                                        ", " +
                                        Pagamento.TB025_FormaPagamentoS +
                                        ", " +
                                        Pagamento.TB025_BancoOrigem +
                                        ", " +
                                        Pagamento.TB025_NossoNumero +

                                        " ) SELECT SCOPE_IDENTITY()";
                string UpdateTB016_0 = "update TB016_Parcela set TB016_Status = 5 " +
                                    ", TB016_DataMovimentacao = '" + Pagamento.TB025_DataMovimentacao.ToString("MM/dd/yyyy") + "'" +
                                    ", TB016_DataPagamento = '" + Pagamento.TB025_DataLiquidacao.ToString("MM/dd/yyyy") + "'" +
                                    ", TB016_DocumentoBanco =" +
                                    Pagamento.TB025_DocumentoBanco +
                                    ", TB016_BancoRecebedor =" +
                                    "'" +
                                    Pagamento.TB025_BancoRecebedor +
                                    "'" +
                                    ", TB016_AgenciaRecebedora =" +
                                    "'" +
                                    Pagamento.TB025_AgenciaRecebedora +
                                    "'" +
                                    ", TB016_ValorTitulo =" +
                                    Pagamento.TB025_ValorTitulo.ToString().Replace(".", "").Replace(",", ".").Replace("R$", "") +
                                    ", TB016_ValorIOF =" +
                                    Pagamento.TB025_ValorIOF.ToString().Replace(".", "").Replace(",", ".").Replace("R$", "") +
                                    ", TB016_ValorTarifa =" +
                                    Pagamento.TB025_ValorTarifa.ToString().Replace(".", "").Replace(",", ".").Replace("R$", "") +
                                    ", TB016_ValorBruto =" +
                                    Pagamento.TB025_ValorCobrado.ToString().Replace(".", "").Replace(",", ".").Replace("R$", "") +
                                    ", TB016_ValorOutrosDesconto =0" +
                                    ", TB016_AlteradoEm =" +
                                    "'" +
                                    Pagamento.TB025_AlteradoEm.ToString("MM/dd/yyyy hh:mm") +
                                    "'" +
                                    ", TB016_AlteradoPor =" +
                                    Pagamento.TB025_AlteradoPor +
                                    ", TB016_FormaProcessamentoBaixa =1" +
                                    "   where TB016_NossoNumero ='" + Pagamento.TB025_NossoNumero + "'" +
                                    " and  TB016_Status < 3";


                string UpdateTB016_4 = "update TB016_Parcela set TB016_Status = 5 " +
                                    ", TB016_DataMovimentacao = '" + Pagamento.TB025_DataMovimentacao.ToString("MM/dd/yyyy") + "'" +
                                    ", TB016_DataPagamento = '" + Pagamento.TB025_DataLiquidacao.ToString("MM/dd/yyyy") + "'" +
                                    ", TB016_DocumentoBanco =" +
                                    Pagamento.TB025_DocumentoBanco +
                                    ", TB016_BancoRecebedor =" +
                                    "'" +
                                    Pagamento.TB025_BancoRecebedor +
                                    "'" +
                                    ", TB016_AgenciaRecebedora =" +
                                    "'" +
                                    Pagamento.TB025_AgenciaRecebedora +
                                    "'" +
                                    ", TB016_ValorTitulo =" +
                                    Pagamento.TB025_ValorTitulo.ToString().Replace(".", "").Replace(",", ".").Replace("R$", "") +
                                    ", TB016_ValorIOF =" +
                                    Pagamento.TB025_ValorIOF.ToString().Replace(".", "").Replace(",", ".").Replace("R$", "") +
                                    ", TB016_ValorTarifa =" +
                                    Pagamento.TB025_ValorTarifa.ToString().Replace(".", "").Replace(",", ".").Replace("R$", "") +
                                    ", TB016_ValorBruto =" +
                                    Pagamento.TB025_ValorCobrado.ToString().Replace(".", "").Replace(",", ".").Replace("R$", "") +
                                    ", TB016_ValorOutrosDesconto =0" +
                                    ", TB016_AlteradoEm =" +
                                    "'" +
                                    Pagamento.TB025_AlteradoEm.ToString("MM/dd/yyyy hh:mm") +
                                    "'" +
                                    ", TB016_AlteradoPor =" +
                                    Pagamento.TB025_AlteradoPor +
                                    ", TB016_FormaProcessamentoBaixa =1" +
                                    "   where TB016_NossoNumero ='" + Pagamento.TB025_NossoNumero + "'" +
                                    " and  TB016_Status =4";

                string UpdateTB012 = "update dbo.TB012_Contratos set TB012_Status = 1 " +
                  " where TB012_id = ( SELECT dbo.TB016_Parcela.TB012_id FROM dbo.TB016_Parcela INNER JOIN dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id WHERE dbo.TB016_Parcela.TB016_NossoNumero = '" + Pagamento.TB025_NossoNumero + "') and TB012_Status = 0";

                string UpdateTB013 = "update dbo.TB013_Pessoa set TB013_Status = 1 WHERE TB012_id = ( SELECT dbo.TB016_Parcela.TB012_id FROM dbo.TB016_Parcela INNER JOIN dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id WHERE dbo.TB016_Parcela.TB016_NossoNumero = '" + Pagamento.TB025_NossoNumero + "' ) AND(TB013_Status = 0)";

                string UpdateTB012_2 = "update dbo.TB012_Contratos set TB012_Status = 1 " +
                 " where TB012_id = ( SELECT dbo.TB016_Parcela.TB012_id FROM dbo.TB016_Parcela INNER JOIN dbo.TB012_Contratos ON dbo.TB016_Parcela.TB012_id = dbo.TB012_Contratos.TB012_id WHERE dbo.TB016_Parcela.TB016_NossoNumero = '" + Pagamento.TB025_NossoNumero + "') and TB012_Status = 4";


                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);

                SqlCommand cmdInsertTB016 = con.CreateCommand();
                SqlCommand cmdUpdateTB016_0 = con.CreateCommand();
                SqlCommand cmdUpdateTB016_4 = con.CreateCommand();
                SqlCommand cmdUpdateTB012 = con.CreateCommand();
                SqlCommand cmdUpdateTB013 = con.CreateCommand();
                SqlCommand cmdUpdateTB012_2 = con.CreateCommand();



                SqlCommand cmdTB020 = con.CreateCommand();
                StringBuilder sSQLTB020 = new StringBuilder();
                sSQLTB020.Append("UPDATE TB020_Unidades SET ");
                
                    sSQLTB020.Append("TB020_Status = 1");
                    sSQLTB020.Append(",TB020_AlteradoEm = ");
                    sSQLTB020.Append("'");
                    sSQLTB020.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                    sSQLTB020.Append("'");
                    sSQLTB020.Append(",TB020_AlteradoPor=");
                    sSQLTB020.Append(Pagamento.TB025_AlteradoPor);
                    sSQLTB020.Append(" where TB012_id =");
                    sSQLTB020.Append(Pagamento.TB012_id);
                    sSQLTB020.Append(" and TB020_Status = 0");



                cmdTB020.CommandText = sSQLTB020.ToString();
                cmdInsertTB016.CommandText = InsertTB025.ToString();
                cmdUpdateTB016_0.CommandText = UpdateTB016_0.ToString();
                cmdUpdateTB016_4.CommandText = UpdateTB016_4.ToString();
                cmdUpdateTB012.CommandText = UpdateTB012.ToString();
                cmdUpdateTB013.CommandText = UpdateTB013.ToString();
                cmdUpdateTB012_2.CommandText = UpdateTB012_2.ToString();

                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {

                    cmdInsertTB016.Transaction = tran;
                    vTB025_id = Convert.ToInt64(cmdInsertTB016.ExecuteScalar());
                    //Comando 1 executado com sucesso!

                    cmdUpdateTB016_0.Transaction = tran;
                    cmdUpdateTB016_0.ExecuteNonQuery();
                    //Comando 2 executado com sucesso!

                    cmdUpdateTB012.Transaction = tran;
                    cmdUpdateTB012.ExecuteNonQuery();
                    //Comando 3 executado com sucesso!

                    cmdUpdateTB013.Transaction = tran;
                    cmdUpdateTB013.ExecuteNonQuery();
                    //Comando 4 executado com sucesso!

                    cmdUpdateTB012_2.Transaction = tran;
                    cmdUpdateTB012_2.ExecuteNonQuery();
                    //Comando 5 executado com sucesso!

                    cmdUpdateTB016_4.Transaction = tran;
                    cmdUpdateTB016_4.ExecuteNonQuery();
                    //Comando 6 executado com sucesso!

                    cmdTB020.Transaction = tran;
                    cmdTB020.ExecuteNonQuery();
                    //Comando 7 executado com sucesso!


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

            return vTB025_id;
        }
        public bool SP_U_TB013_GerarCartaoMassa()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ParametrosDAO.StringConexao))
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandTimeout = 300;
                    command.Connection = connection;
                    command.CommandText = "SP_U_TB013_GerarCartaoMassa";
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public List<PagamentosController> PagamentosMesAno(int Mes, int Ano)
        {
            List<PagamentosController> Retorno = new List<PagamentosController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                DateTime Inicio = new DateTime(Ano, Mes, 1);
                DateTime Fim = new DateTime(Ano, Mes, DateTime.DaysInMonth(Ano, Mes)).AddHours(23).AddMinutes(59).AddSeconds(59);

                sSQL.Append("SELECT ");
                sSQL.Append("dbo.TB025_Pagamentos.TB025_id");
                sSQL.Append(", dbo.TB025_Pagamentos.TB025_DataLiquidacao");
                sSQL.Append(", dbo.TB025_Pagamentos.TB025_NossoNumero");
                sSQL.Append(", dbo.TB016_Parcela.TB016_id");
                sSQL.Append(", dbo.TB025_Pagamentos.TB025_CPFCNPJ");
                sSQL.Append(", dbo.TB016_Parcela.TB012_id");
                sSQL.Append(", dbo.TB016_Parcela.TB016_CPFCNPJ");
                sSQL.Append(", dbo.TB016_Parcela.TB016_Pagador");
                sSQL.Append(", dbo.TB016_Parcela.TB016_Status");
                sSQL.Append(", dbo.TB025_Pagamentos.TB025_DataMovimentacao AS Inicio");
                sSQL.Append(", dbo.TB025_Pagamentos.TB025_DataMovimentacao AS Fim");
                sSQL.Append(" FROM ");
                sSQL.Append(" dbo.TB025_Pagamentos ");
                sSQL.Append(" LEFT OUTER JOIN");
                sSQL.Append(" dbo.TB016_Parcela ON dbo.TB025_Pagamentos.TB025_NossoNumero = dbo.TB016_Parcela.TB016_NossoNumero");
                sSQL.Append(" WHERE dbo.TB025_Pagamentos.TB025_DataLiquidacao BETWEEN CONVERT(DATETIME,");
                sSQL.Append("'");
                sSQL.Append(Inicio.ToString("MM/dd/yyyy HH:mm:ss"));
                sSQL.Append("'");
                sSQL.Append(", 102) and CONVERT(DATETIME,");
                sSQL.Append("'");
                sSQL.Append(Fim.ToString("MM/dd/yyyy HH:mm:ss"));
                sSQL.Append("'");
                sSQL.Append(", 102)");
                sSQL.Append("ORDER BY dbo.TB016_Parcela.TB016_id, dbo.TB025_Pagamentos.TB025_NossoNumero");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PagamentosController obj = new PagamentosController();
                    
                    obj.TB016_id = reader["TB016_id"] is DBNull ? 0: Convert.ToInt64( reader["TB016_id"].ToString());     
                    obj.TB025_id = Convert.ToInt64(reader["TB025_id"]);

                    if (obj.TB025_id == 64729)
                    {
                        var temp = obj.TB025_id;
                    }

                    string CPFCNPJ25 = reader["TB025_CPFCNPJ"].ToString().Trim();

                    if (CPFCNPJ25.Length == 11)
                    {
                        obj.TB025_CPFCNPJ = Convert.ToUInt64(reader["TB025_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"000\.000\.000\-00");
                    }
                    else
                    {
                        obj.TB025_CPFCNPJ = Convert.ToUInt64(reader["TB025_CPFCNPJ"].ToString().TrimEnd().TrimStart()).ToString(@"00\.000\.000\/0000\-00");
                    }

                    obj.TB025_DataLiquidacao = Convert.ToDateTime(reader["TB025_DataLiquidacao"]);
                    obj.TB025_NossoNumero = Convert.ToInt64(reader["TB025_NossoNumero"].ToString().Trim());


                    obj.TB012_id = reader["TB012_id"] is DBNull ? 0 : Convert.ToInt64(reader["TB012_id"].ToString());
                    string CPFCNPJ16 = reader["TB016_CPFCNPJ"] is DBNull ? "NÃO ENCONTRADO" : reader["TB016_CPFCNPJ"].ToString();
                    CPFCNPJ16 = CPFCNPJ16.Replace(".","").Replace(",", "").Replace("-", "").Replace("/", "");

                    if (CPFCNPJ16!= "NÃO ENCONTRADO")
                    {
                        if (CPFCNPJ16.Length == 11)
                        {
                            obj.TB016_CPFCNPJ = Convert.ToUInt64(CPFCNPJ16.ToString().TrimEnd().TrimStart()).ToString(@"000\.000\.000\-00");
                        }
                        else
                        {

                        }
                    }
                    obj.TB016_Pagador = reader["TB016_Pagador"] is DBNull ? "NÃO ENCONTRADO" : reader["TB016_Pagador"].ToString();
                    obj.TB016_StatusS = reader["TB016_Status"] is DBNull ? "NÃO ENCONTRADO" : Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(reader["TB016_Status"]));
             

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
        public List<PagamentosController> ParcelasListarErrosSICOOB(DateTime Inicio, DateTime fim)
        {
            List<PagamentosController> Retorno = new List<PagamentosController>();
            try
            {
                SqlConnection con = new SqlConnection(ParametrosDAO.StringConexao);
                StringBuilder sSQL = new StringBuilder();

                sSQL.Append("  SELECT  ");

                sSQL.Append("  dbo.TB025_Pagamentos.TB025_id ");
                sSQL.Append("  , dbo.TB025_Pagamentos.TB025_DataLiquidacao ");
                sSQL.Append("  , dbo.TB025_Pagamentos.TB025_Vencimento ");
                sSQL.Append("  , dbo.TB025_Pagamentos.TB025_NossoNumero ");
                sSQL.Append("  , dbo.TB025_Pagamentos.TB025_ValorTitulo ");
                sSQL.Append("  , dbo.TB016_Parcela.TB016_id ");
                sSQL.Append("  , dbo.TB025_Pagamentos.TB025_CPFCNPJ ");
                sSQL.Append("  , dbo.TB016_Parcela.TB012_id ");
                sSQL.Append("  , dbo.TB016_Parcela.TB016_CPFCNPJ ");
                sSQL.Append("  , dbo.TB016_Parcela.TB016_Pagador ");
                sSQL.Append("  , dbo.TB016_Parcela.TB016_Status ");
                sSQL.Append("  , dbo.TB025_Pagamentos.TB025_DataMovimentacao AS Inicio ");
                sSQL.Append("  , dbo.TB025_Pagamentos.TB025_DataMovimentacao AS Fim ");
                sSQL.Append("  FROM             ");
                sSQL.Append("  dbo.TB025_Pagamentos ");
                sSQL.Append("  LEFT OUTER JOIN ");
                sSQL.Append("  dbo.TB016_Parcela  ");
                sSQL.Append("  ON  ");
                sSQL.Append("  dbo.TB025_Pagamentos.TB025_NossoNumero = dbo.TB016_Parcela.TB016_NossoNumero ");
                sSQL.Append("  WHERE ");
                sSQL.Append("  dbo.TB025_Pagamentos.TB025_DataMovimentacao >=  ");
                sSQL.Append("'");
                sSQL.Append(Inicio.ToString("MM/dd/yyyy"));
                sSQL.Append("'");
                sSQL.Append("  AND ");
                sSQL.Append("  dbo.TB025_Pagamentos.TB025_DataMovimentacao <= ");
                sSQL.Append("'");
                sSQL.Append(fim.ToString("MM/dd/yyyy"));
                sSQL.Append("'");
                sSQL.Append("  AND ");
                sSQL.Append("  dbo.TB016_Parcela.TB012_id IS NULL ");
                sSQL.Append("  ORDER BY  ");
                sSQL.Append("  dbo.TB016_Parcela.TB016_id ");
                sSQL.Append("  , dbo.TB025_Pagamentos.TB025_NossoNumero ");

                SqlCommand command = new SqlCommand(sSQL.ToString(), con);
                command.CommandTimeout = 300;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PagamentosController obj = new PagamentosController();
                    obj.TB025_id                = Convert.ToInt64(reader["TB025_id"]);
                    obj.TB025_ValorTitulo       = Convert.ToDouble(reader["TB025_ValorTitulo"]);
                    obj.TB025_DataLiquidacao    = Convert.ToDateTime(reader["TB025_DataLiquidacao"]);
                    obj.TB025_Vencimento        = Convert.ToDateTime(reader["TB025_Vencimento"]);
                    obj.TB025_NossoNumero       = Convert.ToInt64(reader["TB025_NossoNumero"]);
                    obj.TB025_CPFCNPJ           = reader["TB025_CPFCNPJ"].ToString();

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

    }
}
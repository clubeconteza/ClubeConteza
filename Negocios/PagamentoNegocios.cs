
using Controller;
using DAO;
using System;
using System.Collections.Generic;

namespace Negocios
{
    public class PagamentoNegocios
    {
        public bool PagamentoIncluirImporcacao(List<PagamentosController> Pagamentos_L)
        {

            try
            {
                PagamentoDAO Dao = new PagamentoDAO();
                LogNegocios Log_N = new LogNegocios();

                foreach (PagamentosController Pagamento in Pagamentos_L)
                {

                    /*Verificar se a Parcela ja não foi importada*/
                    if (1007021 == Pagamento.TB025_NossoNumero)
                    {
                        var temp2 = Pagamento.TB025_NossoNumero;
                    }

                    Int64 Validacao = Dao.ValidaParcelaJaIncluida(Pagamento.TB025_BancoOrigem, Pagamento.TB025_DocumentoBanco);
                    if (Validacao == 0)
                    {
                        Int64 vTB025_id = Dao.PagamentoIncluir(Pagamento);
                        if (vTB025_id > 0)
                        {
                            LogController Log_C = new LogController();
                            Log_C.TB025_Id = vTB025_id;
                            Log_C.TB011_Id = Pagamento.TB025_AlteradoPor;
                            Log_C.TB000_IdTabela = 25;
                            Log_C.TB000_Tabela = "Pagamentos";
                            Log_C.TB000_Data = DateTime.Now;
                            Log_C.TB000_Descricao = string.Format(MensagensLog.L0016.ToString(), Pagamento.TB025_CPFCNPJ.Trim(), Pagamento.TB025_ValorCobrado, Pagamento.TB025_Vencimento.ToString("dd/MM/yyyy"));
                            Log_N.LogInsert(Log_C);
                        }
                    }

                }

                /*Atualizar Parcelas, Contratos e Gerar Cartao*/
                if (Dao.SP_U_TB013_GerarCartaoMassa())
                {
                    LogController Log_C = new LogController();

                    Log_C.TB012_Id = 0;
                    Log_C.TB011_Id = Pagamentos_L[0].TB025_AlteradoPor;
                    Log_C.TB000_IdTabela = 25;
                    Log_C.TB000_Tabela = "Pagamentos";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = MensagensLog.L0017.ToString();
                    Log_N.LogInsert(Log_C);
                }

                new ParcelaDao().SetarParcelaVencida(Pagamentos_L[0].TB025_AlteradoPor);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<PagamentosController> PagamentosMesAno(int Mes, int Ano)
        {
            try
            {
                PagamentoDAO DAO = new PagamentoDAO();
                return DAO.PagamentosMesAno(Mes, Ano);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public bool SP_U_TB016_Vincula_TB025(Int64 TB016_id, Int64 TB012_id, Int64 TB025_id)
        //{
        //    try
        //    {
        //        PagamentoDAO DAO = new PagamentoDAO();
        //        return DAO.SP_U_TB016_Vincula_TB025(TB016_id, TB012_id, TB025_id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public long PagamentoIncluir(PagamentosController Pagamento, Int64 TB013_AlteradoPor,Int64 TB012_Id)
        //{
        //    try
        //    {
        //        PagamentoDAO DAO = new PagamentoDAO();

        //        Int64 Retorno = DAO.PagamentoIncluir(Pagamento);

        //        if(Retorno>0)
        //        {
        //            LogNegocios Log_N = new LogNegocios();
        //            LogController Log_C = new LogController();
        //            Log_C.TB012_Id = TB012_Id;
        //            Log_C.TB011_Id = TB013_AlteradoPor;
        //            Log_C.TB000_IdTabela = 25;
        //            Log_C.TB000_Tabela = "Pagamento";
        //            Log_C.TB000_Data = DateTime.Now;
        //            Log_C.TB000_Descricao = string.Format(MensagensLog.L0028.ToString(), Retorno);
        //            Log_N.LogInsert(Log_C);
        //        }

        //        return Retorno;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool SP_U_TB013_GerarCartaoMassa()
        //{
        //    try
        //    {
        //        PagamentoDAO DAO = new PagamentoDAO();
        //        return DAO.SP_U_TB013_GerarCartaoMassa();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<PagamentosController> ParcelasListarErrosSICOOB(DateTime Inicio, DateTime fim)
        {
            try
            {
                PagamentoDAO DAO = new PagamentoDAO();
                return DAO.ParcelasListarErrosSICOOB(Inicio, fim);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

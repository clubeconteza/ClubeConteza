using Controller;
using DAO;
using System;
using System.Collections.Generic;

namespace Negocios
{
    public class ContratoDocNegocios
    {
        public Int64 VerificaExistenciaDocumento(long TB012_id, int TB029_Tipo)
        {
            try
            {
                ContratoDocDAO DAO = new ContratoDocDAO();
                return  DAO.VerificaExistenciaDocumento(TB012_id, TB029_Tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ContratoDocController DocImpressaoInserir(ContratoDocController Documento)
        {
            try
            {
                ContratoDocDAO DAO = new ContratoDocDAO();
                ContratoDocController Retorno = DAO.DocImpressaoInserir(Documento);

                if(Retorno.TB029_Id>0)
                {
                    LogNegocios Log_N = new LogNegocios();
                    LogController Log_C = new LogController();

                    Log_C.TB012_Id = Documento.TB012_id;
                    Log_C.TB011_Id = Documento.TB029_DocImpressaoPor;
                    Log_C.TB000_IdTabela = 29;
                    Log_C.TB000_Tabela = "Documentos";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = MensagensLog.L0032.ToString();
                    Log_N.LogInsert(Log_C);
                }
                return Retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContratoDocController> DocContratoLista(long TB012_id)
        {
            try
            {
                ContratoDocDAO DAO = new ContratoDocDAO();
                return DAO.DocContratoLista(TB012_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ContratoDocController DocImpressaoSelect(ContratoDocController Doc)
        {
            try
            {
                ContratoDocDAO DAO = new ContratoDocDAO();
                return DAO.DocImpressaoSelect(Doc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

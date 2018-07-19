using Controller;
using DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Negocios
{
    public class MensagemNegocios
    {
        public bool smsAgendados(DateTime dataReferencia)
        {
            try
            {
                List<MensagemController> mensagens = new MensagemDAO().smsAgendados(dataReferencia);
                foreach (var sms in mensagens)
                {
                    /*Envia SMS*/

                    /*Grava retorno na tabela TB039_Mensagem*/

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }

        public List<MensagemController> smsListar(DateTime dataReferencia)
        {
            try
            {
                return  new MensagemDAO().smsListar(dataReferencia);
             
            }
            catch (Exception e)
            {
                throw e;
            }

       
        }

        public bool MensagemExcluir(long TB039_id, long TB009_id, long TB012_Id, long TB011_Id)
        {
            try
            {
                if(new MensagemDAO().MensagemExcluir(TB039_id, TB009_id))
                {
                    LogController Log_C = new LogController();

                    Log_C.TB012_Id = TB012_Id;
                    Log_C.TB011_Id = TB011_Id;
                    Log_C.TB000_IdTabela = 12;
                    Log_C.TB000_Tabela = "Contratos";
                    Log_C.TB000_Data = DateTime.Now;
                    string.Format(MensagensLog.L0084.ToString(), TB012_Id.ToString());
                    new LogNegocios().LogInsert(Log_C);
                    return true;
                }
               
            }
            catch (Exception e)
            {
                throw e;
            }

            return false;
        }

    

        public bool MensagemIncluir(List<MensagemController> mensagens)
        {
            try
            {
                return new MensagemDAO().MensagemIncluir(mensagens);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
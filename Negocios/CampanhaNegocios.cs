using Controller;
using DAO;
using System;
using System.Collections.Generic;


namespace Negocios
{
    public class CampanhaNegocios
    {
        public List<CampanhaController> Campanhas(string filtro)
        {
            try
            {

                return new CampanhaDAO().campanhas(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CampanhaController campanha(long TB041_id)
        {
            try
            {

                return new CampanhaDAO().campanha(TB041_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool campanhaUpdateCanalSms(CampanhaController sms)
        {
            try
            {
                if (new CampanhaDAO().campanhaUpdateCanalSms(sms))
                {
                    LogNegocios Log_N = new LogNegocios();
                    LogController Log_C = new LogController();

                    Log_C.TB012_Id = sms.TB012_id;
                    Log_C.TB011_Id = sms.TB041_AlteradoPor;
                    Log_C.TB000_IdTabela = 41;
                    Log_C.TB000_Tabela = "Campanha";
                    Log_C.TB000_Data = DateTime.Now;
                    Log_C.TB000_Descricao = string.Format(MensagensLog.L0082.ToString(), sms.TB041_id.ToString());
                    Log_N.LogInsert(Log_C);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
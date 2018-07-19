using Boleto.DAO;
using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boleto.Negocios
{
    public class LogNegocios
    {
        public LogController LogInsert(LogController Log)
        {
            try
            {
                LogDAO DAO = new LogDAO();
                return DAO.LogInsert(Log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LogController> LogContratoSelect(Int64 TB012_Id)
        {
            try
            {
                LogDAO DAO = new LogDAO();
                return DAO.LogContratoSelect(TB012_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
using Controller;
using DAO;
using System;
using System.Collections.Generic;

namespace Negocios
{
    public class LogNegocios
    {
        public LogController LogInsert(LogController Log)
        {
            try
            {
                LogDao DAO = new LogDao();
                return DAO.LogInsert(Log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<LogController> LogContratoSelect(Int64 TB012_Id)
        //{
        //    try
        //    {
        //        LogDao DAO = new LogDao();
        //        return DAO.LogContratoSelect(TB012_Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}

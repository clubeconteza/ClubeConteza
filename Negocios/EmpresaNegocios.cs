using Controller;
using DAO;
using System;
using System.Collections.Generic;

namespace Negocios
{
    public class EmpresaNegocios
    {
        public EmpresaController Empresa(long tb001Id)
        {
            try
            {
     
                return new EmpresaDao().Empresa(tb001Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public List<EmpresaController> Empresas()
        {
            try
            {

                return new EmpresaDao().Empresas();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
    }
}

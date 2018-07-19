using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DAO
{
    public class ValidaChave
    {
        CriptografiaDAO Cript = new CriptografiaDAO();

        public string  ValidarChave(string Chave)
        {
            string Retorno = "Erro";
            try
            {
                string[] Parametros = Cript.Decrypt(Chave).Split(';');

                Retorno = Parametros[0].ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }
    }
}

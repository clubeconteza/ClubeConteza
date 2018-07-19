
using System.Text;
using Controller;
using DAO;
using System;
using System.Collections.Generic;

namespace Negocios
{
    public class mensalidadePremiadaNegocios
    {
        public List<mensalidadePremiadaController> listarpremios(DateTime ano)
        {
            try
            {
             
                return new mensalidadePremiadaDAO().listarpremios(ano);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public mensalidadePremiadaController item(long TB042_id)
        {
            try
            {
                return new mensalidadePremiadaDAO().item(TB042_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public mensalidadePremiadaController sorteio(long numeroDaSorte, long min, long max)
        {
            try
            {
                mensalidadePremiadaController retorno = new mensalidadePremiadaController();
                int tentativa = 0;

                int par = 0;
               

                while (retorno.TB012_id == 0)
                {
                    long numero = 0;// numeroDaSorte;
                    if(par == 0)
                    {
                        numero =  numeroDaSorte;
                        par = 1;
                    }
                    else
                    { 
                        if (par==1)
                        {
                            par = 2;
                            numero = numeroDaSorte- tentativa;
                        }
                        else
                        {
                            if (par == 2)
                            {
                                par = 1;
                                numero = numeroDaSorte + tentativa;
                            }
                        }
                    }

                    retorno = new mensalidadePremiadaDAO().sorteio(numero, min, max);
                    //if(tentativa==0)
                    //{
                    //    tentativa = 1;
                    //    //numeroDaSorte
                    //}
                    //else
                    //{
                    //    if (tentativa == 1)
                    //    {
                    //        tentativa = 2;
                    //    }
                    //}
                   
                    tentativa++;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public PessoaController recuperacontemplado(long contrato)
        {
            try
            {

                return new mensalidadePremiadaDAO().recuperacontemplado(contrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool contemplar(mensalidadePremiadaController contemplado)
        {
            try
            {
                LogController Log_C = new LogController();
            
                Log_C.TB000_Descricao   = string.Format(MensagensLog.L0083.ToString(), contemplado.TB042_id);        
                Log_C.TB012_Id          = contemplado.TB012_id;
                Log_C.TB011_Id          = contemplado.TB042_AlteradoPor;
                Log_C.TB000_IdTabela    = 42;
                Log_C.TB000_Tabela      = "Mensalidade Premiada";
                Log_C.TB000_Data        = DateTime.Now;

                if(new mensalidadePremiadaDAO().contemplar(contemplado))
                {
                    new LogNegocios().LogInsert(Log_C);
                }
            return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool atualizarconsumo(string consumo)
        {
            try
            {

                return new mensalidadePremiadaDAO().atualizarconsumo(consumo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool prepararsorteio()
        {
            try
            {

                return new mensalidadePremiadaDAO().prepararsorteio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocios
{
    public class CategoriaIdadeNegocios
    {
        //public CategoriaIdadeControler DistribuicaoIsencaoDataReferencia(List<CategoriaIdadeControler> Participantes)
        //{
        //    try
        //    {
        //        CategoriaIdadeControler Retorno = new CategoriaIdadeControler();

        //        foreach (var Pessoa in Participantes)
        //        {
        //            DateTime dttFromDate = Pessoa.DataReferencia;
        //            DateTime dttToDate = Pessoa.DataNascimento;

        //            TimeSpan Idade;
        //            Idade = dttFromDate - dttToDate;

        //            int vIdade = Convert.ToInt32((Idade.Days) / 365);
        //            if (vIdade <= 6)
        //            {
        //                Retorno.Menor++;
        //            }

        //            if (vIdade >= 7 && vIdade <= 12)
        //            {
        //                Retorno.Menor++;
        //            }

        //            if (vIdade >= 13)
        //            {
        //                Retorno.Maior++;
        //            }
        //        }


        //        return Retorno;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public CategoriaIdadeControler DistribuicaoIsencaoIdade(List<CategoriaIdadeControler> Participantes)
        {
            try
            {
                CategoriaIdadeControler Retorno = new CategoriaIdadeControler();

                foreach (var Pessoa in Participantes)
                {
                    if (Pessoa.idade <= 6)
                    {
                        
                        Retorno.Isento++;
                    }

                    if (Pessoa.idade >= 7 && Pessoa.idade <= 12)
                    {
                        Retorno.Menor++;
                    }

                    if (Pessoa.idade >= 13)
                    {
                        Retorno.Maior++;
                    }
                }


                return Retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

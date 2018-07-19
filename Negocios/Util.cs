using Controller;
using System;
using System.Collections.Generic;
using System.Linq;



    
    
    namespace Negocios
{
    public class Util
    {
        public bool CPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public Int64 NumeroAleatorio()
        {
            Int64 Retorno = 0;

            Random rnd = new Random();
           



            ContratoNegocios Contrato_N = new ContratoNegocios();
            /*Consultar uma lista de Cartões*/
            List<ContratosController> CartoesEmitidos = Contrato_N.ContratoCodCartaoUtilizados();

            int Repetir = 1;
            while (Repetir > 0)
            {
                int numero = rnd.Next(117698, 999999);

                if ((from i in CartoesEmitidos where i.TB012_CodCartao == numero select i).Count() ==0)
                {
                    Repetir = 0;
                    Retorno = numero;
                }
                //else
                //{
                //    Repetir = 1;
                //}
            }

            //if ((from i in CartoesEmitidos where i.TB012_CodCartao == numero select i).Count() < 1)
            //{
            //    /*Refaz o Processo*/
            //    NumeroAleatorio();
            //}
            //else
            //{
            //    Retorno = numero.ToString();
            //}

            return Retorno;
        }

        public  bool CNPJ(string vrCNPJ)
            {

            string CNPJ = vrCNPJ.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "").Trim();
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                     CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                        int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                        int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);

                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));

                }

                //string vtemp = CNPJOk[0] && CNPJOk[1];
                return (CNPJOk[0] && CNPJOk[1]);

            }
            catch
            {
                return false;
            }

        }

        public long numeroDaSorteGerar()
        {
            Random randNum = new Random();

            string numeroDaSorte = "";
            for (int i = 0; i < 5; i++)

                numeroDaSorte = numeroDaSorte + randNum.Next(9);
            return Convert.ToInt64(numeroDaSorte);
        }

        public bool contemNumeros(string texto)
        {
            if (texto.Where(c => char.IsNumber(c)).Count() > 0)
                return true;
            else
                return false;
        }

        public bool contemLetras(string texto)
        {
            if (texto.Where(c => char.IsLetter(c)).Count() > 0)
                return true;
            else
                return false;
        }

    }
}

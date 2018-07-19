using System;
using System.Security.Cryptography;

namespace PortalClubeConteza.Utilities
{
    public class GeradorSenhaAleatoria
    {
        private static int SenhaTamanhoMinimoPadrao = 8;
        private static int SenhaTamanhoMaximoPadrao = 10;

        private static string SenhaCaracteresMinusculos = "abcdefghijklmnopqrstuvwxyz";
        private static string SenhaCaracteresMaiusculos = "ABCDEFGHIJKLMNOPQRSTUvWXYZ";
        private static string SenhaCaracteresNumericos = "0123456789";
        private static string SenhaCaracteresEspeciais = "*$&!%@#";

        public static string GerarSenha()
        {
            return GerarSenha(SenhaTamanhoMinimoPadrao, SenhaTamanhoMaximoPadrao);
        }

        public static string GerarSenha(int tamanho)
        {
            return GerarSenha(tamanho, tamanho);
        }

        public static string GerarSenha(int tamanhoMin, int tamanhoMax)
        {
            if (tamanhoMin <= 0 || tamanhoMax <= 0 || tamanhoMin > tamanhoMax)
                return null;

            var grupoCaracteres = new char[][]
            {
                SenhaCaracteresMinusculos.ToCharArray(),
                SenhaCaracteresMaiusculos.ToCharArray(),
                SenhaCaracteresNumericos.ToCharArray(),
                SenhaCaracteresEspeciais.ToCharArray()
            };

            var caracteresExcluidosGrupo = new int[grupoCaracteres.Length];

            for (int i = 0; i < caracteresExcluidosGrupo.Length; i++)
            {
                caracteresExcluidosGrupo[i] = grupoCaracteres[i].Length;
            }

            var ordenaGruposExcluidos = new int[grupoCaracteres.Length];

            for (int i = 0; i < ordenaGruposExcluidos.Length; i++)
            {
                ordenaGruposExcluidos[i] = i;
            }

            var bytesAleatorios = new byte[4];

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytesAleatorios);

            var converter = BitConverter.ToInt32(bytesAleatorios, 0);

            var random = new Random(converter);

            char[] senha = null;

            if (tamanhoMin < tamanhoMax)
            {
                senha = new char[random.Next(tamanhoMin, tamanhoMax + 1)];
            }
            else
            {
                senha = new char[tamanhoMin];
            }

            int proximosCaracteres;
            int proximosGrupos;
            int proximosOrdenaGruposExcluidos;
            int ultimosCaracteres;
            int ultimosOrdenaGruposExcluidos = ordenaGruposExcluidos.Length - 1;

            for (int i = 0; i < senha.Length; i++)
            {
                if (ultimosOrdenaGruposExcluidos == 0)
                {
                    proximosOrdenaGruposExcluidos = 0;
                }
                else
                {
                    proximosOrdenaGruposExcluidos = random.Next(0, ultimosOrdenaGruposExcluidos);
                }

                proximosGrupos = ordenaGruposExcluidos[proximosOrdenaGruposExcluidos];
                ultimosCaracteres = caracteresExcluidosGrupo[proximosGrupos] - 1;

                if (ultimosCaracteres == 0)
                {
                    proximosCaracteres = 0;
                }
                else
                {
                    proximosCaracteres = random.Next(0, ultimosCaracteres + 1);
                }

                senha[i] = grupoCaracteres[proximosGrupos][proximosCaracteres];

                if (ultimosCaracteres == 0)
                {
                    caracteresExcluidosGrupo[proximosGrupos] = grupoCaracteres[proximosGrupos].Length;
                }
                else
                {
                    if (ultimosCaracteres != proximosCaracteres)
                    {
                        var temp = grupoCaracteres[proximosGrupos][ultimosCaracteres];
                        grupoCaracteres[proximosGrupos][ultimosCaracteres] = grupoCaracteres[proximosGrupos][proximosCaracteres];
                        grupoCaracteres[proximosGrupos][proximosCaracteres] = temp;
                    }
                    caracteresExcluidosGrupo[proximosGrupos]--;
                }

                if (ultimosOrdenaGruposExcluidos == 0)
                {
                    ultimosOrdenaGruposExcluidos = ordenaGruposExcluidos.Length - 1;
                }
                else
                {
                    if (ultimosOrdenaGruposExcluidos != proximosOrdenaGruposExcluidos)
                    {
                        var temp = ordenaGruposExcluidos[ultimosOrdenaGruposExcluidos];
                        ordenaGruposExcluidos[ultimosOrdenaGruposExcluidos] = ordenaGruposExcluidos[proximosOrdenaGruposExcluidos];
                        ordenaGruposExcluidos[proximosOrdenaGruposExcluidos] = temp;
                    }
                    ultimosOrdenaGruposExcluidos--;
                }
            }

            return new string(senha);
        }
    }
}
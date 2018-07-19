using Controller;
using System;

namespace ContezaAdmin
{
    public class ParametrosInterface
    {
        public static UsuarioAPPController objUsuarioLogado = new UsuarioAPPController();
        public static String ConectReport  { get; set; }
        public static String Intranet { get; set; }
        //public static string Portal;
        //public static string PathImagensParceiros;
        public static String PastaLogoUnidade   { get; set; }
        //public static String ftpUsuario     { get; set; }
        //public static String ftpSenha       { get; set; }
    }
}

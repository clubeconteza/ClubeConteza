

using System;

namespace Controller
{
    public class UsuarioPortalController
    {

        public string       TB033_CPF               { get; set; }

        public string       TB033_ChaveTemporaria   { get; set; }
        //public string       TB033_NomeCompleto      { get; set; }
        public string       TB033_Senha             { get; set; }
        public DateTime     TB033_CadastradoEm      { get; set; }
        public long         TB033_CadastradoPor     { get; set; }
        public DateTime     TB033_AlteradoEm        { get; set; }
        public long         TB033_AlteradoPor       { get; set; }
        public int          TB033_Status            { get; set; }
        /*
            0 - Inativo; 
            1 - Ativo; 
        */
        public DateTime     TB033_UltimoAcesso      { get; set; }
        //public string       TB033_ChaveValidacao    { get; set; }
        public PessoaController Pessoa { get; set; }
    }
}
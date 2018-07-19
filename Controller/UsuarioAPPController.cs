using System;
using System.Collections.Generic;

namespace Controller
{
    public class UsuarioAPPController
    {
        public string   VS                              { get; set; }
        public string   Banco                           { get; set; }
        public string   TB011_ftpServidor               { get; set; }
        public string   TB011_ftpUsuario                { get; set; }
        public string   TB011_ftpSenha                  { get; set; }
        public long     TB011_Id                        { get; set; }
        public string   TB011_CPF                       { get; set; }
        public string   TB011_NomeExibicao              { get; set; }
        public string   TB011_NomeCompleto              { get; set; }
        public string   TB011_Senha                     { get; set; }
        public string   TB011_StatusS                   { get; set; }
        public enum TB011_StatusE
        {
            Inativo     = 0,
            Ativo       = 1
        }

        public long     TB037_Id                        { get; set; }
        public int      TB037_TipoComissao              { get; set; }
        public double   TB037_Aliquota                  { get; set; }
        public double   TB037_Valor                     { get; set; }
        public string   TB010_Perfil                    { get; set; }

        public PaisController           Pais            { get; set; }
        public EstadoController         Estado          { get; set; }
        public MunicipioController      Municipio       { get; set; }
        public AcessoController         Perfil          { get; set; }
        public List<AcessoController>   Modulos         { get; set; }
        public List<AcessoController>   Privilegios     { get; set; }
    }
}

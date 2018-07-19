using System;

namespace Controller
{
    public class MensagemController
    {


        public long     TB039_id                { get; set; }
        public long     TB009_id                { get; set; }
        public string   TB039_Mensagem          { get; set; }
        public string   TB009_Contato           { get; set; }

        

        public int      TB009_TipoI             { get; set; }
        public string   TB009_TipoS             { get; set; }
        public enum TB009_TipoE
        {
            Celular     = 1,
            Fixo        = 2,
            Email       = 3,
            Nextel      = 4
        }        
        public string   TB039_SeuNumero         { get; set; }
        public string   TB039_Destino           { get; set; }
        public string   TB039_Assunto           { get; set; }
        public string   TB039_Conteudo          { get; set; }
        public DateTime TB039_DataCriacao       { get; set; }
        public DateTime TB039_Agendamento        { get; set; }
        public DateTime TB039_EviadoEm          { get; set; }
        public int      TB039_RetornoCod        { get; set; }
        public string   TB039_RetornoDef        { get; set; }
        public int      TB039_StatusI           { get; set; }
        public string   TB039_StatusS           { get; set; }
        public enum TB039_StatusE
        {
            Gravado     = 0,
            Agendado    = 1,
            Enviar      = 2,
            Enviado     = 3,
            Confirmado  = 4,
            Erro        = 5
        }

        public long     TB012_id                { get; set; }
        public int      TB012_StatusI           { get; set; }
        public string TB012_StatusS             { get; set; }
        public enum TB012_StatusE
        {
            Cadastrado      = 0,
            Ativo           = 1,
            Bloqueado       = 2,
            Inativo         = 3,
            Inadimplente    = 4,
            Cancelado       = 5,
            Negociado       = 6
        }


        public long     TB041_id { get; set; }
        public long     TB013_id                { get; set; }


        public string   TB013_NomeCompleto      { get; set; }

        public ContratosController Contrato     { get; set; }
    }
}

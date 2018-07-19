using System;


namespace Controller
{
    public class CampanhaController
    {
        public long         TB041_id                { get; set; }
        public long         TB012_id                { get; set; }
        public string       TB041_Campanha          { get; set; }
        public DateTime     TB041_Inicio            { get; set; }
        public DateTime     TB041_Fim               { get; set; }
        public DateTime     TB041_CadastradoEm      { get; set; }
        public long         TB041_CadastradoPor     { get; set; }
        public DateTime     TB041_AlteradoEm        { get; set; }
        public long         TB041_AlteradoPor       { get; set; }
        public string       AlteradoPor             { get; set; }
        public int          TB041_Sms               { get; set; }
        public string       TB041_SmsAssunto        { get; set; }
        public string       TB041_SmsConteudo       { get; set; }       
        public DateTime     TB041_SmsAgendamento    { get; set; }
        public string       TB041_StatusS           { get; set; }
        public enum TB041_StatusE
        {
            Cadastradp  = 0,
            Ativo       = 1,
            Cancelado   = 2
        }

    }
}

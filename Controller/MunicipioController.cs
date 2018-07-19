namespace Controller
{
    public class MunicipioController
    {
        public long     TB006_id        { get; set; }   
        public string   TB006_Municipio { get; set; }
        public string   TB006_Capital   { get; set; }
        public string   TB006_Codigo    { get; set; }
        public string   TB006_IBGE      { get; set; }
        public long     TB006_Lote      { get; set; }   

        public EstadoController Estado { get; set; }
    }
}

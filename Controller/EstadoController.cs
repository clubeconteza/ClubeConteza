namespace Controller
{
    public class EstadoController
    {
        public long     TB005_Id        { get; set; }
        public string   TB005_Sigla     { get; set; }
        public string   TB005_Estado    { get; set; }
        public string   TB005_Codigo    { get; set; }

        public PaisController Pais      { get; set; }       
    }
}

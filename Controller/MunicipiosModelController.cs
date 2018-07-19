namespace Controller
{
    public class MunicipiosModelController
    {
        public long Id { get; set; }
        public long? IdEstado { get; set; }
        public string Municipio { get; set; }
        public int Capital { get; set; }
        public string Codigo { get; set; }
        public string Ibge { get; set; }
        public float Lote { get; set; }
    }
}

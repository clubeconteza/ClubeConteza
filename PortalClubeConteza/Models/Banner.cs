namespace PortalClubeConteza.Models
{
    public class Banner
    {
        public long Id { get; set; }
        public string UrlImagem { get; set; }
        public string NomeArquivo { get; set; }
        public string Titulo { get; set; }
        public string Link { get; set; }
        public string Tooltip { get; set; }
        public int? LinkFormaAberturaLink { get; set; }
    }
}
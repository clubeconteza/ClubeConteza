using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalClubeConteza.Entities
{
    [Table("TB019_Banner")]
    public class Banner
    {
        [Key]
        [Column("TB019_Id")]
        public long Id { get; set; }

        [Column("TB006_id")]
        public long? IdMunicipio { get; set; }

        [Column("TB012_id")]
        public long? IdContratos { get; set; }

        [Column("TB019_Sessao")]
        public int? Sessao { get; set; }

        [Column("TB019_URLImagem")]
        public string UrlImagem { get; set; }

        [Column("TB019_NomeArquivo")]
        public string NomeArquivo { get; set; }

        [Column("TB019_Titulo")]
        public string Titulo { get; set; }

        [Column("TB019_Link")]
        public string Link { get; set; }

        [Column("TB019_LinkFormaAberturaLink")]
        public int? LinkFormaAberturaLink { get; set; }

        [Column("TB019_ToolTip")]
        public string ToolTip { get; set; }

        [Column("TB019_Status")]
        public int? Status { get; set; }

        [Column("TB019_Sequencia")]
        public int? Sequencia { get; set; }
    }
}
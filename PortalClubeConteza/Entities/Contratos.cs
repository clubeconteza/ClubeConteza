using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalClubeConteza.Entities
{
    [Table("TB012_Contratos")]
    public class Contratos
    {
        [Key]
        [Column("TB012_id")]
        public long Id { get; set; }

        [Column("TB012_TipoContrato")]
        public int? TipoContrato { get; set; }

        [Column("TB013_id")]
        public long? IdPessoa { get; set; }

        [Column("TB006_id")]
        public long? IdMunicipio { get; set; }

        [Column("TB012_Status")]
        public int? Status { get; set; }
    }
}
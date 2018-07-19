using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalClubeConteza.Entities
{
    [Table("TB006_Municipio")]
    public class Municipio
    {
        [Key]
        [Column("TB006_id")]
        public long Id { get; set; }

        [Column("TB005_Id")]
        public long? IdEstado { get; set; }

        [Column("TB006_Municipio")]
        public string Municipios { get; set; }
    }
}
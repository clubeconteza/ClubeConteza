using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalClubeConteza.Entities
{
    [Table("TB005_Estado")]
    public class Estado
    {
        [Key]
        [Column("TB005_Id")]
        public long Id { get; set; }

        [Column("TB005_Sigla")]
        public string Sigla { get; set; }
    }
}
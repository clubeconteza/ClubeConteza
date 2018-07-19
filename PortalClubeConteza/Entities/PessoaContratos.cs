using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalClubeConteza.Entities
{
    [Table("TB040_TB013_TB012")]
    public class PessoaContratos
    {
        [Key]
        [Column("TB040_id")]
        public long Id { get; set; }

        [Column("TB013_id")]
        public long IdPessoa { get; set; }

        [Column("TB012_id")]
        public long IdContratos { get; set; }
    }
}
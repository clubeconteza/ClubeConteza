using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalClubeConteza.Entities
{
    [Table("TB009_Contato")]
    public class Contato
    {
        [Key]
        [Column("TB009_id")]
        public long Id { get; set; }

        [Column("TB013_id")]
        public long? IdPessoa { get; set; }

        [Column("TB009_Tipo")]
        public int? Tipo { get; set; }

        [Column("TB009_Contato")]
        public string Contatos { get; set; }
    }
}
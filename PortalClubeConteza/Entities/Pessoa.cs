using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalClubeConteza.Entities
{
    [Table("TB013_Pessoa")]
    public class Pessoa
    {
        [Key]
        [Column("TB013_id")]
        public long Id { get; set; }

        [Column("TB012_id")]
        public long? IdContratos { get; set; }

        [Column("TB013_CPFCNPJ")]
        public string CpfCnpj { get; set; }

        [Column("TB013_NomeCompleto")]
        public string NomeCompleto { get; set; }

        [Column("TB013_DataNascimento")]
        public DateTime? DataNascimento { get; set; }

        [Column("TB006_id")]
        public long? IdMunicipio { get; set; }

        [Column("TB013_Status")]
        public long? Status { get; set; }

        [Column("TB012_Corporativo")]
        public long? Corporativo { get; set; }
    }
}
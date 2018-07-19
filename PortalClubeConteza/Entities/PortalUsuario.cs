using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalClubeConteza.Entities
{
    [Table("TB033_PortalUsuario")]
    public class PortalUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("TB013_id")]
        public long IdPessoa { get; set; }

        [Column("TB033_Senha")]
        public string Senha { get; set; }

        [Column("TB033_CadastradoEm")]
        public DateTime? CadastradoEm { get; set; }

        [Column("TB033_CadastradoPor")]
        public long? CadastradoPor { get; set; }

        [Column("TB033_AlteradoEm")]
        public DateTime? AlteradoEm { get; set; }

        [Column("TB033_AlteradoPor")]
        public long? AlteradoPor { get; set; }

        [Column("TB033_Status")]
        public int? Status { get; set; }

        [Column("TB033_UltimoAcesso")]
        public DateTime? UltimoAcesso { get; set; }

        [Column("TB033_ChaveTemporaria")]
        public string ChaveTemporaria { get; set; }
    }
}
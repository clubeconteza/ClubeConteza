using PortalClubeConteza.Entities;
using System.Configuration;
using System.Data.Entity;

namespace PortalClubeConteza.DAO
{
    public class EntidadesContext : DbContext
    {
        public EntidadesContext() 
            : base(new CriptografiaDAO().Decrypt(ConfigurationManager.ConnectionStrings["EntidadesContext"].ConnectionString))
        {
        }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Contratos> Contratos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<PessoaContratos> PessoaContratos { get; set; }
        public DbSet<PortalUsuario> PortalUsuarios { get; set; }
    }
}
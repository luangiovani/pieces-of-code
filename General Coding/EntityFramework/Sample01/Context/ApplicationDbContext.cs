using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using TAJ.Database.Entity;
using TAJ.Database.EntityConfig;

namespace TAJ.Database.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ConnString", throwIfV1Schema: false)
        {
        }

        public IDbSet<Area> Area { get; set; }
        public IDbSet<Bairro> Bairro { get; set; }
        public IDbSet<Cidade> Cidade { get; set; }
        public IDbSet<Cliente> Cliente { get; set; }
        public IDbSet<Estado> Estado { get; set; }
        public IDbSet<Filial> Filial { get; set; }
        public IDbSet<Foto> Foto { get; set; }
        public IDbSet<Galeria> Galeria { get; set; }
        public IDbSet<Novidade> Novidade { get; set; }
        public IDbSet<Pais> Pais { get; set; }
        public IDbSet<Perfil_Area> Perfil_Area { get; set; }
        public IDbSet<Playlist> Playlist { get; set; }
        public IDbSet<Programacao> Programacao { get; set; }
        public IDbSet<Promocao> Promocao { get; set; }
        public IDbSet<Pharmacy> Pharmacy { get; set; }
        public IDbSet<Usuario_Filial> Usuario_Filial { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // General Custom Context Properties
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(128));

            // ModelConfiguration
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new ApplicationRoleConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserRoleConfiguration());
            modelBuilder.Configurations.Add(new AreaConfiguration());
            modelBuilder.Configurations.Add(new BairroConfiguration());
            modelBuilder.Configurations.Add(new CidadeConfiguration());
            modelBuilder.Configurations.Add(new ClienteConfiguration());
            modelBuilder.Configurations.Add(new EstadoConfiguration());
            modelBuilder.Configurations.Add(new FilialConfiguration());
            modelBuilder.Configurations.Add(new FotoConfiguration());
            modelBuilder.Configurations.Add(new GaleriaConfiguration());
            modelBuilder.Configurations.Add(new NovidadeConfiguration());
            modelBuilder.Configurations.Add(new PaisConfiguration());
            modelBuilder.Configurations.Add(new PerfilAreaConfiguration());
            modelBuilder.Configurations.Add(new PlaylistConfiguration());
            modelBuilder.Configurations.Add(new ProgramacaoConfiguration());
            modelBuilder.Configurations.Add(new PromocaoConfiguration());
            modelBuilder.Configurations.Add(new PharmacyConfiguration());
            modelBuilder.Configurations.Add(new UsuarioFilialConfiguration());

            base.OnModelCreating(modelBuilder);

            // ModelConfiguration
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuario");
            modelBuilder.Entity<IdentityRole>().ToTable("Perfil");
            modelBuilder.Entity<IdentityUserRole>().ToTable("Usuario_Perfil");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("Usuario_Login");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("Usuario_Claim");
        }

       

        
    }
}
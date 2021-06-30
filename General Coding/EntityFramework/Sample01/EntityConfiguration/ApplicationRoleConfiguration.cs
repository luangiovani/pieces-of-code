using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class ApplicationRoleConfiguration : EntityTypeConfiguration<ApplicationRole>
    {
        public ApplicationRoleConfiguration()
        {
            ToTable("Perfil");

            Property(o => o.Id)
                .HasMaxLength(128)
                .HasColumnName("id_perfil");

            Property(o => o.Name)
                .HasMaxLength(256)
                .HasColumnName("nome");
        } 
    }
}
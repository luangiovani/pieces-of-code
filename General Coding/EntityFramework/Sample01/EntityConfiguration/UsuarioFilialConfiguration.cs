using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class UsuarioFilialConfiguration : EntityTypeConfiguration<Usuario_Filial>
    {
        public UsuarioFilialConfiguration()
        {
            HasKey(o => new {o.id_filial, o.id_usuario});

            HasRequired(o => o.filial)
                .WithMany(o => o.usuario_filial)
                .HasForeignKey(o => o.id_filial);

            HasRequired(o => o.usuario)
               .WithMany(o => o.usuario_filial)
               .HasForeignKey(o => o.id_usuario);

            Property(o => o.dt_cadastro)
                .IsRequired();
        }
    }
}
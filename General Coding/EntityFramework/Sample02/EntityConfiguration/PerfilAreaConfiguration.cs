using System.Data.Entity.ModelConfiguration;
using Framework.Database.Entity;

namespace Framework.Database.EntityConfig
{
    public class PerfilAreaConfiguration : EntityTypeConfiguration<Perfil_Area>
    {
        public PerfilAreaConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(o => new {o.id_perfil, o.id_area});

            HasRequired(o => o.perfil)
                .WithMany(o => o.perfil_area)
                .HasForeignKey(o => o.id_perfil);

            HasRequired(o => o.area)
                .WithMany(o => o.perfil_area)
                .HasForeignKey(o => o.id_area);

            Property(o => o.ind_visualizar)
                .IsRequired();

            Property(o => o.ind_cadastrar)
                .IsRequired();

            Property(o => o.ind_excluir)
                .IsRequired();

            Property(o => o.dt_cadastro)
               .IsRequired();
        }
    }
}
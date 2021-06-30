using Framework.Database.Entity.CBL;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Framework.Database.EntityConfiguration.CBL
{
    public class CloudConfiguration : EntityTypeConfiguration<Cloud>
    {
        public CloudConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(c => c.idCloud)
                .Property((ac => ac.idCloud)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(c => c.serviceOrderClouds)
                                         .WithRequired(ci => ci.clouds)
                                         .HasForeignKey(ci => ci.idCloud);

            HasMany(c => c.vencimentosClouds)
                                    .WithRequired(ci => ci.cloud)
                                    .HasForeignKey(ci => ci.idCloud);

        }
    }
}

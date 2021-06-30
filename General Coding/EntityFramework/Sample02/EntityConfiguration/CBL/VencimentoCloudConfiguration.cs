using Framework.Database.Entity.CBL;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Framework.Database.EntityConfiguration.CBL
{
    public class VencimentoCloudConfiguration : EntityTypeConfiguration<VencimentosCloud>
    {
        public VencimentoCloudConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(c => c.idVencimentosCloud)
                .Property((ac => ac.idVencimentosCloud)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(c => c.cloud)
                .WithMany(s => s.vencimentosClouds)
                .HasForeignKey(c => c.idCloud);
        }
    }
}

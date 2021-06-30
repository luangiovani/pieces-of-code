using Framework.Database.Entity.CBL;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Framework.Database.EntityConfiguration.CBL
{
    public class ServiceOrderCloudConfiguration : EntityTypeConfiguration<ServiceOrderCloud>
    {
        public ServiceOrderCloudConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(c => c.idServiceOrderCloud)
                .Property((ac => ac.idServiceOrderCloud)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(c => c.locaisMicrocomputador)
                            .WithMany(s => s.serviceOrderClouds)
                            .HasForeignKey(c => c.idLocalMicrocomputador);

            HasRequired(c => c.clouds)
                            .WithMany(s => s.serviceOrderClouds)
                            .HasForeignKey(c => c.idCloud);

            HasRequired(c => c.vencimentoCloud)
                            .WithMany(s => s.serviceOrderClouds)
                            .HasForeignKey(c => c.idVencimentoCloud);

            Property(s => s.serviceOrder_id).IsRequired();

            Property(s => s.serviceOrder_id).HasPrecision(18, 0);

            Property(s => s.informacoesAdicionais).HasMaxLength(4000);

        }
    }
}

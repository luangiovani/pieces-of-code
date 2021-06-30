using Framework.Database.Entity.CBL;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Framework.Database.EntityConfiguration.CBL
{
    public class LocalMicrocomputadorConfiguration : EntityTypeConfiguration<LocalMicrocomputador>
    {
        public LocalMicrocomputadorConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(c => c.idLocalMicrocomputador)
                .Property((ac => ac.idLocalMicrocomputador)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(c => c.serviceOrderClouds)
                                   .WithRequired(ci => ci.locaisMicrocomputador)
                                   .HasForeignKey(ci => ci.idLocalMicrocomputador);

        }
    }
}

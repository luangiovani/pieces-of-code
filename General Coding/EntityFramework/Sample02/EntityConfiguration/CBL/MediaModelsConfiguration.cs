using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.EntityConfiguration.CBL
{
    public class MediaModelsConfiguration : EntityTypeConfiguration<MediaModels>
    {
        public MediaModelsConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(m => m.mediaModels_id)
                .Property((m => m.mediaModels_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(m => m.model).HasMaxLength(500);

            Property(m => m.compatibility).HasMaxLength(500);

            Property(dt => dt.dateRegistration).IsRequired();

            Property(dt => dt.userRegistration_id).IsRequired();

            HasRequired(m => m.manufacturer)
                .WithMany(m => m.mediaModels)
                .HasForeignKey(m => m.manufacturer_id);
        }
    }
}

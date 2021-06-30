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
    public class ServiceOrderMediasConfiguration : EntityTypeConfiguration<ServiceOrderMedias>
    {
        public ServiceOrderMediasConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(s => s.serviceOrderMedias_id)
                .Property((ac => ac.serviceOrderMedias_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(s => s.serviceOrder)
                .WithMany(so => so.serviceOrderMedias)
                .HasForeignKey(s => s.serviceOrder_id);

            HasRequired(s => s.media)
                .WithMany(m => m.serviceOrderMedias)
                .HasForeignKey(s => s.media_id);

            Property(s => s.dateRegistration).IsRequired();
            Property(s => s.userRegistration_id).IsRequired();
        }
    }
}

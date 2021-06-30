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
    public class ServiceOrderStatusConfiguration : EntityTypeConfiguration<ServiceOrderStatus>
    {
        public ServiceOrderStatusConfiguration()
        {
            HasKey(ss => ss.serviceOrderStatus_id)
                .Property(ss => ss.serviceOrderStatus_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(o => o.serviceOrderStatusParent)
                .WithMany(o => o.serviceOrderStatusDependents)
                .HasForeignKey(o => o.serviceOrderStatusParent_id);

            ///Tamanho máximo de 100 caracteres para nome de Hear Of Us
            Property(ss => ss.name).HasMaxLength(100);

            Property(ss => ss.nameToClient).HasMaxLength(100);

            ///Tamanho máximo de 500 caracteres para de Hear Of Us
            Property(ss => ss.description).HasMaxLength(500);

            Property(ss => ss.order).IsRequired();

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property(ss => ss.active).IsRequired();

            Property(ss => ss.dateRegistration).IsRequired();

            Property(ss => ss.userRegistration_id).IsRequired();
        }
    }
}

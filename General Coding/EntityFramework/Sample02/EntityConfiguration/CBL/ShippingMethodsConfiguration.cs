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
    public class ShippingMethodsConfiguration : EntityTypeConfiguration<ShippingMethods>
    {
        public ShippingMethodsConfiguration()
        {
            HasKey(sm => sm.shippingMethod_id)
                .Property(ss => ss.shippingMethod_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ///Tamanho máximo de 100 caracteres para nome de Hear Of Us
            Property(sm => sm.name).HasMaxLength(100);

            ///Tamanho máximo de 500 caracteres para de Hear Of Us
            Property(sm => sm.description).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property(sm => sm.active).IsRequired();

            Property(ss => ss.dateRegistration).IsRequired();

            Property(ss => ss.userRegistration_id).IsRequired();
        }
    }
}

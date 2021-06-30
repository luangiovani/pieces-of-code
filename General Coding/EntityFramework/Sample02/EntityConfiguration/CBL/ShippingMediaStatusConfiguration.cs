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
    public class ShippingMediaStatusConfiguration : EntityTypeConfiguration<ShippingMediaStatus>
    {
        public ShippingMediaStatusConfiguration()
        {
            HasKey(ss => ss.shippingMediaStatus_id)
                .Property(ss => ss.shippingMediaStatus_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(ss => ss.name).HasMaxLength(100);

            Property(ss => ss.description).HasMaxLength(500);

            //Property(ss => ss.active).IsRequired();

            Property(ss => ss.dateRegistration).IsRequired();

            Property(ss => ss.userRegistration_id).IsRequired();
        }
    }
}

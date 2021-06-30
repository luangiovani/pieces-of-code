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
    public class CustomerContactConfiguration : EntityTypeConfiguration<CustomerContact>
    {
        public CustomerContactConfiguration()
        {
            HasKey(c => c.customerContact_id)
               .Property((ac => ac.customerContact_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(c => c.customer)
                .WithMany(c => c.contacts)
                .HasForeignKey(c => c.customer_id);

            HasRequired(c => c.contact)
                .WithMany(c => c.customerContacts)
                .HasForeignKey(c => c.contact_id);

            Property(c => c.dateRegistration).IsRequired();

            Property(c => c.userRegistration_id).IsRequired();
        }
    }
}

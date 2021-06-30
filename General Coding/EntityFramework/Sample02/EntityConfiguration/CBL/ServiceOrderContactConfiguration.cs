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
    public class ServiceOrderContactConfiguration : EntityTypeConfiguration<ServiceOrderContact>
    {
        public ServiceOrderContactConfiguration()
        {
            HasKey(s => s.serviceOrderContact_id);
                Property(s => s.serviceOrderContact_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(s => s.serviceOrder)
                .WithMany(s => s.contacts)
                .HasForeignKey(s => s.serviceOrder_id);

            HasRequired(s => s.contact)
                .WithMany(c => c.serviceOrderContacts)
                .HasForeignKey(s => s.contact_id);

            Property(s => s.dateRegistration).IsRequired();

            Property(s => s.userRegistration_id).IsRequired();
                
        }
    }
}

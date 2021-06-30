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
    public class SuppliersContactConfiguration : EntityTypeConfiguration<SuppliersContact>
    {
        public SuppliersContactConfiguration()
        {
            HasKey(s => s.suppliersContact_id)
                .Property(ss => ss.suppliersContact_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(s => s.contact)
                .WithMany(c => c.suppliersContacts)
                .HasForeignKey(s => s.contact_id);

            HasRequired(s => s.supplier)
                .WithMany(s => s.contacts)
                .HasForeignKey(s => s.supplier_id);

            Property(ss => ss.dateRegistration).IsRequired();

            Property(ss => ss.userRegistration_id).IsRequired();
        }
    }
}

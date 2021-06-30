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
    public class ServiceOrderNotesConfiguration : EntityTypeConfiguration<ServiceOrderNotes>
    {
        public ServiceOrderNotesConfiguration()
        {
            HasKey(s => s.serviceOrderNotes_id)
                .Property(s => s.serviceOrderNotes_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(s => s.serviceOrder)
                .WithMany(s => s.serviceOrderNotes)
                .HasForeignKey(s => s.serviceOrder_id);

            HasRequired(s => s.note)
                .WithMany(n => n.serviceOrderNotes)
                .HasForeignKey(s => s.note_id);

            Property(s => s.dateRegistration).IsRequired();

            Property(s => s.userRegistration_id).IsRequired();

            Property(s => s.serviceOrderStatus).HasMaxLength(150).IsRequired();
        }
    }
}

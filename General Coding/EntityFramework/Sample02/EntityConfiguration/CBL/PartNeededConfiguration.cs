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
    public class PartNeededConfiguration : EntityTypeConfiguration<PartNeeded>
    {
        public PartNeededConfiguration()
        {
            HasKey(p => p.partNeeded_id)
                .Property((ac => ac.partNeeded_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(p => p.supplier)
                .WithMany(s => s.parts)
                .HasForeignKey(p => p.supplier_id);

            HasOptional<Media>(p => p.media)
                .WithMany(m => m.partsNeeded)
                .HasForeignKey(m => m.media_id);

            HasRequired(p => p.serviceOrderQuoting)
                .WithMany(m => m.partsNeeded)
                .HasForeignKey(m => m.serviceOrder_id);

            Property(dt => dt.dateRegistration).IsRequired();

            Property(dt => dt.userRegistration_id).IsRequired();

            Property(p => p.partNeeded).HasMaxLength(500);

            Property(p => p.partCost).HasPrecision(18, 2);

            Property(p => p.arriving).IsOptional();

        }
    }
}

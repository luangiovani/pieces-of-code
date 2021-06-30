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
    public class MediaConfiguration : EntityTypeConfiguration<Media>
    {
        public MediaConfiguration()
        {
            HasKey(m => m.media_id)
                .Property((ac => ac.media_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(dt => dt.dateRegistration).IsRequired();

            Property(dt => dt.userRegistration_id).IsRequired();

            HasOptional(m => m.manufacturer)
                .WithMany(m => m.medias)
                .HasForeignKey(m => m.manufacturer_id);

            HasOptional(m => m.supplier)
                .WithMany(s => s.medias)
                .HasForeignKey(m => m.supplier_id);

            HasOptional(m => m.location)
                .WithMany(l => l.medias)
                .HasForeignKey(m => m.location_id);

            HasRequired(m => m.mediaStatus)
                .WithMany(m => m.medias)
                .HasForeignKey(m => m.mediaStatus_id);

            HasMany(m => m.serviceOrderMedias)
                .WithRequired(s => s.media)
                .HasForeignKey(s => s.media_id)
                .WillCascadeOnDelete(true);

            HasMany(m => m.partsNeeded)
                .WithOptional(s => s.media)
                .HasForeignKey(s => s.media_id);

            HasMany(m => m.stockMovements)
                .WithOptional(s => s.Media)
                .HasForeignKey(s => s.media_id)
                .WillCascadeOnDelete(true);

            Property(m => m.model).HasMaxLength(200);

            Property(m => m.make).HasMaxLength(500);

            Property(m => m.serial_no).HasMaxLength(40);

            Property(m => m.part_no).HasMaxLength(40);

            Property(m => m.revision_no).HasMaxLength(40);

            Property(m => m.firmware_no).HasMaxLength(40);

            Property(m => m.size).HasMaxLength(40);

            Property(m => m.pcb_id).HasMaxLength(40);

            Property(m => m.pcb).HasMaxLength(500);

            Property(m => m.dateEntered).IsRequired();

            Property(m => m.mlc_no).HasMaxLength(40);

            Property(m => m.mfgDate).IsOptional();

            Property(m => m.oem_no).HasMaxLength(40);

            Property(m => m.upLevel_no).HasMaxLength(40);

            Property(m => m.series).HasMaxLength(500);

            Property(m => m.condition).HasMaxLength(200);

            Property(m => m.conditionInformation).HasMaxLength(500);

            Property(m => m.dcmSite_no).HasMaxLength(40);

            Property(m => m.purchaseFrom).HasMaxLength(200);

            Property(m => m.hda).HasMaxLength(200);

            //Property().IsRequired();

            Property(m => m.extensionParts).HasMaxLength(4000);

            Property(m => m.stockAddress).HasMaxLength(250).IsOptional();

            Property(m => m.madeIN).HasMaxLength(250).IsOptional();

            Property(m => m.modelInputByCustomer).HasMaxLength(500).IsOptional();

            Property(m => m.size_unit).HasMaxLength(10).IsOptional();

            
        }
    }
}

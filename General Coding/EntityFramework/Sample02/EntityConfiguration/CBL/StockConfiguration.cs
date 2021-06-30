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
    public class StockConfiguration : EntityTypeConfiguration<Stock>
    {
        public StockConfiguration()
        {
            //Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(s => s.stock_id)
                .Property((s => s.stock_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(s => s.material).HasMaxLength(2000).IsRequired();
            Property(s => s.dateOfMovement).IsRequired();
            Property(s => s.quantity).IsRequired();
            Property(s => s.typeOfMovement).IsRequired();
            Property(s => s.stockAddress).HasMaxLength(2000).IsOptional();
            Property(s => s.location_id).IsOptional();
            Property(s => s.serviceOrder_id).IsOptional();
            Property(s => s.notes).HasMaxLength(8000).IsOptional();
            Property(s => s.dateRegistration).IsRequired();
            Property(s => s.lastUpdateDate).IsRequired();
            Property(s => s.userRegistration_id).IsRequired();
            Property(s => s.lastUpdateUser_id).IsRequired();
            Property(s => s.id_old).IsOptional();

            HasOptional(s => s.Location)
                .WithMany(l => l.locationStocks)
                .HasForeignKey(s => s.location_id);

            HasOptional(s => s.Media)
                .WithMany(m => m.stockMovements)
                .HasForeignKey(s => s.media_id);

            HasOptional(s => s.Component)
                .WithMany(m => m.stockMovements)
                .HasForeignKey(s => s.component_id);

        }
    }
}

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
    public class PaymentMethodsConfiguration : EntityTypeConfiguration<PaymentMethods>
    {
        public PaymentMethodsConfiguration()
        {
            HasKey(p => p.paymentMethods_id)
                .Property((ac => ac.paymentMethods_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(s => s.serviceOrderPayments)
                .WithRequired(sm => sm.paymentMethod)
                .HasForeignKey(sm => sm.paymentMethod_id);

            Property(dt => dt.dateRegistration).IsRequired();

            Property(dt => dt.userRegistration_id).IsRequired();

            ///Tamanho máximo de 100 caracteres para nome da Marca
            Property(m => m.name).HasMaxLength(100);

            ///Tamanho máximo de 500 caracteres para a descrição da Marca
            Property(m => m.description).HasMaxLength(500);

            Property(m => m.AvailableDateFrom).IsOptional();

            Property(m => m.AvailableDateTo).IsOptional();

            Property(m => m.MinParcels).IsOptional();

            Property(m => m.MaxParcels).IsOptional();

            Property(m => m.TaxPercentage).IsOptional().HasPrecision(18, 2);

            Property(m => m.TaxMonetary).IsOptional().HasPrecision(18, 2);

            Property(m => m.AdditionalInformation).IsOptional().HasMaxLength(4000);
        }
    }
}

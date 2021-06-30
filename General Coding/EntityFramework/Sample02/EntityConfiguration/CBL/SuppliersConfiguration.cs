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
    public class SuppliersConfiguration : EntityTypeConfiguration<Suppliers>
    {
        public SuppliersConfiguration()
        {
            HasKey(s => s.supplier_id)
                .Property(ss => ss.supplier_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(s => s.city)
                .WithMany(c => c.suppliers)
                .HasForeignKey(s => s.city_id);

            HasMany(s => s.contacts)
                .WithRequired(c => c.supplier)
                .HasForeignKey(s => s.supplier_id);

            HasMany(s => s.medias)
                .WithOptional(m => m.supplier)
                .HasForeignKey(m => m.supplier_id);

            ///Tamanho máximo de 250 caracteres para o nome do cliente
            Property(s => s.name).HasMaxLength(250);

            ///Tamanho máximo de 500 caracteres para o endereço do cliente
            Property(s => s.address).HasMaxLength(500);

            ///Tamanho máximo de 20 caracteres para o cep / código / endereço postal do cliente
            Property(s => s.postalZipCode).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o cep / código / endereço postal do cliente
            Property(s => s.website).HasMaxLength(200);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property(s => s.active).IsRequired();

            Property(ss => ss.dateRegistration).IsRequired();

            Property(ss => ss.userRegistration_id).IsRequired();
        }
    }
}

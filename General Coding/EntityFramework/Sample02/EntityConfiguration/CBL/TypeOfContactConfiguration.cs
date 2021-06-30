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
    public class TypeOfContactConfiguration : EntityTypeConfiguration<TypeOfContact>
    {
        public TypeOfContactConfiguration()
        {
            HasKey(t => t.typeContact_id)
                .Property(s => s.typeContact_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(t => t.contacts)
                .WithRequired(c => c.typeOfContact)
                .HasForeignKey(c => c.typeContact_id);

            Property(t => t.name).HasMaxLength(250);

            Property(t => t.description).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            ////Property().IsRequired();
        }
    }
}

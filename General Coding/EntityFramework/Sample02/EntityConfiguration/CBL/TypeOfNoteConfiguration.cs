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
    public class TypeOfNoteConfiguration : EntityTypeConfiguration<TypeOfNote>
    {
        public TypeOfNoteConfiguration()
        {
            HasKey(t => t.typenote_id)
                .Property(s => s.typenote_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(t => t.notes)
                .WithRequired(n => n.typeOfNote)
                .HasForeignKey(n => n.typenote_id);

            Property(t => t.name).HasMaxLength(250);

            Property(t => t.description).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            ////Property().IsRequired();
        }
    }
}

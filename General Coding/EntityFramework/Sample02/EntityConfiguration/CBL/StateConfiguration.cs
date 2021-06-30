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
    public class StateConfiguration : EntityTypeConfiguration<State>
    {
        public StateConfiguration()
        {
            HasKey(s => s.state_id)
                .Property(ss => ss.state_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(s => s.country)
                .WithMany(c => c.states)
                .HasForeignKey(s => s.country_id);

            HasMany(s => s.cities)
                .WithRequired(c => c.state)
                .HasForeignKey(c => c.state_id);

            Property(s => s.name).HasMaxLength(250);

            Property(s => s.initials).HasMaxLength(6);
        }
    }
}

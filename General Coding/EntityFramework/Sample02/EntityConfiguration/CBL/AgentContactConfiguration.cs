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
    public class AgentContactConfiguration : EntityTypeConfiguration<AgentContacts>
    {
        public AgentContactConfiguration()
        {
            HasKey(ac => ac.agentContact_id);
            Property((ac => ac.agentContact_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(ac => ac.agent)
                .WithMany(a => a.contacts)
                .HasForeignKey(ac => ac.agent_id);

            HasRequired(ac => ac.contact)
                .WithMany(a => a.agentContacts)
                .HasForeignKey(ac => ac.contact_id);

            Property(ac => ac.email).HasMaxLength(250);//.IsRequired();
            Property(ac => ac.password).HasMaxLength(20);//.IsRequired();

            //Property(ac => ac.active).IsRequired();

            Property(ac => ac.dateRegistration).IsRequired();

            Property(ac => ac.userRegistration_id).IsRequired();
        }
    }
}

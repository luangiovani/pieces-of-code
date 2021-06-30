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
    /// <task_url>https://esfera.teamworkpm.net/tasks/7054873</task_url>
    /// <autor>Luan Fernandes</autor>
    /// <date>11/08/2016</date>
    /// <sumary>
    /// Configuracao da Entidade Notes(Observações), para mapeamento na tabela Notes no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class NotesConfiguration : EntityTypeConfiguration<Notes>
    {
        /// <summary>
        /// Constructor
        /// Construtor da classe, mapeando as propriedades com suas peculiaridades
        /// </summary>
        public NotesConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(n => n.note_id)
                .Property((ac => ac.note_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(n => n.typeOfNote)
                .WithMany(t => t.notes)
                .HasForeignKey(n => n.typenote_id);

            HasRequired(n => n.user)
                .WithMany(u => u.notes)
                .HasForeignKey(n => n.user_id);

            Property(n => n.description).HasMaxLength(8000);

            Property(n => n.date).IsRequired();

            //Property().IsRequired();
        }
    }
}

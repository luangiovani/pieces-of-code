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
    /// Configuracao da Entidade LabNotes (Observações do Laboratório), para mapeamento na tabela LabNotes no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class LabNotesConfiguration : EntityTypeConfiguration<LabNotes>
    {
        /// <summary>
        /// Constructor
        /// Construtor da classe, mapeando as propriedades com suas peculiaridades
        /// </summary>
        public LabNotesConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(l => l.labNote_id)
                .Property((ac => ac.labNote_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(dt => dt.dateRegistration).IsRequired();

            Property(dt => dt.userRegistration_id).IsRequired();

            HasRequired(l => l.usuario)
                .WithMany(u => u.labNotes)
                .HasForeignKey(l => l.userRegistration_id);

            ///Configuração de Relacionamento com a Tabela ServiceOrder (Ordem de Serviço)
            HasRequired(l => l.serviceOrder)
                .WithMany(s => s.labNotes)
                .HasForeignKey(l => l.serviceOrder_id);

            ///Tamanho máximo de caracteres para a nota do Lab
            Property(l => l.note).HasMaxLength(8000);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property(l => l.active).IsRequired();
        }
    }
}

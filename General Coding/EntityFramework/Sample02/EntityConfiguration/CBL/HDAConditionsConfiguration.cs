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
    /// Configuracao da Entidade Condições de HDA, para mapeamento na tabela HDAConditions no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class HDAConditionsConfiguration : EntityTypeConfiguration<HDAConditions>
    {
        /// <summary>
        /// Constructor
        /// Construtor da classe, mapeando as propriedades com suas peculiaridades
        /// </summary>
        public HDAConditionsConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(h => h.hdaCondition_id)
                .Property((ac => ac.hdaCondition_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(dt => dt.dateRegistration).IsRequired();

            Property(dt => dt.userRegistration_id).IsRequired();

            ///Tamanho máximo de 100 caracteres para nome de HDA
            Property(h => h.name).HasMaxLength(100);

            ///Tamanho máximo de 500 caracteres para de HDA
            Property(h => h.description).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property().IsRequired();
        }
    }
}

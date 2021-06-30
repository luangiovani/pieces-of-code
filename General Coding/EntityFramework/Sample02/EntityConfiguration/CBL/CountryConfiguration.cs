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
    /// Configuracao da Entidade Country (País), para mapeamento na tabela Country no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        /// <summary>
        /// Constructor
        /// Construtor da classe, mapeando as propriedades com suas peculiaridades
        /// </summary>
        public CountryConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(c => c.country_id)
                .Property((ac => ac.country_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ///Configuração de relacionamento das Cidades que contém este país
            HasMany(c => c.cities)
                .WithRequired(ci => ci.country)
                .HasForeignKey(ci => ci.country_id);

            ///Configuração de relacionamento dos Estados que contém este país
            HasMany(c => c.states)
                .WithRequired(s => s.country)
                .HasForeignKey(s => s.country_id);

            ///Tamanho máximo de 3 caracteres para a sigla do país
            Property(c => c.initials).HasMaxLength(3);

            ///Tamanho máximo de 250 caracteres para o nome do país
            Property(c => c.name).HasMaxLength(250);
        }
    }
}

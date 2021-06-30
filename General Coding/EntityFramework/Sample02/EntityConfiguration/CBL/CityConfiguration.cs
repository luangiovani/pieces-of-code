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
    /// Configuracao da Entidade Cidade, para mapeamento na tabela City no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class CityConfiguration : EntityTypeConfiguration<City>
    {
        /// <summary>
        /// Constructor
        /// Construtor da classe, mapeando as propriedades com suas peculiaridades
        /// </summary>
        public CityConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(c => c.city_id)
                .Property((ac => ac.city_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ///Configuração de relacionamento dos Agents (Parceiros CBL) que possuem endereço nesta cidade
            HasMany(c => c.agents)
                .WithRequired(a => a.city)
                .HasForeignKey(a => a.city_id);

            ///Configuração de relacionamento das Locations (Escritórios CBL) que possuem endereço nesta cidade
            HasMany(c => c.locations)
                .WithRequired(l => l.city)
                .HasForeignKey(l => l.city_id);

            ///Configuração de relacionamento dos Contacts (Contatos) que possuem endereço nesta cidade
            HasMany(c => c.contacts)
                .WithOptional(co => co.city)
                .HasForeignKey(co => co.city_id);

            HasMany(c => c.suppliers)
                .WithRequired(s => s.city)
                .HasForeignKey(s => s.city_id);

            HasMany(c => c.suppliers)
                .WithRequired(s => s.city)
                .HasForeignKey(s => s.city_id);

            HasMany(c => c.serviceOrdersBilling)
                .WithOptional(s => s.city);

            HasMany(c => c.serviceOrdersShip)
                .WithOptional(s => s.city);

            ///Configuração de relacionamento do State (Estado) desta cidade
            HasRequired(c => c.state)
                .WithMany(s => s.cities)
                .HasForeignKey(c => c.state_id);

            ///Configuração de relacionamento do Country (País) desta cidade
            HasRequired(c => c.country)
                .WithMany(co => co.cities)
                .HasForeignKey(c => c.country_id);

            ///Tamanho máximo de 500 caracteres para o nome da cidade
            Property(c => c.name).HasMaxLength(500);

        }
    }
}

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
    /// Configuracao da Entidade Agent (Parceiros da CBL), para mapeamento na tabela Agent no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class AgentConfiguration : EntityTypeConfiguration<Agent>
    {
        /// <summary>
        /// Constructor
        /// Construtor da classe, mapeando as propriedades com suas peculiaridades
        /// </summary>
        public AgentConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(o => o.agent_id)
                .Property((ac => ac.agent_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ///Configuração de Relacionamento com a Tabela Cidades
            HasRequired(a => a.city)
                .WithMany(c => c.agents)
                .HasForeignKey(a => a.city_id);

            HasMany(a => a.agentCommissions)
                .WithRequired(ac => ac.agent)
                .HasForeignKey(ac => ac.agent_id);

            HasMany(a => a.contacts)
                .WithRequired(c => c.agent)
                .HasForeignKey(c => c.agent_id);

            ///Tamanho máximo de 500 caracteres para o nome
            Property(a => a.name).HasMaxLength(500);

            ///Tamanho máximo de 800 caracteres para o nome da empresa do parceiro
            Property(a => a.companyName).HasMaxLength(800);

            ///Tamanho máximo de 400 caracteres para o endereço do parceiro
            Property(a => a.address).HasMaxLength(400);

            ///Tamanho máximo de 20 caracteres para o cep/código postal do endereço do parceiro
            Property(a => a.postalZipCode).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o website do parceiro
            Property(a => a.website).HasMaxLength(250);

            ///Tamanho máximo de 150 caracteres para o nome de usuário do parceiro
            Property(a => a.userName).HasMaxLength(150);

            ///Tamanho máximo de 10 caracteres para a senha do usuário do parceiro
            Property(a => a.password).HasMaxLength(10);

            ///Tamanho máximo de 250 caracteres para a informação encaminhado para
            Property(a => a.forwardedTo).HasMaxLength(250);

            ///Tamanho máximo de 20 caracteres para a informação número de demarcação
            Property(a => a.demarcation_no).HasMaxLength(20);

            ///Data de Cadastro é obrigatória
            Property(a => a.dateRegistration).IsRequired();

            //Usuario que cadastrou é obrigatório
            Property(a => a.userRegistration_id).IsRequired();

            ///Data de Informação do programa é opcional
            Property(a => a.dateProgramInformation).IsOptional();

            ///Tamanho máximo de 250 caracteres para o tipo de programa de relacionamento
            ///Informação da coluna name da Entidade ProgramInformationType
            Property(a => a.programInformationType).HasMaxLength(250);

            ///Tamanho máximo de 2000 caracteres para as notas do programa de relacionamento
            Property(a => a.programInformationNotes).HasMaxLength(2000);

            ///Informação se o Parceito está ativo ou não é obrigatória
            //Property(a => a.active).IsRequired();
        }
    }
}

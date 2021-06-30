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
    /// Configuracao da Entidade Contact (Contato), para mapeamento na tabela Contact no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class ContactConfiguration : EntityTypeConfiguration<Contact>
    {
        /// <summary>
        /// Constructor
        /// Construtor da classe, mapeando as propriedades com suas peculiaridades
        /// </summary>
        public ContactConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(c => c.contact_id)
                .Property((ac => ac.contact_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ///Configuração de relacionamento da Cidade dos Contatos
            HasOptional(c => c.city)
                .WithMany(ci => ci.contacts)
                .HasForeignKey(c => c.city_id);

            ///Configuração de relacionamento do Type Of Contact (Tipo de Contato) que este contato possui
            HasRequired(c => c.typeOfContact)
                .WithMany(t => t.contacts)
                .HasForeignKey(a => a.typeContact_id);

            HasMany(c => c.suppliersContacts)
                .WithRequired(s => s.contact)
                .HasForeignKey(c => c.contact_id);

            HasMany(c => c.agentContacts)
                .WithRequired(s => s.contact)
                .HasForeignKey(c => c.contact_id);

            HasMany(c => c.customerContacts)
                .WithRequired(s => s.contact)
                .HasForeignKey(c => c.contact_id);

            HasMany(c => c.serviceOrderContacts)
                .WithRequired(s => s.contact)
                .HasForeignKey(c => c.contact_id);

            ///Tamanho máximo de 200 caracteres para o nome do contato
            Property(c => c.name).HasMaxLength(200);

            ///Tamanho máximo de 20 caracteres para o Toll Free (Número de telefone gratuito 0800) do contato
            Property(c => c.tollFree).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o telefone do contato
            Property(c => c.telephone).HasMaxLength(20);

            ///Tamanho máximo de 6 caracteres para o ramal do telefone do contato
            Property(c => c.extension).HasMaxLength(6);

            ///Tamanho máximo de 20 caracteres para o número do fax do contato
            Property(c => c.fax).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o número de celular do contato
            Property(c => c.mobile).HasMaxLength(20);

            ///Tamanho máximo de 150 caracteres para o e-mail do contato
            Property(c => c.email).HasMaxLength(150);

            ///Tamanho máximo de 150 caracteres para outros do contato
            Property(c => c.other).HasMaxLength(150);

            ///Tamanho máximo de 500 caracteres para observações do contato
            Property(c => c.notes).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property().IsRequired();

            ///Indicador que o campo receiveNotifications (ativo ou não) é obrigatório
            Property(c => c.receiveNotifications).IsRequired();

            Property(ct => ct.dateRegistration).IsRequired();

            Property(ct => ct.userRegistration_id).IsRequired();


            ///Tamanho máximo de 500 caracteres para o endereço do cliente
            Property(c => c.address).HasMaxLength(500);

            Property(c => c.addressNumbers).HasMaxLength(20);

            Property(c => c.addressExtension).HasMaxLength(150);

            ///Tamanho máximo de 150 caracteres para o bairro do endereço do cliente
            Property(c => c.district).HasMaxLength(150);

            ///Tamanho máximo de 20 caracteres para o cep / código / endereço postal do cliente
            Property(c => c.postalZipCode).HasMaxLength(20);
            
            ///Tamanho máximo de 20 caracteres para o cpf/cnpj ou outro documento que venha a substituir do cliente
            Property(c => c.cpf).HasMaxLength(20);

            Property(c => c.indPrincipal).IsOptional();
        }
    }
}

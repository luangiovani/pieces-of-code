using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using Framework.Database.Entity;
using Framework.Database.EntityConfig;
using Framework.Database.Entity.CBL;
using Framework.Database.EntityConfiguration.CBL;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Database.Context
{
    /// <task_url>https://esfera.teamworkpm.net/tasks/7054873</task_url>
    /// <autor>Luan Fernandes</autor>
    /// <date>11/08/2016</date>
    /// <sumary>
    /// Configuracao da conexão com banco de dados, contexto para leitura e persistência de dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        #region Constructor

        /// <summary>
        /// Construtor padrão que cria a ponte de conexão com banco de dados
        /// </summary>
        public ApplicationDbContext()
            : base("ConnString", throwIfV1Schema: false)
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            //this.Configuration.LazyLoadingEnabled = false;
        }
        #endregion

        #region DbSet Properties

        /// <summary>
        /// Set da Entidade Area
        /// </summary>
        public IDbSet<Area> Area { get; set; }

        /// <summary>
        /// Set da Entidade Perfil_Area
        /// </summary>
        public IDbSet<Perfil_Area> Perfil_Area { get; set; }
        public IDbSet<Agent> Agent { get; set; }
        public IDbSet<AgentCommissions> AgentCommissions { get; set; }
        public IDbSet<AgentContacts> AgentContacts { get; set; }
        public IDbSet<City> City { get; set; }
        public IDbSet<Contact> Contact { get; set; }
        public IDbSet<Country> Country { get; set; }
        public IDbSet<Currency> Currency { get; set; }
        public IDbSet<Customer> Customer { get; set; }
        public IDbSet<CustomerContact> CustomerContacts { get; set; }
        public IDbSet<DocumentType> DocumentType { get; set; }
        public IDbSet<FaultFound> FaultFound { get; set; }
        public IDbSet<FileAlocationType> FileAllocationType { get; set; }
        public IDbSet<HDAConditions> HDAConditions { get; set; }
        public IDbSet<HearOfUs> HearOfUs { get; set; }
        public IDbSet<Interfaces> Interfaces { get; set; }
        public IDbSet<JobClass> JobClass { get; set; }
        public IDbSet<LabNotes> LabNotes { get; set; }
        public IDbSet<Locations> Locations { get; set; }
        public IDbSet<Make> Make { get; set; }
        public IDbSet<Manufacturer> Manufacturer { get; set; }
        public IDbSet<Media> Media { get; set; }
        public IDbSet<MediaConditions> MediaConditions { get; set; }
        public IDbSet<MediaStatus> MediaStatus { get; set; }
        public IDbSet<Notes> Notes { get; set; }
        public IDbSet<OpSystem> OpSystem { get; set; }
        public IDbSet<PackageConditions> PackageConditions { get; set; }
        public IDbSet<PartNeeded> PartNeeded { get; set; }
        public IDbSet<PaymentMethods> PaymentMethods { get; set; }
        public IDbSet<PCBConditions> PCBConditions { get; set; }
        public IDbSet<PointOfContact> PointOfContact { get; set; }
        public IDbSet<ProgramInformationType> ProgramInformationType { get; set; }
        public IDbSet<RateOurService> RateOurService { get; set; }
        public IDbSet<SendLetterOfReference> SendLetterOfReference { get; set; }
        public IDbSet<ServiceOrder> ServiceOrder { get; set; }
        public IDbSet<ServiceOrderBilling> ServiceOrderBilling { get; set; }
        public IDbSet<ServiceOrderContact> ServiceOrderContact { get; set; }
        public IDbSet<ServiceOrderEvaluation> ServiceOrderEvaluation { get; set; }
        public IDbSet<ServiceOrderInquiryFollowUp> ServiceOrderInquiryFollowUp { get; set; }
        public IDbSet<ServiceOrderMedias> ServiceOrderMedias { get; set; }
        public IDbSet<ServiceOrderNotes> ServiceOrderNotes { get; set; }
        public IDbSet<ServiceOrderQuoting> ServiceOrderQuoting { get; set; }
        public IDbSet<ServiceOrderRecoveryFollowUp> ServiceOrderRecoveryFollowUp { get; set; }
        public IDbSet<ServiceOrderShipping> ServiceOrderShipping { get; set; }
        public IDbSet<ServiceOrderStatus> ServiceOrderStatus { get; set; }
        public IDbSet<ShippingMediaStatus> ShippingMediaStatus { get; set; }
        public IDbSet<ShippingMethods> ShippingMethods { get; set; }
        public IDbSet<State> State { get; set; }
        public IDbSet<Suppliers> Suppliers { get; set; }
        public IDbSet<SuppliersContact> SuppliersContacts { get; set; }
        public IDbSet<TechTeams> TechTeams { get; set; }
        public IDbSet<TypeOfContact> TypeOfContact { get; set; }
        public IDbSet<TypeOfNote> TypeOfNote { get; set; }
        public IDbSet<TypeOfService> TypeOfService { get; set; }
        public IDbSet<TypeOfRAID> TypeRAID { get; set; }
        public IDbSet<TypeRAIDControlled> TypeRAIDControlled { get; set; }
        public IDbSet<WouldBeAReference> WouldBeAReference { get; set; }
        public IDbSet<Component> Component { get; set; }
        public IDbSet<BusinessType> BusinessType { get; set; }
        public IDbSet<Stock> Stock { get; set; }
        public IDbSet<QuoteLinesOptions> QuoteLinesOptions { get; set; }
        public IDbSet<MediaModels> MediaModels { get; set; }
        public IDbSet<RoleLocations> RoleLocations { get; set; }
        public IDbSet<LogSistema> LogSistema { get; set; }
        public IDbSet<EmailServiceOrder> EmailServiceOrder { get; set; }
        public IDbSet<AdditionalServices> AdditionalServices { get; set; }
        //public IDbSet<ServiceOrderAdditionalServices> ServiceOrderAdditionalServices { get; set; }
        public IDbSet<ServiceOrderPayments> ServiceOrderPayments { get; set; }
        public IDbSet<ServiceOrderPaymentsItems> ServiceOrderPaymentsItems { get; set; }
        public IDbSet<FollowUpMessagesBody> FollowUpMessagesBody { get; set; }
        public IDbSet<FollowUpMessagesToSend> FollowUpMessagesToSend { get; set; }
        public IDbSet<FollowUpMessagesSentedHistory> FollowUpMessagesSentedHistory { get; set; }
        public IDbSet<MadeIn> MadeIn { get; set; }
        public IDbSet<EquipmentsSell> EquipmentsSell { get; set; }
        public IDbSet<ServiceOrderSms> serviceOrderSms { get; set; }
        public IDbSet<SmsMenu> smsMenu { get; set; }
        public IDbSet<LocalMicrocomputador> LocaisMicrocomputadores {get; set;}
        //public IDbSet<Vencimento> Vencimentos { get; set; }
        public IDbSet<Cloud> Clouds { get; set; }
        public IDbSet<VencimentosCloud> VencimentosClouds { get; set; }
        public IDbSet<ServiceOrderCloud> ServiceOrderClouds { get; set; }


        #endregion

        #region ApplicationDbCobntext Create

        /// <summary>
        /// Método de Criação do Contexto com o banco de dados
        /// Caso não exista o banco de dados, cria um novo com nome, usuário e senha conforme
        /// definido no arquivo web.config na variável ConnString
        /// </summary>
        /// <returns>objeto de contexto da conexão com banco de dados</returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        #endregion

        #region Overrided Methods

        /// <summary>
        /// Método que define como ocorreá o Build do modelo, ou seja a criação das entidades em tabelas do banco de dados
        /// </summary>
        /// <param name="modelBuilder">Padrão utilizado para criação das entidades em tabelas</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Convenções
            /// Conventions
            
            /// Remove a pluralização das tabelas, ou seja, a tabela Perfil não fica Perfils no banco de dados
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            /// Remove a relação em cascata de deleção quando o relacionamento entre as tabelas seja de 1 para Muitos
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            /// Remove a relação em cascata de deleção quando o relacionamento entre as tabelas seja de Muitos para Muitos
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            #endregion

            #region Opções gerais Customizadas para o contexto

            /// Configura que todas as colunas do modelo (Entidades) do tipo string refletem ao tipo Varchar no banco de dados
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            /// Configura que, quando não explicitado no arquivo de configuração, o tamanho padrão para varchar no banco de dados é de 128 caracteres
            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(128));

            // ModelConfiguration
            /// Chamada de Configuração da tabela de usuários
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());

            /// Chamada de Configuração da tabela de Perfil
            modelBuilder.Configurations.Add(new ApplicationRoleConfiguration());

            /// Chamada de Configuração da tabela de Perfil de usuário
            modelBuilder.Configurations.Add(new ApplicationUserRoleConfiguration());
            
            /// Chamada de Configuração da tabela de áreas
            modelBuilder.Configurations.Add(new AreaConfiguration());

            /// Chamada de Configuração da tabela de relacionamento entre Perfil e Área
            modelBuilder.Configurations.Add(new PerfilAreaConfiguration());


            modelBuilder.Configurations.Add(new AgentCommissionsConfiguration());
            modelBuilder.Configurations.Add(new AgentConfiguration());
            modelBuilder.Configurations.Add(new AgentContactConfiguration());
            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new ContactConfiguration());
            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new CurrencyConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new CustomerContactConfiguration());
            modelBuilder.Configurations.Add(new DocumentTypeConfiguration());
            modelBuilder.Configurations.Add(new FaultFoundConfiguration());
            modelBuilder.Configurations.Add(new FileAlocationTypeConfiguration());
            modelBuilder.Configurations.Add(new HDAConditionsConfiguration());
            modelBuilder.Configurations.Add(new HearOfUsConfiguration());
            modelBuilder.Configurations.Add(new InterfacesConfiguration());
            modelBuilder.Configurations.Add(new JobClassConfiguration());
            modelBuilder.Configurations.Add(new LabNotesConfiguration());
            modelBuilder.Configurations.Add(new LocationsConfiguration());
            modelBuilder.Configurations.Add(new MakeConfiguration());                  
            modelBuilder.Configurations.Add(new ManufacturerConfiguration());
            modelBuilder.Configurations.Add(new MediaConditionsConfiguration());
            modelBuilder.Configurations.Add(new MediaConfiguration());
            modelBuilder.Configurations.Add(new MediaStatusConfiguration());  
            modelBuilder.Configurations.Add(new NotesConfiguration());
            modelBuilder.Configurations.Add(new OpSystemConfiguration());       
            modelBuilder.Configurations.Add(new PackageConditionsConfiguration());
            modelBuilder.Configurations.Add(new PartNeededConfiguration());
            modelBuilder.Configurations.Add(new PaymentMethodsConfiguration());
            modelBuilder.Configurations.Add(new PCBConditionsConfiguration());
            modelBuilder.Configurations.Add(new PointOfContactConfiguration());
            modelBuilder.Configurations.Add(new ProgramInformationTypeConfiguration());
            modelBuilder.Configurations.Add(new RateOurServiceConfiguration());
            modelBuilder.Configurations.Add(new SendLetterOfReferenceConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderBillingConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderContactConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderEvaluationConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderInquiryFollowUpConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderMediasConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderNotesConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderQuotingConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderRecoveryFollowUpConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderShippingConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderStatusConfiguration());
            modelBuilder.Configurations.Add(new ShippingMediaStatusConfiguration());
            modelBuilder.Configurations.Add(new ShippingMethodsConfiguration());
            modelBuilder.Configurations.Add(new StateConfiguration());                 
            modelBuilder.Configurations.Add(new SuppliersConfiguration());
            modelBuilder.Configurations.Add(new SuppliersContactConfiguration());             
            modelBuilder.Configurations.Add(new TechTeamsConfiguration());             
            modelBuilder.Configurations.Add(new TypeOfContactConfiguration());
            modelBuilder.Configurations.Add(new TypeOfNoteConfiguration());
            modelBuilder.Configurations.Add(new TypeOfServiceConfiguration());
            modelBuilder.Configurations.Add(new TypeRAIDConfiguration());
            modelBuilder.Configurations.Add(new TypeRAIDControlledConfiguration());
            modelBuilder.Configurations.Add(new WouldBeAReferenceConfiguration());
            modelBuilder.Configurations.Add(new ComponentConfiguration());
            modelBuilder.Configurations.Add(new BusinessTypeConfiguration());
            modelBuilder.Configurations.Add(new StockConfiguration());
            modelBuilder.Configurations.Add(new QuoteLinesOptionsConfiguration());
            modelBuilder.Configurations.Add(new MediaModelsConfiguration());
            modelBuilder.Configurations.Add(new RoleLocationsConfiguration());
            modelBuilder.Configurations.Add(new LogSistemaConfiguration());
            modelBuilder.Configurations.Add(new EmailServiceOrderConfiguration());
            //modelBuilder.Configurations.Add(new AdditionalServicesConfiguration());
            //modelBuilder.Configurations.Add(new ServiceOrderAdditionalServicesConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderPaymentsConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderPaymentsItemsConfiguration());
            modelBuilder.Configurations.Add(new FollowUpMessagesBodyConfiguration());
            modelBuilder.Configurations.Add(new FollowUpMessagesToSendConfiguration());
            modelBuilder.Configurations.Add(new FollowUpMessagesSentedHistoryConfiguration());
            modelBuilder.Configurations.Add(new MadeInConfiguration());
            modelBuilder.Configurations.Add(new EquipmentsSellConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderSmsConfiguration());
            modelBuilder.Configurations.Add(new SmsMenuConfiguration());
            modelBuilder.Configurations.Add(new LocalMicrocomputadorConfiguration());
            //modelBuilder.Configurations.Add(new VencimentoConfiguration());
            modelBuilder.Configurations.Add(new CloudConfiguration());
            modelBuilder.Configurations.Add(new VencimentoCloudConfiguration());
            modelBuilder.Configurations.Add(new ServiceOrderCloudConfiguration());

            /// Criação do modelo de dados de acordo com as configurações
            base.OnModelCreating(modelBuilder);

            #endregion

            #region Configuração do mapeamento das Entidades em tabelas
            // ModelConfiguration
            /// Mapeamento da Entidade ApplicationUser para a tabela Usuario
            modelBuilder.Entity<ApplicationUser>().ToTable("Usuario");

            /// Mapeamento da Entidade IdentityRole para a tabela Perfil
            modelBuilder.Entity<IdentityRole>().ToTable("Perfil");

            /// Mapeamento da Entidade IdentityUserRole para a tabela Usuario_Perfil
            modelBuilder.Entity<IdentityUserRole>().ToTable("Usuario_Perfil");

            /// Mapeamento da Entidade IdentityUserLogin para a tabela Usuario_Login
            modelBuilder.Entity<IdentityUserLogin>().ToTable("Usuario_Login");

            /// Mapeamento da Entidade IdentityUserClaim para a tabela Usuario_Claim
            modelBuilder.Entity<IdentityUserClaim>().ToTable("Usuario_Claim");

            #endregion
        }

        #endregion

    }
}
using AutoMapper;
using Framework.Database.Entity;
using Framework.Database.Entity.CBL;
using Framework.Domain.Model;
using Framework.Domain.Model.CBL;

namespace Framework.Domain.AutoMapper
{
   public class DomainToViewModel : Profile
   {
      public DomainToViewModel()
      {
         //WEB
         CreateMap<ApplicationRole, PerfilViewModel>();
         CreateMap<ApplicationUser, UsuarioViewModel>();
         CreateMap<Area, AreaViewModel>();
         CreateMap<Perfil_Area, PerfilAreaViewModel>();
         CreateMap<Agent, AgentViewModel>();
         CreateMap<AgentContacts, AgentContactViewModel>();
         CreateMap<CustomerContact, CustomerContactViewModel>();
         CreateMap<MediaConditions, MediaConditionsViewModel>();
         CreateMap<PartNeeded, PartNeededViewModel>();
         CreateMap<ReferredBy, ReferredByViewModel>();
         CreateMap<ServiceOrderBilling, ServiceOrderBillingViewModel>();
         CreateMap<ServiceOrderContact, ServiceOrderContactViewModel>();
         CreateMap<ServiceOrderEvaluation, ServiceOrderEvaluationViewModel>();
         CreateMap<ServiceOrderInquiryFollowUp, ServiceOrderInquiryFollowUpViewModel>();
         CreateMap<ServiceOrderMedias, ServiceOrderMediasViewModel>();
         CreateMap<ServiceOrderNotes, ServiceOrderNotesViewModel>();
         CreateMap<ServiceOrderQuoting, ServiceOrderQuotingViewModel>();
         CreateMap<ServiceOrderRecoveryFollowUp, ServiceOrderRecoveryFollowUpViewModel>();
         CreateMap<ServiceOrderShipping, ServiceOrderShippingViewModel>();
         CreateMap<ShippingMediaStatus, ShippingMediaStatusViewModel>();
         CreateMap<SuppliersContact, SuppliersContactViewModel>();
         CreateMap<AgentCommissions, AgentCommissionsViewModel>();
         CreateMap<City, CityViewModel>();
         CreateMap<Contact, ContactViewModel>();
         CreateMap<Country, CountryViewModel>();
         CreateMap<Currency, CurrencyViewModel>();
         CreateMap<Customer, CustomerViewModel>();
         CreateMap<DocumentType, DocumentTypeViewModel>();
         CreateMap<FaultFound, FaultFoundViewModel>();
         CreateMap<FileAlocationType, FileAlocationTypeViewModel>();
         CreateMap<HDAConditions, HDAConditionsViewModel>();
         CreateMap<HearOfUs, HearOfUsViewModel>();
         CreateMap<Interfaces, InterfacesViewModel>();
         CreateMap<JobClass, JobClassViewModel>();
         CreateMap<LabNotes, LabNotesViewModel>();
         CreateMap<Locations, LocationsViewModel>();
         CreateMap<Make, MakeViewModel>();
         CreateMap<Manufacturer, ManufacturerViewModel>();
         CreateMap<Media, MediaViewModel>();
         CreateMap<MediaStatus, MediaStatusViewModel>();
         CreateMap<Notes, NotesViewModel>();
         CreateMap<OpSystem, OpSystemViewModel>();
         CreateMap<PackageConditions, PackageConditionsViewModel>();
         CreateMap<PaymentMethods, PaymentMethodsViewModel>();
         CreateMap<PCBConditions, PCBConditionsViewModel>();
         CreateMap<PointOfContact, PointOfContactViewModel>();
         CreateMap<ProgramInformationType, ProgramInformationTypeViewModel>();
         CreateMap<RateOurService, RateOurServiceViewModel>();
         CreateMap<SendLetterOfReference, SendLetterOfReferenceViewModel>();
         CreateMap<ServiceOrder, ServiceOrderViewModel>();
         CreateMap<ServiceOrderStatus, ServiceOrderStatusViewModel>();
         CreateMap<ShippingMethods, ShippingMethodsViewModel>();
         CreateMap<State, StateViewModel>();
         CreateMap<Suppliers, SuppliersViewModel>();
         CreateMap<TechTeams, TechTeamsViewModel>();
         CreateMap<TypeOfContact, TypeOfContactViewModel>();
         CreateMap<TypeOfNote, TypeOfNoteViewModel>();
         CreateMap<TypeOfService, TypeOfServiceViewModel>();
         CreateMap<TypeOfRAID, TypeRAIDViewModel>();
         CreateMap<TypeRAIDControlled, TypeRAIDControlledViewModel>();
         CreateMap<WouldBeAReference, WouldBeAReferenceViewModel>();
         CreateMap<Component, ComponentViewModel>();
         CreateMap<BusinessType, BusinessTypeViewModel>();
         CreateMap<Stock, StockViewModel>();
         CreateMap<QuoteLinesOptions, QuoteLinesOptionsViewModel>();
         CreateMap<MediaModels, MediaModelsViewModel>();
         CreateMap<RoleLocations, RoleLocationsViewModel>();
         CreateMap<EmailServiceOrder, EmailServiceOrderViewModel>();
         CreateMap<AdditionalServices, AdditionalServicesViewModel>();
         //Mapper.CreateMap<ServiceOrderAdditionalServices, ServiceOrderAdditionalServicesViewModel>();
         CreateMap<ServiceOrderPayments, ServiceOrderPaymentsViewModel>();
         CreateMap<ServiceOrderPaymentsItems, ServiceOrderPaymentsItemsViewModel>();
         CreateMap<FollowUpMessagesBody, FollowUpMessagesBodyViewModel>();
         CreateMap<FollowUpMessagesToSend, FollowUpMessagesToSendViewModel>();
         CreateMap<FollowUpMessagesSentedHistory, FollowUpMessagesSentedHistoryViewModel>();
         CreateMap<MadeIn, MadeInViewModel>();
         CreateMap<EquipmentsSell, EquipmentsSellViewModel>();
         CreateMap<ServiceOrderSms, ServiceOrderSmsViewModel>();
         CreateMap<SmsMenu, SmsMenuViewModel>();
         CreateMap<VencimentosCloud, VencimentoCloudViewModel>();
         //var config = new MapperConfiguration(o =>
         //{
         //    o.CreateMap<ApplicationRole, PerfilViewModel>();
         //    o.CreateMap<ApplicationUser, UsuarioViewModel>();
         //    o.CreateMap<Area, AreaViewModel>();
         //    o.CreateMap<Cliente, ClienteViewModel>();
         //    o.CreateMap<Filial, FilialViewModel>();
         //    o.CreateMap<Foto, FotoViewModel>();
         //    o.CreateMap<Galeria, GaleriaViewModel>();
         //    o.CreateMap<Novidade, NovidadeViewModel>();
         //    //o.CreateMap<Perfil_Area, PerfilAreaViewModel>();
         //    o.CreateMap<Playlist, PlaylistViewModel>();
         //    o.CreateMap<Programacao, ProgramacaoViewModel>();
         //    o.CreateMap<Promocao, PromocaoViewModel>();
         //    o.CreateMap<Usuario_Filial, UsuarioFilialViewModel>();
         //});

         //config.CreateMapper();
      }
   }
}
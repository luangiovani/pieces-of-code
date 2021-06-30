using AutoMapper;
using Framework.Database.Entity;
using Framework.Database.Entity.CBL;
using Framework.Domain.Model;
using Framework.Domain.Model.CBL;

namespace Framework.Domain.AutoMapper
{
   public class ViewModelToDomain : Profile
   {
      public ViewModelToDomain()
      {
         //WEB
         CreateMap<PerfilViewModel, ApplicationRole>().IgnoreAllNonExisting();
         CreateMap<UsuarioViewModel, ApplicationUser>();
         CreateMap<AreaViewModel, Area>().IgnoreAllNonExisting();
         CreateMap<PerfilAreaViewModel, Perfil_Area>().IgnoreAllNonExisting();
         CreateMap<AgentViewModel, Agent>().IgnoreAllNonExisting();
         CreateMap<AgentCommissionsViewModel, AgentCommissions>().IgnoreAllNonExisting();
         CreateMap<AgentContactViewModel, AgentContacts>().IgnoreAllNonExisting();
         CreateMap<CustomerContactViewModel, CustomerContact>().IgnoreAllNonExisting();
         CreateMap<MediaConditionsViewModel, MediaConditions>().IgnoreAllNonExisting();
         CreateMap<PartNeededViewModel, PartNeeded>().IgnoreAllNonExisting();
         CreateMap<ReferredByViewModel, ReferredBy>().IgnoreAllNonExisting();
         CreateMap<ServiceOrderBillingViewModel, ServiceOrderBilling>().IgnoreAllNonExisting();
         CreateMap<ServiceOrderContactViewModel, ServiceOrderContact>().IgnoreAllNonExisting();
         CreateMap<ServiceOrderEvaluationViewModel, ServiceOrderEvaluation>().IgnoreAllNonExisting();
         CreateMap<ServiceOrderInquiryFollowUpViewModel, ServiceOrderInquiryFollowUp>().IgnoreAllNonExisting();
         CreateMap<ServiceOrderMediasViewModel, ServiceOrderMedias>();
         CreateMap<ServiceOrderNotesViewModel, ServiceOrderNotes>().IgnoreAllNonExisting();
         CreateMap<ServiceOrderQuotingViewModel, ServiceOrderQuoting>().IgnoreAllNonExisting();
         CreateMap<ServiceOrderRecoveryFollowUpViewModel, ServiceOrderRecoveryFollowUp>().IgnoreAllNonExisting();
         CreateMap<ServiceOrderShippingViewModel, ServiceOrderShipping>().IgnoreAllNonExisting();
         CreateMap<ShippingMediaStatusViewModel, ShippingMediaStatus>().IgnoreAllNonExisting();
         CreateMap<SuppliersContactViewModel, SuppliersContact>().IgnoreAllNonExisting();
         CreateMap<CityViewModel, City>().IgnoreAllNonExisting();
         CreateMap<ContactViewModel, Contact>().IgnoreAllNonExisting();
         CreateMap<CountryViewModel, Country>().IgnoreAllNonExisting();
         CreateMap<CurrencyViewModel, Currency>().IgnoreAllNonExisting();
         CreateMap<CustomerViewModel, Customer>().IgnoreAllNonExisting();
         CreateMap<DocumentTypeViewModel, DocumentType>().IgnoreAllNonExisting();
         CreateMap<FaultFoundViewModel, FaultFound>().IgnoreAllNonExisting();
         CreateMap<FileAlocationTypeViewModel, FileAlocationType>().IgnoreAllNonExisting();
         CreateMap<HDAConditionsViewModel, HDAConditions>().IgnoreAllNonExisting();
         CreateMap<HearOfUsViewModel, HearOfUs>().IgnoreAllNonExisting();
         CreateMap<InterfacesViewModel, Interfaces>().IgnoreAllNonExisting();
         CreateMap<JobClassViewModel, JobClass>().IgnoreAllNonExisting();
         CreateMap<LabNotesViewModel, LabNotes>().IgnoreAllNonExisting();
         CreateMap<LocationsViewModel, Locations>().IgnoreAllNonExisting();
         CreateMap<MakeViewModel, Make>().IgnoreAllNonExisting();
         CreateMap<ManufacturerViewModel, Manufacturer>().IgnoreAllNonExisting();
         CreateMap<MediaViewModel, Media>().IgnoreAllNonExisting();
         CreateMap<MediaStatusViewModel, MediaStatus>().IgnoreAllNonExisting();
         CreateMap<NotesViewModel, Notes>().IgnoreAllNonExisting();
         CreateMap<OpSystemViewModel, OpSystem>().IgnoreAllNonExisting();
         CreateMap<PackageConditionsViewModel, PackageConditions>().IgnoreAllNonExisting();
         CreateMap<PaymentMethodsViewModel, PaymentMethods>().IgnoreAllNonExisting();
         CreateMap<PCBConditionsViewModel, PCBConditions>().IgnoreAllNonExisting();
         CreateMap<PointOfContactViewModel, PointOfContact>().IgnoreAllNonExisting();
         CreateMap<ProgramInformationTypeViewModel, ProgramInformationType>().IgnoreAllNonExisting();
         CreateMap<RateOurServiceViewModel, RateOurService>().IgnoreAllNonExisting();
         CreateMap<SendLetterOfReferenceViewModel, SendLetterOfReference>().IgnoreAllNonExisting();
         CreateMap<ServiceOrderViewModel, ServiceOrder>().IgnoreAllNonExisting();
         CreateMap<ServiceOrderStatusViewModel, ServiceOrderStatus>().IgnoreAllNonExisting();
         CreateMap<ServiceOrderInquiryFollowUpViewModel, ServiceOrderInquiryFollowUp>().IgnoreAllNonExisting();
         CreateMap<ShippingMethodsViewModel, ShippingMethods>().IgnoreAllNonExisting();
         CreateMap<StateViewModel, State>().IgnoreAllNonExisting();
         CreateMap<SuppliersViewModel, Suppliers>().IgnoreAllNonExisting();
         CreateMap<TechTeamsViewModel, TechTeams>().IgnoreAllNonExisting();
         CreateMap<TypeOfContactViewModel, TypeOfContact>().IgnoreAllNonExisting();
         CreateMap<TypeOfNoteViewModel, TypeOfNote>().IgnoreAllNonExisting();
         CreateMap<TypeOfServiceViewModel, TypeOfService>().IgnoreAllNonExisting();
         CreateMap<TypeRAIDViewModel, TypeOfRAID>().IgnoreAllNonExisting();
         CreateMap<TypeRAIDControlledViewModel, TypeRAIDControlled>().IgnoreAllNonExisting();
         CreateMap<WouldBeAReferenceViewModel, WouldBeAReference>().IgnoreAllNonExisting();
         CreateMap<ComponentViewModel, Component>().IgnoreAllNonExisting();
         CreateMap<BusinessTypeViewModel, BusinessType>().IgnoreAllNonExisting();
         CreateMap<StockViewModel, Stock>();
         CreateMap<QuoteLinesOptionsViewModel, QuoteLinesOptions>();
         CreateMap<MediaModelsViewModel, MediaModels>();
         CreateMap<RoleLocationsViewModel, RoleLocations>();
         CreateMap<EmailServiceOrderViewModel, EmailServiceOrder>();
         CreateMap<AdditionalServicesViewModel, AdditionalServices>();
         //Mapper.CreateMap<ServiceOrderAdditionalServicesViewModel, ServiceOrderAdditionalServices>();
         CreateMap<ServiceOrderPaymentsViewModel, ServiceOrderPayments>();
         CreateMap<ServiceOrderPaymentsItemsViewModel, ServiceOrderPaymentsItems>();
         CreateMap<FollowUpMessagesBodyViewModel, FollowUpMessagesBody>();
         CreateMap<FollowUpMessagesToSendViewModel, FollowUpMessagesToSend>();
         CreateMap<FollowUpMessagesSentedHistoryViewModel, FollowUpMessagesSentedHistory>();
         CreateMap<MadeInViewModel, MadeIn>();
         CreateMap<ServiceOrderSmsViewModel, ServiceOrderSms>().IgnoreAllNonExisting();
         CreateMap<SmsMenuViewModel, SmsMenu>().IgnoreAllNonExisting();
         CreateMap<VencimentoCloudViewModel, VencimentosCloud>().IgnoreAllNonExisting();
      }
   }
}
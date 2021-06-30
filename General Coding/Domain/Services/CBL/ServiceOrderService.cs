using AutoMapper;
using Framework.Database.Entity.CBL;
using Framework.Domain.Model;
using Framework.Domain.Model.CBL;
using Framework.Domain.Repository.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Services.CBL
{
   public class ServiceOrderService : ServiceBase<ServiceOrder, ServiceOrderViewModel>
   {
      private readonly ServiceOrderRepository _repository;


      public ServiceOrderService(ServiceOrderRepository repository)
          : base(repository)
      {
         _repository = repository;

      }



      public ICollection<ServiceOrderViewModel> GetEstruturado()
      {
         List<ServiceOrder> final = new List<ServiceOrder>();

         var orders = _repository.GetAll();

         var listaFinal = new List<ServiceOrderViewModel>();

         foreach (var order in orders)
         {
            listaFinal.Add(Mapper.Map<ServiceOrderViewModel>(order));
         }

         return listaFinal;
      }

      public ICollection<ServiceOrderViewModel> GetEstruturado(Expression<Func<ServiceOrder, bool>> exp)
      {
         List<ServiceOrder> final = new List<ServiceOrder>();

         var orders = _repository.GetAll(exp);

         var listaFinal = new List<ServiceOrderViewModel>();

         foreach (var order in orders)
         {
            listaFinal.Add(Mapper.Map<ServiceOrderViewModel>(order));
         }

         return listaFinal;
      }

      public ServiceOrderViewModel GetByIdNovo(decimal serviceOrderId)
      {
         var serviceOrder = _repository.GetById(serviceOrderId);

         var objeach = serviceOrder.serviceOrderMedias;
         var listMedias = new List<ServiceOrderMediasViewModel>();
         foreach (var item in objeach)
         {
            listMedias.Add(item);
         }

         ServiceOrderViewModel objServiceOrderViewModel = new ServiceOrderViewModel
         {
            //agentComissions = obj.agentComissions,
            approvalDate = serviceOrder.approvalDate,
            arrivedBy = serviceOrder.arrivedBy,
            contacts = serviceOrder.contacts.Cast<ServiceOrderContactViewModel>().ToList(),
            //contacts = (ICollection<ServiceOrderContactViewModel>)obj.contacts,
            CSR = serviceOrder.CSR,
            customer = serviceOrder.customer,
            customer_id = serviceOrder.customer_id,
            customerContact_id = serviceOrder.customerContact_id,
            customerContactEmail = serviceOrder.customerContactEmail,
            customerContactMobile = serviceOrder.customerContactMobile,
            customerContactName = serviceOrder.customerContactName,
            customerContactTelephone = serviceOrder.customerContactTelephone,
            date = serviceOrder.date,
            dateAssigned = serviceOrder.dateAssigned,
            //dtaAprovacaoContrato = obj.dtaAprovacaoContrato,
            estimate = serviceOrder.estimate,
            extensionStatus = serviceOrder.extensionStatus,
            id_old = serviceOrder.id_old,
            inTransfer = serviceOrder.inTransfer,
            //labNotes = obj.labNotes,
            location = serviceOrder.location,
            location_id = serviceOrder.location_id,
            locationReceived = serviceOrder.locationReceived,
            locationReceived_id = serviceOrder.locationReceived_id,
            mostImportantFilesToRecovery = serviceOrder.mostImportantFilesToRecovery,
            note = serviceOrder.note,
            originOfServiceOrder = serviceOrder.originOfServiceOrder,
            packageCondidition = serviceOrder.packageCondidition,
            referredBy = serviceOrder.referredBy,
            serviceOrder_id = serviceOrder.serviceOrder_id,
            serviceOrderBilling = serviceOrder.serviceOrderBilling,
            serviceOrderEvaluation = serviceOrder.serviceOrderEvaluation,
            serviceOrderInquiryFollowUp = serviceOrder.serviceOrderInquiryFollowUp,
            serviceOrderMedias = listMedias,
            //serviceOrderNotes = (List<ServiceOrderNotesViewModel>)obj.serviceOrderNotes,
            serviceOrderQuoting = serviceOrder.serviceOrderQuoting,
            serviceOrderRecoveryFollowUp = serviceOrder.serviceOrderRecoveryFollowUp,
            serviceOrderShipping = serviceOrder.serviceOrderShipping,
            serviceToExecute = serviceOrder.serviceToExecute,
            smartNumber = serviceOrder.smartNumber,
            status = serviceOrder.status,
            statusDate = serviceOrder.statusDate,
            subStatus = serviceOrder.subStatus,
            takenBy = serviceOrder.takenBy,
            techsName = serviceOrder.techsName,
            transferNote = serviceOrder.transferNote,
            typeOfService = serviceOrder.typeOfService,
            urlUploadContrato = serviceOrder.urlUploadContrato,
            //user = obj.user,
            user_id = serviceOrder.user_id,
            //userAssigned = obj.userAssigned,
            userAssigned_id = serviceOrder.userAssigned_id,
            wayBillNumber = serviceOrder.wayBillNumber
         };

         return objServiceOrderViewModel;
      }

      public HomeViewModel GetHomeInformations(List<int> locations)
      {
         var model = _repository.GetHomeInformations(locations);

         var last2days = DateTime.Now.AddDays(-2);
         var last14days = DateTime.Now.AddDays(-14);
         var last60days = DateTime.Now.AddDays(-60);

         var ordersIncoming = this.GetAllPrimitiveType(s => ((s.location_id.HasValue && locations.Contains(s.location_id.Value)) || locations.Contains(s.locationReceived_id))
             && s.date >= last2days
             && s.originOfServiceOrder == "Site"
             ).OrderByDescending(s => s.date).ToList();

         foreach (var soModel in ordersIncoming)
         {
            model.lastIncomingJobs.Add(new LastIncomingJobsViewModel()
            {
               serviceOrderId = soModel.serviceOrder_id,
               customerName = soModel.customer != null ? soModel.customer.name : "NO CUSTOMER",
               OS_Series = soModel.location_id.HasValue && !soModel.id_old.HasValue ? soModel.location.OS_Series : (soModel.locationReceived_id > 0 && !soModel.id_old.HasValue ? soModel.locationReceived.OS_Series : ""),
               isRush = (soModel.typeOfService != null && soModel.typeOfService.ToUpper() == "RUSH")
            });
         }

         var ordersLastInteractions = this.GetAllPrimitiveType(s => ((s.location_id.HasValue && locations.Contains(s.location_id.Value)) || locations.Contains(s.locationReceived_id))
             && s.date >= last60days
             && s.serviceOrderNotes.Any(n => n.dateRegistration >= last14days)
             ).OrderByDescending(s => s.date).ToList();

         foreach (var soModel in ordersLastInteractions)
         {
            var note = soModel.serviceOrderNotes.OrderByDescending(n => n.dateRegistration).FirstOrDefault();

            model.recentInteractions.Add(new RecentInteractionsViewModel()
            {
               serviceOrderId = soModel.serviceOrder_id,
               date = note.dateRegistration,
               description = note.note.description,
               userName = note.note.user.nome,
               customerName = soModel.customer.name,
               location = soModel.location_id.HasValue ? soModel.location.name : "Received on: " + soModel.locationReceived.name,
               OS_Series = soModel.location_id.HasValue && !soModel.id_old.HasValue ? soModel.location.OS_Series : (soModel.locationReceived_id > 0 && !soModel.id_old.HasValue ? soModel.locationReceived.OS_Series : ""),
               isRush = (soModel.typeOfService != null && soModel.typeOfService.ToUpper() == "RUSH")
            });
         }

         return model;
      }

      public ICollection<ServiceOrderIndexViewModel> GetOrdersFilters(List<int> locations, string filterByStatus = "", string filterByCustomer = "",
          string filterByDateFrom = "", string filterByDateTo = "", string filterByUser = "", string filterByCustomerCPFCNPJ = "",
          string filterByQuotingSubStatus = "", string filterByTypeOfService = "", string filterBysubStatus = "", bool filterByStatusUser = false,
          string filterByPlanoComprado = "", string filterByPlanoAdquirido = "", string filterByLocalMicrocomputador = "",
          int pagina = 1, int qtdPorPagina = 10)
      {
         return _repository.GetOrdersFilters(locations, filterByStatus, filterByCustomer, filterByDateFrom, filterByDateTo, filterByUser,
                                             filterByCustomerCPFCNPJ, filterByQuotingSubStatus, filterByTypeOfService, filterBysubStatus, filterByStatusUser,
                                             filterByPlanoComprado, filterByPlanoAdquirido, filterByLocalMicrocomputador, pagina, qtdPorPagina).ToList();
      }

      public ICollection<ReportsViewModel> GetOrdersReport(List<int> locations, string filterByStatus = "", string filterByCustomer = "",
          string filterByDateFrom = "", string filterByDateTo = "", string filterByStatusDateFrom = "", string filterByStatusDateTo = "",
          string filterByCustomerCPFCNPJ = "", string filterByQuotingSubStatus = "", string filterByTypeOfService = "",
          string filterByPlanoComprado = "", string filterByPlanoAdquirido = "", string filterByLocalMicrocomputador = "",
          string LayoutFilter = "")
      {
         return _repository.GetOrdersReport(locations, filterByStatus, filterByCustomer, filterByDateFrom, filterByDateTo,
             filterByStatusDateFrom, filterByStatusDateTo, filterByCustomerCPFCNPJ, filterByQuotingSubStatus, filterByTypeOfService,
             filterByPlanoComprado, filterByPlanoAdquirido, filterByLocalMicrocomputador,
             LayoutFilter).ToList();
      }

      public decimal SaveByProcedure(ServiceOrderViewModel model)
      {

         try
         {
            decimal result = -2;
            if (model.serviceOrder_id == 0)
               result = Insert(model).serviceOrder_id;
            else
               result = UpdateReturnId(model);

            if (result != -2)
               return result;
            else
               return -2;
         }
         catch (Exception x)
         {
            return -2;
         }

         //try
         //{
         //    ServiceOrderViewModel so;
         //    if (model.serviceOrder_id == 0)
         //        so = base.Insert(model);
         //    else
         //        so = base.Update(model);

         //    if (so != null)
         //        return so.serviceOrder_id;
         //    else
         //        return -2;
         //}
         //catch (Exception x)
         //{
         //    return -2;
         //}

         //return _repository.InsertUpdateProcedure(model);
      }

      private decimal UpdateReturnId(ServiceOrderViewModel model)
      {
         var current = _repository.GetById(model.serviceOrder_id);
         current = Mapper.Map<ServiceOrderViewModel, ServiceOrder>(model, current);
         return _repository.Update(current).serviceOrder_id;
      }

      public void saveStatusServiceOrder(decimal serviceOrder, string status)
      {
         _repository.saveStatusServiceOrder(serviceOrder, status);
      }

      public void SaveInTransferLocationId(decimal serviceOrderId, int locationId, string TransferNotes)
      {
         _repository.SaveInTransferLocationId(serviceOrderId, locationId, TransferNotes);
      }

      public string SaveCodigoRastreio(string serviceOrderId, string codigoRastreio)
      {
         string retorno = "";
         try
         {
            retorno = "OK";
            _repository.SaveCodigoRastreio(serviceOrderId, codigoRastreio);
         }
         catch (Exception e)
         {

            retorno = "Erro: " + e.Message;
         }
         return retorno;
      }

      public string SaveUrlUpload(string serviceOrderId, string urlUploadContrato)
      {
         string retorno = "";
         try
         {
            retorno = "OK";
            _repository.SaveUrlUpload(serviceOrderId, urlUploadContrato);
         }
         catch (Exception e)
         {

            retorno = "Erro: " + e.Message;
         }
         return retorno;
      }


      public IEnumerable<DetailServiceOrderViewModel> selectFromDetailPortal(string serviceOrderId)
      {
         return _repository.selectFromDetailPortal(serviceOrderId);
      }


      public IEnumerable<EquipmentsSellViewModel> selectFromEquipmentsSell(string usuarioLogado, string serviceOrderId)
      {
         return _repository.selectFromEquipmentsSell(usuarioLogado, serviceOrderId);
      }


      public Decimal selectTotalOrcamento(string serviceOrderId)
      {
         return _repository.selectTotalOrcamento(serviceOrderId);
      }
      public List<LocationsViewModel> ListLocation(string location_id)
      {
         return _repository.ListLocation(location_id);
      }

      public void UpdateNovo(ServiceOrderViewModel model)
      {
         var current = _repository.GetById(model.serviceOrder_id);
         current = Mapper.Map<ServiceOrderViewModel, ServiceOrder>(model, current);
         _repository.Update(current);
      }

      public ServiceOrderViewModel GetByIdNovo(decimal? serviceOrderId)
      {
         var objServiceOrder = _repository.GetById(serviceOrderId);

         var objeach = objServiceOrder.serviceOrderMedias;
         var listMedias = new List<ServiceOrderMediasViewModel>();
         foreach (var item in objeach)
         {
            listMedias.Add(item);
         }

         ServiceOrderViewModel obj = new ServiceOrderViewModel
         {
            //agentComissions = obj.agentComissions,
            approvalDate = objServiceOrder.approvalDate,
            arrivedBy = objServiceOrder.arrivedBy,
            contacts = objServiceOrder.contacts.Cast<ServiceOrderContactViewModel>().ToList(),
            //contacts = (ICollection<ServiceOrderContactViewModel>)obj.contacts,
            CSR = objServiceOrder.CSR,
            customer = objServiceOrder.customer,
            customer_id = objServiceOrder.customer_id,
            customerContact_id = objServiceOrder.customerContact_id,
            customerContactEmail = objServiceOrder.customerContactEmail,
            customerContactMobile = objServiceOrder.customerContactMobile,
            customerContactName = objServiceOrder.customerContactName,
            customerContactTelephone = objServiceOrder.customerContactTelephone,
            date = objServiceOrder.date,
            dateAssigned = objServiceOrder.dateAssigned,
            //dtaAprovacaoContrato = objServiceOrder.dtaAprovacaoContrato,
            estimate = objServiceOrder.estimate,
            extensionStatus = objServiceOrder.extensionStatus,
            id_old = objServiceOrder.id_old,
            inTransfer = objServiceOrder.inTransfer,
            //labNotes = objServiceOrder.labNotes,
            location = objServiceOrder.location,
            location_id = objServiceOrder.location_id,
            locationReceived = objServiceOrder.locationReceived,
            locationReceived_id = objServiceOrder.locationReceived_id,
            mostImportantFilesToRecovery = objServiceOrder.mostImportantFilesToRecovery,
            note = objServiceOrder.note,
            originOfServiceOrder = objServiceOrder.originOfServiceOrder,
            packageCondidition = objServiceOrder.packageCondidition,
            referredBy = objServiceOrder.referredBy,
            serviceOrder_id = objServiceOrder.serviceOrder_id,
            serviceOrderBilling = objServiceOrder.serviceOrderBilling,
            serviceOrderEvaluation = objServiceOrder.serviceOrderEvaluation,
            serviceOrderInquiryFollowUp = objServiceOrder.serviceOrderInquiryFollowUp,
            serviceOrderMedias = listMedias,
            //serviceOrderNotes = (List<ServiceOrderNotesViewModel>)obj.serviceOrderNotes,
            serviceOrderQuoting = objServiceOrder.serviceOrderQuoting,
            serviceOrderRecoveryFollowUp = objServiceOrder.serviceOrderRecoveryFollowUp,
            serviceOrderShipping = objServiceOrder.serviceOrderShipping,
            serviceToExecute = objServiceOrder.serviceToExecute,
            smartNumber = objServiceOrder.smartNumber,
            status = objServiceOrder.status,
            statusDate = objServiceOrder.statusDate,
            subStatus = objServiceOrder.subStatus,
            takenBy = objServiceOrder.takenBy,
            techsName = objServiceOrder.techsName,
            transferNote = objServiceOrder.transferNote,
            typeOfService = objServiceOrder.typeOfService,
            urlUploadContrato = objServiceOrder.urlUploadContrato,
            //user = objServiceOrder.user,
            user_id = objServiceOrder.user_id,
            //userAssigned = objServiceOrder.userAssigned,
            userAssigned_id = objServiceOrder.userAssigned_id,
            wayBillNumber = objServiceOrder.wayBillNumber
         };

         return obj;
      }
   }
}

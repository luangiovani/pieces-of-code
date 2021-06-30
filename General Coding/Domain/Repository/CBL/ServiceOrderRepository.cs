using Framework.Database.Entity.CBL;
using Framework.Domain.Model;
using Framework.Domain.Model.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Repository.CBL
{
   public class ServiceOrderRepository : RepositoryBase<ServiceOrder>
   {
      public HomeViewModel GetHomeInformations(List<int> locations)
      {
         string inLocations = "";

         foreach (var locationId in locations)
         {
            if (inLocations == "")
            {
               inLocations = locationId.ToString();
            }
            else
            {
               inLocations += "," + locationId.ToString();
            }
         }
         #region SQL
         string sSql = @"SELECT 
(SELECT COUNT(DISTINCT serviceOrder_id) 
  FROM ServiceOrder
 WHERE CONVERT(DATE, date, 103) >= CONVERT(DATE, GETDATE()-2, 103)
   AND status NOT IN('Closed')
   AND (location_id IN(" + inLocations + @") OR(location_id IS NULL AND locationReceived_id IN(" + inLocations + @"))))newJobs,
(SELECT COUNT(DISTINCT S.serviceOrder_id) 
  FROM ServiceOrder S
  JOIN ServiceOrderQuoting SQ ON SQ.serviceOrder_id = S.serviceOrder_id
 WHERE CONVERT(DATE, date, 103) <= CONVERT(DATE, GETDATE()-7, 103)
   AND status = 'Go Ahead'
   AND DATEDIFF(DAY, ISNULL(S.approvalDate,GETDATE()), GETDATE()) > SQ.quoteDays
   AND (S.location_id IN(" + inLocations + @") OR(S.location_id IS NULL AND S.locationReceived_id IN(" + inLocations + @")))) delayJobs,
(SELECT COUNT(DISTINCT serviceOrder_id)
  FROM ServiceOrder
 WHERE userAssigned_id IS NULL
   AND status NOT IN('Closed')
   AND (location_id IN(" + inLocations + @") OR(location_id IS NULL AND locationReceived_id IN(" + inLocations + @")))) unassignedJobs,
(SELECT COUNT(DISTINCT serviceOrder_id) 
  FROM ServiceOrder
 WHERE status IN('Go Ahead')
 AND (location_id IN(" + inLocations + @") OR(location_id IS NULL AND locationReceived_id IN(" + inLocations + @")))) goAheadJobs,
(SELECT COUNT(DISTINCT S.serviceOrder_id) 
  FROM ServiceOrder S
LEFT JOIN ServiceOrderInquiryFollowUp SI ON SI.serviceOrder_id = S.serviceOrder_id
 WHERE S.status IN('Incoming')
   /*AND CONVERT(DATE, S.date, 103) < CONVERT(DATE, GETDATE()-2, 103)*/
   /*AND (S.location_id IN(" + inLocations + @") OR(S.location_id IS NULL AND S.locationReceived_id IN(" + inLocations + @")))*/
   AND (/*SI.serviceOrder_id IS NULL OR*/ SI.dateComplete IS NULL)) followUpJobs,
(SELECT COUNT(DISTINCT SE.serviceOrder_id)
  FROM ServiceOrder S
  JOIN ServiceOrderEvaluation SE ON SE.serviceOrder_id = S.serviceOrder_id
 WHERE SE.diagnosisFinished = 0
   AND UPPER(S.status) IN('PENDING')
   AND (S.location_id IN(" + inLocations + @") OR(S.location_id IS NULL AND S.locationReceived_id IN(" + inLocations + @")))) waitQuotingJobs,
(SELECT COUNT(DISTINCT serviceOrder_id) 
  FROM ServiceOrder
 WHERE status IN('Quoted')
 AND (location_id IN(" + inLocations + @") OR(location_id IS NULL AND locationReceived_id IN(" + inLocations + @")))) quotedJobs,
(SELECT COUNT(DISTINCT serviceOrder_id) 
  FROM ServiceOrder
 WHERE status IN('Incoming','Pending','Quoted','Go Ahead')
   AND TypeOfService = 'Rush'
   AND (location_id IN(" + inLocations + @") OR(location_id IS NULL AND locationReceived_id IN(" + inLocations + @")))) rushOrders";
         #endregion

         return Db.Database.SqlQuery<HomeViewModel>(sSql).FirstOrDefault();
      }

      public IEnumerable<ServiceOrderIndexViewModel> GetOrdersFilters(List<int> locations, string filterByStatus = "", string filterByCustomer = "",
          string filterByDateFrom = "", string filterByDateTo = "", string filterByUser = "", string filterByCustomerCPFCNPJ = "",
          string filterByQuotingSubStatus = "", string filterByTypeOfService = "", string filterBySubStatus = "", bool filterByStatusUser = false,
          string filterByPlanoComprado = "", string filterByPlanoAdquirido = "", string filterByLocalMicrocomputador = "",
          int pagina = 1, int qtdPorPagina = 10)
      {
         #region sSql

         #region Locations
         string inLocations = "";

         foreach (var locationId in locations)
         {
            if (inLocations == "")
            {
               inLocations = locationId.ToString();
            }
            else
            {
               inLocations += "," + locationId.ToString();
            }
         }
         #endregion

         #region Principal
         string sSql = @"
SELECT /*Top(" + qtdPorPagina + @" * " + pagina + @") ROW_NUMBER() OVER (ORDER BY CASE WHEN S.status = 'Go Ahead' THEN DATEDIFF(DAY, ISNULL(S.approvalDate,GETDATE()), GETDATE()) ELSE 0 END desc) ,*/
S.serviceOrder_id, 
CONVERT(VARCHAR(10), S.date, 103) + ' '  + convert(VARCHAR(5), s.date, 14) as serviceOrderDate,
C.name as customerName,
S.status as serviceOrderStatus,
CASE WHEN S.locationReceived_id IS NOT NULL THEN 
CONVERT(VARCHAR(10), S.serviceOrder_id)+'/'+LR.OS_Series 
ELSE 
CONVERT(VARCHAR(10), S.serviceOrder_id)+'/'+ISNULL(L.OS_Series,'') 
END as orderIdOrderSeries,
CONVERT(BIT, CASE WHEN S.typeOfService = 'Rush' THEN 1 else 0 END) isRush,
CASE WHEN S.status = 'Go Ahead' THEN
DATEDIFF(DAY, ISNULL(S.approvalDate,GETDATE()), GETDATE())
ELSE
0 
END delayDays, S.subStatus
FROM ServiceOrder S
JOIN Customer C ON C.customer_id = S.customer_id
JOIN Locations LR on LR.location_id = s.locationReceived_id 
LEFT JOIN Locations L on L.location_id = S.location_id
LEFT JOIN ServiceOrderCloud SOC ON ISNULL(SOC.serviceOrder_id,0) = S.serviceOrder_id
WHERE (S.location_id IN(" + inLocations + @") OR S.locationReceived_id IN(" + inLocations + @")) ";
         #endregion

         #region filterByStatusUser
         if (filterByStatusUser)
         {
            sSql += @" AND (CONVERT(DATE, S.date, 103) >= CONVERT(DATE, '01/01/2010', 103) ";
            if ((String.IsNullOrEmpty(filterByStatus)) && (String.IsNullOrEmpty(filterBySubStatus)))
            {
               sSql += @"AND S.status != 'Closed' AND S.status != 'Declined' AND S.status != 'Recovered' AND S.status != 'Unrecovered' AND S.status != 'Unrecovered DOA'";
            }
            sSql += @"or (CONVERT(DATE, S.date, 103) >= CONVERT(DATE, '01/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "" + @"', 103))
                ) ";
         }
         #endregion

         #region Filter By Status and SubStatus

         if (!String.IsNullOrEmpty(filterByStatus))
         {
            if (filterByStatus == "Transfer")
            {
               filterByStatus = "";
               filterBySubStatus = "Transfer";
            }
         }
         if (!String.IsNullOrEmpty(filterByStatus))
         {
            sSql += @" AND S.status = '" + filterByStatus + @"' ";
         }
         if (!String.IsNullOrEmpty(filterBySubStatus))
         {
            sSql += @" AND S.subStatus = '" + filterBySubStatus + @"' ";
         }

         #endregion

         #region Filter By Customer
         if (!String.IsNullOrEmpty(filterByCustomer))
         {
            sSql += @" AND C.name LIKE'%" + filterByCustomer + @"%' ";
         }
         #endregion

         #region Filter By Date From
         if (!String.IsNullOrEmpty(filterByDateFrom))
         {
            if (filterByStatus == "Closed" || filterByStatus == "Declined" || filterByStatus == "Recovered" || filterByStatus == "Unrecovered" || filterByStatus == "Unrecovered DOA")
            {
               sSql += @" AND CONVERT(DATE, S.statusDate, 103) >= CONVERT(DATE, '" + filterByDateFrom + @"', 103) ";
            }
            else
            {
               sSql += @" AND CONVERT(DATE, S.date, 103) >= CONVERT(DATE, '" + filterByDateFrom + @"', 103) ";
            }
         }
         #endregion

         #region Filter By Date To
         if (!String.IsNullOrEmpty(filterByDateTo))
         {
            if (filterByStatus == "Closed" || filterByStatus == "Declined" || filterByStatus == "Recovered" || filterByStatus == "Unrecovered" || filterByStatus == "Unrecovered DOA")
            {
               sSql += @" AND CONVERT(DATE, S.statusDate, 103) <= CONVERT(DATE, '" + filterByDateTo + @"', 103) ";
            }
            else
            {
               sSql += @" AND CONVERT(DATE, S.date, 103) <= CONVERT(DATE, '" + filterByDateTo + @"', 103) ";
            }
         }
         #endregion

         #region Filter By User
         if (!String.IsNullOrEmpty(filterByUser))
         {
            sSql += @" AND S.userAssigned_id = '" + filterByUser + @"' ";


         }
         #endregion

         #region Filter By Customer CPFCNPJ
         if (!String.IsNullOrEmpty(filterByCustomerCPFCNPJ))
         {
            sSql += @" AND C.cpfCnpj LIKE '%" + filterByCustomerCPFCNPJ + @"%' ";
         }
         #endregion

         #region Filter By Quoting Status
         if (!String.IsNullOrEmpty(filterByQuotingSubStatus))
         {
            sSql += @" AND S.subStatus = '" + filterByQuotingSubStatus + @"' ";
         }
         #endregion

         #region Filter By Type Of Service
         if (!String.IsNullOrEmpty(filterByTypeOfService))
         {
            sSql += @" AND S.typeOfService = '" + filterByTypeOfService + @"' ";
         }
         #endregion

         #region Filter by string filterByPlanoComprado
         if (!String.IsNullOrEmpty(filterByPlanoComprado))
         {
            sSql += @" AND SOC.idCloud = '" + filterByPlanoComprado + @"' ";
         }
         #endregion

         #region Filter by string filterByPlanoAdquirido
         if (!String.IsNullOrEmpty(filterByPlanoAdquirido))
         {
            sSql += @" AND SOC.idVencimentoCloud = '" + filterByPlanoAdquirido + @"' ";
         }
         #endregion

         #region Filter by string filterByLocalMicrocomputador
         if (!String.IsNullOrEmpty(filterByLocalMicrocomputador))
         {
            sSql += @" AND SOC.idLocalMicrocomputador = '" + filterByLocalMicrocomputador + @"' ";
         }
         #endregion


         #region Group By
         sSql += @" GROUP BY s.serviceOrder_id, s.date, c.name, S.location_id, s.locationReceived_id, S.status, L.OS_Series, LR.OS_Series, S.typeOfService,S.approvalDate, S.subStatus";
         #endregion

         #region Order By | Pagination
         int aux = qtdPorPagina * (pagina - 1);

         sSql = @"SELECT * FROM (" + sSql + @")t1 ORDER BY delayDays DESC OFFSET " + aux + @" ROWS FETCH NEXT " + qtdPorPagina + @" ROWS ONLY";
         #endregion

         #endregion

         return Db.Database.SqlQuery<ServiceOrderIndexViewModel>(sSql).ToList();
      }

      public IEnumerable<ReportsViewModel> GetOrdersReport(List<int> locations, string filterByStatus = "", string filterByCustomer = "",
          string filterByDateFrom = "", string filterByDateTo = "", string filterByStatusDateFrom = "", string filterByStatusDateTo = "",
          string filterByCustomerCPFCNPJ = "", string filterByQuotingSubStatus = "", string filterByTypeOfService = "",
          string filterByPlanoComprado = "", string filterByPlanoAdquirido = "", string filterByLocalMicrocomputador = "",
          string LayoutFilter = "")
      {
         #region sSql Orders

         #region Locations
         string inLocations = "";

         foreach (var locationId in locations)
         {
            if (inLocations == "")
            {
               inLocations = locationId.ToString();
            }
            else
            {
               inLocations += "," + locationId.ToString();
            }
         }
         #endregion

         #region Principal
         string sSql = @"SELECT CONVERT(VARCHAR, S.serviceOrder_id) ServiceOrderId,
		   CONVERT(VARCHAR(10), S.date, 103) + ' '  + convert(VARCHAR(5), s.date, 14) ServiceOrderDate,
		   LR.name ServiceOrderLocationReceived,
		   CASE WHEN CL.location_id IS NULL THEN LR.name ELSE CL.name END ServiceOrderLocation,
		   S.status ServiceOrderStatus,
		   S.subStatus ServiceOrderSubStatus,
		   S.referredBy ServiceOrderReferredBy,
		   CONVERT(VARCHAR(10), SQ.quoteDate, 103) ServiceOrderDateQuoted,
		   CONVERT(VARCHAR(10), S.approvalDate, 103) ServiceOrderApprovalDate,
           convert(varchar,DATEDIFF(day, S.approvalDate, S.statusDate))+'' ServiceOrderRecoveryTime,
		   CONVERT(VARCHAR(10), S.statusDate, 103) ServiceOrderStatusDate,
		   CONVERT(VARCHAR, DATEDIFF(DAY, ISNULL(S.approvalDate,GETDATE()), GETDATE())) ServiceOrderDelay,
		   ISNULL(SN.description,'') ServiceOrderNotes,
		   CONVERT(VARCHAR(10), SN.date, 103) + ' '  + convert(VARCHAR(5), SN.date, 14) ServiceOrderNotesDate,
		   ISNULL(SN.nome,'') ServiceOrderNotesUser,
		   ISNULL(LN.note,'') ServiceOrderLabNotes,
		   CONVERT(VARCHAR(10), LN.dateRegistration, 103) + ' '  + convert(VARCHAR(5), LN.dateRegistration, 14) ServiceOrderLabNotesDate,
		   ISNULL(LN.nome,'') ServiceOrderLabNotesUser,
		   S.typeOfService ServiceOrderTypeOfService,
		   CONVERT(VARCHAR(10), S.dateAssigned, 103) + ' '  + convert(VARCHAR(5), S.dateAssigned, 14) ServiceOrderDateAssigned,
		   ISNULL(UA.nome,'') ServiceOrderUserAssigned,
           C.name ServiceOrderCustomer,
           ISNULL(C.cpfCnpj,'') ServiceOrderCustomerCPFCnpj,
           ISNULL(SE.faultFound,'') ServiceOrderHardError
	 FROM ServiceOrder(NOLOCK) S
     JOIN Customer(NOLOCK) C ON C.customer_id = S.customer_id
	 JOIN Locations(NOLOCK) LR ON LR.location_id = S.locationReceived_id
LEFT JOIN Locations(NOLOCK) CL ON CL.location_id = S.location_id
LEFT JOIN ServiceOrderQuoting(NOLOCK) SQ ON SQ.serviceOrder_id = S.serviceOrder_id
LEFT JOIN ServiceOrderEvaluation(NOLOCK) SE ON SE.serviceOrder_id = S.serviceOrder_id
LEFT JOIN ServiceOrderCloud SOC ON ISNULL(SOC.serviceOrder_id,0) = S.serviceOrder_id
LEFT JOIN Usuario UA ON UA.id_usuario = S.userAssigned_id
OUTER APPLY
(
	SELECT TOP 1 N.description, N.date, UN.nome
	  FROM ServiceOrderNotes SN
	  JOIN Notes N ON N.note_id = SN.note_id AND N.typenote_id IN(3,4)
	  JOIN Usuario UN ON UN.id_usuario = N.user_id
     WHERE SN.serviceOrder_id = S.serviceOrder_id
  ORDER BY SN.serviceOrderNotes_id DESC
)SN
OUTER APPLY
(
SELECT TOP 1 LN.note, LN.dateRegistration, UL.nome
	  FROM LabNotes LN
	  JOIN Usuario UL ON UL.id_usuario = LN.userRegistration_id
     WHERE LN.serviceOrder_id = S.serviceOrder_id
  ORDER BY LN.labNote_id DESC
)LN
WHERE ( /* S.location_id IN(" + inLocations + @") OR */ S.locationReceived_id IN(" + inLocations + @")
) ";
         #endregion

         #region Filter By Status
         if (!String.IsNullOrEmpty(filterByStatus))
         {
            sSql += @" AND S.status = '" + filterByStatus + @"' ";
         }
         if (LayoutFilter == "Received")
         {
            sSql += @" AND S.status != 'Incoming' ";
         }
         #endregion

         #region Filter By Customer
         if (!String.IsNullOrEmpty(filterByCustomer))
         {
            sSql += @" AND C.name LIKE'%" + filterByCustomer + @"%' ";
         }
         #endregion

         #region Filter By Date From
         if (!String.IsNullOrEmpty(filterByDateFrom))
         {
            sSql += @" AND CONVERT(DATE, S.date, 103) >= CONVERT(DATE, '" + filterByDateFrom + @"', 103) ";
         }
         #endregion

         #region Filter By Date To
         if (!String.IsNullOrEmpty(filterByDateTo))
         {
            sSql += @" AND CONVERT(DATE, S.date, 103) <= CONVERT(DATE, '" + filterByDateTo + @"', 103) ";
         }
         #endregion

         #region Filter By Status Date From
         if (!String.IsNullOrEmpty(filterByStatusDateFrom))
         {
            sSql += @" AND CONVERT(DATE, S.statusDate, 103) >= CONVERT(DATE, '" + filterByStatusDateFrom + @"', 103) ";
         }
         #endregion

         #region Filter By Status Date To
         if (!String.IsNullOrEmpty(filterByStatusDateTo))
         {
            sSql += @" AND CONVERT(DATE, S.statusDate, 103) <= CONVERT(DATE, '" + filterByStatusDateTo + @"', 103) ";
         }
         #endregion

         #region Filter By Customer CPFCNPJ
         if (!String.IsNullOrEmpty(filterByCustomerCPFCNPJ))
         {
            sSql += @" AND C.cpfCnpj LIKE '%" + filterByCustomerCPFCNPJ + @"%' ";
         }
         #endregion

         #region Filter SubStatus
         if (!String.IsNullOrEmpty(filterByQuotingSubStatus))
         {
            sSql += @" AND S.subStatus = '" + filterByQuotingSubStatus + @"' ";
         }
         #endregion

         #region Filter By Type Of Service
         if (!String.IsNullOrEmpty(filterByTypeOfService))
         {
            sSql += @" AND S.typeOfService = '" + filterByTypeOfService + @"' ";
         }
         #endregion

         #region Filter by string filterByPlanoComprado
         if (!String.IsNullOrEmpty(filterByPlanoComprado))
         {
            sSql += @" AND SOC.idCloud = '" + filterByPlanoComprado + @"' ";
         }
         #endregion

         #region Filter by string filterByPlanoAdquirido
         if (!String.IsNullOrEmpty(filterByPlanoAdquirido))
         {
            sSql += @" AND SOC.idVencimentoCloud = '" + filterByPlanoAdquirido + @"' ";
         }
         #endregion

         #region Filter by string filterByLocalMicrocomputador
         if (!String.IsNullOrEmpty(filterByLocalMicrocomputador))
         {
            sSql += @" AND SOC.idLocalMicrocomputador = '" + filterByLocalMicrocomputador + @"' ";
         }
         #endregion

         #region Group By | Order By
         sSql += @" ORDER BY S.serviceOrder_id, S.date";
         #endregion

         #endregion

         #region Obter as OS de acordo com o SQL e Filtros
         var reportsOrder = Db.Database.SqlQuery<ReportsViewModel>(sSql).ToList();
         #endregion

         #region Obtém as Medias de acordo com as OS retornadas da Consulta

         if (reportsOrder == null)
            reportsOrder = new List<ReportsViewModel>();

         foreach (var order in reportsOrder)
         {
            #region Medias
            #region sSql Medias da OS

            string sSqlMediasOS = @"SELECT CONVERT(VARCHAR, SM.serviceOrderMedias_id) ServiceOrderMediaId,
	                                           CONVERT(VARCHAR, M.media_id) MediaId,
	                                           MN.name MediaMake,
	                                           M.model MediaModel,
	                                           MS.name MediaStatus,
	                                           M.conditionInformation MediaError
                                          FROM ServiceOrderMedias(NOLOCK) SM
                                          JOIN Media(NOLOCK) M ON M.media_id = SM.media_id
                                          JOIN MediaStatus(NOLOCK) MS ON MS.mediaStatus_id = M.mediaStatus_id
                                          JOIN Manufacturer(NOLOCK) MN ON MN.manufacturer_id = M.manufacturer_id
                                         WHERE SM.serviceOrder_id = " + order.ServiceOrderId + @"
                                        ORDER BY SM.serviceOrder_id, SM.serviceOrderMedias_id";

            #endregion

            var mediasReport = Db.Database.SqlQuery<MediasReportsViewModel>(sSqlMediasOS).ToList();
            if (mediasReport != null)
            {
                order.Medias = mediasReport;
            }
            #endregion

            #region Clouds

            #region sSql Clouds da OS

            string sSqlCloudsOS = @"     
                  SELECT SC.idServiceOrderCloud,
	                     CONVERT(varchar(10), SC.dtBloqueio, 103) dataVencimento,
		                 (CONVERT(VARCHAR,C.tamanho) + ' ' + C.unidade_tamanho)tamanhoCloud
                    FROM ServiceOrderCloud(NOLOCK) SC
                    JOIN Cloud (NOLOCK) C ON C.idCloud = SC.idCloud
	                JOIN VencimentosCloud (NOLOCK) VC ON VC.idCloud = C.idCloud
                   WHERE SC.serviceOrder_id = " + order.ServiceOrderId + @"
                GROUP BY SC.idServiceOrderCloud,
	                     CONVERT(varchar(10), SC.dtBloqueio, 103),
		                 (CONVERT(VARCHAR,C.tamanho) + ' ' + C.unidade_tamanho)
                ORDER BY CONVERT(varchar(10), SC.dtBloqueio, 103),
		                 (CONVERT(VARCHAR,C.tamanho) + ' ' + C.unidade_tamanho)";
            #endregion

            var cloudsReport = Db.Database.SqlQuery<ServiceOrderCloudsReportsViewModel>(sSqlCloudsOS).ToList();
            if (cloudsReport != null)
            {
                order.Clouds = cloudsReport;
            }
            #endregion

            
         }

         #endregion

         return reportsOrder;
      }

      public void saveStatusServiceOrder(decimal serviceOrderId, string status)
      {
         string sSql = "update ServiceOrder SET status = '" + status + "' where serviceOrder_id = '" + serviceOrderId + "' ";
         Db.Database.ExecuteSqlCommand(sSql);
      }


      public decimal InsertUpdateProcedure(ServiceOrderViewModel model)
      {
         string sSql = "EXEC [dbo].[SP_INS_UP_SERVICEORDER] ";
         sSql += model.serviceOrder_id + ",";
         sSql += "'" + model.user_id + "',";
         sSql += model.customer_id + ",";
         sSql += "'" + (!String.IsNullOrEmpty(model.referredBy) ? model.referredBy : "") + "',";
         sSql += model.dateAssigned.HasValue ? "'" + model.dateAssigned.Value.ToString("dd/MM/yyyy HH:mm:ss") + "'," : "NULL,";
         sSql += (!String.IsNullOrEmpty(model.userAssigned_id) ? ("'" + model.userAssigned_id + "'") : "NULL") + ",";
         sSql += "'" + (!String.IsNullOrEmpty(model.status) ? model.status : "") + "',";
         sSql += "'" + (!String.IsNullOrEmpty(model.extensionStatus) ? model.extensionStatus : "") + "',";
         sSql += "'" + (!String.IsNullOrEmpty(model.takenBy) ? model.takenBy : "") + "',";
         sSql += "'" + (!String.IsNullOrEmpty(model.CSR) ? model.CSR : "") + "',";
         sSql += "'" + (!String.IsNullOrEmpty(model.typeOfService) ? model.typeOfService : "") + "',";
         sSql += "'" + (!String.IsNullOrEmpty(model.serviceToExecute) ? model.serviceToExecute : "") + "',";
         sSql += model.estimate + ",";
         sSql += model.locationReceived_id + ",";
         sSql += (model.location_id.HasValue ? model.location_id.Value.ToString() : "NULL") + ",";
         sSql += "'" + (!String.IsNullOrEmpty(model.arrivedBy) ? model.arrivedBy : "") + "',";
         sSql += "'" + (!String.IsNullOrEmpty(model.wayBillNumber) ? model.wayBillNumber : "") + "',";
         sSql += "'" + (!String.IsNullOrEmpty(model.packageCondidition) ? model.packageCondidition : "") + "',";
         sSql += "'" + (!String.IsNullOrEmpty(model.smartNumber) ? model.smartNumber : "") + "',";
         sSql += "'" + (!String.IsNullOrEmpty(model.techsName) ? model.techsName : "") + "',";
         sSql += (model.id_old.HasValue ? model.id_old.Value.ToString() : "NULL") + ",";
         sSql += "'" + (!String.IsNullOrEmpty(model.originOfServiceOrder) ? model.originOfServiceOrder : "") + "',";
         sSql += "'" + (!String.IsNullOrEmpty(model.mostImportantFilesToRecovery) ? model.mostImportantFilesToRecovery : "") + "',";
         sSql += "'" + (!String.IsNullOrEmpty(model.note) ? model.note : "") + "',";
         sSql += (model.customerContact_id.HasValue ? model.customerContact_id.Value.ToString() : "NULL") + ",";
         sSql += "'" + (!String.IsNullOrEmpty(model.customerContactName) ? model.customerContactName : "") + "',";
         sSql += "'" + (!String.IsNullOrEmpty(model.customerContactEmail) ? model.customerContactEmail : "") + "',";
         sSql += "'" + (!String.IsNullOrEmpty(model.customerContactTelephone) ? model.customerContactTelephone : "") + "',";
         sSql += "'" + (!String.IsNullOrEmpty(model.customerContactMobile) ? model.customerContactMobile : "") + "',";
         sSql += model.approvalDate.HasValue ? "'" + model.approvalDate.Value.ToString("dd/MM/yyyy HH:mm:ss") + "'," : "NULL,";
         sSql += "'" + (!String.IsNullOrEmpty(model.subStatus) ? model.subStatus : "") + "'";

         return Db.Database.SqlQuery<decimal>(sSql).FirstOrDefault();
      }


      public void SaveInTransferLocationId(decimal serviceOrderId, int locationId, string TransferNotes)
      {
         string sSql = "update ServiceOrder SET location_id = '" + locationId + "' where serviceOrder_id = '" + serviceOrderId + "' ";
         Db.Database.ExecuteSqlCommand(sSql);
      }

      public void SaveCodigoRastreio(string serviceOrderId, string codigoRastreio)
      {
         string sSql = "update ServiceOrder SET codigoRastreio = '" + codigoRastreio + "' where serviceOrder_id = '" + serviceOrderId + "' ";
         Db.Database.ExecuteSqlCommand(sSql);

         sSql = "";
         sSql += "INSERT INTO Notes (description,date,typenote_id,user_id,id_old) VALUES('" + codigoRastreio + "',getdate(),4,'d0c8b995-aeb5-4b32-a441-2c9717da65e1'," + serviceOrderId + "); ";
         Db.Database.ExecuteSqlCommand(sSql);

         sSql = "";
         sSql += "INSERT INTO ServiceOrderNotes (note_id,serviceOrder_id,dateRegistration,userRegistration_id,id_old,serviceOrderStatus) ";
         sSql += "VALUES ";
         sSql += "((select note_id from Notes where user_id = 'd0c8b995-aeb5-4b32-a441-2c9717da65e1' and description = '" + codigoRastreio + "' and id_old = " + serviceOrderId + ") ";
         sSql += "," + serviceOrderId + ",getdate(),'d0c8b995-aeb5-4b32-a441-2c9717da65e1',null," + serviceOrderId + ") ";
         Db.Database.ExecuteSqlCommand(sSql);



      }

      public void SaveUrlUpload(string serviceOrderId, string urlUploadContrato)
      {
         string sSql = "update ServiceOrder SET urlUploadContrato = '" + urlUploadContrato + "' where serviceOrder_id = '" + serviceOrderId + "' ";
         Db.Database.ExecuteSqlCommand(sSql);
      }


      public IEnumerable<DetailServiceOrderViewModel> selectFromDetailPortal(string serviceOrderId)
      {
         string sSql = @" select convert(varchar(300),so.serviceOrder_id) ServiceOrderId
,convert(varchar(300),l.OS_Series) OS_Series 
,convert(varchar(300),so.status) ServiceOrderStatus
,convert(varchar(300),so.subStatus) ServiceOrderSubStatus

, case 
when so.status = 'Incoming' and isnull(so.codigoRastreio,'') = '' then 'Aguardando envio de mídia' 
when so.status = 'Incoming' and isnull(so.codigoRastreio,'') <> '' then 'Mídia Enviada: '  + so.codigoRastreio
when so.status = 'Pending' then 'Mídia recebida, Aguardando Orçamento'
when so.status = 'Quoted' and isnull(so.urlUploadContrato,'') = '' then 'Orçamento Disponibilizado'
when so.status = 'Quoted' and isnull(so.dtaAprovacaoContrato,'') = '' then 'Aguardando Confirmação de Contrato'
when so.status = 'Quoted' and isnull(so.subStatus,'') = 'Investiment' then 'Aguardando Investimento Financeiro'
when so.status = 'Quoted' then 'Serviço Aprovado para Execução'
when so.status = 'Go Ahead' and isnull(so.subStatus,'') = '' then 'Serviço em Execução'
when so.status = 'Recovered' and isnull(sob.datePaid,'') <> '' and  isnull(so.subStatus,'') <> 'Project Completed' then 'Pagamento Recebido' 
when so.status = 'Recovered' and isnull(so.subStatus,'') = '' and isnull(idPagamentoPagSeguro,'') = '' then 'Serviço sendo finalizado, Aguardando Pagamento'
when so.status = 'Recovered' and isnull(so.subStatus,'') = 'Waiting Destination' and isnull(idPagamentoPagSeguro,'') = '' then 'Serviço sendo finalizado, Aguardando Mídia Destino, Aguardando Pagamento'
when so.status = 'Recovered' and isnull(so.subStatus,'') = '' and isnull(idPagamentoPagSeguro,'') <> '' then 'Serviço sendo finalizado, Código Transação: ' + so.idPagamentoPagSeguro
when so.status = 'Recovered' and isnull(so.subStatus,'') = 'Waiting Destination' then 'Serviço sendo finalizado, Aguardando Mídia Destino'
when so.status = 'Recovered' and isnull(so.subStatus,'') = 'Project Completed' then 'Serviço Finalizado'
when so.status = 'Unrecovered' then 'Serviço Efetuado, Sem Recuperação'
when so.status = 'Closed' then 'O.S. Encerrada'
else sos.nameToClient end ServiceOrderStatusClient
,convert(varchar(300),sos.nameToClient) ServiceOrderStatusCliente
,convert(varchar(300),so.serviceToExecute) TypeOfService
,convert(varchar(300),c.name) CustomerName
,convert(varchar(300),c.email) CustomerEmail
,convert(varchar(300),co.name) CustomerContactName
,convert(varchar(300),co.mobile) CustomerContactPhone
,convert(varchar(300),co.email) CustomerContactEmail
,convert(varchar(300),so.serviceToExecute) ServiceToExecute
,convert(varchar(300),mnf.name) MarcaMedia
,convert(varchar(300),m.model) ModeloMedia
,convert(varchar(300),m.serial_no) SerieMedia
,convert(varchar(300),so.codigoRastreio) codigoRastreio
,convert(varchar(300),so.urlUploadContrato) urlUploadContrato
,convert(varchar(300),so.dtaAprovacaoContrato,103) dtaAprovacaoContrato
,CONVERT(varchar(300), convert(decimal(10,2),(convert(decimal(10,2),isnull(soq.quotedAmount,0.00))+(select isnull(sum(convert(decimal(10,2),Amount)*convert(decimal(10,2),SalePrice)),0.00) from EquipmentsSell where serviceOrder_id = so.serviceOrder_id)))) valorOrcamento
,CONVERT(varchar(300), isnull(soq.quotedAmount,0.00)) valorOrcamentoOriginal
,replace(isnull((select  'Amount(' + Amount+') Make(' + Make+') Model('+ model+') SalePrice('+ salePrice+')[br]' from  EquipmentsSell where serviceOrder_id = so.serviceOrder_id FOR XML PATH ('')),''),'[br]','<br>') equipamentosAdiquiridos
,convert(varchar(300),so.idPagamentoPagSeguro) idPagamentoPagSeguro
,case sos.name when 'Incoming' then 0 when 'Go Ahead' then 1 when 'Pending' then 2 when 'Quoted' then 3 when 'Recovered' then 4 when 'Waiting Destination' then 5 else 99 end ServiceOrderStatusNumero
,isnull(convert(varchar(300), cur.currency),'BRL') currency
,isnull(convert(varchar(20),sob.datePaid,103),'') datePagamento
,isnull(convert(varchar(20),soq.dueDate,103),'') dateVencimentoPagamento
,isnull(convert(varchar(300),sob.paymentMethod),'') metodoPagamento
,isnull(convert(varchar(300),sob.freight),'') frete
,LEN(replace(replace(replace(replace(cpfCnpj,'-',''),'.',''),',',''),'\','')) as qtdCpfCnpj
,'https://system.cbltech.com.br/'+isnull(l.foto,'') as foto
from ServiceOrder so
left outer join Locations l on l.location_id = so.locationReceived_id
left outer join ServiceOrderStatus sos on sos.name = so.status
left outer join Customer c on c.customer_id = so.customer_id
left outer join CustomerContact cc on cc.customer_id = so.customer_id and cc.customerContact_id = (select min(customerContact_id) from CustomerContact where customer_id = so.customer_id)
left outer join Contact co on co.contact_id = cc.contact_id
left outer join ServiceOrderMedias som on som.serviceOrder_id = so.serviceOrder_id
left outer join media m on m.media_id = som.media_id
left outer join Manufacturer mnf on mnf.manufacturer_id = m.manufacturer_id
left outer join ServiceOrderQuoting soq on soq.serviceOrder_id = so.serviceOrder_id
left outer join Currency cur on cur.initials = soq.currency
left outer join ServiceOrderBilling sob on so.serviceOrder_id = sob.serviceOrder_id
where so.serviceOrder_id = " + serviceOrderId + " ";
         return Db.Database.SqlQuery<DetailServiceOrderViewModel>(sSql).ToList();
      }


      public IEnumerable<EquipmentsSellViewModel> selectFromEquipmentsSell(string usuariologado, string serviceOrderId)
      {
         string sSql = "";

         sSql = @"select isnull(es.equipament_id,0) equipament_id, isnull(es.amount,'0') amount,
m.media_id media_id, convert(varchar,m.make) make, convert(varchar,m.model) model, convert(varchar,m.size) size, convert(varchar,m.saleprice) saleprice  
from media m
left outer join EquipmentsSell es on m.media_id = es.media_id and m.saleprice = es.saleprice and es.serviceOrder_id = " + serviceOrderId + @"
where m.saleprice > 0 
and ((indFisico = 1  and (select count(1) fisico from Customer where customer_id = '" + usuariologado + @"' and LEN(cpfCnpj) <= 11 )>=1) 
or (indjuridico = 1  and (select count(1) juridico from Customer where customer_id = '" + usuariologado + @"' and LEN(cpfCnpj) > 11 )>=1) 
or (indparceiro = 1  and (select count(1) parceiro from AgentContacts where agentContact_id = '" + usuariologado + @"' )>=1) 
) union 
select isnull(es.equipament_id,0) equipament_id, isnull(es.amount,'0') amount,
es.media_id media_id, convert(varchar,es.make) make, convert(varchar,es.model) model, convert(varchar,es.size) size, convert(varchar,es.saleprice) saleprice  
from EquipmentsSell es 
where  es.serviceOrder_id = " + serviceOrderId + @" 
order by media_id, make";


         return Db.Database.SqlQuery<EquipmentsSellViewModel>(sSql).ToList();
      }




      public Decimal selectTotalOrcamento(string serviceOrderId)
      {
         string sSql = "";

         sSql = @"select convert(decimal(10,2),sum(valorOrcamento)) from (select CONVERT(varchar(300), soq.quotedAmount) valorOrcamento
from ServiceOrder so
left outer join ServiceOrderQuoting soq on soq.serviceOrder_id = so.serviceOrder_id
where so.serviceOrder_id = " + serviceOrderId + @"
union all
select convert(decimal(10,2),Amount)*convert(decimal(10,2),SalePrice) from EquipmentsSell
where serviceOrder_id = " + serviceOrderId + @") as tb";


         return Db.Database.SqlQuery<Decimal>(sSql).FirstOrDefault();
      }


      public List<LocationsViewModel> ListLocation(string location_id)
      {
         string sSql = "";

         sSql = @"select l.*
from locations l
inner join city c on l.city_id = c.city_id
where state_id in (select state_id from locations l
inner join city c on l.city_id = c.city_id
where location_id = " + location_id + ") and location_id not in (" + location_id + ")";



         return Db.Database.SqlQuery<LocationsViewModel>(sSql).ToList();
      }


   }
}

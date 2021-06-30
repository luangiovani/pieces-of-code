using Framework.Database.Entity.CBL;
using Framework.Domain.Model.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Repository.CBL
{
    public class StockRepository : RepositoryBase<Stock>
    {
        public IEnumerable<StockViewModel> getAllFromView(List<int> locations, string dateF, string dateT, string typeMovement, string material, int locationId = 0,
            string mediaStatusFilter = "", string mediaManufacturerFilter = "", string mediaSerialNoFilter = "", string mediaPartNoFilter = "", string mediaRevisionNoFilter = "",
            string mediaFirmwareFilter = "", string mediaSizeFilter = "", string mediaInterfaceFilter = "", string mediaPCBIdFilter = "", string mediaPCBFilter = "",
            string mediaMCLFilter = "", string mediaOEMFilter = "", string mediaHDAFilter = "", string mediaSeriesFilter = "", string mediaMadeInFilter = "",
            string mediaDCMSiteFilter = "", string mediaUpLevelFilter = "", string mediaAllFilter = "", string numberOS = "", string media_id = "")
        {
            #region Consulta SQL
            string sSql = @"Select distinct S.stock_id,
	   S.component_id,
	   S.media_id,
	   S.dateOfMovement,
	   S.dateRegistration,
	   S.lastUpdateDate,
	   S.lastUpdateUser_id,
	   S.location_id,
	   CASE WHEN M.media_id > 0 AND LEN(M.model) > 0 THEN M.model ELSE S.material END material,
	   S.quantity,
	   S.typeOfMovement,
	   S.serviceOrder_id,
	   MM.compatibility,
	   CASE WHEN S.serviceOrder_id > 0 THEN
		CASE WHEN SO.id_old IS NULL THEN
			L.OS_Series
		ELSE
			''
		END
	   ELSE
		''
	   END AS OS_Series,
	   ISNULL(MN.name,'') MediaManufacturer,
	   ISNULL(LM.name,'') MediaLocation,
	   ISNULL(MS.name,'') MediaStatus,
	   ISNULL(M.serial_no,'') MediaSerial,
	   ISNULL(M.part_no,'') MediaPartNo,
	   ISNULL(M.revision_no,'') MediaRevisionNo,
	   ISNULL(M.firmware_no,'') MediaFirmwareNo,
	   ISNULL(M.size,'') MediaSize,
	   ISNULL(M.interfaceType,'') MediaInterface,
	   ISNULL(M.pcb_id,'') MediaPCBId,
	   ISNULL(M.pcb,'') MediaPCB,
	   ISNULL(M.mlc_no,'') MediaMLCNo,
	   CASE WHEN M.mfgDate IS NULL THEN '' ELSE CONVERT(VARCHAR, M.mfgDate, 103) END MediaMFGDate,
	   ISNULL(M.oem_no,'')  MediaOemNo,
	   ISNULL(M.upLevel_no,'')  MediaUpLevelNo,
	   ISNULL(M.series,'')  MediaSeries,
	   ISNULL(M.condition,'')  MediaCondition,
	   ISNULL(M.conditionInformation,'')  MediaConditionInfo,
	   ISNULL(M.dcmSite_no,'')  MediaDCMSiteNo,
	   ISNULL(M.hda,'')  MediaHDA,
	   ISNULL(MM.compatibility,'')  MediaCompatibility,
	   ISNULL(M.madeIN,'')  MediaMadeIn
  FROM Stock S
LEFT JOIN Media M ON M.media_id = S.media_id
LEFT JOIN Manufacturer MN ON MN.manufacturer_id = M.manufacturer_id
LEFT JOIN MediaModels MM ON MM.model = M.model
LEFT JOIN Locations LM ON LM.location_id = M.location_id
LEFT JOIN MediaStatus MS ON MS.mediaStatus_id = M.mediaStatus_id
LEFT JOIN ServiceOrder SO ON SO.serviceOrder_Id = S.serviceOrder_Id
LEFT JOIN Locations L ON ISNULL(L.location_id,SO.locationReceived_id) = SO.location_id
WHERE 1=1";

            #region Monta Where

            #region Locations
            string inLocations = "";

            foreach (var location_Id in locations)
            {
                if (inLocations == "")
                {
                    inLocations = location_Id.ToString();
                }
                else
                {
                    inLocations += "," + location_Id.ToString();
                }
            }

            #region Filter By Specific Location
            if (locationId > 0)
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

            if (!String.IsNullOrEmpty(inLocations))
            {
                sSql += @" AND S.location_id IN(" + inLocations + @")";
            }
            #endregion

            #region Filter By Date From
            if (!String.IsNullOrEmpty(dateF))
            {
                sSql += @" AND CONVERT(DATE, S.dateOfMovement, 103) >= CONVERT(DATE, '" + dateF + @"', 103) ";
            }
            #endregion

            #region Filter By Date To
            if (!String.IsNullOrEmpty(dateT))
            {
                sSql += @" AND CONVERT(DATE, S.dateOfMovement, 103) <= CONVERT(DATE, '" + dateT + @"', 103) ";
            }
            #endregion

            #region Filter BY Type Of Movement
            if (!String.IsNullOrEmpty(typeMovement))
            {
                sSql += @" AND S.typeOfMovement = '" + dateT + @"' ";
            }
            #endregion

            #region Filter By Material
            if (!String.IsNullOrEmpty(material))
            {
                sSql += @" AND S.material = '" + material + @"' ";
            }
            #endregion

            #region Filter By Media Status
            if (!String.IsNullOrEmpty(mediaStatusFilter) && !String.IsNullOrWhiteSpace(mediaStatusFilter))
            {
                sSql += @" AND M.mediaStatus_id = " + mediaStatusFilter + @" ";
            }
            #endregion

            #region Filter By Media Manufacturer
            if (!String.IsNullOrEmpty(mediaManufacturerFilter) && !String.IsNullOrWhiteSpace(mediaManufacturerFilter))
            {
                sSql += @" AND M.manufacturer_id = " + mediaManufacturerFilter + @" ";
            }
            #endregion

            #region Filter By Media Serial #
            if (!String.IsNullOrEmpty(mediaSerialNoFilter) && !String.IsNullOrWhiteSpace(mediaSerialNoFilter))
            {
                sSql += @" AND ISNULL(M.serial_no,'') LIKE '%" + mediaSerialNoFilter + @"%' ";
            }
            #endregion

            #region Filter By Media Part #
            if (!String.IsNullOrEmpty(mediaPartNoFilter) && !String.IsNullOrWhiteSpace(mediaPartNoFilter))
            {
                sSql += @" AND ISNULL(M.part_no,'') LIKE '%" + mediaPartNoFilter + @"%' ";
            }
            #endregion

            #region Filter By Media Revision #
            if (!String.IsNullOrEmpty(mediaRevisionNoFilter) && !String.IsNullOrWhiteSpace(mediaRevisionNoFilter))
            {
                sSql += @" AND ISNULL(M.revision_no,'') LIKE '%" + mediaRevisionNoFilter + @"%' ";
            }
            #endregion

            #region Filter By Media Firmware #
            if (!String.IsNullOrEmpty(mediaFirmwareFilter) && !String.IsNullOrWhiteSpace(mediaFirmwareFilter))
            {
                sSql += @" AND ISNULL(M.firmware_no,'') LIKE '%" + mediaFirmwareFilter + @"%' ";
            }
            #endregion

            #region Filter By Media Size
            if (!String.IsNullOrEmpty(mediaSizeFilter) && !String.IsNullOrWhiteSpace(mediaSizeFilter))
            {
                sSql += @" AND ISNULL(M.size,'') LIKE '%" + mediaSizeFilter + @"%' ";
            }
            #endregion
            #region Filter By media_id
            if (!String.IsNullOrEmpty(media_id) && !String.IsNullOrWhiteSpace(media_id))
            {
                sSql += @" AND ISNULL(M.media_id,'') LIKE '" + media_id + @"' ";
            }
            #endregion
            

            #region Filter By Media Interface
            if (!String.IsNullOrEmpty(mediaInterfaceFilter) && !String.IsNullOrWhiteSpace(mediaInterfaceFilter))
            {
                sSql += @" AND ISNULL(M.interfaceType,'') LIKE '%" + mediaInterfaceFilter + @"%' ";
            }
            #endregion

            #region Filter By Media PCB Id
            if (!String.IsNullOrEmpty(mediaPCBIdFilter) && !String.IsNullOrWhiteSpace(mediaPCBIdFilter))
            {
                sSql += @" AND ISNULL(M.pcb_id,'') LIKE '%" + mediaPCBIdFilter + @"%' ";
            }
            #endregion

            #region Filter By Media PCB
            if (!String.IsNullOrEmpty(mediaPCBFilter) && !String.IsNullOrWhiteSpace(mediaPCBFilter))
            {
                sSql += @" AND ISNULL(M.pcb,'') LIKE '%" + mediaPCBFilter + @"%' ";
            }
            #endregion

            #region Filter By Media MLC
            if (!String.IsNullOrEmpty(mediaMCLFilter) && !String.IsNullOrWhiteSpace(mediaMCLFilter))
            {
                sSql += @" AND ISNULL(M.mlc_no,'') LIKE '%" + mediaMCLFilter + @"%' ";
            }
            #endregion

            #region Filter By Media OEM
            if (!String.IsNullOrEmpty(mediaOEMFilter) && !String.IsNullOrWhiteSpace(mediaOEMFilter))
            {
                sSql += @" AND ISNULL(M.oem_no,'') LIKE '%" + mediaOEMFilter + @"%' ";
            }
            #endregion

            #region Filter By Media HDA
            if (!String.IsNullOrEmpty(mediaHDAFilter) && !String.IsNullOrWhiteSpace(mediaHDAFilter))
            {
                sSql += @" AND ISNULL(M.hda,'') LIKE '%" + mediaHDAFilter + @"%' ";
            }
            #endregion

            #region Filter By Media Series
            if (!String.IsNullOrEmpty(mediaSeriesFilter) && !String.IsNullOrWhiteSpace(mediaSeriesFilter))
            {
                sSql += @" AND ISNULL(M.series,'') LIKE '%" + mediaSeriesFilter + @"%' ";
            }
            #endregion

            #region Filter By Media MadeIN
            if (!String.IsNullOrEmpty(mediaMadeInFilter) && !String.IsNullOrWhiteSpace(mediaMadeInFilter))
            {
                sSql += @" AND ISNULL(M.madeIN,'') LIKE '%" + mediaMadeInFilter + @"%' ";
            }
            #endregion

            #region Filter By Media DCM Site
            if (!String.IsNullOrEmpty(mediaDCMSiteFilter) && !String.IsNullOrWhiteSpace(mediaDCMSiteFilter))
            {
                sSql += @" AND ISNULL(M.dcmSite_no,'') LIKE '%" + mediaDCMSiteFilter + @"%' ";
            }
            #endregion

            #region Filter By Media UpLevel
            if (!String.IsNullOrEmpty(mediaUpLevelFilter) && !String.IsNullOrWhiteSpace(mediaUpLevelFilter))
            {
                sSql += @" AND ISNULL(M.upLevel_no,'') LIKE '%" + mediaUpLevelFilter + @"%' ";
            }
            #endregion

            #region Filter By Media All Fields
            if (!String.IsNullOrEmpty(mediaAllFilter) && !String.IsNullOrWhiteSpace(mediaAllFilter))
            {
                sSql += @" AND (
                ISNULL(MN.name,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(LM.name,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MS.name,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.serial_no,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.part_no,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.revision_no,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.firmware_no,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.size,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.interfaceType,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.pcb_id,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.pcb,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.mlc_no,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.oem_no,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.upLevel_no,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.series,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.condition,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.conditionInformation,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.dcmSite_no,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.hda,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MM.compatibility,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(M.madeIN,'')  LIKE '%" + mediaAllFilter + @"%' OR
                ISNULL(M.model,'')  LIKE '%" + mediaAllFilter + @"%')";
            }
            #endregion

            #region Filter By numberOS
            if (!String.IsNullOrEmpty(numberOS))
            {
                sSql += @" AND SO.serviceOrder_Id = '" + numberOS + @"' ";
            }
            #endregion

            #endregion
            #endregion

            return Db.Database.SqlQuery<StockViewModel>(sSql).ToList();
        }

        public IEnumerable<StockViewModel> getAllFromView_VW_MediasInStock(List<int> locations, string dateF, string dateT, string typeMovement, string material, int locationId = 0,
            string mediaStatusFilter = "", string mediaConditionFilter = "", string mediaManufacturerFilter = "", string mediaSerialNoFilter = "", string mediaPartNoFilter = "", string mediaRevisionNoFilter = "",
            string mediaFirmwareFilter = "", string mediaSizeFilter = "", string mediaInterfaceFilter = "", string mediaPCBIdFilter = "", string mediaPCBFilter = "",
            string mediaMCLFilter = "", string mediaOEMFilter = "", string mediaHDAFilter = "", string mediaSeriesFilter = "", string mediaMadeInFilter = "",
            string mediaDCMSiteFilter = "", string mediaUpLevelFilter = "", string mediaAllFilter = "", string numberOS = "", string media_id = "", string stock_id = "")
        {
            #region Consulta SQL
            string sSql = @"Select * from VW_MediasInStock WHERE 1=1";
            #endregion

            #region Monta Where

            #region Locations
            string inLocations = "";

            foreach (var location_Id in locations)
            {
                if (inLocations == "")
                {
                    inLocations = location_Id.ToString();
                }
                else
                {
                    inLocations += "," + location_Id.ToString();
                }
            }

            #region Filter By Specific Location
            if (locationId > 0)
            {
                inLocations = locationId.ToString();
            }
            #endregion

            if (!String.IsNullOrEmpty(inLocations))
            {
                sSql += @" AND location_id IN(" + inLocations + @")";
            }
            #endregion

            #region Se informado a Media ou Número da O.S., não aplica os demais filtros

            #region Filter By media_id
            if (!String.IsNullOrEmpty(media_id) && !String.IsNullOrWhiteSpace(media_id))
            {
                sSql += @" AND ISNULL(media_id,'') = '" + media_id + @"' ";
            }
            #endregion

            #region Filter By stock_id
            if (!String.IsNullOrEmpty(stock_id) && !String.IsNullOrWhiteSpace(stock_id))
            {
                sSql += @" AND ISNULL(stock_id,'') = '" + stock_id + @"' ";
            }
            #endregion

            #region Filter By numberOS
            else if (!String.IsNullOrEmpty(numberOS))
            {
                sSql += @" AND serviceOrder_Id = '" + numberOS + @"' ";
            }
            #endregion

            #endregion

            #region Demais Filtros, não informado media ou Número da O.S.
            else
            {
                #region Filter By Date From
                if (!String.IsNullOrEmpty(dateF))
                {
                    sSql += @" AND CONVERT(DATE, dateOfMovement, 103) >= CONVERT(DATE, '" + dateF + @"', 103) ";
                }
                #endregion

                #region Filter By Date To
                if (!String.IsNullOrEmpty(dateT))
                {
                    sSql += @" AND CONVERT(DATE, dateOfMovement, 103) <= CONVERT(DATE, '" + dateT + @"', 103) ";
                }
                #endregion

                #region Filter BY Type Of Movement
                if (!String.IsNullOrEmpty(typeMovement))
                {
                    sSql += @" AND typeOfMovement = '" + dateT + @"' ";
                }
                #endregion

                #region Filter By Material
                if (!String.IsNullOrEmpty(material))
                {
                    sSql += @" AND material = '" + material + @"' ";
                }
                #endregion

                #region Filter By Media Status
                if (!String.IsNullOrEmpty(mediaStatusFilter) && !String.IsNullOrWhiteSpace(mediaStatusFilter))
                {
                    sSql += @" AND mediaStatus_id = " + mediaStatusFilter + @" ";
                }
                #endregion

                #region Filter By Media Condition
                if (!String.IsNullOrEmpty(mediaConditionFilter) && !String.IsNullOrWhiteSpace(mediaConditionFilter))
                {
                    sSql += @" AND MediaCondition = '" + mediaStatusFilter + @"' ";
                }
                #endregion

                #region Filter By Media Manufacturer
                if (!String.IsNullOrEmpty(mediaManufacturerFilter) && !String.IsNullOrWhiteSpace(mediaManufacturerFilter))
                {
                    sSql += @" AND manufacturer_id = " + mediaManufacturerFilter + @" ";
                }
                #endregion

                #region Filter By Media Serial #
                if (!String.IsNullOrEmpty(mediaSerialNoFilter) && !String.IsNullOrWhiteSpace(mediaSerialNoFilter))
                {
                    sSql += @" AND ISNULL(MediaSerial,'') LIKE '%" + mediaSerialNoFilter + @"%' ";
                }
                #endregion

                #region Filter By Media Part #
                if (!String.IsNullOrEmpty(mediaPartNoFilter) && !String.IsNullOrWhiteSpace(mediaPartNoFilter))
                {
                    sSql += @" AND ISNULL(MediaPartNo,'') LIKE '%" + mediaPartNoFilter + @"%' ";
                }
                #endregion

                #region Filter By Media Revision #
                if (!String.IsNullOrEmpty(mediaRevisionNoFilter) && !String.IsNullOrWhiteSpace(mediaRevisionNoFilter))
                {
                    sSql += @" AND ISNULL(MediaRevisionNo,'') LIKE '%" + mediaRevisionNoFilter + @"%' ";
                }
                #endregion

                #region Filter By Media Firmware #
                if (!String.IsNullOrEmpty(mediaFirmwareFilter) && !String.IsNullOrWhiteSpace(mediaFirmwareFilter))
                {
                    sSql += @" AND ISNULL(MediaFirmwareNo,'') LIKE '%" + mediaFirmwareFilter + @"%' ";
                }
                #endregion

                #region Filter By Media Size
                if (!String.IsNullOrEmpty(mediaSizeFilter) && !String.IsNullOrWhiteSpace(mediaSizeFilter))
                {
                    sSql += @" AND ISNULL(MediaSize,'') LIKE '%" + mediaSizeFilter + @"%' ";
                }
                #endregion

                #region Filter By Media Interface
                if (!String.IsNullOrEmpty(mediaInterfaceFilter) && !String.IsNullOrWhiteSpace(mediaInterfaceFilter))
                {
                    sSql += @" AND ISNULL(MediaInterface,'') LIKE '%" + mediaInterfaceFilter + @"%' ";
                }
                #endregion

                #region Filter By Media PCB Id
                if (!String.IsNullOrEmpty(mediaPCBIdFilter) && !String.IsNullOrWhiteSpace(mediaPCBIdFilter))
                {
                    sSql += @" AND ISNULL(MediaPCBId,'') LIKE '%" + mediaPCBIdFilter + @"%' ";
                }
                #endregion

                #region Filter By Media PCB
                if (!String.IsNullOrEmpty(mediaPCBFilter) && !String.IsNullOrWhiteSpace(mediaPCBFilter))
                {
                    sSql += @" AND ISNULL(MediaPCB,'') LIKE '%" + mediaPCBFilter + @"%' ";
                }
                #endregion

                #region Filter By Media MLC
                if (!String.IsNullOrEmpty(mediaMCLFilter) && !String.IsNullOrWhiteSpace(mediaMCLFilter))
                {
                    sSql += @" AND ISNULL(MediaMLCNo,'') LIKE '%" + mediaMCLFilter + @"%' ";
                }
                #endregion

                #region Filter By Media OEM
                if (!String.IsNullOrEmpty(mediaOEMFilter) && !String.IsNullOrWhiteSpace(mediaOEMFilter))
                {
                    sSql += @" AND ISNULL(MediaOemNo,'') LIKE '%" + mediaOEMFilter + @"%' ";
                }
                #endregion

                #region Filter By Media HDA
                if (!String.IsNullOrEmpty(mediaHDAFilter) && !String.IsNullOrWhiteSpace(mediaHDAFilter))
                {
                    sSql += @" AND ISNULL(MediaHDA,'') LIKE '%" + mediaHDAFilter + @"%' ";
                }
                #endregion

                #region Filter By Media Series
                if (!String.IsNullOrEmpty(mediaSeriesFilter) && !String.IsNullOrWhiteSpace(mediaSeriesFilter))
                {
                    sSql += @" AND ISNULL(MediaSeries,'') LIKE '%" + mediaSeriesFilter + @"%' ";
                }
                #endregion

                #region Filter By Media MadeIN
                if (!String.IsNullOrEmpty(mediaMadeInFilter) && !String.IsNullOrWhiteSpace(mediaMadeInFilter))
                {
                    sSql += @" AND ISNULL(MediaMadeIn,'') LIKE '%" + mediaMadeInFilter + @"%' ";
                }
                #endregion

                #region Filter By Media DCM Site
                if (!String.IsNullOrEmpty(mediaDCMSiteFilter) && !String.IsNullOrWhiteSpace(mediaDCMSiteFilter))
                {
                    sSql += @" AND ISNULL(MediaDCMSiteNo,'') LIKE '%" + mediaDCMSiteFilter + @"%' ";
                }
                #endregion

                #region Filter By Media UpLevel
                if (!String.IsNullOrEmpty(mediaUpLevelFilter) && !String.IsNullOrWhiteSpace(mediaUpLevelFilter))
                {
                    sSql += @" AND ISNULL(MediaUpLevelNo,'') LIKE '%" + mediaUpLevelFilter + @"%' ";
                }
                #endregion

                #region Filter By Media All Fields
                if (!String.IsNullOrEmpty(mediaAllFilter) && !String.IsNullOrWhiteSpace(mediaAllFilter))
                {
                    sSql += @" AND (
                ISNULL(MediaManufacturer,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaLocation,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaStatus,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaSerial,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaPartNo,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaRevisionNo,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaFirmwareNo,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaSize,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaInterface,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaPCBId,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaPCB,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaMLCNo,'') LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaOemNo,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaUpLevelNo,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaSeries,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaCondition,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaConditionInfo,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaDCMSiteNo,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaHDA,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaCompatibility,'')  LIKE '%" + mediaAllFilter + @"%' OR
	            ISNULL(MediaMadeIn,'')  LIKE '%" + mediaAllFilter + @"%' OR
                ISNULL(model,'')  LIKE '%" + mediaAllFilter + @"%')";
                }
                #endregion
            }
            

            

            #endregion

            #endregion

            return Db.Database.SqlQuery<StockViewModel>(sSql).ToList();
        }

    }
}

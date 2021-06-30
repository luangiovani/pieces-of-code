using Framework.Database.Entity.CBL;
using Framework.Domain.Model.CBL;
using Framework.Domain.Repository.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Services.CBL
{
    public class StockService : ServiceBase<Stock, StockViewModel>
    {
        private readonly StockRepository _repository;

        public StockService(StockRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public ICollection<StockViewModel> getAllFromView(List<int> locations, string dateF, string dateT, string typeMovement, string material, int locationId = 0,
            string mediaStatusFilter = "", string mediaManufacturerFilter = "", string mediaSerialNoFilter = "", string mediaPartNoFilter = "", string mediaRevisionNoFilter = "",
            string mediaFirmwareFilter = "", string mediaSizeFilter = "", string mediaInterfaceFilter = "", string mediaPCBIdFilter = "", string mediaPCBFilter = "",
            string mediaMCLFilter = "", string mediaOEMFilter = "", string mediaHDAFilter = "", string mediaSeriesFilter = "", string mediaMadeInFilter = "",
            string mediaDCMSiteFilter = "", string mediaUpLevelFilter = "", string mediaAllFilter = "", string numberOS = "", string media_id = "")
        {
            return _repository.getAllFromView(locations, dateF, dateT, typeMovement, material, locationId,
                mediaStatusFilter,
                mediaManufacturerFilter,
                mediaSerialNoFilter,
                mediaPartNoFilter,
                mediaRevisionNoFilter,
                mediaFirmwareFilter,
                mediaSizeFilter,
                mediaInterfaceFilter,
                mediaPCBIdFilter,
                mediaPCBFilter,
                mediaMCLFilter,
                mediaOEMFilter,
                mediaHDAFilter,
                mediaSeriesFilter,
                mediaMadeInFilter,
                mediaDCMSiteFilter,
                mediaUpLevelFilter,
                mediaAllFilter, numberOS, media_id).ToList();
        }

        public ICollection<StockViewModel> getAllFromView_VW_MediasInStock(List<int> locations, string dateF, string dateT, string typeMovement, string material, int locationId = 0,
            string mediaStatusFilter = "", string mediaConditionFilter = "", string mediaManufacturerFilter = "", string mediaSerialNoFilter = "", string mediaPartNoFilter = "", string mediaRevisionNoFilter = "",
            string mediaFirmwareFilter = "", string mediaSizeFilter = "", string mediaInterfaceFilter = "", string mediaPCBIdFilter = "", string mediaPCBFilter = "",
            string mediaMCLFilter = "", string mediaOEMFilter = "", string mediaHDAFilter = "", string mediaSeriesFilter = "", string mediaMadeInFilter = "",
            string mediaDCMSiteFilter = "", string mediaUpLevelFilter = "", string mediaAllFilter = "", string numberOS = "", string media_id = "", string stock_id = "")
        {
            return _repository.getAllFromView_VW_MediasInStock(
                locations, 
                dateF, 
                dateT, 
                typeMovement, 
                material, 
                locationId,
                mediaStatusFilter,
                mediaConditionFilter,
                mediaManufacturerFilter,
                mediaSerialNoFilter,
                mediaPartNoFilter,
                mediaRevisionNoFilter,
                mediaFirmwareFilter,
                mediaSizeFilter,
                mediaInterfaceFilter,
                mediaPCBIdFilter,
                mediaPCBFilter,
                mediaMCLFilter,
                mediaOEMFilter,
                mediaHDAFilter,
                mediaSeriesFilter,
                mediaMadeInFilter,
                mediaDCMSiteFilter,
                mediaUpLevelFilter,
                mediaAllFilter, 
                numberOS,
                media_id, stock_id).ToList();
        }

    }
}

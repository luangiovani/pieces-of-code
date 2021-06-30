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
    public class LocationsService : ServiceBase<Locations, LocationsViewModel>
    {
        private readonly LocationsRepository _repository;

        public LocationsService(LocationsRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

    public LocationsViewModel GetByIdNovo(int? locationReceived_id)
    {
      var objLocations = _repository.GetById(locationReceived_id);

      LocationsViewModel objLocationsViewModel = new LocationsViewModel
      {
        accountAgency = objLocations.accountAgency,
        accountNumber = objLocations.accountNumber,
        accountType = objLocations.accountType,
        address = objLocations.address,
        adminMachineIp = objLocations.adminMachineIp,
        adminMachinePwd = objLocations.adminMachinePwd,
        adminMachineUser = objLocations.adminMachineUser,
        adminPcIp = objLocations.adminPcIp,
        bank = objLocations.bank,
        city = objLocations.city,
        city_id = objLocations.city_id,
        cnpj = objLocations.cnpj,
        companyName = objLocations.companyName,
        dateRegistration = objLocations.dateRegistration,
        description = objLocations.description,
        district = objLocations.district,
        fax1 = objLocations.fax1,
        //emails = obj.emailsServiceOrder,
        fax2 = objLocations.fax2,
        ftpIp = objLocations.ftpIp,
        ftpPwd = objLocations.ftpPwd,
        ftpUser = objLocations.ftpUser,
        gatewayIp = objLocations.gatewayIp,
        ie = objLocations.ie,
        internalGateway = objLocations.internalGateway,
        internalSubNet = objLocations.internalSubNet,
        ipRange = objLocations.ipRange,
        labMachineIP = objLocations.labMachineIP,
        labMachinePwd = objLocations.labMachinePwd,
        labMachineUser = objLocations.labMachineUser,
        line1 = objLocations.line1,
        line2 = objLocations.line2,
        line3 = objLocations.line3,
        line4 = objLocations.line4,
        line5 = objLocations.line5,
        location_id = objLocations.location_id,
        maxParcels = objLocations.maxParcels,
        name = objLocations.name,
        newsGroupIp = objLocations.newsGroupIp,
        newsGroupPwd = objLocations.newsGroupPwd,
        newsGroupUser = objLocations.newsGroupUser,
        OS_Series = objLocations.OS_Series,
        otherInformations = objLocations.otherInformations,
        phoneExtension = objLocations.phoneExtension,
        postalZipCode = objLocations.postalZipCode,
        primaryDNS = objLocations.primaryDNS,
        qcAdminMachineIp = objLocations.qcAdminMachineIp,
        qcAdminMachinePwd = objLocations.qcAdminMachinePwd,
        qcAdminMachineUser = objLocations.qcAdminMachineUser,
        //roleLocations = obj.roleLocations,
        sacContactEmail = objLocations.sacContactEmail,
        sacContactName = objLocations.sacContactName,
        secondaryDNS = objLocations.secondaryDNS,
        subNet = objLocations.subNet,
        telephone = objLocations.telephone,
        tollFree = objLocations.tollFree,
        userRegistration_id = objLocations.userRegistration_id,
        //users = obj.users,
        vncAdminMachineIp = objLocations.vncAdminMachineIp,
        vncAdminMachinePwd = objLocations.vncAdminMachinePwd,
        vncAdminMachineUser = objLocations.vncAdminMachineUser,
        vncLabMachineIP = objLocations.vncLabMachineIP,
        vncLabMachinePwd = objLocations.vncLabMachinePwd,
        vncLabMachineUser = objLocations.vncLabMachineUser,
        website = objLocations.website,
        foto = objLocations.foto,
      };

      return objLocationsViewModel;
    }
  }
}

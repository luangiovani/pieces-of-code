using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class PrintServiceOrderViewModel
    {
        public PrintServiceOrderViewModel ()
	    {
            medias = new List<MediasOnPrintServiceOrderViewModel>();
	    }

        public string ServiceOrderId { get; set; }
        public string OS_Series { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContactName { get; set; }
        public string CustomerContactPhone { get; set; }
        public string CustomerContactFax { get; set; }
        public string ServiceOrderDate { get; set; }
        public string ServiceToExecute { get; set; }
        public string TypeOfService { get; set; }
        public string ServiceOrderStatus { get; set; }
        public string CustomerRepresentant { get; set; }
        public string LocationReceived { get; set; }
        public string LocationCurrent { get; set; }
        public string Agent { get; set; }
        public string SmartNumber { get; set; }
        public string OperatingSystem{ get; set; }
        public string FATType{ get; set; }
        public string SystemSetup{ get; set; }
        public string RaidControl{ get; set; }
        public string OperatingSystemVersion{ get; set; }
        public string Compression{ get; set; }
        public string NumberOfVolumes{ get; set; }
        public string BlockSize{ get; set; }
        public string PartitionInfo{ get; set; }
        public string Failure{ get; set; }
        public string TargetData{ get; set; }
        public string PreRecovery{ get; set; }
        public string Raid{ get; set; }
        public string Notes{ get; set; }
        public List<MediasOnPrintServiceOrderViewModel> medias { get; set; }
        public string CpfCnpj { get; set; }
        public string IeRg { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string District { get; set; }
    }

    public class MediasOnPrintServiceOrderViewModel
    {
        public string Number { get; set; }
        public string Make  { get; set; }
        public string Model  { get; set; }
        public string Serial  { get; set; }
        public string Part  { get; set; }
        public string Interface  { get; set; }
    }

    public class PrintBudgetViewModel
    {
        public PrintBudgetViewModel()
	    {
            medias = new List<MediasOnPrintServiceOrderViewModel>();
	    }

        public string ServiceOrderId { get; set; }
        public string OS_Series { get; set; }
        public string ServiceOrderDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCPFCNPJ { get; set; }
        public string CustomerRG { get; set; }
        public string CustomerCityState { get; set; }
        public string ServiceOrderDiagnosis { get; set; }
        public string ServiceOrderTimeNeeded { get; set; }
        public string ServiceOrderTotalCost { get; set; }
        public string ServiceOrderTotalCostInitialsCash { get; set; }
        public string LocationBank { get; set; }
        public string LocationAccountAgency { get; set; }
        public string LocationAccountNumber { get; set; }
        public string LocationAccountType { get; set; }
        public string LocationBankMaxParcels { get; set; }
        public string LocationName { get; set; }
        public string LocationCNPJ { get; set; }
        public string LocationIE { get; set; }
        public string LocationAddress { get; set; }
        public List<MediasOnPrintServiceOrderViewModel> medias { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class DetailServiceOrderViewModel
    {
        public DetailServiceOrderViewModel()
	    {
            medias = new List<MediasOnDetailServiceOrderViewModel>();
	    }

        public string ServiceOrderId { get; set; }
        public string OS_Series { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContactName { get; set; }
        public string CustomerContactPhone { get; set; }
        public string CustomerContactEmail { get; set; }
        public string CustomerContactFax { get; set; }
        public string ServiceOrderDate { get; set; }
        public string ServiceOrderReceived { get; set; }
        public string ServiceToExecute { get; set; }
        public string TypeOfService { get; set; }
        public string ServiceOrderStatus { get; set; }
        public string ServiceOrderSubStatus { get; set; }
        public int ServiceOrderStatusNumero { get; set; }
        public string ServiceOrderStatusClient { get; set; }
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
        public string Raid { get; set; }
        public string Notes { get; set; }
        public string MarcaMedia { get; set; }
        public string ModeloMedia { get; set; }
        public string SerieMedia { get; set; }

        public string codigoRastreio { get; set; }
        public string urlUploadContrato { get; set; }
        public string dtaAprovacaoContrato { get; set; }
        public string idPagamentoPagSeguro { get; set; }

        public string valorOrcamento { get; set; }
        public string currency { get; set; }
        public string datePagamento { get; set; }
        public string dateVencimentoPagamento { get; set; }

        public string valorOrcamentoOriginal { get; set; }
        public string equipamentosAdiquiridos { get; set; }
        public string metodoPagamento { get; set; }
        public string frete { get; set; }
        public int qtdCpfCnpj { get; set; }
        public string foto { get; set; }
        public List<MediasOnDetailServiceOrderViewModel> medias { get; set; }
    }

    public class MediasOnDetailServiceOrderViewModel
    {
        public string Number { get; set; }
        public string Make  { get; set; }
        public string Model  { get; set; }
        public string Serial  { get; set; }
        public string Part  { get; set; }
        public string Interface  { get; set; }
    }

    
}
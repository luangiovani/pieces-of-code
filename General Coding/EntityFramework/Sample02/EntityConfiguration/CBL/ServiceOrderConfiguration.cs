using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.EntityConfiguration.CBL
{
   public class ServiceOrderConfiguration : EntityTypeConfiguration<ServiceOrder>
   {
      public ServiceOrderConfiguration()
      {
         HasKey(s => s.serviceOrder_id)
             .Property((ac => ac.serviceOrder_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

         Property(s => s.date).IsRequired();

         HasRequired(s => s.user)
             .WithMany(u => u.serviceOrders)
             .HasForeignKey(s => s.user_id);

         HasOptional(s => s.userAssigned)
             .WithMany(u => u.serviceOrdersAssigned)
             .HasForeignKey(s => s.userAssigned_id);

         Property(s => s.dateAssigned).IsOptional();

         HasOptional(s => s.customer)
             .WithMany(c => c.serviceOrders)
             .HasForeignKey(s => s.customer_id);

         HasMany(s => s.agentComissions)
             .WithRequired(ac => ac.serviceOrder)
             .HasForeignKey(ac => ac.serviceOrder_id);

         HasMany(s => s.labNotes)
            .WithRequired(ln => ln.serviceOrder)
            .HasForeignKey(ln => ln.serviceOrder_id);

         HasOptional(s => s.location)
             .WithMany(l => l.serviceOrders)
             .HasForeignKey(s => s.location_id);

         HasRequired(s => s.locationReceived)
             .WithMany(l => l.serviceOrdersReceived)
             .HasForeignKey(s => s.locationReceived_id);

         HasOptional(s => s.serviceOrderBilling)
             .WithRequired(s => s.serviceOrder);

         HasOptional(s => s.serviceOrderEvaluation)
             .WithRequired(s => s.serviceOrder);

         HasOptional(s => s.serviceOrderInquiryFollowUp)
             .WithRequired(s => s.serviceOrder);

         HasOptional(s => s.serviceOrderQuoting)
             .WithRequired(s => s.serviceOrder);

         HasOptional(s => s.serviceOrderRecoveryFollowUp)
             .WithRequired(s => s.serviceOrder);

         HasOptional(s => s.serviceOrderShipping)
             .WithRequired(s => s.serviceOrder);

         HasMany(s => s.contacts)
             .WithRequired(c => c.serviceOrder)
             .HasForeignKey(c => c.serviceOrder_id);

         HasMany(s => s.serviceOrderMedias)
             .WithRequired(sm => sm.serviceOrder)
             .HasForeignKey(sm => sm.serviceOrder_id);

         HasMany(s => s.serviceOrderPayments)
             .WithRequired(sm => sm.serviceOrder)
             .HasForeignKey(sm => sm.serviceOrder_id);

         //HasMany(s => s.serviceOrderClouds)
         //                .WithRequired(sm => sm.serviceOrders)
         //                .HasForeignKey(sm => sm.serviceOrder_id);

         Property(s => s.referredBy).HasMaxLength(250);

         Property(s => s.status).HasMaxLength(150);

         Property(s => s.extensionStatus).HasMaxLength(500);

         Property(s => s.takenBy).HasMaxLength(250);

         Property(s => s.CSR).HasMaxLength(250);

         Property(s => s.typeOfService).HasMaxLength(200);

         Property(s => s.serviceToExecute).HasMaxLength(2000);

         Property(s => s.mostImportantFilesToRecovery).HasMaxLength(2000);

         Property(s => s.estimate).HasPrecision(18, 2);

         Property(s => s.locationReceived_id).IsRequired();

         Property(s => s.location_id).IsOptional();

         Property(s => s.arrivedBy).HasMaxLength(250);

         Property(s => s.wayBillNumber).HasMaxLength(100);

         Property(s => s.packageCondidition).HasMaxLength(250);

         Property(s => s.smartNumber).HasMaxLength(100);

         Property(s => s.techsName).HasMaxLength(250);

         Property(s => s.originOfServiceOrder).HasMaxLength(500);

         Property(s => s.note).HasMaxLength(5000);

         Property(s => s.customerContact_id).IsOptional();

         Property(s => s.customerContactName).HasMaxLength(200).IsOptional();

         Property(s => s.customerContactEmail).HasMaxLength(150).IsOptional();

         Property(s => s.customerContactTelephone).HasMaxLength(20).IsOptional();

         Property(s => s.customerContactMobile).HasMaxLength(20).IsOptional();

         Property(s => s.approvalDate).IsOptional();

         Property(s => s.statusDate).IsRequired();

         Property(s => s.subStatus).HasMaxLength(150).IsOptional();

         Property(s => s.inTransfer).IsOptional();

         Property(s => s.transferNote).HasMaxLength(350).IsOptional();

         Property(s => s.codigoRastreio).HasMaxLength(150).IsOptional();
         Property(s => s.urlUploadContrato).HasMaxLength(350).IsOptional();
         Property(s => s.idPagamentoPagSeguro).HasMaxLength(350).IsOptional();
         Property(s => s.dtaAprovacaoContrato).IsOptional();

         /*
         HasMany(s => s.additionalService)
             .WithRequired(c => c.serviceOrder)
             .HasForeignKey(c => c.serviceOrder_id);
         //*/
      }
   }
}

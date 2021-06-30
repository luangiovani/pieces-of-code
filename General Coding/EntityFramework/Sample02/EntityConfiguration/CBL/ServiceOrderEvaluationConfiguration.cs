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
    public class ServiceOrderEvaluationConfiguration : EntityTypeConfiguration<ServiceOrderEvaluation>
    {
        public ServiceOrderEvaluationConfiguration()
        {
            HasKey(s => s.serviceOrder_id)
                .Property(s => s.serviceOrder_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(s => s.dateRegistration).IsRequired();
            Property(s => s.userRegistration_id).IsRequired();
            Property(s => s.calledAbout).HasMaxLength(250);
            Property(s => s.estimatedGivenFrom).HasPrecision(18,2);
            Property(s => s.estimatedGivenTo).HasPrecision(18,2);
            Property(s => s. make).HasMaxLength(100);
            Property(s => s. serial_no).HasMaxLength(100);
            Property(s => s. makeOfComputer).HasMaxLength(150);
            Property(s => s. model).HasMaxLength(250);
            Property(s => s. interfaceOfdevice).HasMaxLength(250);
            Property(s => s. operatingSystem).HasMaxLength(250);
            Property(s => s. operatingSystemVersion).HasMaxLength(250);
            Property(s => s. partitionInfo).HasMaxLength(500);
            Property(s => s. raidType).HasMaxLength(250);
            Property(s => s. controlledType).HasMaxLength(250);
            Property(s => s. numberOfVolumes).IsOptional();
            Property(s => s.blockSize).HasPrecision(18,4);
            Property(s => s. raidDetails).HasMaxLength(2000);
            Property(s => s. numberOftapes).IsOptional();
            Property(s => s. typeOfbackupSystem).HasMaxLength(250);
            Property(s => s. numberOfSessions).IsOptional();
            Property(s => s. dataCompressionType).HasMaxLength(250);
            Property(s => s. tapeDetails).HasMaxLength(500);
            Property(s => s. failureMalfunction).HasMaxLength(2500);
            Property(s => s. preRecoveryInfo).HasMaxLength(2500);
            Property(s => s. criticalTargetData).HasMaxLength(2500);
            Property(s => s. fileAllocationType).HasMaxLength(250);
            Property(s => s. numberOfDrivesInSystem).IsOptional();
            Property(s => s. makeOfController).HasMaxLength(250);
            Property(s => s. faultFound).HasMaxLength(250);
            Property(s => s. jobClass).HasMaxLength(250);
            Property(s => s. techTeam).HasMaxLength(250);
            Property(s => s. processedWhere).HasMaxLength(250);
            Property(s => s.techNotes).HasMaxLength(4000);
            Property(s => s.diagnosisFinished).IsOptional();
        }
    }
}

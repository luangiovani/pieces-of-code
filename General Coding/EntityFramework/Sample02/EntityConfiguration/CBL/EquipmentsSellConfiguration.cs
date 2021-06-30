using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Database.Entity.CBL;

namespace Framework.Database.EntityConfiguration.CBL
{
    public class EquipmentsSellConfiguration : EntityTypeConfiguration<EquipmentsSell>
    {
        public EquipmentsSellConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(f => f.equipament_id)
                .Property((f => f.equipament_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(f => f.Amount).IsRequired();
            Property(f => f.Make).IsOptional().HasMaxLength(200);
            Property(f => f.Model).IsOptional().HasMaxLength(200);
            Property(f => f.Size).IsOptional().HasMaxLength(200);
            Property(f => f.SalePrice).IsOptional();
            Property(f => f.media_id).IsRequired();
            Property(f => f.serviceOrder_id).IsRequired();


            /*Amount = obj.Amount,
                Make = obj.Make,
                Model = obj.Model,
                Size = obj.Size,
                SalePrice = obj.SalePrice,
                media_id = obj.media_id,
                serviceOrder_id = obj.serviceOrder_id,*/
        }
    }
}


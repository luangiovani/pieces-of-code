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
    public class DocumentTypeConfiguration : EntityTypeConfiguration<DocumentType>
    {
        public DocumentTypeConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(dc => dc.documentType_id)
                .Property((ac => ac.documentType_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ///Tamanho máximo de 250 caracteres para o nome do tipo de documento
            Property(dt => dt.name).HasMaxLength(250);

            ///Tamanho máximo de 500 caracteres para a descrição do tipo de documento
            Property(dt => dt.description).HasMaxLength(500);

            ///Tamanho máximo de 8000 caracteres para a descrição do tipo de documento
            Property(dt => dt.url_file).HasMaxLength(8000);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property(dt => dt.active).IsRequired();

            Property(dt => dt.dateRegistration).IsRequired();

            Property(dt => dt.userRegistration_id).IsRequired();
        }
    }
}

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
    public class DocumentTypeService : ServiceBase<DocumentType, DocumentTypeViewModel>
    {
        private readonly DocumentTypeRepository _repository;

        public DocumentTypeService(DocumentTypeRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}

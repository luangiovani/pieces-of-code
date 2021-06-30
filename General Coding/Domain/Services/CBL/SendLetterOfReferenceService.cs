﻿using Framework.Database.Entity.CBL;
using Framework.Domain.Model.CBL;
using Framework.Domain.Repository.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Services.CBL
{
    public class SendLetterOfReferenceService : ServiceBase<SendLetterOfReference, SendLetterOfReferenceViewModel>
    {
        private readonly SendLetterOfReferenceRepository _repository;

        public SendLetterOfReferenceService(SendLetterOfReferenceRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}

using AutoMapper;
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
    public class NotesService : ServiceBase<Notes, NotesViewModel>
    {
        private readonly NotesRepository _repository;

        public NotesService(NotesRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

    public void UpdateNovo(NotesViewModel note)
    {
      var current = _repository.GetById(note.note_id);
      current = Mapper.Map<NotesViewModel, Notes>(note, current);
      _repository.Update(current);
    }
  }
}

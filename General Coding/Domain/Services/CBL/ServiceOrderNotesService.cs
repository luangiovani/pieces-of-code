using AutoMapper;
using Framework.Database.Entity.CBL;
using Framework.Domain.Model.CBL;
using Framework.Domain.Repository.CBL;
using Framework.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Services.CBL
{
  public class ServiceOrderNotesService : ServiceBase<ServiceOrderNotes, ServiceOrderNotesViewModel>
  {
    private readonly ServiceOrderNotesRepository _repository;

    public ServiceOrderNotesService(ServiceOrderNotesRepository repository)
        : base(repository)
    {
      _repository = repository;
    }

    public ICollection<ServiceOrderNotesViewModel> GetEstruturado()
    {
      List<ServiceOrderNotes> final = new List<ServiceOrderNotes>();

      var notes = _repository.GetAll();

      return Mapper.Map<ICollection<ServiceOrderNotesViewModel>>(notes);
    }

    public IEnumerable<ServiceOrderNotesViewModel> GetAllByTypeNoteAndServiceOrderId(int timeLine, decimal serviceOrderId)
    {
      var notes = _repository.GetAll(t => t.note.typenote_id == (int)TypeOfNoteEnum.TimeLine && t.serviceOrder_id == serviceOrderId).ToList();

      var lstReturn = new List<ServiceOrderNotesViewModel>();

      foreach (var note in notes)
      {
        ServiceOrderNotesViewModel objNote = new ServiceOrderNotesViewModel
        {
          dateRegistration = note.dateRegistration,
          note = note.note,
          note_id = note.note_id,          
          serviceOrderNotes_id = note.serviceOrderNotes_id,
          serviceOrderStatus = note.serviceOrderStatus,
          serviceOrder_id = note.serviceOrder_id,
          userRegistration_id = note.userRegistration_id
          //serviceOrderOfNote = note.serviceOrderOfNote
        };
      }

      return lstReturn;
    }
  }
}

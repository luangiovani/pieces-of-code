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
    public class ContactService : ServiceBase<Contact, ContactViewModel>
    {
        private readonly ContactRepository _repository;

        public ContactService(ContactRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public override void DeleteById(int Id)
        {
            string sSqlQuery = "DELETE FROM Contact WHERE contact_id = " + Id;
            _repository.DeleteteById(sSqlQuery);
        }

        public ContactEditViewModel GetEditContact(int contactId)
        {
            var model = base.GetById(contactId);
            var contact = new ContactEditViewModel(model);
            return contact;
        }

        public void AtualizaContato(int? city_id, int contact_id)
        {
            _repository.AtualizaContato(city_id, contact_id);
        }

    public void UpdateObjetoContact(ContactViewModel contact)
    {
      var current = _repository.GetById(contact.contact_id);
      current = Mapper.Map<ContactViewModel, Contact>(contact, current);

      _repository.Update(current);
    }
  }
}

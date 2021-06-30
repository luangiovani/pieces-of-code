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
  public class CustomerService : ServiceBase<Customer, CustomerViewModel>
  {
    private readonly CustomerRepository _repository;

    public CustomerService(CustomerRepository repository)
        : base(repository)
    {
      _repository = repository;
    }

    public int GetTOTAL_ROWS(string search)
    {
      return _repository.GetTOTAL_ROWS(search);
    }

    public paginacaoCustomer getPaginacao(int length, int page)
    {
      return _repository.getPaginacao(length, page);
    }

    public virtual CustomerViewModel GetByEmailSenha(string email, string password)
    {
      return Mapper.Map<CustomerViewModel>(_repository.GetByEmailSenha(email, password));
    }

    public string GetHash(string email)
    {
      return _repository.GetHash(email);
    }

    public virtual IEnumerable<CustomerViewModel> GetSoundex(string term)
    {
      return Mapper.Map<IEnumerable<CustomerViewModel>>(_repository.GetSoundex(term));
    }

    public virtual IEnumerable<Customer> GetSoundexPrimitiveType(string term)
    {
      return _repository.GetSoundex(term);
    }

    public CustomerViewModel GetByEmail(string email)
    {
      return _repository.GetByEmail(email);
    }

    public void UpdateObjetoCustomer(CustomerViewModel customer)
    {
      var current = _repository.GetById(customer.customer_id);
      current = Mapper.Map<CustomerViewModel, Customer>(customer, current);

      _repository.Update(current);

      //CustomerViewModel objCustomerViewModel = new CustomerViewModel
      //{
      //  address = objCustomer.address,
      //  addressExtension = objCustomer.addressExtension,
      //  addressNumber = objCustomer.addressNumber,
      //  business_type = objCustomer.business_type,
      //  cpfCnpj = objCustomer.cpfCnpj,
      //  creditTerms = objCustomer.creditTerms,
      //  customer_id = objCustomer.customer_id,
      //  dateLastAccessPortal = objCustomer.dateLastAccessPortal,
      //  dateLastUpdateInformations = objCustomer.dateLastUpdateInformations,
      //  dateRegistration = objCustomer.dateRegistration,
      //  district = objCustomer.district,
      //  email = objCustomer.email,
      //  hearOfUs = objCustomer.hearOfUs,
      //  name = objCustomer.name,
      //  notes = objCustomer.notes,
      //  password = objCustomer.password,
      //  percentage = objCustomer.percentage,
      //  pointOfContact = objCustomer.pointOfContact,
      //  postalZipCode = objCustomer.postalZipCode,
      //  rgIdState = objCustomer.rgIdState,
      //  tokenCode = objCustomer.tokenCode,
      //  userRegistration_id = objCustomer.userRegistration_id,
      //  website = objCustomer.website,      

        

      //};


      //return Mapper.Map<TModel>(_repository.Update(current));


      //Customer objCustomer = _repository.GetById(customer.customer_id);//new Customer();

      //objCustomer.address = customer.address;
      //objCustomer.addressExtension = customer.addressExtension;
      //objCustomer.addressNumber = customer.addressNumber;
      //objCustomer.business_type = customer.business_type;
      ////Contacts
      //List<Contact> lstContact = new List<Contact>();
      //foreach (var item in customer.contacts)
      //{
      //  CustomerContact objCustomerContact = _repository.//new CustomerContact();

      //  objCustomerContact.contact = item.contact;
      //  objCustomerContact.contact_id = item.contact_id;
      //  objCustomerContact.customer = item.

      //}

      ////objCustomer.contacts = customer.contacts;
      //objCustomer.cpfCnpj = customer.cpfCnpj;
      //objCustomer.creditTerms = customer.creditTerms;
      //objCustomer.customer_id = customer.customer_id;
      //objCustomer.dateLastAccessPortal = customer.dateLastAccessPortal;
      //objCustomer.dateLastUpdateInformations = customer.dateLastUpdateInformations;
      //objCustomer.dateRegistration = customer.dateRegistration;
      //objCustomer.district = customer.district;
      //objCustomer.email = customer.email;
      //objCustomer.hearOfUs = customer.hearOfUs;
      ////objCustomer.id_old = customer.id_old;
      //objCustomer.name = customer.name;
      //objCustomer.notes = customer.notes;
      //objCustomer.password = customer.password;
      //objCustomer.percentage = customer.percentage;
      //objCustomer.pointOfContact = customer.pointOfContact;
      //objCustomer.postalZipCode = customer.postalZipCode;
      //objCustomer.rgIdState = customer.rgIdState;
      ////objCustomer.serviceOrderPayments = customer.serviceOrderPayments;
      ////objCustomer.serviceOrders = customer.serviceOrders;
      //objCustomer.tokenCode = customer.tokenCode;
      //objCustomer.userRegistration_id = customer.userRegistration_id;
      //objCustomer.website = customer.website;

    }
  }
}

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
  public class SuppliersService : ServiceBase<Suppliers, SuppliersViewModel>
  {
    private readonly SuppliersRepository _repository;

    public SuppliersService(SuppliersRepository repository)
        : base(repository)
    {
      _repository = repository;
    }

    public static object Select(Func<object, SuppliersViewModel> p)
    {
      throw new NotImplementedException();
    }

    public void Delete(MediaViewModel o)
    {
      throw new NotImplementedException();
    }

    public List<SuppliersViewModel> GetAllNew()
    {
      var suppliers = _repository.GetAll().ToList();

      var lstSuppliers = new List<SuppliersViewModel>();

      foreach (var supplier in suppliers)
      {
        SuppliersViewModel objSupplier = new SuppliersViewModel
        {
          name = supplier.name,
          address = supplier.address,
          city = supplier.city,
          website = supplier.website,
          supplier_id = supplier.supplier_id
        };

        lstSuppliers.Add(objSupplier);
      }

      return lstSuppliers;
    }
  }
}

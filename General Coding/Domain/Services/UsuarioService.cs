using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Framework.Database.Entity;
using Framework.Domain.Identity;
using Framework.Domain.Model;
using System.Collections.Generic;
using Framework.Domain.Repository;
using System.Linq.Expressions;

namespace Framework.Domain.Services
{

  public class UsuarioService : ServiceBase<ApplicationUser, UsuarioViewModel>
  {

    private readonly UsuarioRepository _repository;

    public UsuarioService(UsuarioRepository repository) : base(repository)
    {
      _repository = repository;
    }

    public void DeletePerfil(string id_perfil)
    {
      _repository.GetByPerfil(id_perfil)
          .ToList()
          .ForEach(o =>
      {
        _repository.DeletePerfil(o);
      });
    }

    public List<UsuarioViewModel> GetAllContainsLocationId(Expression<Func<ApplicationUser, bool>> exp)
    {
      var users = _repository.GetAll(exp).ToList();

      var lstUsers = new List<UsuarioViewModel>();

      foreach (var user in users)
      {
        UsuarioViewModel objUser = new UsuarioViewModel
        {
          nome = user.nome,
          Email = user.Email,
          telefone = user.telefone,
          celular = user.celular,
          id = user.Id
        };

        lstUsers.Add(objUser);
      }

      return lstUsers;
    }

    #region Identity Code
    //public UsuarioViewModel FindById(string id)
    //{
    //    return Mapper.Map<UsuarioViewModel>(_repository.GetById(new Guid(id)));
    //}

    //public UsuarioViewModel FindByEmail(string email)
    //{
    //    return Mapper.Map<UsuarioViewModel>(_repository.Find(o => o.Email == email).FirstOrDefault());
    //}

    //public void SignIn(ApplicationSignInManager manager, UsuarioViewModel usuario, bool isPersistent, bool rememberBrowser)
    //{
    //    manager.SignIn(Mapper.Map<ApplicationUser>(usuario), isPersistent, rememberBrowser);
    //}

    //public async Task<SignInStatus> PasswordSignInAsync(ApplicationSignInManager manager, string email, string password, bool rememberMe)
    //{
    //    return await manager.PasswordSignInAsync(email, password, rememberMe, false);
    //}

    //public async Task SetTwoFactorEnabledAsync(ApplicationUserManager manager, string userId, bool enabled)
    //{
    //    await manager.SetTwoFactorEnabledAsync(userId, true);
    //}

    //public IdentityResult Create(ApplicationUserManager manager, UsuarioViewModel usuario, string password)
    //{
    //    return manager.Create(Mapper.Map<ApplicationUser>(usuario), password);
    //}

    //public IdentityResult Update(ApplicationUserManager manager, UsuarioViewModel usuario)
    //{
    //    return manager.Update(Mapper.Map<ApplicationUser>(usuario));
    //}

    //public string GeneratePasswordResetToken(ApplicationUserManager manager, string id)
    //{
    //    return manager.GeneratePasswordResetToken(id);
    //}

    //public async Task<string> GenerateChangePhoneNumberTokenAsync(ApplicationUserManager manager, string id, string number)
    //{
    //    return await manager.GenerateChangePhoneNumberTokenAsync(id, number);
    //}

    //public IdentityResult ResetPassword(ApplicationUserManager manager, string id, string code, string password)
    //{
    //    return manager.ResetPassword(id, code, password);
    //}

    public async Task<IdentityResult> AddPasswordAsync(ApplicationUserManager manager, string userId, string password)
    {
      return await manager.AddPasswordAsync(userId, password);
    }

    public IdentityResult AddPassword(ApplicationUserManager manager, string userId, string password)
    {
      return manager.AddPassword(userId, password);
    }

    //public async Task<IdentityResult> ChangePasswordAsync(ApplicationUserManager manager, string userId, string currentPassword, string newPassword)
    //{
    //    return await manager.ChangePasswordAsync(userId, currentPassword, newPassword);
    //}

    //public async Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUserManager manager, string userId)
    //{
    //    return await manager.GetLoginsAsync(userId);
    //}

    //public async Task<IdentityResult> AddLoginAsync(ApplicationUserManager manager, string userId, UserLoginInfo info)
    //{
    //    return await manager.AddLoginAsync(userId, info);
    //}

    //public async Task<IdentityResult> RemoveLoginAsync(ApplicationUserManager manager, string userId, UserLoginInfo info)
    //{
    //    return await manager.RemoveLoginAsync(userId, info);
    //}

    //public async Task<string> GetPhoneNumberAsync(ApplicationUserManager manager, string userId)
    //{
    //    return await manager.GetPhoneNumberAsync(userId);
    //}

    //public async Task<IdentityResult> SetPhoneNumberAsync(ApplicationUserManager manager, string userId)
    //{
    //    return await manager.SetPhoneNumberAsync(userId, null);
    //}

    //public async Task<IdentityResult> ChangePhoneNumberAsync(ApplicationUserManager manager, string userId, string number, string code)
    //{
    //    return await manager.ChangePhoneNumberAsync(userId, number, code);
    //}

    //public async Task<bool> GetTwoFactorEnabledAsync(ApplicationUserManager manager, string userId)
    //{
    //    return await manager.GetTwoFactorEnabledAsync(userId);
    //}

    //public async Task SendEmailAsync(ApplicationUserManager manager, string userId, string subject, string body)
    //{
    //    await manager.SendEmailAsync(userId, subject, body);
    //}
    #endregion
  }
}
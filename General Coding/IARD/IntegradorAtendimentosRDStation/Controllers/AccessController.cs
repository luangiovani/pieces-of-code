using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Classes;
using IntegradorAtendimentosRDStation.Models;
using Newtonsoft.Json;

namespace IntegradorAtendimentosRDStation.Controllers
{
    public class AccessController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl, string msgm)
        {
            IPrincipal user = HttpContext.User;
            if (user != null && user.Identity.IsAuthenticated)
            {
                if (user.Identity is FormsIdentity)
                {
                    FormsIdentity id = (FormsIdentity)user.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    string userdata = ticket.UserData;

                    JsonSerializer serializer = new JsonSerializer();
                    StringReader sr = new StringReader(userdata);
                    JsonReader jr = new JsonTextReader(sr);
                    var usuario = serializer.Deserialize<UsuarioModel>(jr);

                    return String.IsNullOrEmpty(returnUrl) ? RedirectToAction("Index", "Home") : RedirectToLocal(returnUrl);
                }
            }
            else if (Request.IsAjaxRequest())
            {
                return Json(new
                {
                    success = false,
                    returnUrl
                }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuarioModel model, string returnUrl)
        {
            returnUrl = String.IsNullOrEmpty(returnUrl) ? "" : returnUrl;

            if (returnUrl.Length <= 3)
            {
                returnUrl = "";
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Autenticacao gerUsuario = Autenticacao.ValidaUsuario(model.Login.ToUpper(), model.Senha.ToUpper());

                    if (gerUsuario != null)
                    {
                        #region Obter Informações do Usuário

                        Paginacao Log = Pessoa.LoadDataPaginacao(" AND A572_cd_pes=" + Convert.ToString(gerUsuario.CodigoUsuario), 1, 10, "1");
                        if (Log.DataReader.Read())
                        {
                            if (Log.DataReader["A572_e_mail"].ToString() != "")
                            {
                                model.Email = Log.DataReader["A572_e_mail"].ToString();
                            }
                            model.Nome = Log.DataReader["A572_nome"].ToString();
                        }
                        Log.DataReader.Close();

                        #endregion

                        AutenticarUsuario(model);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Requisição não sucedida. Login ou senha são inválidos, tente novamente.");
                        return View(model);
                    }
                }
                catch (Exception exLogin)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao tentar processar sua solicitação.: "+exLogin.Message);
                    return View(model);
                }
            }
            else
                ModelState.AddModelError("", "Requisição não sucedida. Login ou senha não informados, tente novamente.");

            return View(model);
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        private void VerificarCookieExistenteERemover()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
            }
        }

        public void AutenticarUsuario(UsuarioModel usuario)
        {
            VerificarCookieExistenteERemover();

            DateTime now = DateTime.Now;
            DateTime expiration = now.AddMinutes(30);

            StringWriter stringWriter = new StringWriter();
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(stringWriter, new UsuarioModel() { Login = usuario.Login, Nome = usuario.Nome, Email = usuario.Email, Senha = usuario.Senha });
            StringBuilder stringBuilder = stringWriter.GetStringBuilder();
            string jsonUsuario = stringBuilder.ToString();
            stringWriter.Close();

            var ticket = new FormsAuthenticationTicket(
                                                       1, // version 
                                                       usuario.Nome, // user name
                                                       DateTime.Now, // create time
                                                       DateTime.Now.AddMinutes(30), // expire time
                                                       false, // persistent
                                                       jsonUsuario); // user data, such as roles

            var strEncryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strEncryptedTicket);
            Response.Cookies.Add(cookie);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 3)
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IntegradorAtendimentosRDStation.Models;
using Newtonsoft.Json;

namespace IntegradorAtendimentosRDStation.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public string usuarioLogadoNome = String.Empty;

        public BaseController()
        {
            if (this.HttpContext != null && this.HttpContext.User != null)
            {
                if (this.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (this.HttpContext.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)this.HttpContext.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string userdata = ticket.UserData;

                        JsonSerializer serializer = new JsonSerializer();
                        StringReader sr = new StringReader(userdata);
                        JsonReader jr = new JsonTextReader(sr);
                        var usuario = serializer.Deserialize<UsuarioModel>(jr);
                        usuarioLogadoNome = usuario.Nome;
                    }
                }
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            Exception e = filterContext.Exception;

            var viewResult = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml"
            };


            var msgErro = new StringBuilder();

            msgErro.Append("Esta mensagem deve ser direcionada a Esfera Informática.").Append("<br/>");
            msgErro.Append(string.IsNullOrEmpty(e.Message) ? "Não identificado." : e.Message).Append("<br/>");
            msgErro.Append(e.InnerException == null ? "Sem InnerException." : (e.InnerException.StackTrace == null ? "Sem StackTrace" : e.InnerException.StackTrace.ToString())).Append("<br/>");

            viewResult.ViewBag.ErrorMessage = msgErro.ToString();
            viewResult.ViewBag.ActionToRedirect = "Index";
            viewResult.ViewBag.ControllerToRedirect = "Home";

            filterContext.Result = viewResult;
            filterContext.ExceptionHandled = true;
        }

        public new HttpContextBase HttpContext
        {
            get
            {
                HttpContextWrapper context =
                    new HttpContextWrapper(System.Web.HttpContext.Current);
                return (HttpContextBase)context;
            }
        }
    }
}
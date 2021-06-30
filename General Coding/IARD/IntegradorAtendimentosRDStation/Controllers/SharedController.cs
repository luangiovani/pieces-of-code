using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntegradorAtendimentosRDStation.Controllers
{
    public class SharedController : Controller
    {
        public PartialViewResult _Progress()
        {
            if (string.IsNullOrEmpty(ViewBag.ErrorMessage))
            {
                return PartialView();
            }
            else
                return null;
        }
    }
}
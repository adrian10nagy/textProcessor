using DAL.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class CreditsController : Controller
    {
        // GET: Credits
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Partners()
        {
            return View();
        }

        public ActionResult Definitions()
        {
            var definitionSources = Kit.Instance.Definitions.GetAllDefinitionSources();
            ViewData["definitionSources"] = definitionSources;

            return View();
        }
    }
}
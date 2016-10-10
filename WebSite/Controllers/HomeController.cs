using DAL.Helpers.Extensions;
using DAL.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // HighlightedWordsPV

        //[HttpPost]
        //public ActionResult AsyncUpdateWordsExistance(string mainText)
        //{
        //    var unidentifiedWords = BL.Business.WordBL.getUnrecognizedWords(mainText);
        //    var unidentifiedWords2 = BL.Business.WordBL.getUnrecognizedWordsIndexes(mainText);

        //    return PartialView("HighlightedWordsPV", unidentifiedWords);
        //}
    }
}
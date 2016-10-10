using BL.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Helpers;

namespace WebSite.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GuessWord()
        {
            var word = KitBL.Instance.WordBL.GetGuessGameWord();

            return View(word);
        }

        public ActionResult ScrambleWord()
        {
            var word = KitBL.Instance.WordBL.GetScrambleGameWord();

            return View(word);
        }


        [HttpPost]
        public ActionResult ScrambleWordCorrect()
        {
            if (!BaseMVC.IsLoggedIn())
            {
                return Json(new { isLoggedIn = false });

            }

            return Json(new { isLoggedIn = true });
        }

        #region Modals

        //action for modal popup
        public PartialViewResult _ScrambleWordCorrectAnswer()
        {
            return PartialView("_ScrambleWordCorrectAnswer");
        }

        #endregion
    }
}
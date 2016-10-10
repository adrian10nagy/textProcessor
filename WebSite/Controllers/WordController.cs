using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.SDK;
using DAL.Entities;
using BL.Business;
using BL.Entities;

namespace WebSite.Controllers
{
    public class WordController : Controller
    {
        // GET: Word
        public ActionResult Index()
        {
            return View();
        }

        // GET: Word/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Word/Create
        public ActionResult Definition(int id)
        {
            var WordDefinitions = Kit.Instance.Definitions.GetAllWordDefinitionsByLexemId(id);
            ViewData["WordDefinitions"] = WordDefinitions;
            ViewData["WordLexemText"] = (Kit.Instance.Words.GetWordById(id) != null) ? Kit.Instance.Words.GetWordById(id).FormNoAcc : null;

            return View();
        }

        public ActionResult Random()
        {
            var word = Kit.Instance.Words.GetRandomMainWord(0.8, 12);
            var definitions = Kit.Instance.Definitions.GetAllWordDefinitionsByLexemId(word.lexemId);
            ViewData["word"] = word;
            ViewData["definitions"] = definitions;

            return View();
        }

    
        //[HttpPost]
        //public ActionResult AsyncAddWordToExistance(string wordText)
        //{
        //    try
        //    {
        //        var word = new Word();
        //        word.Name = wordText;
        //        word.Origin = Origin.NA;
        //        word.Created = DateTime.Now;
        //        var wordId = Kit.Instance.Words.AddWord(word);

        //        return Json(new { success = true, wordId = wordId });
        //    }
        //    catch
        //    {
        //        return Json(new { success = false });

        //    }
        //}

        // action for modal popup
        // AddWordDetailsPV
        //public PartialViewResult AddWordDetailsPV()
        //{
        //    return PartialView("AddWordDetailsPV", new Word());
        //}

        [HttpPost]
        public JsonResult AjaxAddWordDetailsNoun(Noun nounWord)
        {
            try
            {
                Kit.Instance.Words.AddNoun(nounWord);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });

            }

            //if (ModelState.IsValid)
            //{
            //    //isSuccess = Save data here return boolean
            //}
        }


        [HttpPost]
        public JsonResult AjaxAddWordDetailsVerb(Verb verbWord)
        {
            bool isSuccess = true;

            if (ModelState.IsValid)
            {
                //isSuccess = Save data here return boolean
            }
            return Json(isSuccess);
        }

        [HttpGet]
        public ActionResult Rime(string cauta)
        {
            if (cauta == null)
            {
                return View();
            }

            var wordRimes = KitBL.Instance.WordBL.GetRimeWords(cauta);
            ViewData["wordRimes"] = wordRimes;
            ViewData["cauta"] = cauta;

            return View();
        }


        [HttpGet]
        public ActionResult WordOfTheDay()
        {
            WordViewModel word = KitBL.Instance.WordBL.WordOfTheDay(DateTime.Now);
            ViewData["word"] = word;

            return View();
        }
    }
}

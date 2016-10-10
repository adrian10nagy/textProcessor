using DAL.Entities;
using DAL.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class ResourceController : Controller
    {
        // GET: Resource
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllWords()
        {
            //List<WordMain> words = Kit.Instance.Words.GetAllMainWords();
            List<WordMain> words = new List<WordMain>();

            return View(words);
        }

        [HttpGet]
        public ActionResult Search(string searchText)
        {
            List<WordInflection> inflectedWords = new List<WordInflection>();
            List<WordMain> words = new List<WordMain>();            
            
            if (searchText == null || searchText == "")
            {
                return RedirectToAction("Index", "Resource");
            }

            words = Kit.Instance.Words.GetAllWordsByName(searchText);

            if (words.Count == 1)
            {
                return RedirectToAction("Definition", "Word", new { @id = words.First().lexemId });
            }

            words = Kit.Instance.Words.GetAllWordsByNameRelative(searchText);

            if (words.Count < 1)
            {
                inflectedWords = Kit.Instance.WordInflections.GetAllInflectedWordsByName(searchText);
                if (inflectedWords.Count < 1)
                {
                    return RedirectToAction("Index", "Resource");
                }
            }
            ViewData["inflectedWords"] = inflectedWords;

            return View(words);
        }

        public ActionResult Details(int id)
        {
            WordMain word = Kit.Instance.Words.GetWordById(id);

            List<WordInflection> inflectedWords = Kit.Instance.WordInflections.GetAllInflectedWorsdByLexemId(id);

            ViewData["inflectedWords"] = inflectedWords;

            return View(word);
        }
    }
}
using BL.Business;
using BL.Helpers;
//using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class TextController : Controller
    {
        // GET: Text
        public ActionResult Index()
        {
            return View();
        }

        // GET: Text
        public ActionResult SpellCheck()
        {
            return View();
        }

        // GET: Text
        public ActionResult Result(string fileName)
        {
            var filePath =  HostingEnvironment.MapPath(@"~/App_Data/"+ fileName);
            var lines = System.IO.File.ReadAllLines(filePath);
            StringBuilder sb = new StringBuilder();
            foreach (var line in lines)
            {
                sb.Append(line + "\n");
            }

            ViewData["fileName"] = fileName;
            ViewData["words"] = sb.ToString();
            ViewData["filePath"] = filePath;

            return View();
        }

        public ActionResult Verifica()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verifica(FormCollection collection)
        {
            var fileName = string.Empty;
            if (ModelState.IsValid)
            {
                var file1 = Request.Files[0];
                if(file1 != null)
                {
                    var file = (HttpPostedFileWrapper)file1;
                    var words = FileProcess.GetWordsFromFile(file);
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in words)
                    {
                        sb.Append(item + Environment.NewLine);
                    }
                    Task<List<string>> unrecognizedWords = WordProcess.GetUnrecognizedWordsFromText(sb.ToString());

                    fileName = Path.GetFileNameWithoutExtension(file.FileName) + ".txt";
                    FileProcess.SaveFile(fileName, sb.ToString());
                }
            }
            return RedirectToAction("Result", "Text", new { fileName = fileName });
        }

        [HttpPost]
        public async Task<ActionResult> AsyncCheckTextSpelling(string textToCheck)
        {
            Task<List<string>> unrecognizedWords = WordProcess.GetUnrecognizedWordsFromText(textToCheck);
            ViewData["unidentifiedWords"] = await unrecognizedWords;

            return PartialView("_CheckText");
        }
    }
}
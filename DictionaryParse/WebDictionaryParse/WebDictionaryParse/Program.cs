using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
//using WebDictionaryParse.Entities;
using WebDictionaryParse.Extensions;
using DAL.Entities;

namespace WebDictionaryParse
{


    class Program
    {
        static void Main(string[] args)
        {
            // bring all Words 
            var allWordsDB = DAL.SDK.Kit.Instance.Words.GetAllMainWords();

            // bring words with unexisting sinonims
            var allWordsWithNoSinonims = new List<WordMain>();
            foreach (var item in allWordsDB)
            {
                var allSinonims = DAL.SDK.Kit.Instance.WordSinonims.GetSinonimsByWordId(item.lexemId);
                if (allSinonims.Count < 10)
                {
                    allWordsWithNoSinonims.Add(item);
                }
            }


            foreach (var word in allWordsWithNoSinonims)
            {
                var sinonims = GetSinonimsFromDictooByWord(word.FormNoAcc);  //intins , larg
                var log = word.FormNoAcc;

                foreach (var sinonim in sinonims)
                {
                    if (sinonim != string.Empty && sinonim != " ")
                    {

                        // get words that exists in DB for asociation
                        //var id2 = allWordsDB.Where(w => w.FormNoAcc == sinonim).ToList();
                        var id2 = DAL.SDK.Kit.Instance.Words.GetAllWordsByName(sinonim.ToString().Trim().Replace('ş', 'ș'));
                        RawSQL.executeQuery("exec WordsMainGetByName N'" + sinonim.ToString().Trim().Replace('ş', 'ș') + "'");
                        var id3 = DAL.SDK.Kit.Instance.Words.GetAllWordsByName("rămas");
                        var id4 = DAL.SDK.Kit.Instance.Words.GetAllWordsByName("diferă");

                        // method 1
                        if (id2.Count() != 0)
                        {
                            var id = id2.First().lexemId;
                            DAL.SDK.Kit.Instance.WordSinonims.AddSinonim(word.lexemId, id, DateTime.Now);

                        }
                        else
                        {
                            DAL.SDK.Kit.Instance.WordSinonims.AddSinonimMissingWord(word.lexemId, DateTime.Now, sinonim);
                        }
                    }

                }


            }
        }

        public static string GetPageText(string urlAddress)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string data = string.Empty;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

            }
            return data;
        }

        public static List<string> GetSinonimsFromDictooByWord(string word)
        {
            HtmlWeb web = new HtmlWeb();
            List<string> wordsFinal = new List<string>();
            HtmlDocument document = web.Load("http://dex.dictoo.eu/index.php?cheie=" + word + "&m=0");
            HtmlNode[] mainNode = document.DocumentNode.SelectNodes("//div[@class='cell colspan6']") != null ? document.DocumentNode.SelectNodes("//div[@class='cell colspan6']").ToArray() : null;
            var mainNodeHTML = mainNode[0].InnerHtml;
            if (!mainNodeHTML.Contains("Nu există rezultate pentru termenul sau termenii căutați"))
            {
                var indexOne = -1;
                var indexTwo = -1;

                do
                {
                    indexOne = mainNodeHTML.IndexOf("<script");
                    indexTwo = mainNodeHTML.IndexOf("</script>");
                    if (indexOne != -1 && indexTwo != -1)
                    {
                        mainNodeHTML = mainNodeHTML.Replace(mainNodeHTML.Substring(indexOne, indexTwo - indexOne + 9), string.Empty);
                    }


                } while (indexOne != -1 && indexTwo != -1);

                var mainDiv1 = mainNodeHTML.ToString().Split(new string[] { "<strong>" }, StringSplitOptions.None);
                var mainDiv2 = mainDiv1[1].Split(new string[] { "<span style=\"font-weight: bold;\">Sinonime</span>" }, StringSplitOptions.None);
                var mainDivRawWords = string.Empty;

                if (mainDiv2.Count() > 1)
                {
                    if (mainDiv2[0].Contains("style=\"text-decoration:none;\">" + word + "</a>") ||
                        (mainDiv2[0].IndexOf("style=\"text-decoration:none;\">" + word + "</a>", StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (mainDiv2[0].IndexOf("style=\"text-decoration:none;\">" + word + " (", StringComparison.OrdinalIgnoreCase) >= 0) ||
                        mainDiv2[0].Contains("style=\"text-decoration:none;\">" + word + " ("))
                    {
                        mainDivRawWords = mainDiv2[1];

                        do
                        {
                            indexOne = mainDivRawWords.IndexOf("(");
                            indexTwo = mainDivRawWords.IndexOf(")");
                            if (indexOne != -1 && indexTwo != -1)
                            {
                                mainDivRawWords = mainDivRawWords.Replace(mainDivRawWords.Substring(indexOne, indexTwo - indexOne + 1), string.Empty);
                            }


                        } while (indexOne != -1 && indexTwo != -1);


                        HtmlDocument htmlDocIn = new HtmlDocument();
                        htmlDocIn.LoadHtml(mainDivRawWords);
                        var resultIn = htmlDocIn.DocumentNode.SelectNodes("//a").ToArray();
                        var combinedResults = string.Empty;
                        foreach (var item in resultIn)
                        {
                            //if (item.InnerText.Contains(",") || item.InnerText.Contains(","))
                            combinedResults = combinedResults + " " + item.InnerText;
                        }
                        //var wordsWithSpaces = combinedResults.KeepOnlyAlphabetical();
                        combinedResults = combinedResults.Replace("\n", string.Empty).Replace("\r", string.Empty);
                        var test = combinedResults.Split(new char[] { ',', ';', '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        foreach (var item in test)
                        {
                            wordsFinal.Add(item.KeepOnlyAlphabetical().Trim());
                        }
                        // wordsFinal = wordsWithSpaces.Split(new char[] { ' ' }, StringSplitOptions.None).ToList();
                    }

                    //get
                }
                else
                {
                    //not found?
                    throw new Exception();
                }

            }

            return wordsFinal;
        }

    }
}

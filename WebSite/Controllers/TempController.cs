using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class TempController : Controller
    {
        // GET: Temp
        public ActionResult Index()
        {
            int[] b = new int[] { 10, 3,5, 17, 3, 6, 12, 1, 4 };
            alg1(b, 5);
            return View();
        }

        public static int alg1(int[] a, int b) // lgoritm de sortare a unui array
        {
            int c = 0;
            int d = a.Length - 1;
            while (d >= c)
            {
                int e = alg2(a, c, d, c);  // de unde incepem(c) pana la ce index sa se verifice (d- dimensiunea) si interschimbe numere
                if (e == b) // cat returneaza ca fie = cu lungimea
                {
                    return a[e]; // returneaza maximul?
                }
                else if (e < b)
                {
                    c = e + 1;
                }
                else
                {
                    d = e - 1; // daca indexul e mai mare decat dimensiunea array, scade lungimea lui si devine pozitia modificata -1 (length -1)
                }
            }
            return -1;
        }

        public static int alg2(int[] a, int b, int c, int d)
        {
            int e = a[d];
            int f = b;
            alg3(a, d, c); // 
            for (int i = b; i < c; i++)
            {
                if (a[i] < e) // muta elementele mai mari decat flotantul in locul lor, daca NU e gata inca lista
                {
                    alg3(a, i, f);
                    f++;
                }
            }
            alg3(a, c, f); // muta elementele mai mari decat flotantul in locul lor, daca e gata inca lista
            return f;
        }

        public static void alg3(int[] a, int b, int c) // inter schimba pozitiile a 2 elemente din array
        {
            if (b != c)
            {
                a[c] = (a[b] = (a[c] = a[b] ^ a[c]) ^ a[b]) ^ a[c];
            }
        }


    }
}
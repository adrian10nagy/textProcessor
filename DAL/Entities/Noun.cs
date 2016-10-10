using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Noun
    {
        public Word Word { get; set; }
        public Case Case { get; set; }
        public Multiplicity Multiplicity { get; set; }
        public Proper Proper { get; set; }
        public Gender Gender { get; set; }

        public int WordId{ get; set; }
    }
}

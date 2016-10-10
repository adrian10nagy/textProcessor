using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Verb
    {
        public Word Word { get; set; }
        public Time Time { get; set; }
        public Persons PersonaNumber { get; set; }
        public Multiplicity Multiplicity { get; set; }
        public Mods Mods { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Pronoun
    {
        public Word Word { get; set; }
        public Persons PersonaNumber { get; set; }
        public Multiplicity Multiplicity { get; set; }
        public Gender Gender { get; set; }
    }
}

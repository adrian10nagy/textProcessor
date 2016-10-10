using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class BindingWord
    {
        public Word Word { get; set; }
        public BindingWordType BindingWordType { get; set; }
    }

    public enum BindingWordType
    {
        NA = 1,
        Propozitii = 2,
        Conjunctii = 3
    }
}

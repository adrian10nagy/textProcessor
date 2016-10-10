using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Word
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

        public State StateRef { get; set; }
        public Origin Origin { get; set; }
    }
}


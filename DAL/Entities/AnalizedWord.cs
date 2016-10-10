using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class AnalizedWord
    {
        public string Name { get; set; }
        public bool Exists { get; set; }
        public bool MissedCast { get; set; }
        public Dictionary<int,int> WrongIn {get; set;}
    }
}

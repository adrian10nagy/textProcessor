using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class WordSinonim
    {
        public int Id { get; set; }
        public int WordMainId { get; set; }
        public int WordSinonimId { get; set; }
        public DateTime Created { get; set; }
        public int Approved { get; set; }
    }
}

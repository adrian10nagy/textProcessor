using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class WordInflection
    {
        public int Id { get; set; }
        public int InflextionFormId { get; set; }
        public string FormNoAcc { get; set; }
        public string FromUtf { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public string ModelType { get; set; }
        public int LexemId { get; set; }
    }
}

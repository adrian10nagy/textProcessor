using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Entities
{
    public class WordViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LexemId { get; set; }
        public List<WordDefinitionViewModel> Definitions { get; set; }
    }
}

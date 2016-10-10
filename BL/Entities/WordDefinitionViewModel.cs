using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Entities
{
    public class WordDefinitionViewModel
    {
        public int LexemId { get; set; }
        public int DefinitionId { get; set; }
        public string Lexicon { get; set; }
        public string htmlRep { get; set; }
        public string SourceShortName { get; set; }
        public int SourceDisplayOrder { get; set; }
        public string SourceUrlName { get; set; }
    }
}

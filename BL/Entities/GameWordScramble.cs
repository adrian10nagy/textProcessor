using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Entities
{
    public class GameWordScramble
    {
        public string Name { get; set; }
        public Dictionary<int,char> NameChar { get; set; }
        public Dictionary<int, char> NameScrabled { get; set; }
        public List<WordDefinitionViewModel> Definitions { get; set; }
    }
}

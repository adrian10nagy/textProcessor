using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Entities
{
    public class GameWord
    {
        public string Name { get; set; }
        public List<GameWordChar> Characters { get; set; }
        public List<WordDefinitionViewModel> Definitions { get; set; }
        public List<char> SugestedCharacters { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DefinitionSource
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string UrlName { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Year { get; set; }
        public int DisplayOrder { get; set; }
    }
}

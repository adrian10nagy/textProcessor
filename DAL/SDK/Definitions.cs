using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SDK
{
    public class Definitions
    {
        private static IDefinitionRepository _repository;

        static Definitions()
        {
            _repository = new Repository();
        }

        public List<WordDefinition> GetAllWordDefinitionsByLexemId(int id)
        {
            return _repository.GetAllWordDefinitionsByLexemId(id).OrderBy(x => x.SourceDisplayOrder).ToList();
        }

        public List<WordDefinition> GetAllWordDefinitionsByLexemText(string lexemText)
        {
            return _repository.GetAllWordDefinitionsByLexemText(lexemText);
        }

        public List<DefinitionSource> GetAllDefinitionSources()
        {
            return _repository.GetAllDefinitionSources();
        }

    }
}

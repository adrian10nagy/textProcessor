using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IDefinitionRepository
    {
        List<WordDefinition> GetAllWordDefinitionsByLexemId(int id);
        List<WordDefinition> GetAllWordDefinitionsByLexemText(string lexemText);
        List<DefinitionSource> GetAllDefinitionSources();
    }

    public partial class Repository : IDefinitionRepository
    {
        public List<WordDefinition> GetAllWordDefinitionsByLexemId(int id)
        {
            List<WordDefinition> words = new List<WordDefinition>();
            _dbRead.Execute(
                "WordDefinitionGetByLexemId",
            new[] { 
                new SqlParameter("@id", id), 
            },
                r => words.Add(new WordDefinition()
                {
                    LexemId = Read<int>(r, "LexemId"),
                    DefinitionId = Read<int>(r, "DefinitionId"),
                    Lexicon = Read<string>(r, "Lexicon"),
                    htmlRep = Read<string>(r, "htmlRep"),
                    SourceShortName = Read<string>(r, "SourceShortName"),
                    SourceDisplayOrder = Read<int>(r, "SourceDisplayOrder"),
                    SourceUrlName = Read<string>(r, "SourceUrlName"),
                }));

            return words;
        }

        public List<WordDefinition> GetAllWordDefinitionsByLexemText(string lexemText)
        {
            List<WordDefinition> words = new List<WordDefinition>();
            _dbRead.Execute(
                "WordDefinitionGetByLexemText",
            new[] { 
                new SqlParameter("@lexemText", lexemText), 
            },
                r => words.Add(new WordDefinition()
                {
                    LexemId = Read<int>(r, "LexemId"),
                    DefinitionId = Read<int>(r, "DefinitionId"),
                    Lexicon = Read<string>(r, "Lexicon"),
                    htmlRep = Read<string>(r, "htmlRep"),
                    SourceShortName = Read<string>(r, "SourceShortName"),
                    SourceDisplayOrder = Read<int>(r, "SourceDisplayOrder"),
                    SourceUrlName = Read<string>(r, "SourceUrlName"),
                }));

            return words;
        }

        public List<DefinitionSource> GetAllDefinitionSources()
        {
            List<DefinitionSource> words = new List<DefinitionSource>();
            _dbRead.Execute(
                "WordDefinitionSourceGetAll",
            null,
                r => words.Add(new DefinitionSource()
                {
                    Id = Read<int>(r, "Id"),
                    ShortName = Read<string>(r, "ShortName"),
                    UrlName = Read<string>(r, "UrlName"),
                    Name = Read<string>(r, "Name"),
                    Author = Read<string>(r, "Author"),
                    Publisher = Read<string>(r, "Publisher"),
                    Year = Read<string>(r, "Year"),
                    DisplayOrder = Read<int>(r, "DisplayOrder"),
                }));

            return words;
        }
    }
}

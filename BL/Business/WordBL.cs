using BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Helpers.Extensions;

namespace BL.Business
{
    public class WordBL
    {
        #region Properties

        public List<string> AllWords
        {
            get
            {
                return GetAllWords();
            }
            set
            {
                _allWords = value;
            }
        }

        public List<string> AlreadyCheckedWords
        {
            get
            {
                return GetAlreadyCheckedWords();
            }
            set
            {
                SetAlreadyCheckedWords(value);
            }
        }

        private static List<string> _allWords { get; set; }
        private static List<string> _alreadyCheckedWords { get; set; }

        #endregion

        public static List<string> GetAllWords()
        {
            if (_allWords == null)
            {
                List<string> words = DAL.SDK.Kit.Instance.WordInflections.GetAllInflectedWords();
                _allWords = words;
            }

            return _allWords;
        }

        public static List<string> GetAlreadyCheckedWords()
        {
            return new List<string>();
        }

        public static void SetAlreadyCheckedWords(List<string> AlreadyCheckedWords)
        {
            _alreadyCheckedWords.AddRange(AlreadyCheckedWords);
        }

        public Dictionary<int, List<WordViewModel>> GetRimeWords(string word)
        {
            var rimeWords = new Dictionary<int, List<WordViewModel>>();

            for (int i = word.Length; i > 1; i--)
            {
                var rimeForLength = DAL.SDK.Kit.Instance.WordInflections.GetAllInflectedWordsByLastChars(word.Substring(word.Length - i));
                var rimes = new List<WordViewModel>();
                foreach (var item in rimeForLength)
                {
                    rimes.Add(new WordViewModel
                    {
                        Name = item.FormNoAcc
                    });
                }
                rimeWords.Add(i, rimes);
            }

            return rimeWords;
        }

        public List<WordDefinitionViewModel> GetWordDefinitionsById(int lexemId)
        {
            var definitions = DAL.SDK.Kit.Instance.Definitions.GetAllWordDefinitionsByLexemId(lexemId);
            List<WordDefinitionViewModel> wordDefinitions = new List<WordDefinitionViewModel>();

            foreach (var item in definitions)
            {
                wordDefinitions.Add(new WordDefinitionViewModel
                {
                    htmlRep = item.htmlRep,
                    SourceUrlName = item.SourceUrlName,
                    SourceShortName = item.SourceShortName
                });
            }
            return wordDefinitions;
        }

        public List<WordDefinitionViewModel> GetWordDefinitionsByName(string name)
        {
            var definitions = DAL.SDK.Kit.Instance.Definitions.GetAllWordDefinitionsByLexemText(name);
            List<WordDefinitionViewModel> wordDefinitions = new List<WordDefinitionViewModel>();

            foreach (var item in definitions)
            {
                wordDefinitions.Add(new WordDefinitionViewModel
                {
                    htmlRep = item.htmlRep,
                    SourceUrlName = item.SourceUrlName,
                    SourceShortName = item.SourceShortName
                });
            }
            return wordDefinitions;
        }

        public WordViewModel WordOfTheDay(DateTime date)
        {
            var word = DAL.SDK.Kit.Instance.Words.GetWordOfTheDay(date);
            var wordDefinitions = GetWordDefinitionsById(word.lexemId);
            WordViewModel wordOfTheDay = new WordViewModel
            {
                LexemId = word.lexemId,
                Name = word.FormNoAcc,
                Definitions = wordDefinitions
            };

            return wordOfTheDay;
        }


        public void AddAlreadyCheckedWords(List<string> words)
        {
            if(_alreadyCheckedWords == null)
            {
                _alreadyCheckedWords = new List<string>();
            }
            _alreadyCheckedWords.AddRange(words);
        }

        public GameWord GetGuessGameWord()
        {
            var randomWord = DAL.SDK.Kit.Instance.Words.GetRandomMainWord(0.99, 7);
            GameWord word = new GameWord();
            word.Name = randomWord.FormNoAcc;
            word.Characters = getGuessWordCharacters(randomWord.FormNoAcc);
            word.Definitions = BL.Business.KitBL.Instance.WordBL.GetWordDefinitionsByName(randomWord.FormNoAcc);
            word.SugestedCharacters = getSuggestedCharacters(word.Characters.Where(c => c.IsVisible == false).Select(s => s.Character).ToList());
            for (int i = 0; i < word.Characters.Count(); i++)
            {
                word.Characters[i].Position = i;
            }
            return word;
        }

        public GameWordScramble GetScrambleGameWord()
        {
            var randomWord = DAL.SDK.Kit.Instance.Words.GetRandomMainWord(0.99, 7);
            GameWordScramble word = new GameWordScramble();
            word.Name = randomWord.FormNoAcc;
            word.NameChar = getScrambleWordCharacters(randomWord.FormNoAcc);
            word.Definitions = BL.Business.KitBL.Instance.WordBL.GetWordDefinitionsByName(randomWord.FormNoAcc);
            word.NameScrabled = scrambleWordCharacters(randomWord.FormNoAcc);
            return word;
        }

        public List<GameWordChar> getGuessWordCharacters(string word, int level = 1)
        {
            Random rnd = new Random();
            var wordChar = word.ToArray();

            List<GameWordChar> gameWordChars = new List<GameWordChar>();
            List<int> hiddenChars = new List<int>();
            var shouldRetry = false;
            //var howManyHidden = wordChar.Count();
            var howManyHidden = 1;

            for (int i = 0; i < howManyHidden; i++)
            {
                do
                {
                    shouldRetry = false;
                    int poz = rnd.Next(wordChar.Length);
                    if (hiddenChars.Contains(poz))
                    {
                        shouldRetry = true;
                    }
                    else
                    {
                        shouldRetry = false;
                        hiddenChars.Add(poz);
                    }
                } while (shouldRetry);
            }

            for (int i = 0; i < wordChar.Length; i++)
            {
                var gameWordChar = new GameWordChar();
                if (hiddenChars.Contains(i))
                {
                    gameWordChar.IsVisible = false;
                }
                else
                {
                    gameWordChar.IsVisible = true;
                }
                gameWordChar.Character = wordChar[i];
                gameWordChars.Add(gameWordChar);
            }

            return gameWordChars;
        }

        public List<char> getSuggestedCharacters(List<char> chars)
        {
            List<char> suggestedChars = new List<char>();
            foreach (var c in chars)
            {
                suggestedChars.Add(Char.ToUpper(c));
            }

            bool shouldRetry = false;
            var random = new Random();

            for (int i = 0; i < 3; i++)
            {
                char character = 'a';
                do
                {
                    shouldRetry = false;
                    character = (char)('A' + random.Next(0, 26));
                    if (suggestedChars.Contains(character))
                    {
                        shouldRetry = true;
                    }
                } while (shouldRetry);
                suggestedChars.Add(character);
            }

            return suggestedChars;
        }

        public Dictionary<int, char> getScrambleWordCharacters(string wordName)
        {
            Dictionary<int, char> wordChars = new Dictionary<int, char>();
            for (int i = 0; i < wordName.Length; i++)
            {
                wordChars.Add(i, wordName[i]);
            }
            return wordChars;
        }

        public Dictionary<int, char> scrambleWordCharacters(string wordName)
        {
            var rnd = new Random();
            Dictionary<int, char> wordChars = new Dictionary<int, char>();

            var wordNameScreambled = wordName.OrderBy(item => rnd.Next()).ToList();
            for (int i = 0; i < wordNameScreambled.Count(); i++)
            {
                wordChars.Add(i, wordNameScreambled[i]);
            }
            return wordChars;
        }


        // temporar
        public static List<DAL.Entities.AnalizedWord> getUnrecognizedWords(string textString)
        {
            List<string> wordList = new List<string>();
            var analyzedWords = new List<DAL.Entities.AnalizedWord>();

            List<DAL.Entities.Word> allWords = DAL.SDK.Kit.Instance.Words.GetAll();
            List<string> listSentences = textString.Split(new Char[] { '.', '!', '?', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var sentence in listSentences)
            {
                var listWords = sentence.Split(new Char[] { ' ', ',', '"' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                List<DAL.Entities.AnalizedWord> existanceWords = DAL.Helpers.WordHelpers.Existence.GetConvertedWordsByText(listWords);
                analyzedWords.AddRange(existanceWords);
            }
            var unidentifiedWords = analyzedWords.Where(word => word.Exists == false).ToList();

            return unidentifiedWords;
        }

        public static Dictionary<int, int> getUnrecognizedWordsIndexes(string mainText)
        {
            var unidentifiedWords = getUnrecognizedWords(mainText);
            var wordsIndexes = new Dictionary<int, int>();

            foreach (var word in unidentifiedWords)
            {
                var indexes = mainText.AllIndexesOf(word.Name);
                foreach (var index in indexes)
                {
                    if (!wordsIndexes.Contains(index))
                    {
                        wordsIndexes.Add(index.Key, index.Value);
                    }
                }
            }

            return wordsIndexes;
        }
    }
}

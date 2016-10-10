using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;

using DAL.SDK;
using DAL.Entities;
using BL.Entities;
using BL.Business;
namespace UnitTests.Game
{
    class ScrambleWordTests
    {
        private Mock<IWordRepository> wordRepositoryMock;
        private Mock<IDefinitionRepository> definitionRepositoryMock;

        private Words words;
        private List<WordDefinition> wordDefinitions;
        private WordMain wordMain;

        Mock<IWordRepository> _mRandomRep;
        Words _sdkrandomFaqs;

        [SetUp]
        public void setup()
        {
            _mRandomRep = new Mock<IWordRepository>();
            //kjkjk_sdkrandomFaqs = new Words(_mRandomRep.Object);
            
            this.wordRepositoryMock.Setup(call => call.GetRandomMainWord(It.IsAny<double>(), It.IsAny<int>()))
                .Returns(this.wordMain);
            this.definitionRepositoryMock.Setup(call => call.GetAllWordDefinitionsByLexemText(It.IsAny<string>()))
                .Returns(this.wordDefinitions);
        }

        [Test]
        public void GetAllAccounts_IfBkAndCepHaveSameEcAccount_BkLastChangeDateGreater_ReturnsBkAccount()
        {
            var result = this.words.GetWordOfTheDay(It.IsAny<DateTime>());
        }

        [TestCase(0)]
        [TestCase(0.99)]
        [TestCase(2)]
        public void Game_ScrambleWord_Default(int frequency)
        {
            // arrange
            var expected = new GameWordScramble
            {
                Definitions = new List<WordDefinitionViewModel>()
            };

            var x = KitBL.Instance.WordBL.GetScrambleGameWord();

            // act
            var result = this.words.GetRandomMainWord(It.IsAny<double>(), It.IsAny<int>());
            

            // assert is handled by the ExpectedException 
            Assert.AreEqual(expected.Definitions.Count(), x.Definitions.Count());
        }
    }
}

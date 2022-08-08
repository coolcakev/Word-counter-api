using Businnes_logic.Services;
using Businnes_logic.TextStrategy;
using Businnes_logic.TextStrategy.Interfaces;
using Businnes_logic.TextStrategy.StrategyItem;
using Domain.DTOs;
using Domain.Enums.TextEnums;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class TextServiceTest
    {
        private readonly TextService _testService;
        public TextServiceTest()
        {
            var strategyTypes = new ITextType[] { new OneWord(), new TwoWords(), new ThreeWords() };
            var textStrategy = new TextStrategy(strategyTypes);
            _testService = new TextService(textStrategy);
        }
        [Fact]
        public async Task GetWord_With_ExludedWords_Null_Return_EmptyArray()
        {
            //Arrange
            var textMode = TextMode.One;
            var textDTO = new TextDTO()
            {
                Count = 1,
                Text = ""
            };
            ExludedWords exludedWords = null;
            //Action
            var actual = await _testService.GetWord(textMode, textDTO, exludedWords);
            //Assertaion
            Assert.Empty(actual);
        }
        [Fact]
        public async Task GetWord_With_TextDTO_Null_Return_EmptyArray()
        {
            //Arrange
            var textMode = TextMode.One;
            TextDTO textDTO = null;
            ExludedWords exludedWords = new ExludedWords();
            //Action
            var actual = await _testService.GetWord(textMode, textDTO, exludedWords);
            //Assertaion
            Assert.Empty(actual);
        }
        [Fact]
        public async Task GetWord_For_One_Return_WordStatisticArray()
        {
            //Arrange
            var textMode = TextMode.One;
            var textDTO = new TextDTO()
            {
                Count = 2,
                Text = "hello hello baby at"
            };
            ExludedWords exludedWords = new ExludedWords()
            {
                Articles = new List<string>(),
                OtherWords = new List<string>() { "baby" },
                PersonalPronouns = new List<string>(),
                Preposition = new List<string>(),
                SpecialWord = new List<string>(),
                TimeWord = new List<string>(),
            };

            var expected = new List<WordStatistic>() {
                new WordStatistic(){
                  Count = 2,
                  Word="hello",
                  Frequency=2*1.0/3*100,
                },
                new WordStatistic(){
                  Count = 1,
                  Word="at",
                  Frequency=1*1.0/3*100,
                }
            };
            //Action
            var actual =( await _testService.GetWord(textMode, textDTO, exludedWords)).ToList();
      
            //Assertaion
            expected.Should().BeEquivalentTo(actual);
        }
        [Fact]
        public async Task GetWord_For_Two_Return_WordStatisticArray()
        {
            //Arrange
            var textMode = TextMode.Two;
            var textDTO = new TextDTO()
            {
                Count = 2,
                Text = "hello hello hello baby at"
            };
            ExludedWords exludedWords = new ExludedWords()
            {
                Articles = new List<string>(),
                OtherWords = new List<string>() { "baby" },
                PersonalPronouns = new List<string>(),
                Preposition = new List<string>(),
                SpecialWord = new List<string>(),
                TimeWord = new List<string>(),
            };

            var expected = new List<WordStatistic>() {
                new WordStatistic(){
                  Count = 2,
                  Word="hello hello",
                  Frequency=2.0/3*100,
                },
                new WordStatistic(){
                  Count = 1,
                  Word="hello at",
                  Frequency=1.0/3*100,
                }
            };
            //Action
            var actual = (await _testService.GetWord(textMode, textDTO, exludedWords)).ToList();

            //Assertaion
            expected.Should().BeEquivalentTo(actual);
        }
        [Fact]
        public async Task GetWord_For_Three_Return_WordStatisticArray()
        {
            //Arrange
            var textMode = TextMode.Three;
            var textDTO = new TextDTO()
            {
                Count = 2,
                Text = "hello hello baby at boat"
            };
            ExludedWords exludedWords = new ExludedWords()
            {
                Articles = new List<string>(),
                OtherWords = new List<string>() { "baby" },
                PersonalPronouns = new List<string>(),
                Preposition = new List<string>(),
                SpecialWord = new List<string>(),
                TimeWord = new List<string>(),
            };

            var expected = new List<WordStatistic>() {
                new WordStatistic(){
                  Count = 1,
                  Word="hello hello at",
                  Frequency=1.0/2*100,
                },
                new WordStatistic(){
                  Count = 1,
                  Word="hello at boat",
                  Frequency=1.0/2*100,
                }
            };
            //Action
            var actual = (await _testService.GetWord(textMode, textDTO, exludedWords)).ToList();

            //Assertaion
            expected.Should().BeEquivalentTo(actual);
        }
    }
}

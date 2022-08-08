using Businnes_logic.TextStrategy;
using Businnes_logic.TextStrategy.Interfaces;
using Businnes_logic.TextStrategy.StrategyItem;
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
    public class TextStrategyTest
    {
        private readonly TextStrategy _textStrategy;

        public TextStrategyTest()
        {
            var strategyTypes = new ITextType[] { new OneWord(), new TwoWords(), new ThreeWords() };

            _textStrategy = new TextStrategy(strategyTypes);
        }
        [Fact]
        public void GetWords_NeedElement_Is_Null_ReturnExeption()
        {
            //Arrange
            List<string> needsElement = null;
            var textMode = TextMode.One;

            //Act
            //Arrange            
            Assert.Throws<ArgumentNullException>(() => _textStrategy.GetWords(needsElement, textMode));
        }
        [Fact]
        public void GetWords_TextMode_Is_Null_ReturnExeption()
        {
            //Arrange
            var strategyTypes = new ITextType[] { new OneWord(), new TwoWords() };
            var textStrategy = new TextStrategy(strategyTypes);
            var needsElement = new List<string>();
            var textMode = TextMode.Three;

            //Act
            //Arrange            
            Assert.Throws<ArgumentNullException>(() => textStrategy.GetWords(needsElement, textMode));
        }

        [Fact]
        public void GetWords_OneWord_ReturnList()
        {
            //Arrange
            var strategyTypes = new ITextType[] { new OneWord(), new TwoWords(), new ThreeWords() };

            var needsElement = new List<string>() { "hello", "world", "!" };
            var textMode = TextMode.One;

            var expected = new List<string>() { "hello", "world", "!" };

            //Act
            var actual = _textStrategy.GetWords(needsElement, textMode);

            //Arrange            
            expected.Should().BeEquivalentTo(actual);

        }
        [Fact]
        public void GetWords_TwoWord_ReturnList()
        {
            //Arrange
            var strategyTypes = new ITextType[] { new OneWord(), new TwoWords(), new ThreeWords() };

            var needsElement = new List<string>() { "hello", "world", "!" };
            var textMode = TextMode.Two;

            var expected = new List<string>() { "hello world", "world !" };

            //Act
            var actual = _textStrategy.GetWords(needsElement, textMode);

            //Arrange            
            expected.Should().BeEquivalentTo(actual);

        }
        [Fact]
        public void GetWords_ThreeWord_ReturnList()
        {
            //Arrange
            var strategyTypes = new ITextType[] { new OneWord(), new TwoWords(), new ThreeWords() };

            var needsElement = new List<string>() { "hello", "world", "!" };
            var textMode = TextMode.Three;

            var expected = new List<string>() { "hello world !" };

            //Act
            var actual = _textStrategy.GetWords(needsElement, textMode);

            //Arrange            
            expected.Should().BeEquivalentTo(actual);

        }
    }
}

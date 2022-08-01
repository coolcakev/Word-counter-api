using Businnes_logic.TextStrategy.Interfaces;
using Businnes_logic.TextStrategy.StrategyItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businnes_logic.TextStrategy
{
    public class TextFactory : ITextFactory
    {
        private readonly OneWord _oneWord;
        private readonly TwoWords _twoWords;
        private readonly ThreeWords _threeWords;

        public TextFactory(OneWord oneWord, TwoWords twoWords, ThreeWords threeWords)
        {
            _oneWord = oneWord;
            _twoWords = twoWords;
            _threeWords = threeWords;
        }

        public ITextType[] Create() => new ITextType[] { _oneWord, _twoWords, _threeWords };


    }
}

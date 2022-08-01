using Businnes_logic.TextStrategy.Interfaces;
using Domain.Enums.TextEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businnes_logic.TextStrategy.StrategyItem
{
    public class OneWord : ITextType
    {
        public TextMode Mode => TextMode.One;

        public List<string> GetWords(List<string> needElements)
        {
            return needElements;
        }
    }
}

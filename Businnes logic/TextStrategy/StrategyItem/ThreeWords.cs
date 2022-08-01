using Businnes_logic.TextStrategy.Interfaces;
using Domain.Enums.TextEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businnes_logic.TextStrategy.StrategyItem
{
    public class ThreeWords : ITextType
    {
        public TextMode Mode => TextMode.Three;

        public List<string> GetWords(List<string> needElements)
        {
            var modifiedNeedElements = new List<string>();
            for (int i = 0; i < needElements.Count - 2; i++)
            {
                modifiedNeedElements.Add($"{needElements[i]} {needElements[i + 1]} {needElements[i + 2]}");
            }
            return modifiedNeedElements;
        }
    }
}

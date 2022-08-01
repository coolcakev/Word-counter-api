using Businnes_logic.TextStrategy.Interfaces;
using Domain.Enums.TextEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businnes_logic.TextStrategy
{
    public class TextStrategy: ITextStrategy
	{
		private readonly ITextType[] _operators;

		public TextStrategy(ITextType[] operators)
		{
			_operators = operators;
		}		

        public List<string> GetWords(List<string> needElements, TextMode mode)
        {
            return _operators.FirstOrDefault(x=>x.Mode==mode)?.GetWords(needElements) ?? throw new ArgumentNullException(nameof(mode));

		}
    }
}

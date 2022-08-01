using Domain.Enums.TextEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businnes_logic.TextStrategy.Interfaces
{
    public interface ITextStrategy
    {
        List<string> GetWords(List<string> needElements, TextMode mode);
    }
}

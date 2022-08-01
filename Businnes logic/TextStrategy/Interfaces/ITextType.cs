using Domain.Enums.TextEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businnes_logic.TextStrategy.Interfaces
{
    public interface ITextType
    {
        TextMode Mode { get; }

        List<string> GetWords(List<string> needElements);

    }
}

using Domain.DTOs;
using Domain.Enums.TextEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businnes_logic.Interfaces
{
    public interface ITextService
    {
        Task<GlobalStatistic> GetWord(TextMode mode, TextDTO textDTO, ExludedWords exludedWords);       
    }
}

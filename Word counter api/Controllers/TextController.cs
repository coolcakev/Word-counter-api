using Businnes_logic.Interfaces;
using Businnes_logic.Services;
using Domain.DTOs;
using Domain.Enums;
using Domain.Enums.TextEnums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Word_counter_api.Helpers;

namespace Word_counter_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly ITextService _tesxService;
        private readonly IMemoryCache _cache;

        public TextController(ITextService tesxService, IMemoryCache cache)
        {
            _tesxService = tesxService;
            _cache = cache;
        }
        [HttpPost("{textMode}")]
        public async Task<IActionResult> GetWord(string textMode,[FromBody]TextDTO textDTO)
        {
            var isSuccessParse = Enum.TryParse(typeof(TextMode), textMode,true,out object result);
            if (textDTO == null|| !isSuccessParse) {
                return BadRequest();
            }

            var excludedWordsCashe = CacheHelper.GetItemFromCacheMemory<ExludedWords>(CasheType.ExcludedWords.ToString(), _cache);

            var words = await _tesxService.GetWord((TextMode)result, textDTO, excludedWordsCashe);
            return Ok(words);
        }       
    }
}

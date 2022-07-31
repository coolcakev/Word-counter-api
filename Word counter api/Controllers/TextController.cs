using Businnes_logic.Interfaces;
using Businnes_logic.Services;
using Domain.DTOs;
using Domain.Enums;
using Domain.Enums.TextEnums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        [HttpPost("one")]
        public async Task<IActionResult> GetOneWord([FromBody]TextDTO textDTO)
        {
            if (textDTO == null) {
                return BadRequest();
            }

            var excludedWordsCashe = CacheHelper.GetItemFromCacheMemory<ExludedWords>(CasheType.ExcludedWords.ToString(), _cache);

            var result = await _tesxService.GetWord(TextMode.One, textDTO, excludedWordsCashe);
            return Ok(result);
        }
        [HttpPost("two")]
        public async Task<IActionResult> GetTwoWord(TextDTO textDTO)
        {
            if (textDTO == null)
            {
                return BadRequest();
            }
            var excludedWordsCashe = CacheHelper.GetItemFromCacheMemory<ExludedWords>(CasheType.ExcludedWords.ToString(), _cache);

            var result = await _tesxService.GetWord(TextMode.Two, textDTO, excludedWordsCashe);
            return Ok(result);
        }
        [HttpPost("three")]
        public async Task<IActionResult> GetTreeWord(TextDTO textDTO)
        {
            if (textDTO == null)
            {
                return BadRequest();
            }
            var excludedWordsCashe = CacheHelper.GetItemFromCacheMemory<ExludedWords>(CasheType.ExcludedWords.ToString(), _cache);

            var result = await _tesxService.GetWord(TextMode.Three, textDTO, excludedWordsCashe);
            return Ok(result);
        }
    }
}

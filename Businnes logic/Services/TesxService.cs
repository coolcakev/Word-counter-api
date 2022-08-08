using Businnes_logic.Interfaces;
using Businnes_logic.TextStrategy.Interfaces;
using Domain.DTOs;
using Domain.Enums.TextEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Businnes_logic.Services
{
    public class TextService : ITextService
    {
        private readonly ITextStrategy _textStrategy;

        public TextService(ITextStrategy textStrategy)
        {
            _textStrategy = textStrategy;
        }

        public Task<IEnumerable<WordStatistic>> GetWord(TextMode mode, TextDTO textDTO, ExludedWords exludedWords)
        {
            if (textDTO ==null|| string.IsNullOrWhiteSpace(textDTO.Text))
            {
                return Task.FromResult(Enumerable.Empty<WordStatistic>());
            }

            return Task.Run(() =>
            {
                var needElements = GetNeedWord(textDTO.Text, exludedWords);

                needElements = _textStrategy.GetWords(needElements, mode);

                var groupedWords = needElements.GroupBy(x => x).OrderByDescending(x => x.Count()).ToList();

                var total = groupedWords.Sum(x => x.Count());

                if (textDTO.Count.HasValue) {
                    groupedWords = groupedWords.Take(textDTO.Count.Value).ToList();
                }
                var wordStatistics = groupedWords.Select(groupedWord => new WordStatistic() {
                    Word = groupedWord.Key,
                    Count = groupedWord.Count(),
                    Frequency= (groupedWord.Count()*1.0/ total)*100,
                });
                
                return wordStatistics;
            });

        }
        private List<string> GetNeedWord(string text, ExludedWords exludedWords)
        {
            if (text==null) {
                return new List<string>();
            }

            var exludesString = new List<string>();
            exludesString.AddRange(exludedWords?.Articles??new List<string>());
            exludesString.AddRange(exludedWords?.Preposition ?? new List<string>());
            exludesString.AddRange(exludedWords?.PersonalPronouns ?? new List<string>());
            exludesString.AddRange(exludedWords?.SpecialWord ?? new List<string>());
            exludesString.AddRange(exludedWords?.TimeWord ?? new List<string>());
            exludesString.AddRange(exludedWords?.OtherWords ?? new List<string>());

            var textArray = text.Split(',', ' ', '.','\n');
            var needElements = new List<string>();

            foreach (var word in textArray)
            {
                if (word.Contains("\'")) {
                    continue;
                }

                var regex = new Regex(@"[A-Za-z]+");
                var match = regex.Match(word);
                if (match.Success && !exludesString.Contains(word.ToLower()))
                {
                    needElements.Add(match.Value.ToLower());
                }
            }
            return needElements;
        }

    }
}

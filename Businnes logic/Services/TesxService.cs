using Businnes_logic.Interfaces;
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
        public Task<GlobalStatistic> GetWord(TextMode mode, TextDTO textDTO, ExludedWords exludedWords)
        {
            return Task.Run(() =>
            {
                var globalStatistic = new GlobalStatistic()
                {
                    Total = 0,
                    WordStatistics = new List<WordStatistic>()
                };
                if (string.IsNullOrWhiteSpace(textDTO.Text))
                {
                    return globalStatistic;
                }

                var needElements = GetNeedWord(textDTO.Text, exludedWords);

                needElements = GetModifiedByGroup(needElements, mode);        
                
                var groupedWords = needElements.GroupBy(x => x).OrderByDescending(x => x.Count());

                globalStatistic.Total += groupedWords.Sum(x => x.Count());
                foreach (var groupedWord in groupedWords.Take(textDTO.Count))
                {

                    var wordStatistic = new WordStatistic()
                    {
                        Word = groupedWord.Key,
                        Count = groupedWord.Count(),
                    };

                    globalStatistic.WordStatistics.Add(wordStatistic);
                }
                return globalStatistic;
            });

        }
        private List<string> GetNeedWord(string text, ExludedWords exludedWords)
        {

            var exludesString = new List<string>();
            exludesString.AddRange(exludedWords.Articles);
            exludesString.AddRange(exludedWords.Preposition);

            var textArray = text.Split(',', ' ', '.');
            var needElements = new List<string>();

            foreach (var word in textArray)
            {
                var regex = new Regex(@"[A-Za-z]+");
                var match = regex.Match(word);
                if (match.Success && !exludesString.Contains(word))
                {
                    needElements.Add(match.Value);
                }
            }
            return needElements;
        }
        private List<string> GetModifiedByGroup(List<string> needElements, TextMode mode) => mode switch
        {
            TextMode.One => needElements,
            TextMode.Two => GetTwoWord(needElements),
            TextMode.Three => GetTreeWord(needElements),
        };

        private List<string> GetTreeWord(List<string> needElements)
        {
            var modifiedNeedElements = new List<string>();
            for (int i = 0; i < needElements.Count - 2; i++)
            {
                modifiedNeedElements.Add($"{needElements[i]} {needElements[i + 1]} {needElements[i + 2]}");
            }
            return modifiedNeedElements;
        }

        private List<string> GetTwoWord(List<string> needElements)
        {
            var modifiedNeedElements = new List<string>();
            for (int i = 0; i < needElements.Count - 1; i++)
            {
                modifiedNeedElements.Add($"{needElements[i]} {needElements[i + 1]}");
            }
            return modifiedNeedElements;
        }
       

    }
}

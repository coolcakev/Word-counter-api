﻿using Businnes_logic.Interfaces;
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
        public Task<IEnumerable<WordStatistic>> GetWord(TextMode mode, TextDTO textDTO, ExludedWords exludedWords)
        {
            if (string.IsNullOrWhiteSpace(textDTO.Text))
            {
                return Task.FromResult(Enumerable.Empty<WordStatistic>());
            }

            return Task.Run(() =>
            {
                var needElements = GetNeedWord(textDTO.Text, exludedWords);

                needElements = GetModifiedByGroup(needElements, mode);

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

            var exludesString = new List<string>();
            exludesString.AddRange(exludedWords.Articles);
            exludesString.AddRange(exludedWords.Preposition);
            exludesString.AddRange(exludedWords.PersonalPronouns);
            exludesString.AddRange(exludedWords.SpecialWord);
            exludesString.AddRange(exludedWords.TimeWord);
            exludesString.AddRange(exludedWords.OtherWords);

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ExludedWords
    {
        public List<string> Articles { get; set; }
        public List<string> Preposition { get; set; }
        public List<string> PersonalPronouns { get; set; }
        public List<string> SpecialWord { get; set; }
        public List<string> TimeWord { get; set; }
        public List<string> OtherWords { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public  class GlobalStatistic
    {
        public List<WordStatistic> WordStatistics { get; set; }
        public int Total { get; set; }
    }
}

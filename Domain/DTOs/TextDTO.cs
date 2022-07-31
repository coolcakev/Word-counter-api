﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class TextDTO
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public int Count { get; set; }
    }
}

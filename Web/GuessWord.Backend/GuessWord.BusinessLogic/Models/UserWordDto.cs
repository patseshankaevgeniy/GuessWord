﻿using System.Collections.Generic;

namespace GuessWord.BusinessLogic.Models
{
    public class UserWordDto
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public int? Status { get; set; }
        public List<string> Translations { get; set; }
    }
}

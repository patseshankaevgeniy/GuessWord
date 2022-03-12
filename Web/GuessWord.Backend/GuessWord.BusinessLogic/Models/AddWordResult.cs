using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessWord.BusinessLogic.Models
{
    public class AddWordResult
    {
        public bool Succeeded { get; set; }
        public WordErrorType wordErrorType { get; set; }
    }
}

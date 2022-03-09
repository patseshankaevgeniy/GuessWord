using System.ComponentModel.DataAnnotations;

namespace GuessWord.Domain.Entities
{
    public class UserAuthModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

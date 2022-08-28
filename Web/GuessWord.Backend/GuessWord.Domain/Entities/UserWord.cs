using GuessWord.Domain.Enums;

namespace GuessWord.Domain.Entities
{
    public class UserWord : BaseEntity
    {
        public int UserId { get; set; }
        public int WordId { get; set; }
        public int Complexity { get; set; }
        public WordStatus Status { get; set; }
        public int TargetRepeatNumber { get; set; }
        public int RepeatNumber { get; set; }

        public User User { get; set; }
        public Word Word { get; set; }
    }
}

namespace GuessWord.Domain.Entities
{
    public class Translation
    {
        public int Id { get; set; }
        public int WordId { get; set; }
        public int WordTranslationId { get; set; }
    }
}

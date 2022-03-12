namespace GuessWord.Domain.Entities
{
    public class WordTranslation
    {
        public int Id { get; set; }
        public int WordId { get; set; }
        public int WordTranslationId { get; set; }

        public Word Word { get; set; }
        public Word Translation { get; set; }
    }
}

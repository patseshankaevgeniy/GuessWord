namespace GuessWord.Domain.Entities
{
    public class WordTranslation : BaseEntity
    {
        public int WordId { get; set; }
        public int TranslationId { get; set; }

        public Word Word { get; set; }
        public Word Translation { get; set; }
    }
}

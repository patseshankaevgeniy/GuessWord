namespace GuessWord.Domain.Entities
{
    public class WordWithTranslation : BaseEntity
    {
        public string Value { get; set; }
        public string Translation { get; set; }
    }
}

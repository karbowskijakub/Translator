namespace Translator.Models
{
    public class Translate
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string TranslatedText { get; set; }
        public DateTime Date { get; set; }
    }
}

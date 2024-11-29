using Newtonsoft.Json;
namespace Translator.Services
{
    public class TranslationService
    {
        private readonly HttpClient _httpClient;

        public TranslationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Translate(string inputText, string translationType)
        {
            var response = await _httpClient.GetAsync($"https://api.funtranslations.com/translate/{translationType}.json?text={inputText}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(content);
            return result.contents.translated;
        }
    }
}

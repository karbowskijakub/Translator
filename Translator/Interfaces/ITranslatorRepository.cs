using PagedList;
using Translator.Models;

namespace Translator.Interfaces
{
    public interface ITranslatorRepository
    {
        Task AddTranslationField(string text, string translatedText);
        Task<IEnumerable<Translate>> GetAllFields();
        Task<IPagedList<Translate>> GetFilteredTranslations(
            DateTime? startDate,
            DateTime? endDate,
            string textFilter,
            string translatedTextFilter,
            int pageNumber = 1,
            int pageSize = 10);
        Task<IEnumerable<object>> GetTranslationTypes();


    }
}

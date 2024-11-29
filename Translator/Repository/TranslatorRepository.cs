using Translator.Data;
using Microsoft.EntityFrameworkCore;
using Translator.Interfaces;
using Translator.Models;
using Translator.Enums;
using PagedList;


namespace Translator.Repositories
{
    public class TranslatorRepository : ITranslatorRepository
    {
        private readonly AppDbContext _context;

        public TranslatorRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddTranslationField(string text, string translatedText)
        {
            var field = new Translate
            {
                Id = Guid.NewGuid(),
                Text = text,
                TranslatedText = translatedText,
                Date = DateTime.Now
            };

            _context.TranslationFields.Add(field);
            await _context.SaveChangesAsync();
        }

        public async Task<IPagedList<Translate>> GetFilteredTranslations(
            DateTime? startDate,
            DateTime? endDate,
            string textFilter,
            string translatedTextFilter,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var translations = _context.TranslationFields.AsQueryable();

            if (startDate.HasValue)
            {
                translations = translations.Where(t => t.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                translations = translations.Where(t => t.Date <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(textFilter))
            {
                translations = translations.Where(t => t.Text.ToLower().StartsWith(textFilter.ToLower()));
            }

            if (!string.IsNullOrEmpty(translatedTextFilter))
            {
                translations = translations.Where(t => t.TranslatedText.ToLower().StartsWith(translatedTextFilter.ToLower()));
            }

            return translations
                .OrderBy(t => t.Date)
                .ToPagedList(pageNumber, pageSize); 
        }

        public async Task<IEnumerable<object>> GetTranslationTypes()
        {
            var translationTypes = Enum.GetValues(typeof(TranslationType))
                                        .Cast<TranslationType>()
                                        .Select(e => new { Value = (int)e, Name = e.ToString() })
                                        .ToList();

            return await Task.FromResult(translationTypes);
        }

        public async Task<IEnumerable<Translate>> GetAllFields()
        {
            return await _context.TranslationFields.ToListAsync();
        }
    }
}

using Translator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Translator.Controllers
{
    public class ManageController : Controller
    {
        private readonly ITranslatorRepository _translatorRepository;

        public ManageController(ITranslatorRepository translatorRepository)
        {
            _translatorRepository = translatorRepository;
        }

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, string textFilter, string translatedTextFilter, int pageNumber = 1, int pageSize = 10)
        {
            var translations = await _translatorRepository.GetFilteredTranslations(startDate, endDate, textFilter, translatedTextFilter, pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = translations.PageCount; 

            return View(translations);
        }

    }
}

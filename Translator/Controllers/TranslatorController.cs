using Microsoft.AspNetCore.Mvc;
using Translator.Interfaces;
using Translator.Services;
using Translator.Models;

namespace Translator.Controllers
{
    [Route("Translator")] 
    [ApiController]
    public class TranslatorController : Controller
    {
        private readonly ITranslatorRepository _translatorRepository;
        private readonly TranslationService _translationService;

        public TranslatorController(TranslationService translationService, ITranslatorRepository translatorRepository)
        {
            _translationService = translationService;
            _translatorRepository = translatorRepository;
        }

        [HttpGet("translation-types")]
        public async Task<IActionResult> GetTranslationTypes()
        {
            try
            {
                var translationTypes = await _translatorRepository.GetTranslationTypes();
                return Ok(translationTypes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = "An error:", details = ex.Message });
            }
        }

        [HttpPost("Translate")]
        public async Task<IActionResult> Translate([FromBody] TranslationRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Input))
            {
                return BadRequest(new { message = "Input text cannot be empty." });
            }

            try
            {
                var translatedText = await _translationService.Translate(request.Input, request.TranslationType);

                await _translatorRepository.AddTranslationField(request.Input, translatedText);

                return Ok(new { TranslatedText = translatedText });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = "An error:", details = ex.Message });
            }
        }


        [HttpGet("history")]
        public async Task<IActionResult> History()
        {
            try
            {
                var records = await _translatorRepository.GetAllFields();
                return Ok(records);  
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = "An error:", details = ex.Message });
            }
        }
    }
}

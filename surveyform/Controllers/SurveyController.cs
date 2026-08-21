using Microsoft.AspNetCore.Mvc;
using SurveyApp.Data;
using SurveyApp.Models;
using SurveyApp.Services;

namespace SurveyApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPdfService _pdfService;
        private readonly IEmailService _emailService;
        private readonly ILogger<SurveyController> _logger;

        public SurveyController(ApplicationDbContext context, IPdfService pdfService,
            IEmailService emailService, ILogger<SurveyController> logger)
        {
            _context = context;
            _pdfService = pdfService;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new SurveyResponse());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SurveyResponse response)
        {
            if (!ModelState.IsValid)
            {
                return View(response);
            }

            response.SubmittedAt = DateTime.Now;

            _context.SurveyResponses.Add(response);
            int rowsAffected = await _context.SaveChangesAsync();

            var pdfBytes = _pdfService.GenerateSurveyPdf(response);
            var fileName = _pdfService.GenerateFileName(response);

            try
            {
                await _emailService.SendSurveyEmailAsync(response.FullName, response.Email,
                    response.SubmittedAt, pdfBytes, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send survey email for response {Id}", response.Id);
            }

            return RedirectToAction("Success", new { id = response.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Success(int id)
        {
            var response = await _context.SurveyResponses.FindAsync(id);
            if (response == null) return RedirectToAction("Index", "Home");
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadPdf(int id)
        {
            var response = await _context.SurveyResponses.FindAsync(id);
            if (response == null) return NotFound();

            var pdfBytes = _pdfService.GenerateSurveyPdf(response);
            var fileName = _pdfService.GenerateFileName(response);
            return File(pdfBytes, "application/pdf", fileName);
        }
    }
}
using SurveyApp.Models;

namespace SurveyApp.Services
{
    public interface IPdfService
    {
        byte[] GenerateSurveyPdf(SurveyResponse response);
        string GenerateFileName(SurveyResponse response);
    }
}

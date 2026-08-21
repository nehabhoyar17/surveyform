namespace SurveyApp.Services
{
    public interface IEmailService
    {
        Task SendSurveyEmailAsync(string respondentName, string respondentEmail,
            DateTime submittedAt, byte[] pdfBytes, string pdfFileName);
    }
} 
namespace SurveyApp.Models
{
    public class GmailSettings
    {
        public string SenderEmail { get; set; } = string.Empty;
        public string AppPassword { get; set; } = string.Empty;
        public string ReceiverEmail { get; set; } = string.Empty;
        public string SenderName { get; set; } = "Survey Website";
    }
}

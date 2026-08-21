using System.ComponentModel.DataAnnotations;

namespace SurveyApp.Models
{
    public class SurveyResponse
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required, Phone, StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required, Range(1, 120)]
        public int Age { get; set; }

        [Required, StringLength(20)]
        public string Gender { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Satisfaction { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string EaseOfUse { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string QualityRating { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Recommendation { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Likes { get; set; }

        [StringLength(1000)]
        public string? Improvements { get; set; }

        [Required, StringLength(20)]
        public string UseAgain { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? AdditionalComments { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}  
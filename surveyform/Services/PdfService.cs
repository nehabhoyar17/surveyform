using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SurveyApp.Models;
using System.Drawing;
using System.Reflection.Metadata;

namespace SurveyApp.Services
{
    public class PdfService : IPdfService
    {
        public PdfService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public string GenerateFileName(SurveyResponse response)
        {
            var safeName = string.Join("_", response.FullName.Split(' '));
            return $"SurveyResponse_{safeName}_{response.SubmittedAt:yyyyMMdd}.pdf";
        }

        public byte[] GenerateSurveyPdf(SurveyResponse r)
        {
            var document = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("Your Opinion Matters — Survey Response")
                            .FontSize(20).Bold().FontColor(Colors.Blue.Darken2);
                        col.Item().PaddingTop(2).Text($"Submitted on: {r.SubmittedAt:dddd, dd MMMM yyyy - hh:mm tt}")
                            .FontSize(10).FontColor(Colors.Grey.Darken1);
                        col.Item().PaddingTop(8).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
                    });

                    page.Content().PaddingVertical(15).Column(col =>
                    {
                        col.Spacing(15);

                        col.Item().Text("Personal Information").FontSize(14).Bold().FontColor(Colors.Blue.Darken1);
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c => { c.RelativeColumn(1); c.RelativeColumn(2); });
                            AddRow(table, "Full Name", r.FullName);
                            AddRow(table, "Email", r.Email);
                            AddRow(table, "Phone Number", r.PhoneNumber);
                            AddRow(table, "Age", r.Age.ToString());
                            AddRow(table, "Gender", r.Gender);
                            AddRow(table, "City", r.City);
                        });

                        col.Item().PaddingTop(10).Text("Survey Answers").FontSize(14).Bold().FontColor(Colors.Blue.Darken1);
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c => { c.RelativeColumn(2); c.RelativeColumn(3); });
                            AddRow(table, "Satisfaction with service", r.Satisfaction);
                            AddRow(table, "Ease of use", r.EaseOfUse);
                            AddRow(table, "Overall quality rating", r.QualityRating);
                            AddRow(table, "Likely to recommend", r.Recommendation);
                            AddRow(table, "What they liked most", string.IsNullOrWhiteSpace(r.Likes) ? "-" : r.Likes);
                            AddRow(table, "Suggested improvements", string.IsNullOrWhiteSpace(r.Improvements) ? "-" : r.Improvements);
                            AddRow(table, "Would use service again", r.UseAgain);
                            AddRow(table, "Additional comments", string.IsNullOrWhiteSpace(r.AdditionalComments) ? "-" : r.AdditionalComments);
                        });
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Generated automatically by Your Opinion Matters Survey System").FontSize(9).FontColor(Colors.Grey.Darken1);
                    });
                });
            });

            return document.GeneratePdf();
        }

        private static void AddRow(TableDescriptor table, string label, string value)
        {
            table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                .Text(label).Bold().FontColor(Colors.Grey.Darken2);
            table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                .Text(value);
        }
    }
} 
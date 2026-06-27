using System;
using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Models.App;
using CodeBook.Data.App;
namespace CodeBook.Business.App.Services
{
    public class ReportService : IReportService
    {
        private readonly CodeBookContext context;

        public ReportService(CodeBookContext context)
        {
            this.context = context;
        }
        public void SubmitReport(int reporterId, ReportRequest request)
        {
            var report = new Report
            {
                ReporterId = reporterId,
                PostId = request.PostId,
                CommentId = request.CommentId,
                Reason = request.Reason,
                Description = request.Description,
                Status = ReportStatus.Pending,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };
            context.reports.Add(report);
            context.SaveChanges();
        }

        public List<ReportDTO> GetPendingReports()
        {
            return context.reports
                .Where(r => r.Status == ReportStatus.Pending)
                .Select(r => new ReportDTO
                {
                    Id = r.Id,
                    ReporterId = r.ReporterId,
                    PostId = r.PostId,
                    CommentId = r.CommentId,
                    Reason = r.Reason,
                    Description = r.Description,
                    Status = r.Status.ToString(),
                    DateCreated = r.DateCreated
                }).ToList();
        }

        public void UpdateReportStatus(int reportId, string status)
        {
            var report = context.reports.FirstOrDefault(r => r.Id == reportId);
            if (report != null)
            {
                report.Status = Enum.Parse<ReportStatus>(status);
                report.DateUpdated = DateTime.UtcNow;
                context.SaveChanges();
            }
        }
    }
}

using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using AutoMapper;
using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using System;
namespace CodeBook.Business.App.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper mapper;

        public ReportService(IReportRepository reportRepository, IMapper mapper)
        {
            this._reportRepository = reportRepository;
            this.mapper = mapper;
        }
        public bool SubmitReport(int reporterId, ReportRequest request)
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
            _reportRepository.Add(report);
            _reportRepository.SaveChanges();
            return true;
        }

        public List<ReportDTO> GetPendingReports()
        {
            var report = _reportRepository.GetPendingReports();
            return mapper.Map<List<ReportDTO>>(report);
            /* context.reports
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
                 }).ToList();*/
        }

        public void UpdateReportStatus(int reportId, UpdateReportStatusDto dto)
        {
            var report = _reportRepository.GetReportbyId(reportId);
            if (report != null)
            {
                report.Status = dto.Status;
                report.DateUpdated = DateTime.UtcNow;
                _reportRepository.Update(report);
                _reportRepository.SaveChanges();
            }
        }
    }
}

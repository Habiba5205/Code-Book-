using System;
using CodeBook.Business.App.DTOs;
namespace CodeBook.Business.App.Services
{
	public interface IReportService
    {
        void SubmitReport(int reporterId, ReportRequest request);
        List<ReportDto> GetPendingReports();
        void UpdateReportStatus(int reportId, string status);
    }
}

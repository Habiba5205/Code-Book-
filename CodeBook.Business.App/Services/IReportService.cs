using System;
using CodeBook.Business.App.DTOs;
namespace CodeBook.Business.App.Services
{
	public interface IReportService
    {
        Task SubmitReport(int reporterId, ReportRequest request);
        Task<List<ReportDto>> GetPendingReports();
        Task UpdateReportStatusAsync(int reportId, string status);
    }
}

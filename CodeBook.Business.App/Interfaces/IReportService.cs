using System;
using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Middleware;

namespace CodeBook.Business.App.Interfaces
{
	public interface IReportService
    {
        ErrorResponse SubmitReport(int reporterId, ReportRequest request);
        List<ReportDTO> GetPendingReports();
        void UpdateReportStatus(int reportId, UpdateReportStatusDto dto);
    }
}

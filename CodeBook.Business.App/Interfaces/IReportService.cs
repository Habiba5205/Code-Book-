using System;
using CodeBook.Business.App.DTOs;

namespace CodeBook.Business.App.Interfaces
{
	public interface IReportService
    {
        void SubmitReport(int reporterId, ReportRequest request);
        List<ReportDTO> GetPendingReports();
        void UpdateReportStatus(int reportId, string status);
    }
}

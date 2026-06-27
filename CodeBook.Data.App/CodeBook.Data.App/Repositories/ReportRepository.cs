using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBook.Data.App.Repositories
{
    public class ReportRepository : IReportRepository
    {

        private readonly CodeBookContext _context;

        public ReportRepository(CodeBookContext context)
        {
            _context = context;
        }

        public List<Report> GetPendingReports()
        {
            return _context.reports.Where(r => r.Status == ReportStatus.Pending).ToList();
        }

        public void Add(Report report)
        {
            _context.reports.Add(report);
        }

        public void Update(Report report)
        {
            _context.reports.Update(report);
        }

        public Report GetReportbyId(int? reportId)
        {
            return _context.reports.FirstOrDefault(r => r.Id == reportId);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}

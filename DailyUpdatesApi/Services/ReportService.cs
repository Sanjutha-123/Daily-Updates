using DailyUpdates.Data;
using DailyUpdates.Models;
using Microsoft.EntityFrameworkCore;


namespace DailyUpdates.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbcontext _context;

        public ReportService(AppDbcontext context)
        {
            _context = context;
        }

        public async Task AddReportAsync(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Report>> GetReportsByUserAsync(int userId)
        {
            return await _context.Reports
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Report?> GetReportByIdAsync(int id, int userId)
        {
            return await _context.Reports
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
        }

        public async Task<bool> UpdateReportAsync(int id, int userId, Report report)
        {
            var existing = await GetReportByIdAsync(id, userId);
            if (existing == null) return false;

            existing.Task = report.Task;
            existing.Description = report.Description;
            existing.Issues = report.Issues;
            existing.Signin = report.Signin;
            existing.Signout = report.Signout;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReportAsync(int id, int userId)
        {
            var report = await GetReportByIdAsync(id, userId);
            if (report == null) return false;

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }

       
    }
}

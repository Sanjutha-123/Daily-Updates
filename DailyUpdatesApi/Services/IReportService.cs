using DailyUpdates.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyUpdates.Services
{
    public interface IReportService
    {
        Task AddReportAsync(Report report);
        Task<List<Report>> GetReportsByUserAsync(int userId);
        Task<Report?> GetReportByIdAsync(int id, int userId);
        Task<bool> UpdateReportAsync(int id, int userId, Report report);
        Task<bool> DeleteReportAsync(int id, int userId);
    }
}

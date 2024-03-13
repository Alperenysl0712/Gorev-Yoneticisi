using Görev_Yöneticisi.Models;

namespace Görev_Yöneticisi.Interfaces
{
    public interface IReportsService
    {
        void checkLogin(int userId);

        Task<string> addNewReport(int userId, string? priorityLevel, string? Category, string? Header, string? Detail, DateOnly endDate, string? Status);

        Task<List<Reports>> showReportsByLevel(int userId, string priorityLevel);

        Task<List<Reports>> showReportsByCategory(int userId, string Category);

        Task<List<Reports>> showReportsByStatus(int userId, string Status);

        Task<Dictionary<DateOnly, List<Reports>>> showReportsByDates(int userId);

        Task<Dictionary<string, List<Reports>>> showReportsByMonths(int userId);

        Task<Dictionary<string, List<Reports>>> showReportsByYears(int userId);

        Task<Reports> deleteReport(int userId, int reportId);

        Task<List<Reports>> deleteAllReport(int userId);


    }
}

using Görev_Yöneticisi.Interfaces;
using Görev_Yöneticisi.Models;
using System.Reflection.PortableExecutable;

namespace Görev_Yöneticisi.Services
{
    public class ReportsService : IReportsService
    {

        readonly IRepository<Reports> _reportsRepository;

        readonly String[] months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

        public ReportsService(IRepository<Reports> reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        public void checkLogin(int userId)
        {
            if (userId == 0)
            {
                throw new Exception("Please login for report queries.");
            }
        }

        public Task<string> addNewReport(int userId, string? priorityLevel, string? Category, string? Header, string? Detail, DateOnly endDate, string? Status)
        {

            checkLogin(userId);

            if (Header.Trim() == null || Category.Trim() == null || Status.Trim() == null || endDate == null)
            {
                throw new ArgumentNullException("You must be enter Header, Category, Status and End Date area.");
            }

            Reports newReport = new Reports
            {
                UserId = userId,
                priorityLevel = priorityLevel,
                Category = Category,
                Header = Header,
                Detail = Detail,
                addedDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                endDate = endDate,
                Status = Status
            };

            _reportsRepository.Add(newReport);

            return Task.FromResult("New report added successfully.");
        }

        public Task<List<Reports>> showReportsByCategory(int userId, string Category)
        {
            checkLogin(userId);

            if (Category.Trim() == null)
            {
                throw new ArgumentNullException("You must be enter Category area.");
            }

            List<Reports> newReports = _reportsRepository.GetAll(k => k.UserId == userId && k.Category.Equals(Category)).ToList();

            return Task.FromResult(newReports);

        }

        public Task<Dictionary<DateOnly, List<Reports>>> showReportsByDates(int userId)
        {
            checkLogin(userId);
            Dictionary<DateOnly, List<Reports>> calendar = new();

            List<Reports> newReports = _reportsRepository.GetAll(k => k.UserId == userId).OrderBy(k => k.endDate).ToList();

            foreach (Reports report in newReports)
            {
                if (calendar.ContainsKey(report.endDate))
                {
                    calendar[report.endDate].Add(report);
                }
                else
                {
                    calendar.Add(report.endDate, new List<Reports> { report });
                }
            }

            return Task.FromResult(calendar);

        }

        public Task<List<Reports>> showReportsByLevel(int userId, string priorityLevel)
        {
            checkLogin(userId);

            if (priorityLevel.Trim() == null)
            {
                throw new ArgumentNullException("You must be enter Priority Level area.");
            }

            List<Reports> newReports = _reportsRepository.GetAll(k => k.UserId == userId && k.priorityLevel.Equals(priorityLevel)).ToList();

            return Task.FromResult(newReports);
        }

        public Task<Dictionary<string, List<Reports>>> showReportsByMonths(int userId)
        {
            checkLogin(userId);

            Dictionary<string, List<Reports>> calendar = new();

            List<Reports> newReports = _reportsRepository.GetAll(k => k.UserId == userId).OrderBy(k => k.endDate).ToList();

            foreach (Reports report in newReports)
            {
                if (calendar.ContainsKey(months[report.endDate.Month - 1]))
                {
                    calendar[months[report.endDate.Month - 1]].Add(report);
                }
                else
                {
                    calendar.Add(months[report.endDate.Month - 1], new List<Reports> { report });
                }
            }

            return Task.FromResult(calendar);
        }

        public Task<List<Reports>> showReportsByStatus(int userId, string Status)
        {
            checkLogin(userId);

            if (Status.Trim() == null)
            {
                throw new ArgumentNullException("You must be enter Status Level area.");
            }

            List<Reports> newReports = _reportsRepository.GetAll(k => k.UserId == userId && k.Status.Equals(Status)).ToList();

            return Task.FromResult(newReports);
        }

        public Task<Dictionary<string, List<Reports>>> showReportsByYears(int userId)
        {
            checkLogin(userId);

            Dictionary<string, List<Reports>> calendar = new();

            List<Reports> newReports = _reportsRepository.GetAll(k => k.UserId == userId).OrderBy(k => k.endDate).ToList();

            foreach (Reports report in newReports)
            {
                if (calendar.ContainsKey($"Year - {report.endDate.Year}"))
                {
                    calendar[$"Year - {report.endDate.Year}"].Add(report);
                }
                else
                {
                    calendar.Add($"Year - {report.endDate.Year}", new List<Reports> { report });
                }
            }

            return Task.FromResult(calendar);
        }

        public Task<Reports> deleteReport(int userId, int reportId)
        {
            checkLogin(userId);

            Reports? report = _reportsRepository.Get(k => k.Id == reportId);

            if (report == null)
            {
                throw new Exception("Report could not find with this Id.");
            }

            _reportsRepository.Remove(report);

            return Task.FromResult(report);
        }

        public Task<List<Reports>> deleteAllReport(int userId)
        {
            checkLogin(userId);

            List<Reports> newReports = _reportsRepository.GetAll(k => k.UserId == userId).ToList();

            _reportsRepository.RemoveAll(newReports);

            return Task.FromResult(newReports);

        }
    }
}

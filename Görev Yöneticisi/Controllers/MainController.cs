using Görev_Yöneticisi.Interfaces;
using Görev_Yöneticisi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Görev_Yöneticisi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private static int userID;

        readonly IUserService _userService;

        readonly IReportsService _reportsService;

        public MainController(IUserService userService, IReportsService reportsService)
        {
            _userService = userService;
            _reportsService = reportsService;
        }

        [HttpPost("CreateNewUser")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> registerUser([FromQuery][Required] string username, [FromQuery][DataType(DataType.Password), Required] string password, [FromQuery][DataType(DataType.Password), Required] string passwordAgain)
        {
            var result = await _userService.registerUser(username, password, passwordAgain);

            return result;
        }

        [HttpPost("LoginUser")]
        [AllowAnonymous]
        public async Task<ActionResult<UserTokenInfo>> loginUser([FromQuery][Required] string username, [FromQuery][DataType(DataType.Password), Required] string password)
        {
            var result = await _userService.loginUser(username, password);

            userID = result.userId;

            return result;
        }



        [HttpPost("AddNewReport")]
        [Authorize]
        public async Task<ActionResult<string>> addNewReport(
            [FromQuery, Required, SwaggerParameter("<br>1) Critical<br>2) Important<br>3) Routine"), Range(1, 3)] int priorityLevel,
            [FromQuery, Required] string Category,
            [FromQuery, Required] string Header,
            [FromQuery, DataType(DataType.Text)] string Detail,
            [FromQuery][Required][SwaggerParameter("<br>Year-Month-Day<br>Ex. 2021-11-27")] DateOnly endDate,
            [FromQuery, Required, SwaggerParameter("<br>1) Active<br>2) Abandoned<br>3) Complete"), Range(1, 3)] int Status)
        {


            var result = await _reportsService.addNewReport(userID, Templates.priorityL[priorityLevel], Category.ToLower(), Header, Detail, endDate, Templates.status[Status]);

            return result;
        }


        [HttpPost("GetReportsByCategory")]
        [Authorize]
        public async Task<ActionResult<List<Reports>>> GetReportsByCategory([FromQuery, Required] string Category)
        {
            var result = await _reportsService.showReportsByCategory(userID, Category);

            return result;
        }

        [HttpGet("GetReportsByCalendar")]
        [Authorize]
        public async Task<ActionResult<Dictionary<DateOnly, List<Reports>>>> GetReporstByCalendar()
        {
            var result = await _reportsService.showReportsByDates(userID);

            return result;
        }

        [HttpPost("GetReportsByPriorityLevel")]
        [Authorize]
        public async Task<ActionResult<List<Reports>>> GetReportsByPriorityLevel([FromQuery, Required, SwaggerParameter("<br>1) Critical<br>2) Important<br>3) Routine"), Range(1, 3)] int priorityLevel)
        {
            var result = await _reportsService.showReportsByLevel(userID, Templates.priorityL[priorityLevel]);

            return result;
        }

        [HttpGet("GetReporstByMonths")]
        [Authorize]
        public async Task<ActionResult<Dictionary<string, List<Reports>>>> GetReporstByMonths()
        {
            var result = await _reportsService.showReportsByMonths(userID);

            return result;
        }

        [HttpPost("GetReportsByStatus")]
        [Authorize]
        public async Task<ActionResult<List<Reports>>> GetReportsByStatus([FromQuery, Required][SwaggerParameter("<br>1) Active<br>2) Abandoned<br>3) Complete"), Range(1, 3)] int Status)
        {
            var result = await _reportsService.showReportsByStatus(userID, Templates.status[Status]);

            return result;
        }

        [HttpGet("GetReporstByYears")]
        [Authorize]
        public async Task<ActionResult<Dictionary<string, List<Reports>>>> GetReporstByYears()
        {
            var result = await _reportsService.showReportsByYears(userID);

            return result;
        }

        [HttpDelete("DeleteReportById")]
        [Authorize]
        public async Task<ActionResult> DeleteReportById([FromQuery, Required, Range(0, int.MaxValue)] int reportId)
        {
            var result = await _reportsService.deleteReport(userID, reportId);

            return Ok(new { Message = "Selected report removed successfully", RemovedReport = result });
        }

        [HttpDelete("DeleteAllReports")]
        [Authorize]
        public async Task<ActionResult> DeleteAllReports()
        {
            var result = await _reportsService.deleteAllReport(userID);

            return Ok(new { Message = "All reports removed successfully", RemovedReport = result });
        }
    }
}

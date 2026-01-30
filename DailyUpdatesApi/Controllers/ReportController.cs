using DailyUpdates.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DailyUpdates.Services;  

namespace DailyUpdates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        private int GetUserIdFromToken()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim == null ? 0 : int.Parse(claim.Value);
        }

        // ADD REPORT
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Report report)
        {
            int userId = GetUserIdFromToken();
            if (userId == 0)
                return Unauthorized("Invalid token or user not found.");

            report.UserId = userId;
            await _reportService.AddReportAsync(report);

            return Ok(new { message = "Report added successfully", report });
        }

        // GET ALL REPORTS
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            int userId = GetUserIdFromToken();
            if (userId == 0)
                return Unauthorized("Invalid token or user not found.");

            var reports = await _reportService.GetReportsByUserAsync(userId);
            return Ok(reports);
        }

        // GET REPORT BY ID
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            int userId = GetUserIdFromToken();
            if (userId == 0)
                return Unauthorized("Invalid token or user not found.");

            var report = await _reportService.GetReportByIdAsync(id, userId);
            if (report == null)
                return NotFound("Report not found.");

            return Ok(report);
        }

        // UPDATE REPORT
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Report updatedReport)
        {
            int userId = GetUserIdFromToken();
            if (userId == 0)
                return Unauthorized("Invalid token or user not found.");

            var success = await _reportService.UpdateReportAsync(id, userId, updatedReport);
            if (!success)
                return NotFound("Report not found.");

            return Ok(new { message = "Report updated successfully" });
        }

        // DELETE REPORT
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = GetUserIdFromToken();
            if (userId == 0)
                return Unauthorized ("Invalid token or user not found.");

            var success = await _reportService.DeleteReportAsync(id, userId);
            if (!success)
                return NotFound("Report not found.");

            return Ok (new { message = "Report deleted successfully" });
        }
    }
}

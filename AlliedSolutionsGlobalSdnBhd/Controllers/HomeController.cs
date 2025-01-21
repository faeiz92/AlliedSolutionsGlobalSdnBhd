using System.Diagnostics;
using AlliedSolutionsGlobalSdnBhd.Models;
using Microsoft.AspNetCore.Mvc;
using AlliedSolutionsGlobalSdnBhd.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlliedSolutionsGlobalSdnBhd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AccessDatabaseService<EqpReservationsDTO> _accessDatabaseService;
        private readonly AccessDatabaseService<PLCControlsDTO> _accessDatabaseService2;

        public HomeController(ILogger<HomeController> logger,  AccessDatabaseService<EqpReservationsDTO> accessDatabaseService, AccessDatabaseService<PLCControlsDTO> accessDatabaseService2)
        {
            _logger = logger;
            _accessDatabaseService = accessDatabaseService;
            _accessDatabaseService2 = accessDatabaseService2;
        }

        public async Task<IActionResult> Index()
        {
            string tableName = "EqpReservations";
            List<EqpReservationsDTO> data = await _accessDatabaseService.GetDataFromAccessDatabaseAsync(tableName);

            return View(data); 
        }

        public async Task<IActionResult> Privacy()
        {
            string tableName = "PLCControl";
            List<PLCControlsDTO> data = await _accessDatabaseService2.GetDataFromAccessDatabaseAsync(tableName);

            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

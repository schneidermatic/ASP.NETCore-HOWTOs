using Demo015.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Demo015.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BackgroundWorkerQueue _backgroundWorkerQueue;

        public HomeController(ILogger<HomeController> logger, BackgroundWorkerQueue backgroundWorkerQueue)
        {
            _logger = logger;
            _backgroundWorkerQueue = backgroundWorkerQueue;

        }

        private async Task CallAsyncApi()
        {
            _logger.LogInformation($"Starting at {DateTime.UtcNow.TimeOfDay}");
            _backgroundWorkerQueue.QueueBackgroundWorkItem(async token =>
            {
                while (true) { 
                    _logger.LogInformation($"I'm still running at {DateTime.UtcNow.TimeOfDay}");
                    await Task.Delay(10000);
                }
            });
        }

        public async Task<IActionResult> Index()
        {
            await CallAsyncApi();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
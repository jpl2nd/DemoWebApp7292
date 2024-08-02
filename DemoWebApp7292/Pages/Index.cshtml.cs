using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.ApplicationInsights;

namespace DemoWebApp7292.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly TelemetryClient _telemetryClient;

        public IndexModel(ILogger<IndexModel> logger, TelemetryClient telemetryClient)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
        }

        public void OnGet()
        {

            // Track a custom event with additional data
            var properties = new Dictionary<string, string>
        {
            { "User", User?.Identity?.Name ?? "Anonymous" },
            { "Page", "Home" }
        };

            var metrics = new Dictionary<string, double>
        {
            { "LoadTime", 1.23 }
        };

            _telemetryClient.TrackEvent("HomePageVisited", properties, metrics);
        

          


        }
    }
}

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
              _telemetryClient.TrackEvent("HomePageVisited");

          


        }
    }
}

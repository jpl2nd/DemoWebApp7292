using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Abstractions;

namespace DemoWebApp7292.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly TelemetryClient _telemetryClient;

        public PrivacyModel(ILogger<PrivacyModel> logger,  TelemetryClient telemetryClient)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
        }

        public void OnGet()
        {
            
                try
                {
                    // Simulate an exception
                    throw new Exception("Test exception");
                }
                catch (Exception ex)
                {
                    _telemetryClient.TrackException(ex);
                }

           
            
        }
    }

}

using Azure.Core;
using Azure.Core.Diagnostics;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Abstractions;
using System.Diagnostics.Tracing;

namespace DemoWebApp7292.Pages
{
    public class InsightsModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly TelemetryClient _telemetryClient;
        private readonly IConfiguration _configuration;
        


        public InsightsModel(ILogger<PrivacyModel> logger,  TelemetryClient telemetryClient, IConfiguration configuration)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
            _configuration = configuration;
      
        }

       

        public async Task OnGetAsync()
        {
            using AzureEventSourceListener listener = AzureEventSourceListener.CreateConsoleLogger(EventLevel.Verbose);
            var credential = new DefaultAzureCredential();
            var x = credential.GetType();
            

            //Application Insights
            var properties = new Dictionary<string, string>
            {
                { "AuthenticationType",  x.FullName ?? "None" },
                { "Page", "Insights" }
                

            };


            
            try
            {
                // Simulate and exception and send it application insights
                throw new Exception("Test exception");
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
            }

            _telemetryClient.TrackEvent("Application Insights Page Loaded");
            _telemetryClient.TrackTrace("Insights Custom Message", SeverityLevel.Information, properties);
            _telemetryClient.TrackPageView("InsightsPageVisited");









        }
    }

}

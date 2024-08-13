using Azure.Core;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Abstractions;

namespace DemoWebApp7292.Pages
{
    public class StorageModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly TelemetryClient _telemetryClient;
        private readonly IConfiguration _configuration;
        


        public StorageModel(ILogger<PrivacyModel> logger,  TelemetryClient telemetryClient, IConfiguration configuration)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
            _configuration = configuration;
      
        }

        public List<BlobInfo> Blobs { get; private set; } = new List<BlobInfo>();

        public async Task OnGetAsync()
        {

            var startTime = DateTime.UtcNow;
            //Application Insights
            try
            {
                // Simulate and exception and send it application insights
                throw new Exception("Test exception");
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
            }



            // Use DefaultAzureCredential to authenticate with Managed Identity
            var credential = new DefaultAzureCredential();
            var tokenRequestContext = new TokenRequestContext(new[] { "https://storage.azure.com/.default" });


            // Get Storage Account Name and Container Name from configuration
            string storageAccountName = _configuration["StorageAccountName"];
            string containerName = _configuration["ContainerName"];

            // Construct the Blob service client URI
            string blobServiceUri = $"https://{storageAccountName}.blob.core.windows.net";

            // Create a BlobServiceClient using DefaultAzureCredential and get a reference to the container
            BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(blobServiceUri), credential);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            // Get a user delegation key for the SAS token
            UserDelegationKey userDelegationKey = await blobServiceClient.GetUserDelegationKeyAsync(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(7));

            // List blobs in the container and generate download links
            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                // Get a reference to the blob
                BlobClient blobClient = containerClient.GetBlobClient(blobItem.Name);

                // Generate a SAS token for the blob
                BlobSasBuilder sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = containerName,
                    BlobName = blobItem.Name,
                    Resource = "b",
                    ExpiresOn = DateTimeOffset.UtcNow.AddHours(1) // Set the expiration time for the SAS token
                };
                sasBuilder.SetPermissions(BlobSasPermissions.Read);


                // Generate the SAS token
                Uri sasUri = new Uri(blobClient.Uri + "?" + sasBuilder.ToSasQueryParameters(userDelegationKey, storageAccountName).ToString());

                // Add the blob info to the list
                Blobs.Add(new BlobInfo { BlobName = blobItem.Name, SasUri = sasUri });


            }
            var endTime = DateTime.UtcNow;
            var duration = endTime - startTime;
           
            var metrics = new Dictionary<string, TimeSpan>
        {
            { "LoadTime", duration }
        };



        }
    }

}

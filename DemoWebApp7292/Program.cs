using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using DemoWebApp7292.wwwroot;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility;

var builder = WebApplication.CreateBuilder(args);


// Retrieve the connection string from Key Vault
var keyVaultName = builder.Configuration["ApplicationInsights:KeyVaultName"]; 
var secretName = builder.Configuration["ApplicationInsights:SecretName"];
var kvUri = $"https://{keyVaultName}.vault.azure.net";

var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
KeyVaultSecret secret = client.GetSecret(secretName);
string appInsightsConnectionString = secret.Value;

// Create ApplicationInsightsServiceOptions and set the connection string
var appInsightsOptions = new ApplicationInsightsServiceOptions
{
    ConnectionString = appInsightsConnectionString
};

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddApplicationInsightsTelemetry(appInsightsOptions);
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton(client);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


app.Run();

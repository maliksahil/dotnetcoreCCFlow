using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using api;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;

namespace Microsoft.AspNetCore.Authentication
{
    public static class AzureAdServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddAzureAdBearer(this AuthenticationBuilder builder)
            => builder.AddAzureAdBearer(_ => { });

        public static AuthenticationBuilder AddAzureAdBearer(this AuthenticationBuilder builder, Action<AzureADOptions> configureOptions)
        {
            builder.Services.Configure(configureOptions);
            builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureAzureOptions>();
            builder.AddJwtBearer();
            return builder;
        }

        private class ConfigureAzureOptions : IConfigureNamedOptions<JwtBearerOptions>
        {
            private readonly AzureADOptions _azureOptions;

            public ConfigureAzureOptions(IOptions<AzureADOptions> azureOptions)
            {
                _azureOptions = azureOptions.Value;
            }

            public void Configure(string name, JwtBearerOptions options)
            {
                options.Authority = $"{_azureOptions.Instance}{_azureOptions.TenantId}";
                options.TokenValidationParameters.ValidAudiences = new string[] {
                    _azureOptions.ClientId, 
                    $"https://{_azureOptions.Domain}/api"};
                options.Events = JwtBearerMiddlewareDiagnostics.Subscribe(options.Events);
            }

            public void Configure(JwtBearerOptions options)
            {
                Configure(Options.DefaultName, options);
            }
        }
    }
}

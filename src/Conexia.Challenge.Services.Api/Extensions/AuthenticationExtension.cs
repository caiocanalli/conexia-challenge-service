using Conexia.Challenge.Application.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Conexia.Challenge.Services.Api.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuthenticationConfig(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var tokenConfigurations = new TokenDescriptor();

            new ConfigureFromConfigurationOptions<TokenDescriptor>(
                    configuration.GetSection("JwtTokenOptions"))
                .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = false;
                bearerOptions.SaveToken = true;

                var paramsValidation = bearerOptions.TokenValidationParameters;

                paramsValidation.IssuerSigningKey = SigningCredentialsConfiguration.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;

            });
        }
    }
}

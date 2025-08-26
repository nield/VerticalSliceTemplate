﻿using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using VerticalSliceTemplate.Api.Swagger;

namespace VerticalSliceTemplate.Api.Configurations;

public static class Swagger
{
    public static void ConfigureSwagger(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options => 
        {
            //// Add OAuth2 authentication scheme
            //options.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
            //{
            //    Type = SecuritySchemeType.OAuth2,
            //    Flows = new OpenApiOAuthFlows
            //    {
            //        Implicit = new OpenApiOAuthFlow
            //        {
            //            AuthorizationUrl = new Uri(config["AuthorityOptions:AuthorizationUrl"]!),
            //            TokenUrl = new Uri(config["AuthorityOptions:TokenUrl"]!),                        
            //            Scopes = new Dictionary<string, string>
            //            {
            //                { "minimal-api-aud", "Minimal Api" }
            //            }
            //        }
            //    }
            //});            

            //// Add security requirement
            //options.AddSecurityRequirement(new OpenApiSecurityRequirement
            //{
            //    {
            //        new OpenApiSecurityScheme
            //        {
            //            Reference = new OpenApiReference
            //            {
            //                Type = ReferenceType.SecurityScheme,
            //                Id = "Keycloak"
            //            }
            //        },
            //        Array.Empty<string>()
            //    }
            //});

            options.OperationFilter<SwaggerDefaultValues>();
        });
    }

    public static void UseApiDocumentation(this WebApplication app, IConfiguration config)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();

            // build a swagger endpoint for each discovered API version
            foreach (var groupName in descriptions.Select(x => x.GroupName))
            {
                var url = $"/swagger/{groupName}/swagger.json";
                var name = groupName.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
            }

            options.OAuthClientId(config["AuthorityOptions:Client"]);
        });
    }
}
using VerticalSliceTemplate.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.ConfigureLogging();

builder.AddApiServices();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseLogging();

app.UseCompression();

app.UseExceptionHandler(_ => { });

app.UseHeaderPropagation();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.MapOpenApi();
    app.UseApiDocumentation(app.Configuration);
}

app.UseHttpsRedirection();

app.AddApiEndpoints();

await app.ApplyMigrations();

await app.RunAsync();



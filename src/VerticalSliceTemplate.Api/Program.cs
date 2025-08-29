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

app.UseHttpsRedirection();

app.AddApiEndpoints();

if (!app.Environment.IsProduction())
{
    app.UseApiDocumentation();
}

await app.ApplyMigrations();

await app.RunAsync();

// Make the implicit Program class public so test projects can access it
public partial class Program
{
    protected Program()
    {

    }
}

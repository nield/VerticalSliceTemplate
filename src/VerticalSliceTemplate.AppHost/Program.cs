var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("Seq", 8002)
    .WithLifetime(ContainerLifetime.Persistent);

var redis = builder.AddRedis("Redis", 8004)
    .WithLifetime(ContainerLifetime.Persistent);

var sqlPassword = builder.AddParameter("sqlPassword", secret: true);
var database = builder.AddSqlServer("Sql", sqlPassword, 8003)
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("SqlDatabase", "templateDb");

builder.AddProject<Projects.VerticalSliceTemplate_Api>("verticalslicetemplate-api")
    .WithReference(database)
    .WaitFor(database)
    .WithReference(redis)
    .WaitFor(redis)
    .WaitFor(seq)
    .WithEnvironment("SEQ_SERVER_URL", "http://localhost:8002")
    .WithUrls(context =>
    {
        foreach (var url in context.Urls)
        {
            url.DisplayLocation = UrlDisplayLocation.DetailsOnly;
        }
    
        context.Urls.Add(new ResourceUrlAnnotation
        {
            DisplayText = "Scalar UI",
            Url = "/scalar",
            Endpoint = context.GetEndpoint("https")
        });
    });

await builder.Build().RunAsync();

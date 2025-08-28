var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("Seq", 8002)
    .WithLifetime(ContainerLifetime.Persistent);

var redis = builder.AddRedis("Redis", 8004)
    .WithLifetime(ContainerLifetime.Persistent);

var sqlPassword = builder.AddParameter("sqlPassword");
var database = builder.AddSqlServer("Sql", sqlPassword, 8003)
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("SqlDatabase", "templateDb");

builder.AddProject<Projects.VerticalSliceTemplate_Api>("verticalslicetemplate-api")
    .WithReference(database)
    .WaitFor(database)
    .WithReference(redis)
    .WaitFor(redis)
    .WaitFor(seq)
    .WithEnvironment("SEQ_SERVER_URL", "http://localhost:8002");

await builder.Build().RunAsync();

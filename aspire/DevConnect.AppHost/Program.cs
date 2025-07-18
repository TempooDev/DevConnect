var builder = DistributedApplication.CreateBuilder(args);

var postgreServer = builder.AddPostgres("postgres").WithPgAdmin();

var db = postgreServer.AddDatabase("devconnectdb");

var apiService = builder.AddProject<Projects.DevConnect_ApiService>("apiservice").
    WithExternalHttpEndpoints()
    .WithReference(db)
    .WaitFor(db);

builder.AddProject<Projects.DevConnect_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();

using GraphQL.AspNet.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddGraphQL();

var app = builder.Build();

app.UseGraphQL();
app.UseGraphQLPlayground();

app.Run();

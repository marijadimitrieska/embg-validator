using EmbgValidatorApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<EmbgValidatorService>();

var app = builder.Build();

app.MapControllers();

app.Run();
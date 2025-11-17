using EmbgValidatorApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddSingleton<EmbgValidatorService>();

builder.Services.AddCors(options =>{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

app.UseStaticFiles();

app.MapControllers();

app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("/index.html");
    return Task.CompletedTask;
});

app.Run();
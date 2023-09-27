using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using JwtTokenAuthentication;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot();
builder.Services.AddJwtAuthentication();
var app = builder.Build();
app.UseOcelot().Wait();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.Run();

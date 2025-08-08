using RUM.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder);
builder.Services.AddApplicationLoging(builder);

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/Error/Status", "?code={0}");

app.UseExceptionHandler("/Error/Exception");
app.UseRateLimiter();
app.UseRouting();
app.UseAuthentication();
app.UseRateLimiter();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}")
    .WithStaticAssets();
app.Run();

using RUM.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:80");
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
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/Account/Login");
        return;
    }
    await next();
});

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Create}")
    .WithStaticAssets();

app.Run();

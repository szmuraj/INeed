using INeed.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization; // Wymagane do lokalizacji
using System.Globalization;              // Wymagane do CultureInfo

var builder = WebApplication.CreateBuilder(args);

// 1. Pobieramy ConnectionString
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Konfiguracja Serwisów Lokalizacji
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
    .AddViewLocalization()              // W³¹cza lokalizacjê dla Widoków (.cshtml)
    .AddDataAnnotationsLocalization();  // W³¹cza lokalizacjê dla Walidacji modeli

// 3. Konfiguracja Bazy Danych
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null)
    ));

builder.Services.AddTransient<INeed.Services.IEmailService, INeed.Services.EmailService>();

var app = builder.Build();

// 4. Konfiguracja Obs³ugiwanych Jêzyków (PL i EN)
var supportedCultures = new[] { "pl-PL", "en-US" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("pl-PL")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

// WYMUSZENIE: Ciasteczko ma najwy¿szy priorytet przy wyborze jêzyka
localizationOptions.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// 5. URUCHOMIENIE LOKALIZACJI (Musi byæ przed UseRouting)
app.UseRequestLocalization(localizationOptions);

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
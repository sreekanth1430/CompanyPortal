using CompanyPortal.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<CompanyDBContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "Links",
            pattern: "Links/{id?}",
            defaults: new { controller = "EmployeeExperience", action = "LinkIndex" });

        endpoints.MapControllerRoute(
            name: "Documents",
            pattern: "Documents/{id?}",
            defaults: new { controller = "EmployeeExperience", action = "DocumentIndex" });

        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });

app.Run();

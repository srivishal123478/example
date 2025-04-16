using Microsoft.EntityFrameworkCore;
using PaymentAndNotifications.Data;
using PaymentAndNotifications.Hubs;
using PaymentAndNotifications.Services;
using PaymentAndNotifications.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddScoped<NotificationRepository>(); // Fix for NotificationService dependency
builder.Services.AddScoped<NotificationService>();

builder.Services.AddScoped<PaymentService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stripeWebhookSecret = configuration["Stripe:WebhookSecret"];
    return new PaymentService(stripeWebhookSecret); // Fix for PaymentService dependency
});

builder.Services.AddScoped<EmailService>();
// Add controllers with Razor View support
builder.Services.AddControllersWithViews();
// Add SignalR for real-time notifications
builder.Services.AddSignalR();



// Add DbContext for database (if needed)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middleware pipeline configuration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Features/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Map SignalR hub
app.MapHub<NotificationHub>("/notificationHub");

// Map controllers and views
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Features}/{action=Index}/{id?}");

app.Run();
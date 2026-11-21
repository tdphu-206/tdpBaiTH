using Microsoft.EntityFrameworkCore;
using Tdp_241230817.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
   
    var p = options.ModelBindingMessageProvider;
    p.SetValueMustBeANumberAccessor(field => $"{field} phải là số.");
    p.SetAttemptedValueIsInvalidAccessor((value, field) => $"{field} phải là số (giá trị '{value}' không hợp lệ).");
    p.SetValueIsInvalidAccessor(value => $"Giá trị '{value}' không hợp lệ.");
});

builder.Services.AddDbContext<QLHangHoaContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("QLHangHoa")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

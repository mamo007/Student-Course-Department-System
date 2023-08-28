using Microsoft.EntityFrameworkCore;
using WebApplication1.IService;
using WebApplication1.Models;
using WebApplication1.Services;
using Microsoft.AspNetCore.Identity;
using System;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IDepartmentService,DepartmentService>();
            builder.Services.AddTransient<IStudentService, StudentService>();
            builder.Services.AddTransient<ICourseService, CourseService>();
            builder.Services.AddTransient<IDepartmentCoursesService, DepartmentCoursesService>();
            builder.Services.AddTransient<IStudentDepartment, StudentService>();
            builder.Services.AddTransient<IStudentCourse, StudentCourseService>();
            builder.Services.AddDbContext<ITIASPContext>(
                s => s.UseSqlServer(builder.Configuration.GetConnectionString("Con1")
                )
                );

            builder.Services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ITIASPContext>();

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ITIASPContext>();

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
            app.UseAuthentication();;

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
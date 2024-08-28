using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShushrutEyeHospitalCRM.ApplicationContext;
using ShushrutEyeHospitalCRM.Helper;
using ShushrutEyeHospitalCRM.Mapper;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Repositories.Implementation;
using ShushrutEyeHospitalCRM.Repositories.Interface;
using ShushrutEyeHospitalCRM.Services.Implementation;
using ShushrutEyeHospitalCRM.Services.Interface;
using System.Configuration;

namespace ShushrutEyeHospitalCRM.ServiceExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with Scoped lifetime
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(CommonHelper.ConnectionString));
                options.UseLazyLoadingProxies();
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
                       

            services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                // opt.SignIn.RequireConfirmedEmail = true;
                opt.Lockout.MaxFailedAccessAttempts = 5;
            })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                    opt.TokenLifespan = TimeSpan.FromHours(2));


            services.AddScoped<Func<ApplicationDbContext>>((provider) => () => provider.GetService<ApplicationDbContext>()!);


            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time   
            });
            services.AddMvc();
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepositories<>), typeof(Repository<>));
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IDepartmentService, DepartmentService>()
                .AddScoped<IDoctorService, DoctorService>()
                .AddScoped<IPatientService, PatientService>()
                .AddScoped<IReceptionService, ReceptionService>()
                .AddScoped<IRefractionService, RefractionService>()
                .AddScoped<IDashboardService, DashboardService>()
                .AddScoped<ICounslingService, CounslingService>();
        }
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            IMapper mapper = config.CreateMapper();
            return services.AddSingleton(mapper);
        }
        public static IServiceCollection ConfigureApplicationCookie(this IServiceCollection services)
        {
            return services.ConfigureApplicationCookie(options =>
             {
                 options.LoginPath = CommonHelper.LoginUrl;
                 options.AccessDeniedPath = CommonHelper.AccessDeniedUrl;
             });
        }
    }
}

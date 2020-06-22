using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using LeoQuiz.Core.Abstractions.Repositories;
using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Dto;
using LeoQuiz.Core.Entities;
using LeoQuiz.Core.Validators;
using LeoQuiz.DAL;
using LeoQuiz.DAL.Repositories;
using LeoQuiz.Extentions;
using LeoQuiz.Services;
using LeoQuiz.Services.Extentions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LeoQuiz
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public async void ConfigureServices(IServiceCollection services)
        {
            AddCors(services);

            AddAutoMapper(services);

            services.AddMvc().AddFluentValidation();

            services.AddDbContext<LeoQuizApiContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            AddIdentity(services);

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
            });

            AddAuthentication(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LeoQuiz API", Version = "v1" });
            });

            AddValidators(services);

            AddServices(services);

            AddRepositories(services);

            services.AddSingleton<ILoggerService, LoggerService>();

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerService logger)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LeoQuiz API V1");
            });

            app.UseCors("AllowMyOrigin");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roleCheck = await RoleManager.RoleExistsAsync("Admin").ConfigureAwait(false);
            if (!roleCheck)
            {
                await RoleManager.CreateAsync(new IdentityRole("Admin")).ConfigureAwait(false);
                await RoleManager.CreateAsync(new IdentityRole("Interviewee")).ConfigureAwait(false);
            }
        }

        private void AddCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);
        }

        private void AddAutoMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new LeoQuiz.Core.Mapper());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<LeoQuizApiContext>()
                    .AddRoleManager<RoleManager<IdentityRole>>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
            });

        }

        private void AddAuthentication(IServiceCollection services)
        {
            SymmetricSecurityKey privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("marvel marvel marvel marvel marvel marvel marvel marvel marvel marvel marvel marvel marvel marvel"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = privateKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                };
            });
        }

        private void AddValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<QuizDto>, QuizValidator>();
            services.AddTransient<IValidator<QuestionDto>, QuestionValidator>();
            services.AddTransient<IValidator<AnswerDto>, AnswerValidator>();
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IPassedQuizService, PassedQuizService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
        }

        private void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IPassedQuizRepository, PassedQuizRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

    }
}

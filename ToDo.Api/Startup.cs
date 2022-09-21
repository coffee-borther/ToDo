using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyToDo.Api;
using MyToDo.Api.Context;
using MyToDo.Api.Context.Repository;
using MyToDo.Api.Extensions;
using MyToDo.Api.Services;
using ToDo.Api.Context.Repository;

namespace ToDo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyToDoContext>(option =>
                {
                    var connectionString = Configuration.GetConnectionString("ToDoConnection");
                    option.UseSqlite(connectionString);
                }).AddUnitOfWork<MyToDoContext>()
                .AddCustomRepository<MyToDo.Api.Context.ToDo, ToDoRepository>()
                .AddCustomRepository<Memo, MemoRepository>()
                .AddCustomRepository<User, UserRepository>();
            services.AddTransient<IToDoService, ToDoService>();
            services.AddTransient<IMemoService, MemoService>();
            services.AddTransient<ILoginService, LoginService>();
            var AutoMapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new AutoMapperProFile());
            });
            services.AddSingleton(AutoMapperConfig.CreateMapper());
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "ToDo.Api", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo.Api v1"));
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
            });
        }
    }
}
using System;
using Api.DataModels;
using Api.Services;
using Api.Services.DocumentDb;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Api
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
            services.AddMvc();

            services.TryAddSingleton(provider => new DocumentDbRepositorySettings
            {
                Endpoint = new Uri("https://dbeklbpc33o5p5k.documents.azure.com:443/"),
                Key = "UUSh6LypgXzIw9UWCXCG6lPMjynrahDRqefEvvTlQ9KuLJZ5itJQGmRyxJ4H9x34ZR4bzOOzr7s4x6d0i0J27g=="
            });

            services.TryAddScoped<IGameResultRepository, GameResultRepository>();


            services.TryAddSingleton(provider =>
            {
                return new MapperConfiguration(config =>
                    {
                        config.CreateMap<DataModels.GameResult, DomainModel.GameResult>()
                            .ForMember(r => r.RunDifferential, opt => opt.Ignore());
                    })
                    .CreateMapper();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
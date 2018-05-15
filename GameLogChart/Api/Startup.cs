using System;
using Api.DomainModel;
using Api.Services.DocumentDb;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using GameResult = Api.DataModels.GameResult;

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
                Endpoint = new Uri(Configuration["CosmosDb:Endpoint"]),
                Key = Configuration["CosmosDb:Key"]
            });

            services.TryAddScoped<IGameResultRepository, GameResultRepository>();


            services.TryAddSingleton(provider =>
            {
                return new MapperConfiguration(config =>
                    {
                        config.CreateMap<GameResult, DomainModel.GameResult>()
                            .ForMember(r => r.RunDifferential, opt => opt.Ignore());

                        config.CreateMap<Services.DocumentDb.GameResult, GameResult>()
                            .ConvertUsing<GameResultTypeConverter>();
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
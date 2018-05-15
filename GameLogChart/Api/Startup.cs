using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Api.DomainModel;
using Api.Services.DocumentDb;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Swashbuckle.AspNetCore.Swagger;
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
                            .ForMember(r => r.RunDifferential, options => options.Ignore());

                        config.CreateMap<Services.DocumentDb.GameResult, GameResult>()
                            .ConvertUsing<GameResultTypeConverter>();

                        config.CreateMap<IEnumerable<DomainModel.GameResult>, ApiModels.GameResult>()
                            .ForMember(r => r.Count, options => options.Ignore())
                            .ConvertUsing<ApiModels.GameResultTypeConverter>();
                    })
                    .CreateMapper();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Baseball API",
                    Description = "A simple Baseball API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Farooq Mahmud",
                        Email = string.Empty,
                        Url = "https://github.com/farooqam"
                    },
                    License = new License
                    {
                        Name = "The MIT License",
                        Url = "https://opensource.org/licenses/MIT"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Baseball API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
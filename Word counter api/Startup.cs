using Businnes_logic.Interfaces;
using Businnes_logic.Services;
using Businnes_logic.TextStrategy;
using Businnes_logic.TextStrategy.Interfaces;
using Businnes_logic.TextStrategy.StrategyItem;
using Domain.DTOs;
using Domain.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Word_counter_api.Helpers;

namespace Word_counter_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("exludedWords.json");

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Word_counter_api", Version = "v1" });
            });
            services.AddCors((options) => {
                options.AddPolicy("Default", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });
            });
            services.AddMemoryCache();
            services.AddScoped<ITextService, TextService>();

            services.AddScoped<OneWord>();
            services.AddScoped<TwoWords>();
            services.AddScoped<ThreeWords>();
            services.AddScoped<ITextFactory, TextFactory>();
            services.AddScoped<ITextStrategy, TextStrategy>();
            services.AddScoped<ITextType[]>(provider =>
            {
                var factory = (ITextFactory)provider.GetService(typeof(ITextFactory));
                return factory.Create();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMemoryCache cache)
        {
            var excludedWordsCashe = CacheHelper.GetItemFromCacheMemory<ExludedWords>(CasheType.ExcludedWords.ToString(), cache);
            if (excludedWordsCashe == null)
            {
                var excludedWords = new ExludedWords();
                Configuration.Bind(excludedWords);
                CacheHelper.SetItemInCacheMemory(excludedWords,CasheType.ExcludedWords.ToString(), cache);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Word_counter_api v1"));
            }
            app.UseCors("Default");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

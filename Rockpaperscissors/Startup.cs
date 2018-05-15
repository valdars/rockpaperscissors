using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rockpaperscissors.Components;
using Rockpaperscissors.Components.ChoiceProviders;
using Rockpaperscissors.Components.GameStateStorages;
using Rockpaperscissors.Components.PlayerProviders;
using Rockpaperscissors.Components.Players;

namespace Rockpaperscissors
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
            services
                .AddMvc()
                .AddSessionStateTempDataProvider();
            services.AddSession();

            MemoryPlayerProvider.RegisterPlayer<HumanPlayer>("humanPlayer", "Human player");
            MemoryPlayerProvider.RegisterPlayer<RandomComputerPlayer>("randomComputerPlayer", "Random computer player");
            MemoryPlayerProvider.RegisterPlayer<TacticalComputerPlayer>("tacticalComputerPlayer", "Tactical computer player");
            services.AddSingleton<IPlayerProvider>(new MemoryPlayerProvider());

            services.AddTransient<IGameStateStorage, SessionGameStateStorage>();
            services.AddSingleton<IChoiceProvider, StaticChoiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Game}/{action=Index}/{id?}");
            });
        }
    }
}

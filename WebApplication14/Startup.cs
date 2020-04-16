using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication14
{
    public class Startup
    {
     
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddIdentityServer()
                //身份信息授权资源
               .AddInMemoryIdentityResources(Config.Ids)
               //API访问授权资源
               .AddInMemoryApiResources(Config.GetApis())
               //添加客户端
               .AddInMemoryClients(Config.GetClients())
               .AddTestUsers(Config.GetUsers());
            builder.AddDeveloperSigningCredential();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            //访问wwwroot目录静态文件
            app.UseStaticFiles();
            //使用Mvc中间件
            app.UseMvcWithDefaultRoute();
        }
    }
}

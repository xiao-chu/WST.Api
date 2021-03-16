using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Web.AspNetCore;
using NLog.Web;

namespace WSTWeb.API
{
    public class Programu
    {
        public static void Main(string[] args)
        {
            // 设置读取指定位置的nlog.config文件
            NLogBuilder.ConfigureNLog("NLog/nlog.config");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                 // 配置使用NLog
                .UseNLog();
    }
}

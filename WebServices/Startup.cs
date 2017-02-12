using Microsoft.Owin;
using Owin;
using WebServices;
using Serilog;
using System;
using Microsoft.Extensions.Logging;

[assembly: OwinStartup(typeof(Startup))]

namespace WebServices
{
    public partial class Startup
    {
       public Startup()
        {
          Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "\\logs\\log-{Date}.log", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}", shared: true)
            .CreateLogger();
        }

        public void Configuration(IAppBuilder app, ILoggerFactory loggerFactory)
        {
            //Specifying dispose: true closes and flushes the Serilog `Log` class when the app shuts down.
            loggerFactory.AddSerilog(dispose: true);
            ConfigureAuth(app);
        }
    }
}
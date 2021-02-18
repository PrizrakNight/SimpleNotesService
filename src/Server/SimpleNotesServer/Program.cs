using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SimpleNotesServer
{
    public class Program
    {
        public static long TimeStarted { get; private set; }

        public static void Main(string[] args)
        {
            TimeStarted = DateTimeOffset.Now.ToUnixTimeSeconds();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

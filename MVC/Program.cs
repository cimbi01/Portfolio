using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MVC
{
    public class Program
    {
        /*
        * TODO: MVC:
        * 
        * TODO: Use Hidden fields
        * 
        * Controllers:      
        *      Employer:
        *          Create -> JobAdvertisement
        *          Read -> Own JobOffers -> (Own) ReceivedJobOffers Accept/Decline +->(Own) OfferedJobOfferers Start, End
        *          Read -> Employees ProfessionData -> JobOffer
        *          
        * TODO: Tests
       */

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

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
        * Controllers:
        *      Employee:
        *          Skills List -> Read-Update / Create
        *          References List -> Read-Update / Create
        *          Read -> (Own) ReceivedJobOffers Accept/Decline +-> (Own) ReceivedJobOffers CalculateSuitability()
        *          Read  -> Advertisements (Employers) -> +-> CalculateSuitability, Apply
        *      Employer:
        *          Create -> JobAdvertisement
        *          Read -> Own JobOffers -> (Own) ReceivedJobOffers Accept/Decline +->(Own) OfferedJobOfferers Start, End
        *          Read -> Employees ProfessionData -> JobOffer
        *
        *   Decorator -> RenderView -> JS -> add ability
        *          
        * TODO: Factory
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

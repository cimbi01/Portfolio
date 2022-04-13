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
        *      WorkingPerson
        *          Read -> Own JobOffers (Filter, Sort) -> (Own) ReceivedJobOffers Accept/Decline
        *      Employee:
        *          Create/Read/Update/Delete -> ProfessionData -> Skills, References
        *          Read -> Own JobOffers (Filter, Sort) -> (Own) ReceivedJobOffers Accept/Decline +-> (Own) ReceivedJobOffers CalculateSuitability()
        *          Read  -> Advertisements (Employers) (Filter, Sort) -> (Own) ReceivedJobOffers Accept/Decline +-> CalculateSuitability, Apply
        *      Employer:
        *          Create -> JobAdvertisement
        *          Read -> Employers/Own JobOffers (Filter, Sort, Etc.) -> CalculateSuitability, (Own) ReceivedJobOffers Accept/Decline +->(Own) OfferedJobOfferers Start, End
        *          Read -> Employees/Own Contact, ReceivedJobOffers, OfferedJobOffers +-> JobOffer
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

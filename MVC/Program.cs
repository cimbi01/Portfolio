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
         * AddSingleton<OfferHandler, OfferHandler)
         * AddTransient<Factory, Factory>
         * AddTransient<OfferHandleredFactory, OfferHandleredFactory>
         * 
         * Controllers:
         *      WorkingPerson
         *          Create (Employer / Employee)
         *          Read -> Employers/Own JobOffers (Filter, Sort, Etc.) -> CalculateSuitability -> (Own) ReceivedJobOffers Accept/Decline
         *          Read -> Employees/Own Contact, ReceivedJobOffers, OfferedJobOffers -> (Own) ReceivedJobOffers Accept/Decline
         *          Update -> Own Contact
         *      Employee:
         *          Create/Read/Update/Delete -> ProfessionData -> Skills, References
         *          Read -> Employers/Own JobOffers (Filter, Sort, Etc.) -> CalculateSuitability, (Own) ReceivedJobOffers Accept/Decline + Apply
         *      Employer:
         *          Create -> JobAdvertisement
         *          Read -> Employers/Own JobOffers (Filter, Sort, Etc.) -> CalculateSuitability, (Own) ReceivedJobOffers Accept/Decline +->(Own) OfferedJobOfferers Start, End
         *          Read -> Employees/Own Contact, ReceivedJobOffers, OfferedJobOffers +-> JobOffer
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

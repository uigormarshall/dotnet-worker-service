using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using worker_log.Entities;

namespace worker_log
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private  IList<CompanyEntitity> _companies;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _companies = new List<CompanyEntitity>();
            mockCompanies();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


                foreach (var company in _companies)
                {
                    var ping = IsConectedToHost(company.Uri);
                    var strStatus = ping == true ? "Okay" : "Problem";
                    var timeNow = DateTimeOffset.Now;
                    _logger.LogInformation($"{strStatus} with {company.Name} on: {timeNow}");
                }

                await Task.Delay(10000, stoppingToken);
            }
        }

        public static bool IsConectedToHost(Uri uri)
        {
            try
            {
                Dns.GetHostEntry(uri.Host);
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        private void mockCompanies()
        {
            _companies.Add(new CompanyEntitity("Google", "http://google.com"));
            _companies.Add(new CompanyEntitity("Interisk Site", "https://www.brasiliano.com.br/"));
            _companies.Add(new CompanyEntitity("Interisk Dashboard", "http://google.com"));
            _companies.Add(new CompanyEntitity("Namespace", "http://namespace.net.br"));

        }
    }
}
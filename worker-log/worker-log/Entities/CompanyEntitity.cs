using System;
using System.Collections.Generic;
using System.Text;

namespace worker_log.Entities
{
    public class CompanyEntitity
    {
        public CompanyEntitity(string name, string uri)
        {
            Name = name;
            Uri = new Uri(uri);
        }
        public string Name { get; set; }
        public Uri Uri { get; set; }
    }
}

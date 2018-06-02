using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    public class regionCountry
    {
        public string regionCode { get; set; }
        public List<countryCount> countryCount { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestarauntRatingsConsumerApi
{
    public class RatingsDto
    {
        public Address address { get; set; }
        public string borough { get; set; }
        public string cuisine { get; set; }
        public Grade[] grades { get; set; }
        public string name { get; set; }
        public string restaurant_id { get; set; }
    }

    public class Address
    {
        public string building { get; set; }
        public float coord { get; set; }
        public string street { get; set; }
        public string zipcode { get; set; }
    }

    public class Grade
    {
        public Date date { get; set; }
        public string grade { get; set; }
        public Nullable<int> score { get; set; }
    }

    public class Date
    {
        public long date { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherInfo.Entities
{
    public class SearchCriteria
    {
        /// <summary>
        /// value should be zipcode and country code seperated by comma(,)
        /// 500089,IN
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// city
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// flag used in UI to select zipcode/city
        /// </summary>
        public bool isZipCode { get; set; }
    }
}


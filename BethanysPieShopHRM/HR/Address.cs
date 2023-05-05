using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.HR
{
    public class Address
    {
        private string street;
        private string city;
        private string houseNumber;
        private string zipCode;

        public Address(string street, string city, string houseNumber, string zipCode)
        {
            Street = street;
            City = city;
            HouseNumber = houseNumber;
            ZipCode = zipCode;
        }

        public string Street { get { return street; } set { street = value; }}   

        public string City { get { return city; } set { city = value; } }

        public string HouseNumber { get { return houseNumber; } set { houseNumber = value; } }

        public string ZipCode { get { return zipCode; } set { zipCode = value; }}
    }
}

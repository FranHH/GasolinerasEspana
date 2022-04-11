using System;
using System.Collections.Generic;
using System.Text;

namespace GasolinerasEspana.Models
{
    [Serializable]
    public class GetAddress
    {
        public GetAddress(Address iLListAdress)
        {
            this.Address = iLListAdress;
        }

        public Address Address { get; set; }

    }

    public class Address
    {
        public Address(){}
        
        public Address(string code, string code3, string code1, 
                            string lng, string distancia, string housenumber, 
                            string locali, string code4, string name2, string calle, 
                            string codigoPostal, string countryCode, string name1, string lat)
        {
            this.adminCode2 = code;
            this.adminCode3 = code3;
            this.adminCode1 = code1;
            this.lng = lng;
            this.distance = distancia;
            this.houseNumber = housenumber;
            this.locality = locali;
            this.adminCode4 = code4;
            this.adminName2 = name2;
            this.street = calle;
            this.postalcode = codigoPostal;
            this.countryCode = countryCode;
            this.adminName1 = name1;
            this.lat = lat;
        }


        public string adminCode2 { get; set; }
        public string adminCode3 { get; set; }
        public string adminCode1 { get; set; }
        public string lng { get; set; }
        public string distance { get; set; }
        public string houseNumber { get; set; }
        public string locality { get; set; }
        public string adminCode4 { get; set; }
        public string adminName2 { get; set; }
        public string street { get; set; }
        public string postalcode { get; set; }
        public string countryCode { get; set; }
        public string adminName1 { get; set; }
        public string lat { get; set; }

    }
}

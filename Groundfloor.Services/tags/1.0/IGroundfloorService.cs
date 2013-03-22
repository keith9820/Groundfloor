using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Groundfloor.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGroundfloorService" in both code and config file together.
    [ServiceContract]
    public interface IGroundfloorService
    {
        [OperationContract]
        string Encrypt(string data, string secret, bool urlencode = false);

        [OperationContract]
        string Decrypt(string data, string secret, bool urldecode = false);

        [OperationContract(Name="GeocodeByZip")]
        Location Geocode(string zipcode);

        [OperationContract]
        Location Geocode(string city, string state);

        [OperationContract]
        Location ReverseGeocode(string latitude, string longitude);

        [OperationContract]
        string GetToken(bool urlencode = false);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Location
    {
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Latitude { get; set; }
        [DataMember]
        public string Longitude { get; set; }
        [DataMember]
        public string Zipcode { get; set; }
    }
}
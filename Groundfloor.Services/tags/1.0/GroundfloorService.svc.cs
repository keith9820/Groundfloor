using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Groundfloor.Security;
using System.Configuration;

namespace Groundfloor.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GroundfloorService" in code, svc and config file together.
    public class GroundfloorService : IGroundfloorService
    {
        public string Encrypt(string data, string secret, bool urlencode = false)
        {
            var result = Encryptor.Encrypt64(data, secret);

            if (urlencode)
                result = result.EncodeUrl();

            return result;
        }

        public string Decrypt(string data, string secret, bool urldecode = false)
        {
            if (urldecode)
                data = data.DecodeUrl();

            return data.Decrypt64(secret);
        }

        public Location Geocode(string zipcode)
        {
            var context = new GeoLocationEntities();
            var loc = (from USCity c in context.USCity
                       let zip = context.USCity.Select(x => c.Zipcode).Cast<string>().FirstOrDefault()
                      where zip == zipcode
                      select c).FirstOrDefault();

            if (loc != null)
            {
                return new Location { City = loc.City, State = loc.State, Latitude = loc.Lat.ToString(), Longitude = loc.Lon.ToString(), Zipcode = zipcode };
            }

            return new Location();
        }
        public Location Geocode(string city, string state)
        {
            var context = new GeoLocationEntities();
            var loc = context.USCities.Where(c => c.City.Equals(city, StringComparison.InvariantCultureIgnoreCase) && c.Region.Equals(state, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            if (loc != null)
            {
                return new Location { City = city, State = state, Latitude = loc.Latitude.GetValueOrDefault().ToString(), Longitude = loc.Longitude.GetValueOrDefault().ToString() };
            }

            return new Location();
        }

        public Location ReverseGeocode(string latitude, string longitude)
        {
            var context = new GeoLocationEntities();
            //todo:  calculate nearest city
            var loc = context.USCities.Where(c => c.Latitude.ToString() == latitude && c.Longitude.ToString() == longitude).FirstOrDefault();

            if (loc != null)
            {
                return new Location { City = loc.City, State = loc.Region, Latitude = latitude, Longitude = longitude };
            }

            return new Location();
        }


        public string GetToken(bool urlencode = false)
        {
            string secret = ConfigurationManager.AppSettings["AppSecret"];
            string data = DateTime.Now.ToUniversalTime().ToString();
            return Encrypt(data, secret, urlencode);
        }
    }
}

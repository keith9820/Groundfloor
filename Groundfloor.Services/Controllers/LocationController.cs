using System;
using System.Linq;
using System.Web.Mvc;
using GroundFloor.Services.Models;
using MvcApplication1.Models;

namespace GroundFloor.Services.Controllers
{
    public class LocationController : Controller
    {
        public ActionResult GeocodeZip(string zipcode)
        {
            var context = new GeoLocationEntities();
            var loc = (from USCity c in context.USCity
                       let zip = context.USCity.Select(x => c.Zipcode).Cast<string>().FirstOrDefault()
                       where zip == zipcode
                       select c).FirstOrDefault();

            if (loc != null)
            {
                return Json(new Location { City = loc.City, State = loc.State, Latitude = loc.Lat.ToString(), Longitude = loc.Lon.ToString(), Zipcode = zipcode }, "text/plain", JsonRequestBehavior.AllowGet);
            }

            return Json(new Location(), "text/plain", JsonRequestBehavior.AllowGet);
        }
        public ActionResult GeocodeCity(string city, string state)
        {
            var context = new GeoLocationEntities();
            var loc = context.USCities.Where(c => c.City.Equals(city, StringComparison.InvariantCultureIgnoreCase) && c.Region.Equals(state, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            if (loc != null)
            {
                return Json(new Location { City = city, State = state, Latitude = loc.Latitude.GetValueOrDefault().ToString(), Longitude = loc.Longitude.GetValueOrDefault().ToString()}, "text/plain", JsonRequestBehavior.AllowGet);
            }

            return Json(new Location(), "text/plain", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReverseGeocode(string latitude, string longitude)
        {
            var context = new GeoLocationEntities();
            //todo:  calculate nearest city
            var loc = context.USCities.Where(c => c.Latitude.ToString() == latitude && c.Longitude.ToString() == longitude).FirstOrDefault();

            if (loc != null)
            {
                return Json(new Location { City = loc.City, State = loc.Region, Latitude = latitude, Longitude = longitude }, "text/plain", JsonRequestBehavior.AllowGet);
            }

            return Json(new Location(), "text/plain", JsonRequestBehavior.AllowGet);
        }
    }
}

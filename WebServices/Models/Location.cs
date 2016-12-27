
namespace WebServices.Models
{
    public class Location
    {

        public Location(double Lat, double Lng)
        {
            this.Lat = Lat;
            this.Lng = Lng;
        }
        public double Lat { get; set; }

        public double Lng { get; set; }
    }
}
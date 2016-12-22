namespace WebServices.Models
{
    public class Location
    {
        public Location(double lat, double lng)
        {
            this.Lat = lat;
            this.Lng = lng;
        }

        public double Lat;       

        public double Lng;       


    }
}
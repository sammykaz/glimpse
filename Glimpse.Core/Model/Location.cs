using MvvmCross.Core.ViewModels;

namespace Glimpse.Core.Model
{
    public class Location : MvxNotifyPropertyChanged
    {
        public Location(double lat, double lon)
        {
            _lat = lat;
            _lon = lon;
        }

        private double _lat;

        public double Lat
        {
            get { return _lat; }
            set
            {
                _lat = value;
                RaisePropertyChanged(() => Lat);
            }
        }

        private double _lon;

        public double Lon
        {
            get { return _lon; }
            set
            {
                _lon = value;
                RaisePropertyChanged(() => Lon);
            }
        }

        public override bool Equals(object obj)
        {
            var lRhs = obj as Location;
            if (lRhs == null)
                return false;

            return (lRhs.Lat == Lat) && (lRhs.Lon == Lon);
        }

        public override int GetHashCode()
        {
            return Lat.GetHashCode() + Lon.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0:0.00000} {1:0.00000}", Lat, Lon);
        }
    }
}
using KozzionCore.DataStructure.Science;

namespace KozzionGeography.GeoLocation
{
    public struct GeoLocationDecimalDegree
    {
        public AngleDegree Latitude { get; private set; }
        public AngleDegree Longitude { get; private set; }

    
        public GeoLocationDecimalDegree(AngleDegree latitude, AngleDegree longitude)
            : this()
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return "Latitude: " + Latitude + " Longitude " + Longitude;
        }
    }
}

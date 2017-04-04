using KozzionCore.DataStructure.Science;
using KozzionGeography.GeoLocation;
using System;

namespace KozzionGeography.Tools
{
    public class ToolsGeoLocation
    {
  

        public static Meter ComputeDistanceHaversine(GeoLocationDecimalDegree location_0, GeoLocationDecimalDegree location_1)
        {
            return ComputeDistanceHaversine(location_0.Latitude, location_0.Longitude, location_1.Latitude, location_1.Longitude);
        }
        public static Meter ComputeDistanceHaversine(AngleRadian latitude_radian_0, AngleRadian longitude_radian_0, AngleRadian latitude_radian_1, AngleRadian longitude_radian_1)
        {
            //Haversine
            AngleRadian latitude_difference = latitude_radian_1 - latitude_radian_0;
            AngleRadian longitude_difference = longitude_radian_1 - longitude_radian_0;

            double arc = Math.Sin((double)latitude_difference / 2.0) * Math.Sin((double)latitude_difference / 2.0) +
                Math.Cos((double)latitude_radian_0) * Math.Cos((double)latitude_radian_1) *
                Math.Sin((double)longitude_difference / 2.0) * Math.Sin((double)longitude_difference / 2);
            return GeographyConstants.EarthRadiusInMeters * 2.0 * Math.Asin(Math.Min(1, Math.Sqrt(arc)));
        }
   
        public static Meter ComputeDistanceEquirectangular(GeoLocationDecimalDegree location_0, GeoLocationDecimalDegree location_1)
        {
            //Scuks
            return ComputeDistanceEquirectangular(location_0.Latitude, location_0.Longitude, location_1.Latitude, location_1.Longitude);
        }
        public static Meter ComputeDistanceEquirectangular(AngleRadian latitude_0, AngleRadian longitude_0, AngleRadian latitude_1, AngleRadian longitude_1)
        {
            double x = (double)(longitude_1 - longitude_0) * Math.Cos((double)(latitude_0 + latitude_1) / 2.0);
            double y = (double)(latitude_1 - latitude_0);
            return Math.Sqrt(x * x + y * y) * GeographyConstants.EarthRadiusInMeters;
        }
    }
}

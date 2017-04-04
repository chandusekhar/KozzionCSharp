using KozzionCore.DataStructure.Science;
using KozzionGeography.Tools;
using KozzionMathematics.Function;

namespace KozzionGeography.GeoLocation
{
    class FunctionDistanceGeoLocation : IFunctionDistance<GeoLocationDecimalDegree, Meter>
    {
        public string FunctionType { get { return "FunctionDistanceGeoLocation"; } }
        public Meter Compute(GeoLocationDecimalDegree value_0, GeoLocationDecimalDegree value_1)
        {
            return ToolsGeoLocation.ComputeDistanceHaversine(value_0, value_1);
        }

        public Meter ComputeToRectangle(GeoLocationDecimalDegree valeu_0, GeoLocationDecimalDegree upper, GeoLocationDecimalDegree lower)
        {
            throw new System.NotImplementedException();
        }
    }
}

using KozzionCore.DataStructure.Science;
using KozzionGeography.GeoLocation;
using KozzionMathematics.Tools;
using System;
using System.Collections.Generic;

namespace KozzionGeography.NavalTracker
{
    public class NavalTracker
    {
        public Meter SurveyRangeInMeters { get; private set; }
        public NavalTracker(double sight_range_in_meters) 
        {
            this.SurveyRangeInMeters = new Meter(sight_range_in_meters);
        }

        public NavalTracker()
          : this(20000)
        {
        }

        public void Track(List<Tuple<GeoLocationDecimalDegree, string>> ship_pings)
        { 

        }


        public Tuple<Second[], Second[], MeterPerSecond[]> CheckPattern(List<Tuple<GeoLocationDecimalDegree, DateTime>> pings)
        {
            if (pings.Count  < 2)
            {
                return new Tuple<Second[], Second[], MeterPerSecond[]>(new Second[pings.Count], new Second[pings.Count], new MeterPerSecond[pings.Count]);
            }
            int ping_count = pings.Count;
            GeoLocationDecimalDegree[] ship_ping_locations = new GeoLocationDecimalDegree[ping_count];
            for (int ping_index = 0; ping_index < ping_count; ping_index++)
			{
                ship_ping_locations[ping_index] = pings[ping_index].Item1;
            }
            Second[] ping_seconds            = new Second[ping_count];
            MeterPerSecond[] ping_velocities = new MeterPerSecond[ping_count];
            //double[] ping_areas = new double[ping_count];
            Second[] ping_seconds_in_range = new Second[ping_count];
    

            Meter[,] distance_matrix = ToolsMathFunction.FillArray<GeoLocationDecimalDegree,GeoLocationDecimalDegree,Meter>(new FunctionDistanceGeoLocation(),ship_ping_locations,ship_ping_locations);


            Meter ping_distance = new Meter();
            ping_seconds[0] =(Second) ((pings[1].Item2 - pings[0].Item2).TotalMilliseconds) / 2000.0;
            ping_distance = distance_matrix[0, 1] / 2.0;
            ping_velocities[0] = ping_distance / ping_seconds[0];
            for (int ping_index = 1; ping_index < ping_count - 1; ping_index++)
			{
                ping_seconds[ping_index] += new Second((pings[ping_index].Item2 - pings[ping_index - 1].Item2).TotalMilliseconds) / 2000.0;
                ping_seconds[ping_index] += new Second((pings[ping_index + 1].Item2 - pings[ping_index].Item2).TotalMilliseconds) / 2000.0;
                ping_distance = (distance_matrix[ping_index, ping_index - 1] + distance_matrix[ping_index, ping_index + 1]) / 2.0;
                ping_velocities[ping_index] = ping_distance / ping_seconds[ping_index];
            }
            ping_seconds[ping_count - 1] = new Second((pings[ping_count - 1].Item2 - pings[ping_count - 2].Item2).TotalMilliseconds) / 2000.0;
            ping_distance = distance_matrix[ping_count - 2, ping_count - 1] / 2.0;
            ping_velocities[ping_count - 1] = ping_distance / ping_seconds[ping_count - 1];


            for (int index_ping_0 = 0; index_ping_0 < ping_count; index_ping_0++)
			{    
	            for (int index_ping_1 = 0; index_ping_1 < ping_count; index_ping_1++)
			    {
                    if(distance_matrix[index_ping_0, index_ping_1] < SurveyRangeInMeters)
                    {
                        ping_seconds_in_range[index_ping_0] += ping_seconds[index_ping_1];
                    }
    
                }
            }
            return new Tuple<Second[], Second[], MeterPerSecond[]>(ping_seconds, ping_seconds_in_range, ping_velocities);
        }
    }
}

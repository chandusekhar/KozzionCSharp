
using KozzionCore.DataStructure.Science;
using KozzionCore.IO.CSV;
using KozzionCore.Tools;
using KozzionGeography.GeoLocation;
using KozzionGeography.NavalTracker;
using KozzionGeography.Tools;
using KozzionPlotting.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KozzionGeographyCL
{
    class Program
    {
        static void Main(string[] args)
        {
            string path        = @"E:\Data\Dropbox\Dropbox\TestData\XWhite3\DATA\";
            string file_input = ""; //TODO
            //string file_input  = "POLAR_DUKE_SURVEY_.txt";
            Dictionary<string, List<Tuple<GeoLocationDecimalDegree, DateTime>>> data = ReadData(path + file_input);
            //Dictionary<string, List<Tuple<GeoLocationDecimal, DateTime>>> data = ReadData(@"E:\Data\Dropbox\Dropbox\TestData\XWhite3\DATA\FUGRO.txt");
            //Dictionary<string, List<Tuple<GeoLocationDecimal, DateTime>>> data = ReadData(@"E:\Data\Dropbox\Dropbox\TestData\XWhite3\DATA\FUGRO.txt");
            //Dictionary<string, List<Tuple<GeoLocationDecimal, DateTime>>> data = ReadData(@"E:\Data\Dropbox\Dropbox\TestData\XWhite3\DATA\FUGRO.txt");
            //Dictionary<string, List<Tuple<GeoLocationDecimal, DateTime>>> data = ReadData(@"E:\Data\Dropbox\Dropbox\TestData\XWhite3\DATA\FUGRO.txt");
            //Dictionary<string, List<Tuple<GeoLocationDecimal, DateTime>>> data = ReadData(@"E:\Data\Dropbox\Dropbox\TestData\XWhite3\DATA\FUGRO.txt");

            NavalTracker tracker = new NavalTracker(20000);
            foreach(string ship_id in data.Keys)
			{
                System.Console.WriteLine("ship_id: " + ship_id);
                List<Tuple<GeoLocationDecimalDegree, DateTime>> ship_data = data[ship_id];

                //for (int point_index = 1; point_index < ship_data.Count - 1; point_index++)                
                //{
                //    System.Console.WriteLine(ship_data[point_index].Item2);
                //    System.Console.WriteLine((ship_data[point_index + 1].Item2 - ship_data[point_index - 1].Item2).TotalMilliseconds / 1000.0);
                //    System.Console.WriteLine(ship_data[point_index + 1].Item1);
                //    System.Console.WriteLine(ship_data[point_index - 1].Item1);    
                //    System.Console.WriteLine(ToolsGeoLocation.ComputeDistanceHaversine(ship_data[point_index + 1].Item1,ship_data[point_index - 1].Item1) / 2);
                    
                //}
                //System.Console.Read();
                Tuple<Second[], Second[], MeterPerSecond[]> result = tracker.CheckPattern(ship_data);
                for (int point_index = 0; point_index < ship_data.Count; point_index++)
                {
                    System.Console.WriteLine(result.Item1[point_index] + "\t"  + result.Item2[point_index] + "\t" + result.Item3[point_index]);
                }
                //ToolsPlotting.PlotLinesXY(path + ship_id + "time.png", result.Item2));
                //ToolsPlotting.PlotLinesXY(path + ship_id + "velocity.png", result.Item3);
			}
     
            //TestComputeDistance0();
        }

        private static Dictionary<string, List<Tuple<GeoLocationDecimalDegree, DateTime>>> ReadData(string file_path)
        {

            string [,] table = ToolsIOCSV.ReadCSVFile(file_path, Delimiter.Comma);
            //for (int i = 0; i < table.GetLength(1); i++)
            //{
            //    System.Console.WriteLine(i);
            //    System.Console.WriteLine(table[0, i]);
            //}
            Dictionary<string, List<Tuple<GeoLocationDecimalDegree, DateTime>>> data = new Dictionary<string, List<Tuple<GeoLocationDecimalDegree, DateTime>>>();
            for (int i = 1; i < table.GetLength(0); i++)
            {

                string ship_id = table[i, 14];
                double latitutde = Double.Parse(table[i, 5].Replace(".", ","));
                double longitude = Double.Parse(table[i, 4].Replace(".", ","));
                string[] date_time_utc_split = table[i, 11].Split(" ".ToCharArray());
                string[] date_utc_split = date_time_utc_split[0].Split("/".ToCharArray());
                DateTime time = DateTime.Parse(date_time_utc_split[1]);
                DateTime date = new DateTime(Int32.Parse(date_utc_split[2]), Int32.Parse(date_utc_split[0]), Int32.Parse(date_utc_split[1]));
                DateTime data_time = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, DateTimeKind.Utc);
                System.Console.WriteLine(ship_id);
                System.Console.WriteLine(latitutde);
                System.Console.WriteLine(longitude);
                System.Console.WriteLine(data_time);
                if (!data.ContainsKey(ship_id))
                {
                    data.Add(ship_id, new List<Tuple<GeoLocationDecimalDegree, DateTime>>());
                }
                data[ship_id].Add(new Tuple<GeoLocationDecimalDegree,DateTime>(new GeoLocationDecimalDegree((AngleDegree)latitutde, (AngleDegree)longitude), data_time));
            }
            List<string> ship_ids = new List<string>(data.Keys);
            foreach (string ship_id in ship_ids)
            {
                data[ship_id] = new List<Tuple<GeoLocationDecimalDegree, DateTime>>(data[ship_id].OrderBy(tuple => tuple.Item2));
            }

            return data;
        }

        private static void TestComputeDistance0()
        {

            // zero distance
            System.Console.WriteLine(ToolsGeoLocation.ComputeDistanceHaversine(new GeoLocationDecimalDegree(), new GeoLocationDecimalDegree()));
            // utrecht Groningen 156.49Km
            System.Console.WriteLine(ToolsGeoLocation.ComputeDistanceHaversine(new GeoLocationDecimalDegree((AngleDegree)52.107995, (AngleDegree)5.119632), new GeoLocationDecimalDegree((AngleDegree)53.213763, (AngleDegree)6.555392)));
            // utrecht Groningen 234.31Km
            System.Console.WriteLine(ToolsGeoLocation.ComputeDistanceHaversine(new GeoLocationDecimalDegree((AngleDegree)52.107995, (AngleDegree)5.119632), new GeoLocationDecimalDegree((AngleDegree)52.043364, (AngleDegree)8.546902)));
            
            // zero distance
            System.Console.WriteLine(ToolsGeoLocation.ComputeDistanceEquirectangular(new GeoLocationDecimalDegree(), new GeoLocationDecimalDegree()));
            // utrecht Bieleveld 156.49Km
            System.Console.WriteLine(ToolsGeoLocation.ComputeDistanceEquirectangular(new GeoLocationDecimalDegree((AngleDegree)52.107995, (AngleDegree)5.119632), new GeoLocationDecimalDegree((AngleDegree)53.213763, (AngleDegree)6.555392)));
            // utrecht Bieleveld 234.31Km
            System.Console.WriteLine(ToolsGeoLocation.ComputeDistanceEquirectangular(new GeoLocationDecimalDegree((AngleDegree)52.107995, (AngleDegree)5.119632), new GeoLocationDecimalDegree((AngleDegree)52.043364, (AngleDegree)8.546902)));
           
    
        }
    }
}

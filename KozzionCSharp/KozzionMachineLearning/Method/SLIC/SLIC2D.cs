
using KozzionMathematics.Function.Implementation.Distance;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;

namespace KozzionMachineLearning.Method.SLIC
{
    public class SLIC2D : ASLIC<IRaster2DInteger, float[]>
    {

        public SLIC2D(
            int iteration_count, 
            int [] desired_cluster_dimensions,
            float feature_weight) 

            : base(
                iteration_count,
                desired_cluster_dimensions,
                new FunctionDistanceEuclidean(),
                null,
                feature_weight)
        {
            Debug.Assert(d_cluster_dimensions.Length == 2);
        }



        public override Tuple<IImageRaster<IRaster2DInteger, int>, IImageRaster<IRaster2DInteger, float>, IList<int[]>, IList<float[]>> Initialize(IRaster2DInteger raster)
        {
            IImageRaster2D<int> region_image = new ImageRaster2D<int>(raster);
            IImageRaster2D<float> distance_image = new ImageRaster2D<float>(raster, Single.MaxValue);
            Tuple<IList<int[]>, IList<float[]>> Centroids = InitializeCentroids(raster);
            IList<int[]> cluster_spatial_centroids = Centroids.Item1;
            IList<float[]> cluster_feature_centroids = Centroids.Item2;

            int[] neigbourhood_dimensions = d_cluster_dimensions_double;
            int neigbourhood_element_count = ((neigbourhood_dimensions[0] * 2) + 1) * ((neigbourhood_dimensions[1] * 2) + 1);
            int[] element_index_indexes = new int[neigbourhood_element_count];

            for (int centroid_index = 0; centroid_index < cluster_spatial_centroids.Count; centroid_index++)
            {
                raster.GetNeigbourhoodElementIndexesRBA(cluster_spatial_centroids[centroid_index], neigbourhood_dimensions, element_index_indexes);
                for (int element_index_index = 0; element_index_index < neigbourhood_element_count; element_index_index++)
                {
                    int element_index = element_index_indexes[element_index_index];
                    if (element_index != -1)
                    {
                        int[] element_coordinates = raster.GetElementCoordinates(element_index);
                        float distance = FunctionDistanceEuclidean.ComputeStatic(element_coordinates, cluster_spatial_centroids[centroid_index]);
                        if (distance < distance_image.GetElementValue(element_index))
                        {
                            distance_image.SetElementValue(element_index, distance);
                            region_image.SetElementValue(element_index, centroid_index);
                        }
                    }
                }
            }
            return new Tuple<IImageRaster<IRaster2DInteger, int>, IImageRaster<IRaster2DInteger, float>, IList<int[]>, IList<float[]>>(region_image, distance_image, cluster_spatial_centroids, cluster_feature_centroids);
        }


        public Tuple<IList<int[]>, IList<float[]>> InitializeCentroids(IRaster2DInteger raster)
        {
            int centroid_count_x = raster.Size0 / d_cluster_dimensions[0];
            int centroid_count_y = raster.Size1 / d_cluster_dimensions[1];

            int centroid_offset_x = ((raster.Size0 % d_cluster_dimensions[0]) + d_cluster_dimensions[0]) / 2;
            int centroid_offset_y = ((raster.Size1 % d_cluster_dimensions[1]) + d_cluster_dimensions[1]) / 2;

            int centroid_count = centroid_count_x * centroid_count_y;
            IList<int[]> cluster_spatial_centroids = new List<int[]>();
            IList<float[]> cluster_feature_centroids = new List<float[]>();
            for (int index_centroid_x = 0; index_centroid_x < centroid_count_x; index_centroid_x++)
            {
                for (int index_centroid_y = 0; index_centroid_y < centroid_count_y; index_centroid_y++)
                {
                    cluster_spatial_centroids.Add(new int[2]
                    {
                        centroid_offset_x + (d_cluster_dimensions[0] * (index_centroid_x)),
                        centroid_offset_y + (d_cluster_dimensions[1] * (index_centroid_y))                       
                    });
                    cluster_feature_centroids.Add(new float[0]);
                }
            }
            return new Tuple<IList<int[]>, IList<float[]>>(cluster_spatial_centroids, cluster_feature_centroids);
        }

    }
}

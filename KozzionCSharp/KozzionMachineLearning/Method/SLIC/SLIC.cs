using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace KozzionMachineLearning.Method.SLIC
{

    public abstract class ASLIC<IRasterType, DomainType>
        where IRasterType : IRasterInteger
    {
   
        private int d_iteration_count;
        protected int[] d_cluster_dimensions;
        protected int[] d_cluster_dimensions_double;
        private IFunctionDistance<DomainType, float> d_distance_features;
        private IFunction<IList<DomainType>, DomainType> d_centroid_feature_calculator;
        private float feature_weight;

        public ASLIC(
            int iteration_count,
            int [] cluster_dimensions,
            IFunctionDistance<DomainType, float> distance_features,
            IFunction<IList<DomainType>, DomainType> centroid_calculator,
            float feature_weight)
        {
            this.d_iteration_count = iteration_count;
            this.d_cluster_dimensions = cluster_dimensions;
            this.d_cluster_dimensions_double = ToolsMathCollectionInteger.Multiply(d_cluster_dimensions, 2);
            this.d_distance_features = distance_features;
            this.d_centroid_feature_calculator = centroid_calculator;
            this.feature_weight = feature_weight;
        }


        public Tuple<IImageRaster<IRasterType, int>, IList<int[]>, IList<DomainType>> Cluster(IImageRaster<IRasterType, DomainType> image_features)
        {
            Debug.Assert(image_features.Raster.DimensionCount == d_cluster_dimensions.Length);

            //Initialize cluster spatial centress at regular grid steps S.
            Tuple<IImageRaster<IRasterType, int>, IImageRaster<IRasterType, float>, IList<int[]>, IList<DomainType>> initialization = Initialize(image_features.Raster);
            IImageRaster<IRasterType, int> image_labeling = initialization.Item1;
            IImageRaster<IRasterType, float> image_distance = initialization.Item2;
            IList<int[]> cluster_spatial_centroids = initialization.Item3;
            IList<DomainType> cluster_feature_centroids = initialization.Item4;

            int dimensions_count = image_features.Raster.DimensionCount;
            int cluster_count = cluster_spatial_centroids.Count;

            // Find out how big the neighborhood should be
            int count_neigbourhood_elements = 1;
            for (int index_dimension = 0; index_dimension < dimensions_count; index_dimension++)
			{
                count_neigbourhood_elements *= (d_cluster_dimensions[index_dimension] * 2) + 1;
            }
            int[] neigbourhood_element_indexes = new int[count_neigbourhood_elements];


            //Initialize cluster feature centroids at their suposed positions
            ComputeCentroids(image_labeling, image_features, cluster_spatial_centroids, cluster_feature_centroids);

            for (int iteration_index = 0; iteration_index < d_iteration_count; iteration_index++)
			{
                AssignElements(image_labeling, image_distance, image_features, cluster_spatial_centroids, cluster_feature_centroids, neigbourhood_element_indexes);
                ComputeCentroids(image_labeling, image_features, cluster_spatial_centroids, cluster_feature_centroids);
            }


            //TODO postprocessing Remove small isolated components
            return new Tuple<IImageRaster<IRasterType,int>,IList<int[]>,IList<DomainType>>(image_labeling, cluster_spatial_centroids, cluster_feature_centroids);
        }

        public void ComputeCentroids(
            IImageRaster<IRasterType, int> image_labeling,
            IImageRaster<IRasterType, DomainType> image_data, 
            IList<int[]> cluster_spatial_centroids, 
            IList<DomainType> cluster_feature_centroids)
        {
            //Use the cluster raster for spacing the clusters in the image  
            IList<IList<DomainType>> clusters = new List<IList<DomainType>>();
            int dimension_count = image_labeling.Raster.DimensionCount;
            int element_count = image_labeling.Raster.ElementCount;
            int [] element_coordinates = new int[dimension_count];
            //clear clusters
            // TODO make paralel
            for (int cluster_index = 0; cluster_index < cluster_spatial_centroids.Count; cluster_index++)
            {
                clusters.Add(new List<DomainType>());
                for (int dimension_index = 0; dimension_index < image_labeling.Raster.DimensionCount; dimension_index++)
                {
                    cluster_spatial_centroids[cluster_index][dimension_index] = 0;
                }
            }

            // Aggregate cluster.
            // TODO make paralel
            for (int element_index = 0; element_index < element_count; element_index++)
            {
                int cluster_index = image_labeling.GetElementValue(element_index);
                image_data.Raster.GetElementCoordinatesRBA(element_index, element_coordinates);
                clusters[cluster_index].Add(image_data.GetElementValue(element_index));
                ToolsMathCollectionInteger.AddFill(cluster_spatial_centroids[cluster_index], element_coordinates, cluster_spatial_centroids[cluster_index]);
            }

            // Compute new cluster centers.
            // TODO make paralel
            for (int cluster_index = 0; cluster_index < clusters.Count; cluster_index++)
            {
                if (clusters[cluster_index].Count != 0)
                {
                    for (int dimension_index = 0; dimension_index < image_labeling.Raster.DimensionCount; dimension_index++)
                    {
                        cluster_spatial_centroids[cluster_index][dimension_index] /= clusters[cluster_index].Count;
                    }
                    cluster_feature_centroids[cluster_index] = d_centroid_feature_calculator.Compute(clusters[cluster_index]);
                }
                else
                {
                    // reduce cluster count
                    cluster_feature_centroids.RemoveAt(cluster_index);
                    cluster_spatial_centroids.RemoveAt(cluster_index);
                    clusters.RemoveAt(cluster_index);
                    cluster_index--;
                }
            }  
        }

        public void AssignElements(
            IImageRaster<IRasterType, int> image_labeling, 
            IImageRaster<IRasterType, float> image_distance, 
            IImageRaster<IRasterType, DomainType> image_data,
            IList<int[]> cluster_spatial_centroids, 
            IList<DomainType> cluster_feature_centroids,
            int[] neigbourhood_element_indexes)
        {
            int dimension_count = image_labeling.Raster.DimensionCount;
            int[] element_coordinates = new int[dimension_count];

            for (int element_index = 0; element_index < image_distance.Raster.ElementCount; element_index++)
            {
                image_distance.SetElementValue(element_index, Single.MaxValue);
            }

            //for each cluster
            // TODO make paralel (Beware race conditoin in setting labeling and distance RB ordering might help or make it image driven)
            for (int index_cluster = 0; index_cluster < cluster_spatial_centroids.Count; index_cluster++)
            {
                DomainType centroid = cluster_feature_centroids[index_cluster];
                int[] coordinates_cluster = cluster_spatial_centroids[index_cluster];

                //for each pixel in a region around the cluster neigbourhood
                image_data.Raster.GetNeigbourhoodElementIndexesRBA(coordinates_cluster, this.d_cluster_dimensions, neigbourhood_element_indexes);
                for (int element_index_index = 0; element_index_index < neigbourhood_element_indexes.Length; element_index_index++)
                {   
                    int index_element = neigbourhood_element_indexes[element_index_index];
                    if (index_element != -1)
                    {
                        image_data.Raster.GetElementCoordinatesRBA(index_element, element_coordinates);
                        //Compute the distance D between the element and the cluster.
                        float distance = 0;
                        for (int dimension_index = 0; dimension_index < dimension_count; dimension_index++)
                        {
                            float dimension_distance = ((float)Math.Abs(coordinates_cluster[dimension_index] - element_coordinates[dimension_index])) / this.d_cluster_dimensions[dimension_index];
                            distance += dimension_distance * dimension_distance;
                        }
                        distance += d_distance_features.Compute(centroid, image_data.GetElementValue(index_element)) * this.feature_weight;
                        //Compute if it is less than the current distance update cluster membership
                        if (distance < image_distance.GetElementValue(index_element))
                        {
                            image_labeling.SetElementValue(index_element, index_cluster);
                            image_distance.SetElementValue(index_element, distance);
                        }
                    }
                }

            }
        }

        public abstract Tuple<IImageRaster<IRasterType, int>, IImageRaster<IRasterType, float>, IList<int[]>, IList<DomainType>> Initialize(IRasterType raster);
    }
}

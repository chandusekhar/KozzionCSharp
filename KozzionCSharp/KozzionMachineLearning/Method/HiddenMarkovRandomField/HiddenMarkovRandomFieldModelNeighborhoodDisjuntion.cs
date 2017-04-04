using System;
using System.Collections.Generic;
using System.Diagnostics;
using KozzionCore.Tools;
using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionMathematics.Function;

namespace KozzionMachineLearning.Method
{
    public class ModelHMRFNeighborhoodDisjuntion<RasterType, FeatureType>
        where RasterType : IRasterInteger
    {

        private IFunction<int[], float>[] label_likelyhood_models;
        private IFunction<FeatureType[], float>[] feature_likelyhood_models;
        private int background_label;
        private FeatureType background_feature;
        private int label_count;
        private int feature_image_count;
        private int [] neighborhood_size;
        private int neighborhood_element_count;

        public ModelHMRFNeighborhoodDisjuntion(
            IFunction<int[], float>[] label_likelyhood_models,
            IFunction<FeatureType[], float>[] feature_likelyhood_models,
            int background_label,
            FeatureType background_feature,
            int feature_image_count,
            int[] neighborhood_size)
        {
            Debug.Assert(label_likelyhood_models.Length == feature_likelyhood_models.Length);
            this.label_likelyhood_models = label_likelyhood_models;
            this.feature_likelyhood_models = feature_likelyhood_models;
            this.background_label = background_label;
            this.background_feature = background_feature;
            this.label_count = label_likelyhood_models.Length;
            this.feature_image_count = feature_image_count;
            this.neighborhood_size = ToolsCollection.Copy(neighborhood_size);
            this.neighborhood_element_count = 1;
            for (int dimension_index = 0; dimension_index < neighborhood_size.Length; dimension_index++)
            {
                this.neighborhood_element_count *= (neighborhood_size[dimension_index] * 2) + 1;
            }
        }


        public void ImproveLabeling(
            IImageRaster<RasterType, int> initial_labeling, 
            IImageRaster<RasterType, int> new_labeling, 
            IList<IImageRaster<RasterType, FeatureType>> feature_images)
        {
            int element_count = initial_labeling.Raster.ElementCount;
            int neighborhood_size = 0;

            int [] element_coordinates = new int [initial_labeling.Raster.DimensionCount];
            int [] element_neighborhood_indexes = new int [this.neighborhood_element_count];
            int [] state_vector = new int [this.neighborhood_element_count];
            FeatureType [] feature_neighborhood_values = new FeatureType [neighborhood_size];
            FeatureType [] feature_vector = new FeatureType [neighborhood_size];

            for (int element_index = 0; element_index < element_count; element_index++)
            {
                initial_labeling.Raster.GetNeigbourhoodElementIndexesRBA(element_coordinates, this.neighborhood_size, element_neighborhood_indexes);
               // initial_labeling.GetElementValuesFill(element_neighborhood_indexes, this.background_label, state_vector);

                for (int feature_image_index = 0; feature_image_index < feature_image_count; feature_image_index++)
			    {
                    //feature_images[feature_image_index].GetElementValuesFill(element_neighborhood_indexes, this.background_feature, feature_neighborhood_values);
                    Array.Copy(feature_neighborhood_values,0,feature_vector,this.neighborhood_element_count * feature_image_index, this.neighborhood_element_count);
                }

                float best_likelyhood = Single.MinValue;

                for (int label_index = 0; label_index < this.label_count; label_index++)
			    {
                    float likelyhood = label_likelyhood_models[label_index].Compute(state_vector) * feature_likelyhood_models[label_index].Compute(feature_vector);
                    if (best_likelyhood < likelyhood)
                    {
                        best_likelyhood = likelyhood;
                        new_labeling.SetElementValue(label_index, label_index);
                    }

			    }
            }
        }
    }
}

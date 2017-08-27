using System;
using System.Collections.Generic;
using KozzionMachineLearning.Clustering.KMeans;
using KozzionMathematics.Function;
using KozzionCore.Tools;

namespace KozzionMachineLearning.Clustering
{
    public class CentroidFloat32 : ICentroidDistance<float>
    {
        public IList<float[]> Members { get; private set; }       

        private float[] location;

        private IFunctionDistance<float[], float> distance_function;

        public CentroidFloat32(IFunctionDistance<float[],float> distance_function, IList<float[]> instance_feature_list, float[] location)
        {
            this.distance_function = distance_function;
            this.Members = new List<float[]>(instance_feature_list);
            this.location = ToolsCollection.Copy(location);
        }

        public double ComputeDistance(float[] instance_features)
        {
            return distance_function.Compute(this.location, instance_features);
        }
    }
}
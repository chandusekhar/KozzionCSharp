using System;
using System.Collections.Generic;
using KozzionGraphics.ElementTree;
using KozzionGraphics.ElementTree.AlphaPartition;
using KozzionGraphics.ElementTree.AlphaPartition.Implementation;
using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionGraphics.Image.Topology;
using KozzionMathematics.Function;
using KozzionGraphics.ElementTree.MaxTree;
using KozzionMathematics.Algebra;

namespace KozzionMachineLearning.Model
{
    public class ClassifierPartitionTree3D
    {
        IAlphaPartitionTreeBuilder<float [], float> builder;
        ITopologyElementEdgeRaster<IRaster3DInteger> topology;
        IFunctionDissimilarity<float[], float> edge_model;
        IFunction<IElementTreeNode<float>, bool> node_detector;


        public ClassifierPartitionTree3D()
        {
            builder = new AlphaPartitionTreeBuilderMinTree<float[], float>(new AlgebraRealFloat32(), new MaxTreeBuilderSingleQueue<float>());
            topology = null;
            edge_model = null;
            node_detector = null;
        }


        public void Train(IImageRaster<IRaster3DInteger, float[]> feature_image, IImageRaster<IRaster3DInteger, bool> labeled_image) 
        {
        }

        public IImageRaster<IRaster3DInteger, float> Predict(IImageRaster<IRaster3DInteger, float []> feature_image) 
        {
            AlphaPartitionTree3D<float[], float> tree = new AlphaPartitionTree3D<float[], float>(
                builder,
                topology,
                edge_model,
                feature_image);

            
            IList<IElementTreeNode<float>> nodes = tree.GetSmallestSatisfiers(node_detector);

            IImageRaster<IRaster3DInteger, float> image = new ImageRaster3D<float>(feature_image.Raster, Single.MaxValue);
            for (int element_index = 0; element_index < nodes.Count; element_index++)
            {
                
            }
            return image;
        }
    }
}

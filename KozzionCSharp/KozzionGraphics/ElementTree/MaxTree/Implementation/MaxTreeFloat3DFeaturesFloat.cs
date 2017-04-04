using System;
using KozzionCore.DataStructure;
using KozzionGraphics.ElementTree.Feature;
using KozzionGraphics.ElementTree.MaxTree.Filter;
using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionGraphics.Image.Topology;

namespace KozzionGraphics.ElementTree.MaxTree.Implementation
{
    public class MaxTreeFloat3DFeaturesFloat :IMaxTree<float>
    {
     
        private IRaster3DInteger raster;
        private IMaxTree<float> inner_max_tree;
        private float[,] features;

        public MaxTreeFloat3DFeaturesFloat(IImageRaster<IRaster3DInteger, float> image, FeatureGeneratorElementNode3DFloat32 generator, IProgressReporter reporter)
        {
            this.raster = image.Raster;
            this.inner_max_tree = new MaxTreeBuilderSingleQueue<float>().BuildMaxTree(image.GetElementValues(false), new ComparerNatural<float>(), new TopologyElementRaster3D6Connectivity(image.Raster), image.Raster.ElementCount);
            if (generator != null)
            {
                this.features = new float[inner_max_tree.NodeCount, generator.FeatureCount];
                generator.GenerateFeaturesTree(raster, inner_max_tree.BottomLevelNode, features);
            }
            else
            {
                this.features = null;
            }
        }

        public MaxTreeFloat3DFeaturesFloat(IImageRaster<IRaster3DInteger, float> image, IProgressReporter reporter) 
            : this(image, null, reporter)
        {
    
        }

        public MaxTreeFloat3DFeaturesFloat(IImageRaster<IRaster3DInteger, float> image, FeatureGeneratorElementNode3DFloat32 generator)
            : this(image, generator, null)
        {

        }

       

        public int FullElementCount
        {
            get { return this.inner_max_tree.FullElementCount; }
        }

        public int RealElementCount
        {
            get { return this.inner_max_tree.RealElementCount; }
        }

        public int[] GetFullElementsIndexesOfElementLevelAndAbove(int element_index)
        {
            throw new NotImplementedException();
        }

        public int[] GetFullElementsIndexesOfElementLevelAndAbove(int element_index, float level)
        {
            throw new NotImplementedException();
        }

        public int[] GetRealElementsIndexesOfElementLevelAndAbove(int element_index, float level)
        {
            throw new NotImplementedException();
        }

        public int[] GetRealElementsIndexesOfElementLevelAndAbove(int element_index)
        {
            throw new NotImplementedException();
        }

        public float[] GetDisplayValues()
        {
            return inner_max_tree.GetDisplayValues();
        }

        public void FilterAbsorbing(KozzionMathematics.Function.IFunction<IMaxTreeNode<float>, bool> filter)
        {
            throw new NotImplementedException();
        }

        public void Filter(IMaxTreeFilter<float> filter)
        {
            inner_max_tree.Filter(filter);
        }

        public float[,] GetFeatures()
        {
            return features;
        }

        public IImageRaster3D<float> GetFilteredImage()
        {
            return new ImageRaster3D<float>(raster, GetDisplayValues(), false);
        }


        public int NodeCount
        {
            get { return inner_max_tree.NodeCount; }
        }

        public IMaxTreeNode<float> BottomLevelNode
        {
            get { return inner_max_tree.BottomLevelNode; }
        }


        public IMaxTreeNode<float> GetNode(int element_index)
        {
            throw new NotImplementedException();
        }
    }
}

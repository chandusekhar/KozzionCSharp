using System;
using KozzionCore.DataStructure;
using KozzionGraphics.ElementTree.Feature;
using KozzionGraphics.ElementTree.MaxTree.Filter;
using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionGraphics.Image.Topology;

namespace KozzionGraphics.ElementTree.MaxTree.Implementation
{
    public class MaxTreeFloat3DFeaturesDouble :IMaxTree<float>
    {
     
        private IRaster3DInteger raster;
        private IMaxTree<float> inner_max_tree;
        private double[,] features;

        public MaxTreeFloat3DFeaturesDouble(IImageRaster<IRaster3DInteger, float> image, FeatureGeneratorElementNode3DDouble generator, IProgressReporter reporter)
        {
            this.raster = image.Raster;
            this.inner_max_tree = new MaxTreeBuilderSingleQueue<float>().BuildMaxTree(image.GetElementValues(false), new ComparerNatural<float>(), new TopologyElementRaster3D6Connectivity(image.Raster), image.Raster.ElementCount);
            if (generator != null)
            {
                this.features = new double[inner_max_tree.NodeCount, generator.FeatureCount];
                generator.GenerateFeaturesTree(raster, inner_max_tree.BottomLevelNode, features);
            }
            else
            {
                this.features = null;
            }
        }

        public MaxTreeFloat3DFeaturesDouble(IImageRaster<IRaster3DInteger, float> image, IProgressReporter reporter) 
            : this(image, null, reporter)
        {
    
        }

        public MaxTreeFloat3DFeaturesDouble(IImageRaster<IRaster3DInteger, float> image, FeatureGeneratorElementNode3DDouble generator)
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

        public double[,] GetFeatures()
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
            return inner_max_tree.GetNode(element_index);
        }

        int IMaxTree<float>.FullElementCount
        {
            get { throw new NotImplementedException(); }
        }

        int IMaxTree<float>.RealElementCount
        {
            get { throw new NotImplementedException(); }
        }

        int IMaxTree<float>.NodeCount
        {
            get { throw new NotImplementedException(); }
        }

        IMaxTreeNode<float> IMaxTree<float>.BottomLevelNode
        {
            get { throw new NotImplementedException(); }
        }

        int[] IMaxTree<float>.GetFullElementsIndexesOfElementLevelAndAbove(int element_index)
        {
            throw new NotImplementedException();
        }

        int[] IMaxTree<float>.GetFullElementsIndexesOfElementLevelAndAbove(int element_index, float level)
        {
            throw new NotImplementedException();
        }

        int[] IMaxTree<float>.GetRealElementsIndexesOfElementLevelAndAbove(int element_index, float level)
        {
            throw new NotImplementedException();
        }

        int[] IMaxTree<float>.GetRealElementsIndexesOfElementLevelAndAbove(int element_index)
        {
            throw new NotImplementedException();
        }

        float[] IMaxTree<float>.GetDisplayValues()
        {
            throw new NotImplementedException();
        }

        void IMaxTree<float>.Filter(IMaxTreeFilter<float> filter)
        {
            throw new NotImplementedException();
        }

    }
}

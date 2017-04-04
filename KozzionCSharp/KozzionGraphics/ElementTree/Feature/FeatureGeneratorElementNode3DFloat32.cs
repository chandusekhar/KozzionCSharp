using System;
using System.Collections.Generic;
using KozzionGraphics.ElementTree.MaxTree;
using KozzionGraphics.Image.Raster;

namespace KozzionGraphics.ElementTree.Feature
{
    public class FeatureGeneratorElementNode3DFloat32
	{


        public int Offset { get; private set; }
		// TODO move geometric moments to other generator and have generators play nice

        public int IndexSX { get; private set; }

        public int IndexSY { get; private set; }

        public int IndexSZ { get; private set; }

        public int IndexSXX { get; private set;}

        public int IndexSYY { get; private set; }

        public int IndexSZZ { get; private set; }

        public int IndexSXY { get; private set; }

        public int IndexSXZ { get; private set; }

        public int IndexSYZ { get; private set; }

        public int IndexNonCompactness { get; private set; }

        public int IndexElongation { get; private set; }

        public int IndexFlatness { get; private set; }

        public int IndexSparceness { get; private set; }

        public int IndexSize { get; private set; }

		private float []         eigenvalues;
		private float []         covariance_matrix;
		private int []           element_coordinates;

 
		public FeatureGeneratorElementNode3DFloat32(
			int offset)
		{

            this.Offset = offset;
            this.IndexSX = Offset + 0;
            this.IndexSY = Offset + 1;
            this.IndexSZ = Offset + 2;
            this.IndexSXX = Offset + 3;
            this.IndexSYY = Offset + 4;
            this.IndexSZZ = Offset + 5;
            this.IndexSXY = Offset + 6;
            this.IndexSXZ = Offset + 7;
            this.IndexSYZ = Offset + 8;
            this.IndexNonCompactness = Offset + 9;
            this.IndexElongation = Offset + 10;
            this.IndexFlatness = Offset + 11;
            this.IndexSparceness = Offset + 12;
            this.IndexSize = Offset + 13;
            this.eigenvalues = new float[3];
            this.covariance_matrix = new float[9];
            this.element_coordinates = new int[3];
		}

        public FeatureGeneratorElementNode3DFloat32()
            : this(0)
        {
        }

		public int FeatureCount
		{
			get
			{
				return 14;
			}
		}
  
        public void GenerateFeaturesTree(IRaster3DInteger raster, IMaxTreeNode<float> bottom_level_node, float[,] features)
        {
            //Generate from the leafs downwards
            Queue<IMaxTreeNode<float>> queue = new Queue<IMaxTreeNode<float>>();
            Stack<IMaxTreeNode<float>> node_stack = new Stack<IMaxTreeNode<float>>();

            queue.Enqueue(bottom_level_node);
            while (queue.Count != 0)
            {
               IMaxTreeNode<float> node = queue.Dequeue();
               node_stack.Push(node);
               foreach (IMaxTreeNode<float>  child_node in node.GetNodeChildren())
               {
                   queue.Enqueue(child_node);
               }
            }

            while (node_stack.Count != 0)
            {
                IMaxTreeNode<float> node = node_stack.Pop();
                GenerateFeaturesNode(raster, node, features);
            }
        }
		/** Scale and rotation invariant 3D features
		 * As in disertation Fred Kawanuka page 79 & Wilkinson Roerding for scale invariant matrix
		 */
		private void GenerateFeaturesNode(
            IRaster3DInteger raster,
            IMaxTreeNode<float> node,            
			float [,] features)
		{
            int node_index = node.NodeIndex;
			// 1 get_size
			float size = node.CulmativeRealSize;

			// 2 compute geometric moments
			int [] elements = node.GetElementIndexArrayNodeReal();
			foreach (int element_index in elements)
			{
				raster.GetElementCoordinatesRBA(element_index, element_coordinates);
                features[node_index, IndexSX] += element_coordinates[0];
                features[node_index, IndexSY] += element_coordinates[1];
                features[node_index, IndexSZ] += element_coordinates[2];

                features[node_index, IndexSXX] += element_coordinates[0] * element_coordinates[0];
                features[node_index, IndexSYY] += element_coordinates[1] * element_coordinates[1];
                features[node_index, IndexSZZ] += element_coordinates[2] * element_coordinates[2];

                features[node_index, IndexSXY] += element_coordinates[0] * element_coordinates[1];
                features[node_index, IndexSXZ] += element_coordinates[0] * element_coordinates[2];
                features[node_index, IndexSYZ] += element_coordinates[1] * element_coordinates[2];
			}

            foreach (IMaxTreeNode<float> child in node.GetNodeChildren())
			{
				int child_node_index = child.NodeIndex;
                features[node_index, IndexSX] += features[child_node_index, IndexSX];
                features[node_index, IndexSY] += features[child_node_index, IndexSY];
                features[node_index, IndexSZ] += features[child_node_index, IndexSY];

                features[node_index, IndexSXX] += features[child_node_index, IndexSXX];
                features[node_index, IndexSYY] += features[child_node_index, IndexSYY];
                features[node_index, IndexSZZ] += features[child_node_index, IndexSZZ];

                features[node_index, IndexSXY] += features[child_node_index, IndexSXY];
                features[node_index, IndexSXZ] += features[child_node_index, IndexSXZ];
                features[node_index, IndexSYZ] += features[child_node_index, IndexSYZ];
			}

			// 2 compute covariance matrix (only the parts we will use note tha)
            float x_mean = features[node_index, IndexSX] / size;
            float y_mean = features[node_index, IndexSY] / size;
            float z_mean = features[node_index, IndexSZ] / size;
            covariance_matrix[0] = features[node_index, IndexSXX] - 2 * features[node_index, IndexSX] * x_mean + x_mean * x_mean * size;
            covariance_matrix[4] = features[node_index, IndexSYY] - 2 * features[node_index, IndexSY] * y_mean + y_mean * y_mean * size;
            covariance_matrix[8] = features[node_index, IndexSZZ] - 2 * features[node_index, IndexSZ] * z_mean + z_mean * z_mean * size;

            covariance_matrix[1] = features[node_index, IndexSXY] - features[node_index, IndexSX] * y_mean - features[node_index, IndexSY] * x_mean + x_mean * y_mean
				* size;
            covariance_matrix[2] = features[node_index, IndexSXZ] - features[node_index, IndexSX] * z_mean - features[node_index, IndexSZ] * x_mean + x_mean * z_mean
				* size;
            covariance_matrix[5] = features[node_index, IndexSYZ] - features[node_index, IndexSY] * z_mean - features[node_index, IndexSZ] * y_mean + y_mean * z_mean
				* size;
			;

			covariance_matrix[0] = (covariance_matrix[0] + size / 12.0f) / (float) Math.Pow(size, 5.0 / 3.0);
			covariance_matrix[4] = (covariance_matrix[4] + size / 12.0f) / (float) Math.Pow(size, 5.0 / 3.0);
			covariance_matrix[8] = (covariance_matrix[8] + size / 12.0f) / (float) Math.Pow(size, 5.0 / 3.0);

			covariance_matrix[1] = covariance_matrix[1] / (float) Math.Pow(size, 5.0 / 3.0);
			covariance_matrix[2] = covariance_matrix[2] / (float) Math.Pow(size, 5.0 / 3.0);
			covariance_matrix[5] = covariance_matrix[5] / (float) Math.Pow(size, 5.0 / 3.0);

			// CollectionTools.print(covariance_matrix);
			// 3 compute eigenvalues
			ToolsMathMatrix3Float32.EigenvaluesSymetricRBA(covariance_matrix, eigenvalues);

			// compute non-compactness
            features[node_index, IndexNonCompactness] = eigenvalues[0] + eigenvalues[1] + eigenvalues[2];

			// 4 sort so that e_abs[0] < e_abs[1] < e_abs[2]
			eigenvalues[0] = Math.Abs(eigenvalues[0]);
			eigenvalues[1] = Math.Abs(eigenvalues[1]);
			eigenvalues[2] = Math.Abs(eigenvalues[2]);
			Array.Sort(eigenvalues);

			// 5 compute d values
			float d0 = (float) Math.Sqrt((eigenvalues[0] * 20));
			float d1 = (float) Math.Sqrt((eigenvalues[1] * 20));
			float d2 = (float) Math.Sqrt((eigenvalues[2] * 20));

			// 6 Compute features:

			// compute elongation differ
            features[node_index, IndexElongation] = eigenvalues[2] / eigenvalues[1];
			// compute flasness
            features[node_index, IndexFlatness] = eigenvalues[1] / eigenvalues[0];
			// compute sparceness
            features[node_index, IndexSparceness] = d0 * d1 * d2;

            features[node_index, IndexSize] = size; 
		}







      
    }
}
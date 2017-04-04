using System;
using System.Collections.Generic;
using KozzionMachineLearning.Voting;
using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.k_d_tree;
using KozzionMathematics.Function;
using KozzionMachineLearning.Method.JointTable;
using KozzionMachineLearning.Model;
using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Reporting;
using System.IO;

namespace KozzionMachineLearning.Method.NearestNeighbor
{
    public class ModelNearestNeighborKDTree<DomainType, DistanceType, LabelType>  :
        AModelDiscrete<DomainType, LabelType>,
        IModelDiscreteIterative<DomainType, LabelType>
   
        where DomainType : IComparable<DomainType>
        where DistanceType : IComparable<DistanceType>
	{

        protected KDTree<DomainType, DistanceType, LabelType> kdtree;
        public IFunctionDistance<DomainType[], DistanceType> DistanceFunction { get; }
        private IVotingSystem<Tuple<DomainType[], DistanceType, LabelType>> voting_system;
        private int neighbor_count;

        public ModelNearestNeighborKDTree(
            string model_type,
            IDataContext data_contex,
            KDTree<DomainType, DistanceType, LabelType> kdtree,
            IFunctionDistance<DomainType[], DistanceType> distance_function,
            IVotingSystem<Tuple<DomainType[], DistanceType, LabelType>> voting_system,
            int neighbor_count)
        : base(data_contex, model_type)
        {
            this.kdtree = kdtree;
            this.DistanceFunction = distance_function;
            this.voting_system = voting_system;
            this.neighbor_count = neighbor_count;
        }

        public ModelNearestNeighborKDTree(
            IDataContext data_contex,
            KDTree<DomainType, DistanceType, LabelType> kdtree,
            IFunctionDistance<DomainType[], DistanceType> distance_function,
            IVotingSystem<Tuple<DomainType[], DistanceType, LabelType>> voting_system,
            int neighbor_count)
        : this("ModelNearestNeighborKDTree", data_contex, kdtree, distance_function, voting_system, neighbor_count)
        {
            this.kdtree = kdtree;
            this.DistanceFunction = distance_function;
            this.voting_system = voting_system;
            this.neighbor_count = neighbor_count;
        }

        public ModelNearestNeighborKDTree(
            IDataContext data_contex,
			IAlgebraReal<DomainType> algebra,
            IFunctionDistance<DomainType[], DistanceType> distance_function,
            IVotingSystem<Tuple<DomainType[], DistanceType, LabelType>> voting_system,
			int neighbor_count)
            : this(data_contex, new KDTree<DomainType, DistanceType, LabelType>(algebra, data_contex.FeatureCount), distance_function, voting_system, neighbor_count)
		{     
		}
        public ModelNearestNeighborKDTree(
            string model_type,
            IDataContext data_contex,
            IAlgebraReal<DomainType> algebra,
            IFunctionDistance<DomainType[], DistanceType> distance_function,
            IVotingSystem<Tuple<DomainType[], DistanceType, LabelType>> voting_system,
            int neighbor_count)
            : this(model_type, data_contex, new KDTree<DomainType, DistanceType, LabelType>(algebra, data_contex.FeatureCount), distance_function, voting_system, neighbor_count)
        {
        }

        public ModelNearestNeighborKDTree(
            IDataContext data_contex,
            IAlgebraReal<DomainType> algebra,
            IFunctionDistance<DomainType [], DistanceType> distance_function)
            : this(data_contex, algebra, distance_function, new SingleVoteSystem<Tuple<DomainType[], DistanceType, LabelType>>(), 1)
        {

        }

        public ModelNearestNeighborKDTree(
            string model_type,
             IDataContext data_contex,
             IAlgebraReal<DomainType> algebra,
             IFunctionDistance<DomainType[], DistanceType> distance_function)
             : this(model_type, data_contex, algebra, distance_function, new SingleVoteSystem<Tuple<DomainType[], DistanceType, LabelType>>(), 1)
        {

        }

        public void Write(BinaryWriter write)
        {
            throw new NotImplementedException();
        }

        public override LabelType GetLabel(DomainType[] instance_features)
        { 
            NearestNeighbourEnumerator<DomainType, DistanceType, LabelType> enumerator = kdtree.NearestNeighbors(instance_features, DistanceFunction, default(DistanceType), default(DistanceType), false, neighbor_count);
            IList<Tuple<DomainType[], DistanceType, LabelType>> neighbors = new List<Tuple<DomainType[], DistanceType, LabelType>>();
            do
            {
                neighbors.Add(enumerator.Current);
            }
            while (enumerator.MoveNext());
			return voting_system.elect(neighbors).Item3;
		}

        public IModelDiscreteIterative<DomainType, LabelType> GenerateModelDiscrete(IDataSet<DomainType, LabelType> training_set)
        {
            ModelNearestNeighborKDTree<DomainType, DistanceType, LabelType> model = new ModelNearestNeighborKDTree<DomainType, DistanceType, LabelType>(
                this.DataContext, 
                this.kdtree.Copy(), 
                this.DistanceFunction, 
                this.voting_system, 
                this.neighbor_count);
            IList<Tuple<DomainType[], LabelType>> training_instances = new List<Tuple<DomainType[], LabelType>>();
            for (int instance_index = 0; instance_index < training_set.InstanceCount; instance_index++)
            {
                training_instances.Add(new Tuple<DomainType[], LabelType>(training_set.GetInstanceFeatureData(instance_index), training_set.GetInstanceLabelData(instance_index)[0]));
            }
            model.Add(training_instances);
            return model;
        }

        public IModelLabelIterative<DomainType, LabelType> GenerateModelLabel(IDataSet<DomainType, LabelType> training_set)
        {
            return GenerateModelDiscrete(training_set);
        }

        public void Add(IList<Tuple<DomainType[], LabelType>> training_instances)
        {

            foreach (Tuple<DomainType[], LabelType> example in training_instances)
            {
                if (example.Item1.Length == kdtree.DimensionCount)
                {
                    kdtree.AddPoint(example.Item1, example.Item2);
                }
                else
                {
                    throw new Exception("Data dimensions do not agree");
                }
            }
        }

  





        /*
       public ClassType classify(
           float [] input,
           bool prevent_self_retrieval)
       {
           Entry<ClassType> [] neighbors = d_nearest_neighbors_searcher.get(d_kdtree, input, d_neighbor_count,
               prevent_self_retrieval);
           return d_voting_system.vote_single(convert_to_list(neighbors));
       }
       
       private List<Tuple2<Float, ClassType>> convert_to_list(
           Entry<ClassType> [] neighbors)
       {
           List<Tuple2<Float, ClassType>> voting_data = new Vector<Tuple2<Float, ClassType>>();
           for (Entry<ClassType> entry : neighbors)
           {
               voting_data.add(new Tuple2<Float, ClassType>(entry.get_distance(), entry.get_neighbor().getValue()));
           }
           return voting_data;
       }
		
       public double cross_validate()
       {
           double correct = 0;
           double incorrect = 0;
           List<float []> data_points = new List<float []>(d_kdtree.keySet());
           for (float [] data_point : data_points)
           {
               ClassType label = d_kdtree.get(data_point);
               ClassType classification = classify(data_point, true);
               if (label.equals(classification))
               {
                   correct++;
               }
               else
               {
                   incorrect++;
               }
           }

           return correct / (correct + incorrect);
       }
       */
    }
}
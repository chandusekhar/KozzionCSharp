using KozzionCore.Tools;
using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Model;
using KozzionMachineLearning.Voting;
using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Method.NearestNeighbor
{
    public class ModelNearestNeighborList<DomainType, DistanceType, LabelType> :
        AModelDiscrete<DomainType, LabelType>,
        IModelDiscreteIterative<DomainType, LabelType>

        where DomainType : IComparable<DomainType>
        where DistanceType : IComparable<DistanceType>
    {

        protected IList<Tuple<DomainType[], LabelType>> list;
        protected IFunctionDistance<DomainType[], DistanceType> distance_function;
        protected IVotingSystem<Tuple<DomainType[], DistanceType, LabelType>> voting_system;
        protected int neighbor_count;

        public ModelNearestNeighborList(
            IDataContext data_contex,
            IList<Tuple<DomainType[], LabelType>> list,
            IFunctionDistance<DomainType[], DistanceType> distance_function,
            IVotingSystem<Tuple<DomainType[], DistanceType, LabelType>> voting_system,
            int neighbor_count)
            : base(data_contex, "ModelNearestNeighborList")
        {
            this.list = list;
            this.distance_function = distance_function;
            this.voting_system = voting_system;
            this.neighbor_count = neighbor_count;
        }

        public ModelNearestNeighborList(
            IDataContext data_contex,
            IFunctionDistance<DomainType[], DistanceType> distance_function,
            IVotingSystem<Tuple<DomainType[], DistanceType, LabelType>> voting_system,
            int neighbor_count)
                : this(data_contex, new List<Tuple<DomainType [], LabelType>>(), distance_function, voting_system, neighbor_count)
        {
        }


        public ModelNearestNeighborList(
            IDataContext data_contex,
            IFunctionDistance<DomainType[], DistanceType> distance_function)
                : this(data_contex, distance_function, new SingleVoteSystem<Tuple<DomainType[], DistanceType, LabelType>>(), 1)
        {

        }

        public void Write(BinaryWriter write)
        {
            throw new NotImplementedException();
        }

        public override LabelType GetLabel(DomainType[] instance_features)
        {
            IList<Tuple<DomainType[], DistanceType, LabelType>> neighbors = new List<Tuple<DomainType[], DistanceType, LabelType>>();
            foreach (Tuple<DomainType[], LabelType> neighbor in this.list)
            {
                neighbors.Add(new Tuple<DomainType[], DistanceType, LabelType>(neighbor.Item1, this.distance_function.Compute(neighbor.Item1, instance_features), neighbor.Item2));
            }
            List<Tuple<DomainType[], DistanceType, LabelType>> ordered_neighbors = new List<Tuple<DomainType[], DistanceType, LabelType>>(neighbors.OrderBy(tuple => tuple.Item2));
            Tuple<DomainType[], DistanceType, LabelType>[] selected_neighbors = ToolsCollection.Crop(ordered_neighbors, this.neighbor_count);
            return voting_system.elect(selected_neighbors).Item3;
        }

        public IModelDiscreteIterative<DomainType, LabelType> GenerateModelDiscrete(IDataSet<DomainType, LabelType> training_set)
        {
            ModelNearestNeighborList<DomainType, DistanceType, LabelType> model = new ModelNearestNeighborList<DomainType, DistanceType, LabelType>(
                this.DataContext,
                new List<Tuple<DomainType[], LabelType>>(this.list),
                this.distance_function,
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
                this.list.Add(example);
            }
        }

 
    }
}

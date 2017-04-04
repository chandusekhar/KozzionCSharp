using KozzionMachineLearning.DataSet;
using System;
using System.Collections.Generic;
using KozzionMachineLearning.Model;
using KozzionMachineLearning.Method.JointTable;

namespace KozzionMachineLearning.FeatureSelection
{
    public class FeatureSelectorGreedy<DomainType, LabelType> : 
        IFeatureSetSelectorDiscrete<DomainType, LabelType>
    {
        IFeatureSetEvaluatorDiscrete<DomainType, LabelType> feature_set_evaluator;
        public FeatureSelectorGreedy(IFeatureSetEvaluatorDiscrete<DomainType, LabelType> feature_set_evaluator)
        {
            this.feature_set_evaluator = feature_set_evaluator;
        }
    
        public Tuple<IModelDiscrete<DomainType, LabelType>, IList<int>> SelectFeatureSet(ITemplateModelDiscrete<DomainType, LabelType> template, IDataSet<DomainType, LabelType> data_set)
        {
			ISet<ISet<int>> black_list = new HashSet<ISet<int>>();
			ISet<int> full_set = new HashSet<int>();
            for (int feature_index = 0; feature_index < data_set.FeatureCount; feature_index++)
			{
				full_set.Add(feature_index);
			}

			ISet<int> current_set = new HashSet<int>();
			double best_score = feature_set_evaluator.Evaluate(template, data_set, current_set);
			bool improvement = true;
			while (improvement)
			{
				List<ISet<int>> options = new List<ISet<int>>();
				options.AddRange(RemoveFeature(current_set, black_list));
				options.AddRange(AddFeature(current_set, black_list, full_set));
				improvement = false;
				foreach (ISet<int> option in options)
				{
                    double score = feature_set_evaluator.Evaluate(template, data_set, option);
					//System.out.print("scorein " + score);
					//CollectionTools.print(option);
					if (best_score.CompareTo(score) == -1)
					{
						//System.out.print("New best scorein " + score);
						//CollectionTools.print(option);
						best_score = score;
						current_set = option;
						improvement = true;
					}
				}
			}
            List<int> current_list = new List<int>(current_set);
            current_list.Sort();

            IModelDiscrete<DomainType, LabelType> model = template.GenerateModelDiscrete(data_set.SelectFeatures(current_list));
            return new Tuple<IModelDiscrete<DomainType, LabelType>, IList<int>>(model, current_list);
        }

		private IList<ISet<int>> RemoveFeature(ISet<int> current_set, ISet<ISet<int>> black_list)
		{
			IList<ISet<int>> options = new List<ISet<int>>();

			foreach (int feature_index in current_set)
			{
				ISet<int> new_set = new HashSet<int>(current_set);
				new_set.Remove(feature_index);
				if (!black_list.Contains(new_set))
				{
					options.Add(new_set);
					black_list.Add(new_set);
				}
			}

			return options;
		}

		private IList<ISet<int>> AddFeature(ISet<int> current_set, ISet<ISet<int>> black_list, ISet<int> full_set)
		{
			IList<ISet<int>> options = new List<ISet<int>>();

			foreach (int feature_index in full_set)
			{
				if (!current_set.Contains(feature_index))
				{
					ISet<int> new_set = new HashSet<int>(current_set);
					new_set.Add(feature_index);

					if (!black_list.Contains(new_set))
					{
						options.Add(new_set);
						black_list.Add(new_set);
					}
				}
			}        
			return options;
		}

      
    }
}
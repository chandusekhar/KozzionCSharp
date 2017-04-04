using KozzionMachineLearning.DataSet;
using System;
using System.Collections.Generic;

namespace KozzionMachineLearning.FeatureSelection
{
    public class DetectorEvaluatorROCAUC  : IEvaluatorNominalDataDetector
    {
        //PairComparator<float, bool> d_comparator;
        //float                          d_hitrate_increment;
        //float                          d_false_positive_increment;

        public DetectorEvaluatorROCAUC()
        {
            //d_comparator = new PairComparator<float, bool>(PairComparatorMode.First);
        }

  


        //public float evaluate(INominalDataDetector detector, IDataSetNominalLabeled<bool> evaluation_data_set)
        //{
        //    List<Tuple<float, bool>> estimated_chances = new List<Tuple<float, bool>>();
        //    foreach (INominalDataInstance entry : evaluation_data_set.get_positive_example_list())
        //    {
        //        estimated_chances.Add(new Tuple<float, bool>(detector.compute_positive_score(entry), true));
        //    }
        //    for (INominalDataInstance entry : evaluation_data_set.get_negative_example_list())
        //    {
        //        estimated_chances.add(new Tuple<float, bool>(detector.compute_positive_score(entry), false));
        //    }
        //    Collections.sort(estimated_chances, d_comparator);
        //    float[] true_positive_rate = new float[estimated_chances.size() + 2];
        //    float[] false_positive_rate = new float[estimated_chances.size() + 2];
        //    true_positive_rate[0] = 0;
        //    false_positive_rate[0] = 0;
        //    true_positive_rate[estimated_chances.size() + 1] = 1;
        //    false_positive_rate[estimated_chances.size() + 1] = 1;
        //    int true_positives = 0;
        //    int false_positives = 0;
        //    int true_negatives = evaluation_data_set.get_negative_example_list().size(); ;
        //    int false_negatives = evaluation_data_set.get_positive_example_list().size();
        //    for (int index = 1; index < false_positive_rate.length - 1; index++)
        //    {
        //        Tuple2<Float, Boolean> pair = estimated_chances.get(index - 1);
        //        //System.out.println(pair.get_object1());
        //        if (pair.get_object2())
        //        {
        //            //Increase hit rate
        //            true_positives++;
        //            false_negatives--;
        //        }
        //        else
        //        {
        //            //Increase false positive rate
        //            false_positives++;
        //            true_negatives--;
        //        }
        //        FMeasure measure = new FMeasure(true_positives, false_positives, true_negatives, false_negatives);
        //        true_positive_rate[index] = (float)measure.get_true_positive_rate();
        //        false_positive_rate[index] = (float)measure.get_false_positive_rate();
        //    }
        //    for (int index = 0; index < false_positive_rate.length; index++)
        //    {
        //        //System.out.println(true_positive_rate[index]);
        //        //System.out.println(false_positive_rate[index]);
        //    }
        //    return TrapeziodIntegralEvaluatorFloat.evaluate(new Tuple2<float[], float[]>(true_positive_rate, false_positive_rate));
        //}
    }
}
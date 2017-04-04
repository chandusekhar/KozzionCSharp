using KozzionCore.Tools;
using KozzionMathematics.Function;
using KozzionMathematics.Function.Implementation;
using KozzionMathematics.Statistics.Test.OneSampleROC;
using KozzionMathematics.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Shady
{
    public class SelectionOptimization
    {

        public static IList<int> Minimize(bool[] labels, IFunction<ISet<int>, double> to_minimize)
        {
            return Minimize(labels, to_minimize, new FunctionContstant<ISet<int>, bool>(true));
        }

        public static IList<int> Minimize(bool[] labels,  IFunction<ISet<int>, double> to_minimize, IFunction<ISet<int>, bool> constraints) 
        {
            bool has_new = true;
            ISet<int> all = new HashSet<int>(ToolsMathSeries.RangeInt32(0, labels.Length));
            ISet<int> current = new HashSet<int>(ToolsMathSeries.RangeInt32(0, labels.Length));
            while (has_new)
            {
                List<ISet<int>> options = new List<ISet<int>>();
                GenerateOptionsRemove(all, current, options, constraints);
                GenerateOptionsAdd(all, current, options, constraints);
                Tuple<ISet<int>, double, bool> best = PickMinimal(current, options, to_minimize);
                current = best.Item1;
                double current_p = best.Item2;
                has_new = best.Item3;


                //Comment
                Console.WriteLine(current_p);
            }
            List<int> current_list = new List<int>(current);
            current_list.Sort();
            return current_list;
        } 


        //public static IList<int> OptimizeAUCDifference(bool[] labels, double[] values, double[][] values_other, IFunction<ISet<int>,bool> constraints)
        //{
        //    bool has_new = true;
        //    ISet<int> all = new HashSet<int>(ToolsMathSeries.RangeInt32(0, labels.Length));
        //    ISet<int> current = new HashSet<int>(ToolsMathSeries.RangeInt32(0, labels.Length));
        //    while (has_new)
        //    {
        //        List<ISet<int>> options = new List<ISet<int>>();
        //        GenerateOptionsRemove(all, current, options, constraints);
        //        GenerateOptionsAdd(all, current, options, constraints);
        //        Tuple<ISet<int>, bool> best = PickBest(current, options, labels, values, values_other);
        //        current = best.Item1;
        //        has_new = best.Item2;
        //    }
        //    List<int> current_list = new List<int>(current);
        //    current_list.Sort();
        //    return current_list;
        //}

        private static void GenerateOptionsRemove(ISet<int> all, ISet<int> current, List<ISet<int>> options, IFunction<ISet<int>, bool> constraints)
        {
            foreach (int to_remove in all)
            {
                if (current.Contains(to_remove))
                {
                    ISet<int> option = new HashSet<int>(current);
                    option.Remove(to_remove);
                    if (constraints.Compute(option))
                    {
                        options.Add(option);
                    }
                }
            }
        }

        private static void GenerateOptionsAdd(ISet<int> all, ISet<int> current, List<ISet<int>> options, IFunction<ISet<int>, bool> constraints)
        {
            foreach (int to_add in all)
            {
                if (!current.Contains(to_add))
                {
                    ISet<int> option = new HashSet<int>(current);
                    option.Add(to_add);
                    if (constraints.Compute(option))
                    {
                        options.Add(option);
                    }
                }
            }
        }

        private static Tuple<ISet<int>, double, bool> PickMinimal(ISet<int> current, List<ISet<int>> options, IFunction<ISet<int>, double> to_minimize)
        {
            ISet<int> minimal_option = current;
            double minimial_value = to_minimize.Compute(current);
            bool has_new = false;
            foreach (ISet<int> option in options)
            {
                double new_value = to_minimize.Compute(option);
                if (new_value < minimial_value)
                {
                    minimal_option = option;
                    minimial_value = new_value;
                    has_new = true;
                }
            }                
            return new Tuple<ISet<int>, double, bool>(minimal_option, minimial_value, has_new);
        }



        private static double Evaluate(ISet<int> current, bool[] labels, double[] values, double[][] values_other)
        {
            // Here the actual testing happend we want the lowest p value between the values set and its competitors
            bool[] selected_labels = labels.Select(current);
            double auc = ToolsMathStatistics.ComputeROCAUCTrapeziod(selected_labels, values.Select(current));

            double best_other_auc = ToolsMathStatistics.ComputeROCAUCTrapeziod(selected_labels, values_other[0].Select(current));
            for (int index = 1; index < values_other.Length; index++)
            {
                //TODO Here we actually want a statistical test
                double other_auc = ToolsMathStatistics.ComputeROCAUCTrapeziod(selected_labels, values_other[index].Select(current));
                if (best_other_auc < other_auc)
                {
                    best_other_auc = other_auc;
                }

        
            }
            return auc - best_other_auc;     
        }

        private static double Evaluate(ISet<int> current, bool[] labels, double[] values)
        {
            // Here the actual testing happend we want the lowest p value between the values set and its competitors
            return 1 - TestROCMannWhitneyWilcoxon.TestStatic(labels.Select(current), values.Select(current));
        }
    }
}

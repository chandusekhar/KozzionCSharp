﻿using KozzionMachineLearning.Clustering.KMeans;
using KozzionMachineLearning.DataSet;
using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering
{ 
    public class TemplateCentroidCalculatorNominal : ITemplateCentroidCalculator<int, ICentroidDistance<int>>
    {
        public IFunction<IList<int[]>, ICentroidDistance<int>> Generate(IDataContext data_context)
        {
            return new CentroidCalculatorNominal(data_context);
        }
    }
}

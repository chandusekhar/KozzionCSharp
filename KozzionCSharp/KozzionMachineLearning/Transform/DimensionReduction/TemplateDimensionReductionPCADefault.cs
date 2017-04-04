
using KozzionMachineLearning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMachineLearning.DataSet;
using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Function;
using MathNet.Numerics.LinearAlgebra;

namespace KozzionMachineLearning.Transform
{
    public class TemplateDimensionReductionPCADefault : TemplateDimensionReductionPCA<Matrix<double>, IDataSet<double>>
    {
        public TemplateDimensionReductionPCADefault(int dimension_count)
            : base(new AlgebraLinearReal64MathNet(), dimension_count)
        {
        }

    }
}

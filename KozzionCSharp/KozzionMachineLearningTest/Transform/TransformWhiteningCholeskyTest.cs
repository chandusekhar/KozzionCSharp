using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Transform;
using KozzionMathematics.Tools;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearningTest.Transform
{

    [TestClass]
    public class TransformWhiteningCholeskyTest
    {

        //A =
        //0.6837    0.1175
        //0.1321    0.6407
        //0.7227    0.3288
        //0.1104    0.6538
        [TestMethod]
        public void TestTemplateTransformWhiteningCholesky()
        {
            //Set up
            var array = new double[,] {
                { 0.6837,    0.1175 },
                { 0.1321,    0.6407 },
                { 0.7227,    0.3288},
                { 0.1104,    0.6538}};
            DataSetDefault dataSet = new DataSetDefault(array);
            Matrix<double> matrix_before = dataSet.GetFeatureDataAsMatrix();
            TemplateTransformWhiteningCholesky template = new TemplateTransformWhiteningCholesky();

            //Act
            var matrix_before_covariance = ToolsMathLinear.CovarianceMatrix(matrix_before);
            var tranform = template.Generate(dataSet);
            var after = tranform.TransformForward(dataSet);
            var matrix_after = after.GetFeatureDataAsMatrix();
            var matrix_after_covariance = ToolsMathLinear.CovarianceMatrix(matrix_after);
            var reverse = tranform.TransformBackward(after);

            //Test
            Assert.AreEqual(matrix_after_covariance[0, 0], 1, 0.0001);
            Assert.AreEqual(matrix_after_covariance[0, 1], 0, 0.0001);
            Assert.AreEqual(matrix_after_covariance[1, 0], 0, 0.0001);
            Assert.AreEqual(matrix_after_covariance[1, 1], 1, 0.0001);

            Assert.AreEqual(reverse.InstanceList[0].FeatureData[0], 0.6837, 0.0001);
            Assert.AreEqual(reverse.InstanceList[0].FeatureData[1], 0.1175, 0.0001);
            Assert.AreEqual(reverse.InstanceList[1].FeatureData[0], 0.1321, 0.0001);
            Assert.AreEqual(reverse.InstanceList[1].FeatureData[1], 0.6407, 0.0001);
            Assert.AreEqual(reverse.InstanceList[2].FeatureData[0], 0.7227, 0.0001);
            Assert.AreEqual(reverse.InstanceList[2].FeatureData[1], 0.3288, 0.0001);
            Assert.AreEqual(reverse.InstanceList[3].FeatureData[0], 0.1104, 0.0001);
            Assert.AreEqual(reverse.InstanceList[3].FeatureData[1], 0.6538, 0.0001);
        }
    }
}
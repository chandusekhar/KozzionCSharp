using KozzionMachineLearning.Model;
using KozzionMachineLearning.Method.JointTable;
using libsvm;
using System;
using System.Linq;
using KozzionMachineLearning.Reporting;
using System.Collections.Generic;
using KozzionCore.Tools;
using KozzionMathematics.Tools;
using KozzionMachineLearning.DataSet;

namespace KozzionMachineLearning.Method.SupportVectorMachine
{
    /// <summary>
    /// This class wraps the Lib-SVM library below is a discription of the most important paramters
    /// </summary>
    //C_SVC      C-Support Vector Classification.         
    // n-class classification (2 or more classes), allows imperfect separation of classes with penalty multiplier C for outliers.
    //NU_SVC     nu-Support Vector Classification.       
    // n-class classification with possible imperfect separation.Parameter nu (in the range 0..1, the larger the value, the smoother the decision boundary) is used instead of C.
    //ONE_CLASS  Distribution Estimation (One-class SVM). 
    // All the training data are from the same class, SVM builds a boundary that separates the class from the rest of the feature space.
    //EPS_SVR    epsilon-Support Vector Regression.
    // The distance between feature vectors from the training set and the fitting hyper-plane must be less than p. For outliers the penalty multiplier C is used.
    //NU_SVR     nu-Support Vector Regression. 
    //nu is used instead of p.

    public class TemplateModelLibSVMCSVC : 
        ATemplateModelLikelihood<double, int>
    {
        public double C { get; private set; }
        public double Gamma { get; private set; }
        public int CacheSize { get; private set; }

        public TemplateModelLibSVMCSVC(double c, double gamma, int cache_size)
        {
            this.C = c;
            this.Gamma = gamma;
            this.CacheSize = cache_size;
        }

        public TemplateModelLibSVMCSVC(double c, double gamma)
            : this(c, gamma, 100)
        {
  
        }
        public TemplateModelLibSVMCSVC()
            : this(1,1)
        {

        }

        public override IModelLikelihood<double, int, double> GenerateModelLikelihood(IDataSet<double, int> training_set)
        {
            svm_problem prob = new svm_problem();
            prob.l = training_set.InstanceCount;
            prob.x = CreateNodeArray(ToolsCollection.ConvertToArray2D( training_set.FeatureData));
            prob.y = ToolsCollection.ConvertToDoubleArray(ToolsCollection.ConvertToArray2D(training_set.LabelData).Select1DIndex1(0));

            //Train model---------------------------------------------------------------------
            return new ModelLibSVMCSVC(training_set.DataContext, new C_SVC(prob, KernelHelper.RadialBasisFunctionKernel(this.Gamma), this.C, this.CacheSize, true));
        }


        public static double[] TestSVM(double [] test_case)
        {
            
            double[,] matrix = new double[5, 2];
            matrix[0, 0] = 1;
            matrix[0, 1] = 1;

            matrix[1, 0] = 1;
            matrix[1, 1] = 0;

            matrix[2, 0] = 0;
            matrix[2, 1] = 1;

            matrix[3, 0] = 0;
            matrix[3, 1] = 0;

            matrix[4, 0] = 10;
            matrix[4, 1] = 10;
            int[] labels = new int[] { 0, 1, 1, 0, 2};
            IDataSet<double, int> training_set = null;// new DataSet<double, int>(ToolsCollection.ConvertToArrayArray(matrix), DataLevel.NOMINAL, labels);



            IModelLikelihood<double, int, double> model = new TemplateModelLibSVMCSVC().GenerateModelLikelihood(training_set);
    

            //double C = 200;
            //double gamma = 0.8;

            return model.GetLikelihoods(test_case);
        }

        public static svm_node[][] CreateNodeArray(double [,] values)
        {
            svm_node[][] svm_nodes = new svm_node[values.GetLength(0)][];
            for (int row_index = 0; row_index < values.GetLength(0); row_index++)
            {
                svm_nodes[row_index] = CreateNodeArray(values.Select1DIndex0(row_index)); ;
            }  
            return svm_nodes;
        }


        public static svm_node [] CreateNodeArray(IReadOnlyList<double>  values)
        {
            svm_node[] svm_node = new svm_node[values.Count + 1];
            for (int index = 0; index < values.Count; index++)
            {
                svm_node[index] = new svm_node();
                svm_node[index].index = index;
                svm_node[index].value = values[index];
            }
            svm_node[values.Count] = new svm_node();
            svm_node[values.Count].index = -1;      //Each row of properties should be terminated with a -1 according to the readme
            return svm_node;
        }

        //svm_parameter param = new svm_parameter(); // for posible suppresion of print
        //param.svm_type = (int)SvmType.C_SVC;
        //param.kernel_type =  (int)KernelType.RBF;
        //param.degree = 3;
        //param.gamma = 0.5;
        //param.coef0 = 0;
        //param.nu = 0.5;
        //param.cache_size = 100;
        //param.C = 1;
        //param.eps = 1e-3;
        //param.p = 0.1;
        //param.shrinking = 1;
        //param.probability = 0;
        //param.nr_weight = 0;
        //param.weight_label = null;
        //param.weight = null;             
    }
}
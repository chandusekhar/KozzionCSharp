
using System;
using KozzionCore.Tools;
using KozzionMathematics.Function;

namespace KozzionMathematics.Numeric.Minimizer
{
    public class Simplex
    {
        public int LargestVertexIndex { get; private set; }
        public double LargestVertexValue { get; private set; }

        public int SmallestVertexIndex { get; private set; }
        public double SmallestVertexValue { get; private set; }

        public int ParameterCount { get; private set; }
        public int VertexCount { get; private set; }

        public double[][] vertexes;
        public double[] vertex_values;

        public double[] SmallestVertex { get { return vertexes[SmallestVertexIndex]; } }
        public double[] LargestVertex { get { return vertexes[LargestVertexIndex]; } }

        // static vertex constructor
        public Simplex(double[] initial_parameters, IFunction<double[], double> minimization_function, IFunction<double[], bool> validation_function)
        {
            ParameterCount = initial_parameters.Length;
            VertexCount = 1;
            this.vertexes = ToolsCollection.CreateArrayArray<double>(VertexCount, ParameterCount);
            this.vertex_values = new double[1];
            for (int parameter_index = 0; parameter_index < ParameterCount; parameter_index++)
            {
                this.vertexes[0][parameter_index] = initial_parameters[parameter_index];  
            }
            if (validation_function.Compute(initial_parameters))
            {
                SetVertexValue(0, minimization_function.Compute(initial_parameters));
            }
            LargestVertexIndex = 0;
        }


        public Simplex(double[] initial_parameters, double[] initial_step,  IFunction<double[], double> minimization_function,  IFunction<double[], bool> validation_function)
        {
            if (initial_parameters.Length!= initial_step.Length )
            {
                throw new Exception("unequal lenght");
            }
            ParameterCount = initial_parameters.Length;
            VertexCount = 1;
            for (int paramter_index = 0; paramter_index < ParameterCount; paramter_index++)
            {
                if (initial_step[paramter_index] != 0)
                {
                    VertexCount++;
                }                    
            }

            if (VertexCount < 3)
            {
                throw new Exception("Simplex to small");
            }

            this.vertexes = ToolsCollection.CreateArrayArray<double>(VertexCount, ParameterCount);
            this.vertex_values = new double[VertexCount];
        
            //initialize the vertexes

            //TODO check for fixed paramters
            int vertex_index = 0;
            for (int step_index = 0; step_index < ParameterCount; step_index++)
            {
                if (initial_step[step_index] != 0)
                {
                    for (int parameter_index = 0; parameter_index < ParameterCount; parameter_index++)
                    {
                        if (parameter_index == step_index)
                        {
                            this.vertexes[vertex_index][parameter_index] = initial_parameters[parameter_index] + initial_step[step_index];
                        }
                        else
                        {
                            this.vertexes[vertex_index][parameter_index] = initial_parameters[parameter_index];
                        }
                    }
                    vertex_values[vertex_index] = minimization_function.Compute(this.vertexes[vertex_index]);
                    vertex_index++;
                }
            }

            // The last vertex get all steps negative
            for (int parameter_index = 0; parameter_index < ParameterCount; parameter_index++)
            {
                this.vertexes[vertex_index][parameter_index] += initial_parameters[parameter_index] - initial_step[parameter_index];
            }
            SetVertexValue(vertex_index, minimization_function.Compute(this.vertexes[vertex_index]));
        }


        public void SetVertexLargest(double[] vertex_parameters, double vertex_value)
        {
            SetVertex(LargestVertexIndex, vertex_parameters, vertex_value);
        }

        public void SetVertex(int vertex_index, double[] vertex_parameters, double vertex_value)
        {
            ToolsCollection.CopyRBA(vertex_parameters, vertexes[vertex_index]);
            SetVertexValue(vertex_index, vertex_value);
        }

        public void SetVertexValue(int vertex_value_index, double vertex_value)
        {
            vertex_values[vertex_value_index] = vertex_value;
            LargestVertexValue = double.MinValue;
            SmallestVertexValue = double.MaxValue;

            for (int vertex_index = 0; vertex_index < VertexCount; vertex_index++)
            {
                if (LargestVertexValue < vertex_values[vertex_index])
                {
                    LargestVertexIndex = vertex_index;
                    LargestVertexValue = vertex_values[vertex_index];
                }
                if (vertex_values[vertex_index] < SmallestVertexValue)
                {
                    SmallestVertexIndex = vertex_index;
                    SmallestVertexValue = vertex_values[vertex_index];
                }
            }
        }

        public void ComputeCentroidVertex(double [] centroid_vertex)
        {
            //exclude biggest vertex!
            for (int parameter_index = 0; parameter_index < ParameterCount; parameter_index++)
            {
                centroid_vertex[parameter_index] = 0;
                for (int vertex_index = 0; vertex_index < VertexCount; vertex_index++)
                {
                    if (vertex_index != LargestVertexIndex)
                    {
                        centroid_vertex[parameter_index] += vertexes[vertex_index][parameter_index];
                    }
                }
                centroid_vertex[parameter_index] /= (VertexCount - 1.0);
            }
        }

    }
}

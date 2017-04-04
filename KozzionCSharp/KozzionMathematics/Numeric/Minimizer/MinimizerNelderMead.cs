using KozzionMathematics.Function;
using KozzionMathematics.Function.Implementation;
using KozzionCore.Tools;
using System;

namespace KozzionMathematics.Numeric.Minimizer
{
    public class MinimizerNelderMead : AMinimizer<double[], Simplex>
    {
        public double reflection_factor { get; }
        public double expansion_factor { get; }
        public double contraction_factor { get; }
        public double shrink_factor { get; }
        public bool TrackResuls { get; }

        public MinimizerNelderMead(double reflection_factor, double expansion_factor, double contraction_factor, double shrink_factor, bool track_results = false)
        {
            this.reflection_factor = reflection_factor;
            this.expansion_factor = expansion_factor;
            this.contraction_factor = contraction_factor;
            this.shrink_factor = shrink_factor;
            this.TrackResuls = track_results;
        }

        public MinimizerNelderMead(bool track_results = false)
         :   this(1.0, 2.0, 0.5, 0.5, track_results)
        { 
        }


  

        public override MinimizerResult Minimize(
                IFunction<double[], double> minimization_function,
                IFunction<double[], bool> validation_function,
                IMinimizerHaltingCriterion<Simplex> halting_criterion,
                double[] parameters_initial,
                double[] initial_vextex_size)
        {
  
            Simplex simplex = new Simplex(parameters_initial, initial_vextex_size, minimization_function, validation_function); /* Holds vertices of simplex */
            MinimizerResult result = new MinimizerResult(simplex);
            if (TrackResuls)
            {
                for (int vertex_index = 0; vertex_index < simplex.VertexCount; vertex_index++)
                {
                    result.EvaluationList.Add(ToolsCollection.Copy(simplex.vertexes[vertex_index]));
                    result.ValueList.Add(simplex.vertex_values[vertex_index]);
                    result.IterationList.Add(0);
                } 
            }

            double[] centroid_vertex = new double[simplex.ParameterCount];
            for (int iteration = 1; iteration <= halting_criterion.MaximumIterationCount; iteration++)
            {

                simplex.ComputeCentroidVertex(centroid_vertex);
                
                // Check if the reflected point is valid; if so, accept or try expansion, if not then try an iside or outside contraction.
                bool reflection_or_expansion_successfull = TryReflectionAndExpansion(minimization_function, validation_function, simplex, centroid_vertex, result, iteration);

                // Check if reflextion and expansion where succesfull
                if (!reflection_or_expansion_successfull)
                {
                    bool contraction_successful = TryContraction(minimization_function, validation_function, simplex, centroid_vertex, result, iteration);
                    // Check if contraction was succesfull
                    if (!contraction_successful)
                    {         
                        bool shrink_successful = TryShrink(minimization_function, validation_function, simplex, centroid_vertex, result, iteration);
                        // Check if shrink was succesfull
                        if (!shrink_successful)
                        {
                            return result;
                        }
                    }
                }

                if (halting_criterion.CheckHalt(simplex))
                {
                    result.IsHalted = false;      
                    break;
                }
            }
            result.IsSuccesFull = true;
            return result;
        }


      

        private void ComputeReflectionVertex(Simplex simplex, double[] centroid_vertex, double [] reflection_vertex)
        {
            for (int parameter_index = 0; parameter_index < simplex.ParameterCount; parameter_index++)
            {
                reflection_vertex[parameter_index] = centroid_vertex[parameter_index] + (reflection_factor * (centroid_vertex[parameter_index] - simplex.vertexes[simplex.LargestVertexIndex][parameter_index]));
            }
        }

        private void ComputeExpansionVertex(Simplex simplex, double[] centroid_vertex, double [] expansion_vertex)
        {

            for (int parameter_index = 0; parameter_index < simplex.ParameterCount; parameter_index++)
            {
                expansion_vertex[parameter_index] = centroid_vertex[parameter_index] + (expansion_factor * (centroid_vertex[parameter_index] - simplex.vertexes[simplex.LargestVertexIndex][parameter_index]));
            }
        }

        public void ComputeContractionVertex(Simplex simplex, double[] centroid_vertex, double [] contraction_vertex)
        {
            for (int parameter_index = 0; parameter_index < simplex.ParameterCount; parameter_index++)
            {
                contraction_vertex[parameter_index] = centroid_vertex[parameter_index] + (contraction_factor * (simplex.vertexes[simplex.LargestVertexIndex][parameter_index] - centroid_vertex[parameter_index]));
            }
        }

        public bool TryReflectionAndExpansion(
            IFunction<double[], double> minimization_function,
            IFunction<double[], bool> validation_function, 
            Simplex simplex,
            double[] centroid_vertex,
            MinimizerResult result,
            int iteration)
        {
            double[] reflection_vertex = new double[simplex.ParameterCount];
            double[] expansion_vertex = new double[simplex.ParameterCount];

            ComputeReflectionVertex(simplex, centroid_vertex, reflection_vertex);
            if (validation_function.Compute(reflection_vertex))
            {
                double reflection_value = minimization_function.Compute(reflection_vertex);
                if (TrackResuls)
                {
                    result.EvaluationList.Add(ToolsCollection.Copy(reflection_vertex));
                    result.ValueList.Add(reflection_value);
                    result.IterationList.Add(iteration);
                }

                //Check if we improve
                if (reflection_value < simplex.LargestVertexValue)
                {
                    // Check if we improve but ar not the lowest
                    if ((simplex.SmallestVertexValue <= reflection_value))
                    {
                        // If we improve but are not the lowest set it
                        simplex.SetVertexLargest(reflection_vertex, reflection_value);    
                        return true;
                    }
                    else
                    {
                        // If reflextion results in the best vertex try expansion
                        ComputeExpansionVertex(simplex, centroid_vertex, expansion_vertex);                 
                        if (validation_function.Compute(expansion_vertex))
                        {
                            double expansion_value = minimization_function.Compute(expansion_vertex);
                            if (TrackResuls)
                            {
                                result.EvaluationList.Add(ToolsCollection.Copy(expansion_vertex));
                                result.ValueList.Add(expansion_value);
                                result.IterationList.Add(iteration);
                            }

                            if (expansion_value < reflection_value)
                            {
                                simplex.SetVertexLargest(expansion_vertex, expansion_value);
                            }
                            else
                            {
                                simplex.SetVertexLargest(reflection_vertex, reflection_value);
                            }
                        }
                        else
                        {
                            //Use reflection because expansion was invalid
                            simplex.SetVertexLargest(reflection_vertex, reflection_value);
                       
                        }
                        return true; //Reflection and possibly expansion was succesful
                    }
                }
                //Reflection was not successful (not lower) 
                return false;
            }
            else
            {
                //Reflection was not successful (not valid) 
                return false;
            }
        }

        public bool TryContraction(

            IFunction<double[], double> minimization_function,
            IFunction<double[], bool> validation_function,
            Simplex simplex,
            double[] centroid_vertex,
            MinimizerResult result,
            int iteration)
        {
            double[] contraction_vertex = new double[simplex.ParameterCount];
            ComputeContractionVertex(simplex, centroid_vertex, contraction_vertex);

            if (validation_function.Compute(contraction_vertex))
            {
                double contraction_value = minimization_function.Compute(contraction_vertex);
                if (TrackResuls)
                {
                    result.EvaluationList.Add(ToolsCollection.Copy(contraction_vertex));
                    result.ValueList.Add(contraction_value);
                    result.IterationList.Add(iteration);
                }

                if (contraction_value < simplex.LargestVertexValue)
                {
                    simplex.SetVertexLargest(contraction_vertex, contraction_value);
                
                    //Contraction was successful
                    return true;
                }
                else
                {
                    //Contraction was not successful (not lower)  
                    return false;
                }
            }
            else
            {
                //Contraction was not successful (not valid) 
                return false;
            }
        }

        public bool TryShrink(
            IFunction<double[], double> minimization_function,
            IFunction<double[], bool> validation_function,
            Simplex simplex,
            double[] centroid_vertex,
            MinimizerResult result, 
            int iteration)
        {
            for (int vertex_index = 0; vertex_index < simplex.VertexCount; vertex_index++)
            {
                if (vertex_index != simplex.SmallestVertexIndex)
                {
                    for (int parameter_index = 0; parameter_index < simplex.ParameterCount; parameter_index++)
                    {
                        simplex.vertexes[vertex_index][parameter_index] = simplex.vertexes[simplex.SmallestVertexIndex][parameter_index] + (shrink_factor * (simplex.vertexes[vertex_index][parameter_index] - simplex.vertexes[simplex.SmallestVertexIndex][parameter_index]));
                    }

                    if (validation_function.Compute(simplex.vertexes[vertex_index]))
                    {
                        double shrink_value = minimization_function.Compute(simplex.vertexes[vertex_index]);
                        if (TrackResuls)
                        {
                            result.EvaluationList.Add(ToolsCollection.Copy(simplex.vertexes[vertex_index]));
                            result.ValueList.Add(shrink_value);
                            result.IterationList.Add(iteration);
                        }
                        simplex.SetVertexValue(vertex_index, shrink_value);
                    }
                    else
                    {
                        //Shrink was not successful (not valid) 
                        return false;
                    }
                }
            }
            //Shrink was successful noone cares about the vertex values
            return true;

        }

    }
}


using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Function;
using System;
using System.Security.Cryptography;
using KozzionMachineLearning.Method.MultiLayerPerceptron;

namespace KozzionMachineLearning.Method.multi_layer_perceptron
{
    public class MultiLayerPerceptronNew<DataType>
	{
		/*
		 * MultiLayerPerceptron(int[] setup);
		 * setup = [#Inputs, #Hidden_1 neurons, ..., #Hidden_k neurons, #Output neurons]
		 * = [l0, l1, ..., l{K-1}]
		 * non-linear function NN: R^n --> R^m, implemented by:
		 * repeated application of non-linear layer functions
		 * NN(s) = (L{0} o ... o L{K-1})(s)
		 * evaluating new input: double [] process(double[] input)
		 * learning from example: void learn(double[] input, double[] target)
		 */

		// local data:
        private RandomNumberGenerator d_random;
        private IAlgebraLinear<DataType> d_algebra;

        private double d_learning_rate; // change with setLearningRate(double)
        private double d_eligibility; // change with setEligibility(double)


		private int               d_layer_count;         // nr of learnable layers (so only hidden and output layers)
        private PerceptronNew<DataType>[] d_layers;
		private int []                    d_setup;



		//
		// ------------- constructors -----------//

	

        public MultiLayerPerceptronNew(
            IAlgebraLinear<DataType> algebra,
			int [] setup,
            IFunction<double, double> output_function,
            IFunction<double, double> output_function_derivative,
            double learning_rate,
            double eligibility)
		{
            d_algebra = algebra;
            d_random = new RNGCryptoServiceProvider();
            d_learning_rate = learning_rate;
            d_eligibility = eligibility;
			Perceptron.nLayers = 0;
			this.d_setup = setup;
			d_layer_count = setup.Length - 1; // nr of learnable layers, no input layer
            d_layers = new PerceptronNew<DataType>[d_layer_count];

			// hidden + output layers:
			for (int l = 0; l < d_layer_count; ++l)
			{
				int nInput = setup[l]; // input size = previous layers output size
				int nOutput = setup[l + 1]; // output size = nr of neurons in this layer

                d_layers[l] = new PerceptronNew<DataType>(d_algebra, nInput, nOutput, output_function, output_function_derivative);
			}
		}

		// ------------- public functions -----------//
		/*
		 * processing functions
		 */
        public AMatrix<DataType> forward(
            AMatrix<DataType> input)
		{
			// each layer processes the output of the previous layer
			AMatrix<DataType> output = d_layers[0].process(input);
			for (int layer_index = 1; layer_index < d_layer_count; layer_index++)
			{
                output = d_layers[layer_index].process(output);
			}
			// return final output
            return output;
		}





		public void train(
            AMatrix<DataType>[] training_inputs,
            AMatrix<DataType>[] training_targets)
		{
			int [] draw_unique = d_random.RandomPermutation(training_inputs.Length);
			foreach (int integer in draw_unique)
			{
				train(training_inputs[integer], training_targets[integer]);
			}

			// train_batch(training_inputs_double, training_targets_double);
		}


        public void train(
            AMatrix<DataType> input,
            AMatrix<DataType> target)
        {// train on single input and target example
            // first pass: forward information propagation
            AMatrix<DataType> output = forward(input);

            // second pass: backward information propagation
            AMatrix<DataType> error = target - output;  
            for (int later_index = d_layer_count - 1; later_index >= 0; later_index--)
            {
                error = d_layers[later_index].back_propagate(error);
            }

            // third pass: learning. AMatrix<MatrixMemory<float>, float> 
            // uses the information stored during previous passes
            for (int layer_index = 0; layer_index < d_layer_count; layer_index++)
            {
                d_layers[layer_index].learn(d_learning_rate, d_eligibility);
            }
        }

		public void train_batch(
            AMatrix<DataType>[] inputs,
            AMatrix<DataType>[] targets)
		{// train on data set; batch learning
		 // outer loop goes through all input, and makes the layers collect batch weights.
			for (int index_example = 0; index_example < inputs.Length; index_example++)
			{
                AMatrix<DataType> output = forward(inputs[index_example]);

				// second pass: backward information propagation
                AMatrix<DataType> error = targets[index_example] - output;
                for (int later_index = d_layer_count - 1; later_index >= 0; later_index--)
                {
                    error = d_layers[later_index].back_propagate(error);
                }
				// third pass: learning.
				// uses the information stored during previous passes
				for (int layer_index = 0; layer_index < d_layer_count; layer_index++)
				{
					d_layers[layer_index].train_batch(d_learning_rate, d_eligibility);
				}
			}

			for (int layer_index = 0; layer_index < d_layer_count; layer_index++) // all input has been parsed, apply the
																				  // weights gained from this batch.
			{
				d_layers[layer_index].applyBatchWeights();
			}
		}

		

		public void collect_weights2(
            AMatrix<DataType> input,
            double t)
		{
			d_layers[0].process(input);
            for (int l = 1; l < d_layer_count; l++)
            {
                d_layers[l].process(d_layers[l - 1].getOutput());
            }

			// second pass: backward information propagation
			//float [] target = {t};
			//d_layers[d_layer_count - 1].back_propagate(MathToolsFloatArray.subtract(target, d_layers[d_layer_count - 1].getOutput()));
			for (int l = d_layer_count - 2; l >= 0; l--)
				d_layers[l].back_propagate(d_layers[l + 1].getInputError());

			// third pass: learning.
			// uses the information stored during previous passes
			for (int l = 0; l < d_layer_count; l++)
				d_layers[l].train_batch(d_learning_rate, d_eligibility);
		}

		public void apply_weights()
		{
			for (int l = 0; l < d_layer_count; l++)
				// all input has been parsed, apply the weights gained from this batch.
				d_layers[l].applyBatchWeights(); // this method also resets the weightchanges
		}

		/*
		 * getters and setters
		 */
		public void set_learning_rate(
            double alpha)
		{
			d_learning_rate = alpha;
		}

		public void set_eligibility(
            double lambda)
		{
			d_eligibility = lambda;
		}

		public int [] get_hidden_layer_sizes()
		{
			return d_setup;
		}

		public int get_input_dimension_count()
		{
			return d_layers[0].getInputSize();
		}

		public int get_output_dimension_count()
		{
			return d_layers[d_layer_count - 1].getOutputSize();
		}

		/*
		 * printing
		 */
		public override String ToString()
		{// only weights
			String ret = "";
			ret += d_layer_count + " ";
			for (int l = 0; l < d_layer_count; ++l)
				ret += d_layers[l].ToString() + " ";
			return ret;
		}

	}
}
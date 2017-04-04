using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.Matrix;
using KozzionMathematics.Function;
using System;
using System.Security.Cryptography;

namespace KozzionMachineLearning.Method.MultiLayerPerceptron
{
    public class PerceptronNew<MatrixType>
	{

		/* 
		 * class NeuronLayer(int n, int m); 
		 *  non-linear function L: R^n --> R^m, implemented by: 
		 * 		- a linear mapping:
		 *          	W: R^n --> R^m,  activation = W*input
		 *     	- followed by a non-linear mapping:
		 *          	f: R^m --> R^m,      output = f(activation)
		 *  
		 *  evaluating: double [] process(double[] input)
		 *  learning:   double [] learn  (double[] error)
		 *  
		 *  available info: double [] getActivation()
		 *                  double [] getOutput    ()
		 *                  double [] getError     ()
		 *  
		 * N.B.: this implementation of a layer is different form normal implementations
		 * The weights are the weights used on the incoming signals, not the outgoing ones. 
		 * This was done to make to avoid having three different layer types.
		 */

		//static data
		public static int nLayers = 0;
		public const int IDENTITY = 0;
        public const int RANDOM = 1;

        private IAlgebraLinear<MatrixType> d_algebra;
        private IFunction<double, double> d_output_function;
        private IFunction<double, double> d_output_function_derivative;

		//local data:
		private int layerNumber;
	
		private int    d_input_size;		//length of input + 1 for bias
		private int    d_output_size;     //length of output

        private AMatrix<MatrixType> d_weights; //weights for linear mapping. W(i,:); weights belonging to neuron i
        private AMatrix<MatrixType> d_batch_weights; // temp storage for weights during batch learning
        private AMatrix<MatrixType> d_eligibility_traces; //lambda-traces
        private AMatrix<MatrixType> d_input;   //The input signal is stored during forward-pass
        private AMatrix<MatrixType> d_activation;   // = W*in --> matrix times vector
        private AMatrix<MatrixType> d_output;   // = f(activation)
        private AMatrix<MatrixType> d_output_error;   //The error on the output is stored during backpropagation
        private AMatrix<MatrixType> activation_error;   // = f'(activation) .* output_error
        private AMatrix<MatrixType> input_error;   // = W^T*activation_error

        private RandomNumberGenerator d_random;
		//------------- constructors -----------//
        public PerceptronNew(
                 IAlgebraLinear<MatrixType> algebra,
				int input_dimensions, 
				int output_dimensions,
                IFunction<double, double> output_function,
                IFunction<double, double> output_function_derivative)
		{
            d_random = new RNGCryptoServiceProvider();
            d_algebra = algebra;
			d_output_function = output_function;
			d_output_function_derivative =         output_function_derivative;
    		layerNumber = nLayers++; //useful for debugging purposes
    	
    		d_input_size  = input_dimensions + 1;  //input + bias
    		d_output_size = output_dimensions;

            d_weights = d_algebra.CreateZeros(d_input_size, d_output_size);
            d_batch_weights = d_algebra.CreateZeros(d_input_size, d_output_size);
            d_eligibility_traces = d_algebra.CreateZeros(d_input_size, d_output_size);
            d_input = d_algebra.CreateZeros(1, d_input_size);
            input_error = d_algebra.CreateZeros(1, d_input_size);
            d_activation = d_algebra.CreateZeros(1, d_output_size);
            activation_error = d_algebra.CreateZeros(1, d_output_size);
            d_output = d_algebra.CreateZeros(1, d_output_size);
            d_output_error = d_algebra.CreateZeros(1, d_output_size);

            ClearInputMatrix();
		}  
	
		//------------- public functions -----------//
		/*
		 * processing functions
		 */
        public AMatrix<MatrixType> process(AMatrix<MatrixType> input) 
		{
			//apply function y = f(Wx)
    		//store local copy of the input and create space for bias
            d_input.SetSubMatrix(0, 0, input);
            d_input.SetElement(1, d_input_size - 1, 1.0);//put in bias
    	
			//calculate and store activation
            d_activation = d_input * d_weights;
         
    		//calculate and store output
            d_output = d_activation.Transform(d_output_function);
		
			//and return the output to be processed by later layers
			return d_output;
		}

        public AMatrix<MatrixType> back_propagate(AMatrix<MatrixType> output_error)
		{
			//only use after forward signal has been processed
    		//store local copy of the output error
            d_output_error = output_error; //will remove bias of next layer
    	
    		//calculate and store activation error
            activation_error =  (d_activation * d_output_error).Transform(d_output_function_derivative);

            //calculate and store output

    	
    		//calculate and store the input error
    	    input_error = d_weights * activation_error;
	   		//and return signal to be processed by previous layers
    		return input_error;
		}
    
		public void applyBatchWeights()
		{
			d_weights += d_batch_weights; // apply batch weights to all weights.
			d_batch_weights.Clear(); // and reset the batch weight array to be ready for the next batch.		
		}

        public void train_batch(double learning_rate, double eligibility)
		{//only use after backward signal has been backpropagated		
			//adjust all weights in this layer, including bias

            AMatrix<MatrixType> gradient = d_input * activation_error;

            d_eligibility_traces *= eligibility;   //decay of past information
			d_eligibility_traces += gradient; //addition of information

            d_batch_weights += d_eligibility_traces * learning_rate; //gradient;
			
		}

        public void learn(double learning_rate, double eligibility)
		{//only use after backward signal has been backpropagated		
			//adjust all weights in this layer, including bias
            AMatrix<MatrixType> gradient = d_input * activation_error;

            d_eligibility_traces *= eligibility;   //decay of past information
            d_eligibility_traces += gradient; //addition of information

            d_weights += d_eligibility_traces * learning_rate; //gradient;
		}
	
		/*
		 * getters and setters
		 */
		public int getInputSize()
		{//including bias
			return d_input_size;
		}

		public int getOutputSize()
		{
			return d_output_size;
		}

        public AMatrix<MatrixType> getInput()
		{//including 1.0 for bias
			return d_input;
		}

        public AMatrix<MatrixType> getActivation() 
		{
			return d_activation;
		}

        public AMatrix<MatrixType> getOutput() 
		{
			return d_output;
		}

        public AMatrix<MatrixType> getInputError()
		{//including error on bias
			return input_error;
		}

        public AMatrix<MatrixType> getActivationError() 
		{
            return activation_error;
		}

        public AMatrix<MatrixType> getOutputError() 
		{
			return d_output_error;
		}
	
	
		public void ClearInputMatrix()
		{// destroy all information
            d_weights = d_algebra.CreateRandomUnit(d_weights.RowCount, d_weights.ColumnCount, d_random);

		}
	
		/*
		 * printing
		 */
		public override String ToString()
		{
			String ret = "layer" + (layerNumber+1) + "Dim=[ " + d_output_size + " " + d_input_size + " ];\n";
			ret += "net.W{" + (layerNumber+1) + "}=[ "; //idx+1 for matlab....
			for(int o = 0; o < d_output_size; ++o)
			{	
				for(int i = 0; i < d_input_size; ++i)
        		{	
        			ret += d_weights.GetElement(o, i).ToString();
        			ret	+= i + 1 == d_input_size ? 
        				  (o + 1 == d_output_size ? " ];\n" : " ;\n            ") : " ";
        		}
			}
			return ret;
		}
	}
}
using KozzionCore.Tools;
using KozzionMathematics.Function;
using System;
using System.Security.Cryptography;
namespace KozzionMachineLearning.Method.multi_layer_perceptron
{
	public class Perceptron
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
	
		private IFunction<float,float> d_output_function;
		private IFunction<float,float> d_output_function_derivative;

		//local data:
		private int layerNumber;
	
		private int    d_input_size;		//length of input + 1 for bias
		private int    d_output_size;     //length of output
	
		private float [,] d_weights     ; //weights for linear mapping. W(i,:); weights belonging to neuron i
		private float [,] d_batch_weights     ; // temp storage for weights during batch learning
		private float [,] d_eligibility_traces; //lambda-traces
		private float [] d_input	   		;   //The input signal is stored during forward-pass
		private float [] d_activation  		;   // = W*in --> matrix times vector
		private float []d_output      		;   // = f(activation)
		private float [] d_output_error		;   //The error on the output is stored during backpropagation
		private float [] activation_error;   // = f'(activation) .* output_error
		private float  [] input_error 		;   // = W^T*activation_error

        private RandomNumberGenerator d_random;
		//------------- constructors -----------//
		public Perceptron(
				int input_dimensions, 
				int output_dimensions, 
				IFunction<float,float> output_function,
				IFunction<float,float> output_function_derivative)
		{
            d_random = new RNGCryptoServiceProvider();
			d_output_function = output_function;
			d_output_function_derivative =         output_function_derivative;
    		layerNumber = nLayers++; //useful for debugging purposes
    	
    		d_input_size  = input_dimensions + 1;  //input + bias
    		d_output_size = output_dimensions;
    	
			d_weights          = new float [d_output_size, d_input_size];
			d_batch_weights     = new float [d_output_size, d_input_size];
			d_eligibility_traces = new float [d_output_size, d_input_size];
			d_input		     = new float [d_input_size];
			d_activation       = new float [d_output_size];
			d_output           = new float [d_output_size];
			input_error      = new float [d_input_size];
			activation_error = new float [d_output_size];
			d_output_error     = new float [d_output_size];
        
			setInputMatrix(RANDOM);
		}  
	
		//------------- public functions -----------//
		/*
		 * processing functions
		 */
		public float[] process(float[] input) 
		{
			//apply function y = f(Wx)
    		//store local copy of the input and create space for bias
            ToolsCollection.CopyRBA(input, d_input);
    		d_input[d_input_size - 1] = 1.0f; //put in bias
    	
			//calculate and store activation
            ToolsCollection.SetValue(d_activation, 0.0f);
    		for (int o = 0; o < d_output_size; o++)
    		{
    			for(int i = 0; i < d_input_size; i++)
    			{
    				d_activation[o] += d_weights[o,i] * d_input[i];
    			}
    		}
    		//calculate and store output
    		for (int o = 0; o < d_output_size; o++)
    		{
                d_output[o] = d_output_function.Compute((float)d_activation[o]); //TODO double?
    		}
		
			//and return the output to be processed by later layers
			return d_output;
		}

		public float[] back_propagate(float[] out_error)
		{
			//only use after forward signal has been processed
    		//store local copy of the output error
    		d_output_error = ToolsCollection.Select(out_error, 0, d_output_size); //will remove bias of next layer
    	
    		//calculate and store activation error
    		for(int o = 0; o < d_output_size; o++)
    		{
                activation_error[o] = d_output_function_derivative.Compute((float)d_activation[o]) * d_output_error[o];
    		}
    	
    		//calculate and store the input error
            ToolsCollection.SetValue(input_error, 0);
    		for(int i = 0; i < d_input_size; i++)
    		{	
    			for(int o = 0; o < d_output_size; o++)
        		{
        			input_error[i] += activation_error[o] * d_weights[o, i];
        		}
    		}
	   		//and return signal to be processed by previous layers
    		return input_error;
		}
    
		public void applyBatchWeights()
		{
			for(int o = 0; o < d_output_size; o++)
			{
				for(int i = 0; i < d_input_size; i++)		
				{	
					d_weights[o,i] += d_batch_weights[o,i]; // apply batch weights to all weights.
					d_batch_weights[o,i] = 0; // and reset the batch weight array to be ready for the next batch.
				}
			}
		}
    
		public void train_batch(float learning_rate, float eligibility)
		{//only use after backward signal has been backpropagated		
			//adjust all weights in this layer, including bias
			for(int o = 0; o < d_output_size; o++)
			{
				for(int i = 0; i < d_input_size; i++)		
				{
					float gradient = activation_error[o] * d_input[i];
				
					d_eligibility_traces[o,i] *= (eligibility);   //decay of past information
					d_eligibility_traces[o,i] += gradient; //addition of information
				
					d_batch_weights[o,i] += learning_rate * d_eligibility_traces[o,i]; //gradient;
				}
			}
		}
	
		public void learn(float learning_rate, float eligibility)
		{//only use after backward signal has been backpropagated		
			//adjust all weights in this layer, including bias
			for(int o = 0; o < d_output_size; o++)
			{
				for(int i = 0; i < d_input_size; i++)
				{
                    float gradient = activation_error[o] * d_input[i];
					d_eligibility_traces[o, i] *= (eligibility); //decay of past information
					d_eligibility_traces[o, i] += gradient;     //addition of information
					d_weights[o, i] += learning_rate * d_eligibility_traces[o, i]; //gradient;
				}
			}
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
	
		public float[] getInput()
		{//including 1.0 for bias
			return d_input;
		}
	
		public float[] getActivation() 
		{
			return d_activation;
		}

		public float[] getOutput() 
		{
			return d_output;
		}

		public float[] getInputError()
		{//including error on bias
			return input_error;
		}
	
		public float[] getActivationError() 
		{
            return activation_error;
		}

		public float[] getOutputError() 
		{
			return d_output_error;
		}
	
	
		public void setInputMatrix(int matrix)
		{// destroy all information
			switch (matrix) 
			{
			case Perceptron.IDENTITY:
				for (int o = 0; o < d_output_size; o++)
				{	
					for (int i = 0; i < d_input_size; i++)
			
					{
                        if (o == i)
                        {
                            d_weights[o, i] = 1;
                        }
                        else
                        {
                            d_weights[o, i] = 0;
                        }
					}
				}
				break;
			case Perceptron.RANDOM: //w = [-1..1]
				for (int o = 0; o < d_output_size; o++)
				{	
					for (int i = 0; i < d_input_size; i++)
					{
						d_weights[o, i] = (d_random.RandomFloat32Unit()  / 2.0f) - 0.25f; // [- 0.25, 0.25]
					}					
				}
				break;
			}
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
        			ret += d_weights[o, i];
        			ret	+= i + 1 == d_input_size ? 
        				  (o + 1 == d_output_size ? " ];\n" : " ;\n            ") : " ";
        		}
			}
			return ret;
		}
	}
}
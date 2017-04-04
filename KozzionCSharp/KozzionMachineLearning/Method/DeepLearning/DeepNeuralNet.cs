namespace KozzionMachineLearning.Method.deeplearning
{
    class DeepNeuralNet
    {
        //X is the input training distribution for the network
        //epsilon is a learning rate for the stochastic gradient descent in Contrastive Divergence
        //L is the number of layers to train
        //n=(n[1], ...,n[L]) is the number of hidden units in each layer
        //W[i] is the weight matrix for level i, for i from 1 to L
        //b[i] is the bias vector for level i, for i from 0 to L

       // PreTrainUnsupervisedDBN(X, epsilon, L, n, W, b):



        public void PreTrainUnsupervisedDBN(float [][] training_data, float learning_rate,  RBMLayer [] layers) 
        {
            //initialize b[0]=0
            //for l=1 to L:
            // while not stopping criterion:
            //sample g[0]=x from X
            //for i=1 to l-1:
            //   sample g[i] from Q(g[i]|g[i-1])
            //RBMupdate(g[l-1], epsilon, W[l], b[l], b[l-1])
        }

        // v[0] is a sample from the training distribution for the RBM
        // epsilon is a learning rate for the stochastic gradient descent in Contrastive Divergence
        // W is the RBM weight matrix, of dimension (number of hidden units, number of inputs)
        // b is the RBM biases vector for hidden units
        //c is the RBM biases vector for input units

        public void RBMupdate(float [] example, float learning_rate, float [,] weights, float hidden_biases, float input_biases)
        {
  
            //RBMupdate(v[0], epsilon, W, b, c):

              //  for all hidden units i:
                //    compute Q(h[0][i] = 1 | v[0]) # for binomial units, sigmoid(b[i] + sum_j(W[i][j] * v[0][j]))
                  //  sample h[0][i] from Q(h[0][i] = 1 | v[0])

                //for all visible units j:
                  //  compute P(v[1][j] = 1 | h[0]) # for binomial units, sigmoid(c[j] + sum_i(W[i][j] * h[0][i]))
                    //sample v[1][j] from P(v[1][j] = 1 | h[0])

                //for all hidden units i:
                  //  compute Q(h[1][i] = 1 | v[1]) # for binomial units, sigmoid(b[i] + \sum_j(W[i][j] * v[1][j]))

//                W += epsilon * (h[0] * v[0]' - Q(h[1][.] = 1 | v[1]) * v[1]')
    //            b += epsilon * (h[0] - Q(h[1][.] = 1 | v[1]))
  //              c += epsilon * (v[0] - v[1])


           // weights       += learning_rate * ()
            //hidden_biases += learning_rate * (hidden_biases[0] - Q(example);
            //input_biases  += learning_rate * (example[0] - example[0]);
        }
    }
}

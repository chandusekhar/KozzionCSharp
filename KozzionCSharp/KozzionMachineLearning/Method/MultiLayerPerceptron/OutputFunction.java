package com.kozzion.library.machinelearning.estimator.multilayerperceptron;

public class OutputFunction
{
    public enum Type
    {
        LINEAR, STEP, SIGMOID, TANH, BOLTZMANN
    }

    Type d_type;

    public OutputFunction(Type type)
    {
        d_type = type;
    }

    public void setFunction(Type type)
    {
        d_type = type;
    }

    public double [] normal(double input[])
    {
        double [] output = new double [input.length];
        for (int i = 0; i < input.length; i++)
            output[i] = normal(input[i]);
        return output;
    }

    public double normal(double input)
    {
        switch (d_type)
        {
            case LINEAR:
                return input;
            case STEP:
                return input;
            case SIGMOID:
                return normalSigmoid(input);
            case TANH:
                return normalTanh(input);
            case BOLTZMANN:
                return normalBoltzmann(input);
        }
        return 0;
    }

    public double [] derivative(double input[])
    {
        double [] output = new double [input.length];
        for (int i = 0; i < input.length; i++)
            output[i] = derivative(input[i]);
        return output;
    }

    public double derivative(double input)
    {
        switch (d_type)
        {
            case LINEAR:
                return input; // NI
            case STEP:
                return input; // NI
            case SIGMOID:
                return derivativeSigmoid(input);
            case TANH:
                return derivativeTanh(input);
            case BOLTZMANN:
                return derivativeBoltzmann(input);
        }
        return 0;
    }

    // ////sigmoid
    private double normalSigmoid(double input)
    {
        return 1 / (1 + Math.exp(-input));
    }

    private double derivativeSigmoid(double input)
    {
        double temp = normalSigmoid(input);
        return temp * (1 - temp);
    }

    // ////tanh
    private double normalTanh(double input)
    {
        return Math.tanh(input);
    }

    private double derivativeTanh(double input)
    {
        double temp = normalTanh(input);
        return 1 - temp * temp;
    }

    // ////boltzmann binary stochastic neuron
    private double normalBoltzmann(double input)
    {
        return Math.random() > normalSigmoid(input) ? 1.0 : 0.0;
    }

    private double derivativeBoltzmann(double input)
    {
        return derivativeSigmoid(input);
    }

    public String toString()
    {//matlab functions
    	switch(d_type)
    	{
    	case LINEAR:    return "f = @(x)(x);df = @(x)(1)"; //NI
        case STEP:      return "f = @(x)(x>0);df = @(x)(1)"; //NI
        case SIGMOID:   return "f = @(x)(1./(1+exp(-x)));df = @(x)((1-1./(1+exp(-x)))./(1+exp(-x)))";
        case TANH:	    return "f = @(x)(tanh(x));df = @(x)(1 - tanh(x)*tanh(x))";
        case BOLTZMANN: return "??";
    	}
    	return "unknown function";
    }
}

package com.kozzion.library.machinelearning.methods.supportvectormachine;

import java.io.File;
import java.io.IOException;

import com.kozzion.library.machinelearning.estimator.IEstimatorFloatArrayTrainable;
import com.kozzion.library.machinelearning.estimator.IFunctionFloatArray;

import de.bwaldvogel.liblinear.Feature;
import de.bwaldvogel.liblinear.FeatureNode;
import de.bwaldvogel.liblinear.Linear;
import de.bwaldvogel.liblinear.Model;
import de.bwaldvogel.liblinear.Parameter;
import de.bwaldvogel.liblinear.Problem;
import de.bwaldvogel.liblinear.SolverType;

public class LibLinearSvm
    implements
        IEstimatorFloatArrayTrainable
{
    Model     d_model;
    Parameter d_parameter;

    @Override
    public IFunctionFloatArray aproximate(
        float [][] instances,
        float [][] targetes)
    {
        // TODO Auto-generated method stub
        return null;
    }

    public LibLinearSvm()
    {
        SolverType solver = SolverType.L2R_LR; // -s 0
        double C = 1.0; // cost of constraints violation
        double eps = 0.01; // stopping criteria
        d_parameter = new Parameter(solver, C, eps);
    }

    public float [] estimate(
        float [] input)
    {
        return new float [] {(float) Linear.predict(d_model, create_instance(input))};
    }

    @Override
    public void train(
        float [][] training_inputs,
        float [][] training_targets)
    {
        Problem problem = new Problem();
        problem.l = training_inputs.length; // number of training examples
        problem.n = training_inputs[0].length; // number of features
        problem.x = create_instances(training_inputs); // feature nodes
        problem.y = create_targets(training_targets); // target values
        d_model = Linear.train(problem, d_parameter);

    }

    private double [] create_targets(
        float [][] float_targets)
    {
        double [] double_targets = new double [float_targets.length];
        for (int index = 0; index < float_targets.length; index++)
        {
            double_targets[index] = float_targets[index][0];
        }
        return double_targets;
    }

    private Feature [] create_instance(
        float [] input)
    {
        Feature [] instance = new Feature [input.length];
        for (int index = 0; index < instance.length; index++)
        {
            instance[index] = new FeatureNode(index, input[index]);
        }
        return instance;
    }

    private Feature [][] create_instances(
        float [][] training_inputs)
    {
        Feature [][] instances = new Feature [training_inputs.length] [];
        for (int index = 0; index < training_inputs.length; index++)
        {
            instances[index] = create_instance(training_inputs[index]);
        }
        return instances;
    }

    public void save_to_file(
        File file)
    {
        try
        {
            d_model.save(file);
        }
        catch (IOException e)
        {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

    public void load_fron_file(
        File file)
    {
        try
        {
            d_model = Model.load(file);
        }
        catch (IOException e)
        {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }
}

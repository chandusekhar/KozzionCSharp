package com.kozzion.library.machinelearning.estimator.multilayerperceptron;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;



import com.kozzion.library.core.datastructure.Tuple2;
import com.kozzion.library.core.utility.CollectionTools;
import com.kozzion.library.machinelearning.transform.PercentileTransformDouble;
import com.kozzion.library.math.statistics.FMeasure;

public class MultiLayerPerceptronClassifierOld
{
    private MultiLayerPerceptron d_multi_layer_perceptron;
    private PercentileTransformDouble d_transform; 
    public MultiLayerPerceptronClassifierOld(int intput_size, int hidden_layer_size)
    {
        int [] setup = {intput_size, hidden_layer_size, 1};
        d_multi_layer_perceptron = new MultiLayerPerceptron(setup);
    }
    
    public void train_transform(List<Tuple2<double [], Boolean>> untransformed_examples, double lower_percentile)
    {
        List<double []> raw_data = new ArrayList<>();
        
        for (Tuple2<double[], Boolean> pair : untransformed_examples)
        {
            raw_data.add(pair.get_object1());
        }
        d_transform = new PercentileTransformDouble(raw_data, lower_percentile);
    }
    
    public void train(List<Tuple2<double [], Boolean>> untransformed_examples, int epoch_count, double learning_rate)
    {
        d_multi_layer_perceptron.set_learning_rate(learning_rate);
        List<Tuple2<double [], double []>> transformed_examples = new ArrayList<>();

        if (d_transform == null)
        {
            for (Tuple2<double[], Boolean> pair : untransformed_examples)
            {
                if (pair.get_object2())
                {
                    double [] target = {1.0};
                    transformed_examples.add(new Tuple2<double[], double[]>(pair.get_object1(), target));
                }
                else
                {
                    double [] target = {0.0}; 
                    transformed_examples.add(new Tuple2<double[], double[]>(pair.get_object1(), target));
                }
              
            } 
        }
        else
        {
            for (Tuple2<double[], Boolean> pair : untransformed_examples)
            {
                if (pair.get_object2())
                {
                    double [] target = {1.0};
                    transformed_examples.add(new Tuple2<double[], double[]>(d_transform.transform_copy(pair.get_object1()), target));
                }
                else
                {
                    double [] target = {0.0}; 
                    transformed_examples.add(new Tuple2<double[], double[]>(d_transform.transform_copy(pair.get_object1()), target));
                }
              
            }
        }

        
        for (int epoch = 0; epoch < epoch_count; epoch++)
        {
            Collections.shuffle(transformed_examples);
            for (Tuple2<double[], double[]> transformed_example : transformed_examples)
            {
                d_multi_layer_perceptron.train(transformed_example.get_object1(), transformed_example.get_object2());
            } 
        }
    }
    
    public FMeasure test(List<Tuple2<double [], Boolean>> test_examples, double output_threshold)
    {
        FMeasure measure = new FMeasure();
        for (Tuple2<double[], Boolean> input_pair : test_examples)
        {
            
            boolean output = false;            
            double result = 0;
            if(d_transform == null)
            {
                result = d_multi_layer_perceptron.estimate(input_pair.get_object1())[0];
            }
            else
            {
                result = d_multi_layer_perceptron.estimate(d_transform.transform_copy(input_pair.get_object1()))[0];
            }
    
            if (output_threshold <= result )
            {
                output = true;
            }
            measure.add(input_pair.get_object2(),output);
            
        }
        return measure;
    }
}

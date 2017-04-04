package com.kozzion.library.machinelearning.classifier;

import java.awt.Point;
import java.util.List;

import com.kozzion.library.core.datastructure.Tuple2;
import com.kozzion.library.graphics.elementtree.feature.IFeatureGeneratorFloat;
import com.kozzion.library.graphics.elementtree.feature.MomentGeneratorFloat2D;
import com.kozzion.library.graphics.image.IIntegerRasterBooleanImage2D;
import com.kozzion.library.graphics.image.IntegerRasterBooleanImage2D;

/**
 * Uses invariant moments to classify binary images Invariant moments are scale
 * and translation invariant but not rotation invariant
 */
public class ClassifierBooleanImage2DClassifier<ClassType>
    extends
        AClassifier<IntegerRasterBooleanImage2D, ClassType>
{
    IFeatureGeneratorFloat<List<Point>>                  d_feature_generator;
    IClassifierIncremental<IIntegerRasterBooleanImage2D, ClassType> d_inner_classifier;


    public ClassifierBooleanImage2DClassifier()
    {
        d_feature_generator = new MomentGeneratorFloat2D(4);
        d_inner_classifier = null;
    }

    public void train(
        List<Tuple2<IntegerRasterBooleanImage2D, ClassType>> data)
    {

    }

    @Override
    public ClassType classify(
        IntegerRasterBooleanImage2D input)
    {
        float [] features = new float [d_feature_generator.get_feature_count()];
        d_feature_generator.generate_features(input.get_points_with_value(true), features);
        return null;// d_inner_classifier.classify(features);
    }

}

package com.kozzion.library.graphics.color;

import com.kozzion.library.graphics.image.IntegerRasterIntegerImage2D;

public class IntegerToBooleanConverterTemplateOtsu implements IIntegerToBooleanConverterTemplate
{

    @Override
    public IIntegerToBooleanConverter create_integer_to_boolean_converter(int [] values)
    {
        int [] histData = new int [256];
        for (int i : values)
        {
            histData[i]++;
        }

        // Total number of pixels
        int total = values.length;

        float sum = 0;
        for (int t = 0; t < 256; t++)
        {
            sum += t * histData[t];
        }
        float sumB = 0;
        int wB = 0;
        int wF = 0;

        float best_variance = 0;
        int best_threshold = 0;

        for (int threshold = 0; threshold < 256; threshold++)
        {
            wB += histData[threshold]; // Weight Background
            wF = total - wB; // Weight Foreground
            
            if (wB == 0)
                continue;


            if (wF == 0)
                break;

            sumB += (float) (threshold * histData[threshold]);

            float mB = sumB / wB; // Mean Background
            float mF = (sum - sumB) / wF; // Mean Foreground

            // Calculate Between Class Variance
            float varBetween = (float) wB * (float) wF * (mB - mF) * (mB - mF);

            // Check if new maximum found
            if (varBetween > best_variance)
            {
                best_variance = varBetween;
                best_threshold = threshold;
            }
        }

        return new IntegerToBooleanConverterThreshold(best_threshold + 1);
    }

}

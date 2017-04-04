package com.kozzion.library.math.datastructure.kdtree;

import com.kozzion.library.math.datastructure.matrix.DoubleVector;

public abstract class DataEntry<LabellingType> extends DoubleVector
{

    /**
     * 
     */
    private static final long   serialVersionUID = 1L;
    private final LabellingType d_labelling;

    public DataEntry(final LabellingType labelling, final double dimension1_value, final double dimension2_value)
    {
        super(dimension1_value, dimension2_value);
        d_labelling = labelling;
    }

    public final LabellingType get_entry()
    {
        return d_labelling;
    }
}

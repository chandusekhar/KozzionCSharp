package com.kozzion.library.core.file.vcf;

import java.util.List;
import java.util.Map;
import java.util.Vector;

import com.kozzion.library.core.datastructure.map.HyperMapable;

public class VCFRecord implements HyperMapable<String>
{
    Map<String, String> d_fields;

    public VCFRecord(final List<String> templines)
    {
        // TODO Auto-generated constructor stub
    }

    public List<String> get_fields()
    {
        return new Vector<String>(d_fields.keySet());
    }

    public String [] get_array(final List<String> fields)
    {
        // TODO Auto-generated method stub
        return null;
    }

}

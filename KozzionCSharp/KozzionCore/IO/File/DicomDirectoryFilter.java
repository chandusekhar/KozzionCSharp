package com.kozzion.library.core.file.filter;

import java.io.File;
import java.io.FileFilter;


public class DicomDirectoryFilter implements FileFilter
{
    DicomFileFilter d_filter;
    public DicomDirectoryFilter()
    {
        d_filter = new DicomFileFilter();
    }
    @Override
    public boolean accept(File file)
    {
        if (file.isDirectory())
        {
            for (File local_file : file.listFiles())
            {
                if (d_filter.accept(local_file))
                {
                    return true;
                }
            }
            return false;
      
        }
        else
        {
            return false;
        }
        
    }

}

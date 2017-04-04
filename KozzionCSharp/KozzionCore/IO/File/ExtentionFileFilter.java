package com.kozzion.library.core.file.filter;

import java.io.File;
import java.util.HashSet;
import java.util.Set;

import com.kozzion.library.core.file.FileTools;

public class ExtentionFileFilter extends javax.swing.filechooser.FileFilter implements java.io.FileFilter
{
    Set<String> d_accepted_extentions;
    String      d_description;
    boolean     d_allow_directories;

    public ExtentionFileFilter()
    {
        d_accepted_extentions = new HashSet<String>();
        d_allow_directories = true;
    }

    public ExtentionFileFilter(final String extention)
    {
        d_accepted_extentions = new HashSet<String>();
        d_accepted_extentions.add(extention);
        d_description = extention + " files";
        d_allow_directories = true;
    }

    public ExtentionFileFilter(final String extention, final boolean allow_directories)
    {
        d_accepted_extentions = new HashSet<String>();
        d_accepted_extentions.add(extention);
        d_description = extention + " files";
        d_allow_directories = allow_directories;
    }

    @Override
    public boolean accept(final File file)
    {
        if (file.isHidden())
        {
            return false;
        }
        if (file.isDirectory() && d_allow_directories)
        {
            return true;
        }

        if (d_accepted_extentions.contains(FileTools.get_extention(file.getName())))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void addExtension(final String string)
    {
        d_accepted_extentions.add(string);

    }

    @Override
    public String getDescription()
    {
        return d_description;
    }

    public void setDescription(final String description)
    {
        d_description = description;
    }

}

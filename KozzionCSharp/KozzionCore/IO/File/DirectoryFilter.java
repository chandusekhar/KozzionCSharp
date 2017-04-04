package com.kozzion.library.core.file.filter;

import java.io.File;

import javax.swing.filechooser.FileFilter;

public class DirectoryFilter extends FileFilter
{

    @Override
    public boolean accept(final File file)
    {
        return file.isDirectory();
    }

    @Override
    public String getDescription()
    {
        return "Directories";
    }

}

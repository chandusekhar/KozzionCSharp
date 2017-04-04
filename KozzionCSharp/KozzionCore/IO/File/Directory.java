package com.kozzion.library.core.file;

import java.io.File;
import java.io.FileFilter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import com.kozzion.library.core.utility.CollectionTools;

public class Directory implements IDirectory
{
    File d_file;
    public Directory(final String file_name, final boolean create) throws IllegalArgumentException
    {
        this(new File(file_name), create);
    }
    
    public Directory(final String file_name) throws IllegalArgumentException
    {
        this(new File(file_name), false);
    }

    public Directory(final File file) throws IllegalArgumentException
    {
        this(file, false);
    }

    public Directory(final File file, final boolean create) throws IllegalArgumentException
    {
        if (!file.isDirectory())
        {
            if (create)
            {
                file.mkdirs();
                if (!file.isDirectory())
                {
                    throw new IllegalArgumentException("File: " + file.getAbsolutePath() + " , is not a directory");
                }
            }
            else
            {
                throw new IllegalArgumentException("File: " + file.getAbsolutePath() + " , is not a directory");
            }

        }
        d_file = file;
    }

//    @Override
//    public Path to_path()
//    {
//        return d_file.toPath();
//    }

    /**
     * Does not return directories
     */
    @Override
    public List<File> get_local_files()
    {
        final List<File> file_list = CollectionTools.convert_to_list(d_file.listFiles());
        final List<File> local_files = new ArrayList<File>();
        for (final File file : file_list)
        {
            if (!file.isDirectory())
            {
                local_files.add(file);
            }
        }
        return local_files;
    }

    @Override
    public List<File> get_local_files(FileFilter filter)
    {
        final List<File> file_list = CollectionTools.convert_to_list(d_file.listFiles());
        final List<File> local_files = new ArrayList<File>();
        for (final File file : file_list)
        {
            if (!file.isDirectory() && filter.accept(file))
            {
                local_files.add(file);
            }
        }
        return local_files;
    }

    @Override
    public File get_local_file(String file_name)
    {
        return get_local_file(file_name, false);
    }

    @Override
    public File get_local_file(String file_name, boolean create)
    {
        File file = new File(toString() + File.separator + file_name);
        if (file.exists())
        {
            return file;
        }
        else
        {
            if (create)
            {
                FileTools.create_new_file(d_file);
                return file;
            }
            else
            {
                return null;
            }
        }
    }

    public List<String> get_local_file_names()
    {
        final List<File> files = get_local_files();
        final List<String> file_names = new ArrayList<String>();
        for (final File file : files)
        {
            file_names.add(file.getAbsolutePath());
        }
        return file_names;
    }

    @Override
    public IDirectory get_local_directory(String directory_name)
    {
        return get_local_directory(directory_name, false);
    }

    @Override
    public IDirectory get_local_directory(String directory_name, boolean create)
    {
        
        
        File file = new File(toString() + File.separator + directory_name + File.separator);
        if ((file.exists()) && (!file.isDirectory()))
        {
            return null;
        }
        
        if (file.exists())
        {
            return new Directory(file);
        }
        else
        {
            if (create)
            {
                if (file.mkdirs())
                {
                    return new Directory(file);
                }
                else
                {
                    return null;  
                }
            }
            else
            {
                return null;
            }
        }
    }

    /**
     * Return only directories
     */
    @Override
    public List<IDirectory> get_local_directories()
    {
        final List<File> file_list = CollectionTools.convert_to_list(d_file.listFiles());
        final List<IDirectory> local_directories = new ArrayList<IDirectory>();
        for (final File file : file_list)
        {
            if (file.isDirectory())
            {
                local_directories.add(new Directory(file));
            }
        }
        return local_directories;
    }

    @Override
    public List<IDirectory> get_descendants()
    {
        final List<IDirectory> descendants = new ArrayList<IDirectory>();
        get_descendants(descendants, d_file.listFiles());
        return descendants;
    }

    private void get_descendants(final List<IDirectory> descendants, final File [] files)
    {
        for (final File file : files)
        {
            if (file.isDirectory())
            {
                descendants.add(new Directory(file));
                get_descendants(descendants, file.listFiles());
            }
        }
    }

    @Override
    public boolean equals(final Object other)
    {
        if (other instanceof Directory)
        {
            final Directory other_directory = (Directory) other;
            return other_directory.d_file.equals(d_file);
        }
        else
        {
            return false;
        }
    }

    @Override
    public int hashCode()
    {
        return d_file.hashCode();
    }

    @Override
    public boolean exists()
    {
        return d_file.exists();
    }

    public boolean create()
    {
        try
        {
            return d_file.createNewFile();
        }
        catch (final IOException e)
        {
            // TODO Auto-generated catch block
            e.printStackTrace();
            return false;
        }

    }

    @Override
    public String get_absolute_path()
    {
        return d_file.getAbsolutePath();
    }

    @Override
    public String toString()
    {
        return d_file.getAbsolutePath();
    }

    @Override
    public void delete_all()
    {
        //TODO make recursive?
        File [] files = d_file.listFiles();
        for (File file : files)
        {
            file.delete();
        }
    }

    @Override
    public String get_name()
    {
        return d_file.getName();
    }

    @Override
    public File to_file()
    {
        return d_file;
    }

    @Override
    public void move_files_to_directory(List<File> files)
    {
        for (File file : files)
        {
            file.renameTo(get_local_file(file.getName(),true));
        }
        
    }

    public String to_path()
    {
        return d_file.toPath().toString();
    }
}

package com.kozzion.library.core.file;

import java.io.File;
import java.io.FileFilter;
import java.util.List;

public interface IDirectory
{
    public File to_file();

    //public Path to_path();

    public List<IDirectory> get_descendants();

    public List<File> get_local_files();

    public List<File> get_local_files(FileFilter filter);
    
    public boolean exists();

    public String get_absolute_path();

    public List<IDirectory> get_local_directories();

    public void move_files_to_directory(List<File> files);

    public File get_local_file(String string);

    public File get_local_file(String string, boolean create);

    public IDirectory get_local_directory(String string);

    public IDirectory get_local_directory(String string, boolean create);

    public void delete_all();

    public String get_name();

    public String to_path();

}

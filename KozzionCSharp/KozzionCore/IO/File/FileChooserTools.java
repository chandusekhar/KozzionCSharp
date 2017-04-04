package com.kozzion.library.core.file;

import java.awt.Component;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.io.Serializable;

import javax.imageio.ImageIO;
import javax.swing.JFileChooser;
import javax.swing.filechooser.FileFilter;

import com.kozzion.library.core.file.filter.DirectoryFilter;
import com.kozzion.library.core.file.filter.ExtentionFileFilter;
import com.kozzion.library.core.file.filter.ReadableImageFileFilter;
import com.kozzion.library.core.utility.SerializationTools;

public class FileChooserTools
{
    public static BufferedImage promt_image(final Component parent, IDirectory startingpath)
    {
        final JFileChooser file_chooser = new JFileChooser();
        file_chooser.setCurrentDirectory(startingpath.to_file());
        file_chooser.setFileFilter(new ReadableImageFileFilter());
        final int returnVal = file_chooser.showOpenDialog(parent);

        if (returnVal == JFileChooser.APPROVE_OPTION)
        {
            try
            {
                return ImageIO.read(file_chooser.getSelectedFile());
            }
            catch (final IOException e)
            {
                // TODO Auto-generated catch block
                e.printStackTrace();
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public static BufferedImage promt_image(final Component parent)
    {
        final JFileChooser file_chooser = new JFileChooser();
        file_chooser.setFileFilter(new ReadableImageFileFilter());
        final int returnVal = file_chooser.showOpenDialog(parent);

        if (returnVal == JFileChooser.APPROVE_OPTION)
        {
            try
            {
                return ImageIO.read(file_chooser.getSelectedFile());
            }
            catch (final IOException e)
            {
                // TODO Auto-generated catch block
                e.printStackTrace();
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public static File promt_load_file(final Component parent, final String filter_extention)
    {
        return promt_load_file(parent, filter_extention, "Select file");
    }
    
    
    public static File promt_load_file(final Component parent, final String filter_extention, String title)
    {
        final JFileChooser file_chooser = new JFileChooser();
        final ExtentionFileFilter filter = new ExtentionFileFilter();
        filter.addExtension(filter_extention);
        file_chooser.setDialogTitle(title);
        file_chooser.setFileFilter(filter);
        final int returnVal = file_chooser.showOpenDialog(parent);

        if (returnVal == JFileChooser.APPROVE_OPTION)
        {
            return file_chooser.getSelectedFile();
        }
        else
        {
            return null;
        }
    }

    public static File promt_save_file(final Component parent, final String filter_extention)
    {
        final JFileChooser file_chooser = new JFileChooser();
        final ExtentionFileFilter filter = new ExtentionFileFilter();
        filter.addExtension(filter_extention);
        file_chooser.setFileFilter(filter);
        
        final int returnVal = file_chooser.showSaveDialog(parent);

        if (returnVal == JFileChooser.APPROVE_OPTION)
        {
            return file_chooser.getSelectedFile();
        }
        else
        {
            return null;
        }
    }

    public static File promt_file()
    {
        final JFileChooser file_chooser = new JFileChooser();
        final int returnVal = file_chooser.showOpenDialog(null);

        if (returnVal == JFileChooser.APPROVE_OPTION)
        {
            return file_chooser.getSelectedFile();
        }
        else
        {
            return null;
        }
    }

    public static void save_object_to_file(final Component parent, final String filter_extention, final Serializable to_save)
    {
        final JFileChooser file_chooser = new JFileChooser();
        final ExtentionFileFilter filter = new ExtentionFileFilter();
        filter.addExtension(filter_extention);
        file_chooser.setFileFilter(filter);
        final int returnVal = file_chooser.showSaveDialog(parent);

        if (returnVal == JFileChooser.APPROVE_OPTION)
        {
            SerializationTools.serializable_to_file(file_chooser.getSelectedFile(), to_save);
        }
    }

    public static Directory promt_directory(final Component parent, final String approve_button_text)
    {
        final JFileChooser file_chooser = new JFileChooser();
        final FileFilter filter = new DirectoryFilter();
        file_chooser.setFileFilter(filter);
        file_chooser.setAcceptAllFileFilterUsed(false);
        file_chooser.setFileSelectionMode(JFileChooser.DIRECTORIES_ONLY);
        final int returnVal = file_chooser.showDialog(parent, approve_button_text);

        if (returnVal == JFileChooser.APPROVE_OPTION)
        {
            return new Directory(file_chooser.getSelectedFile());
        }
        else
        {
            return null;
        }
    }
    
    public static Directory promt_directory(final Component parent, final String approve_button_text, File current_directory)
    {
        final JFileChooser file_chooser = new JFileChooser();
        file_chooser.setCurrentDirectory(current_directory);
        final FileFilter filter = new DirectoryFilter();
        file_chooser.setFileFilter(filter);
        file_chooser.setAcceptAllFileFilterUsed(false);
        file_chooser.setFileSelectionMode(JFileChooser.DIRECTORIES_ONLY);
        final int returnVal = file_chooser.showDialog(parent, approve_button_text);

        if (returnVal == JFileChooser.APPROVE_OPTION)
        {
            return new Directory(file_chooser.getSelectedFile());
        }
        else
        {
            return null;
        }
    }

}

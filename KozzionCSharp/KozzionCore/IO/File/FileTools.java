package com.kozzion.library.core.file;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.List;
import java.util.Scanner;
import java.util.Vector;
import java.util.zip.ZipEntry;
import java.util.zip.ZipFile;

import org.apache.commons.io.FileUtils;
import org.apache.commons.io.IOUtils;

import com.kozzion.library.core.datastructure.Tuple2;
import com.kozzion.library.core.utility.CollectionTools;
import com.kozzion.library.core.utility.exeptions.FileToLargeExeption;

public class FileTools
{

    public static List<String> read_file(
        final File file)
        throws FileNotFoundException
    {
        return read_stream(new FileInputStream(file));
    }

    public static List<String> read_file_lines(
        File file,
        int line_offset,
        int line_count)
        throws FileNotFoundException
    {
        return read_stream_lines(new FileInputStream(file), line_offset, line_count);
    }

    public static List<String> read_stream(
        final InputStream input_stream)
    {
        final List<String> lines = new ArrayList<String>();
        final Scanner scanner = new Scanner(input_stream);

        while (scanner.hasNext())
        {
            lines.add(scanner.nextLine());
        }
        scanner.close();
        return lines;
    }

    public static List<String> read_stream_lines(
        final InputStream input_stream,
        int lines_offset,
        int lines_count)
    {
        final List<String> lines = new ArrayList<String>();
        final Scanner scanner = new Scanner(input_stream);
        int line_index = 0;
        while (scanner.hasNext() && lines.size() < lines_count)
        {
            if (lines_offset <= line_index)
            {
                lines.add(scanner.nextLine());
            }
            line_index++;
        }
        scanner.close();
        return lines;
    }

    public static void write_to_file(
        final File file,
        final List<String> lines)
    {
        try
        {
            if (!file.exists())
            {
                create_new_file(file);
            }
            final FileWriter writer = new FileWriter(file);
            for (final String string : lines)
            {
                writer.write(string + System.getProperty("line.separator"));
            }
            writer.close();
        }
        catch (final IOException e)
        {
            System.err.println("FileTools::writeToFile ERROR: IOException on file " + file.getAbsolutePath());
            e.printStackTrace();
        }
    }

    public static void write_bytes_to_file(
        final File file,
        final byte [] bytes_to_write)
    {
        try
        {
            if (!file.exists())
            {
                create_new_file(file);
            }
            final FileOutputStream stream = new FileOutputStream(file);
            stream.write(bytes_to_write);
            stream.close();
        }
        catch (final IOException e)
        {
            System.err.println("FileTools::writeToFile ERROR: IOException on file " + file.getAbsolutePath());
            e.printStackTrace();
        }

    }

    public static byte [] file_to_bytes(
        final File file)
    {
        try
        {
            if (!file.exists())
            {
                return null;
            }

            final long long_size = file.length();
            if (Integer.MAX_VALUE < long_size)
            {
                throw new IOException("FileTools::file_to_bytes : Error File size to big for array!!");
            }

            final int int_size = (int) long_size;
            final byte [] bytes = new byte [int_size];
            final FileInputStream fileinputstream = new FileInputStream(file);
            fileinputstream.read(bytes);
            fileinputstream.close();
            return bytes;
        }
        catch (final IOException e)
        {
            System.err.println("FileTools::writeToFile ERROR: IOException on file " + file.getAbsolutePath());
            e.printStackTrace();
        }
        return null;
    }

    public static void appendToFile(
        final File file,
        final List<String> lines)
    {
        if (!file.exists())
        {
            write_to_file(file, lines);
        }
        else
        {
            try
            {
                final List<String> filecontends = read_file(file);
                filecontends.addAll(lines);

                final FileWriter writer = new FileWriter(file);

                for (final String string : filecontends)
                {
                    writer.write(string + System.getProperty("line.separator"));
                }

                writer.close();

            }
            catch (final IOException e)
            {
                System.err.println("FileTools::writeToFile ERROR: IOException on file " + file.getAbsolutePath());
                e.printStackTrace();
            }
        }

    }

    public static void appendToFile(
        final File file,
        final String line)
    {
        final List<String> lines = new Vector<String>();
        lines.add(line);
        appendToFile(file, lines);
    }

    public static String get_extention(
        final String filename)
    {
        final int extentionindex = filename.lastIndexOf('.');
        if (extentionindex == -1)
        {
            return "";
        }
        return filename.substring(extentionindex + 1);
    }

    public static boolean extention_is(
        final String filename,
        final String extention)
    {
        if (get_extention(filename).equals(extention))
        {
            return true;
        }
        return false;
    }

    public static String remove_extention(
        final String filename)
    {
        final int extentionindex = filename.lastIndexOf('.');
        if (extentionindex == -1)
        {
            return filename;
        }
        return filename.substring(0, extentionindex);
    }

    public static void create_new_file(
        final File file)
    {
        final String path = file.getPath();
        String regex = File.separator;
        if (regex.equals("\\"))
        {
            regex += "\\";
        }
        final String [] parts = path.split(regex);
        try
        {
            if (parts.length == 1)
            {
                file.createNewFile();
            }
            else
            {
                String accumulator = parts[0];
                for (int index = 1; index < (parts.length - 1); index++)
                {
                    accumulator += File.separator + parts[index];

                }
                final File dir = new File(accumulator);
                dir.mkdirs();
                file.createNewFile();
            }
        }
        catch (final IOException e)
        {
            System.err.println("FileTools::createNewFile ERROR: IOException on file " + file.getAbsolutePath());
            e.printStackTrace();
        }

    }

    public static File copy_with_new_extention(
        final File backing_file,
        final String new_extention)
    {
        final String old_file_name = backing_file.getPath();
        final String old_extention = get_extention(old_file_name);
        final String new_file_name = old_file_name.substring(0, old_file_name.length() - old_extention.length()) + new_extention;
        return new File(new_file_name);
    }

    public void unzip_all(
        final File to_unzip)
    {

    }

    public List<Tuple2<String, byte []>> unzip_file_to_bytes(
        final File to_unzip)
    {
        try
        {
            final List<Tuple2<String, byte []>> unzipped_files = new Vector<Tuple2<String, byte []>>();
            final ZipFile zip_file = new ZipFile(to_unzip);
            final Enumeration<? extends ZipEntry> enumeration = zip_file.entries();
            while (enumeration.hasMoreElements())
            {
                final ZipEntry zip_entry = enumeration.nextElement();
                System.out.println("Extracting: " + zip_entry);
                unzipped_files.add(new Tuple2<String, byte []>(zip_entry.getName(), unzip_entry_to_bytes(zip_file, zip_entry)));
            }
            return unzipped_files;
        }
        catch (final Exception e)
        {
            e.printStackTrace();
        }
        return null;
    }

    private byte [] unzip_entry_to_bytes(
        final ZipFile zip_file,
        final ZipEntry zip_entry)
        throws IOException,
        FileToLargeExeption
    {
        final long long_size = zip_entry.getSize();
        byte [] unzipped_bytes = null;

        if (Integer.MAX_VALUE < long_size)
        {
            throw new FileToLargeExeption();
        }
        else
        {
            if (long_size == -1)
            {
                unzipped_bytes = CollectionTools.convert_to_array_byte(zip_file.getInputStream(zip_entry));
            }
            else
            {
                final int int_size = (int) long_size;
                unzipped_bytes = CollectionTools.convert_to_array_byte(zip_file.getInputStream(zip_entry), int_size);
            }
        }
        return unzipped_bytes;
    }

    public static void main(
        final String argv[])
    {

    }

    @Deprecated
    public static void move_file_to_directory(
        final File file,
        final File directory)
    {
        if (!directory.isDirectory())
        {
            throw new IllegalArgumentException("Second argument" + directory.getName() + " is not a directory.");
        }
        directory.getAbsolutePath();
        System.out.println(directory.getAbsolutePath());
        final File new_file = new File(directory + File.separator + file.getName());
        System.out.println(new_file.getAbsolutePath());
        if (file.renameTo(new_file))
        {
            System.out.println("Succes");
        }
        else
        {
            System.out.println("Fail");
        }

    }

}

package com.kozzion.library.core.file.csv;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.List;
import java.util.Vector;

import com.kozzion.library.core.file.FileTools;
import com.kozzion.library.core.file.csv.CSVReader.Delimiter;
import com.kozzion.library.core.utility.ArrayTools;
import com.kozzion.library.core.utility.CollectionTools;

public class CSVWriter
{
    public static void writeCSVFile(final File file, final String [][] content)
    {
        final List<String> lines = toLines(content, Delimiter.TAB, Delimiter.DOUBLEQOUTE);
        FileTools.write_to_file(file, lines);

    }

    public static void writeCSVFile(final File file, final String [][] content, final Delimiter fieldseparator,
        final Delimiter stringseparator)
    {
        final List<String> lines = toLines(content, fieldseparator, stringseparator);
        FileTools.write_to_file(file, lines);
    }

    public static void append_to_csv_file(final File csvfile, final String [] newcontent, final Delimiter fieldseparator,
        final Delimiter stringseparator) throws FileNotFoundException
    {
        if (!csvfile.exists())
        {
            writeCSVFile(csvfile, newcontent);
        }
        else
        {
            final String [][] oldcontent = CSVReader.read_csv_file(csvfile, fieldseparator, stringseparator);
            final String [][] content = ArrayTools.append(newcontent, oldcontent);
            writeCSVFile(csvfile, content);
        }
    }

    private static void writeCSVFile(final File csvfile, final String [] array)
    {
        final String [][] table = new String [1] [array.length];
        ArrayTools.copy_fill(array, table, 0, 0);
        writeCSVFile(csvfile, table);

    }

    public static List<String> toLines(final String [][] content, final Delimiter fieldseparator, final Delimiter stringseparator)
    {
        final List<String> lines = new Vector<String>();
        for (final String [] element : content)
        {
            lines.add(toLine(element, fieldseparator, stringseparator));
        }
        return lines;
    }

    public static String toLine(final String [] content, final Delimiter fieldseparator, final Delimiter stringseparator)
    {
        final String fieldseparatorstring = CSVReader.get_delimiter(fieldseparator);
        final String stringseparatorstring = CSVReader.get_delimiter(stringseparator);

        String line = makefield(content[0], stringseparatorstring);
        for (int index = 1; index < content.length; index++)
        {
            line += fieldseparatorstring + makefield(content[index], stringseparatorstring);
        }
        return line;
    }

    public static String makefield(final String value, final String stringseparator)
    {
        if (isNumber(value))
        {
            return value;
        }
        else
        {
            return stringseparator + nullFilter(value) + stringseparator;
        }
    }

    public static String nullFilter(final String value)
    {
        if (value == null)
        {
            return "";
        }
        return value;
    }

    public static boolean isNumber(final String value)
    {
        if (value == null)
        {
            return false;
        }
        if (value.matches("((-|\\+)?[0-9]+(\\.[0-9]+)?)+"))
        {
            return true;
        }
        return false;
    }
}

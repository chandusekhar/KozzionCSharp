package com.kozzion.library.core.file.vcf;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.HashSet;
import java.util.List;
import java.util.Vector;

import com.kozzion.library.core.file.FileTools;

public class VCFTools
{

    private static List<String> get_fields(final List<VCFRecord> contacts)
    {
        final HashSet<String> fieldset = new HashSet<String>();
        for (int index = 0; index < contacts.size(); ++index)
        {
            fieldset.addAll(contacts.get(index).get_fields());
        }
        final List<String> fields = new Vector<String>();
        fields.addAll(fieldset);
        return fields;
    }

    public static List<VCFRecord> file_to_vcf_records(final File vcf_file) throws FileNotFoundException
    {
        final List<String> lines = FileTools.read_file(vcf_file);
        return lines_to_vcf_records(lines);

    }

    private static List<VCFRecord> lines_to_vcf_records(final List<String> lines)
    {
        final List<VCFRecord> vcf_records = new Vector<VCFRecord>();
        List<String> templines = null;
        boolean incard = false;
        for (int index = 0; index < lines.size(); ++index)
        {
            if (incard)
            {
                if (lines.get(index).equals("END:VCARD"))
                {
                    vcf_records.add(new VCFRecord(templines));
                    incard = false;
                }
                else
                {
                    templines.add(lines.get(index));
                }
            }
            else
                if (lines.get(index).equals("BEGIN:VCARD"))
                {
                    incard = true;
                    templines = new Vector<String>();
                }
        }
        return vcf_records;
    }

}

using System;
using System.Collections.Generic;
using System.IO;
using KozzionCore.Tools;

namespace KozzionCore.IO.File
{
    public class FileTypeMatcher
    {
        private List<FileTypeDescriptor> file_type_descriptors;
        public int RequiredHeaderSize;

        public FileTypeMatcher(IList<FileTypeDescriptor> file_type_descriptors) 
        {
            this.file_type_descriptors = new List<FileTypeDescriptor>(file_type_descriptors);
            foreach (FileTypeDescriptor file_type_descriptor in file_type_descriptors)
            {
                RequiredHeaderSize = Math.Max(RequiredHeaderSize, file_type_descriptor.RequiredHeaderSize);
            }
        }
        public void AddDescriptor(FileTypeDescriptor descriptor)
        {
            this.file_type_descriptors.Add(descriptor);
            this.RequiredHeaderSize = Math.Max(RequiredHeaderSize, descriptor.RequiredHeaderSize);
        }

        public FileTypeMatcher() 
            :this (GetDefaultDescriptors())
        {

        }

        public FileTypeMatcher(FileTypeDescriptor file_type_descriptor)
            : this(ToolsCollection.ConvertToList(file_type_descriptor))
        {

        }


        public List<FileTypeDescriptor> MatchFileType(byte[] header)
        {
            return null;
        }

        public List<FileTypeDescriptor> MatchFileType(string file_path)
        {
            List<FileTypeDescriptor> matching_descriptors = new List<FileTypeDescriptor>();
            byte[] buffer = null;
            using (FileStream file_stream = new FileStream(file_path, FileMode.Open, FileAccess.Read))
            {

                buffer = new byte[Math.Min(file_stream.Length, RequiredHeaderSize)];
                file_stream.Read(buffer, 0, buffer.Length);
                file_stream.Close();
            }

            foreach (FileTypeDescriptor file_type_descriptor in file_type_descriptors)
            {
                if (file_type_descriptor.IsOfType(buffer))
                {
                    matching_descriptors.Add(file_type_descriptor);
                }
            }
            return matching_descriptors;
        }

        public bool MatchFileType(string file_path, string file_type)
        {
            List<FileTypeDescriptor> descriptors = MatchFileType(file_path);
            foreach (FileTypeDescriptor descriptor in descriptors)
            {
                if (descriptor.Tag.Equals(file_type))
                {
                    return true;
                }
            }
            return false;        
        }

        private static IList<FileTypeDescriptor> GetDefaultDescriptors()
        {
            List<FileTypeDescriptor> file_type_descriptors = new List<FileTypeDescriptor>();
            file_type_descriptors.Add(new FileTypeDescriptor(
                "DICOM",
                "Dicom file",
                128,
                "DICM"));

            file_type_descriptors.Add(new FileTypeDescriptor(
                "PNGIM",
                "Portable Network Graphics image",
                0,
                ToolsBinary.HexStringToByteArray("89504E470D0A1A0A")));

            file_type_descriptors.Add(new FileTypeDescriptor(
                "TIFFL",
                "Tagged Image File Format encoded in little endian format image",
                0,
                ToolsBinary.HexStringToByteArray("49492A00")));

            file_type_descriptors.Add(new FileTypeDescriptor(
                "TIFFB",
                "Tagged Image File Format encoded in little big format image",
                0,
                ToolsBinary.HexStringToByteArray("4D4D002A")));

            file_type_descriptors.Add(new FileTypeDescriptor(
                 "JPGIM",
                "Joint Photographic Experts Group image",
                0,
                ToolsBinary.HexStringToByteArray("FFD8FFE0")));

            file_type_descriptors.Add(new FileTypeDescriptor(
                 "PDFDO",
                "Portable Document Format",
                0,
                ToolsBinary.HexStringToByteArray("25504446")));

            file_type_descriptors.Add(new FileTypeDescriptor(
                "GIF7A",
                "Graphics Interchange Format 87a image",
                 0,
                 ToolsBinary.HexStringToByteArray("474946383761")));


            file_type_descriptors.Add(new FileTypeDescriptor(
                "GIF9A",
                "Graphics Interchange Format 89a image",
                 0,
                 ToolsBinary.HexStringToByteArray("474946383961")));


            file_type_descriptors.Add(new FileTypeDescriptor(
                "MSODO",
                "Microsoft Office document",
                0,
                ToolsBinary.HexStringToByteArray("D0CF11E0A1B11AE1")));

            file_type_descriptors.Add(new FileTypeDescriptor(
                "MSOEX",
                "Excel spreadsheet subheader (MS Office)",
                512,
                ToolsBinary.HexStringToByteArray("0908100000060500")));

            file_type_descriptors.Add(new FileTypeDescriptor(
                "MSOXF",
                "Microsoft Office Open XML Format ",
                0,
                ToolsBinary.HexStringToByteArray("504B030414000600")));
            //Trailer: Look for 50 4B 05 06 (PK..) followed by 18 additional bytes at the end of the file.

            return file_type_descriptors;
        }
    }
}

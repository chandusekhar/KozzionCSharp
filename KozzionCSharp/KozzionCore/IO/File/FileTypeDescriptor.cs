using System;
using System.IO;
using KozzionCore.Tools;

namespace KozzionCore.IO.File
{
    public class FileTypeDescriptor
    {
        private string[] posible_extensions;
        private byte[] signature;

        public bool IsHeaderType { get; private set; }
        public string Tag { get; private set; }
        public string Description { get; private set;}
        public string DefaultExtension { get; private set;}
        public string ascii_signature { get; private set; }
        public int SignatureOffset { get; private set; }

        public string[] PosibleExtensions { get { return ToolsCollection.Copy(posible_extensions); } }
        public byte[] Signature { get { return ToolsCollection.Copy(signature); } }

        public int RequiredHeaderSize { get { return this.SignatureOffset + this.Signature.Length; } }

        public FileTypeDescriptor(string tag, string description, int signature_offset, byte[] signature)
        {
            this.Tag = tag;
            this.Description = description;
            this.SignatureOffset = signature_offset;
            this.signature = ToolsCollection.Copy(signature);
            this.ascii_signature = ToolsBinary.ByteArrayToRegularString(signature);
            this.IsHeaderType = true;
        }

        public FileTypeDescriptor(string tag, string description, int signature_offset, byte[] signature, string default_extension)
        {
            this.Tag = tag;
            this.Description = description;
            this.SignatureOffset = signature_offset;
            this.signature = ToolsCollection.Copy(signature);
            this.ascii_signature = ToolsBinary.ByteArrayToRegularString(signature);
            this.IsHeaderType = true;
        }

        public FileTypeDescriptor(string tag, string description, int signature_offset,  string ascii_signature)
        {
            this.Tag = tag;
            this.Description = description;
            this.SignatureOffset = signature_offset;
            this.signature = ToolsBinary.RegularStringToByteArrayASCII(ascii_signature);
            this.ascii_signature = ascii_signature;
            this.IsHeaderType = true;
        }


        public FileTypeDescriptor(string tag, string description, int signature_offset, string ascii_signature, string default_extension)
        {
            this.Tag = tag;
            this.Description = description;
            this.SignatureOffset = signature_offset;
            this.signature = ToolsBinary.RegularStringToByteArrayASCII(ascii_signature);
            this.ascii_signature = ascii_signature;
            this.IsHeaderType = true;
            this.DefaultExtension = default_extension;
            this.posible_extensions = new string[1];
            this.posible_extensions[0] = default_extension;
        }

        public bool IsOfType(byte [] header) 
        {
            if (!IsHeaderType)
            {
                return false;
            }

            if (header.Length < RequiredHeaderSize)
            {
                return false;
            }

            for (int index = 0; index < signature.Length; index++)
            {
                if (header[index + SignatureOffset] != signature[index])
                {
                    return false;
                }                
            }
            return true;
        }

        public bool IsOfType(string file_path)
        {
            byte[] buffer = null;
            using (FileStream file_stream = new FileStream(file_path, FileMode.Open, FileAccess.Read))
            {

                buffer = new byte[Math.Min(file_stream.Length, RequiredHeaderSize)];
                file_stream.Read(buffer, 0, buffer.Length);
                file_stream.Close();
            }
            return IsOfType(buffer);
        }
    }
}

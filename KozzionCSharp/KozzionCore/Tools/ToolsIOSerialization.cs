using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ToolsIOSerialization
{

    public static void SerializeToFile(string file_path, object to_serialize)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(file_path));
		using (Stream stream = File.Open(file_path, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, to_serialize);
        }
    }

    public static Type SerializeFromFile<Type>(string file_path)
    {
        if (!File.Exists(file_path))
        {
			throw new FileNotFoundException(file_path);
        }
		Type to_serialize = default(Type);
        using (Stream stream = File.Open(file_path, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            object untyped = formatter.Deserialize(stream);
            to_serialize = (Type)untyped;
        }
        return to_serialize;
    }

    public static Type SerializeFromBytes<Type>(byte[] bytes)
    {
        Type to_serialize = default(Type);
        using (Stream stream = new MemoryStream(bytes))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            to_serialize = (Type)formatter.Deserialize(stream);
        }
        return to_serialize;
    }

    //public static <Type extends Serializable> Type stream_to_serializable(final FileInputStream fileinputstream)
    //{
    //    try
    //    {
    //        final ObjectInputStream objectinputstream = new ObjectInputStream(fileinputstream);
    //        final Object object = objectinputstream.readObject();
    //        objectinputstream.close();
    //        return (Type) object;

    //    }
    //    catch (final IOException e)
    //    {
    //        System.err.println("FileTools::readFromStream ERROR: IOException on fileinputstream");
    //        e.printStackTrace();
    //    }
    //    catch (final ClassNotFoundException e)
    //    {
    //        // TODO Auto-generated catch block
    //        e.printStackTrace();
    //    }
    //    return null;
    //}

    //public static <Type extends Serializable> Type bytes_to_serializable(final byte [] bytes)
    //{
    //    final ByteArrayInputStream bytearrayinputstream = new ByteArrayInputStream(bytes);
    //    ObjectInputStream objectinputstream = null;
    //    Type object = null;
    //    try
    //    {
    //        objectinputstream = new ObjectInputStream(bytearrayinputstream);
    //        object = (Type) objectinputstream.readObject(); // TODO double
    //                                                        // dispatch?
    //    }
    //    catch (final IOException e)
    //    {
    //        System.err.println("CommunicationTools::bytesToSerializable Error: IOException while reading object");
    //        e.printStackTrace();
    //        return null;

    //    }
    //    catch (final ClassNotFoundException e)
    //    {
    //        System.err.println("CommunicationTools::bytesToSerializable Error: Class not found in buffer");
    //        e.printStackTrace();
    //    }
    //    return object;
    //}

    //public static <Type extends Serializable> Type byte_buffer_to_serializable(final ByteBuffer initial_message)
    //{
    //    final ByteArrayInputStream bytearrayinputstream = new ByteArrayInputStream(initial_message.array());
    //    ObjectInputStream objectinputstream = null;
    //    Type object = null;
    //    try
    //    {
    //        objectinputstream = new ObjectInputStream(bytearrayinputstream);
    //        object = (Type) objectinputstream.readObject(); // TODO double
    //                                                        // dispatch?
    //    }
    //    catch (final IOException e)
    //    {
    //        System.err.println("CommunicationTools::bytesToSerializable Error: IOException while reading object");
    //        e.printStackTrace();
    //        return null;

    //    }
    //    catch (final ClassNotFoundException e)
    //    {
    //        System.err.println("CommunicationTools::bytesToSerializable Error: Class not found in buffer");
    //        e.printStackTrace();
    //    }
    //    return object;
    //}

    //public static byte [] convert_long_to_byte_array(final long value)
    //{
    //    final ByteArrayOutputStream output_stream = new ByteArrayOutputStream(8);
    //    final DataOutputStream data_stream = new DataOutputStream(output_stream);
    //    try
    //    {
    //        data_stream.writeLong(value);
    //    }
    //    catch (final IOException e)
    //    {
    //        throw new IllegalStateException("This exception should not be possible");
    //    }
    //    return output_stream.toByteArray();
    //}

    //public static long convert_byte_array_to_long(final byte [] array)
    //{
    //    final ByteArrayInputStream input_stream = new ByteArrayInputStream(array);
    //    final DataInputStream data_stream = new DataInputStream(input_stream);
    //    try
    //    {
    //        return data_stream.readLong();
    //    }
    //    catch (final IOException e)
    //    {
    //        throw new IllegalStateException("This exception should not be possible");
    //    }
    //}


}

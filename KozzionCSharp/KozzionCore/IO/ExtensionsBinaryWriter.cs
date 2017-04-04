using KozzionCore.Tools;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace System
{
    public static class ExtensionsBinary
    {

        public static void WriteEnum<EnumType>(this BinaryWriter writer, EnumType value)
        {
            writer.Write(ToolsEnum.EnumToInt32(value));
        }

        public static EnumType ReadEnum<EnumType>(this BinaryReader reader)
        {
            return ToolsEnum.Int32ToEnum<EnumType>(reader.ReadInt32());
        }

        public static void WriteBoolOtherEndian(this BinaryWriter writer, bool value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            writer.Write(bytes);
        }

        public static bool ReadBoolOtherEndian(this BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(1);
            Array.Reverse(bytes); //TODO does this even do anything?
            return BitConverter.ToBoolean(bytes, 0);
        }

        public static void WriteUInt16OtherEndian(this BinaryWriter writer, ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            writer.Write(bytes);
        }

        public static ushort ReadUInt16OtherEndian(this BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(2);
            Array.Reverse(bytes);
            return  BitConverter.ToUInt16(bytes, 0);
        }

        public static void WriteUInt32OtherEndian(this BinaryWriter writer, uint value)
        {
            byte[] bytes =  BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            writer.Write(bytes);
        }


        public static uint ReadUInt32OtherEndian(this BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(4);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }


        public static int ReadInt32OtherEndian(this BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(4);
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }


        public static ulong ReadUInt64OtherEndian(this BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(8);
            Array.Reverse(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }


        public static long ReadInt64OtherEndian(this BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(8);
            Array.Reverse(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }


        public static void WriteFloat32OtherEndian(this BinaryWriter writer, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            writer.Write(bytes);
        }


        public static float ReadFloat32OtherEndian(this BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(4);
            Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        public static void WriteFloat64OtherEndian(this BinaryWriter writer, double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            writer.Write(bytes);
        }

        public static double ReadFloat64OtherEndian(this BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(8);
            Array.Reverse(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        public static void WriteBoolArray1DOtherEndian(this BinaryWriter writer, bool[] values)
        {
            for (int index = 0; index < values.Length; index++)
            {
                WriteBoolOtherEndian(writer, values[index]);
            }
        }

        public static void WriteUInt16Array1DOtherEndian(this BinaryWriter writer, ushort [] values)
        {
            for (int index = 0; index < values.Length; index++)
            {
                WriteUInt16OtherEndian(writer, values[index]);
            }
        }
        

        public static bool[] ReadBoolArray1DOtherEndian(this BinaryReader reader, int count)
        {
            bool[] array = new bool[count];
            for (int index = 0; index < count; index++)
            {
                array[index] = reader.ReadBoolOtherEndian();
            }
            return array;
        }

        public static ushort[] ReadUInt16Array1DOtherEndian(this BinaryReader reader, int count)
        {
            ushort[] array = new ushort[count];
            for (int index = 0; index < count; index++)
            {
                array[index] = reader.ReadUInt16OtherEndian();
            }
            return array;
        }

        public static void WriteUInt32Array1DOtherEndian(this BinaryWriter writer, uint[] values)
        {
            for (int index = 0; index < values.Length; index++)
            {
                WriteUInt32OtherEndian(writer, values[index]);
            }
        }

        public static uint[] ReadUInt32Array1DOtherEndian(this BinaryReader reader, int count)
        {
            uint[] array = new uint[count];
            for (int index = 0; index < count; index++)
            {
                array[index] = reader.ReadUInt32OtherEndian();
            }
            return array;
        }

        public static void WriteDateTimeUTC(this BinaryWriter writer, DateTimeUTC date_time)
        {
            ulong date_time_ulong = (ulong)(date_time.Ticks | ((long)date_time.Kind) << 62);
            writer.Write(date_time_ulong);

        }

        public static DateTimeUTC ReadDateTimeUTC(this BinaryReader reader)
        {
            ulong date_time_ulong = reader.ReadUInt64();
            long ticks = (long)(date_time_ulong & 0x3FFFFFFFFFFFFFFF);
            DateTimeKind kind = (DateTimeKind)(date_time_ulong >> 62);
            return new DateTimeUTC(new DateTime(ticks, kind));
        }


        public static void WriteDateTime(this BinaryWriter writer, DateTime date_time)
        {
            ulong date_time_ulong = (ulong)(date_time.Ticks | ((long)date_time.Kind) << 62);
            writer.Write(date_time_ulong);
  
        }

        public static DateTime ReadDateTime(this BinaryReader reader)
        {
            ulong date_time_ulong = reader.ReadUInt64();
            long ticks = (long)(date_time_ulong & 0x3FFFFFFFFFFFFFFF);
            DateTimeKind kind = (DateTimeKind)(date_time_ulong >> 62);
            return new DateTime(ticks, kind);
        }


        public static void WriteFloat32Array1DOtherEndian(this BinaryWriter writer, float[] values)
        {
            for (int index = 0; index < values.Length; index++)
            {
                WriteFloat32OtherEndian(writer, values[index]);
            }
        }

        public static float[] ReadFloat32Array1DOtherEndian(this BinaryReader reader, int count)
        {
            float[] array = new float[count];
            for (int index = 0; index < count; index++)
            {
                array[index] = reader.ReadFloat32OtherEndian();
            }
            return array;
        }

        public static void WriteFloat64Array1DOtherEndian(this BinaryWriter writer, double[] values)
        {
            for (int index = 0; index < values.Length; index++)
            {
                WriteFloat64OtherEndian(writer, values[index]);
            }
        }

        public static double[] ReadFloat64Array1DOtherEndian(this BinaryReader reader, int count)
        {
            double[] array = new double[count];
            for (int index = 0; index < count; index++)
            {
                array[index] = reader.ReadFloat64OtherEndian();
            }
            return array;
        }


        public static float[] ReadFloat32Array1D(this BinaryReader reader, int count)
        {
            float[] array = new float[count];
            for (int index = 0; index < count; index++)
            {
                array[index] = reader.ReadSingle();
            }
            return array;
        }

        // arrays
        public static void WriteByteArray1D(this BinaryWriter writer, IList<byte> list)
        {
            writer.Write(list.Count);
            for (int index = 0; index < list.Count; index++)
            {
                writer.Write(list[index]);
            }
        }

    

        public static byte[] ReadByteArray1D(this BinaryReader reader)
        {

            int size_0 = reader.ReadInt32();
            byte[] array = new byte[size_0];
            for (int index = 0; index < size_0; index++)
            {
                array[index] = reader.ReadByte();
            }
            return array;
        }

        public static void Write(this BinaryWriter writer, byte[,] array)
        {
            writer.Write(array.GetLength(0));
            writer.Write(array.GetLength(1));
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array.GetLength(1); index_1++)
                {
                    writer.Write(array[index_0, index_1]);
                }
            }
        }

        public static byte[,] ReadByteArray2D(this BinaryReader reader)
        {
            int size_0 = reader.ReadInt32();
            int size_1 = reader.ReadInt32();
            byte[,] array = new byte[size_0, size_1];
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array.GetLength(1); index_1++)
                {
                    array[index_0, index_1] = reader.ReadByte();
                }
            }
            return array;
        }

        public static void Write(this BinaryWriter writer, IList<int> list)
        {
            writer.Write(list.Count);
            for (int index = 0; index < list.Count; index++)
            {
                writer.Write(list[index]);
            }
        }

        public static int[] ReadInt32Array1D(this BinaryReader reader)
        {

            int size_0 = reader.ReadInt32();
            int[] array = new int[size_0];
            for (int index = 0; index < size_0; index++)
            {
                array[index] = reader.ReadInt32();
            }
            return array;
        }

        public static void Write(this BinaryWriter writer, int[,] array)
        {
            writer.Write(array.GetLength(0));
            writer.Write(array.GetLength(1));
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array.GetLength(1); index_1++)
                {
                    writer.Write(array[index_0, index_1]);
                }
            }
        }

        public static int[,] ReadInt32Array2D(this BinaryReader reader)
        {
            int size_0 = reader.ReadInt32();
            int size_1 = reader.ReadInt32();
            int[,] array = new int[size_0, size_1];
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array.GetLength(1); index_1++)
                {
                    array[index_0, index_1] = reader.ReadInt32();
                }
            }
            return array;
        }




        public static void Write(this BinaryWriter writer, IList<bool> list)
        {
            writer.Write(list.Count);
            for (int index = 0; index < list.Count; index++)
            {
                writer.Write(list[index]);
            }
        }

        public static bool[] ReadBooleanArray1D(this BinaryReader reader)
        {

            int size_0 = reader.ReadInt32();
            bool[] array = new bool[size_0];
            for (int index = 0; index < size_0; index++)
            {
                array[index] = reader.ReadBoolean();
            }
            return array;
        }

        public static void Write(this BinaryWriter writer, bool[,] array)
        {
            writer.Write(array.GetLength(0));
            writer.Write(array.GetLength(1));
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array.GetLength(1); index_1++)
                {
                    writer.Write(array[index_0, index_1]);
                }
            }
        }

        public static bool[,] ReadBooleanArray2D(this BinaryReader reader)
        {
            int size_0 = reader.ReadInt32();
            int size_1 = reader.ReadInt32();
            bool[,] array = new bool[size_0, size_1];
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array.GetLength(1); index_1++)
                {
                    array[index_0, index_1] = reader.ReadBoolean();
                }
            }
            return array;
        }





        public static void Write(this BinaryWriter writer, IList<float> list)
        {
            writer.Write(list.Count);
            for (int index = 0; index < list.Count; index++)
            {
                writer.Write(list[index]);
            }
        }

        public static float[] ReadFloat32Array1D(this BinaryReader reader)
        {

            int size_0 = reader.ReadInt32();
            float[] array = new float[size_0];
            for (int index = 0; index < size_0; index++)
            {
                array[index] = reader.ReadSingle();
            }
            return array;
        }

        public static void Write(this BinaryWriter writer, float[,] array)
        {
            writer.Write(array.GetLength(0));
            writer.Write(array.GetLength(1));
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array.GetLength(1); index_1++)
                {
                    writer.Write(array[index_0, index_1]);
                }
            }
        }

        public static float[,] ReadFloat32Array2D(this BinaryReader reader)
        {
            int size_0 = reader.ReadInt32();
            int size_1 = reader.ReadInt32();
            float[,] array = new float[size_0, size_1];
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array.GetLength(1); index_1++)
                {
                    array[index_0, index_1] = reader.ReadSingle();
                }
            }
            return array;
        }



        public static void Write(this BinaryWriter writer, IList<double> list)
        {
            writer.Write(list.Count);
            for (int index = 0; index < list.Count; index++)
            {
                writer.Write(list[index]);
            }
        }

        public static double[] ReadFloat64Array1D(this BinaryReader reader)
        {

            int size_0 = reader.ReadInt32();
            double[] array = new double[size_0];
            for (int index = 0; index < size_0; index++)
            {
                array[index] = reader.ReadDouble();
            }
            return array;
        }

        public static void Write(this BinaryWriter writer, double[,] array)
        {
            writer.Write(array.GetLength(0));
            writer.Write(array.GetLength(1));
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array.GetLength(1); index_1++)
                {
                    writer.Write(array[index_0, index_1]);
                }
            }
        }

        public static double[,] ReadFloat64Array2D(this BinaryReader reader)
        {
            int size_0 = reader.ReadInt32();
            int size_1 = reader.ReadInt32();
            double[,] array = new double[size_0, size_1];
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array.GetLength(1); index_1++)
                {
                    array[index_0, index_1] = reader.ReadDouble();
                }
            }
            return array;
        }



        public static void WriteStringArray1D(this BinaryWriter writer, IList<string> list)
        {
            writer.Write(list.Count);
            for (int index = 0; index < list.Count; index++)
            {
                writer.Write(list[index]);
            }
        }

        public static string ReadString(this BinaryReader reader, int length)
        {
            return System.Text.Encoding.Default.GetString(reader.ReadBytes(length));
        }

        public static string[] ReadStringArray1D(this BinaryReader reader)
        {

            int size_0 = reader.ReadInt32();
            string[] array = new string[size_0];
            for (int index = 0; index < size_0; index++)
            {
                array[index] = reader.ReadString();
            }
            return array;
        }

        public static void WriteStringArray2D(this BinaryWriter writer, string[,] array)
        {
            writer.Write(array.GetLength(0));
            writer.Write(array.GetLength(1));
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array.GetLength(1); index_1++)
                {
                    writer.Write(array[index_0, index_1]);
                }
            }
        }

        public static string[,] ReadStringArray2D(this BinaryReader reader)
        {
            int size_0 = reader.ReadInt32();
            int size_1 = reader.ReadInt32();
            string[,] array = new string[size_0, size_1];
            for (int index_0 = 0; index_0 < array.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < array.GetLength(1); index_1++)
                {
                    array[index_0, index_1] = reader.ReadString();
                }
            }
            return array;
        }
    }
}

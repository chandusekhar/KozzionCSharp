using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.IO
{
    public interface IBinaryReader<DataType>
    {
        DataType Read();
        DataType[] ReadArray1D();
        DataType[,] ReadArray2D();
    }

    public abstract class ABinaryReader<DataType> : IBinaryReader<DataType>
    {
        protected BinaryReader reader;
        public ABinaryReader(BinaryReader reader)
        {
            this.reader = reader;
        }

        public abstract DataType Read();
        public abstract DataType [] ReadArray1D();
        public abstract DataType[,] ReadArray2D();
    }

    public class BinaryReaderBoolean : ABinaryReader<bool>
    {
        public BinaryReaderBoolean(BinaryReader reader)
            : base(reader)
        {
        }

        public override bool Read()
        {
            return this.reader.ReadBoolean();
        }

        public override bool[] ReadArray1D()
        {
            return this.reader.ReadBooleanArray1D();
        }

        public override bool[,] ReadArray2D()
        {
            return this.reader.ReadBooleanArray2D();
        }
    }

    public class BinaryReaderInt32 : ABinaryReader<int>
    {
        public BinaryReaderInt32(BinaryReader reader)
            : base(reader)
        {
        }

        public override int Read()
        {
            return this.reader.ReadInt32();
        }

        public override int[] ReadArray1D()
        {
            return this.reader.ReadInt32Array1D();
        }

        public override int[,] ReadArray2D()
        {
            return this.reader.ReadInt32Array2D();
        }
    }

    public class BinaryReaderFloat32 : ABinaryReader<float>
    {
        public BinaryReaderFloat32(BinaryReader reader)
            : base(reader)
        {
        }

        public override float Read()
        {
            return this.reader.ReadSingle();
        }

        public override float[] ReadArray1D()
        {
            return this.reader.ReadFloat32Array1D();
        }

        public override float[,] ReadArray2D()
        {
            return this.reader.ReadFloat32Array2D();
        }
    }

    public class BinaryReaderFloat64 : ABinaryReader<double>
    {
        public BinaryReaderFloat64(BinaryReader reader)
            : base(reader)
        {
        }

        public override double Read()
        {
            return this.reader.ReadDouble();
        }

        public override double[] ReadArray1D()
        {
            return this.reader.ReadFloat64Array1D();
        }

        public override double[,] ReadArray2D()
        {
            return this.reader.ReadFloat64Array2D();
        }
    }

    public class BinaryReaderString : ABinaryReader<string>
    {
        public BinaryReaderString(BinaryReader reader)
            : base(reader)
        {
        }

        public override string Read()
        {
            return this.reader.ReadString();
        }

        public override string[] ReadArray1D()
        {
            return this.reader.ReadStringArray1D();
        }

        public override string[,] ReadArray2D()
        {
            return this.reader.ReadStringArray2D();
        }
    }



}

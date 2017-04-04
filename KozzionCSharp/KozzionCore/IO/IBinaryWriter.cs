using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.IO
{
    public interface IBinaryWriter<DataType>
    {
        void Write(DataType value);
        void Write(IList<DataType> list);
        void Write(DataType [,] array);
    }

    public abstract class ABinaryWriter<DataType> : IBinaryWriter<DataType>
    {
        protected BinaryWriter writer;
        public ABinaryWriter(BinaryWriter writer)
        {
            this.writer = writer;
        }
        public abstract void Write(DataType value);
        public abstract void Write(IList<DataType> list);
        public abstract void Write(DataType[,] array);
    }

    public class BinaryWriterInt32 : ABinaryWriter<int>
    {
        public BinaryWriterInt32(BinaryWriter writer)
            : base(writer)
        {
        }

        public override void Write(int value)
        {
            this.writer.Write(value);
        }
        public override void Write(IList<int> list)
        {
            this.writer.Write(list);
        }

        public override void Write(int[,] array)
        {
            this.writer.Write(array);
        }
    }

    public class BinaryWriterFloat32 : ABinaryWriter<float>
    {
        public BinaryWriterFloat32(BinaryWriter writer)
            : base(writer)
        {
        }

        public override void Write(float value)
        {
            this.writer.Write(value);
        }
        public override void Write(IList<float> list)
        {
            this.writer.Write(list);
        }

        public override void Write(float[,] array)
        {
            this.writer.Write(array);
        }
    }

    public class BinaryWriterFloat64 : ABinaryWriter<double>
    {
        public BinaryWriterFloat64(BinaryWriter writer)
            : base(writer)
        {
        }

        public override void Write(double value)
        {
            this.writer.Write(value);
        }
        public override void Write(IList<double> list)
        {
            this.writer.Write(list);
        }

        public override void Write(double[,] array)
        {
            this.writer.Write(array);
        }
    }

    public class BinaryWriterBoolean : ABinaryWriter<bool>
    {
        public BinaryWriterBoolean(BinaryWriter writer)
            : base(writer)
        {
        }

        public override void Write(bool value)
        {
            this.writer.Write(value);
        }
        public override void Write(IList<bool> list)
        {
            this.writer.Write(list);
        }

        public override void Write(bool[,] array)
        {
            this.writer.Write(array);
        }
    }

    public class BinaryWriterString : ABinaryWriter<string>
    {
        public BinaryWriterString(BinaryWriter writer)
            : base(writer)
        {
        }

        public override void Write(string value)
        {
            this.writer.Write(value);
        }
        public override void Write(IList<string> list)
        {
            this.writer.WriteStringArray1D(list);
        }

        public override void Write(string[,] array)
        {
            this.writer.WriteStringArray2D(array);
        }
    }
}

using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMathematics.Tools
{
    public class ToolsIOFunction
    {
        public static void Read(BinaryReader reader)
        {
        }
        public static void Write<DomainType, RangeType>(BinaryWriter writer, IFunction<DomainType, RangeType> function)
        {
            string function_type = function.FunctionType;
            switch (function_type)
            {

                default: throw new Exception();
            }
        }

    }
}

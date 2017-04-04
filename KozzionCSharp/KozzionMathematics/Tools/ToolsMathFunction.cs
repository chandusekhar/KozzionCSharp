using System;
using KozzionMathematics.Function;

namespace KozzionMathematics.Tools
{
    public class ToolsMathFunction
    {
        public static TypeRange[,] FillArray<TypeDomain0, TypeDomain1, TypeRange>(
        IFunction<TypeDomain0, TypeDomain1, TypeRange> function,
        TypeDomain0[] domain_0_values,
        TypeDomain1[] domain_1_values)
        {
            TypeRange[,] range_values = new TypeRange[domain_0_values.Length, domain_1_values.Length];
            FillArrayRBA<TypeDomain0, TypeDomain1, TypeRange>(function, domain_0_values, domain_1_values, range_values);
            return range_values;
        }

        public static  void FillArrayRBA<TypeDomain0, TypeDomain1, TypeRange>(
            IFunction<TypeDomain0, TypeDomain1, TypeRange> function,
            TypeDomain0[] domain_0_values,
            TypeDomain1[] domain_1_values,
            TypeRange[,] range_values)
        {
            for (int index_0 = 0; index_0 < domain_0_values.Length; index_0++)
            {
                for (int index_1 = 0; index_1 < domain_1_values.Length; index_1++)
                {
                    range_values[index_0, index_1] = function.Compute(domain_0_values[index_0], domain_1_values[index_1]);
                }
            }
        }

        public static DestinationRangeType[] Convert<SourceRangeType, DestinationRangeType>(SourceRangeType[] source, IFunction<SourceRangeType, DestinationRangeType> converter)
        {
            DestinationRangeType [] destination = new DestinationRangeType[source.Length];
            for (int index = 0; index < source.Length; index++)
            {
                destination[index] = converter.Compute(source[index]);
            }
            return destination;
        }
    }
}

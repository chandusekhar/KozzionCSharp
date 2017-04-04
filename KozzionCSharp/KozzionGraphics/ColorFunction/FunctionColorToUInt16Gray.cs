using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using System;
using System.Diagnostics;
using System.Drawing;

namespace KozzionGraphics.ColorFunction
{
	public class FunctionColorToUInt16Gray : IFunction<Color, ushort>
	{
        public string FunctionType { get { return "FunctionColorToUInt16Gray"; } }
        private int[] argb_integer_weigths;
        private int[] argb_integer_temp;
        private int argb_integer_divisor;


		public FunctionColorToUInt16Gray( double [] factors)
		{
			InitMembers(factors);
		}

        public FunctionColorToUInt16Gray(GrayType gray_type)
            : this(GetFactors(gray_type))
        {


		}

        public FunctionColorToUInt16Gray()
            : this(GrayType.AVERAGE)
        {
            

        }
        public ushort Compute(Color color)
        {
            return (ushort)(((color.A * argb_integer_weigths[0]) +
                    (color.R * argb_integer_weigths[1]) +
                    (color.G * argb_integer_weigths[2]) +
                    (color.B * argb_integer_weigths[3]))
                / argb_integer_divisor);
        }

		private void InitMembers(double [] factors)
		{
			Debug.Assert(factors.Length == 4);
            argb_integer_weigths = new int[4];
            argb_integer_temp = new int[4];

    
			//reduced so it will not go out of uint32 range
			double sum = ToolsMathCollection.Sum(factors);

            double multiplier = (int.MaxValue) / (sum * 255); 
			for (int index = 0; index < factors.Length; index++)
			{
                argb_integer_weigths[index] = (ushort)Math.Floor(factors[index] * multiplier);
			}
			argb_integer_divisor = ToolsMathCollectionInteger.Sum(argb_integer_weigths);
		}



        private static double[] GetFactors(GrayType gray_type)
		{
			switch (gray_type)
			{
                case GrayType.AVERAGE:
					return new double [] {0.0, 1.0, 1.0, 1.0};
                case GrayType.LUMINACE:
					return new double [] {0.0, 0.2126, 0.7152, 0.0722}; // http://en.wikipedia.org/wiki/Luma_(video)
                case GrayType.LUMINOSITY:
					return new double [] {0.0, 0.299, 0.587, 0.114}; // http://en.wikipedia.org/wiki/Luminosity
                case GrayType.ALFACHANNEL:
					return new double [] {1.0, 0.0, 0.0, 0.0};
                case GrayType.REDCHANNEL:
					return new double [] {0.0, 1.0, 0.0, 0.0};
                case GrayType.GREENCHANNEL:
					return new double [] {0.0, 0.0, 1.0, 0.0};
                case GrayType.BLUECHANNEL:
					return new double [] {0.0, 0.0, 0.0, 1.0};
                default :
					throw new Exception("Unimplemented color");
			}
		}

     
    }
}
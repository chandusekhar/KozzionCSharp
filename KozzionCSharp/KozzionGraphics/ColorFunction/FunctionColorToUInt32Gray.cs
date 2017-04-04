using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using System;
using System.Diagnostics;
using System.Drawing;

namespace KozzionGraphics.ColorFunction
{
	public class FunctionColorToUInt32Gray :IFunction<Color, uint>
	{
        public string FunctionType { get { return "FunctionColorToUInt32Gray"; } }
        private uint [] argb_integer_weigths;
		private uint [] argb_integer_temp;
		private uint    argb_integer_divisor;


		public FunctionColorToUInt32Gray( double [] factors)
		{
			InitMembers(factors);
		}

		public FunctionColorToUInt32Gray(GrayType gray_type)
           : this (GetFactors(gray_type))
		{

		}

        public FunctionColorToUInt32Gray()
            : this (GrayType.AVERAGE)
        {
        }

        public uint Compute(Color color)
        {
            return ((color.A * argb_integer_weigths[0]) +
                    (color.R * argb_integer_weigths[1]) +
                    (color.G * argb_integer_weigths[2]) +
                    (color.B * argb_integer_weigths[3]))
                / argb_integer_divisor;
        }

		private void InitMembers(double [] factors)
		{
			Debug.Assert(factors.Length == 4);
			argb_integer_weigths = new uint [4];
			argb_integer_temp = new uint [4];

    
			//reduced so it will not go out of uint32 range
			double sum = ToolsMathCollection.Sum(factors);

			double multiplier = (uint.MaxValue) / (sum * 255); 
			for (int index = 0; index < factors.Length; index++)
			{
				argb_integer_weigths[index] = (uint) Math.Floor(factors[index] * multiplier);
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
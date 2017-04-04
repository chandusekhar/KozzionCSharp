using KozzionCore.Tools;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using System;
using System.Drawing;

namespace KozzionGraphics.ColorFunction
{
    public class FunctionColorToFloat32Gray :IFunction<Color, float>
	{
        public string FunctionType { get { return "FunctionColorToFloat32Gray"; } }
        private float [] argb_weigths;
		private float sum;

		public FunctionColorToFloat32Gray(
			  float [] factors)
		{
			if (factors.Length != 4)
			{
				throw new Exception("Factors must be of length 4");
			}
			else
			{
				argb_weigths = ToolsCollection.Copy(factors);
				sum = ToolsMathCollectionFloat.sum(argb_weigths);
			}
		}

		public FunctionColorToFloat32Gray(
			  GrayType gray_type):
				this(GetFactors(gray_type))
		{
    
		}

        public FunctionColorToFloat32Gray()
            : this(GrayType.AVERAGE)
        {

        }

		public float Compute(
			  Color color)
		{
			return ((color.A * argb_weigths[0]) + (color.R * argb_weigths[1]) + (color.G * argb_weigths[2]) + (color.B * argb_weigths[3]))/ sum;
		}


        private static float[] GetFactors(
			  GrayType gray_type)
		{
			switch (gray_type)
			{
				case GrayType.AVERAGE:
					return new float [] {0.0f, 1.0f, 1.0f, 1.0f};
				case GrayType.LUMINACE:
					return new float [] {0.0f, 0.2126f, 0.7152f, 0.0722f}; // http://en.wikipedia.org/wiki/Luma_(video)
				case GrayType.LUMINOSITY:
					return new float [] {0.0f, 0.299f, 0.587f, 0.114f}; // http://en.wikipedia.org/wiki/Luminosity
				case GrayType.ALFACHANNEL:
					return new float [] {1.0f, 0.0f, 0.0f, 0.0f};
				case GrayType.REDCHANNEL:
					return new float [] {0.0f, 1.0f, 0.0f, 0.0f};
				case GrayType.GREENCHANNEL:
					return new float [] {0.0f, 0.0f, 1.0f, 0.0f};
				case GrayType.BLUECHANNEL:
					return new float [] {0.0f, 0.0f, 0.0f, 1.0f};
				default:
					throw new Exception("Unkown gray type");
			}

		}

	}
}

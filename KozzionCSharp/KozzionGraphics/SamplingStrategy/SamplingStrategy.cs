using KozzionGraphics.Image.Raster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionGraphics.Image;
using KozzionCore.Tools;
using KozzionMathematics.Tools;

namespace KozzionGraphics.SamplingStrategy
{
    public class SamplingStrategy4DRandomIndex<RangeType> : ISamplingStrategy<IRaster4DInteger, RangeType>
    {
        private bool random;
        private bool fixed_count;
        private int count;
        private double fraction;

        public SamplingStrategy4DRandomIndex(bool random, bool fixed_count, int count, double fraction)
        {
            this.random = random;
            this.fixed_count = fixed_count;
            this.count = count;
            this.fraction = fraction;

        }

        //Select a sample from a range of images and organise it into and array
        public RangeType[][] Sample(IList<IImageRaster<IRaster4DInteger, RangeType>> images, IImageRaster<IRaster4DInteger, bool> mask)
        {
            List<int> selected_indexes = mask.GetElementIndexesWithValue(true);
            if (this.random)
            {
                ToolsMathCollection.ShuffleIP(selected_indexes);
            }
            int sample_size = count;
            if (! this.fixed_count)
            {
                sample_size = (int)(selected_indexes.Count * fraction);
            }

            RangeType[][] sample = new RangeType[this.count][];
            for (int sample_index = 0; sample_index < this.count; sample_index++)
            {
                sample[sample_index] = new RangeType[images.Count];
                for (int feature_index = 0; feature_index < images.Count; feature_index++)
                {
                    int sample_value_index = selected_indexes[(int)((selected_indexes.Count / ((double)sample_size)) * sample_index)];
                    sample_value_index = ToolsMath.Clamp(sample_value_index, 0, selected_indexes.Count - 1);
                    sample[sample_index][feature_index] = images[feature_index].GetElementValue(sample_value_index);
                }
            }
            return sample;

            
        }
    }
}

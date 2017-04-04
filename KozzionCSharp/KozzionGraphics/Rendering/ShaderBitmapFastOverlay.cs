using KozzionGraphics.ColorFunction;
using KozzionGraphics.Image;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionGraphics.Rendering
{
    public class ShaderBitmapFastOverlay : IShaderBitmapFast<Tuple<IImageRaster2D<bool>, IImageRaster2D<Color>>>
    {
        public void Render(BitmapFast bitmap_fast, Tuple<IImageRaster2D<bool>, IImageRaster2D<Color>> render_image)
        {
            IImageRaster2D<bool> mask = render_image.Item1;
            IImageRaster2D<Color> color = render_image.Item2;
            bitmap_fast.Lock();
            for (int y_index = 0; y_index < bitmap_fast.Height; y_index++)
            {
                for (int x_index = 0; x_index < bitmap_fast.Width; x_index++)
                {
                    if (mask.GetElementValue(x_index, y_index))
                    {
                        bitmap_fast.SetPixel(x_index, y_index, color.GetElementValue(x_index, y_index));
                    }
                }
            }
            bitmap_fast.Unlock();
        }
    }
}

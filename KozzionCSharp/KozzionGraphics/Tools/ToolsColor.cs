using KozzionGraphics.ColorFunction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionGraphics.Tools
{
    public class ToolsColor
    {
        public static System.Windows.Media.Color ConvertToMedia(System.Drawing.Color drawing_color)
        {
            return System.Windows.Media.Color.FromArgb(drawing_color.A, drawing_color.R, drawing_color.G, drawing_color.B);
        }


        public static  System.Drawing.Color ConvertToDrawing(System.Windows.Media.Color media_color)
        {
            return System.Drawing.Color.FromArgb(media_color.A, media_color.R, media_color.G, media_color.B);
        }

        public static Color GetAverage(Color color_0, Color color_1)
        {
            return Color.FromArgb((color_0.A + color_1.A) / 2, (color_0.R + color_1.R) / 2, (color_0.G + color_1.G) / 2, (color_0.B + color_1.B) / 2);
        }

        public static Color[] GetPallete(int pallete_size)
        {

            Color[] pallete = new Color [pallete_size];
            for (int color_index = 0; color_index < pallete_size; color_index++)
            {
                pallete[color_index] = FunctionFloat64ToColorCycle.ComputeStatic((double)color_index / (double)pallete_size);
            }
            return pallete;
        }
    }
}

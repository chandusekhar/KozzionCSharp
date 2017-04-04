using KozzionGraphics.ColorFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionGraphics.Rendering
{
    public interface IShaderBitmapFast<RenderSourceType>
    {
        void Render(BitmapFast bitmap_fast, RenderSourceType render_image);
    }
}

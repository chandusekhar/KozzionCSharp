using KozzionGraphics.ColorFunction;
using System.Drawing;

namespace KozzionGraphics.Rendering
{
    public interface IRendererBitmapFast<RenderSourceType>
    {
        BitmapFast Render(RenderSourceType render_image);
    }
}

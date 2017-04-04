using System.Drawing;

namespace KozzionGraphics.Rendering
{
    public interface IRendererBitmap<RenderSourceType>
    {
        Bitmap Render(RenderSourceType render_image);
    }
}

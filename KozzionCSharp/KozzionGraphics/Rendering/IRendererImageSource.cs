using System.Windows.Media.Imaging;

namespace KozzionGraphics.Rendering
{
    public interface IRendererBitmapSource<RenderSourceType>
    {
        BitmapSource Render(RenderSourceType image);
      
    }
}

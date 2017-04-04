using KozzionGraphics.Image;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media.Imaging;

namespace KozzionGraphics.Tools
{
    public static class ToolsRendering
    {
        public static BitmapSource CreateBitmapSourceFromBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            BitmapData bitmap_data = bitmap.LockBits(
                rectangle,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                int size = (rectangle.Width * rectangle.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    System.Windows.Media.PixelFormats.Bgra32,
                    null,
                    bitmap_data.Scan0,
                    size,
                    bitmap_data.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmap_data);
            }
        }


        public static Bitmap CreateBitmapFromBitmapSource(BitmapSource source)
        {
            Bitmap bitmap = new Bitmap(
               source.PixelWidth,
               source.PixelHeight,
               System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                        BitmapData data = bitmap.LockBits(
                          new Rectangle(System.Drawing.Point.Empty, bitmap.Size),
                          ImageLockMode.WriteOnly,
                          PixelFormat.Format32bppPArgb);
                        source.CopyPixels(
                          System.Windows.Int32Rect.Empty,
                          data.Scan0,
                          data.Height * data.Stride,
                          data.Stride);
            bitmap.UnlockBits(data);
            return bitmap;
        }


        public static BitmapSource CreateBitmapSourceFromBitmap16Bit(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            BitmapData bitmap_data = bitmap.LockBits(
                rectangle,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);

            try
            {
                int size = (rectangle.Width * rectangle.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    System.Windows.Media.PixelFormats.Gray16,
                    null,
                    bitmap_data.Scan0,
                    size,
                    bitmap_data.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmap_data);
            }
        }

        public static WriteableBitmap ConvertImageRaster2DToWriteableBitmap<RangeType>(ImageRaster2D<RangeType> image_raster_base, IFunction<RangeType, int> converter)
        {
            int width = image_raster_base.Raster.Size0;
            int height = image_raster_base.Raster.Size1;
            System.Windows.Media.PixelFormat format = System.Windows.Media.PixelFormats.Bgr24;

            WriteableBitmap result = new WriteableBitmap(width, height, 96, 96, System.Windows.Media.PixelFormats.Bgr24, null);
            int[] pixels = ToolsMathFunction.Convert(image_raster_base.GetElementValues(false), converter);
            int stride = (format.BitsPerPixel / 8) * width;
            int bytes = stride * height;
            //TODO convert
            result.Lock();
            result.WritePixels(new Int32Rect(0, 0, width, height), pixels, bytes, stride);
            result.Unlock();
            return result;
        }

        public static ImageRaster2D<RangeType> ConvertWriteableBitmapToImageRaster2D<RangeType>(WriteableBitmap writeable_bitmap, IFunction<int, RangeType> converter)
        {
            int width = writeable_bitmap.PixelWidth;
            int height = writeable_bitmap.PixelHeight;
            System.Windows.Media.PixelFormat format = System.Windows.Media.PixelFormats.Bgr24;

            int stride = (format.BitsPerPixel / 8) * width;
            int bytes = stride * height;
            int[] pixels = new int[width * height];
            writeable_bitmap.CopyPixels(new Int32Rect(0, 0, width, height), pixels, bytes, stride);
            return new ImageRaster2D<RangeType>(width, height, ToolsMathFunction.Convert(pixels, converter), false);
        }
    }
}

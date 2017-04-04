using KozzionGraphics.Image.Raster;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;

namespace KozzionGraphics.ColorFunction
{
    public class BitmapFast
    {
 
        private Bitmap bitmap;
        private BitmapData bitmap_data;
        private byte[] bitmap_array;       
        private IntPtr bitmap_pointer;

        private bool is_locked;

        public int Width { get; private set; }
        public Raster2DInteger Raster { get; private set; }
        public int Height { get; private set; }
 
        public Bitmap Bitmap
        {
            get { return this.bitmap; }
        }

        public BitmapFast(int size_x, int size_y)
        {
            
            this.bitmap = new Bitmap(size_x, size_y, PixelFormat.Format32bppArgb);
            this.Raster = new Raster2DInteger(size_x, size_y);
            this.Width = bitmap.Width;
            this.Height = bitmap.Height;
            this.is_locked = false;
        }

        public void Lock()
        {
            if (this.is_locked)
            {
                throw new Exception("Bitmap already locked.");
            }

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            this.bitmap_data = this.Bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, this.Bitmap.PixelFormat);
            this.bitmap_pointer = this.bitmap_data.Scan0;        
            int bytes = (this.Width * this.Height) * 4;
            this.bitmap_array = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(this.bitmap_pointer, bitmap_array, 0, this.bitmap_array.Length);          

            this.is_locked = true;
        }

        public void Unlock()
        {
            Unlock(true);
        }

        public void Unlock(bool set_pixels)
        {
            if (!this.is_locked)
            {
                throw new Exception("Bitmap not locked.");
            }
            // Copy the RGB values back to the bitmap
            if (set_pixels)
            {
                System.Runtime.InteropServices.Marshal.Copy(this.bitmap_array, 0, this.bitmap_pointer, this.bitmap_array.Length);
            }
            // Unlock the bits.
            this.Bitmap.UnlockBits(bitmap_data);
            this.is_locked = false;
        }

        public void Clear(Color colour)
        {
            if (!this.is_locked)
            {
                throw new Exception("Bitmap not locked.");
            }
      
            for (int index = 0; index <= this.bitmap_array.Length - 1; index += 4)
            {
                this.bitmap_array[index] = colour.B;
                this.bitmap_array[index + 1] = colour.G;
                this.bitmap_array[index + 2] = colour.R;
                this.bitmap_array[index + 3] = colour.A;
            }
   
        }
        public void SetPixel(Point location, Color colour)
        {
            this.SetPixel(location.X, location.Y, colour);
        }
        public void SetPixel(int x, int y, Color colour)
        {
            if (!this.is_locked)
            {
                throw new Exception("Bitmap not locked.");
            }                  
            int index = ((y * this.Width + x) * 4);
            this.bitmap_array[index] = colour.B;
            this.bitmap_array[index + 1] = colour.G;
            this.bitmap_array[index + 2] = colour.R;
            this.bitmap_array[index + 3] = colour.A;
           
        }

        public Color GetPixel(Point location)
        {
            return this.GetPixel(location.X, location.Y);
        }

        public Color GetPixel(int x, int y)
        {
            if (!this.is_locked)
            {
                throw new Exception("Bitmap not locked.");
            }
    
            int index = ((y * this.Width + x) * 4);
            int b = this.bitmap_array[index];
            int g = this.bitmap_array[index + 1];
            int r = this.bitmap_array[index + 2];
            int a = this.bitmap_array[index + 3];
            return Color.FromArgb(a, r, g, b);
    
        }
    }
}
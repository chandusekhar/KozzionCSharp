
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows;
using KozzionGraphics.Image;
using KozzionMathematics.Function;
using KozzionGraphics.Image.Raster;
using KozzionGraphics.Tools;
using BitMiracle.LibTiff.Classic;
using System.Collections.Generic;

namespace KozzionGraphics.ImageIO
{
    public class ToolsIOImageRaster
    {
        public static void CreateMultipageTiff1(string save_path, IImageRaster<IRaster3DInteger, ushort> image)
        {
 
            int size_x = image.Raster.Size0;
            int size_y = image.Raster.Size1;
            int size_z = image.Raster.Size2;

            const int samplesPerPixel = 1;
            const int bitsPerSample = 16;

            byte[][] firstPageBuffer = new byte[size_y][];
            for (int j = 0; j < size_y; j++)
            {
                firstPageBuffer[j] = new byte[size_x];
                for (int i = 0; i < size_x; i++)
                    firstPageBuffer[j][i] = (byte)(j * i);
            }

            using (Tiff output = Tiff.Open(save_path, "w"))
            {
                for (int page = 0; page < size_z; ++page)
                {
                    output.SetField(TiffTag.IMAGEWIDTH, size_x / samplesPerPixel);
                    output.SetField(TiffTag.IMAGELENGTH, size_y);

                    output.SetField(TiffTag.SAMPLESPERPIXEL, samplesPerPixel);
                    output.SetField(TiffTag.BITSPERSAMPLE, bitsPerSample);
                    output.SetField(TiffTag.ORIENTATION, Orientation.TOPLEFT);
                    output.SetField(TiffTag.PLANARCONFIG, PlanarConfig.CONTIG);

                    if (page % 2 == 0)
                    {
                        output.SetField(TiffTag.PHOTOMETRIC, Photometric.MINISBLACK);
                    }
                    else
                    {
                        output.SetField(TiffTag.PHOTOMETRIC, Photometric.MINISWHITE);
                    }

                    output.SetField(TiffTag.ROWSPERSTRIP, 1);
                    output.SetField(TiffTag.XRESOLUTION, 100.0);
                    output.SetField(TiffTag.YRESOLUTION, 100.0);
                    output.SetField(TiffTag.RESOLUTIONUNIT, ResUnit.INCH);

                    // specify that it's a page within the multipage file
                    output.SetField(TiffTag.SUBFILETYPE, FileType.PAGE);
                    // specify the page number
                    output.SetField(TiffTag.PAGENUMBER, page, size_z);

                    for (int j = 0; j < size_y; ++j)
                    {
                        output.WriteEncodedStrip(j, firstPageBuffer[j], firstPageBuffer.Length);
                    }
                    output.WriteDirectory();
                }
            }
        }

        public static void SaveImageRaster3DAsTIFF163D(string save_path, IImageRaster<IRaster3DInteger, ushort> image)
        {
            int size_x = image.Raster.Size0;
            int size_y = image.Raster.Size1;
            int size_z = image.Raster.Size2;

            const int samplesPerPixel = 1;
            const int bitsPerSample = 16;

            byte[][] page_buffer = new byte[size_y][];
            for (int index_y = 0; index_y < size_y; index_y++)
            {
                page_buffer[index_y] = new byte[size_x * 2];
            }

            using (Tiff output = Tiff.Open(save_path, "w"))
            {
                for (int index_z = 0; index_z < size_z; ++index_z)
                {
                    output.SetField(TiffTag.IMAGEWIDTH, size_x / samplesPerPixel);
                    output.SetField(TiffTag.IMAGELENGTH, size_y);
                    output.SetField(TiffTag.COMPRESSION, Compression.NONE);
                    output.SetField(TiffTag.SAMPLESPERPIXEL, samplesPerPixel);
                    output.SetField(TiffTag.BITSPERSAMPLE, bitsPerSample);
                    output.SetField(TiffTag.ORIENTATION, Orientation.TOPLEFT);
                    output.SetField(TiffTag.PLANARCONFIG, PlanarConfig.CONTIG);

                    if (index_z % 2 == 0)
                    {
                        output.SetField(TiffTag.PHOTOMETRIC, Photometric.MINISBLACK);
                    }
                    else
                    {
                        output.SetField(TiffTag.PHOTOMETRIC, Photometric.MINISWHITE);
                    }

                    output.SetField(TiffTag.ROWSPERSTRIP, 1);
                    output.SetField(TiffTag.XRESOLUTION, 59.0);
                    output.SetField(TiffTag.YRESOLUTION, 59.0);
                    output.SetField(TiffTag.RESOLUTIONUNIT, ResUnit.INCH);

                    // specify that it's a page within the multipage file
                    output.SetField(TiffTag.SUBFILETYPE, FileType.PAGE);
                    // specify the page number
                    output.SetField(TiffTag.PAGENUMBER, index_z, size_z);


                    for (int index_y = 0; index_y < size_y; index_y++)
                    {
                        for (int index_x = 0; index_x < size_x; index_x++)
                        {
                            byte[] bytes = BitConverter.GetBytes(image.GetElementValue(image.Raster.GetElementIndex(index_x, index_y, 0)));                       
                            page_buffer[index_y][(index_x * 2)] = bytes[1];
                            page_buffer[index_y][(index_x * 2) + 1] = bytes[0];  
                        }
                    }
                    for (int index_y = 0; index_y < size_y; ++index_y)
                    {
                        output.WriteEncodedStrip(index_y, page_buffer[index_y], page_buffer[index_y].Length);
                    }
                    output.WriteDirectory();
                }
            }
        }
       

        public static Bitmap ConvertToBitmapUInt16(IImageRaster<IRaster3DInteger, uint> image, int index_z)
        {
            Bitmap bitmap = new Bitmap(image.Raster.Size0, image.Raster.Size1, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);
            //Get a reference to the images pixel data
            Rectangle dimension = new Rectangle(0, 0, image.Raster.Size0, image.Raster.Size1);
            BitmapData bitmap_data = bitmap.LockBits(dimension, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);
            IntPtr pixel_start_address = bitmap_data.Scan0;
            byte[] pixel_values = new byte[image.Raster.Size0 * image.Raster.Size1 * 2];
            //Copy the pixel data into the bitmap structure
            int byte_index = 0;
            for (int index_y = 0; index_y < image.Raster.Size1; index_y++)
            {
                for (int index_x = 0; index_x < image.Raster.Size0; index_x++)
                {
                    byte[] bytes = BitConverter.GetBytes(image.GetElementValue(image.Raster.GetElementIndex(index_x, index_y, index_z)));
                    pixel_values[byte_index] = bytes[0];
                    byte_index++;
                    pixel_values[byte_index] = bytes[1];
                    byte_index++;
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(pixel_values, 0, pixel_start_address, pixel_values.Length);
            bitmap.UnlockBits(bitmap_data);
            return bitmap;
        }


        public static void SaveImageRaster3DAsTIFF3D2(string save_path, IImageRaster<IRaster3DInteger, uint> image)
        {
            Stream destination_stream = new FileStream(save_path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            TiffBitmapEncoder encoder = new TiffBitmapEncoder();
            encoder.Compression = TiffCompressOption.None;
            for (int index_z = 0; index_z < image.Raster.Size2; index_z++)
            {
                Bitmap bitmap = ConvertToBitmapUInt16(image, index_z);
                BitmapSource bitmap_source = ToolsRendering.CreateBitmapSourceFromBitmap16Bit(bitmap);
                encoder.Frames.Add(BitmapFrame.Create(bitmap_source));
            }   
            encoder.Save(destination_stream);
            //TiffBitmapEncoder encoder = new TiffBitmapEncoder(destination_stream_, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            //Stream imageStreamSource = new FileStream(load_path, FileMode.Open, FileAccess.Read, FileShare.Read);
            //TiffBitmapDecoder decoder = new TiffBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            //BitmapSource test_frame = decoder.Frames[0];
            //int size_x = test_frame.PixelWidth;
            //int size_y = test_frame.PixelHeight;
            //int size_z = decoder.Frames.Count;
            //int bits_per_pixel = test_frame.Format.BitsPerPixel;

            //for (int index_z = 0; index_z < size_z; index_z++)
            //{
            //    // save each frame to a bytestream
            //    BitmapSource frame = decoder.Frames[index_z];
            //    //  img.CopyPixels(pixels, stride, 0)
            //    MemoryStream byte_stream = new MemoryStream();
            //    // bitmap.Save(byte_stream, ImageFormat.Tiff);

            //    // and then create a new Image from it
            //    System.Drawing.Image image = System.Drawing.Image.FromStream(byte_stream);
            //    // d
            //}
 
        }

        public static IImageRaster2D<ElementType> LoadImageRaster2DBitmap<ElementType>(string path, IFunction<Color, ElementType> color_converter)
        {
            // Open a Stream and decode a bitmap image
            Bitmap image_color = new Bitmap(path);
            int element_count = image_color.Width * image_color.Height;
            ElementType[] image_array = new ElementType[element_count];
            IRaster2DInteger raster = new Raster2DInteger(image_color.Width, image_color.Height);

            for (int index_y = 0; index_y < image_color.Height; index_y++)
            {
                for (int index_x = 0; index_x < image_color.Width; index_x++)
                {
                    image_array[raster.GetElementIndex(index_x, index_y)] = color_converter.Compute(image_color.GetPixel(index_x, index_y));
                }
            }
            return new ImageRaster2D<ElementType>(raster, image_array, false);
        }

        public static ImageRaster3D<ElementType> LoadImageRaster3DBitmap<ElementType>(string file_path, IFunction<Color, ElementType> color_converter)
        {
            Bitmap image_color = new Bitmap(file_path);
            FrameDimension frame_dimension = GetFrameDimension(image_color);
            IRaster3DInteger raster = new Raster3DInteger(image_color.Width, image_color.Height, image_color.GetFrameCount(frame_dimension));
            ElementType[] image_array = new ElementType[raster.ElementCount];
            for (int index_z = 0; index_z < raster.Size2; index_z++)
            {
                image_color.SelectActiveFrame(frame_dimension, index_z);
                for (int index_y = 0; index_y < raster.Size1; index_y++)
                {
                    for (int index_x = 0; index_x < raster.Size0; index_x++)
                    {
                        image_array[raster.GetElementIndex(index_x, index_y, index_z)] = color_converter.Compute(image_color.GetPixel(index_x, index_y));
                    }
                }
            }
            return new ImageRaster3D<ElementType>(raster, image_array, false);
        }


        public static ImageRaster4D<ElementType> LoadImageRaster4DBitmap<ElementType>(string file_path, IFunction<Color, ElementType> color_converter)
        {
            Bitmap image_color = new Bitmap(file_path);
            FrameDimension frame_dimension = GetFrameDimension(image_color);
            Raster4DInteger raster = new Raster4DInteger(image_color.Width, image_color.Height, image_color.GetFrameCount(frame_dimension), 1);
            ElementType[] image_array = new ElementType[raster.ElementCount];
            for (int index_z = 0; index_z < raster.Size2; index_z++)
            {
                image_color.SelectActiveFrame(frame_dimension, index_z);
                for (int index_y = 0; index_y < raster.Size1; index_y++)
                {
                    for (int index_x = 0; index_x < raster.Size0; index_x++)
                    {
                        image_array[raster.GetElementIndex(index_x, index_y, index_z, 0)] = color_converter.Compute(image_color.GetPixel(index_x, index_y));
                    }
                }
            }
            return new ImageRaster4D<ElementType>(raster, image_array, false);
        }

        public static FrameDimension GetFrameDimension(Bitmap bitmap)
        {
            Guid[] frame_dimensions = bitmap.FrameDimensionsList;
            if (frame_dimensions.Length == 0)
            {
                throw new Exception("No frame dimension present");
            }
            if (frame_dimensions.Length != 1)
            {
                throw new Exception("More than one frame dimension");
            }
            else 
            {
                if(frame_dimensions[0].Equals(FrameDimension.Page.Guid))
                {
                    return FrameDimension.Page;
                }
                else if(frame_dimensions[0].Equals(FrameDimension.Resolution.Guid))
                {
                     return FrameDimension.Resolution;
                }
                else if (frame_dimensions[0].Equals(FrameDimension.Time.Guid))
                {
                    return FrameDimension.Time;
                }
                else 
                {
                    throw new Exception("Unkown FrameDimension: " + frame_dimensions[0]);
                }                   
            }
        }

        public static IImageRaster2D<ElementType> Convert<ElementType>(String path, IFunction<Color, ElementType> color_converter)
        {
            // Open a Stream and decode a bitmap image
            Bitmap image_color = new Bitmap(path);
            int element_count = image_color.Width * image_color.Height;
            ElementType[] image_array = new ElementType[element_count];
            IRaster2DInteger raster = new Raster2DInteger(image_color.Width, image_color.Height);

            for (int index_y = 0; index_y < image_color.Height; index_y++)
            {
                for (int index_x = 0; index_x < image_color.Width; index_x++)
                {
                    image_array[raster.GetElementIndex(index_x, index_y)] = color_converter.Compute(image_color.GetPixel(index_x, index_y));
                }
            }
            return new ImageRaster2D<ElementType>(raster, image_array, false);
        }

        public static void SaveImageBitmapAsGif(string save_path, Bitmap [] bitmaps)
        {
            GifBitmapEncoder gif_encoder = new GifBitmapEncoder();

            foreach (Bitmap bitmap in bitmaps)
            {
                bitmap.GetPropertyItem(0x5100).Value = new byte []{100}; //This should be tested
                BitmapSource src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bitmap.GetHbitmap(),
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());

                gif_encoder.Frames.Add(BitmapFrame.Create(src));
            }
            using (FileStream fs = new FileStream(save_path, FileMode.Create))
            {
                gif_encoder.Save(fs);
            }
        }
        /*
        private static byte [] LoadImagePNG( File file)
        {
            // Open a Stream and decode a PNG image
            Stream imageStreamSource = new FileStream(file.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
            PngBitmapDecoder decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            return load_image(decoder);
        }

        private static byte [] LoadImageJPG( File file)
        {
            // Open a Stream and decode a PNG image
            Stream imageStreamSource = new FileStream(file.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
            PngBitmapDecoder decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            return load_image(decoder);
        }

        private static byte [] load_image(BitmapDecoder file)
        {
            // Open a Stream and decode a PNG image
            Stream imageStreamSource = new FileStream(file.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
            BitmapDecoder decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = decoder.Frames[0];
		
        }
         * 

		

        private static void WriteImagePNG(BitmapSource image)
        {

                FileStream stream = new FileStream("new.png", FileMode.Create);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                TextBlock myTextBlock = new TextBlock();
                myTextBlock.Text = "Codec Author is: " + encoder.CodecInfo.Author.ToString();
                encoder.Interlace = PngInterlaceOption.On;
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
        }
        */



        public static Dictionary<string, string> ReadImageFileMetaData(string file_path, string image_file_type_tag)
        {
            switch (image_file_type_tag)
            {
                case "PNGIM":
                case "JPGIM":
                case "GIF7A":
                case "GIF9A":
                    return ReadBitmapMetaData(file_path);
                default:
                    throw new Exception("Unkown tag type");
            }
        }

        private static Dictionary<string, string> ReadBitmapMetaData(string file_path)
        {
            Dictionary<string, string>  metadata = new Dictionary<string,string>();
            Bitmap bitmap = new Bitmap(file_path);
            metadata.Add("Width", bitmap.Width.ToString());
            metadata.Add("Height", bitmap.Height.ToString());
            metadata.Add("Palette", bitmap.Palette.ToString());
            metadata.Add("PixelFormat", bitmap.PixelFormat.ToString());
            return metadata;
        }

      
    }
}
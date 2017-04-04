using System;
using KozzionGraphics.Image.Raster;

namespace KozzionGraphics.Image
{
    [Serializable()]
	public class ImageRaster2D<ElementType> : ImageRaster<IRaster2DInteger, ElementType>, IImageRaster2D<ElementType>
	{
		public ImageRaster2D() :
			base(new Raster2DInteger(1, 1))
		{
		
		}
        public ImageRaster2D(
            int size_x,
            int size_y) :
            base(new Raster2DInteger(size_x, size_y))
        {
        }

        public ImageRaster2D(IRaster2DInteger raster)
            : this(raster.Size0, raster.Size1)
        {
            
        }



		public ImageRaster2D(
			int size_x,
			int size_y,
            ElementType[] image,
			bool copy_array):
			base(new Raster2DInteger(size_x, size_y), image, copy_array)
		{
		
		}

        public ImageRaster2D(
            IRaster2DInteger raster,
			ElementType[] image,
			bool copy_array) :
            base(raster, image, copy_array)
        {

        }

		public ImageRaster2D(
			  int size_x,
			  int size_y,
              ElementType value) :
			base(new Raster2DInteger(size_x, size_y), value)
		{		

		}

        public ImageRaster2D(IRaster2DInteger raster, ElementType value)
            : this(raster.Size0, raster.Size1, value)
        {
        
        }

 

        public ElementType GetElementValue(int coordinate_x, int coordinate_y)
        {
            return GetElementValue(Raster.GetElementIndex(coordinate_x, coordinate_y));
        }

        public void SetElementValue(int coordinate_x, int coordinate_y, ElementType value)
        {
            SetElementValue(Raster.GetElementIndex(coordinate_x, coordinate_y), value);
        }

		/*
		public IntegerRasterIntegerImage2D(
			IntegerRasterBooleanImage2D boolean_image,
			int true_value,
			int false_value)
		{
			super(new IntegerRaster2D(boolean_image.get_size_x(), boolean_image.get_size_y()));
			for (int element_index = 0; element_index < get_element_count(); element_index++)
			{
				if (boolean_image.get_element_value(element_index))
				{
					set_element_value(element_index, true_value);
				}
				else
				{
					set_element_value(element_index, false_value);
				}
			}
		}

		public IntegerRasterIntegerImage2D(
			  IntegerRasterIntegerImage2D other)
		{
			super(other);
		}
		
		public IntegerRasterIntegerImage2D(
			  BufferedImage bufferedimage,
			  IColorToIntegerConverter converter)
		{
			super(new IntegerRaster2D(bufferedimage.getWidth(), bufferedimage.getHeight()));
			int size_x = get_size_x();
			int size_y = get_size_y();

			for (int index_y = 0; index_y < size_y; index_y++)
			{
				for (int index_x = 0; index_x < size_x; index_x++)
				{
					set_element_value(index_x, index_y, converter.convert_to_integer(bufferedimage.getRGB(index_x, index_y)));
				}
			}

		}

		public IntegerRasterIntegerImage2D(
			IIntegerRaster2D raster)
		{
			super(raster);
		}

		public IntegerRasterIntegerImage2D(
			IIntegerRaster2D raster,
			int [] array,
			boolean copy)
		{
			super(raster, array, copy);
		}
		*/

		/*
		public static IntegerRasterIntegerImage2D read_pgm(  File file) throws IOException
		{
			System.out.println("start reading");
			  FileInputStream input_stream = new FileInputStream(file);
			  Scanner scanner = new Scanner(input_stream);
			String line = scanner.nextLine();
			int skiplines = 3;

			line = scanner.nextLine(); // first lines
			while (line.startsWith("#"))
			{
				skiplines++;
				line = scanner.nextLine(); // comment lines
			}
			  String [] dimensions = line.split(" ");
			  int width = Integer.parseInt(dimensions[0]);
			  int height = Integer.parseInt(dimensions[1]);
			scanner.nextInt();
			scanner.close();

			  FileInputStream new_input_stream = new FileInputStream(file);
			// look for 4 lines (i.e.: the header) and discard them

			while (0 < skiplines)
			{
				int character = 0;

				while (character != 10)
				{
					character = new_input_stream.read();
				}
				skiplines--;
			}

			  int size = width * height;

			// byte [] byte_image = new byte [size];
			// new_input_stream.read(byte_image);

			  int [] int_image = new int [size];
			for (int i = 0; i < int_image.length; i++)
			{
				int_image[i] = new_input_stream.read();
			}

			new_input_stream.close();
			System.out.println("done reading");
			return new IntegerRasterIntegerImage2D(width, height, int_image);
		}
	
	

		public Point get_integer_point(  Integer element)
		{
			return new Point(element % get_width(), element / get_width());
		}

		public IntegerRasterIntegerImage2D slice_vertical(  int start_x,   int limit_x)
		{
			return crop(start_x, 0, limit_x, get_height());
		}

		public IntegerRasterIntegerImage2D slice_horizontal(  int start_y,   int limit_y)
		{
			return crop(0, start_y, get_width(), limit_y);
		}

		// should
		// be
		// first
		// beyond
		// copied
		// region
		public IntegerRasterIntegerImage2D crop(  int start_x,   int start_y,   int limit_x,   int limit_y) // limit
		{
			  IntegerRasterIntegerImage2D cropped = new IntegerRasterIntegerImage2D(limit_x - start_x, limit_y - start_y);
			for (int index_y = start_y; index_y < limit_y; index_y++)
			{
				for (int index_x = start_x; index_x < limit_x; index_x++)
				{
					cropped.set_element_value(index_x - start_x, index_y - start_y, get_pixel_value(index_x, index_y));
				}
			}
			return cropped;
		}

		public List<Integer> get_elements_above(  int threshold)
		{
			  List<Integer> elements = new Vector<Integer>();
			for (int element_index = 0; element_index < get_element_count(); element_index++)
			{
				if (threshold < get_generic_element_value(element_index))
				{
					elements.add(element_index);
				}
			}
			return elements;
		}

		public List<Point> get_points_above(  int threshold)
		{
			  List<Point> points = new Vector<Point>();
			for (int element_index = 0; element_index < get_element_count(); element_index++)
			{
				if (threshold < get_generic_element_value(element_index))
				{
					points.add(get_point(element_index));
				}
			}
			return points;
		}
				
	public boolean contains_pixel(  int x,   int y)
		{
			return ((0 <= x) && (x < get_width()) && (0 <= y) && (y < get_height()));
		}
		public List<Tuple2<Point, Integer>> get_point_values_above(  int threshold)
		{
			  List<Tuple2<Point, Integer>> point_values = new Vector<Tuple2<Point, Integer>>();
			for (int element_index = 0; element_index < get_element_count(); element_index++)
			{
				if (threshold < get_generic_element_value(element_index))
				{
					point_values.add(new Tuple2<Point, Integer>(get_point(element_index), get_generic_element_value(element_index)));
				}
			}
			return point_values;
		}

		public List<Tuple2<Point, Integer>> get_point_values_above(  Point translation,   int threshold)
		{
			System.out.println("threshold" + threshold);
			  List<Tuple2<Point, Integer>> point_values = new Vector<Tuple2<Point, Integer>>();
			for (int element_index = 0; element_index < get_element_count(); element_index++)
			{
				if (threshold < get_generic_element_value(element_index))
				{
					point_values.add(new Tuple2<Point, Integer>(new Point((element_index % get_width()) + translation.x,
						(element_index / get_width()) + translation.y), get_generic_element_value(element_index)));
				}
			}
			return point_values;
		}

		public Point get_point(  int element_index)
		{
			return new Point(element_index % get_width(), element_index / get_width());
		}
		/*
		public IntegerRasterBooleanImage2D threshold(int d_threshold_value, boolean equal_or_above_value)
		{
			boolean [] boolean_image = new boolean [get_element_count()];
			for (int element_index = 0; element_index < boolean_image.length; element_index++)
			{
				if (d_threshold_value <= get_generic_element_value(element_index))
				{
					boolean_image[element_index] = equal_or_above_value;
				}
				else
				{
					boolean_image[element_index] = !equal_or_above_value;
				}
			}
			return new IntegerRasterBooleanImage2D(get_width(), get_height(), boolean_image);
		}
		*/


	}
}
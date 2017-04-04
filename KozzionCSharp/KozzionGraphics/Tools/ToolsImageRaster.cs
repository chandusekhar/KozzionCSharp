using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionGraphics.Image.Topology;
using KozzionMathematics.Function.Implementation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KozzionPerfusionCL.Experiments;
using KozzionMathematics.Tools;

namespace KozzionGraphics.Tools
{
    public class ToolsImageRaster
    {
        public static ImageRaster2D<bool> Threshold<DomainType>(ImageRaster2D<DomainType> image, DomainType threshold)
            where DomainType : IComparable<DomainType>
        {
            return new ImageRaster2D<bool>(image.Raster, image.Convert<bool>(new FunctionStep<DomainType>(threshold)).GetElementValues(false), false);
        }

        public static ImageRaster3D<bool> Threshold<DomainType>(ImageRaster3D<DomainType> image, DomainType threshold)
        where DomainType : IComparable<DomainType>
        {
            return new ImageRaster3D<bool>(image.Raster, image.Convert<bool>(new FunctionStep<DomainType>(threshold)).GetElementValues(false), false);
        }

        public static ImageRaster4D<bool> Threshold<DomainType>(ImageRaster4D<DomainType> image, DomainType threshold)
			where DomainType : IComparable<DomainType>
        {
            return new ImageRaster4D<bool>(image.Convert<bool>(new FunctionStep<DomainType>(threshold)),false);
        }

        public static ImageRaster4D<bool> Threshold<DomainType>(ImageRaster4D<DomainType> image, DomainType lower_threshold, DomainType upper_threshold)
             where DomainType : IComparable<DomainType>
        {
            return new ImageRaster4D<bool>(image.Convert<bool>(new FunctionBoxCar<DomainType>(lower_threshold, upper_threshold)), false);
        }

        public static ImageRaster3D<RangeType> Pad<RangeType>(IImageRaster3D<RangeType> image, RangeType padding_type, int padding_0, int padding_1, int padding_2)
        {
            return Pad(image, padding_type, padding_0, padding_0, padding_1, padding_1, padding_2, padding_2);
        }

        public static Tuple<ImageRaster3D<float>, ImageRaster3D<float>, ImageRaster3D<float>> ComputeGradientMasked(ImageRaster3D<float> image, ImageRaster3D<bool> mask, float[] voxel_size)
        {
            IRaster3DInteger raster = image.Raster;
            List<int> mask_indexes = mask.GetElementIndexesWithValue(true);


            //DO gradient 0
            ImageRaster3D<float> image_gradient_0 = new ImageRaster3D<float>(raster);
            Parallel.For(0, mask_indexes.Count, index_index =>
            {
                int element_index = mask_indexes[index_index];
                int index_0 = element_index % raster.Size0;
                int index_1 = (element_index % (raster.Size0 * raster.Size1)) / raster.Size0;
                int index_2 = element_index / (raster.Size0 * raster.Size1);
                int offset_0 = 1;
                int offset_1 = 0;
                int offset_2 = 0;
                float gradient = ComputeGradientMasked(image, mask, index_0, index_1, index_2, offset_0, offset_1, offset_2, voxel_size);
                image_gradient_0.SetElementValue(element_index, gradient);
            });

            //DO gradient 1
            ImageRaster3D<float> image_gradient_1 = new ImageRaster3D<float>(raster);
            Parallel.For(0, mask_indexes.Count, index_index =>
            {
                int element_index = mask_indexes[index_index];
                int index_0 = element_index % raster.Size0;
                int index_1 = (element_index % (raster.Size0 * raster.Size1)) / raster.Size0;
                int index_2 = element_index / (raster.Size0 * raster.Size1);
                int offset_0 = 0;
                int offset_1 = 1;
                int offset_2 = 0;
                float gradient = ComputeGradientMasked(image, mask, index_0, index_1, index_2, offset_0, offset_1, offset_2, voxel_size);
                image_gradient_1.SetElementValue(element_index, gradient);
            });

            //DO gradient 2
            ImageRaster3D<float> image_gradient_2 = new ImageRaster3D<float>(raster);
            Parallel.For(0, mask_indexes.Count, index_index =>
            {
                int element_index = mask_indexes[index_index];
                int index_0 = element_index % raster.Size0;
                int index_1 = (element_index % (raster.Size0 * raster.Size1)) / raster.Size0;
                int index_2 = element_index / (raster.Size0 * raster.Size1);
                int offset_0 = 0;
                int offset_1 = 0;
                int offset_2 = 1;
                float gradient = ComputeGradientMasked(image, mask, index_0, index_1, index_2, offset_0, offset_1, offset_2, voxel_size);
                image_gradient_2.SetElementValue(element_index, gradient);
            });
            return new Tuple<ImageRaster3D<float>, ImageRaster3D<float>, ImageRaster3D<float>>(image_gradient_0, image_gradient_1, image_gradient_2);
        }


        public static StructuringElement3D CreateElement3DDisk(IRaster3DInteger raster, double[] voxel_size, double disk_radius)
        {
            int disk_radius_0_voxels = 1 + (2 * ((int)(disk_radius / voxel_size[0])));
            int disk_radius_1_voxels = 1 + (2 * ((int)(disk_radius / voxel_size[1])));


            List<int[]> element_offsets = new List<int[]>();
            for (int offset_0 = -disk_radius_0_voxels; offset_0 <= disk_radius_0_voxels; offset_0++)
            {
                for (int offset_1 = -disk_radius_1_voxels; offset_1 <= disk_radius_1_voxels; offset_1++)
                {
                    element_offsets.Add(new int[] { offset_0, offset_1, 0 });
                }              
            }
            return new StructuringElement3D(element_offsets);
        }


        //TODO speed up
        public static void MorphologicalOpeningRBA<RangeType>(ImageRaster3D<RangeType> source, StructuringElement3D structure, RangeType default_value, ImageRaster3D<RangeType> temp, ImageRaster3D<RangeType> target)
            where RangeType : IComparable<RangeType>
        {
            if ((!source.Raster.Equals(temp.Raster)) || (!source.Raster.Equals(target.Raster)))
            {
                throw new Exception("Raster Mismatch");
            }
            IRaster3DInteger raster = source.Raster;

            //Do erosion
            MorphologicalErosionRBA(source, structure, default_value, temp);

            //Do dilation
            MorphologicalDilationRBA(temp, structure, default_value, target);
        }


        public static ImageRaster3D<RangeType> MorphologicalOpening<RangeType>(ImageRaster3D<RangeType> source, StructuringElement3D structure, RangeType default_value)
         where RangeType : IComparable<RangeType>
        {
            ImageRaster3D<RangeType> temp   = new ImageRaster3D<RangeType>(source.Raster);
            ImageRaster3D<RangeType> target = new ImageRaster3D<RangeType>(source.Raster);
            MorphologicalOpeningRBA(source, structure, default_value, temp, target);
            return target;
        }

        public static void MorphologicalErosionRBA<RangeType>(ImageRaster3D<RangeType> source, StructuringElement3D structure, RangeType default_value, ImageRaster3D<RangeType> target)
            where RangeType : IComparable<RangeType>
        {
            if (!source.Raster.Equals(target.Raster))
            {
                throw new Exception("Raster Mismatch");
            }
            IRaster3DInteger raster = source.Raster;
            Parallel.For(0, source.ElementCount, source_element_index =>
            {
                int[] coordinates = raster.GetElementCoordinates(source_element_index);
                RangeType value = default_value;
                bool found = false;
                for (int offset_index = 0; offset_index < structure.FlippedOffsets.Count; offset_index++)
                {
                    int[] offset = structure.FlippedOffsets[offset_index];
                    if (raster.ContainsCoordinates(coordinates[0] + offset[0], coordinates[1] + offset[1], coordinates[2] + offset[2]))
                    {
                        if (found)
                        {
                            value = ToolsMath.Min(value, source.GetElementValue(coordinates[0] + offset[0], coordinates[1] + offset[1], coordinates[2] + offset[2]));
                        }
                        else
                        {
                            value = source.GetElementValue(coordinates[0] + offset[0], coordinates[1] + offset[1], coordinates[2] + offset[2]);
                            found = true;
                        }
                     
                    }
                }
                target.SetElementValue(source_element_index, value);
            });
        }

        public static void MorphologicalDilationRBA<RangeType>(ImageRaster3D<RangeType> source, StructuringElement3D structure, RangeType default_value, ImageRaster3D<RangeType> target)
            where RangeType : IComparable<RangeType>
        {
            if (!source.Raster.Equals(target.Raster))
            {
                throw new Exception("Raster Mismatch");
            }
            IRaster3DInteger raster = source.Raster;
            //Parallel.For(0, source.ElementCount, source_element_index =>
            for (int source_element_index = 0; source_element_index < source.ElementCount; source_element_index++)
            {
                int[] coordinates = raster.GetElementCoordinates(source_element_index);
                RangeType value = default_value;
                bool found = false;
                for (int offset_index = 0; offset_index < structure.FlippedOffsets.Count; offset_index++)
                {
                    int[] offset = structure.FlippedOffsets[offset_index];
                    if (raster.ContainsCoordinates(coordinates[0] + offset[0], coordinates[1] + offset[1], coordinates[2] + offset[2]))
                    {                    
                        if (found)
                        {
                            value = ToolsMath.Max(value, source.GetElementValue(coordinates[0] + offset[0], coordinates[1] + offset[1], coordinates[2] + offset[2]));
                        }
                        else
                        {
                            value = source.GetElementValue(coordinates[0] + offset[0], coordinates[1] + offset[1], coordinates[2] + offset[2]);
                            found = true;
                        }
                    }

             
                }
                target.SetElementValue(source_element_index, value);
            }//);
        }


        public static ImageRaster3D<bool> MorphologicalErosion(ImageRaster3D<bool> mask, float[] voxel_size, float erosion_size)
        {
            ImageRaster3D<float> distance = ToolsDistance.DistanceTransform3D(mask, voxel_size);
            ImageRaster3D<bool> eroded = new ImageRaster3D<bool>(mask.Raster);

            Parallel.For(0, eroded.ElementCount, index =>
            {
                if (distance[index] < erosion_size)
                {
                    eroded[index] = true;
                }
            });
            return eroded;
        }


        public static ImageRaster3D<bool> Union(ImageRaster3D<bool> source_0, ImageRaster3D<bool> source_1)
        {
            ImageRaster3D<bool> target = new ImageRaster3D<bool>(source_0.Raster);
            Parallel.For(0, source_0.ElementCount, index =>
            {
                target[index] = source_0[index] || source_1[index];
            });
            return target;
        }




        public static float ComputeGradientMasked(ImageRaster3D<float> image, ImageRaster3D<bool> mask,
            int index_0, int index_1, int index_2, int offset_0, int offset_1, int offset_2, float[] voxel_size)
        {
            if (mask.Raster.ContainsCoordinates(index_0 + offset_0, index_1 + offset_1, index_2 + offset_2) &&
                           mask.GetElementValue(index_0 + offset_0, index_1 + offset_1, index_2 + offset_2))
            {

                if (mask.Raster.ContainsCoordinates(index_0 - offset_0, index_1 - offset_1, index_2 - offset_2) &&
                               mask.GetElementValue(index_0 - offset_0, index_1 - offset_1, index_2 - offset_2))
                {
                    return (0.5f * image.GetElementValue(index_0 + offset_0, index_1 + offset_1, index_2 + offset_2)
                          - 0.5f * image.GetElementValue(index_0 - offset_0, index_1 - offset_1, index_2 - offset_2)) * voxel_size[0];
                }
                else
                {
                    return (image.GetElementValue(index_0 + offset_0, index_1 + offset_1, index_2 + offset_2)
                        - image.GetElementValue(index_0, index_1, index_2)) * voxel_size[0];
                }
            }
            else
            {
                if (mask.Raster.ContainsCoordinates(index_0 - offset_0, index_1 - offset_1, index_2 - offset_2) &&
                               mask.GetElementValue(index_0 - offset_0, index_1 - offset_1, index_2 - offset_2))
                {
                    return (image.GetElementValue(index_0, index_1, index_2)
                                - image.GetElementValue(index_0 - offset_0, index_1 - offset_1, index_2 - offset_2)) * voxel_size[0];
                }
                else
                {
                    return 0;
                }
            }
        }

        public static ImageRaster3D<RangeType> Pad<RangeType>(IImageRaster3D<RangeType> image, RangeType padding_type, int padding_0_low, int padding_0_high, int padding_1_low, int padding_1_high, int padding_2_low, int padding_2_high)
        {
            ImageRaster3D<RangeType> padded = new ImageRaster3D<RangeType>(image.Raster.Size0 + padding_0_low + padding_0_high,
                                                                           image.Raster.Size1 + padding_1_low + padding_1_high,
                                                                           image.Raster.Size2 + padding_2_low + padding_2_high, padding_type);

            Parallel.For(0, padded.Raster.Size2 - (padding_2_low + padding_2_high), index_2 =>
            {
                for (int index_1 = 0; index_1 < padded.Raster.Size1 - (padding_1_low + padding_1_high); index_1++)
                {
                    for (int index_0 = 0; index_0 < padded.Raster.Size0 - (padding_0_low + padding_0_high); index_0++)
                    {
                        RangeType value = image.GetElementValue(index_0, index_1, index_2);
                        padded.SetElementValue(index_0 + padding_0_low, index_1 + padding_1_low, index_2 + padding_2_low, value);
                    }
                }
            });
            return padded;
        }

        public static void InvertIP(ImageRaster3D<bool> source)
        {
            Parallel.For(0, source.ElementCount, index =>
            {
                source[index] = !source[index];
            });
        }

        public static ImageRaster3D<bool> Invert(ImageRaster3D<bool> source)
        {
            bool [] values_source = source.GetElementValues(false);
            bool [] values_inverted = new bool[values_source.Length];
            Parallel.For(0, values_source.Length, index =>
            {
                values_inverted[index] = !values_source[index];
            });
            return new ImageRaster3D<bool>(source.Raster, values_inverted, true);
        }

        public static ImageRaster2D<bool> GetShell2D4C(ImageRaster2D<bool> source, bool border_is_shell = false)
        {
            ITopologyElement topology = new TopologyElementRaster2D4Connectivity(source.Raster);
            ImageRaster2D<bool> target = new ImageRaster2D<bool>(source.Raster);
            GetShellRBA(source, topology, target, border_is_shell);
            return target;
        }

        public static ImageRaster2D<bool> GetShell2D8C(ImageRaster2D<bool> source, bool border_is_shell = false)
        {
            ITopologyElement topology = new TopologyElementRaster2D8Connectivity(source.Raster);
            ImageRaster2D<bool> target = new ImageRaster2D<bool>(source.Raster);
            GetShellRBA(source, topology, target, border_is_shell);
            return target;
        }

        public static ImageRaster3D<bool> GetShell3D6C(ImageRaster3D<bool> source, bool border_is_shell = false)
        {
            ITopologyElement topology = new TopologyElementRaster3D6Connectivity(source.Raster);
            ImageRaster3D<bool> target = new ImageRaster3D<bool>(source.Raster);
            GetShellRBA(source, topology, target, border_is_shell);
            return target;
        }

        //internal static List<int> GetShell6List(ImageRaster3D<bool> image)
        //{
        //    ITopologyElement topology = new TopologyElementRaster3D6Connectivity(image.Raster);
        //    int[] element_neigbour_array = new int[topology.MaximumConnectivity];
        //    List<int> shell_list = new List<int>();

        //    for (int element_index = 0; element_index < image.Raster.ElementCount; element_index++)
        //    {
        //        if (image.GetElementValue(element_index))
        //        {

        //            bool is_shell = false;
        //            topology.ElementNeighboursRBA(element_index, element_neigbour_array);

        //            foreach (int other_element_index in element_neigbour_array)
        //            {
        //                if (other_element_index != -1)
        //                {
        //                    if (!image.GetElementValue(other_element_index))
        //                    {
        //                        is_shell = true;
        //                    }
        //                }
        //            }

        //            if (is_shell)
        //            {
        //                shell_list.Add(element_index);
        //            }
        //        }
        //    }
        //    return shell_list;
        //}

        public static ImageRaster3D<bool> GetShell3D26C(ImageRaster3D<bool> source, bool border_is_shell = false)
        {
            ITopologyElement topology = new TopologyElementRaster3D26Connectivity(source.Raster);
            ImageRaster3D<bool> target = new ImageRaster3D<bool>(source.Raster);
            GetShellRBA(source, topology, target, border_is_shell);
            return target;
        }


        public static ImageRaster4D<bool> GetShell4D8C(ImageRaster4D<bool> source, bool border_is_shell = false)
        {
            ITopologyElement topology = new TopologyElementRaster4D8Connectivity(source.Raster);
            ImageRaster4D<bool> target = new ImageRaster4D<bool>(source.Raster);
            GetShellRBA(source, topology, target, border_is_shell);
            return target;
        }

        public static void GetShellRBA<RasterType>(IImageRaster<RasterType, bool> source, ITopologyElement topology, IImageRaster<RasterType, bool> target, bool border_is_shell)
            where RasterType : IRasterInteger
        {
            int[] element_neigbour_array = new int[topology.MaximumConnectivity];


            for (int element_index = 0; element_index < source.Raster.ElementCount; element_index++)
            {
                if (source.GetElementValue(element_index))
                {

                    bool is_shell = false;
                    topology.ElementNeighboursRBA(element_index, element_neigbour_array);

                    foreach (int other_element_index in element_neigbour_array)
                    {
                        if (other_element_index != -1)
                        {
                            if (!source.GetElementValue(other_element_index))
                            {
                                is_shell = true;
                            }
                        }
                        else
                        {
                            if (border_is_shell)
                            {
                                is_shell = true;
                            }
                        }

                    }

                    if (is_shell)
                    {
                        target.SetElementValue(element_index, true);
                    }
                }
            }
        }

       

        public static ImageRaster3D<bool> FilterComponents3D6(ImageRaster3D<bool> image, int minimal_size)
        {
            ITopologyElement topology = new TopologyElementRaster3D6Connectivity(image.Raster);
            bool[] result = image.GetElementValues(true);
            bool[] to_do = image.GetElementValues(true);
            int[] neighbours = new int[topology.MaximumConnectivity];
            for (int element_index = 0; element_index < image.Raster.ElementCount; element_index++)
            {
                if(to_do[element_index])
                {
                    List<int> connected_component_list = new List<int>();
                    Queue<int> fringe = new Queue<int>();
                    fringe.Enqueue(element_index);
                    to_do[element_index] = false;
                    connected_component_list.Add(element_index);

                    while (fringe.Count != 0)
                    {
                        int next_element = fringe.Dequeue();  
                        topology.ElementNeighboursRBA(next_element, neighbours);
                        for (int neighbour_index = 0; neighbour_index < neighbours.Length; neighbour_index++)
                        {
                            if ((neighbours[neighbour_index] != -1) && (to_do[neighbours[neighbour_index]]))
                            {
                                fringe.Enqueue(neighbours[neighbour_index]);
                                to_do[neighbours[neighbour_index]] = false;                             
                                connected_component_list.Add(neighbours[neighbour_index]);
                            }
                        }
                    }

                    if (connected_component_list.Count < minimal_size)
                    {
                        result.SetValues(connected_component_list, false);
                    }
                }
            }
            return new ImageRaster3D<bool>(image.Raster, result, false);
        }



        public static ImageRaster3D<bool> Intersect(ImageRaster3D<bool> image_0, ImageRaster3D<bool> image_1)
        {
            ImageRaster3D<bool> result = new ImageRaster3D<bool>(image_0.Raster);
            Parallel.For(0, image_0.Raster.ElementCount, element_index =>
            {
                result.SetElementValue(element_index, image_0.GetElementValue(element_index) && image_1.GetElementValue(element_index));
            });
            return result;
        }

       
        public static void DilateRBA(
            ImageRaster3D<bool> source, 
            ImageRaster3D<bool> structuring_element,
            int pivot_index_0, int pivot_index_1, int pivot_index_2,
            ImageRaster3D<bool> target)
        {
            List<int> shell_source = GetShell3D6C(source).GetElementIndexesWithValue(true);
            
            List<int[]> structure_element_offsets = new List<int[]>();
            List<int[]> structure_element_offsets_shell = new List<int[]>();

            // TODO compute the offsets in source space
            Parallel.For(0, source.ElementCount, element_index =>
            {
                target[element_index] = source[element_index];
            });

            // First should be done in full

            // For the rest only the border is relevant
            Parallel.For(1, shell_source.Count, index_index =>
            {
                throw new NotImplementedException();
                
            });
        }
        //public static List<IIntegerRasterIntegerImage2D> split_z(IIntegerRasterIntegerImage3D image)
        //{
        //    int size_x = image.get_size_x();
        //    int size_y = image.get_size_y();
        //    int size_z = image.get_size_z();
        //    List<IIntegerRasterIntegerImage2D> list = new ArrayList<IIntegerRasterIntegerImage2D>();
        //    for (int index_z = 0; index_z < size_z; index_z++)
        //    {
        //        IIntegerRasterIntegerImage2D image_2d = new IntegerRasterIntegerImage2D(size_x, size_y);
        //        for (int index_y = 0; index_y < size_y; index_y++)
        //        {
        //            for (int index_x = 0; index_x < size_x; index_x++)
        //            {
        //                image_2d.set_element_value(index_x, index_y, image.get_element_value(index_x, index_y, index_z));
        //            }
        //        }
        //        list.add(image_2d);
        //    }
        //    return list;

        //}

        //public static IIntegerRasterFloatImage3D flatten_4d_to_3d(IIntegerRasterFloatImage4D float_image_4d, int dimension_index)
        //{
        //    if (float_image_4d.get_size(dimension_index) != 1)
        //    {
        //        throw new RuntimeException("Dimension " + dimension_index + " not of size 1");
        //    }
        //    return new IntegerRasterFloatImage3D(ArrayTools.delete_index(float_image_4d.get_size_array(), dimension_index),
        //        float_image_4d.get_element_array());

        //}

        //public static IIntegerRasterIntegerImage2D crop(IIntegerRasterIntegerImage2D source, int offset_x, int offset_y, int size_x, int size_y)
        //{
        //    IIntegerRasterIntegerImage2D result = new IntegerRasterIntegerImage2D(size_x, size_y);
        //    for (int index_y = 0; index_y < size_y; index_y++)
        //    {
        //        for (int index_x = 0; index_x < size_x; index_x++)
        //        {
        //            result.set_element_value(index_x, index_y, source.get_element_value(offset_x + index_x, offset_y + index_y));
        //        }
        //    }
        //    return result;
        //}
    }
}
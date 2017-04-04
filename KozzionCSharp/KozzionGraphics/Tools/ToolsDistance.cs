using KozzionGraphics.Image;
using KozzionGraphics.Image.Raster;
using KozzionGraphics.Tools;
using KozzionMathematics.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionGraphics.Tools
{
    public class ToolsDistance
    {
        /**************************************************************************
        *
        * Functions for calculating a fast 3D distance transform from binary data.
        * The function returns squared Euclidean distances. It supports
        * anisotropic voxels.
        *
        * This code is based on the chapter 'Separable Distance Transformation
        * and Its Applications' by Coeurjolly and Vacavant, in: Digital Geometry
        * Algorithms, Lecture Notes in Computational Vision and Biomechanics, Vol 2,
        * pp 189-214. DOI: 10.1007/978-94-007-4174-4_7
        * 
        * Copyright by Edwin Bennink, UMC Utrecht
        * H.E.Bennink@umcutrecht.nl
        * September 2015

        * Addapted for c# by Jaap Oosterbroek, UMC Utrecht
        * J.Oosterbroek@umcutrecht.nl
        * December 2015
        *
        *************************************************************************/
        //public static ImageRaster3D<float> DistanceTransform(ImageRaster3D<bool> mask_image, float[] voxel_size)
        //{
        //    ImageRaster3D<float> distance_image = new ImageRaster3D<float>(mask_image.Raster);
        //    DistanceTransformRBA(mask_image, voxel_size, distance_image);
        //    return distance_image;
        //}

        public static ImageRaster3D<float> DistanceTransform3D(ImageRaster3D<bool> mask_image, float[] voxel_size)
        {
            ImageRaster3D<float> distance_image = new ImageRaster3D<float>(mask_image.Raster);
            DistanceTransform3DOosterbroekRBA(mask_image, voxel_size, distance_image);
            return distance_image;
        }


        public static ImageRaster3D<float> DistanceTransformDual(ImageRaster3D<bool> mask_image, float[] voxel_size)
        {
            ImageRaster3D<float> distance_image_positive = new ImageRaster3D<float>(mask_image.Raster);
            ImageRaster3D<float> distance_image_negative = new ImageRaster3D<float>(mask_image.Raster);

            DistanceTransform3DOosterbroekRBA(mask_image, voxel_size, distance_image_positive);
            ImageRaster3D<bool> mask_inverted = ToolsImageRaster.Invert(mask_image);
            DistanceTransform3DOosterbroekRBA(mask_inverted, voxel_size, distance_image_negative);

            Parallel.For(0, mask_image.Raster.ElementCount, index =>
            {
                if (!mask_inverted[index])
                {
                    distance_image_positive[index] = -distance_image_negative[index];
                }
                else
                {
                    distance_image_positive[index] = distance_image_positive[index];
                }

            });
            return distance_image_positive;
        }

        //public static void DistanceTransformRBA(ImageRaster3D<bool> mask_image, ImageRaster3D<float> distance_image, float[] voxel_size)
        //{
        //    int size_x = mask_image.Raster.SizeX;
        //    int size_y = mask_image.Raster.SizeY;
        //    int size_z = mask_image.Raster.SizeZ;
        //    /* Apply the transform in the x-direction. */
        //    Parallel.For(0, size_z * size_y, plane_index =>
        //    {

        //        int z_index = plane_index / size_y;
        //        int y_index = plane_index - z_index * size_y;
        //        int element_index = size_x * ((size_y * z_index) + y_index);

        //        /* Project distances forward. */
        //        float distance = float.MaxValue;
        //        for (int x_index = 0; x_index < size_x; x_index++)
        //        {
        //            distance += voxel_size[0];
        //            /* The voxel value is true; the distance should be 0. */
        //            if (mask_image[element_index + x_index])
        //            {
        //                distance = 0.0f;
        //            }
        //            distance_image[element_index + x_index] = distance;
        //        }

        //        /* Project distances backward. From this point on we don't have to
        //         * read the source data anymore, since all object voxels now have a
        //         * distance assigned. */
        //        distance = float.MaxValue;
        //        for (int x_index = size_x - 1; x_index >= 0; x_index--)
        //        {
        //            distance += voxel_size[0];
        //            /* The voxel value is 0; the distance should be 0 too. */
        //            if (distance_image[element_index + x_index] == 0.0f)
        //            {
        //                distance = 0.0f;
        //            }
        //            /* Calculate the shortest distance in the row. */
        //            distance_image[element_index + x_index] = Math.Min(distance_image[element_index + x_index], distance);
        //        }
        //    });

        //    /* Apply the transform in the y-direction. */
        //    float size_1_sqr = ToolsMath.Sqr(voxel_size[1]);
        //    float size_1_inv = 1.0f / voxel_size[1];

        //    Parallel.For(0, size_z * size_x, plane_index =>
        //    {
        //        int z_index = plane_index / size_x;
        //        int x_index = plane_index - z_index * size_x;
        //        int element_index = z_index * size_y * size_x + x_index;
        //        float[] temp_1 = new float[size_y];
        //        /* Copy the column and square the distances. */
        //        for (int y_index = 0; y_index < size_y; y_index++)
        //        {
        //            temp_1[y_index] = ToolsMath.Sqr(distance_image[element_index + y_index * size_x]);
        //        }

        //        /* Calculate the smallest squared distance in 2D. */
        //        for (int y_index = 0; y_index < size_y; y_index++)
        //        {

        //            /* Calculate the smallest search range, i.e. y-im to y+im. */
        //            float distance = temp_1[y_index];
        //            if (distance == 0)
        //            {
        //                continue;
        //            }
        //            int im = (int)(size_1_inv * ToolsMath.Sqrt(distance));
        //            if (im == 0)
        //            {
        //                continue;
        //            }

        //            for (int j = Math.Max(0, y_index - im); j < Math.Min(size_y, y_index + im); j++)
        //            {
        //                if (temp_1[j] < distance) distance = Math.Min(distance, temp_1[j] + size_1_sqr * ToolsMath.Sqr(j - y_index));
        //            }

        //            distance_image[element_index + y_index * size_x] = distance;
        //        }
        //    });


        //    /* Apply the transform in the z-direction. */
        //    float size_2_sqr = ToolsMath.Sqr(voxel_size[2]);
        //    float size_2_inv = 1.0f / voxel_size[2];

        //    Parallel.For(0, size_y * size_x, plane_index =>
        //    {
        //        float[] temp_2 = new float[size_z];
        //        int y_index = plane_index / size_x;
        //        int x_index = plane_index - (y_index * size_x);
        //        int element_index = y_index * size_x + x_index;

        //        /* Copy the column. */
        //        for (int z_index = 0; z_index < size_z; z_index++)
        //        {
        //            temp_2[z_index] = distance_image[element_index + z_index * size_y * size_x];
        //        }

        //        /* Calculate the smallest squared distance in 3D. */
        //        for (int z_index = 0; z_index < size_z; z_index++)
        //        {
        //            /* Calculate smallest search range, i.e. z-im to z+im. */
        //            float distance = temp_2[z_index];
        //            if (distance == 0)
        //            {
        //                continue;
        //            }
        //            int im = (int)(size_2_inv * ToolsMath.Sqrt(distance));
        //            if (im == 0)
        //            {
        //                continue;
        //            }

        //            for (int j = Math.Max(0, z_index - im); j < Math.Min(size_z, z_index + im); j++)
        //            {
        //                if (temp_2[j] < distance) distance = Math.Min(distance, temp_2[j] + size_2_sqr * ToolsMath.Sqr(j - z_index));
        //            }

        //            distance_image[element_index + z_index * size_x * size_y] = distance;
        //        }
        //    });
        //}

        public static void DistanceTransform3DMediumRBA(ImageRaster3D<bool> mask_image, float[] voxel_size, ImageRaster3D<float> distance_image)
        {
            IRaster3DInteger raster = mask_image.Raster;
            int size_0 = mask_image.Raster.Size0;
            int size_1 = mask_image.Raster.Size1;
            int size_2 = mask_image.Raster.Size2;

            float[] locations_0 = new float[size_0];
            float[] locations_1 = new float[size_1];
            float[] locations_2 = new float[size_2];

            Parallel.For(0, size_0, index_0 =>
            {
                locations_0[index_0] = index_0 * voxel_size[0];
            });
            Parallel.For(0, size_1, index_1 =>
            {
                locations_1[index_1] = index_1 * voxel_size[1];
            });
            Parallel.For(0, size_2, index_2 =>
            {
                locations_2[index_2] = index_2 * voxel_size[2];
            });

            // Apply the transform in the x-direction
            //for (int plane_index = 0; plane_index < size_z * size_y; plane_index++)
            //{
            Parallel.For(0, size_2 * size_1, plane_index =>
            {
                int index_2 = plane_index / size_1;
                int index_1 = plane_index % size_1;

                // Upwards pass
                float added_distance = float.PositiveInfinity;
                for (int index_0 = 0; index_0 < size_0; index_0++)
                {
                    int element_index = raster.GetElementIndex(index_0, index_1, index_2);
                    added_distance += voxel_size[0];
                    /* The voxel value is true; the distance should be 0. */
                    if (mask_image[element_index])
                    {
                        added_distance = 0.0f;
                    }
                    distance_image[element_index] = ToolsMath.Sqr(added_distance);
                }


                if (distance_image.GetElementValue(size_0 - 1, index_1, index_2) < float.PositiveInfinity)
                {
                    // Downwards pass
                    added_distance = float.PositiveInfinity;
                    for (int index_0 = size_0 - 1; index_0 >= 0; index_0--)
                    {
                        int element_index = raster.GetElementIndex(index_0, index_1, index_2);
                        added_distance += voxel_size[0];
                        /* The voxel value is 0; the distance should be 0 too. */
                        if (distance_image[element_index] == 0.0f)
                        {
                            added_distance = 0.0f;
                        }
                        /* Calculate the shortest distance in the row. */
                        distance_image[element_index] = Math.Min(distance_image[element_index], ToolsMath.Sqr(added_distance));
                    }
                }
            });

    
            // Apply the transform in the y-direction
            //for (int plane_index = 0; plane_index < size_0 * size_2; plane_index++)
            //{
            Parallel.For(0, size_2 * size_0, plane_index =>
            {
                int index_0 = plane_index % size_0;
                int index_2 = plane_index / size_0;

                float[] temp_1 = new float[size_1];
                for (int index_1 = 0; index_1 < size_1; index_1++)
                {
                    temp_1[index_1] = distance_image.GetElementValue(index_0, index_1, index_2);
                }

                // Upwards pass
                for (int index_1 = 0; index_1 < size_1; index_1++)
                {
                    int element_index = raster.GetElementIndex(index_0, index_1, index_2);
                    for (int index_1_inner = index_1 + 1; index_1_inner < size_1; index_1_inner++)
                    {
                        if (distance_image.GetElementValue(index_0, index_1_inner, index_2) <= distance_image[element_index])
                        {
                            break;
                        }
                        float distance = distance_image[element_index] + ToolsMath.Sqr((index_1_inner - index_1) * voxel_size[1]);
                        if (distance < temp_1[index_1_inner])
                        {
                            temp_1[index_1_inner] = distance;
                        }
                    }
                }

                //Downwards pass
                for (int index_1 = size_1 - 1; index_1 >= 0; index_1--)
                {
                    int element_index = raster.GetElementIndex(index_0, index_1, index_2);
                    for (int index_1_inner = index_1 - 1; index_1_inner >= 0; index_1_inner--)
                    {
                        if (distance_image.GetElementValue(index_0, index_1_inner, index_2) <= distance_image[element_index])
                        {
                            break;
                        }
                        float distance = distance_image[element_index] + ToolsMath.Sqr((index_1 - index_1_inner) * voxel_size[1]);
                        if (distance < temp_1[index_1_inner])
                        {
                            temp_1[index_1_inner] = distance;
                        }

                    }
                }

                for (int index_1 = 0; index_1 < size_1; index_1++)
                {
                    distance_image.SetElementValue(index_0, index_1, index_2, temp_1[index_1]);
                }
            });


       

            // Apply the transform in the z-direction. */
            //for (int plane_index = 0; plane_index < size_x * size_y; plane_index++)
            //{
            Parallel.For(0, size_1 * size_0, plane_index =>
            {
                int index_0 = plane_index % size_0;
                int index_1 = plane_index / size_0;
                float[] temp_2 = new float[size_2];
                for (int index_2 = 0; index_2 < size_2; index_2++)
                {
                    temp_2[index_2] = distance_image.GetElementValue(index_0, index_1, index_2);
                }

                // Upwards_pass
                for (int index_2 = 0; index_2 < size_2; index_2++)
                {
                    int element_index = raster.GetElementIndex(index_0, index_1, index_2);
                    for (int index_2_inner = index_2 + 1; index_2_inner < size_2; index_2_inner++)
                    {
                        if (distance_image.GetElementValue(index_0, index_1, index_2_inner) <= distance_image[element_index])
                        {
                            break;
                        }
                        float distance = distance_image[element_index] + ToolsMath.Sqr((index_2_inner - index_2) * voxel_size[2]);
                        if (distance < temp_2[index_2_inner])
                        {
                            temp_2[index_2_inner] = distance;
                        }
                    }
                }

                // Downwards pass
                for (int index_2 = size_2 - 1; index_2 >= 0; index_2--)
                {
                    int element_index = raster.GetElementIndex(index_0, index_1, index_2);
                    for (int index_2_inner = index_2 - 1; index_2_inner >= 0; index_2_inner--)
                    {
                        if (distance_image.GetElementValue(index_0, index_1, index_2_inner) <= distance_image[element_index])
                        {
                            break;
                        }
                        float distance = distance_image[element_index] + ToolsMath.Sqr((index_2 - index_2_inner) * voxel_size[2]);
                        if (distance < temp_2[index_2_inner])
                        {
                            temp_2[index_2_inner] = distance;
                        }
                    }
                }

                //Fill and root
                for (int index_2 = 0; index_2 < size_2; index_2++)
                {
                    distance_image.SetElementValue(index_0, index_1, index_2, ToolsMath.Sqrt(temp_2[index_2]));
                }
            });
        }

        public static void DistanceTransform2DSlowRBA(ImageRaster2D<bool> mask_image, float[] voxel_size, ImageRaster2D<float> distance_image)
        {
            IRaster2DInteger raster = mask_image.Raster;
            int size_0 = mask_image.Raster.Size0;
            int size_1 = mask_image.Raster.Size1;

            // Apply the transform in the x-direction
            //for (int plane_index = 0; plane_index < size_z * size_y; plane_index++)
            //{
            Parallel.For(0, size_0, index_0 =>
            {
                for (int index_1 = 0; index_1 < size_1; index_1++)
                {
                    float best_distance = float.MaxValue;
                    if (mask_image.GetElementValue(index_0, index_1))
                    {
                        best_distance = 0;
                    }
                    else
                    {

                        for (int index_0_1 = 0; index_0_1 < size_0; index_0_1++)
                        {
                            for (int index_1_1 = 0; index_1_1 < size_1; index_1_1++)
                            {
                                if (mask_image.GetElementValue(index_0_1, index_1_1))
                                {
                                    float distance = ToolsMath.Sqrt(ToolsMath.Sqr((index_0_1 - index_0) * voxel_size[0]) + ToolsMath.Sqr((index_1_1 - index_1) * voxel_size[0]));
                                    if (distance < best_distance)
                                    {
                                        best_distance = distance;
                                    }
                                }
                            }
                        }
                    }
                    distance_image.SetElementValue(index_0, index_1, best_distance);
                }
            });
        }

        public static void DistanceTransform2DOosterbroekRBA(ImageRaster2D<bool> mask_image, float[] voxel_size, ImageRaster2D<float> distance_image)
        {
            ImageRaster3D<bool> mask_image_3d = new ImageRaster3D<bool>(mask_image.Raster.Size0, mask_image.Raster.Size1, 1, mask_image.GetElementValues(false), false);
            ImageRaster3D<float> distance_image_3d = new ImageRaster3D<float>(mask_image.Raster.Size0, mask_image.Raster.Size1, 1, distance_image.GetElementValues(false), false);
            DistanceTransform3DOosterbroekRBA(mask_image_3d, voxel_size, distance_image_3d);

        }

        public static void DistanceTransform3DOosterbroekRBA(ImageRaster3D<bool> mask_image, float[] voxel_size, ImageRaster3D<float> distance_image_sqr)
        {
            // fetch some initial data
            IRaster3DInteger raster = mask_image.Raster;
            int size_0 = mask_image.Raster.Size0;
            int size_1 = mask_image.Raster.Size1;
            int size_2 = mask_image.Raster.Size2;       
            float [] locations_0 = new float[size_0];
            float [] locations_1 = new float[size_1];
            float [] locations_2 = new float[size_2];
            Parallel.For(0, size_0, index_0 =>
            {
                locations_0[index_0] = index_0 * voxel_size[0];          
            });
            Parallel.For(0, size_1, index_1 =>
            {
                locations_1[index_1] = index_1 * voxel_size[1];
            });
            Parallel.For(0, size_2, index_2 =>
            {
                locations_2[index_2] = index_2 * voxel_size[2];
            });

            //Initials pass for zeros and infs
            Parallel.For(0, distance_image_sqr.ElementCount, element_index =>
            {
                if (mask_image[element_index])
                {
                    distance_image_sqr[element_index] = 0;
                }
                else
                {
                    distance_image_sqr[element_index] = float.PositiveInfinity;
                }
            });

            // Apply the transform in the x-direction
            Parallel.For(0, size_2 * size_1, plane_index =>
            {
                int index_1 = plane_index % size_1;
                int index_2 = plane_index / size_1;

                float[] start = new float[size_0];
                float[] locations = new float[size_0];
                float[] offsets = new float[size_0];

                int curve_index = -1;

                //Curve pass 
                for (int index_0 = 0; index_0 < size_0; index_0++)
                {
                    curve_index = ComputeCurves(distance_image_sqr, index_0, index_1, index_2, index_0, locations_0, start, locations, offsets, curve_index);
                }

                if (curve_index != -1)
                {
                    //if index == -1 then no curves were build and all are positive infinity          
                    for (int index_0 = size_0 - 1; index_0 >= 0; index_0--)
                    {
                        // Compute pass (backwards)
                        curve_index = ComputeDistance(distance_image_sqr, index_0, index_1, index_2, index_0, locations_0, start, locations, offsets, curve_index);
                    }
                }
            });

            // Apply the transform in the y-direction
            Parallel.For(0, size_2 * size_0, plane_index =>
            {
                int index_0 = plane_index % size_0;
                int index_2 = plane_index / size_0;

                float[] start = new float[size_1];
                float[] locations = new float[size_1];
                float[] offsets = new float[size_1];

                int curve_index = -1;

                //Curve pass 
                for (int index_1 = 0; index_1 < size_1; index_1++)
                {
                    curve_index = ComputeCurves(distance_image_sqr, index_0, index_1, index_2, index_1, locations_1, start, locations, offsets, curve_index);
                }

                if (curve_index != -1)
                {
                    //if index == -1 then no curves were build and all are positive infinity
                    for (int index_1 = size_1 - 1; index_1 >= 0; index_1--)
                    {
                        // Compute pass (backwards)
                        curve_index = ComputeDistance(distance_image_sqr, index_0, index_1, index_2, index_1, locations_1, start, locations, offsets, curve_index);
                    }
                }
            });   

            // Apply the transform in the z-direction. */
            Parallel.For(0, size_1 * size_0, plane_index =>
            {
                int index_0 = plane_index % size_0;
                int index_1 = plane_index / size_0;

                float[] start = new float[size_2];
                float[] locations = new float[size_2];
                float[] offsets = new float[size_2];

                int curve_index = -1;

                //Curve pass 
                for (int index_2 = 0; index_2 < size_2; index_2++)
                {
                    curve_index = ComputeCurves(distance_image_sqr, index_0, index_1, index_2, index_2, locations_2, start, locations, offsets, curve_index);
                }

                if (curve_index != -1)
                {
                    //if index == -1 then no curves were build and all are positive infinity
                    // Compute pass (backwards)
                    for (int index_2 = size_2 - 1; index_2 >= 0; index_2--)
                    {
                        // Compute pass (backwards)
                        curve_index = ComputeDistance(distance_image_sqr, index_0, index_1, index_2, index_2, locations_2, start, locations, offsets, curve_index);
                    }
                }
            });

            //Faniak pass for rooting
            Parallel.For(0, distance_image_sqr.ElementCount, element_index =>
            {
                distance_image_sqr[element_index] = ToolsMath.Sqrt(distance_image_sqr[element_index]);
            });
        }



        private static int ComputeCurves(ImageRaster3D<float> distance_image_sqr, int index_0, int index_1, int index_2, int index_axis, float[] locations_axis, float[] curve_start, float[] locations, float[] offsets, int index)
        {
            float location = locations_axis[index_axis];
            float offset = distance_image_sqr.GetElementValue(index_0, index_1, index_2);
            if (offset < float.PositiveInfinity)
            {
                index++;

                while ((index != 0) && (Collide(offsets[index - 1], offset, locations[index - 1], location) <= curve_start[index - 1]))
                {
                    index--;
                }
                locations[index] = location;
                offsets[index] = offset;

                if (index == 0)
                {
                    curve_start[index] = locations_axis[0];
                }
                else
                {
                    curve_start[index] = Collide(offsets[index - 1], offset, locations[index - 1], location);
                }
            }

            return index;
        }

        private static int ComputeDistance(ImageRaster3D<float> distance_image_sqr, int index_0, int index_1, int index_2, int index_axis, float[] locations_axis, float[] start, float[] locations, float[] offsets, int curve_index)
        {
            float location = locations_axis[index_axis];
            while ((curve_index != 0) && (location < start[curve_index]))
            {
                curve_index--;
            }
            distance_image_sqr.SetElementValue(index_0, index_1, index_2, offsets[curve_index] + ToolsMath.Sqr(location - locations[curve_index]));
            return curve_index;
        }

        private static float Collide(float offset_0, float offset_1, float location_0, float location_1)
        {
            return (ToolsMath.Sqr(location_1) - ToolsMath.Sqr(location_0) + offset_1 - offset_0) / (2.0f * (location_1 - location_0));
        }

       
    }
 
}



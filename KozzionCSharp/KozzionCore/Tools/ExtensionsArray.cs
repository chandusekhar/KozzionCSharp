using KozzionCore.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System
{
    public static class ExtensionsArray
    {
      


        public static ArrayType[] Select<ArrayType>(this ArrayType[] source, IList<int> indexes)
        {
            ArrayType[] target = new ArrayType[indexes.Count];
            Parallel.For(0, indexes.Count, index_target =>
            {
                target[index_target] = source[indexes[index_target]];
            });
            return target;
        }

        public static ArrayType[] Select<ArrayType>(this ArrayType[] source, ISet<int> indexes_set)
        {
            List<int> indexes_list = new List<int>(indexes_set);
            indexes_list.Sort();
            return source.Select(indexes_list);
        }

        public static ArrayType[] Select1DIndex0<ArrayType>(this ArrayType[,] array, int index_0)
        {
            int length = array.GetLength(1);
            ArrayType[] row = new ArrayType[length];
            Parallel.For(0, length, column_index =>
            {
                row[column_index] = array[index_0, column_index];
            });         
            return row;
        }        

        public static ArrayType[,] Select2DIndex0<ArrayType>(this ArrayType[,] array, IList<int> indexes_0)
        {
            int length1 = array.GetLength(1);
            ArrayType[,] rows = new ArrayType[indexes_0.Count, length1];
            Parallel.For(0, indexes_0.Count, index_0_target =>
            {
                int index_0_source = indexes_0[index_0_target];
                for (int column_index = 0; column_index < length1; column_index++)
                {             
                    rows[index_0_target, column_index] = array[index_0_source, column_index];
                }
            });
            return rows;
        }

        public static ArrayType[,] Select2DIndex0<ArrayType>(this ArrayType[,] array, int offset_0, int count)
        {
            int length1 = array.GetLength(1);
            ArrayType[,] rows = new ArrayType[count, length1];
            Parallel.For(0, count, index_0_target =>
            {
                int index_0_source = index_0_target + offset_0;
                for (int column_index = 0; column_index < length1; column_index++)
                {
                    rows[index_0_target, column_index] = array[index_0_source, column_index];
                }
            });
            return rows;
        }

        public static ArrayType[,] Select2DIndex1<ArrayType>(this ArrayType[,] array, IList<int> indexes_1)
        {
            int size_0 = array.GetLength(1);
            ArrayType[,] rows = new ArrayType[size_0, indexes_1.Count];
            Parallel.For(0, indexes_1.Count, index_1_target =>
            {
                int index_1_source = indexes_1[index_1_target];
                for (int index0 = 0; index0 < size_0; index0++)
                {
                    rows[index0, index_1_target] = array[index0, index_1_source];
                }
            });
            return rows;
        }

        public static ArrayType[,] Select2DIndex1<ArrayType>(this ArrayType[,] array, int offset_1, int count)
        {
            int size_0 = array.GetLength(0);
            ArrayType[,] rows = new ArrayType[size_0, count];
            Parallel.For(0, count, index_1_target =>
            {
                int index_1_source = index_1_target + offset_1;
                for (int index_0 = 0; index_0 < size_0; index_0++)
                {
                    rows[index_0, index_1_target] = array[index_0, index_1_source];
                }
            });
            return rows;
        }


        public static ArrayType[,] Select2DIndex01<ArrayType>(this ArrayType[,] array, IList<int> indexes_0 , IList<int> indexes_1)
        {
            int length = array.GetLength(1);
            ArrayType[,] rows = new ArrayType[indexes_0.Count, indexes_1.Count];
            Parallel.For(0, indexes_0.Count, index_0_target =>
            {
                int index_0_source = indexes_0[index_0_target];
                for (int index_1_target = 0; index_1_target < indexes_1.Count; index_1_target++)
                {
                    rows[index_0_target, index_1_target] = array[index_0_source, indexes_1[index_1_target]];
                }
            });
            return rows;
        }

        public static ArrayType[,] Select2DIndex0<ArrayType>(this ArrayType[,] array, int index_0)
        {
            int length = array.GetLength(1);
            ArrayType[,] selection = new ArrayType[1, length];  
            Parallel.For(0, length, column_index =>
            {
                selection[0, column_index] = array[index_0, column_index];
            });
         
            return selection;
        }

        public static ArrayType[] Select1DIndex1<ArrayType>(this ArrayType[,] array, int index_1)
        {
            if (array.GetLength(1) <= index_1)
            {
                throw new Exception("Index out of range");
            }

            int length = array.GetLength(0);
            ArrayType[] colmun = new ArrayType[length];
            Parallel.For(0, length, index_0 =>
            {
                colmun[index_0] = array[index_0, index_1];
            });
            return colmun;
        }
        public static void Select3DRBA<ArrayType>(this ArrayType[,,,] source, int dim_0, int dim_1, int dim_2, int[] offsets, ArrayType[,,] target)
        {
            if ((dim_0 == dim_1) || (dim_1 == dim_2) || (dim_0 == dim_1))
            {
                throw new Exception("Dimension must be different");
            }

            int size_0 = source.GetLength(dim_0);
            int size_1 = source.GetLength(dim_1);
            int size_2 = source.GetLength(dim_2);

            Parallel.For(0, size_0, index_0 =>
            {
                int[] source_index = ToolsCollection.Copy(offsets);
                source_index[dim_0] = index_0;
                for (int index_1 = 0; index_1 < size_1; index_1++)
                {
                    source_index[dim_1] = index_1;
                    for (int index_2 = 0; index_2 < size_2; index_2++)
                    {
                        source_index[dim_2] = index_2;
                        target[index_0, index_1, index_2] = source[source_index[0], source_index[1], source_index[2], source_index[3]];
                    }
                }
            });
        }

        public static ArrayType[,,] Select3D<ArrayType>(this ArrayType[,,,] source, int dim_0, int dim_1, int dim_2, int[] offsets)
        {
            if ((dim_0 == dim_1) || (dim_1 == dim_2) || (dim_0 == dim_1))
            {
                throw new Exception("Dimension must be different");
            }

            int size_0 = source.GetLength(dim_0);
            int size_1 = source.GetLength(dim_1);
            int size_2 = source.GetLength(dim_2);
            ArrayType[,,] target = new ArrayType[source.GetLength(dim_0), source.GetLength(dim_1), source.GetLength(dim_2)];

            Select3DRBA<ArrayType>(source, dim_0, dim_1, dim_2, offsets, target);
            return target;
        }

        public static ArrayType[,,] Select3D<ArrayType>(this ArrayType[,,,,] source, int dim_0, int dim_1, int dim_2, int[] offsets)
        {
            if ((dim_0 == dim_1) || (dim_1 == dim_2) || (dim_0 == dim_1))
            {
                throw new Exception("Dimension must be different");
            }

            int size_0 = source.GetLength(dim_0);
            int size_1 = source.GetLength(dim_1);
            int size_2 = source.GetLength(dim_2);
            ArrayType[,,] target = new ArrayType[size_0, size_1, size_2];

            Parallel.For(0, size_0, index_0 =>
            {
                int[] source_index = ToolsCollection.Copy(offsets);
                source_index[dim_0] = index_0;
                for (int index_1 = 0; index_1 < size_1; index_1++)
                {
                    source_index[dim_1] = index_1;
                    for (int index_2 = 0; index_2 < size_2; index_2++)
                    {
                        source_index[dim_2] = index_2;
                        target[index_0, index_1, index_2] = source[source_index[0], source_index[1], source_index[2], source_index[3], source_index[4]];
                    }
                }
            });
            return target;
        }

        public static void SetValues<ArrayType>(this ArrayType[] array, IList<int> indexes, ArrayType value)
        {
            Parallel.For(0, indexes.Count, index_index =>
            {
                array[indexes[index_index]] = value;
            });
        }

        public static void SetValues<ArrayType>(this ArrayType[] array, IList<int> indexes, IList<ArrayType> values)
        {
            if (indexes.Count != values.Count)
            {
                throw new Exception("Sizes do not match");
            }

            Parallel.For(0, indexes.Count, index_index =>
            {
                array[indexes[index_index]] = values[index_index];
            });
        }


        public static void SetValues2D<ArrayType>(this ArrayType[,,] target, ArrayType[,] source, int dim_0, int dim_1, int[] offsets)
        {
            if (dim_0 == dim_1)
            {
                throw new Exception("Target dimensions must be different");
            }

            int size_0 = source.GetLength(0);
            int size_1 = source.GetLength(1);
       

            Parallel.For(0, size_0, index_0 =>
            {
                int[] target_index = ToolsCollection.Copy(offsets);
                target_index[dim_0] = index_0;
                for (int index_1 = 0; index_1 < size_1; index_1++)
                {      
                    target_index[dim_1] = index_1;
                    target[target_index[0], target_index[1], target_index[2]] = source[index_0, index_1];

                }
            });      
        }


        public static void Set1DIndex0<ArrayType>(this ArrayType[,] array, int index_0, ArrayType[] source)
        {
            Parallel.For(0, array.GetLength(1), index_1 =>
            {
                array[index_0, index_1] = source[index_1];
            });
        }

        public static void Set1DIndex1<ArrayType>(this ArrayType[,] array, int index_1, ArrayType[] source)
        {
            Parallel.For(0, array.GetLength(0), index_0 =>
            {
                array[index_0, index_1] = source[index_0];
            });
        }
    }
}

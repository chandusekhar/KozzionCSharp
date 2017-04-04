using KozzionCore.DataStructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System
{
    public static class ExtensionsIList
    {
        public static ArrayType[] Select<ArrayType>(this ArrayType[] source, int offset, int count)
        {
            ArrayType[] target = new ArrayType[count];
            Parallel.For(0, count, index_target =>
            {
                target[index_target] = source[index_target + offset];
            });
            return target;
        }


        public static IList<DataType> Select<DataType>(this IList<DataType> list, IList<int> selection_indexes)
        {
            DataType[] copy = new DataType[selection_indexes.Count];
            for (int index = 0; index < selection_indexes.Count; index++)
            {
                copy[index] = list[selection_indexes[index]];
            }
            return copy;
        }

        public static IReadOnlyList<DataType> AsReadOnly<DataType>(this IList<DataType> collection)
        {
            return new ReadOnlyWrapper<DataType>(collection);
        }     

        public static DataType RemoveLast<DataType>(this IList<DataType> list)
        {
            if (list.Count == 0)
            {
                throw new Exception("List is empty");
            }
            else
            {
                DataType value = list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
                return value;
            }
        }

 

        public static void RemoveAt<DataType>(this List<DataType> list, IList<int> indexes)
        {
            int offset = 0;
            foreach (int index in indexes)
            {
                list.RemoveAt(index - offset);
                offset++;
            }
        }
        
    }
}

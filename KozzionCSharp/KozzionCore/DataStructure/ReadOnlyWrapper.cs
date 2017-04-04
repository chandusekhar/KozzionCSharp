using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure
{
    public sealed class ReadOnlyWrapper<DataType> : IReadOnlyList<DataType>
    {
        private readonly IList<DataType> source;

        public int Count { get { return this.source.Count; } }
        public DataType this[int index] { get { return this.source[index]; } }

        public ReadOnlyWrapper(IList<DataType> source) { this.source = source; }

        public IEnumerator<DataType> GetEnumerator() { return this.source.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
    }
}

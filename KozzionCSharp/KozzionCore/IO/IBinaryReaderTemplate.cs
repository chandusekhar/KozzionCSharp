using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.IO
{
    public interface IBinaryReaderTemplate<DataType>
    {
        IBinaryReader<DataType> Create(BinaryReader reader);
    }
}

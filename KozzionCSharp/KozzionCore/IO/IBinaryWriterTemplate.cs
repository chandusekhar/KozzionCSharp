using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.IO
{
    public interface IBinaryWriterTemplate<DataType>
    {
        IBinaryWriter<DataType> Create(BinaryWriter writer);
    }
}

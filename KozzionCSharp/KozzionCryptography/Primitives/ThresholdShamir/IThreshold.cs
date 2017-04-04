using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCryptography.Primitives.Threshold
{
    public interface IThreshold<KeyType>
    {
        int RequiredKeyCount { get; }
        KeyType Unlock(IList<Tuple<KeyType, KeyType>> Keys);
    }
}

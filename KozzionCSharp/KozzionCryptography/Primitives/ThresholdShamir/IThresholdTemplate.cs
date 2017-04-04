using KozzionMathematics.Algebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCryptography.Primitives.Threshold
{
    public interface IThresholdTemplate<KeyType>
    {
        Tuple<List<KeyType>, IThreshold<KeyType>> Create(KeyType secret, int key_count, int threshold_count);
    }
}

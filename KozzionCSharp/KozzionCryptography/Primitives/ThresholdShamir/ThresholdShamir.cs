using KozzionMathematics.Algebra;
using KozzionMathematics.FiniteField;
using KozzionMathematics.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCryptography.Primitives.Threshold
{
    //https://en.wikipedia.org/wiki/Shamir%27s_Secret_Sharing

    public class ThresholdLagrangeBigInteger : IThreshold<BigInteger>
    {
        private IAlgebraFieldFinite<BigInteger> algebra;
        public int RequiredKeyCount { get; private set; }
        private BigInteger secret_point;
        private IFunction<BigInteger, BigInteger> hashing_function;
        private BigInteger secret_result_hash;

 


        public ThresholdLagrangeBigInteger(IAlgebraFieldFinite<BigInteger> algebra, int required_key_count, BigInteger secret_point, IFunction<BigInteger, BigInteger> hashing_function, BigInteger secret_result_hash)
        {
            this.algebra = algebra;
            this.RequiredKeyCount = required_key_count;
            this.secret_point = secret_point;
            this.hashing_function = hashing_function;
            this.secret_result_hash = secret_result_hash;     
        }


        public BigInteger Unlock(IList<Tuple<BigInteger, BigInteger>> Keys)
        {
            // Check key count
            if (Keys.Count < RequiredKeyCount)
            {
                throw new Exception("Insufficient Keys");
            }

            // Build langrange polynomial out of required keys
            IFunction<BigInteger, BigInteger> polynomial = new FunctionPolynomialLagrange<BigInteger>(new AlgebraSymbolBigInteger(), Keys);

            // Check that excess key also fall on polynomial
            for (int key_index = RequiredKeyCount; key_index < Keys.Count; key_index++)
            {
                if (polynomial.Compute(Keys[key_index].Item1) != Keys[key_index].Item2 )
                {
                    throw new Exception("Key check failed, on or more keys where not consistent");
                }
            }

            // Compute secret
            BigInteger secret_result = polynomial.Compute(secret_point);

            // Check secret hash
            if (hashing_function.Compute(secret_result) != secret_result_hash)
            {
                throw new Exception("Hash check failed, on or more keys where incorrect");
            }

            return secret_result;
        }
    }
}

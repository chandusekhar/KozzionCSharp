using KozzionMathematics.Algebra;

namespace KozzionMathematics.Function.polynomial
{
    public class PolynomialInt32 :
        Polynomial<int>
    {
        public PolynomialInt32(int[] coeffecients) :
            base(new AlgebraSymbolInt32(), coeffecients)
        {
   
        }        

    }    
}

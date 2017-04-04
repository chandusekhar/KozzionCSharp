namespace KozzionMathematics.Function
{
    public interface IFunction<TypeDomain, TypeRange>
    {
        string FunctionType { get;}

        TypeRange Compute(TypeDomain domain_value_0);    
    }

    public interface IFunction<TypeDomain0, TypeDomain1, TypeRange>
    {
        string FunctionType { get; }

        TypeRange Compute(TypeDomain0 domain_value_0, TypeDomain1 domain_value_1);
    }

}

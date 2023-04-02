namespace Encoder;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class RnaPiece : Attribute {
    public RnaPiece(int order, int constructorArgument) : this(order) => ConstructorArgument = constructorArgument;

    public RnaPiece(int order) => Order = order;

    public int Order { get; set; }
    public int? ConstructorArgument { get; set; }
    public bool IsConstructorParam => ConstructorArgument.HasValue;
}

namespace Encoder.Unit.Models;

internal sealed record ConstructorModel([property: RnaPiece(0, 0)] ushort Id, 
    [property: RnaPiece(1, 1)] int Value, 
    [property: RnaPiece(3, 2)] float FloatValue, 
    [property: RnaPiece(4, 3)] string Name);
    

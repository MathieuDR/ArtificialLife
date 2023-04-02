namespace Encoder.Unit.Models;

internal sealed class NullableModel {
    [RnaPiece(0)]
    public int? MyNullableInteger { get; init; }
}
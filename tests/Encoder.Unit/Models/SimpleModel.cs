using FluentAssertions.Equivalency;

namespace Encoder.Unit.Models; 

internal sealed class SimpleModel {
    [RnaPiece(1)]
    public string MyString { get; init; }
    [RnaPiece(2)]
    public decimal MyDecimal { get; init; }
}
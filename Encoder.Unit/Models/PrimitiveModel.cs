namespace Encoder.Unit.Models;

internal sealed class PrimitiveModel {
    [RnaPiece(0)]
    public bool MyBool { get; init; }

    [RnaPiece(1)]
    public byte MyByte { get; init; }

    [RnaPiece(2)]
    public sbyte MySByte { get; init; }

    [RnaPiece(3)]

    public char MyChar { get; init; }

    [RnaPiece(5)]
    public double MyDouble { get; init; }

    [RnaPiece(6)]
    public float MyFloat { get; init; }

    [RnaPiece(7)]
    public int MyInt { get; init; }

    [RnaPiece(8)]
    public uint MyUInt { get; init; }

    [RnaPiece(9)]
    public long MyLong { get; init; }

    [RnaPiece(10)]
    public ulong MyULong { get; init; }

    [RnaPiece(11)]
    public short MyShort { get; init; }

    [RnaPiece(12)]
    public ushort MyUShort { get; init; }
}

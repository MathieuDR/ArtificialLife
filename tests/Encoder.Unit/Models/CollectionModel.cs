namespace Encoder.Unit.Models;

internal sealed class CollectionModel {
    [RnaPiece(0)]
    public int[] MyArray { get; init; }

    [RnaPiece(1)]
    public List<int> MyList { get; init; }

    [RnaPiece(2)]
    public Dictionary<int, string> Dictionary { get; set; }
}

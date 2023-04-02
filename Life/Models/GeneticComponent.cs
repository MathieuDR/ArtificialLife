namespace Life.Models;

internal abstract record GeneticComponent() : IGeneticComponent {
    public byte[] GetBytes() => throw new NotImplementedException();

    public string GetHex() => throw new NotImplementedException();

    public IGeneticComponent FromBytes(byte[] bytes) => throw new NotImplementedException();

    public IGeneticComponent FromHex(string hex) => throw new NotImplementedException();
}

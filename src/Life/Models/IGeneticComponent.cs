namespace Life.Models;

internal interface IGeneticComponent {
    public byte[] GetBytes();
    public string GetHex();
    public IGeneticComponent FromBytes(byte[] bytes);
    public IGeneticComponent FromHex(string hex);
}

// internal interface IGeneticComponent<out T> : IGeneticComponent where T : IGeneticComponent {
//     public T FromBytes(byte[] bytes);
//     public T FromHex(string hex);
// }

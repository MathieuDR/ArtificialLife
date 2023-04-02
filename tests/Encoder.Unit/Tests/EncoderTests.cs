namespace Encoder.Unit.Tests;

public sealed class EncoderTests {
    Func<object, byte[]> _encoderSut = Encoder.Encode;
    
    [Theory]
    [ClassData(typeof(FakeDataProvider))]
    public void Encoder_ShouldNotThrowError_WhenGivenDifferentModels(object toEncode) {
        // assert no error is thrown
        var b = () => _encoderSut.Invoke(toEncode);

        b.Should().NotThrow();
    }
    
    [Theory]
    [ClassData(typeof(FakeDataProvider))]
    public void Encoder_ShouldReturnNonEmptyByteArray_WhenGivenDifferentModels(object toEncode) {
        // assert no error is thrown
        var bytes =  _encoderSut.Invoke(toEncode);

        bytes.Should().NotBeEmpty();
    }
}

using Bogus;

namespace Encoder.Unit.Tests; 

public sealed class DecoderTests {
    Func<byte[], Type, object> _decoderSut = Decoder.Decode;
    Func<object, byte[]> _encoderSut = Encoder.Encode;

    

    [Theory]
    [ClassData(typeof(FakeDataProvider))]
    public void Decoder_ShouldNotThrowError_WhenGivenDifferentModels(object toEncodeModel) {
        //Arrange
        var encoded = _encoderSut.Invoke(toEncodeModel);
        var type = toEncodeModel.GetType();

        //Act
        var b = () => _decoderSut.Invoke(encoded, type);

        //Assert
        b.Should().NotThrow();
    }
    
    [Theory]
    [ClassData(typeof(FakeDataProvider))]
    public void Decoder_ShouldNotGiveBackNullObject_WhenGivenDifferentModels(object toEncodeModel) {
        //Arrange
        var encoded = _encoderSut.Invoke(toEncodeModel);
        var type = toEncodeModel.GetType();

        //Act
        var  decoded = _decoderSut.Invoke(encoded, type);

        //Assert
        decoded.Should().NotBeNull();
    }
    
    [Theory]
    [ClassData(typeof(FakeDataProvider))]
    public void Decoder_ShouldReturnOriginalEncodedObject_WhenGivenDifferentModels(object toEncodeModel) {
        //Arrange
        var encoded = _encoderSut.Invoke(toEncodeModel);
        var type = toEncodeModel.GetType();

        //Act
        var  decoded = _decoderSut.Invoke(encoded, type);

        //Assert
        decoded.Should().BeEquivalentTo(toEncodeModel);
    }
}

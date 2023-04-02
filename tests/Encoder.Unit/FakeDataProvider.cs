using System.Collections;
using Bogus;

namespace Encoder.Unit; 

internal sealed class FakeDataProvider : IEnumerable<object[]> {
    public static IEnumerable<object[]> TestData() {
        Randomizer.Seed = new Random(1981);
        
        yield return new object[]{ FakerProviders.FakeConstructorSimpleModel.Generate() };
        yield return new object[]{ FakerProviders.FakeNullableModel.Generate() };
        yield return new object[]{ FakerProviders.FakeSimpleModel.Generate() };
        yield return new object[]{ FakerProviders.FakePrimitiveModel.Generate() };
        yield return new object[]{ FakerProviders.FakeCollectionModel.Generate() };
        
        yield return new object[]{ FakerProviders.FakeConstructorSimpleModel.Generate() };
        yield return new object[]{ FakerProviders.FakeNullableModel.Generate() };
        yield return new object[]{ FakerProviders.FakeSimpleModel.Generate() };
        yield return new object[]{ FakerProviders.FakePrimitiveModel.Generate() };
        yield return new object[]{ FakerProviders.FakeCollectionModel.Generate() };
    }
    
    public IEnumerator<object[]> GetEnumerator() => TestData().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

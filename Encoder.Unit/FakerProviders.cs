using Bogus;

namespace Encoder.Unit;

internal sealed class FakerProviders {
    public static Faker<ConstructorModel> FakeConstructorSimpleModel { get; } = new Faker<ConstructorModel>()
        .CustomInstantiator(f =>
            new ConstructorModel(f.Random.UShort(), f.Random.Int(), f.Random.Float(), f.Random.String2(10)));

    public static Faker<NullableModel> FakeNullableModel { get; } = new Faker<NullableModel>()
        .RuleFor(x=> x.MyNullableInteger, f=>f.Random.Int().OrNull(f));

    public static Faker<SimpleModel> FakeSimpleModel { get; } = new Faker<SimpleModel>()
        .RuleFor(x => x.MyString, f => f.Random.String2(20))
        .RuleFor(x => x.MyDecimal, f => f.Random.Decimal());

    public static Faker<PrimitiveModel> FakePrimitiveModel { get; } = new Faker<PrimitiveModel>()
        .RuleFor(x => x.MyBool, f => f.Random.Bool())
        .RuleFor(x => x.MyByte, f => f.Random.Byte())
        .RuleFor(x => x.MySByte, f => f.Random.SByte())
        .RuleFor(x => x.MyChar, f => f.Random.Char())
        .RuleFor(x => x.MyDouble, f => f.Random.Double())
        .RuleFor(x => x.MyFloat, f => f.Random.Float())
        .RuleFor(x => x.MyInt, f => f.Random.Int())
        .RuleFor(x => x.MyUInt, f => f.Random.UInt())
        .RuleFor(x => x.MyLong, f => f.Random.Long())
        .RuleFor(x => x.MyULong, f => f.Random.ULong())
        .RuleFor(x => x.MyShort, f => f.Random.Short())
        .RuleFor(x => x.MyUShort, f => f.Random.UShort());

    public static Faker<CollectionModel> FakeCollectionModel { get; } = new Faker<CollectionModel>()
        .RuleFor(x => x.MyArray, f => f.Make(5, () => f.Random.Int()).ToArray())
        .RuleFor(x => x.MyList, f => f.Make(5, () => f.Random.Int()).ToList())
        .RuleFor(x => x.Dictionary, f => f.Make(5, () => (f.Random.Int(), f.Random.Word()))
            .ToDictionary(y=> y.Item1, y => y.Item2));
}

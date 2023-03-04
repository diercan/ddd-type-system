using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Sample.FinanceSystem.Domain.Tests;

public class MemberDataSerializer<T> : IXunitSerializable
{
    public string Description { get; private set; } = string.Empty;
    public T Object { get; private set; } = default!;

    public MemberDataSerializer() { }

    public MemberDataSerializer(string description, T objectToSerialize)
    {
        Description = description;
        Object = objectToSerialize;
    }

    public void Deserialize(IXunitSerializationInfo info)
    {
        T? obj = JsonConvert.DeserializeObject<T>(info.GetValue<string>("objValue"));
        if (obj == null)
            throw new InvalidDataException("Failed to deserialize the test object");

        Object = obj;
        Description = JsonConvert.DeserializeObject<string>(info.GetValue<string>("descValue")) ?? string.Empty;
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        var json = JsonConvert.SerializeObject(Object);
        info.AddValue("objValue", json);

        var descJson = JsonConvert.SerializeObject(Description);
        info.AddValue("descValue", descJson);
    }

    public override string ToString() => Description;
}

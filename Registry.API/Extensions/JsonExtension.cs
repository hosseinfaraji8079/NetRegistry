using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Registry.API.Extensions;

public static class JsonExtension
{
    public static string SerializeModelToJsonObject(this Dictionary<string, string> model)
    {
        return JsonConvert.SerializeObject(model);
    }

    public static string LowercaseContractResolver(this object obj)
    {
        var settings = new JsonSerializerSettings();
        settings.ContractResolver = new LowercaseContractResolver();
        return JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
    }
}

public class LowercaseContractResolver : DefaultContractResolver
{
    protected override string ResolvePropertyName(string propertyName)
    {
        return propertyName.ToLower();
    }
}
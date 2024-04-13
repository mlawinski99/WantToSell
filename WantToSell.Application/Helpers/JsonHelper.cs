using System.Text.Json;
using System.Text.Json.Serialization;

namespace WantToSell.Application.Helpers;

public static class JsonHelper
{
    public static string Serialize<T>(T value)
    {
        if (value is object)
            return SerializeObject(value);
        
        return JsonSerializer.Serialize(value);
    }
    
    private static string SerializeObject(object value)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        return JsonSerializer.Serialize(value, value.GetType(), options);
    }
    
    public static T Deserialize<T>(string value)
    {
        return JsonSerializer.Deserialize<T>(value);
    }
}
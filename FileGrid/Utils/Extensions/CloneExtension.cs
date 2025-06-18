using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileGrid.Utils.Extensions;

public static class CloneExtensions
{
    public static T DeepClone<T>(this T source)
    {
        if (source == null)
            return default!;

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles, // 避免自引用死循环
        };
        string json = JsonSerializer.Serialize(source, options);
        return JsonSerializer.Deserialize<T>(json, options)!;
    }
}
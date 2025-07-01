using System.Reflection;
using System.ComponentModel.DataAnnotations;



public static class EnumExtensions
{
    /// <summary>
    /// This extension method retrieves the display name of an enum value using the DisplayAttribute.
    /// If the enum value has a DisplayAttribute, it returns the name specified in that attribute.
    /// If not, it returns the enum value's name as a string.
    /// Usage example:
    /// var displayName = MyEnum.SomeValue.GetDisplayName();
    /// This is useful for displaying user-friendly names in UI components or logs.
    /// Note: Ensure that the System.ComponentModel.DataAnnotations namespace is included in your project to use this extension method.
    /// </summary>
    public static string GetDisplayName(this Enum value)
    {
        var type = value.GetType();
        var member = type.GetMember(value.ToString()).FirstOrDefault();

        if (member != null)
        {
            var display = member.GetCustomAttribute<DisplayAttribute>();
            if (display != null)
                return display.Name ?? value.ToString();
        }

        return value.ToString(); // fallback
    }
}

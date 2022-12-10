using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CityForum.Shared;

public static class EnumExtensions
{
    /// <summary>
    /// Returns Enum information
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="val"></param>
    /// <returns></returns>
    public static EnumInfo<T> GetEnumInfo<T>(this Enum val) where T : struct
    {
        string enumName = val.ToString();
        var field = val.GetType().GetField(enumName);
        var inf = new EnumInfo<T>()
        {
            Name = val.ToString(),
            Value = (T)Enum.Parse(typeof(T), val.ToString())
        };
        if (field != null)
        {
            var attr = field.GetCustomAttribute<DisplayAttribute>();
            if (attr != null)
            {
                inf.Name = attr.Name;
                inf.ShortName = attr.ShortName;
                inf.Description = attr.Description;
                inf.GroupName = attr.GroupName;
            }
        }
        return inf;
    }
    /// <summary>
    /// Returns Enum description
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static string GetEnumDescription(this Enum val)
    {
        string enumName = val.ToString();
        var field = val.GetType().GetField(enumName);
        var description = "";
        if (field != null)
        {
            var attr = field.GetCustomAttribute<DescriptionAttribute>();
            if (attr != null)
            {
                description = attr.Description;
            }
        }
        return description;
    }
    /// <summary>
    /// Returns Enum name
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static string GetName(this Enum val)
    {
        string enumName = val.ToString();
        var field = val.GetType().GetField(enumName);
        var res = enumName;
        if (field != null)
        {
            var attr = field.GetCustomAttribute<DisplayAttribute>();
            if (attr != null)
            {
                res = attr.Name;
            }
        }
        return res;
    }

    /// <summary>
    /// Returns Enum description
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static string GetDescription(this Enum val)
    {
        string enumName = val.ToString();
        var field = val.GetType().GetField(enumName);
        var res = enumName;
        if (field != null)
        {
            var attr = field.GetCustomAttribute<DisplayAttribute>();
            if (attr != null)
            {
                res = attr.Description;
            }
        }
        return res;
    }

    public static T GetEnumByDisplayValue<T>(string displayValue) where T : struct
    {
        var enumData = Enum.GetValues(typeof(T)).Cast<T>();
        foreach (var item in enumData)
        {
            var info = ((Enum)Enum.Parse(typeof(T), item.ToString())).GetEnumInfo<T>();
            if (info.Name == displayValue)
            {
                return item;
            }
        }
        return default(T);
    }


    public static IEnumerable<EnumInfo<T>> GetValues<T>() where T : struct
    {
        return Enum.GetValues(typeof(T)).Cast<T>().Select(p => ((Enum)Enum.Parse(typeof(T), p.ToString())).GetEnumInfo<T>());
    }
    /// <summary>
    /// Returns enums count
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static int GetLength<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>().Count();
    }

    public class EnumInfo<T> where T : struct
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
        public T Value { get; set; }
    }
}
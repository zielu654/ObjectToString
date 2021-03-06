using System;
using System.Reflection;

public static class ObjectToString
{
    /// <summary>
    /// Change object(class) to string by key(separator)
    /// </summary>
    /// <param name="obj">Object to change</param>
    /// <param name="separator">String of 4 characters (key) represents separators(default ":|{}")</param>
    /// <returns>String to change</returns>
    public static string ObjToStr(Object obj, string? separator = null)
    {
        char separator1 = ':', separator2 = '|', separator3 = '{', separator4 = '}';
        string result = "";
        if (separator != null && separator.Length == 4)
        {
            separator1 = separator[0];
            separator2 = separator[1];
            separator3 = separator[2];
            separator4 = separator[3];
        }

        Type T = obj.GetType();
        var properties = T.GetProperties();
        if (!T.IsClass || T.IsPrimitive || T == typeof(string)) throw new Exception("It isn't a class");
        List<string> strings = new List<string>();
        foreach (var item in properties)
        {
            var propType = item.PropertyType;
            var propValue = item.GetValue(obj);
            var propName = item.Name;
            if (propType == typeof(string) || propType.IsPrimitive)
                result += $"{propName}{separator1}{propValue}{separator2}";
            else
                strings.Add($"{item.Name}" + ObjToStr(item.GetValue(obj), separator));
        }
        result = separator3 + result + separator4;
        foreach (var item in strings)
            result += separator3 + item + separator4;

        return result;
    }
    /// <summary>
    /// Change string to object(class) by key(separator)
    /// </summary>
    /// <typeparam name="T">Type of result object</typeparam>
    /// <param name="str">String to change</param>
    /// <param name="separator">String of 4 characters (key) represents separators(default ":|{}")</param>
    /// <returns>Object of T type</returns>
    public static T StrToObj<T>(string str, string? separator = null)
    {
        char separator1 = ':', separator2 = '|', separator3 = '{', separator4 = '}';
        if (separator != null && separator.Length == 4)
        {
            separator1 = separator[0];
            separator2 = separator[1];
            separator3 = separator[2];
            separator4 = separator[3];
        }

        T result = (T)Activator.CreateInstance(typeof(T));
        if (!result.GetType().IsClass || result.GetType().IsPrimitive || result.GetType() == typeof(string))
            throw new Exception("It isn't a class");

        var gStrings = Split(str, separator3, separator4);
        var strings = gStrings[0].Split(separator2.ToString(), StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in strings)
        {
            if (item.Length < 2) break;
            string name = item.Split(separator1)[0];
            string value = item.Split(separator1)[1];
            var propToUpdate = typeof(T).GetProperty(name);
            var type = propToUpdate.PropertyType;
            if (propToUpdate != null)
                propToUpdate.SetValue(result, Convert.ChangeType(value, type));
        }
        for (int i = 1; i < gStrings.Length && gStrings.Length > 1; i++)
        {
            string name = gStrings[i].Split(separator3, StringSplitOptions.RemoveEmptyEntries)[0];
            string help = gStrings[i][name.Length..];
            Type objType = typeof(T);

            MethodInfo method = typeof(ObjectToString).GetMethod("StrToObj");
            var ty = result.GetType().GetProperties().Where(p => p.Name == name).FirstOrDefault().PropertyType;
            MethodInfo genericMethod = method.MakeGenericMethod(ty);
            var res = genericMethod.Invoke(help, new object[] { help, separator });
            var propertyToUpdate = objType.GetProperty(name);
            if (propertyToUpdate != null)
                propertyToUpdate.SetValue(result, res);
        }

        return result;
    }

    private static string[] Split(string str, char start, char end)
    {
        List<string> result = new List<string>();
        string help = "";
        int deep = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] != end && str[i] != start)
                help += str[i];
            else if (str[i] == start)
            {
                if (deep >= 1)
                    help += str[i];
                deep++;
            }
            else if (str[i] == end)
            {
                if (deep == 1)
                {
                    result.Add(help);
                    help = "";
                }
                else
                    help += str[i];
                deep--;

            }
        }
        return result.ToArray();
    }
}


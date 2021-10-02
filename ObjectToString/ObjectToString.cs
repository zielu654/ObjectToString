using System;
using System.Reflection;
using System.Text.RegularExpressions;

public static class ObjectToString
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj">object to change</param>
    /// <param name="separator">string of 3 characters represents separators(default ":|;")</param>
    /// <returns></returns>
    public static string ObjToStr(Object obj, string? separator = null)
    {
        char separator1 = ':', separator2 = '|', separator3 = ';';
        string result = "";
        if (separator != null && separator.Length == 3)
        {
            separator1 = separator[0];
            separator2 = separator[1];
        }

        Type T = obj.GetType();
        var properties = T.GetProperties();

        List<string> strings = new List<string>();
        foreach (var item in properties)
        {
            var propType = item.PropertyType;
            var propValue = item.GetValue(obj);
            var propName = item.Name;
            if (propType == typeof(string) || propType.IsPrimitive)
            {
                result += $"{propName}{separator1}{propValue}{separator2}";
            }
            else
            {
                strings.Add($"{separator3}{propName}{separator2}" + ObjToStr(item.GetValue(obj), $"{separator1}{separator2}") + separator3);
            }
        }
        foreach (var item in strings)
        {
            result += item;
        }
        return result;
    }
    public static T StrToObj<T>(string str, string? separator = null)
    {
        char separator1 = ':', separator2 = '|', separator3 = ';';
        if (separator != null && separator.Length == 3)
        {
            separator1 = separator[0];
            separator2 = separator[1];
            separator3 = separator[2];
        }
        T result = (T)Activator.CreateInstance(typeof(T));
        var gStrings1 = str.Split(separator3);
        var gStrings = gStrings1.Where(o => o.Length > 0).ToArray();

        var strings = gStrings[0].Split(separator2.ToString());
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
        if(gStrings.Length > 1)
        {
            for (int i = 1; i < gStrings.Length; i++)
            {
                strings = gStrings[i].Split(separator2);
                string name = strings[0];
                string value = "";
                for (int j = 1; j < strings.Length; j++)
                {
                    value += strings[j] + separator2;
                }
                MethodInfo method = typeof(ObjectToString).GetMethod("StrToObj");
                var ty = result.GetType().GetProperties().Where(p => p.Name == name).FirstOrDefault().PropertyType;
                MethodInfo genericMethod = method.MakeGenericMethod(ty);
                var res = genericMethod.Invoke(value,new object[] { value, separator});
                Type objType = typeof(T);

                var propertyToUpdate = objType.GetProperty(name);
                if (propertyToUpdate != null)
                {
                    propertyToUpdate.SetValue(result, res);
                }
            }
        }

        return result;
    }
}


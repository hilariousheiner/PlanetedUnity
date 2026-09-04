using System.Collections.Generic;

namespace Planeted
{
    public static class PLUtils
    {
        public delegate string ToStringFunc<T>(T el); 

        public static string ListToString<T>(List<T> list, ToStringFunc<T> stringFunc, char separator = ',')
        {
            string result = string.Empty;

            if(list.Count >= 1)
            {
                result += stringFunc(list[0]);

                for(int i = 1; i < list.Count; i++)
                {
                    result = result + separator + stringFunc(list[i]);
                }
            }
            return result;
        }
    }
}
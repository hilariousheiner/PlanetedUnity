using System.Collections.Generic;
using UnityEngine;

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

        public delegate Color ColorDelegate(int x, int y);
        public static void FillRect(this Texture2D tex, int x, int y, int w, int h, ColorDelegate colorDelegate)
        {
            int x0 = Mathf.Max(x, 0);
            int y0 = Mathf.Max(y, 0);
            int x1 = Mathf.Min(tex.width, x + w);
            int y1 = Mathf.Min(tex.height, y + h);

            for (int j = y0; j < y1; ++j)
            {
                for (int i = x0; i < x1; ++i)
                {
                    tex.SetPixel(i, j, colorDelegate(i, j));
                }
            }
        }
    }
}
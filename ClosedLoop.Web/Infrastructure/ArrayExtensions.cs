using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClosedLoop.Web.Infrastructure
{
    public static class ArrayExtensions
    {
        public static T[] WithoutLast<T>(this T[] array)
        {
            if (array.Length - 1 <= 0)
            {
                return new T[0];
            }

            T[] newArray = new T[array.Length - 1];
            for (int i = 0; i < array.Length - 1; i++)
            {
                newArray[i] = array[i];
            }
            return newArray;
        }
    }
}
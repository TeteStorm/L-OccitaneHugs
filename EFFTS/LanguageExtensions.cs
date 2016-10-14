﻿using System.Collections.Generic;

namespace EFFTS
{
    /// <summary>
    /// 
    /// </summary>
    public static class LanguageExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool In<T>(this T source, params T[] list)
        {
            return (list as IList<T>).Contains(source);
        }
    }
}

using System.Collections.Generic;

namespace ServiceManagement.Core.Extensions
{
    public static class ListExtensions
    {
        public static t Last<t>(this List<t> list)
        {
            return list[list.Count - 1];
        }
    }
}
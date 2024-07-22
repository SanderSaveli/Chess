using System.Collections.Generic;

namespace OFG.ChessPeak
{
    public static class ExtensionsIReadOnlyCollection
    {
        public static bool IsEmpty<T>(this IReadOnlyCollection<T> collection) => collection.Count == 0;

        public static bool IsNonEmpty<T>(this IReadOnlyCollection<T> collection) => collection.Count != 0;
    }
}

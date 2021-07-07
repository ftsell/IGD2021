using System;
using System.Collections.Generic;

public static class GriddyUtils {
    public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) {
        if (source == null) throw new ArgumentNullException("source");
        if (selector == null) throw new ArgumentNullException("selector");
        var comparer = Comparer<TKey>.Default;

        using (var sourceIterator = source.GetEnumerator()) {
            if (!sourceIterator.MoveNext())
                throw new InvalidOperationException("Sequence contains no elements");

            var min = sourceIterator.Current;
            var minKey = selector(min);
            while (sourceIterator.MoveNext()) {
                var candidate = sourceIterator.Current;
                var candidateProjected = selector(candidate);
                if (comparer.Compare(candidateProjected, minKey) >= 0) continue;

                min = candidate;
                minKey = candidateProjected;
            }
            return min;
        }
    }


    private static Random rng = new Random();

    public static IList<T> Shuffle<T>(this IList<T> list) {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }
}
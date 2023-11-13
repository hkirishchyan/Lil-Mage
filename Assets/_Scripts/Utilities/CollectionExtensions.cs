using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Utilities
{
    public static class CollectionExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
        
        public static int GetClampedIndex<T>(this IList<T> setList, int value)
        {
            if (value > setList.Count - 1) value = 0;
            if (value < 0) value = setList.Count - 1;
            return value;
        }
    
        public static int GetRingIndex<T>(this IList<T> setList, int index)
        {
            if (setList.Count == 0) throw new IndexOutOfRangeException($"{setList} list is empty, no valid Index");
            return (index % setList.Count + setList.Count) % setList.Count;
        }
        
        public static T GetRandomElement<T> (this IList<T> list) => list[Random.Range(0, list.Count)];

        public static T GetRandomElement<T>(this IList<T> list, out int index)
        {
            index = Random.Range(0, list.Count);
            return list[index];
        }
    
        public static T GetRandomElementExcept<T>(this IEnumerable<T> list, IEnumerable<T> except)
        {
            var setList = list.Except(except).ToList();
            return setList[Random.Range(0, setList.Count)];
        }
        
        public static T GetRandomElementExcept<T>(this IEnumerable<T> list, IEnumerable<T> except, out int index)
        {
            var setList = list.Except(except).ToList();
            index = Random.Range(0, setList.Count);
            return setList[index];
        }
        
        public static void DurstenfeldShuffle<T>(this List<T> collection)
        {
            int n = collection.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int k = Random.Range(0, i + 1);
                (collection[k], collection[i]) = (collection[i], collection[k]);
            }
        }
        
        public static void KnuthShuffle<T>(this IList<T> collection)
        {
            int n = collection.Count;
            for (int i = 0; i < n; i++)
            {
                int randomIndex = Random.Range(i, n);
                (collection[randomIndex], collection[i]) = (collection[i], collection[randomIndex]);
            }
        }
        
        public static void LinqShuffle<T>(this IList<T> collection)
        {
             collection.OrderBy(x => Random.Range(0, int.MaxValue)).ToList();
        }
    }
}
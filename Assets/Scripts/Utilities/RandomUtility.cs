using System.Collections.Generic;
using UnityEngine;

namespace TenTen
{
    public class RandomUtility
    {
        public static T GetRandomFromList<T>(List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    
        public static T PullRandomFromList<T>(ref List<T> list)
        {
            int i = Random.Range(0, list.Count);
        
            T result = list[i];
            list.RemoveAt(i);
        
            return result;
        }
    }
}
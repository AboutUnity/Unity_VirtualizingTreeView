using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub
{
    public static class UnityObjectExt
    {
        public static T FindAnyObjectByType<T>() where T : Object
        {
#if UNITY_2023_1_OR_NEWER
            return Object.FindAnyObjectByType<T>();
#else
            return Object.FindObjectOfType<T>();
#endif
        }

        public static Object FindAnyObjectByType(System.Type type)
        {
#if UNITY_2023_1_OR_NEWER
            return Object.FindAnyObjectByType(type);
#else
            return Object.FindObjectOfType(type);
#endif
        }

        public static T[] FindObjectsByType<T>() where T : Object
        {
#if UNITY_6000_0_OR_NEWER
            return Object.FindObjectsByType<T>(FindObjectsSortMode.None);
#else
            return Object.FindObjectsOfType<T>();
#endif
        }

        public static T[] FindObjectsByType<T>(bool includeInactive) where T : Object
        {

#if UNITY_6000_0_OR_NEWER
            return Object.FindObjectsByType<T>(
                includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude,
                FindObjectsSortMode.None);
#else
            return Object.FindObjectsOfType<T>(includeInactive);
#endif
        }
    }
}

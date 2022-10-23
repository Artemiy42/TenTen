using UnityEngine.SceneManagement;

namespace TenTen
{
    public static class SceneExtensions
    {
        public static T FindComponentOfType<T>(this Scene scene, bool includeInactive = false)
        {
            if (!scene.IsValid())
            {
                return default;
            }

            var rootGameObjects = scene.GetRootGameObjects();
            if (rootGameObjects == null)
            {
                return default;
            }
            
            foreach (var rootGameObject in rootGameObjects)
            {
                var component = rootGameObject.GetComponentInChildren<T>(includeInactive);

                if (!Equals(component, default(T)))
                {
                    return component;
                }
            }

            return default;
        }
    }
}
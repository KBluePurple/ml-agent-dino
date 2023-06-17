using System.Collections.Generic;
using UnityEngine;

namespace AchromaticDev.Util.Pooling
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        internal readonly Dictionary<GameObject, PoolObject> PoolObjectCache = new();
        private readonly Dictionary<GameObject, Pool> _prefabDict = new();

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            if (_instance != null) return;

            var go = new GameObject("PoolManager");
            _instance = go.AddComponent<PoolManager>();
            DontDestroyOnLoad(go);
        }

        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation,
            Transform parent = null)
        {
            if (_instance._prefabDict.TryGetValue(prefab, out var cache))
                return cache.GetObject(prefab, position, rotation, parent);

            Debug.Log("PoolManager: No pool found for " + prefab.name);

            _instance._prefabDict.Add(prefab, ScriptableObject.CreateInstance<Pool>());
            _instance._prefabDict[prefab].prefab = prefab;
            _instance._prefabDict[prefab].Initialize(_instance.transform);

            return _instance._prefabDict[prefab].GetObject(prefab, position, rotation, parent);
        }

        public static GameObject Instantiate(GameObject prefab, Transform parent = null)
        {
            return Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
        }

        public static void Destroy(GameObject gameObject)
        {
            if (_instance.PoolObjectCache.TryGetValue(gameObject, out var cache))
            {
                var prefab = cache.pool.prefab;

                if (_instance._prefabDict.TryGetValue(prefab, out var value))
                {
                    value.ReturnObject(gameObject);
                }
                else
                {
                    Debug.LogWarning($"PoolManager: {prefab.name} is not in the pool dictionary.");
                    Object.Destroy(gameObject);
                }
            }
            else
            {
                Debug.LogWarning($"PoolManager: {gameObject.name} is not in the pool cache.");
                Destroy(gameObject);
            }
        }
    }
}
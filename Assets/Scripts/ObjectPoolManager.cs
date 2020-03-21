using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolManager {

    private static ObjectPoolManager instance;

    public static ObjectPoolManager Instance {
        get {
            if (instance == null)
                instance = new ObjectPoolManager();
            return instance;
        }
    }

    private Dictionary<GameObject, ObjectPool> pools;

    private ObjectPoolManager() {
        pools = new Dictionary<GameObject, ObjectPool>();
    }

    public ObjectPool GetObjectPool(GameObject objectPrefab) {
        if (!pools.ContainsKey(objectPrefab))
            pools.Add(objectPrefab, new ObjectPool(objectPrefab));
        return pools[objectPrefab];
    }
}

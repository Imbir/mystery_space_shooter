using System.Collections.Generic;
using UnityEngine;


public class ObjectPool {

    private GameObject objectPrefab;
    private Queue<GameObject> pool;

    public ObjectPool(GameObject objectPrefab) {
        this.objectPrefab = objectPrefab;
        if (objectPrefab.GetComponent<IPoolable>() == null)
            Debug.LogError("Object does not implement IPoolable");

        pool = new Queue<GameObject>();
    }

    public GameObject Get() {
        GameObject pooledObject;

        if (pool.Count == 0) pooledObject = AddNew();
        else pooledObject = pool.Dequeue();

        pooledObject.SetActive(true);

        return pooledObject;
    }

    private GameObject AddNew() {
        GameObject pooledObject = Object.Instantiate(objectPrefab);
        pooledObject.GetComponent<IPoolable>().SetParentPool(this);
        pooledObject.SetActive(false);
        return pooledObject;
    }

    public void Return(GameObject pooledObject) {
        pooledObject.SetActive(false);
        pool.Enqueue(pooledObject);
    }
}

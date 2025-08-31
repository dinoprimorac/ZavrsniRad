using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    // prefab of an object that we want to pool
    private PoolableObject Prefab;

    // list of objects we want to pool
    private List<PoolableObject> AvailableObjects;

    // 
    private ObjectPool(PoolableObject Prefab, int size)
    {
        this.Prefab = Prefab;
        AvailableObjects = new List<PoolableObject>(size);
    }

    public static ObjectPool CreateInstance(PoolableObject Prefab, int size)
    {
        ObjectPool pool = new ObjectPool(Prefab, size);

        return pool;
    }
}

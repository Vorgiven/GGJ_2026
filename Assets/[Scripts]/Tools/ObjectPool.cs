using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct EnumObj<T, S> where T : Object where S : System.Enum
{
    public S Type;
    public T obj;
}
[System.Serializable]
public class ObjectPool<T> where T : Object
{
    public T obj;
    [SerializeField] private int maxPool = 5;
    [SerializeField] private Queue<PoolItem> queueObjects = new Queue<PoolItem>();

    protected System.Action<GameObject> OnPoolSpawned;
    protected ObjectPool() { }

    public ObjectPool(T obj)
    {
        this.obj = obj;
    }
    // Initialize pool objects
    public virtual void Initialize()
    {
        // Instante obj and set inactive
        for (int i = 0; i < maxPool; i++)
        {
            SpawnPoolObject();
        }
    }

    // Spawn new pool object
    protected virtual void SpawnPoolObject()
    {
        T objPool = MonoBehaviour.Instantiate(obj);
        GameObject objPoolGO = (objPool as MonoBehaviour).gameObject;
        //Add PoolItem script
        PoolItem pl = objPoolGO.AddComponent<PoolItem>();
        IPoolItem ipoolItem = objPoolGO.GetComponent<IPoolItem>();
        if (ipoolItem != null)
            pl.Subscribe(ipoolItem.OnInactive);


        pl.Subscribe(OnPoolItemInactive);
        objPoolGO.gameObject.SetActive(false);
        OnPoolSpawned?.Invoke(objPoolGO.gameObject);
    }

    protected void OnPoolItemInactive(PoolItem item)
    {
        queueObjects.Enqueue(item);
    }

    // Get objectfrom pool
    public GameObject GetObj(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        // spawn new item if queue is empty
        if (queueObjects.Count <= 0)
        {
            maxPool++;
            SpawnPoolObject();
        }

        // get the object from queue
        GameObject obj = queueObjects.Dequeue().gameObject;
        // set its position
        obj.gameObject.transform.position = position;
        // and rotation
        obj.gameObject.transform.rotation = rotation;
        // and parenting
        if (parent)
            obj.transform.SetParent(parent);

        // enable object
        obj.SetActive(true);

        return obj;
    }

    public GameObject GetObj() => GetObj(Vector3.zero, Quaternion.identity);

    public T GetObjectByType(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject goSpawn = GetObj(position, rotation, parent);
        return goSpawn.GetComponent<T>();
    }
    public T GetObjectByType()
    {
        GameObject goSpawn = GetObj();
        return goSpawn.GetComponent<T>();
    }
    // Subscribe events
    public void SubscribeEventObjectSpawned(System.Action<GameObject> action)
    {
        OnPoolSpawned += action;
    }
}

public class ObjectPoolGO : ObjectPool<GameObject>
{
    public ObjectPoolGO()
    {
        //obj = gameObject;
    }
    protected override void SpawnPoolObject()
    {
        GameObject objPoolGO = MonoBehaviour.Instantiate(obj);
        //Add PoolItem script
        PoolItem pl = objPoolGO.AddComponent<PoolItem>();
        IPoolItem ipoolItem = objPoolGO.GetComponent<IPoolItem>();
        if (ipoolItem != null)
            pl.Subscribe(ipoolItem.OnInactive);


        pl.Subscribe(OnPoolItemInactive);
        objPoolGO.gameObject.SetActive(false);
        OnPoolSpawned?.Invoke(objPoolGO.gameObject);
    }
}
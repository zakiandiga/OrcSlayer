using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public bool isExpandable;
    }

    public static ObjectPooler poolerInstance;

    private void Awake()
    { 
        if(poolerInstance == null)
            poolerInstance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, this.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log(tag + "has no pool");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue(); //pull from the pool
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.parent = parent;

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(this.transform);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //[SerializeField] private ObjectPooler objectPooler;
    [SerializeField] private int spawnLimit = 20;
    [SerializeField] private Vector2 randomSpawnTime;
    private int currentSpawn;
    [Tooltip("Match the string with the tag in ObjectPooler")]
    public string objectName = "IronOre";
    private float randomX, randomY, randomZ;

    void Start()
    {
        //objectPooler = ObjectPooler.Instance;
        StartCoroutine(SpawnTimer());

    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawnTimer()
    {
        float spawnTime = Random.Range(randomSpawnTime.x, randomSpawnTime.y);

        yield return new WaitForSeconds(spawnTime);
        currentSpawn += 1;
        if (currentSpawn <= spawnLimit)
        {
            SpawnObject();
        }
        else
        {
            StartCoroutine(SpawnTimer());
        }

    }

    private void SpawnObject()
    {
        randomX = transform.position.x + Random.Range((transform.localScale.x / 2) * -1, (transform.localScale.x / 2));
        randomZ = transform.position.z + Random.Range((transform.localScale.z / 2) * -1, (transform.localScale.y / 2));
        randomY = transform.position.y;

        Vector3 randomVector = new Vector3(randomX, transform.position.y, randomZ);
        Vector3 spawnVector;

        Ray ray = new Ray(randomVector, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            randomY = transform.position.y - hit.distance;

            spawnVector = new Vector3(randomX, randomY, randomZ);
            ObjectPooler.poolerInstance.SpawnFromPool(objectName, spawnVector, Quaternion.identity);
            StartCoroutine(SpawnTimer());

            //Debug.Log("Iron Ore SPAWNED");
        }
    }
}
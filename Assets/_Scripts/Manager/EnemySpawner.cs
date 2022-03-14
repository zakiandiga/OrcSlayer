using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float spawnDelay = 2f;
    private string spawnTimer = "Spawn Timer";

    private string enemyName = "Orc";
    [SerializeField] private List<GameObject> activeEnemies;

    private Player currentPlayer;

    private bool canSpawn = true;
    private int activeEnemyCount;
    private int maxActiveEnemy = 1;


    private void Start()
    {
        Timer.Create(SpawnEnemy, spawnDelay, spawnTimer);
    }

    private void OnEnable()
    {
        Player.OnInitializePlayerUI += AssignPlayer;
        EnemyBehaviour.OnEnemySpawn += AssignEnemy;
    }

    private void OnDisable()
    {
        Player.OnInitializePlayerUI -= AssignPlayer;
        EnemyBehaviour.OnEnemySpawn -= AssignEnemy;
    }

    private void AssignPlayer(Player player)
    {
        currentPlayer = player;
        player.OnDies += CleanSpawner;
    }


    private void AssignEnemy (EnemyBehaviour enemy)
    {
        if(!activeEnemies.Contains(enemy.gameObject))
        {
            activeEnemies.Add(enemy.gameObject);
            enemy.GetComponent<IDamageHandler>().OnClearingCorpse += ClearEnemy;
        }
    }

    private void ClearEnemy(GameObject enemy, Vector3 position)
    {
        if(activeEnemies.Contains(enemy)) 
            enemy.GetComponent<IDamageHandler>().OnClearingCorpse -= ClearEnemy;

        ObjectPooler.poolerInstance.ReturnToPool(enemy);
        activeEnemyCount--;

        if(canSpawn && activeEnemyCount < maxActiveEnemy)
            Timer.Create(SpawnEnemy, spawnDelay, spawnTimer);
    }

    private void SpawnEnemy()
    {
        ObjectPooler.poolerInstance.SpawnFromPool(enemyName, transform.position, Quaternion.identity);
        activeEnemyCount++;
    }

    private void CleanSpawner(GameObject player, Vector3 position)
    {
        canSpawn = false;
    }
}

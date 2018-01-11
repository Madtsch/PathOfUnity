using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    private static GameObject enemies;

    public int enemySpawnMin, enemySpawnMax;
    public float enemyDistanceMin, enemyDistanceMax;

    static int allEnemyCount;

    public GameObject spawnPosition;

    Slime enemySlime;

    // Use this for initialization
    void Start()
    {
        // find the Enemies directory where the spawned enemies are stored
        enemies = GameObject.Find("Enemies");
        enemySlime = Resources.Load<Slime>("Prefabs/Enemies/Slime");
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        int enemyCount = Random.Range(enemySpawnMin, enemySpawnMax);
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        float posX = Random.Range(spawnPosition.transform.position.x - Random.Range(enemyDistanceMin, enemyDistanceMax), spawnPosition.transform.position.x + Random.Range(enemyDistanceMin, enemyDistanceMax));
        float posZ = Random.Range(spawnPosition.transform.position.z - Random.Range(enemyDistanceMin, enemyDistanceMax), spawnPosition.transform.position.z + Random.Range(enemyDistanceMin, enemyDistanceMax));
        Debug.Log("ESC - SpawnEnemy - pos: " + posX +"," + spawnPosition.transform.position.y + "," + posZ);
        Slime slimeInstance = (Slime)Instantiate(enemySlime, new Vector3(posX, spawnPosition.transform.position.y, posZ), spawnPosition.transform.rotation);
        slimeInstance.transform.SetParent(enemies.transform, false);
        allEnemyCount++;
        Debug.Log("ESC - SpawnEnemy - allEnemyCount: " + allEnemyCount);
    }
}

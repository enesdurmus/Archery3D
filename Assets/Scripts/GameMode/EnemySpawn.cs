using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] spawnPoints;

    public void Spawn(int spawnNumber)
    {
        if (spawnNumber <= spawnPoints.Length)
        {
            for (int i = 0; i < spawnNumber; i++)
            {
                Instantiate(enemyPrefab, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
            }
        }
    }
}

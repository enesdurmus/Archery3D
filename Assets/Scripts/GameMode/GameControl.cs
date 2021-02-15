using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject waveCanvas;
    public int waveCount = 1;
    private int enemySum;
    private int enemyKillCount = 0;

    private void Start()
    {
        StartWave();
    }

    void Update()
    {
        if (enemySum == enemyKillCount)
        {
            waveCount++;
            StartWave();
        }
    }

    public void SetKillEnemyCount()
    {
        enemyKillCount++;
    }

    private void StartWave()
    {
        GetComponent<EnemySpawn>().Spawn(3 + waveCount * 2);
        enemySum += 3 + waveCount * 2;
        StartCoroutine(PrintWaveInfo());
    }

    private IEnumerator PrintWaveInfo()
    {
        waveCanvas.SetActive(true);
        waveCanvas.GetComponent<TMPro.TextMeshProUGUI>().SetText("Wave " + waveCount);
        yield return new WaitForSeconds(4f);
        waveCanvas.SetActive(false);
    }
}

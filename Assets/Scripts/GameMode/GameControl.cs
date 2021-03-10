using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject waveText;
    public GameObject scoreText;
    public GameObject pausePanel;
    public Animator transitionAnimator;

    public int waveCount = 1;
    private int enemySum;
    private int enemyKillCount = 0;
    private GameObject player;

    private void Start()
    {
        StartWave();
        player = GameObject.FindGameObjectWithTag("Player");
        transitionAnimator.SetTrigger("Start");
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
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Score : " + enemyKillCount);
    }

    private void StartWave()
    {
        GetComponent<EnemySpawn>().Spawn(3 + waveCount * 2);
        enemySum += 3 + waveCount * 2;
        StartCoroutine(PrintWaveInfo());
    }

    private IEnumerator PrintWaveInfo()
    {
        waveText.SetActive(true);
        waveText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Wave " + waveCount);
        yield return new WaitForSeconds(4f);
        waveText.SetActive(false);
    }

    public void SetVolume()
    {
        AudioListener.volume = pausePanel.transform.Find("OptionMenu").transform.Find("Slider").gameObject.GetComponent<Slider>().value;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.volume = 0f;
        player.GetComponent<PlayerController>().StopPlayerControl();
        pausePanel.SetActive(true);
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1f;
        player.GetComponent<PlayerController>().BeginPlayerControl();
        pausePanel.SetActive(false);
    }
}

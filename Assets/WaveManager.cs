using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class WaveData
{
    public int spawnPointID;
    public float preparationTimeSeconds = 10;
    public GameObject[] enemies;
    public int spawnAmount = 10;
    public float spawnDelaySeconds = 1;
    public bool shuffleEnemiesArray;
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TMPTimer;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform destinationPoint;
    [SerializeField] private WaveData[] waves;
    [SerializeField] private bool autoStart = true;

    private int elapsedPreparationSeconds = 0;
    private int currentEnemyId = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (waves.Length == 0 || !autoStart) return;
        StartCoroutine(PreparatonCountdown(0));
    }

    private IEnumerator PreparatonCountdown(int waveID)
    {
        do
        {
            TMPTimer.text = waves[waveID].preparationTimeSeconds - elapsedPreparationSeconds + "";
            yield return new WaitForSeconds(1);
            elapsedPreparationSeconds++;
        } while (elapsedPreparationSeconds < waves[waveID].preparationTimeSeconds);

        StartCoroutine(SpawnWave(waveID));
    }

    private IEnumerator SpawnWave(int waveID)
    {
        WaveData wave = waves[waveID];
        do
        {
            int enemyID = wave.shuffleEnemiesArray 
                ? Random.Range(0,wave.enemies.Length) 
                : currentEnemyId % wave.enemies.Length;
            var enemy = Instantiate(wave.enemies[enemyID], spawnPoints[wave.spawnPointID]);
            enemy.GetComponent<AStarPathfinder>().targetPosition = destinationPoint;
            yield return new WaitForSeconds(wave.spawnDelaySeconds);
        } while (currentEnemyId < wave.spawnAmount);

        if (waveID < waves.Length) StartCoroutine(PreparatonCountdown(waveID + 1));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
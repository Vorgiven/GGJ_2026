using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<WaveInfo> waveInfos = new List<WaveInfo>();
    private int currentWaveIndex = 0;
    private bool atMaxWave = false;
    private void Start()
    {
        GetCurrentWaveInfo().StartWave();
    }
    private void Update()
    {
        GetCurrentWaveInfo().UpdateWave();
        if (!atMaxWave)
        {
            if (GetCurrentWaveInfo().IsDone())
            {
                NextWave();
            }
        }
    }
    private void NextWave()
    {
        if (currentWaveIndex >= waveInfos.Count)
        {
            atMaxWave = true;
            return;
        }
        currentWaveIndex++;
        GetCurrentWaveInfo().StartWave();
    }
    private WaveInfo GetCurrentWaveInfo() => waveInfos[currentWaveIndex];
}

[System.Serializable]
public class WaveInfo
{
    public List<Enemy> enemies = new List<Enemy>();
    public float waveLastTime = 60;
    public MinMaxValueInt spawnDelayMinMax = new MinMaxValueInt(5,10);
    private float waveLastElapsed = 0;
    private float spawnDelayTime = 0;
    private float spawnDelayTimeElapsed = 0;

    public void StartWave()
    {
        RandomizeTimeSpawn();
    }

    public void UpdateWave()
    {
        spawnDelayTimeElapsed += Time.deltaTime;
        if(spawnDelayTimeElapsed>= spawnDelayTime)
        {
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        RandomizeTimeSpawn();
    }
    private void RandomizeTimeSpawn()
    {
        spawnDelayTime = Random.Range(spawnDelayMinMax.minValue, spawnDelayMinMax.maxValue);
    }
    public bool IsDone() => waveLastElapsed >= waveLastTime;
}


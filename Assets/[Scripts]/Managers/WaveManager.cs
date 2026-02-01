using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefeb;
    [SerializeField] private Transform posMove;
    [SerializeField] private Transform posDone;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private float spawnYThreshold = 5;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] public List<Transform> stopPoints = new List<Transform>();
    [SerializeField] private List<WaveInfo> waveInfos = new List<WaveInfo>();
    private int currentWaveIndex = 0;
    private bool atMaxWave = false;
    private void Start()
    {
        // set up spawnpoints for all wave infos and others
        foreach (WaveInfo waveInfo in waveInfos)
        {
            waveInfo.enemyPrefeb = enemyPrefeb;
            waveInfo.spawnPosition = spawnPosition;
            waveInfo.stopPoints = stopPoints;
            waveInfo.spawnYThreshold = spawnYThreshold;
            waveInfo.spawnPoints = spawnPoints;
            waveInfo.posMove = posMove.position;
            waveInfo.posDone = posDone.position;
        }

        // start first wave
        GetCurrentWaveInfo().StartWave();
    }
    private void Update()
    {
        // Update current wave
        GetCurrentWaveInfo().UpdateWave();

        // if NOT at max wave continue checking if wave is done
        if (!atMaxWave)
        {
            // check if wave has completed
            if (GetCurrentWaveInfo().IsDone())
            {
                // Go to next wave
                NextWave();
            }
        }
    }
    private void NextWave()
    {
        if (currentWaveIndex >= waveInfos.Count-1)
        {
            atMaxWave = true;
            return;
        }
        currentWaveIndex++;
        GetCurrentWaveInfo().StartWave();
    }
    private WaveInfo GetCurrentWaveInfo() => waveInfos[currentWaveIndex];
}

// WAVE INFO CLASS THAT STORES ENEMIES, WAVE LAST ETC.

[System.Serializable]
public class WaveInfo
{
    [SerializeField] private MaskGroupData maskGroupToUnlock;
    [HideInInspector] public Enemy enemyPrefeb;
    public List<MaskTypeData> maskTypes = new List<MaskTypeData>();
    public float waveLastTime = 60;
    public MinMaxValueFloat spawnDelayMinMax = new MinMaxValueFloat(5,10);
    private float waveLastElapsed = 0;
    private float spawnDelayTime = 0;
    private float spawnDelayTimeElapsed = 0;

    // values to set in wave manager
    [HideInInspector] public Vector3 spawnPosition;
    [HideInInspector] public float spawnYThreshold = 5;
    [HideInInspector] public List<Transform> spawnPoints = new List<Transform>();
    [HideInInspector] public List<Transform> stopPoints = new List<Transform>();
    public Vector3 posMove = new Vector3(-3, 0, 0);
    public Vector3 posDone = new Vector3(-6, 6, 0);
    public void StartWave()
    {
        RandomizeTimeSpawn();
        GameManager.instance.UnlockMaskGroup(maskGroupToUnlock);
        waveLastElapsed = 0f;
    }
    // Updating wave such as when to spawn enemy
    public void UpdateWave()
    {
        waveLastElapsed += Time.deltaTime;
        spawnDelayTimeElapsed += Time.deltaTime;
        if(spawnDelayTimeElapsed>= spawnDelayTime)
        {
            spawnDelayTimeElapsed = 0f;
            SpawnEnemy();
        }
    }
    private Enemy SpawnEnemy()
    {
        // Spawn enemy at random
        int stopPointIndex = Random.Range(0, stopPoints.Count);
        int maskTypeIndex = Random.Range(0, maskTypes.Count);
        
        Enemy enemySpawn = GameObject.Instantiate(enemyPrefeb);
        enemySpawn.transform.position = new Vector3(spawnPosition.x
            , stopPoints[stopPointIndex].position.y // randomize Y
            , spawnPosition.z);

        enemySpawn.Initialize(maskTypes[maskTypeIndex]);
        enemySpawn.UpdateMovePos(posMove);
        enemySpawn.UpdateDonePos(posDone);
        RandomizeTimeSpawn();
        return enemySpawn;
    }
    // Randomize Spawn timing
    private void RandomizeTimeSpawn()
    {
        spawnDelayTime = Random.Range(spawnDelayMinMax.minValue, spawnDelayMinMax.maxValue);
    }
    public bool IsDone() => waveLastElapsed >= waveLastTime;
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping;
    WaveConfigSO currentWave;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave(){
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves()
    {
        do{
            foreach(WaveConfigSO wave in waveConfigs){
                currentWave = wave;
                for(int i = 0; i<wave.GetEnemyCount(); i++){
                    Instantiate(wave.GetEnemyPrefab(i),
                                wave.GetStartingWayPoint().position,
                                Quaternion.identity,
                                transform);
                    yield return new WaitForSeconds(wave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }while(isLooping);
    }
}

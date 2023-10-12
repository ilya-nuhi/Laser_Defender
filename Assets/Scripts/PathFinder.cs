using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    List<Transform> waypoints;
    int waypointIndex = 0;
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;

    int enemyIndex;
    int stopWayPoint;

    void Awake() {
        enemySpawner = FindObjectOfType<EnemySpawner>();    
    }

    void Start()
    {   
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[waypointIndex].position;
        enemyIndex = enemySpawner.enemyId;
        stopWayPoint = waypoints.Count - waveConfig.GetEnemyCount() + enemyIndex -1;
    }

    void Update()
    {
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        if(waypointIndex < waypoints.Count){
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if(transform.position == targetPosition){
                if(waypointIndex == stopWayPoint){
                    yield return new WaitForSeconds(waveConfig.enemyWaitTime);
                    waypointIndex = waypoints.Count-2;
                }

                waypointIndex++;
                if(waypointIndex == waypoints.Count - waveConfig.GetEnemyCount() -1){
                    waypointIndex += enemyIndex;
                }

            }
        }
        else{
            Destroy(gameObject);
        }
    }
}

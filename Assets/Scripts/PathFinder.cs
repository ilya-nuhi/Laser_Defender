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

    void Awake() {
        enemySpawner = FindObjectOfType<EnemySpawner>();    
    }

    void Start()
    {   
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if(waypointIndex < waypoints.Count){
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if(transform.position == targetPosition){
                waypointIndex++;
            }
        }
        else{
            Destroy(gameObject);
        }
    }
}

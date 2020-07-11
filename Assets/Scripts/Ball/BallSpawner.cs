using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour, ISpawner
{
    public Transform[] spawnPoints;
    public Ball ballPrefab;
    public Transform parent;

    public static int MaxBallNumber = Config.MaxStartNumber;

    public int totalNumber;

    public float timeThreshold;
    private float nextTime = 0;

    public int remainingNumber;

    private void Start()
    {
        remainingNumber = totalNumber;
    }

    private void Update()
    {
        if(Time.time > nextTime && remainingNumber > 0)
        {
            nextTime = Time.time + timeThreshold;
            int number = GetNumberFromPool();
            this.SpawnBall(number, 0, -1);
        }
    }

    private int GetNumberFromPool()
    {
        int number = UnityEngine.Random.Range(Config.MinStartNumber, Config.MaxStartNumber);
        if(number > remainingNumber)
        {
            number = remainingNumber;
        }

        remainingNumber -= number;
        Debug.Log("Random number " + number);
        return number;
    }

    public void SpawnBall(int number, int remaining, int level)
    {
        int randomSpawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        GameObject go = Instantiate(ballPrefab.gameObject, parent);
        go.transform.position = spawnPoints[randomSpawnPointIndex].position;
        ballPrefab.Init(number, remaining, this, level);
    }
}

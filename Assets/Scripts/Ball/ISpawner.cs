using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    void SpawnBall(int number, int remaining, int level);
}

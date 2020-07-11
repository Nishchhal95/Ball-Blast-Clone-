using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config
{
    public static int MaxBallLevel = 4;
    public static int MinBallLevel = 1;

    public static float maxBallScale = 1;
    public static float minBallScale = 0.2f;

    public static float scaleFactor = (maxBallScale - minBallScale) / MaxBallLevel;

    public static int MinStartNumber = 9;
    public static int MaxStartNumber = 100;
}

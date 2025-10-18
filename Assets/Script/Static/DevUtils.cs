using System;
using UnityEngine;

static class DevUtils
{
    private static float s_chanceMax = 100f;

    public static int GetRandomNumber(int min, int max)
    {
        return UnityEngine.Random.Range(Math.Min(min, max), Math.Max(min, max));
    }

    public static float GetRandomNumber(float min, float max)
    {
        return UnityEngine.Random.Range(Math.Min(min, max), Math.Max(min, max));
    }

    public static Vector3 GetRandomVector3(Vector3 min, Vector3 max)
    {
        return new Vector3(
            GetRandomNumber(min.x, max.x),
            GetRandomNumber(min.y, max.y),
            GetRandomNumber(min.z, max.z)
        );
    }

    public static bool IsChanceSuccess(int chancePercent)
    {
        return UnityEngine.Random.value < (chancePercent / s_chanceMax);
    }
}

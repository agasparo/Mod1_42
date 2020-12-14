using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public static float[,] initWave(float[,] waterMap, float height, int large)
    {
        float divi = height / large;
        for (int z = 1; z < waterMap.GetLength(1); z++)
        {
            for (int x = 1; x < waterMap.GetLength(0); x++)
            {
                if (height < 0)
                    height = 0;
                waterMap[x, z] = height;
                if (height <= 0)
                    return (waterMap);
            }
            height -= (divi * (z - 1));
        }
        return (addBorder(waterMap));
    }

    public static float[,] initUniformWave(float[,] waterMap, float height, int large)
    {
        float divi = height / large;
        for (int z = 1; z < large; z++)
        {
            for (int x = 1; x < waterMap.GetLength(0); x++)
            {
                waterMap[x, z] = height;
            }
        }
        return (addBorder(waterMap));
    }

    static float[,] addBorder(float[,] waterMap)
    {
        for (int z = 0; z < waterMap.GetLength(0); z++)
        {
            for (int x = 0; x < waterMap.GetLength(1); x++)
            {
                if (z == 0 || x == 0 || x + 1 == waterMap.GetLength(1) || z + 1 == waterMap.GetLength(0))
                    waterMap[x, z] = 0;
            }
        }
        return (waterMap);
    }
}

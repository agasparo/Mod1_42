using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBase {

    static int diffusion = 3;
    static float[,] noiseMap;
    static float waterHeight = 0.5f;
    static int sizeMap = 100;
    static float heightNone = 0.0f;

    public static void Params()
    {
        noiseMap = MapGenerator.earthMap;
    }

    public static float[,] waterMove(float[,] waterMap, int proc)
    {
        for (int z = proc + diffusion; z >= proc; z--)
        {
            for (int x = 1; x < noiseMap.GetLength(0); x++)
            {
                if (x < sizeMap && z < sizeMap)
                {
                    waterMap[x, z] = waterHorizontal(waterMap, x, z);
                    if (waterMap[x, z] == heightNone && (waterMap[x - 1, z - 1] > noiseMap[x, z] || waterMap[x + 1, z - 1] > noiseMap[x, z]))
                        waterMap[x, z] = waterHeight;
                    if (waterMap[x, z] == heightNone && (waterMap[x - 1, z + 1] > noiseMap[x, z] || waterMap[x + 1, z + 1] > noiseMap[x, z]))
                        waterMap[x, z] = waterHeight;
                }
            }
        }
        for (int z = proc + diffusion; z >= proc; z--)
        {
            for (int x = 1; x < noiseMap.GetLength(0); x++)
            {
                if (x < sizeMap && z < sizeMap)
                {
                    if (waterMap[x, z] == heightNone && (waterMap[x - 1, z - 1] > noiseMap[x, z] || waterMap[x + 1, z - 1] > noiseMap[x, z]))
                        waterMap[x, z] = waterHeight;
                    if (waterMap[x, z] == heightNone && (waterMap[x - 1, z + 1] > noiseMap[x, z] || waterMap[x + 1, z + 1] > noiseMap[x, z]))
                        waterMap[x, z] = waterHeight;
                }
            }
        }
        return (waterMap);
    }

    public static float[,] waterUp(float[,] waterMap, float heightAdd)
    {
        for (int z = 1; z < waterMap.GetLength(1) - 2; z++)
        {
            for (int x = 1; x < waterMap.GetLength(0) - 2; x++)
            {
                waterMap[x, z] += heightAdd; 
            }
        }
        return (waterMap);
    }

    public static float waterHorizontal(float[,] waterMap, int x, int z)
    {
        float waterHeight = waterMap[x, z - 1];
        if (waterHeight < noiseMap[x, z])
            return (heightNone);
        return (waterHeight);
    }
}

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

    public static float[,] waterMove(float[,] waterMap, int proc, float heightBase)
    {
        if (waterHeight != heightBase)
            waterHeight = heightBase;
        waterMap = WaterMoveDesc(waterMap, proc);
        waterMap = WaterMoveAsc(waterMap, proc);
        if (proc > 7)
            waterMap = smootheWater(waterMap, proc - 6);
        return (waterMap);
    }

    public static float[,] smootheWater(float[,] waterMap, int proc)
    {
        for (int z = proc; z <= proc + diffusion; z++)
        {
            for (int x = 1; x < noiseMap.GetLength(0); x++)
            {
                if (x < sizeMap && z < sizeMap)
                {
                    if (waterMap[x, z] == heightNone && isBorder(waterMap, x, z))
                    {
                        for (int i = 0; i < diffusion; i++)
                        {
                            float newHeight = waterHeight - (.01f * i);
                            waterMap[x, z] = newHeight;
                            if (i + x < noiseMap.GetLength(0) && waterMap[x + i, z] != waterHeight)
                                waterMap[x + i, z] = newHeight;
                            if (x - i >= 2 && waterMap[x - i, z] != waterHeight)
                                waterMap[x - i, z] = newHeight;
                            if (i + z < noiseMap.GetLength(1) && waterMap[x, z + i] != waterHeight)
                                waterMap[x, z + i] = newHeight;
                            if (z - i >= 2 && waterMap[x, z - i] != waterHeight)
                                waterMap[x, z - i] = newHeight;
                            if (z - i >= 2 && waterMap[x, z - i] != waterHeight && x - i >= 2 && waterMap[x - i, z - i] != waterHeight)
                                waterMap[x - i, z - i] = newHeight;
                            if (z - i >= 2 && waterMap[x, z - i] != waterHeight && i + x < noiseMap.GetLength(0) && waterMap[x + i, z - i] != waterHeight)
                                waterMap[x + i, z - i] = newHeight;
                        }
                    }
                }
            }
        }
        return (waterMap);
    }

    public static bool isBorder(float[,] waterMap, int x, int z)
    {
        if (waterMap[x - 1, z - 1] == waterHeight)
            return (true);
        if (waterMap[x - 1, z + 1] == waterHeight)
            return (true);
        if (waterMap[x + 1, z - 1] == waterHeight)
            return (true);
        if (waterMap[x + 1, z + 1] == waterHeight)
            return (true);
        return (false);
    }

    public static float[,] WaterMoveAsc(float[,] waterMap, int proc)
    {
        for (int z = proc; z <= proc + diffusion; z++)
        {
            for (int x = 1; x < noiseMap.GetLength(0); x++)
            {
                if (x < sizeMap && z < sizeMap)
                {
                    waterMap[x, z] = waterHorizontal(waterMap, x, z);
                    if (waterMap[x, z] == heightNone)
                    {
                        waterMap[x, z] = Expend(waterMap, x, z);
                    }
                }
            }
        }
        return (waterMap);
    }

    public static float[,] WaterMoveDesc(float[,] waterMap, int proc)
    {
        for (int z = proc + diffusion; z >= proc; z--)
        {
            for (int x = 1; x < noiseMap.GetLength(0); x++)
            {
                if (x < sizeMap && z < sizeMap)
                {
                    waterMap[x, z] = waterHorizontal(waterMap, x, z);
                    if (waterMap[x, z] == heightNone)
                    {
                        waterMap[x, z] = Expend(waterMap, x, z);
                    }
                }
            }
        }
        return (waterMap);
    }

    public static float Expend(float[,] waterMap, int x, int z)
    {
        if (waterMap[x - 1, z - 1] > noiseMap[x, z] && isSource(waterMap[x - 1, z - 1]))
            return (waterHeight);
        if (waterMap[x + 1, z - 1] > noiseMap[x, z] && isSource(waterMap[x + 1, z - 1]))
            return (waterHeight);
        if (waterMap[x - 1, z + 1] > noiseMap[x, z] && isSource(waterMap[x - 1, z + 1]))
            return (waterHeight);
        if (waterMap[x + 1, z + 1] > noiseMap[x, z] && isSource(waterMap[x + 1, z + 1]))
            return(waterHeight);
        return (0f);
    }

    public static bool isSource(float height)
    {
        if (height == waterHeight)
            return (true);
        return (false);
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

    public static float[,] waterDown(float[,] waterMap, float heightAdd)
    {
        for (int z = 0; z < waterMap.GetLength(1); z++)
        {
            for (int x = 0; x < waterMap.GetLength(0); x++)
            {
                waterMap[x, z] += heightAdd;
            }
        }
        return (waterMap);
    }

    public static float waterHorizontal(float[,] waterMap, int x, int z)
    {
        float currentWaterHeight = waterMap[x, z - 1];
        if (currentWaterHeight > waterHeight)
            currentWaterHeight = heightNone;
        if (currentWaterHeight < noiseMap[x, z])
            return (heightNone);
        return (waterHeight);
    }
}

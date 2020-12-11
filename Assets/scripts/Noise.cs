using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {

    	float[,] noiseMap = new float[mapWidth, mapHeight];

    	System.Random prng = new System.Random(seed);
    	Vector2[] octavesOffsets = new Vector2[octaves];
    	for (int i = 0; i < octaves; i++) {

    		float offsetX = prng.Next(-100000, 100000) + offset.x;
    		float offsetY = prng.Next(-100000, 100000) + offset.y;
    		octavesOffsets[i] = new Vector2(offsetX, offsetY);
    	}

    	if (scale <= 0)
    		scale = 0.0001f;

    	float maxNoiseHeight = float.MinValue;
    	float minNoiseHeight = float.MaxValue;

    	float halfWidth = mapWidth / 2f;
    	float halfHeight = mapHeight / 2f;

    	for (int y = 0; y < mapHeight; y++) {

    		for (int x = 0; x < mapWidth; x++) {

    			float amplitude = 1;
    			float frequency = 1;
    			float noiseHeight = 0;

    			for (int i = 0; i < octaves; i++) {
	    			
	    			float sampleX = (x - halfWidth) / scale * frequency + octavesOffsets[i].x;
	    			float sampleY = (y - halfHeight) / scale * frequency + octavesOffsets[i].y;

	    			float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
	    			noiseHeight += perlinValue * amplitude;

	    			amplitude *= persistance;
	    			frequency *= lacunarity;
    			}

    			if (noiseHeight > maxNoiseHeight)
    				maxNoiseHeight = noiseHeight;
    			else if (noiseHeight < minNoiseHeight)
    				minNoiseHeight = noiseHeight;
    			noiseMap[x, y] = noiseHeight;
    		}
    	}

    	for (int y = 0; y < mapHeight; y++) {

    		for (int x = 0; x < mapWidth; x++) {
    			
    			if (x == mapWidth - 1) {
    				noiseMap[x, y] = 0.3f;
    				noiseMap[x - 1, y] = 0.3f;
    			} else if (x == 1) {
    				noiseMap[x, y] = 0.3f;
    				noiseMap[x - 1, y] = 0.3f;
    			} else if (y == 1) {
    				noiseMap[x, y] = 0.3f;
    				noiseMap[x, y - 1] = 0.3f;
    			} else if (y == mapHeight - 1) {
    				noiseMap[x, y] = 0f;
    				noiseMap[x, y - 1] = 0.3f;
    			} else
    				noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
    		}
    	}

    	return (noiseMap);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData
{
   	public float[,] noiseMap;

    public MapData(float[,] map) {

    	this.noiseMap = map;
    }
}

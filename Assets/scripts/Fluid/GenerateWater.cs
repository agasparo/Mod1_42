﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWater : MonoBehaviour {

    static float[,] waterMap;
    static AnimationCurve waterCurve;
    static int processus = 2;
    static int witch = 0;
    static float multiplier = 13f;
    static GameObject Rain;
    static GameObject Cloud;
    static Light Sun;
    static float heightLinear = 0f;

    public static GenerateWater instance;

    void Start()
    {
        instance = this;
    }

    void RemoveWater()
    {
        WaterBase.waterDown(waterMap, -.01f);
        waterDisplay display = FindObjectOfType<waterDisplay>();
        display.DrawMesh(MeshGenerator.GenerateTerrainMesh(waterMap, multiplier, waterCurve));
        if (waterMap[50, 50] <= 0)
        {
            CancelInvoke();
            simulation.isStarted = false;
            Debug.Log("Finish");
            Debug.Log(simulation.isStarted);
        }
    }

    public void DrawMapInEditor()
    {
        if (processus == 2)
        {
            if (heightLinear == 0) {
                waterMap = new float[101, 101];
                WaterBase.Params();
            }
        }
        if (witch == 1)
        {
            if (processus == 2)
            {
                Rain.SetActive(true);
                Cloud.SetActive(true);
                Sun.intensity = .5f;
            }
            waterMap = RainSimulate();

        }
        if (witch == 4)
        {
            if (processus == 2)
                waterMap = Wave.initWave(waterMap, 0.5f, 8);
            waterMap = WaterBase.waterMove(waterMap, processus, 0.5f);
        }
        if (witch == 2)
        {
            if (processus == 2)
            {
                heightLinear += .05f;
                if (heightLinear <= 1)
                    waterMap = Wave.initUniformWave(waterMap, heightLinear, 3);
            }
            if (heightLinear <= 1)
                waterMap = WaterBase.waterMove(waterMap, processus, heightLinear);
            if (processus >= 102)
                processus = 1;
        }
        processus++;
        waterDisplay display = FindObjectOfType<waterDisplay>();
        display.DrawMesh(MeshGenerator.GenerateTerrainMesh(waterMap, multiplier, waterCurve));
        if ((waterMap[1, 1] >= 1 && witch == 1) || (processus >= 100 && witch == 4) || (heightLinear >= 1.05 && witch == 2))
        {
            instance.CancelInvoke();
            if (Rain.activeSelf)
            {
                Rain.SetActive(false);
                Cloud.SetActive(false);
                Sun.intensity = 1f;
            }
            Debug.Log("Finish add water");
            InvokeRepeating("RemoveWater", 30f, 0.1f);
        }
    }

    public float[,] RainSimulate()
    {
        if (processus == 2)
            waterMap = new float[101, 101];
        waterMap = WaterBase.waterUp(waterMap, .01f);
        return (waterMap);
    }

    public static void Simulate(AnimationCurve waterCurveSimulate, int proc, int type, float begin, float refreshTime, GameObject RainPart, GameObject CloudPart, Light SunPart)
    {
        waterCurve = waterCurveSimulate;
        processus = proc;
        witch = type;
        Rain = RainPart;
        Cloud = CloudPart;
        Sun = SunPart;
        if (instance)
            instance.InvokeRepeating("DrawMapInEditor", begin, refreshTime);
    }
}

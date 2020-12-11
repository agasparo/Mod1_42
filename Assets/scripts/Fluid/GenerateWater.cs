using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWater : MonoBehaviour {

    static float[,] waterMap;
    static AnimationCurve waterCurve;
    static int processus = 2;

    public static GenerateWater instance;

    void Start()
    {
        instance = this;
    }

    public void DrawMapInEditor()
    {
        if (processus == 2)
        {
            waterMap = new float[101, 101];
            waterMap = Wave.initWave(waterMap, 0.5f, 5);
            WaterBase.Params();
        }
        waterMap = WaterBase.waterMove(waterMap, processus);
        processus++;
        waterDisplay display = FindObjectOfType<waterDisplay>();
        display.DrawMesh(MeshGenerator.GenerateTerrainMesh(waterMap, 13.59f, waterCurve));
    }

    public static void Simulate(AnimationCurve waterCurveSimulate, int proc)
    {
        waterCurve = waterCurveSimulate;
        processus = proc;
        if (instance)
            instance.InvokeRepeating("DrawMapInEditor", 10f, 1f);
    }
}

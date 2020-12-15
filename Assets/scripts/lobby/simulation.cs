using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class simulation : MonoBehaviour
{
    public static int SimulationType = 3;
    public static bool isStarted = false;
    public static int SimulationActive = 0;
    public AnimationCurve waterCurve;

    Transform StartBtn;
    GameObject StartChoice;
    public GameObject RainPart;
    public GameObject CloudPart;
    public Light SunPart;
    public Material LampOn;
    public Material LampOff;
    public Material SunTime;
    public Material RainTime;
    public GameObject TextStart;
    public Text SimulationInfos;

    float timer = 0f;
    float time = 0.5f;
    bool timerReached = true;

    private void Start()
    {
        StartBtn = GameObject.FindGameObjectWithTag("btnStart").transform;
        StartChoice = GameObject.FindGameObjectWithTag("StartChoice");
    }

    void OnMouseDown()
    {
        if (!isStarted && SimulationType != 3)
        {
            SimulationActive = SimulationType;
            SimulationType = 3;
            timerReached = false;
            timer = 0;
            isStarted = true;
            StartChoice.GetComponent<MeshRenderer>().material = LampOn;
            Vector3 currentPos = StartBtn.position;
            StartBtn.position = new Vector3(currentPos.x, currentPos.y, currentPos.z + 0.05f);
        }
    }

    private void initSimulation()
    {
        SimulationInfos.text = "Simulation start in 10 s";
        TextStart.SetActive(true);
        InvokeRepeating("HideText", 5f, 10f);
        if (SimulationActive == 1)
            GenerateWater.Simulate(waterCurve, 2, SimulationActive, 10f, 1f, RainPart, CloudPart, SunPart, SunTime, RainTime, TextStart, SimulationInfos);
        else if (SimulationActive == 4)
            GenerateWater.Simulate(waterCurve, 2, SimulationActive, 10f, .13f, RainPart, CloudPart, SunPart, SunTime, RainTime, TextStart, SimulationInfos);
        else if (SimulationActive == 2)
            GenerateWater.Simulate(waterCurve, 2, SimulationActive, 10f, .1f, RainPart, CloudPart, SunPart, SunTime, RainTime, TextStart, SimulationInfos);
    }

    void HideText()
    {
        TextStart.SetActive(false);
        CancelInvoke();
    }

    private void Update()
    {
        if (!isStarted && SimulationActive > 0)
        {
            StartChoice.GetComponent<MeshRenderer>().material = LampOff;
            SimulationActive = 0;
            SimulationType = 3;
        }
        if (SimulationType == 3)
        {
            if (!timerReached)
                timer += Time.deltaTime;
            if (!timerReached && timer > time)
            {
                Vector3 currentPos = StartBtn.position;
                StartBtn.position = new Vector3(currentPos.x, currentPos.y, currentPos.z - 0.05f);
                timerReached = true;
                initSimulation();
            }
        }
    }
}

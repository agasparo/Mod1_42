using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simulation : MonoBehaviour
{
    public static int SimulationType = 3;
    public static bool isStarted = false;
    public AnimationCurve waterCurve;

    Transform StartBtn;
    GameObject StartChoice;
    public Material LampOn;

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
        if (!isStarted)
        {
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
        Debug.Log("begin in 10 s");
        GenerateWater.Simulate(waterCurve, 2);
    }

    private void Update()
    {
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

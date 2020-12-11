using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    Transform RainBtn;
    GameObject RainChoice;
    public Material LampOn;
    public Material LampOff;

    float timer = 0f;
    float time = 0.5f;

    bool timerReached = true;
    bool isChange = false;

    private void Start()
    {
        RainBtn = GameObject.FindGameObjectWithTag("btnRain").transform;
        RainChoice = GameObject.FindGameObjectWithTag("Rainchoice");
    }
    void OnMouseDown()
    {
        if (!simulation.isStarted)
        {
            simulation.SimulationType = 1;
            timerReached = false;
            timer = 0;
            Vector3 currentPos = RainBtn.position;
            RainChoice.GetComponent<MeshRenderer>().material = LampOn;
            RainBtn.position = new Vector3(currentPos.x, currentPos.y, currentPos.z + 0.05f);
        }
    }

    private void Update()
    {
        if (simulation.SimulationType == 1)
        {
            if (!timerReached)
                timer += Time.deltaTime;
            if (!timerReached && timer > time)
            {
                Vector3 currentPos = RainBtn.position;
                RainBtn.position = new Vector3(currentPos.x, currentPos.y, currentPos.z - 0.05f);
                timerReached = true;
                isChange = true;
            }
        }
        else
        {
            if (isChange)
            {
                RainChoice.GetComponent<MeshRenderer>().material = LampOff;
                isChange = false;
            }
        }
    }
}

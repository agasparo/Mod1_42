using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsunami : MonoBehaviour
{
    Transform TsunamiBtn;
    GameObject TsunamiChoice;
    public Material LampOn;
    public Material LampOff;

    float timer = 0f;
    float time = 0.5f;
    
    bool timerReached = true;
    bool isChange = false;
    
    private void Start()
    {
        TsunamiBtn = GameObject.FindGameObjectWithTag("btnTsunami").transform;
        TsunamiChoice = GameObject.FindGameObjectWithTag("Tsunamichoice");
    }
    void OnMouseDown()
    {
        if (!simulation.isStarted)
        {
            simulation.SimulationType = 0;
            timerReached = false;
            timer = 0;
            Vector3 currentPos = TsunamiBtn.position;
            TsunamiChoice.GetComponent<MeshRenderer>().material = LampOn;
            TsunamiBtn.position = new Vector3(currentPos.x, currentPos.y, currentPos.z + 0.05f);
        }
    }

    private void Update()
    {
        if (simulation.SimulationType == 0)
        {
            if (!timerReached)
                timer += Time.deltaTime;
            if (!timerReached && timer > time)
            {
                Vector3 currentPos = TsunamiBtn.position;
                TsunamiBtn.position = new Vector3(currentPos.x, currentPos.y, currentPos.z - 0.05f);
                timerReached = true;
                isChange = true;
            }
        } else {
            if (isChange)
            {
                TsunamiChoice.GetComponent<MeshRenderer>().material = LampOff;
                isChange = false;
            }
        }
    }
}

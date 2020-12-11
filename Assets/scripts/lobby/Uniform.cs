using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uniform : MonoBehaviour
{
    Transform UniformBtn;
    GameObject UniformChoice;
    public Material LampOn;
    public Material LampOff;

    float timer = 0f;
    float time = 0.5f;

    bool timerReached = true;
    bool isChange = false;

    private void Start()
    {
        UniformBtn = GameObject.FindGameObjectWithTag("btnUniform").transform;
        UniformChoice = GameObject.FindGameObjectWithTag("Uniformchoice");
    }
    void OnMouseDown()
    {
        if (!simulation.isStarted)
        {
            simulation.SimulationType = 2;
            timerReached = false;
            timer = 0;
            Vector3 currentPos = UniformBtn.position;
            UniformChoice.GetComponent<MeshRenderer>().material = LampOn;
            UniformBtn.position = new Vector3(currentPos.x, currentPos.y, currentPos.z + 0.05f);
        }
    }

    private void Update()
    {
        if (simulation.SimulationType == 2)
        {
            if (!timerReached)
                timer += Time.deltaTime;
            if (!timerReached && timer > time)
            {
                Vector3 currentPos = UniformBtn.position;
                UniformBtn.position = new Vector3(currentPos.x, currentPos.y, currentPos.z - 0.05f);
                timerReached = true;
                isChange = true;
            }
        }
        else
        {
            if (isChange)
            {
                UniformChoice.GetComponent<MeshRenderer>().material = LampOff;
                isChange = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCOunter : MonoBehaviour {

    float timeCounter;
    // Use this for initialization
    private void OnEnable()
    {
        timeCounter = 0;
    }
    private void Start()
    {
        InvokeRepeating("DisplayTime", 1, 1);
    }
    public void DisplayTime()
    {
        Debug.Log("Time : "+timeCounter);

    }

    // Update is called once per frame
    void Update () {

        timeCounter += Time.deltaTime;
        
	}
}

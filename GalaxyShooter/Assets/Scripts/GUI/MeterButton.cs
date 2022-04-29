using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterButton : MonoBehaviour
{
    public MeterScript progressMeter; // this allows you to link this script to the meter's script via the inspector.
    public int currentProgress; // defines the variable you'd like to keep track of.
    public int maxProgress = 80; // defines the maximum value of your variable.


    void Start()
    {
        currentProgress = 0; // sets your variable to 0 from the start.
        //progressMeter.SetMaxProgress(currentProgress); // sets your meter's fill to maximum from the start.

    }

    private void Update()
    {
        progressMeter.SetProgress(currentProgress); // links your variable to the meter's fill.
        //Debug.Log(currentProgress);
    }
}

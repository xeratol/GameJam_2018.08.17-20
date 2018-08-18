using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedObjectManager : MonoBehaviour {

    private ArrayList TimedObjectList;
    private float CurrentTime;
    private float TimeStep;

    //lerping the time for smoothness :3
    private float TargetTime;
    private float alpha;
    private float lerpspeed;

    private Text Timer;

	// Use this for initialization
	void Awake () {
        TimedObjectList = new ArrayList();

        CurrentTime = 0f;
        TimeStep = 1f;

        TargetTime = 0f;
        alpha = 1f;
        lerpspeed = 0.5f;

        Timer = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!Mathf.Equals(alpha, 1f))
        {
            alpha += Time.deltaTime / lerpspeed;
            if (alpha > 1f)
            {
                alpha = 1f;
            }

            CurrentTime = Mathf.Lerp(CurrentTime, TargetTime, alpha);
            Timer.text = TimerTime(CurrentTime);

            foreach (GameObject g in TimedObjectList)
            {
                g.GetComponent<FrozenTimeBehavior>().MoveWithTime(CurrentTime);
            }
        }
    }

    public void RegisterObject (GameObject TimedObject)
    {
        TimedObjectList.Add(TimedObject);
    }

    public void MoveTime (bool forward)
    {
        TargetTime += (forward ? .5f : -.5f) * TimeStep;
        alpha = 0f; // interpolate
    }

    private string TimerTime (float f)
    {
        bool nega = f < 0;
        f *= nega ? -1f : 1f;
        int minutes = (int)f / 60;
        int seconds = (int)f - minutes * 60;
        int milli = (int)(f * 100f) % 100;

        return ((nega ? "-" : "") + minutes.ToString("D1") + ":" + seconds.ToString("D2") + ":" + milli.ToString("D2"));
    }
}

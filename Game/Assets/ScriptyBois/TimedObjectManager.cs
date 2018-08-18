using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectManager : MonoBehaviour {

    private ArrayList TimedObjectList;
    private float CurrentTime;
    private float TimeStep;

    private float TimeSpeed;
    private float TimeFriction;

	// Use this for initialization
	void Awake () {
        TimedObjectList = new ArrayList();
        CurrentTime = 0;
        TimeStep = 1;
        TimeSpeed = 0;
        TimeFriction = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
        //if (Mathf.Abs(TimeSpeed) < 1f / 2048f)
        //{
        //    return;
        //}

        //CurrentTime += TimeSpeed;
        //if (CurrentTime < 0)
        //{
        //    CurrentTime = 0;
        //}
        
        //foreach (GameObject g in TimedObjectList)
        //{
        //    g.GetComponent<FrozenTimeBehavior>().MoveTime(CurrentTime);
        //}
        //TimeSpeed -= TimeSpeed * TimeFriction;
    }

    public void RegisterObject (GameObject TimedObject)
    {
        TimedObjectList.Add(TimedObject);
    }

    public void MoveTime (bool forward)
    {
        //TimeSpeed += (forward ? .1f : -.1f) * TimeStep;

        CurrentTime += (forward ? .5f : -.5f) * TimeStep;
        CurrentTime = (CurrentTime < 0f ? 0f : CurrentTime);

        foreach (GameObject g in TimedObjectList)
        {
            g.GetComponent<FrozenTimeBehavior>().SetTime(CurrentTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    private TimedObjectManager tom;

	// Use this for initialization
	void Start () {
        tom = GameObject.Find("TimedObjectManager").GetComponent<TimedObjectManager>();
    }
	
	// Update is called once per frame
	void Update () {
        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
        {
            // scroll up
            tom.MoveTime(true);
        }
        else if (d < 0f)
        {
            // scroll down
            tom.MoveTime(false);
        }
    }
}

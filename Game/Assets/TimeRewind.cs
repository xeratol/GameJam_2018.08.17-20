using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewind : MonoBehaviour {

    public Transform prefabBlue;
    public Transform prefabGreen;
    public Transform prefabRed;

    public Transform prefabBlueClone;
    public Transform prefabGreenClone;
    public Transform prefabRedClone;

    public GameObject player;

    bool allcolors;
    int currentColor = 0;
    int order = 0;

    // Use this for initialization
    void Start ()
    {
		player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("q")) {
            Debug.Log("Q pressed and current color is " + currentColor);
            QuantumRift();
        }

        if (Input.GetKeyDown("e"))
        {
            //Debug.Log("E pressed");
        }

        if (Input.GetKeyDown("r"))
        {
            Debug.Log("R pressed and current color is " + currentColor + " while order is " + order );
            Rewind();
        }

        if (Input.GetKeyDown("f"))
        {
            Debug.Log("F pressed  and current color is " + currentColor + " while order is " + order );
            Forward();
        }
    }

    void QuantumRift()
    {
        if(prefabBlueClone == null)
        {
            prefabBlueClone = Instantiate(prefabBlue, transform.position, transform.rotation);
            currentColor = 4;
        }
        else if(prefabGreenClone == null)
        {
            prefabGreenClone = Instantiate(prefabGreen, transform.position, transform.rotation);
            currentColor = 5;
        }
        else if (prefabRedClone == null)
        {
            prefabRedClone = Instantiate(prefabRed, transform.position, transform.rotation);
            allcolors = true;
            currentColor = 1;
            return;
        }

        if (allcolors) {
            if (currentColor == 1)
            {
                currentColor++;
                Destroy(prefabBlueClone.gameObject);
                prefabBlueClone = Instantiate(prefabBlue, transform.position, transform.rotation);
                order = 0;
            }
            else if (currentColor == 2)
            {
                currentColor++;
                Destroy(prefabGreenClone.gameObject);
                prefabGreenClone = Instantiate(prefabGreen, transform.position, transform.rotation);
                order = 0;
            }
            else if (currentColor == 3)
            {
                currentColor = 1;
                Destroy(prefabRedClone.gameObject);
                prefabRedClone = Instantiate(prefabRed, transform.position, transform.rotation);
                order = 0;
            }
        }
    }

    void Rewind()
    {
        switch (currentColor) {
            case 1:
                switch (order) {
                    case 0:
                        player.transform.position = prefabRedClone.position;
                        order = 1;
                        break;
                    case 1:
                        player.transform.position = prefabGreenClone.position;
                        order = 2;
                        break;
                    case 2:
                        player.transform.position = prefabBlueClone.position;
                        order = 0; 
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (order)
                {
                    case 0:
                        player.transform.position = prefabGreenClone.position;
                        order = 1;
                        break;
                    case 1:
                        player.transform.position = prefabBlueClone.position;
                        order = 2;
                        break;
                    case 2:
                        player.transform.position = prefabRedClone.position;
                        order = 0;
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (order)
                {
                    case 0:
                        player.transform.position = prefabBlueClone.position;
                        order = 1;
                        break;
                    case 1:
                        player.transform.position = prefabRedClone.position;
                        order = 2;
                        break;
                    case 2:
                        player.transform.position = prefabGreenClone.position;
                        order = 0;
                        break;
                    default:
                        break;
                }
                break;
            case 4:
                player.transform.position = prefabBlueClone.position;
                break;
            case 5:
                if (order == 0)
                {
                    player.transform.position = prefabGreenClone.position;
                    order++;
                }
                else {
                    player.transform.position = prefabBlueClone.position;
                    order--;
                }
                break;
            default:
                Debug.Log("No rifts yet");
                break;
        }

    }

    void Forward()
    {
        switch (currentColor)
        {
            case 1:
                switch (order)
                {
                    case 0:
                        player.transform.position = prefabRedClone.position;
                        order = 2;
                        break;
                    case 1:
                        player.transform.position = prefabGreenClone.position;
                        order = 0;
                        break;
                    case 2:
                        player.transform.position = prefabBlueClone.position;
                        order = 1;
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (order)
                {
                    case 0:
                        player.transform.position = prefabGreenClone.position;
                        order = 2;
                        break;
                    case 1:
                        player.transform.position = prefabBlueClone.position;
                        order = 0;
                        break;
                    case 2:
                        player.transform.position = prefabRedClone.position;
                        order = 1;
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (order)
                {
                    case 0:
                        player.transform.position = prefabBlueClone.position;
                        order = 2;
                        break;
                    case 1:
                        player.transform.position = prefabRedClone.position;
                        order = 1;
                        break;
                    case 2:
                        player.transform.position = prefabGreenClone.position;
                        order = 0;
                        break;
                    default:
                        break;
                }
                break;
            case 4:
                player.transform.position = prefabBlueClone.position;
                break;
            case 5:
                if (order == 0)
                {
                    player.transform.position = prefabGreenClone.position;
                    order++;
                }
                else
                {
                    player.transform.position = prefabBlueClone.position;
                    order--;
                }
                break;
            default:
                Debug.Log("No rifts yet");
                break;
        }
    }
}

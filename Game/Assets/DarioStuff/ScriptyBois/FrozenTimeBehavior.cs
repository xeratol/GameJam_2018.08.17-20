using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenTimeBehavior : MonoBehaviour {

    [SerializeField] private float ActionTimeFrom;
    [SerializeField] private float ActionTimeTo;

    private Vector3 InitialPosition;
    [SerializeField] private Vector3 InitialVelocity;
    [SerializeField] private Vector3 InitialAcceleration;

    private ParticleSystem ParticleSys;
    private Animator Anim;
    [SerializeField] private string AnimationState;

    // Use this for initialization
    void Start()
    {
        GameObject.Find("TimedObjectManager").GetComponent<TimedObjectManager>().RegisterObject(gameObject);
        InitialPosition = gameObject.GetComponent<Transform>().position;

        ParticleSys = gameObject.GetComponent<ParticleSystem>();
        if (ParticleSys)
        {
            //ParticleSys.randomSeed = 1;
            ParticleSys.Simulate(0, true, true);
        }

        Anim = gameObject.GetComponent<Animator>();
        if(Anim)
        {
            Anim.Play(AnimationState, -1, 0f);
            Anim.speed = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void MoveWithTime (float CurrentTime)
    {
        CurrentTime = Mathf.Clamp(CurrentTime, ActionTimeFrom, ActionTimeTo);

        CurrentTime -= ActionTimeFrom;
        GetComponent<Transform>().position = (InitialPosition) + (CurrentTime * InitialVelocity) + (.5f * CurrentTime * CurrentTime * InitialAcceleration);

        if (ParticleSys)
        {
            ParticleSys.Simulate((CurrentTime > 0f ? CurrentTime : 0f), true, (CurrentTime > 0f ? true : false));
        }

        if (Anim)
        {
            Anim.Play(AnimationState, -1, CurrentTime);
            Anim.speed = 0;
        }
    }
}

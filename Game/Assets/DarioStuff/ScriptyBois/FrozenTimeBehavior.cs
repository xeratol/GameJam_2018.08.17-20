using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenTimeBehavior : MonoBehaviour {

    //lerping the time for smoothness :3
    private float ObjectTime;
    private float TargetTime;
    private float alpha;
    private float lerpspeed;

    private Vector3 InitialPosition;

    [SerializeField] private Vector3 InitialVelocity;
    [SerializeField] private Vector3 InitialAcceleration;

    private ParticleSystem ParticleSys;
    private Animator Anim;
    [SerializeField]private string AnimationState;

    // Use this for initialization
    void Start()
    {
        GameObject.Find("TimedObjectManager").GetComponent<TimedObjectManager>().RegisterObject(gameObject);
        InitialPosition = gameObject.GetComponent<Transform>().position;

        TargetTime = 0f;
        ObjectTime = 0f;
        alpha = 1f;
        lerpspeed = 0.5f;

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
        if(!Mathf.Equals(alpha, 1f))
        {
            alpha += Time.deltaTime / lerpspeed;
            if (alpha > 1f)
            {
                alpha = 1f;
            }

            ObjectTime = Mathf.Lerp(ObjectTime, TargetTime, alpha);
            MoveWithTime(ObjectTime);
        }
	}

    public void SetTime (float CurrentTime)
    {
        TargetTime = CurrentTime;
        alpha = 0f;
    }

    private void MoveWithTime (float CurrentTime)
    {
        GetComponent<Transform>().position = (InitialPosition) + (CurrentTime * InitialVelocity) + (.5f * CurrentTime * CurrentTime * InitialAcceleration);

        if (ParticleSys)
        {
            ParticleSys.Simulate((CurrentTime > 0f ? CurrentTime : 0f), true, (CurrentTime > 0f ? true : false));
        }

        if (Anim)
        {
            //foreach (string s in AnimParamNames)
            //{
            Anim.Play(AnimationState, -1, CurrentTime);
            Anim.speed = 0;
            //}
        }
    }
}

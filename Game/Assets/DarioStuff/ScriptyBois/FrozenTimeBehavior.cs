using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenTimeBehavior : MonoBehaviour {

    [SerializeField] private float ActionTime;
    [Tooltip("Set to <= 0 for infinite duration")]
    [SerializeField] private float ActionDuration;
    [SerializeField] private bool willAct;
    private bool isActing;

    private Vector3 InitialPosition;
    [SerializeField] private Vector3 InitialVelocity;
    [SerializeField] private Vector3 InitialAcceleration;

    private ParticleSystem ParticleSys;

    private Animator Anim;
    [SerializeField] private string AnimationState;

    private SpriteRenderer ActivenessIndicator;
    private Sprite ActiveSprite;
    private Sprite InactiveSprite;
    [SerializeField] private Vector3 IndicatorOffset;

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

        GameObject SpriteChild = new GameObject("Activeness Indicator");
        ActivenessIndicator = SpriteChild.AddComponent<SpriteRenderer>() as SpriteRenderer;
        SpriteChild.transform.parent = gameObject.transform;
        SpriteChild.transform.localPosition = IndicatorOffset;

        ActivenessIndicator.sprite = willAct ? ActiveSprite : InactiveSprite;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void MoveWithTime (float CurrentTime)
    {
        if (!willAct)
        {
            return;
        }

        if (ActionDuration > 0)
        {
            CurrentTime = Mathf.Clamp(CurrentTime, ActionTime, ActionTime + ActionDuration);
        }
        else if (CurrentTime < ActionTime)
        {
            CurrentTime = ActionTime;
        }

        CurrentTime -= ActionTime;
        isActing = CurrentTime > 0;
        ActivenessIndicator.sprite = isActing ? null : ActiveSprite;

        GetComponent<Transform>().position = (InitialPosition) + (CurrentTime * InitialVelocity) + (.5f * CurrentTime * CurrentTime * InitialAcceleration);

        if (ParticleSys)
        {
            ParticleSys.Simulate(CurrentTime, true, true);
        }

        if (Anim)
        {
            Anim.Play(AnimationState, -1, CurrentTime);
            Anim.speed = 0;
        }
    }

    public void ToggleWantingToAct (float time)
    {
        if (isActing)
        {
            return;
        }

        willAct = !willAct;
        ActivenessIndicator.sprite = willAct ? ActiveSprite : InactiveSprite;
        
        if (willAct)
        {
            ActionTime = time;
        }
    }

    public void SetIndicatorSprites (Sprite Active, Sprite Inactive)
    {
        ActiveSprite = Active;
        InactiveSprite = Inactive;
    }

    public void MoreVisibleIndicator (bool enlarge)
    {
        ActivenessIndicator.transform.localScale = enlarge ? new Vector3(1.5f, 1.5f, 1.5f) : new Vector3(1f, 1f, 1f);
    }
}

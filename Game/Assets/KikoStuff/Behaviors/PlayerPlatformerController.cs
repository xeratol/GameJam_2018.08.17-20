using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private Animator animator;

    // Use this for initialization
    void Awake () 
    {
        animator = GetComponent<Animator> ();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis ("Horizontal");

        if (Input.GetButtonDown ("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButtonUp ("Jump")) 
        {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
            }
        }

        if(move.x > 0.01f)
        {
            var angles = transform.eulerAngles;
            angles.y = 0;
            transform.eulerAngles = angles;
        } 
        else if (move.x < -0.01f)
        {
            var angles = transform.eulerAngles;
            angles.y = 180;
            transform.eulerAngles = angles;
        }

        animator.SetBool ("InAir", !grounded);
        animator.SetBool ("Walk", !Mathf.Approximately(velocity.x, 0));

        targetVelocity = move * maxSpeed;
    }
}
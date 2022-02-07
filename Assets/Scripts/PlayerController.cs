using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;

    private float moveSpeed = 8f;
    private float rotateSpeed = 500f;

    private bool gettingInput;

    public Action<float> Walking;

    private Joystick joystick;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        joystick = GameObject.Find("Joystick").GetComponent<Joystick>();
    }

    private void Update()
    {
        Move();
        LookForward();
    }

    private void LookForward()
    {
        Vector3 target = rigidbody.velocity;

        if(target.sqrMagnitude > 0 && gettingInput)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target), rotateSpeed * Time.deltaTime);
    }

    private void Move()
    {
        float velX = joystick.value.x * moveSpeed;
        float velY = joystick.value.y * moveSpeed;

        if(joystick.value.sqrMagnitude > 0) 
        {
            gettingInput = true;
        }
        else 
        {
            gettingInput = false;
        }
        
        rigidbody.velocity = Vector3.forward * velY + Vector3.right * velX + Vector3.up * rigidbody.velocity.y;

        float walkingSpeed = rigidbody.velocity.sqrMagnitude / (moveSpeed*moveSpeed);
        if(gettingInput && walkingSpeed < 0.2f)
            walkingSpeed = 0.2f;

        Walking?.Invoke(walkingSpeed);
    }
}
